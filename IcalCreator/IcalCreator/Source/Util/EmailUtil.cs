using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using IcalCreator.Models;

namespace IcalCreator.Util
{
    /// <summary>
    /// Contiene métodos y utilidades para trabajar con e-mails 
    /// </summary>
    public class EmailUtil
    {
        /// <summary>
        /// Envía un correo con un evento de agenda adjunto. Este método envía sólo un correo
        /// </summary>
        /// <param name="emailSettings">Modelo con las configuraciones de e-mail</param>
        /// <param name="eventModel">Modelo con los datos del evento</param>
        public static void SendMail(EmailSettings emailSettings, EventModel eventModel)
        {
            IcalUtil icalGenerator = new IcalUtil();
            string CalendarEvent = icalGenerator.CreateCalendarEvent(eventModel);

            byte[] byteArray = Encoding.UTF8.GetBytes(CalendarEvent);
            MemoryStream stream = new MemoryStream(byteArray);
            Attachment attach = new Attachment(stream, "Event.ics");

            MailMessage message = new MailMessage(emailSettings.Remitente, emailSettings.Destinatario)
            {
                //Attachments.Add(attach); No se necesita si se usa el método de abajo
                Body = emailSettings.CuerpoMensaje
            };

            ContentType contentType = new ContentType("text/calendar");
            contentType.Parameters.Add("method", "REQUEST");

            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(CalendarEvent, contentType);
            message.AlternateViews.Add(alternateView);

            SmtpClient smtpclient = new SmtpClient
            {
                Host = emailSettings.ServidorSmtp,
                EnableSsl = emailSettings.ActivarSsl,
                Credentials = new NetworkCredential(emailSettings.NombreUsuario, emailSettings.ContrasenaUsuario),
                Port = emailSettings.PuertoSmtp 
            };

            smtpclient.Send(message);
        }

        /// <summary>
        /// Envía un correo con un evento de agenda adjunto a cada correo dentro del modelo
        /// de evento de agenda <see cref="EventModel"/>
        /// </summary>
        /// <param name="emailSettings">Modelo con las configuraciones de e-mail</param>
        /// <param name="eventModel">Modelo con los datos del evento</param>
        /// <param name="asistentes">Modelo con los datos de los asistentes</param>
        public static void SendMails(EmailSettings emailSettings, EventModel eventModel)
        {
            IcalUtil icalGenerator = new IcalUtil();
            string CalendarEvent = icalGenerator.CreateCalendarEvent(eventModel);
            MailMessage message = new MailMessage();

            foreach (var address in eventModel.Asistentes)
            {
                if (IsValidEmail(address.Correo.GetComponents(UriComponents.UserInfo | UriComponents.Host, UriFormat.SafeUnescaped).ToString()))
                {
                    MailAddress to = new MailAddress(address.Correo.GetComponents(UriComponents.UserInfo | UriComponents.Host, UriFormat.SafeUnescaped));
                    message.To.Add(to);
                }
            }

            message.From = new MailAddress(emailSettings.NombreUsuario);
            message.Body = StringUtil.FirstLetterToUpper(eventModel.Descripcion);
            message.Subject = emailSettings.AsuntoMensaje;
            message.To.Add(eventModel.CorreoOrganizador.GetComponents(UriComponents.UserInfo | UriComponents.Host, UriFormat.SafeUnescaped));

            ContentType contentType = new ContentType("text/calendar");
            contentType.Parameters.Add("method", "REQUEST");

            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(CalendarEvent, contentType);
            message.AlternateViews.Add(alternateView);

            SmtpClient smtpclient = new SmtpClient
            {
                Host = emailSettings.ServidorSmtp,
                EnableSsl = emailSettings.ActivarSsl,
                Credentials = new NetworkCredential(emailSettings.NombreUsuario, emailSettings.ContrasenaUsuario),
                Port = emailSettings.PuertoSmtp
            };

            smtpclient.Send(message);
        }


        /// <summary>
        /// Verifica si el texto proporcionado es un e-mail válido
        /// </summary>
        /// <param name="email">Cadena de texto con un e-mail</param>
        /// <returns><see langword="true"/> si es un e-mail válido, en caso contrario, <see langword="false"/></returns>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var address = new MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Convierte una dirección de correo de tipo <see cref="string"/> a <see cref="Uri"/>
        /// </summary>
        /// <param name="email"><see cref="string"/> con dirección de correo</param>
        /// <returns><see cref="Uri"/> con dirección de correo</returns>
        public static Uri StringToEmailUri(string email)
        {
            return new Uri("mailto:" + email);
        }
    }
}
