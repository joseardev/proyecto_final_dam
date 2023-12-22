using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace desktopTickets.utils
{

    //Clase para el envio de mails
    public class Mail
    {
        public static  void EnviarCorreo(string asunto, string cuerpo, string destino  )
        {
            var fromAddress = new MailAddress("jose.alonso.riveiro@ciclosmontecastelo.com", "Jose Alonso");
            var toAddress = new MailAddress(destino, "Jose Alonso");
            const string fromPassword = "CHUZOSDEPUNTA17-"; // Reemplaza con la contraseña real

            //configuracion correo
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587, // Reemplaza con el puerto correcto
                EnableSsl = true, // Depende de tu servidor SMTP
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = asunto,
                Body = cuerpo
            })
            {
                smtp.Send(message);
            }
        }
    }
}
