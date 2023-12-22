using Dapper;
using Newtonsoft.Json;
using ServidorApp.models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ServidorApp
{
    public partial class Form1 : Form
    {
        //  public String mensaje;
        comunicacion com = new comunicacion();

        public Form1()
        {
            InitializeComponent();
            // com.StartServer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // comunicacion com = new comunicacion(); 
                com.StartServer(textMensaje.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public class comunicacion
        {
            TcpListener tcpListener;
            public String mensaje;

            public bool StartServer(String cadena)
            {
                try
                {
                    tcpListener = new TcpListener(IPAddress.Any, 1234);
                    tcpListener.Start();
                    tcpListener.BeginAcceptTcpClient(new AsyncCallback(ProcessEvents), tcpListener);
                    Console.WriteLine("Escuchando el Puerto -> " + 1234);
                    mensaje = cadena;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.ToString());
                    return false;
                }
                return true;
            }

            public void ProcessEvents(IAsyncResult asyn)
            {
                try
                {
                    TcpListener processListen = (TcpListener)asyn.AsyncState;
                    TcpClient tcpClient = processListen.EndAcceptTcpClient(asyn);
                    NetworkStream myStream = tcpClient.GetStream();

                    if (myStream.CanRead)
                    {
                        StreamReader readerStream = new StreamReader(myStream);
                        string clientMessage = readerStream.ReadToEnd();
                        string connectionString = ConfigurationManager.ConnectionStrings["TicketsConnectionString"].ConnectionString;

                        using (IDbConnection db = new SqlConnection(connectionString))
                        {
                            Console.WriteLine(clientMessage);
                            if (clientMessage.StartsWith("GET_TICKETS"))
                            {
                                var parts = clientMessage.Split(':');
                                if (parts.Length > 1)
                                {
                                    var userName = parts[1].Trim();
                                    // Aquí, puedes usar `userName` para filtrar los tickets o para realizar alguna validación
                                    var tickets = db.Query<Ticket>("SELECT TOP 1000 * FROM [Tickets].[dbo].[TK_AVISOS_CAB] WHERE USUARIO_SOLICITANTE = @USUARIO ORDER BY urgente desc ", new { USUARIO = userName });
                                    string response = JsonConvert.SerializeObject(tickets);
                                    writeData(myStream, response);
                                }
                                else
                                {
                                    // Manejar el caso en que no se proporciona un nombre de usuario
                                    writeData(myStream, "Nombre de usuario no proporcionado");
                                }
                            }
                            else if (clientMessage.StartsWith("GET_DETAILS"))
                            {
                                var parts = clientMessage.Split(':');
                                if (parts.Length > 1)
                                {
                                    int ticketId = int.Parse(parts[1].Trim());
                                    var ticketDetails = db.Query<TicketConImagen>(
                                        "SELECT A.ID, A.NUMERO_LINEA, A.DETALLES, A.ESTADO " +
                                        "FROM [Tickets].[dbo].[TK_AVISOS_LIN] A " +
                                        "left JOIN [Tickets].[dbo].[TK_IMAGENES_AVISOS] I ON A.NUMERO_LINEA = I.NUMERO_LINEA " +
                                        "WHERE A.ID = @ID",
                                        new { ID = ticketId }
                                    );
                                    string response = JsonConvert.SerializeObject(ticketDetails);
                                    writeData(myStream, response);
                                }
                                else
                                {
                                    writeData(myStream, "ID no proporcionado");
                                }
                            }
                            else if (clientMessage.StartsWith("GET_USER")) // Aquí asumo que el mensaje vendría como "GET_USER:username"
                            {
                                var userToFetch = clientMessage.Split(':')[1].Trim();
                                var usuario = db.QuerySingleOrDefault<Usuario>(
                                    "SELECT * FROM [Tickets].[dbo].[TK_USUARIOS] WHERE USUARIO = @USUARIO",
                                    new { USUARIO = userToFetch }
                                );

                                if (usuario != null)
                                {
                                    string response = JsonConvert.SerializeObject(usuario);
                                    writeData(myStream, response);
                                }
                                else
                                {
                                    writeData(myStream, "El usuario no existe");
                                }
                            }
                            else if (clientMessage.StartsWith("CREATE_TICKET:"))
                            {
                                var ticketDataJson = clientMessage.Substring("CREATE_TICKET:".Length).Trim();
                                var newTicket = JsonConvert.DeserializeObject<Ticket>(ticketDataJson);
                                writeData(myStream, ticketDataJson);

                                if (db.State != ConnectionState.Open)
                                {
                                    db.Open();
                                }

                                using (var transaction = db.BeginTransaction())
                                {
                                    try
                                    {
                                        var ticketId = db.QuerySingle<int>(
                                            @"INSERT INTO [Tickets].[dbo].[TK_AVISOS_CAB]
                                                ([FECHA], [ESTADO], [USUARIO_SOLICITANTE], [TIPO_AVISO], [ORIGEN_AVISO], [URGENTE], [OBSERVACIONES])
                                            OUTPUT INSERTED.ID
                                            VALUES (GETDATE(), @ESTADO, @USUARIO_SOLICITANTE, @TIPO_AVISO, @ORIGEN_AVISO, @URGENTE, @OBSERVACIONES);",
                                            newTicket,
                                            transaction: transaction);

                                        db.Execute(
                                            @"INSERT INTO [Tickets].[dbo].[TK_AVISOS_LIN] (ID, DETALLES)
                                            VALUES (@ID, @DETALLES);",
                                            new { ID = ticketId, DETALLES = " " + newTicket.OBSERVACIONES },
                                            transaction: transaction);

                                        transaction.Commit();
                                        writeData(myStream, "Ticket y detalle creados con éxito. ID: " + ticketId);
                                    }
                                    catch (Exception ex)
                                    {
                                        transaction.Rollback();
                                        writeData(myStream, "Error al crear el ticket: " + ex.Message);
                                    }
                                }
                            }
                            else if (clientMessage.StartsWith("LOGIN:"))
                            {
                                Console.WriteLine("LOGIN");
                                var loginInfo = clientMessage.Substring("LOGIN:".Length).Trim().Split(':');
                                var username = loginInfo[0];
                                var password = loginInfo[1]; // Esto debería ser un hash en un escenario real

                                var usuario = db.QuerySingleOrDefault<Usuario>(
                                    "SELECT USUARIO, PERFIL, NOMBRE, APELLIDOS, CENTRO, PERMISOS_CREAR_TK, PERMISOS_MODIFICAR_TK, PERMISOS_BORRAR_TK FROM [Tickets].[dbo].[TK_USUARIOS] WHERE USUARIO = @USUARIO AND PASSWORD = @PASSWORD",
                                    new { USUARIO = username, PASSWORD = password }
                                );

                                if (usuario != null)
                                {
                                    // Usuario encontrado, inicio de sesión correcto
                                    string response = JsonConvert.SerializeObject(usuario);
                                    Console.WriteLine(response);
                                    writeData(myStream, response);
                                }
                                else
                                {
                                    // Usuario no encontrado, inicio de sesión fallido
                                    writeData(myStream, "Inicio de sesión fallido");
                                }
                            }
                            else
                            {
                                writeData(myStream, "Petición no reconocida");
                            }
                        }

                        readerStream.Close();
                    }

                    myStream.Close();
                    tcpClient.Close();

                    tcpListener.BeginAcceptTcpClient(new AsyncCallback(ProcessEvents), tcpListener);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error en ProcessEvents: " + e.ToString());
                }
            }

            public static void writeData(NetworkStream networkStream, string dataToClient)
            {
                Console.WriteLine("Procesando envío...");
                Console.WriteLine("Mensaje: " + dataToClient);
                Byte[] sendBytes = null;
                try
                {
                    sendBytes = Encoding.ASCII.GetBytes(dataToClient);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                    // consola("Enviado correctamente"); 
                    Console.WriteLine("Enviado al cliente correctamente");
                }
                catch (SocketException e)
                {
                    // consola("Ocurrio un error"); 
                    Console.WriteLine("Ocurrió un error en el envío al cliente, " + e);
                    throw;
                }
            }
        }

        private void textMensaje_TextChanged(object sender, EventArgs e)
        {
            com.mensaje = textMensaje.Text;
        }
    }
}
