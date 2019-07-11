using System;
using System.Collections.Generic;
using IcalCreator.Models;
using IcalCreator.Models.Enums;
using IcalCreator.Util;

namespace IcalCreator.Test
{
    /// <summary>
    /// Clase de prueba para envío de correos 
    /// </summary>
    public class EmailSendTest
    {

        public EmailSendTest()
        {
            Send();
        }

        private Asistentes UnAsistente = new Asistentes()
        {
            Nombre = "Homero",
            EstadoParticipacion = EstadoParticipacion.Accepted,
            Rsvp = true,
            Correo = EmailUtil.StringToEmailUri("homero@simpson.com")
        };

        private Asistentes OtroAsistente = new Asistentes()
        {
            Nombre = "Lenny",
            EstadoParticipacion = EstadoParticipacion.Accepted,
            Rsvp = true,
            Correo = EmailUtil.StringToEmailUri("lenny@simpson.com")
        };

        

        public List<Asistentes> ListaAsistentes()
        {
            List<Asistentes> listaAsistentes = new List<Asistentes>() { UnAsistente, OtroAsistente };

            return listaAsistentes;
        }

        private void Send()
        {
            EventModel eventModel = new EventModel()
            {
                FechaInicio = DateTime.Now.AddDays(1),
                FechaFin = DateTime.Now.AddDays(2),
                TodoElDia = true,
                Transparencia = TransparencyType.Transparent,
                Descripcion = "Evento de prueba",
                Prioridad = PriorityType.Media,
                Visibilidad = VisibilityType.Publico,
                Ubicacion = "Guadalajara, Jalisco, México",
                Resumen = "Reunión de prueba para generar archivos *.ics",
                NombreOrganizador = "Homero Simpson",
                CorreoOrganizador = new Uri("mailto:homero@simpson.com"),
                Asistentes = ListaAsistentes(),
                AccionAlarma = AlarmAction.Audio,
                TipoAlerta = AlertType.DiezMinutosAntes,
                ResumenAlarma = "Reunión"
            };

            EmailSettings emailSettings = new EmailSettings()
            {
                NombreUsuario = "code.leak.data@gmail.com",
                ContrasenaUsuario = "",
                ServidorSmtp = "smtp.gmail.com",
                Destinatario = "hector.mtz.grc@gmail.com",
                Remitente = "code.leak.data@gmail.com",
                ActivarSsl = true,
                CuerpoMensaje = "Correo de prueba",
                AsuntoMensaje =  "Correo de prueba"
            };

            EmailUtil.SendMails(emailSettings, eventModel);
        }
    }
}
