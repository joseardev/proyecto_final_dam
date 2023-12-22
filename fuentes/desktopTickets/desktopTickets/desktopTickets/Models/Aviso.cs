using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktopTickets.Models
{
    public class Aviso
    {
        public int ID { get; set; }
        public DateTime FECHA { get; set; }
        public string ESTADO { get; set; }
        public string USUARIO_SOLICITANTE { get; set; }
        public string TIPO_AVISO { get; set; }
        
        public string DESCRIPCION_TIPO_AVISO { get; set; }
        public string ORIGEN_AVISO { get; set; }

        public string ORIGEN_AVISO_DESCRIPCION { get; set; }

        public string OBSERVACIONES { get; set; }
    }
}
