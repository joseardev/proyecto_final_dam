using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorApp.models
{
    public class Ticket
    {
        // Elimina la propiedad FECHA si no se va a utilizar desde el JSON.
        public int ID { get; set; } // Considera si realmente necesitas el ID aquí.

        public string ESTADO { get; set; }
        public string USUARIO_SOLICITANTE { get; set; }
        public string TIPO_AVISO { get; set; }
        public string ORIGEN_AVISO { get; set; }

        public bool URGENTE { get; set; }
        
        public string OBSERVACIONES { get; set; }
        public string DETALLES { get; set; }
    }


}
