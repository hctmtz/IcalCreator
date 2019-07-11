using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using IcalCreator.Models;
using IcalCreator.Models.Enums;
using IcalCreator.Util;

namespace IcalCreator.Test
{
    /// <summary>
    /// Clase de prueba para generar un archivo de evento de agenda
    /// </summary>
    public class IcalCreateTest
    {
        private IcalUtil icalGenerator = new IcalUtil();

        public IcalCreateTest()
        {

        }

        private Asistentes UnAsistente = new Asistentes()
        {
            Nombre = "Homero",
            EstadoParticipacion = EstadoParticipacion.Accepted,
            Rsvp = true,
            Correo = new Uri("mailto:homero@simpson.com")
        };

        private Asistentes OtroAsistente = new Asistentes()
        {
            Nombre = "Lenny",
            EstadoParticipacion = EstadoParticipacion.Accepted,
            Rsvp = true,
            Correo = new Uri("mailto:lenny@simpson.com")
        };


        public List<Asistentes> ListaAsistentes()
        {
            List<Asistentes> listaAsistentes = new List<Asistentes>() { UnAsistente, OtroAsistente };

            return listaAsistentes;
        }


        public void CreateTestIcs()
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

            MessageBox.Show(icalGenerator.CreateCalendarEvent(eventModel));

            File.WriteAllText(Path.Combine(icalGenerator.CurrentDirectory, "Event.ics"), icalGenerator.CreateCalendarEvent(eventModel));
        }
    }
}
