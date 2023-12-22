using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorApp.models
{
    public class Usuario
    {
        public string USUARIO { get; set; }
        public string PERFIL { get; set; }
        public string PERMISOS_CREAR_TK { get; set; }
        public string PERMISOS_MODIFICAR_TK { get; set; }
        public string PERMISOS_BORRAR_TK { get; set; }
    }

}
