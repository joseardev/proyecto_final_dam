

using System.Windows.Media.Imaging;

namespace desktopTickets.Models
{
    public class ImagenAviso
    {
        public int ID_AvisoCab { get; set; }
        public int NumeroLinea { get; set; }
        public byte[] DatosImagen { get; set; } // Esta propiedad mapea a la columna IMAGEN de SQL
        public BitmapImage Imagen { get; set; } // No se mapea directamente, se calcula a partir de DatosImagen
    }


}
