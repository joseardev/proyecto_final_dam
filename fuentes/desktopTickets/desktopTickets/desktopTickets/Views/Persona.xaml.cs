using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Data;
using Dapper;
using System.Configuration;
using System.Data.SqlClient;
using desktopTickets.ViewModels;
using System.Windows.Controls;
using System;

namespace desktopTickets.Views
{
    /// <summary>
    /// Lógica de interacción para persona.xaml
    /// </summary>

    public partial class Persona : Window
    {
        public Persona()
        {
            InitializeComponent();
            var viewModel = this.DataContext as PersonaViewModel;
            
            if (viewModel != null)
            {
                viewModel.RequestClose += ViewModel_RequestClose;
                viewModel.OnNavigate += HandleNavigation;
            }
            
        }
        private void ViewModel_RequestClose()
        {
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (this.DataContext is PersonaViewModel viewModel)
            {
                viewModel.RequestClose -= ViewModel_RequestClose;
                // Desregistrar otros eventos si es necesario
            }
        }
        // Este método se llamará cuando se dispare el evento OnNavigate del ViewModel.
        private void HandleNavigation(string destination)
        {
            UserControl newContent = null;

            // Dependiendo del destino, instancias el UserControl correspondiente.
            switch (destination)
            {
                case "AdminView":
                    newContent = new UserControl(); // Asumiendo que tienes un UserControl para la vista de administrador
                    break;
                
                    // Añadir más casos según sea necesario.
            }

         
            
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Pasamos el password por cada letra que se escribe
            var passwordBox = sender as PasswordBox;
            var viewModel = this.DataContext as PersonaViewModel;
            if (viewModel != null)
            {
                viewModel.Password = passwordBox.Password;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}