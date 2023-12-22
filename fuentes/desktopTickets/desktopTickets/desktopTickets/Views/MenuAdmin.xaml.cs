using desktopTickets.ViewModels;
using desktopTickets.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace desktopTickets.Views
{
    /// <summary>
    /// Lógica de interacción para MenuAdmin.xaml
    /// </summary>
    public partial class MenuAdmin : Window
    {
        string username;
        public MenuAdmin()
        {
            InitializeComponent();
            
            this.DataContext = new MainPageViewModel(username);
            // this.username = username;
        }
        public MenuAdmin(string username)
        {
            InitializeComponent();
            username = "admin";
            this.DataContext = new MainPageViewModel(username);
            this.username = username;
        }
        private void AdminPage_Click(object sender, RoutedEventArgs e)
        {
            // Abre la ventana de Administración
            AdminPage adminPage = new AdminPage();
            adminPage.Show();

        }

        private void MainPage_Click(object sender, RoutedEventArgs e)
        {
            //username = "";
            // Abre la ventana Principal
            //username = "Admin";
            MainPage mainPage = new MainPage(username);
            mainPage.Show();
        }
    }
}
