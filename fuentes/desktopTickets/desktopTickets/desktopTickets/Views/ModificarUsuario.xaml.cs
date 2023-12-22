using desktopTickets.Models;
using desktopTickets.ViewModels;
using System.Windows;
namespace desktopTickets.Views
{
    /// <summary>
    /// Lógica de interacción para ModificarUsuario.xaml
    /// </summary>
    public partial class ModificarUsuario : Window
    {       
        public ModificarUsuario(Usuario usuario)
        {
            InitializeComponent();
            // Aquí puedes asignar el objeto usuario a tu ViewModel de ModificarUsuario
            // y/o a los campos de la ventana para prellenarlos
            this.DataContext = new ModificarUsuarioViewModel(usuario);
        }

    }
}
