using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IcalCreator.Data;
using IcalCreator.Log;
using IcalCreator.Models;
using IcalCreator.Models.Enums;
using IcalCreator.Param;
using IcalCreator.Settings;
using IcalCreator.Util;

namespace IcalCreator.Controller
{
    /// <summary>
    /// Contiene los métodos para obtener los identificadores de
    /// las citas médicas, los datos de los asistentes y los datos del evento,
    /// así cómo los métodos de envío
    /// </summary>
    public class CitaEventGenerator
    {
        private EventModel eventModel = new EventModel();

        /// <summary>
        /// Contiene los métodos para obtener los identificadores de
        /// las citas médicas, los datos de los asistentes y los datos del evento,
        /// así cómo los métodos de envío
        /// </summary>
        public CitaEventGenerator()
        {
            /// 👨‍💻 Autor: Héctor Martínez - < hector.mtz.grc@gmail.com >
        }

        /// <summary>
        /// Obtiene datos, genera y envía un evento de agenda. 
        /// Se usa para interfaz gráfica.
        /// </summary>
        /// <param name="episodio">Episodio/Evento/Identificador de cirugía/procedimiento médico</param>
        public bool SendEvent(int episodio)
        {
            List<EventModel> eventModel = GetEventData(episodio);

            try
            {
                EmailSettings emailSettings = new EmailSettings()
                {
                    NombreUsuario = EmailValues.EmailUser,
                    ContrasenaUsuario = EmailValues.EmailPassword,
                    ServidorSmtp = EmailValues.SmtpServer,
                    PuertoSmtp = int.Parse(EmailValues.SmtpPort),
                    ActivarSsl = EmailValues.SslActive == "1" ? true : false,
                    AsuntoMensaje = "Recordatorio de cirugía programada",   //Placeholder
                    CuerpoMensaje = "Recordatorio de cirguía programada",   //Placeholder
                    Remitente = EmailValues.EmailUser
                };

                foreach (var events in eventModel)
                {
                    EmailUtil.SendMails(emailSettings, events);
                }

                return true;
            }
            catch (Exception e)
            {
                LogUtil.CreateErrorLog(AppSettingsValues.SettingsDirectory, "IcalCreator", e);
                return false;
            }
        }

        /// <summary>
        /// Obtiene datos, genera y envía un evento de agenda. 
        /// Se usa para línea de comandos.
        /// </summary>
        /// <param name="episodio">Episodio/Evento/Identificador de cirugía/procedimiento médico</param>
        public void SendEventCommandLine(int episodio)
        {
            List<EventModel> eventModel = GetEventData(episodio);

            try
            {
                EmailSettings emailSettings = new EmailSettings()
                {
                    NombreUsuario = EmailValues.EmailUser,
                    ContrasenaUsuario = EmailValues.EmailPassword,
                    ServidorSmtp = EmailValues.SmtpServer,
                    PuertoSmtp = int.Parse(EmailValues.SmtpPort),
                    ActivarSsl = EmailValues.SslActive == "1" ? true : false,
                    AsuntoMensaje = "Recordatorio de cita médica",
                    CuerpoMensaje = "Recordatorio de cita médica"
                };

                foreach (var events in eventModel)
                {
                    EmailUtil.SendMails(emailSettings, events);
                }
            }
            catch (Exception e)
            {
                LogUtil.CreateErrorLog(AppSettingsValues.SettingsDirectory, "IcalCreator", e);
            }
        }

        /// <summary>
        /// Obtiene datos necesarios para llenar el modelo <see cref="EventModel"/>
        /// y generar un evento de agenda. En este método se asume el valor de varios 
        /// datos y se retorna un valor predeterminado
        /// </summary>
        /// <param name="episodio">Episodio/Evento/Identificador de cirugía/procedimiento médico</param>
        /// <returns>Colección de datos de evento de agenta</returns>
        public List<EventModel> GetEventData(int episodio)
        {
            List<EventModel> eventModel = new List<EventModel>();
            Persistance persistance = new Persistance();

            using (var dataContext = persistance.GetObjectContext())
            {
                try
                {
                    eventModel = (from P in dataContext.ppGenerarEventoAgendaCita(episodio)
                                  select new EventModel
                                  {
                                      FechaInicio = (DateTime)P.dtFechaInicio,
                                      FechaFin = (DateTime)P.dtFechaFin,
                                      TodoElDia = false,
                                      Secuencia = 0,
                                      Transparencia = TransparencyType.Transparent,
                                      Descripcion = P.vchDescripcion,
                                      Prioridad = PriorityType.Media,
                                      Visibilidad = VisibilityType.Publico,
                                      Ubicacion = P.vchUbicacion,
                                      Resumen = P.vchResumen,
                                      NombreOrganizador = P.vchNombreOrganizador,
                                      CorreoOrganizador = EmailUtil.StringToEmailUri(P.vchCorreoOrganizador),
                                      Asistentes = GetAttendees(episodio),
                                      AccionAlarma = AlarmAction.Display,
                                      TipoAlerta = AlertType.UnaHoraAntes,
                                      ResumenAlarma = P.vchResumenAlerta
                                  }).ToList();
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
            return eventModel;
        }

        /// <summary>
        /// Obtiene los datos necesarios para llenar el modelo de <see cref="Asistentes"/>
        /// </summary>
        /// <param name="episodio">Episodio/Evento/Identificador de cirugía/procedimiento médico</param>
        /// <returns>Colección de datos de asistentes</returns>
        public List<Asistentes> GetAttendees(int episodio)
        {
            List<Asistentes> Asistentes = new List<Asistentes>();
            Persistance persistance = new Persistance();

            using (var dataContext = persistance.GetObjectContext())
            {
                try
                {
                    Asistentes = (from Q in dataContext.ppObtenerAsistentesCitaAgenda(episodio)
                                  select new Asistentes
                                  {
                                      Nombre = Q.vchNombre,
                                      Correo = EmailUtil.StringToEmailUri(Q.vchCorreo),
                                      EstadoParticipacion = (EstadoParticipacion) Q.iEstadoParticipación,
                                      Rsvp = Q.iRsvp == 1 ? true : false
                                  }).ToList();
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
            return Asistentes;
        }

        /// <summary>
        /// Retorna los eventos de cita médica para
        /// envíar notificaciones
        /// </summary>
        /// <returns></returns>
        public List<Evento> GetEvents()
        {
            List<Evento> Eventos = new List<Evento>();
            Persistance persistance = new Persistance();

            using (var dataContext = persistance.GetObjectContext())
            {
                try
                {
                    Eventos = (from Q in dataContext.ppObtenerCitaMedicaAgenda()
                               select new Evento
                               {
                                   FolioEvento = (int) Q.iCodCita
                               }).ToList();
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
            return Eventos;
        }
    }
}
