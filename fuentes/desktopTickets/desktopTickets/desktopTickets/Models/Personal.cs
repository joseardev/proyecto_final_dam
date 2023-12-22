using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktopTickets.Models
{
    public class Personal
    {
        public string USUARIO { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDOS { get; set; }
        public string CENTRO { get; set; }
        public string PASSWORD { get; set; }
        public string PERFIL { get; set; }
        public bool PERMISOS_CREAR_TK { get; set; }
        public bool PERMISOS_MODIFICAR_TK { get; set; }
        public bool PERMISOS_BORRAR_TK { get; set; }
        public string MAIL { get; set; }
    }
}
