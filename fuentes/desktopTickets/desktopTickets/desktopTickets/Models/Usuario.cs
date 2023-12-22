using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktopTickets.Models
{
    public class Usuario
    {
        public string USUARIO { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDOS { get; set; }
        public string CENTRO { get; set; }
        public string PASSWORD { get; set; } // Es importante considerar no mostrar la contraseña en la UI.
        public string PERFIL { get; set; }
        public bool PermisosCrearTk { get; set; }
        public bool PermisosModificarTk { get; set; }
        public bool PermisosBorrarTk { get; set; }
        public string MAIL { get; set; }
    }

}
