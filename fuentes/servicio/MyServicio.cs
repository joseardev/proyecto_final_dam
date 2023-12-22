using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Diagnostics;
using System.Timers;
using Dapper;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace Tarea04ServiciosProcesos
{
    public partial class MyServicio : ServiceBase
    {
        private int eventId = 1;
        private Timer timer;
        private readonly string logFilePath = @"C:\Logs\MyServicioLog.txt"; // Asegúrate de que esta ruta exista y sea accesible

        public MyServicio()
        {
            InitializeComponent();
            //registro de eventos
            eventLog1 = new EventLog();
            if (!EventLog.SourceExists("TareaProyecto"))
            {
                EventLog.CreateEventSource("TareaProyecto", "TareaProyectoLog");
            }
            eventLog1.Source = "TareaProyecto";
            eventLog1.Log = "TareaProyectoLog";
        }

        protected override void OnStart(string[] args)
        {
            /
            Log("Servicio iniciado");            
            eventLog1.WriteEntry("Servicio iniciado.");
            timer = new Timer(30000); // 30 segundos
            timer.Elapsed += new ElapsedEventHandler(OnTimer);
            timer.Start();
        }

        protected override void OnStop()
        {
            Log("Servicio detenido.");
            eventLog1.WriteEntry("Servicio detenido.");
            timer.Stop();
            timer.Dispose();
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            Log("Ejecutando tarea programada.");
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
            ConsultarTabla();
        }

        //consulta datos tickets en estado pendiente y urgentes
        private void ConsultarTabla()
        {
            try
            {
                Log("ConsultarTabla");
                string connectionString = ConfigurationManager.ConnectionStrings["TicketsConnectionString"].ConnectionString;
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    string sqlQuery = "SELECT * FROM TK_AVISOS_CAB where urgente = 1 and estado = 'Pendiente'";
                    var avisos = db.Query<AvisoCabecera>(sqlQuery).ToList();
                    Log("ConsultarTabla.2");
                    foreach (var aviso in avisos)
                    {
                        Log("ConsultarTabla tiene datos");
                        string message = $"ID: {aviso.ID}, Fecha: {aviso.Fecha}, Estado: {aviso.Estado}";
                        Log(message);
                        eventLog1.WriteEntry(message, EventLogEntryType.Information);
                        EnviarCorreo("Tickets Urgentes", message);
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error al consultar la tabla: {ex.Message}";
                Log(errorMessage);
                eventLog1.WriteEntry(errorMessage, EventLogEntryType.Error);
            }
        }
        //funcion envio de emails
        private void EnviarCorreo(string asunto, string cuerpo)
        {
            //datos de envio
            var fromAddress = new MailAddress("jose.alonso.riveiro@ciclosmontecastelo.com", "Jose Alonso");
            var toAddress = new MailAddress("jose.alonso.riveiro@ciclosmontecastelo.com", "Jose Alonso");
            const string fromPassword = "CHUZOSDEPUNTA17-";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587, // puerto 
                EnableSsl = true, //servidor SMTP
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = asunto,
                Body = cuerpo
            })
            {
                smtp.Send(message);
            }
        }

        private void Log(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                // En caso de que no se pueda escribir en el archivo de log.
                EnviarCorreo("Error Logs", "Revisar escritura fichero log");
            }
        }

        ///Clases
        public class AvisoCabecera
        {
            public int ID { get; set; }
            public DateTime? Fecha { get; set; }
            public string Estado { get; set; }
            public string UsuarioSolicitante { get; set; }
            public string TipoAviso { get; set; }
            public string OrigenAviso { get; set; }
        }

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }
    }
}
