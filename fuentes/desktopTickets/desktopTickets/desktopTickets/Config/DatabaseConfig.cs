using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktopTickets.Config
{
    public static class DatabaseConfig
    {
        // Este método se utiliza para obtener la cadena de conexión a la base de datos
        // desde el archivo de configuración de la aplicación (generalmente App.config o Web.config).
        public static string GetConnectionString()
        {
            // Retorna la cadena de conexión obtenida del archivo de configuración.
            // "TicketsConnectionString"
            // la cadena de conexión en el archivo de configuración.
            return ConfigurationManager.ConnectionStrings["TicketsConnectionString"].ConnectionString;
        }
    }
}
