using desktopTickets.Config;
using desktopTickets.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Dapper;
using System.Data;

namespace desktopTickets.ViewModels
{
    public class RevisarTicketViewModel : INotifyPropertyChanged
    {
        //Datos que observa la vista
        private ObservableCollection<string> _estadosDisponibles;
        public ObservableCollection<string> EstadosDisponibles
        {
            get { return _estadosDisponibles; }
            set
            {
                _estadosDisponibles = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<AvisoLinea> _lineasAviso;
        public ObservableCollection<AvisoLinea> LineasAviso
        {
            get { return _lineasAviso; }
            set
            {
                _lineasAviso = value;
                OnPropertyChanged();
            }
        }

        private AvisoLinea _selectedLineaAviso;
        public AvisoLinea SelectedLineaAviso
        {
            get { return _selectedLineaAviso; }
            set
            {
                _selectedLineaAviso = value;
                OnPropertyChanged();
            }
        }

        //Comando de guardado
        public ICommand GuardarCambiosCommand { get; private set; }

        //peticion de cerrado 
        public event EventHandler RequestClose;
        public RevisarTicketViewModel(int ticketId)
        {
            CargarEstadosDisponibles();
            CargarLineasAviso(ticketId);
            GuardarCambiosCommand = new RelayCommand(GuardarCambios);
        }
        private void CargarEstadosDisponibles()
        {            
            EstadosDisponibles = new ObservableCollection<string> { "Pendiente", "Resuelto" };
        }
        //consulta TK_AVISOS_LIN
        private void CargarLineasAviso(int ticketId)
        {
            using (var db = new SqlConnection(DatabaseConfig.GetConnectionString()))
            {
                string query = "SELECT [ID], [NUMERO_LINEA], [DETALLES] FROM [Tickets].[dbo].[TK_AVISOS_LIN] WHERE ID = @Id";
                var lineasAviso = db.Query<AvisoLinea>(query, new { Id = ticketId }).ToList();
                LineasAviso = new ObservableCollection<AvisoLinea>(lineasAviso);
            }
        }

        //Actualiza los cambios realizados 
        private void GuardarCambios()
        {
            if (SelectedLineaAviso != null)
            {
                using (var db = new SqlConnection(DatabaseConfig.GetConnectionString()))
                {
                    string updateQuery = "UPDATE [Tickets].[dbo].[TK_AVISOS_LIN] SET DETALLES = @Detalles, ESTADO = @Estado WHERE ID = @Id";
                    db.Execute(updateQuery, new
                    {
                        Detalles = SelectedLineaAviso.Detalles,
                        Estado = SelectedLineaAviso.Estado,
                        Id = SelectedLineaAviso.ID
                    });
                }
                OnRequestClose();
            }
        }
        //invocador de cerrado
        protected virtual void OnRequestClose()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}