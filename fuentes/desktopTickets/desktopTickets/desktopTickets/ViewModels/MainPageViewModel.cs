using Dapper;
using desktopTickets.Config;
using desktopTickets.Models;
using desktopTickets.utils;
using desktopTickets.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace desktopTickets.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        //Datos que observa la vista
        private ObservableCollection<Aviso> _avisos;
        public ObservableCollection<Aviso> Avisos
        {
            get { return _avisos; }
            set
            {
                _avisos = value;
                OnPropertyChanged();
            }
        }

        private Usuario _userInfo;
        public Usuario UserInfo
        {
            get { return _userInfo; }
            set
            {
                _userInfo = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateStatusCommand { get; set; }
        public ICommand DeleteAvisoCommand { get; set; }
        public ICommand FiltrarCommand { get; set; }
        public ICommand ModifyUserInfoCommand { get; set; }
        public ICommand RevisarCommand { get; set; }

        public ObservableCollection<string> ListaEstados { get; set; } = new ObservableCollection<string>
        {
            "Pendiente",
            "Resuelto",
            // Otros estados que desees agregar
        };

        private string _filtroEstado;
        public string FiltroEstado
        {
            get { return _filtroEstado; }
            set
            {
                _filtroEstado = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _fechaInicio;
        public DateTime? FechaInicio
        {
            get { return _fechaInicio; }
            set
            {
                _fechaInicio = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _fechaFin;
        public DateTime? FechaFin
        {
            get { return _fechaFin; }
            set
            {
                _fechaFin = value;
                OnPropertyChanged();
            }
        }

        private string _estadoSeleccionado;
        public string EstadoSeleccionado
        {
            get { return _estadoSeleccionado; }
            set
            {
                _estadoSeleccionado = value;
                OnPropertyChanged();
            }
        }

        private Aviso _selectedAviso;
        public Aviso SelectedAviso
        {
            get { return _selectedAviso; }
            set
            {
                _selectedAviso = value;
                OnPropertyChanged();
            }
        }

        private string _loggedInUser;

        public MainPageViewModel(string username)
        {
            _loggedInUser = username;
            UpdateStatusCommand = new RelayCommand(UpdateStatus);
            DeleteAvisoCommand = new RelayCommand(DeleteAviso);
            FiltrarCommand = new RelayCommand(FiltrarAvisos);
            ModifyUserInfoCommand = new RelayCommand(ModifyUserInfo);
            RevisarCommand = new RelayCommand(RevisarTicket);

            LoadData();
            LoadUserInfo();
        }
        private void RevisarTicket()
        {
            if (SelectedAviso != null)
            {
                // Aquí creas y muestras la nueva ventana, pasando el ID del ticket seleccionado
                var revisarTicketWindow = new RevisarTicketWindow(SelectedAviso.ID);
                revisarTicketWindow.ShowDialog();
            }
            else
            {
                // Opcional: Mostrar algún mensaje de error si no hay un aviso seleccionado
            }
        }

        private void LoadData()
        {
            using (IDbConnection db = new SqlConnection(DatabaseConfig.GetConnectionString()))
            {
                // Modifica la consulta para incluir la descripción del tipo de aviso
                var query = @"
                        SELECT c.[ID], c.[FECHA], c.[ESTADO], c.[USUARIO_SOLICITANTE], c.[TIPO_AVISO], c.[ORIGEN_AVISO], t.[DESCRIPCION] AS DESCRIPCION_TIPO_AVISO,o.[DESCRIPCION] AS ORIGEN_AVISO_DESCRIPCION,c.[OBSERVACIONES] AS OBSERVACIONES
                        FROM [Tickets].[dbo].[TK_AVISOS_CAB] c
                        LEFT JOIN [Tickets].[dbo].[TK_TIPOS_AVISOS] t ON c.TIPO_AVISO = t.TIPO
                        LEFT JOIN [Tickets].[dbo].[TK_TIPOS_ORIGEN] o ON c.ORIGEN_AVISO = o.origen";

                var conditions = new List<string>();

                if (!string.IsNullOrEmpty(FiltroEstado))
                {
                    conditions.Add("c.ESTADO = @FiltroEstado");
                }
                if (FechaInicio.HasValue)
                {
                    conditions.Add("c.FECHA >= @FechaInicio");
                }
                if (FechaFin.HasValue)
                {
                    conditions.Add("c.FECHA <= @FechaFin");
                }
                // Filtrar por usuario logueado
                if (!string.IsNullOrEmpty(_loggedInUser) && _loggedInUser != "admin")
                {
                    conditions.Add("c.USUARIO_SOLICITANTE = @LoggedInUser");
                }
                if (conditions.Any())
                {
                    query += " WHERE " + string.Join(" AND ", conditions);
                }

                // Asegúrate de pasar los parámetros al método Query si están siendo usados en la consulta
                var parametros = new { FiltroEstado, FechaInicio, FechaFin, LoggedInUser = _loggedInUser };

                var avisos = db.Query<Aviso>(query, parametros).ToList();

                // Actualiza la colección de Avisos con los resultados
                Avisos = new ObservableCollection<Aviso>(avisos);
            }
        }

        private string GetMailFromUsername(string username)
        {
            using (IDbConnection db = new SqlConnection(DatabaseConfig.GetConnectionString()))
            {
                string query = "SELECT MAIL FROM [Tickets].[dbo].[TK_USUARIOS] WHERE USUARIO = @Username";
                var mail = db.QuerySingleOrDefault<string>(query, new { Username = username });

                return mail ?? string.Empty; // Retorna un string vacío si no se encuentra el mail
            }
        }
        private void LoadUserInfo()
        {
            using (IDbConnection db = new SqlConnection(DatabaseConfig.GetConnectionString()))
            {
                UserInfo = db.QuerySingle<Usuario>(@"
            SELECT [USUARIO], 
                   [PASSWORD], 
                   [PERFIL], 
                   [NOMBRE], 
                   [APELLIDOS], 
                   [CENTRO], 
                   CAST([PERMISOS_CREAR_TK] AS BIT) AS PermisosCrearTk,
                   CAST([PERMISOS_MODIFICAR_TK] AS BIT) AS PermisosModificarTk, 
                   CAST([PERMISOS_BORRAR_TK] AS BIT) AS PermisosBorrarTk,
                   [MAIL]
            FROM [Tickets].[dbo].[TK_USUARIOS] 
            WHERE [USUARIO] = @Username", new { Username = _loggedInUser });
            }
        }
        private void ModifyUserInfo()
        {
            // Crear una instancia de la ventana ModificarUsuario y pasarle la información del usuario actual
            var modificarUsuarioWindow = new ModificarUsuario(UserInfo);

            // Mostrar la ventana de edición
            var result = modificarUsuarioWindow.ShowDialog();

            // Si el usuario guardó los cambios en la ventana de edición, recargar la información del usuario
            if (result.HasValue && result.Value)
            {
                LoadUserInfo();
            }
        }

        private void UpdateStatus()
        {
            // Comprueba si el usuario tiene permisos para modificar
            if (!UserInfo.PermisosModificarTk)
            {
                MessageBox.Show("No tienes permisos para modificar el estado de los avisos.", "Permiso Denegado", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SelectedAviso != null && !string.IsNullOrEmpty(EstadoSeleccionado))
            {
                SelectedAviso.ESTADO = EstadoSeleccionado;

                using (IDbConnection db = new SqlConnection(DatabaseConfig.GetConnectionString()))
                {
                    string query = "UPDATE [Tickets].[dbo].[TK_AVISOS_CAB] SET ESTADO = @Estado WHERE ID = @Id";
                    db.Execute(query, new { Estado = SelectedAviso.ESTADO, Id = SelectedAviso.ID });
                }
                Mail.EnviarCorreo("Cambio de estado "+ SelectedAviso.ESTADO, SelectedAviso.ID.ToString(), GetMailFromUsername(SelectedAviso.USUARIO_SOLICITANTE));                
                LoadData();
            }
        }


        private void DeleteAviso()
        {
            // Comprueba si el usuario tiene permisos para borrar
            if (!UserInfo.PermisosBorrarTk)
            {
                MessageBox.Show("No tienes permisos para borrar avisos.", "Permiso Denegado", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SelectedAviso != null)
            {
                // Mostrar cuadro de diálogo de confirmación
                var result = MessageBox.Show("¿Estás seguro de querer borrar este aviso?", "Confirmación", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (IDbConnection db = new SqlConnection(DatabaseConfig.GetConnectionString()))
                    {
                        string query = "DELETE FROM [Tickets].[dbo].[TK_AVISOS_CAB] WHERE ID = @Id";
                        db.Execute(query, new { Id = SelectedAviso.ID });
                        Mail.EnviarCorreo("AVISO BORRADO ", SelectedAviso.ID.ToString(), GetMailFromUsername(SelectedAviso.USUARIO_SOLICITANTE));
                    }

                    LoadData();
                }
            }
        }



        private void FiltrarAvisos()
        {
            LoadData();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class RelayCommand : ICommand
        {
            private readonly Action _execute;

            public RelayCommand(Action execute)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                _execute();
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }
    }
}
