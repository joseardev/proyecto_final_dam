using Dapper;
using desktopTickets.Config;
using desktopTickets.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace desktopTickets.ViewModels
{
    public class AdminPageViewModel : INotifyPropertyChanged
    {
        private string _newUsername;
        private string _newNombre;
        private string _newApellidos;
        private string _newCentro;
        private string _newPassword;
        private Personal _selectedUser;
        private List<string> _perfiles;

        public string NewUsername
        {
            get { return _newUsername; }
            set { _newUsername = value; OnPropertyChanged(); }
        }

        public string NewNombre
        {
            get { return _newNombre; }
            set { _newNombre = value; OnPropertyChanged(); }
        }

        public string NewApellidos
        {
            get { return _newApellidos; }
            set { _newApellidos = value; OnPropertyChanged(); }
        }

        public string NewCentro
        {
            get { return _newCentro; }
            set { _newCentro = value; OnPropertyChanged(); }
        }

        public string NewPassword
        {
            get { return _newPassword; }
            set { _newPassword = value; OnPropertyChanged(); }
        }

        private string _newPerfil;

        public string NewPerfil
        {
            get { return _newPerfil; }
            set
            {
                _newPerfil = value; OnPropertyChanged();
            }
        }

        private string _newMail;
        public string NewMail
        {
            get { return _newMail; }
            set
            {
                if (_newMail != value)
                {
                    _newMail = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _newCrear;

        public bool NewCrear
        {
            get { return _newCrear; }
            set
            {
                _newCrear = value; OnPropertyChanged();
            }
        }

        private bool _newModificar;

        public bool NewModificar
        {
            get { return _newModificar; }
            set
            {
                _newModificar = value; OnPropertyChanged();
            }
        }

        private bool _newDelete;

        public bool NewDelete
        {
            get { return _newDelete; }
            set
            {
                _newDelete = value; OnPropertyChanged();
            }
        }

        public List<string> Perfiles
        {
            get { return _perfiles; }
            set
            {
                _perfiles = value;
                OnPropertyChanged(nameof(Perfiles));
            }
        }

        public Personal SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                if (_selectedUser != null)
                {
                    NewUsername = _selectedUser.USUARIO;
                    NewNombre = _selectedUser.NOMBRE;
                    NewApellidos = _selectedUser.APELLIDOS;
                    NewCentro = _selectedUser.CENTRO;
                    NewPassword = _selectedUser.PASSWORD;                    
                    NewCrear = _selectedUser.PERMISOS_CREAR_TK;
                    NewModificar = _selectedUser.PERMISOS_MODIFICAR_TK;
                    NewDelete = _selectedUser.PERMISOS_BORRAR_TK;
                    NewMail = _selectedUser.MAIL;
                    NewPerfil =_selectedUser.PERFIL;
                }
                OnPropertyChanged();
            }
        }

        public ICommand AddUserCommand { get; set; }
        public ICommand UpdateUserCommand { get; set; }

        public ICommand DeleteUserCommand { get; set; }
        public List<Personal> PersonalList { get; set; } = new List<Personal>();

        public AdminPageViewModel()
        {
            AddUserCommand = new RelayCommand(AddUser);
            UpdateUserCommand = new RelayCommand(UpdateUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);
            Perfiles = new List<string> { "Administrador", "Soporte", "Cliente" };
            LoadPersonalList();
        }

        //agregar un nuevo usuario a la base de datos.
        private void AddUser()
        {
            // Verifica los inputs del usuario. Si no son válidos, termina la ejecución del método.
            if (!ValidateUserInput()) return;

            try
            {              
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.GetConnectionString()))
                {                                 
                    string query = "INSERT INTO TK_USUARIOS (USUARIO, NOMBRE, APELLIDOS, CENTRO, PASSWORD, PERFIL, PERMISOS_CREAR_TK, PERMISOS_MODIFICAR_TK, PERMISOS_BORRAR_TK, MAIL) VALUES (@Username, @Nombre, @Apellidos, @Centro, @Password, @Perfil, @PermCrear, @PermModificar, @PermBorrar, @Mail)";

                    // Ejecuta la consulta SQL con los parámetros proporcionados, representando los datos del nuevo usuario.
                    connection.Execute(query, new
                    {
                        Username = NewUsername,
                        Nombre = NewNombre,
                        Apellidos = NewApellidos,
                        Centro = NewCentro,
                        Password = NewPassword,
                        Perfil = NewPerfil,
                        PermCrear = NewCrear,
                        PermModificar = NewModificar,
                        PermBorrar = NewDelete,
                        Mail = NewMail
                    });
                }

                // Carga la lista de personal, posiblemente para actualizar la interfaz de usuario.
                LoadPersonalList();

                // Muestra un mensaje de éxito al usuario.
                MessageBox.Show("Usuario agregado con éxito.");
            }
            catch (Exception ex)
            {
                // En caso de excepción, muestra un mensaje de error.
                MessageBox.Show("Error al agregar usuario: " + ex.Message);
            }
        }

        // validar los datos de entrada del usuario.
        private bool ValidateUserInput()
        {
            // Verifica si alguno de los campos requeridos (nombre de usuario, nombre, apellidos, centro, contraseña, perfil)           
            if (string.IsNullOrWhiteSpace(NewUsername) ||
                string.IsNullOrWhiteSpace(NewNombre) ||
                string.IsNullOrWhiteSpace(NewApellidos) ||
                string.IsNullOrWhiteSpace(NewCentro) ||
                string.IsNullOrWhiteSpace(NewPassword) ||
                string.IsNullOrWhiteSpace(NewPerfil) ||
                !IsValidEmail(NewMail))
            {
                // Si alguno de los campos no cumple con los criterios de validación, muestra un mensaje de error 
                MessageBox.Show("Todos los campos son requeridos y el correo electrónico debe ser válido.");
                return false;
            }
            return true;
        }


        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        }

        private void UpdateUser()
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("No hay ningún usuario seleccionado para actualizar.");
                return;
            }

            if (!ValidateUserInput()) return;

            try
            {
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.GetConnectionString()))
                {
                    string query = "UPDATE TK_USUARIOS SET NOMBRE=@Nombre, APELLIDOS=@Apellidos, CENTRO=@Centro, PASSWORD=@Password, PERFIL=@Perfil, PERMISOS_CREAR_TK=@PermCrear, PERMISOS_MODIFICAR_TK=@PermModificar, PERMISOS_BORRAR_TK=@PermBorrar, MAIL=@Mail WHERE USUARIO=@Username";
                    connection.Execute(query, new
                    {
                        Username = NewUsername,
                        Nombre = NewNombre,
                        Apellidos = NewApellidos,
                        Centro = NewCentro,
                        Password = NewPassword,
                        Perfil = NewPerfil,
                        PermCrear = NewCrear,
                        PermModificar = NewModificar,
                        PermBorrar = NewDelete,
                        Mail = NewMail
                    });
                }
                LoadPersonalList();
                MessageBox.Show("Usuario actualizado con éxito.");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al actualizar el usuario en la base de datos: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado al actualizar el usuario: " + ex.Message);
            }
        }

        private void DeleteUser()
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("No hay ningún usuario seleccionado para eliminar.");
                return;
            }

            try
            {
                var result = MessageBox.Show("¿Estás seguro de que deseas eliminar este usuario?", "Confirmación", MessageBoxButton.YesNo);
                if (result != MessageBoxResult.Yes)
                    return;

                using (SqlConnection connection = new SqlConnection(DatabaseConfig.GetConnectionString()))
                {
                    string query = "DELETE FROM TK_USUARIOS WHERE USUARIO=@Username";
                    connection.Execute(query, new { Username = SelectedUser.USUARIO });
                }
                LoadPersonalList();
                MessageBox.Show("Usuario eliminado con éxito.");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al eliminar el usuario en la base de datos: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado al eliminar el usuario: " + ex.Message);
            }
        }

        private void LoadPersonalList()
        {
            using (SqlConnection connection = new SqlConnection(DatabaseConfig.GetConnectionString()))
            {
                PersonalList = connection.Query<Personal>("SELECT TOP (1000) [USUARIO], [NOMBRE], [APELLIDOS], [CENTRO], [PASSWORD], [PERFIL], [PERMISOS_CREAR_TK], [PERMISOS_MODIFICAR_TK], [PERMISOS_BORRAR_TK], MAIL FROM [Tickets].[dbo].[TK_USUARIOS]").ToList();
            }
            OnPropertyChanged(nameof(PersonalList));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
