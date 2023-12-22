using desktopTickets.Models;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Configuration;
using Dapper;
using desktopTickets.Views;
using desktopTickets.Config;

namespace desktopTickets.ViewModels
{
    public class ModificarUsuarioViewModel : INotifyPropertyChanged
    {
        private Usuario _usuario;
        public Usuario Usuario
        {
            get { return _usuario; }
            set
            {
                _usuario = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; set; }

        public ModificarUsuarioViewModel(Usuario usuario)
        {
            Usuario = usuario;
            SaveCommand = new RelayCommand(SaveChanges);
        }

        private void SaveChanges()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(DatabaseConfig.GetConnectionString()))
                {
                    string query = @"UPDATE [Tickets].[dbo].[TK_USUARIOS]
                             SET [NOMBRE] = @NOMBRE, [APELLIDOS] = @APELLIDOS, [CENTRO] = @CENTRO, [PERFIL] = @PERFIL
                             WHERE [USUARIO] = @USUARIO";

                    db.Execute(query, Usuario);

                    // Cierra la ventana después de guardar los cambios
                    // Puedes obtener la ventana actual del ViewModel usando Application.Current.Windows
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(ModificarUsuario))
                        {
                            window.Close();
                            break;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejo de excepciones SQL: Muestra un mensaje de error en caso de una excepción SQL.
                MessageBox.Show("Error SQL al guardar los cambios: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones generales: Muestra un mensaje de error en caso de una excepción no esperada.
                MessageBox.Show("Error al guardar los cambios: " + ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Define la clase RelayCommand que implementa la interfaz ICommand.
        // Esta clase se utiliza para crear comandos en aplicaciones WPF o similares,
        // permitiendo una separación clara entre la lógica de los comandos y la interfaz de usuario.
        public class RelayCommand : ICommand
        {
            // Campo privado para almacenar la acción a ejecutar.
            private readonly Action _execute;

            // Constructor que recibe una acción a ejecutar. 
            // Si la acción es null, lanza una excepción para prevenir estados inválidos.
            public RelayCommand(Action execute)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            }

            // Método CanExecute que determina si el comando puede ejecutarse.
            // En esta implementación, siempre retorna true, indicando que el comando siempre está habilitado.
            public bool CanExecute(object parameter)
            {
                return true;
            }

            // Método Execute que ejecuta la acción definida.
            // Se invoca cuando se activa el comando.
            public void Execute(object parameter)
            {
                _execute();
            }

            // Evento CanExecuteChanged que se dispara cuando cambia la posibilidad de ejecutar el comando.
            // Aquí, se suscribe y se desuscribe del evento RequerySuggested del CommandManager.
            // Esto asegura que el comando se reevalúe automáticamente en ciertas condiciones del sistema,
            // como cambios en el foco de los controles de la interfaz de usuario.
            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }
    }
}
