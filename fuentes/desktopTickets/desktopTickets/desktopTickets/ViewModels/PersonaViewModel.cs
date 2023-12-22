using Dapper;
using desktopTickets.Config;
using desktopTickets.Views;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace desktopTickets.ViewModels
{
    public class PersonaViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<string> OnNavigate; // Evento para manejar la navegación

        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
        public event Action RequestClose;
        public ICommand LoginCommand { get; set; }

        public PersonaViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        private void Login()
        {
            try
            {
                // Intenta verificar las credenciales y obtener el perfil de usuario.
                var (isSuccessful, userProfile) = VerifyCredentials(Username, Password);

                // Si las credenciales son correctas...
                if (isSuccessful)
                {
                    // Si el usuario es un administrador, muestra la ventana de administración.
                    if (userProfile == "Administrador")
                    {
                        MenuAdmin menuAdmin = new MenuAdmin(Username);
                        menuAdmin.Show();                       
                    }
                    // Si no es administrador, muestra la ventana principal.
                    else
                    {
                        MainPage mainPage = new MainPage(Username);
                        mainPage.Show();
                    }
                    RequestClose?.Invoke();
                }
                // Si las credenciales no son correctas, muestra un mensaje de error.
                else
                {
                    MessageBox.Show("Acceso denegado. Usuario o contraseña incorrectos.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones: Muestra un mensaje de error en caso de una excepción no esperada.
                MessageBox.Show("Error al iniciar sesión: " + ex.Message);
            }
        }

        private (bool IsSuccessful, string UserProfile) VerifyCredentials(string username, string password)
        {
            try
            {
                // Establece una conexión a la base de datos utilizando la cadena de conexión.
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.GetConnectionString()))
                {
                    // Define la consulta para obtener el perfil y contraseña del usuario basado en el nombre de usuario.
                    string query = "SELECT PERFIL, PASSWORD FROM TK_USUARIOS WHERE USUARIO = @Username";

                    // Ejecuta la consulta y recupera el perfil y contraseña del usuario.
                    var user = connection.QuerySingleOrDefault<(string PERFIL, string PASSWORD)>(query, new { Username = username });

                    // Verifica si la contraseña proporcionada coincide con la contraseña almacenada en la base de datos.
                    if (!VerifyPassword(password, user.PASSWORD))
                        return (false, null); // Si no coincide, devuelve falso y nulo.

                    // Si la contraseña es correcta, devuelve verdadero y el perfil del usuario.
                    return (true, user.PERFIL);
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones: Muestra un mensaje de error en caso de una excepción no esperada.
                MessageBox.Show("Error al verificar las credenciales: " + ex.Message);
                return (false, null);
            }
        }

        private bool VerifyPassword(string providedPassword, string storedHash)
        {
            //TODO(FALTA HASH)
            // Implementar la verificación del hash aquí.
            return providedPassword == storedHash;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Define una clase RelayCommand que implementa la interfaz ICommand.
    public class RelayCommand : ICommand
    {
        // Campo privado que almacena una referencia a un método que no retorna un valor y no acepta parámetros.
        private readonly Action _execute;

        // Constructor que asigna el método a ejecutar al campo _execute.
        public RelayCommand(Action execute)
        {
            // Asignar el método de acción pasada al campo privado _execute.
            _execute = execute;
        }

        // Método de la interfaz ICommand que determina si el comando puede ser ejecutado.
        // En este caso, siempre retorna true, lo que indica que el comando siempre está habilitado.
        public bool CanExecute(object parameter) => true;

        // Método de la interfaz ICommand que ejecuta la acción asignada al comando.
        public void Execute(object parameter)
        {
            // Ejecutar el método asignado al campo _execute.
            _execute();
        }

        // Evento de la interfaz ICommand que se puede disparar cuando cambia la disponibilidad de ejecución del comando.
        // No está implementado en este caso, por lo que no se utiliza.
        public event EventHandler CanExecuteChanged;

        // Método para disparar manualmente el evento CanExecuteChanged.
        // Esto es útil si la lógica de CanExecute depende de alguna condición externa.
        public void OnCanExecuteChanged()
        {
            // Disparar el evento CanExecuteChanged para notificar que la posibilidad de ejecución ha cambiado.
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
