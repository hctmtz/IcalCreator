using System;
using Ical.Net.DataTypes;

namespace IcalCreator.Models
{
    /// <summary>
    /// Representa el recordatorio de una alarma y su lapso de tiempo 
    /// previo a la hora en que se programó el evento
    /// </summary>
    public class AlertType
    {
        public AlertType()
        {
            ///Los datos de esta clase no deben ser
            ///modificados; son de sólo lectura únicamente
        }

        /// <summary>
        /// Ejecutar alarma a la hora exacta
        /// </summary>
        public static Trigger HoraExacta { get; } = new Trigger(TimeSpan.FromMinutes(0));

        /// <summary>
        /// Ejecutar alarma cinco minutos antes
        /// </summary>
        public static Trigger CincoMinutosAntes { get; } = new Trigger(TimeSpan.FromMinutes(-5));

        /// <summary>
        /// Ejecutar alarma diez minutos antes
        /// </summary>
        public static Trigger DiezMinutosAntes { get; } = new Trigger(TimeSpan.FromMinutes(-10));

        /// <summary>
        /// Ejecutar alarma quince minutos antes
        /// </summary>
        public static Trigger QuinceMinutosAntes { get; } = new Trigger(TimeSpan.FromMinutes(-15));

        /// <summary>
        /// Ejecutar alarma treinta minutos antes
        /// </summary>
        public static Trigger TreintaMinutosAntes { get; } = new Trigger(TimeSpan.FromMinutes(-30));

        /// <summary>
        /// Ejecutar alarma una hora antes
        /// </summary>
        public static Trigger UnaHoraAntes { get; } = new Trigger(TimeSpan.FromHours(-1));

        /// <summary>
        /// Ejecutar alarma un día antes
        /// </summary>
        public static Trigger UnDiaAntes { get; } = new Trigger(TimeSpan.FromDays(-1));
    }
}
