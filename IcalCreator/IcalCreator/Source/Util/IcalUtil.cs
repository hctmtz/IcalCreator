using System;
using System.IO;
using System.Reflection;
using Ical.Net;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Ical.Net.Serialization.iCalendar.Serializers;
using IcalCreator.Models;

namespace IcalCreator.Util
{
    /// <summary>
    /// Contiene métodos y atributos útiles para crear archivos
    /// en formato ICalendar (*.ics)
    /// </summary>
    public class IcalUtil
    {
        /// <summary>
        /// Modelo para evento de agenda
        /// </summary>
        private EventModel eventModel = new EventModel();

        /// <summary>
        /// Directorio actual del ejecutable
        /// </summary>
        public string CurrentDirectory { get; } = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        public IcalUtil()
        {
            //👨‍💻 Autor: Héctor Martínez - <hector.mtz.grc@gmail.com>
        }

        /// <summary>
        /// Crea un archivo de evento de agenda en la ruta proporcionada
        /// </summary>
        /// <param name="eventModel">Modelo de evento de agenda</param>
        /// <param name="path">Ruta dónde se guardará el archivo de evento de agenda</param>
        public void CreateCalendarEventFile(EventModel eventModel, string path)
        {
            File.WriteAllText(Path.Combine(path, "Event.ics"), CreateCalendarEvent(eventModel));
        }

        /// <summary>
        /// Crea un evento de tipo ICal compatible con diferentes tipos de agendas
        /// </summary>
        /// <param name="eventModel">Modelo con los datos necesarios para crear el formato</param>
        /// <returns>Formato de ICal</returns>
        public string CreateCalendarEvent(EventModel eventModel)
        {
            DateTime startDate = eventModel.FechaInicio;
            DateTime endDate = eventModel.FechaFin;

            Event calendarEvent = new Event()
            {
                DtStart = new CalDateTime(startDate),
                DtEnd = new CalDateTime(endDate),
                DtStamp = new CalDateTime(DateTime.Now),
                IsAllDay = eventModel.TodoElDia,
                Sequence = eventModel.Secuencia,
                Transparency = (TransparencyType) eventModel.Transparencia,
                Description = eventModel.Descripcion,
                Priority = (int) eventModel.Prioridad,
                Class = EnumUtil.GetEnumDescription(eventModel.Visibilidad),
                Location = eventModel.Ubicacion,
                Summary = eventModel.Resumen,
                Uid = Guid.NewGuid().ToString(),
                Organizer = new Organizer()
                {
                    CommonName = eventModel.NombreOrganizador,
                    Value = eventModel.CorreoOrganizador
                }
            };

            foreach (var Asistentes in eventModel.Asistentes)
            {
                calendarEvent.Attendees.Add(new Attendee()
                {
                    CommonName = Asistentes.Nombre,
                    ParticipationStatus = EnumUtil.GetEnumDescription(Asistentes.EstadoParticipacion),
                    Rsvp = Asistentes.Rsvp,
                    Value = Asistentes.Correo
                });
            }

            Alarm alarm = new Alarm()
            {
                Action = (AlarmAction) eventModel.AccionAlarma,
                Trigger = eventModel.TipoAlerta,
                Summary = eventModel.ResumenAlarma
            };

            calendarEvent.Alarms.Add(alarm);
            Calendar calendar = new Calendar();
            calendar.Events.Add(calendarEvent);

            CalendarSerializer serializer = new CalendarSerializer(new SerializationContext());

            return serializer.SerializeToString(calendar);
        }
    }
}
