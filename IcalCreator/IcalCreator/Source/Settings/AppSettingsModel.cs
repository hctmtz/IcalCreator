using System;

namespace IcalCreator.Settings
{
    /// <summary>
    /// Modelo para configuraciones de aplicación
    /// </summary>
    public class AppSettingsModel : AppSettings<AppSettingsModel>
    {
        /// <summary>
        /// Nombre de la aplicación
        /// </summary>
        public string AppName { get; private set; } = "ICalendar Event Creator";

        /// <summary>
        /// Fecha y hora de la última edición. La fecha se serializa en 
        /// formato UTC; para recuperar la hora, se debe interpretrar 
        /// en UTC y convertirla a hora local.
        /// </summary>
        public DateTime LastEdit { get; private set; } = DateTime.Now;

        /// <summary>
        /// Intervalo de tiempo en minutos
        /// </summary>
        public int TimeInterval { get; set; }

        /// <summary>
        /// Usuario de la computadora
        /// </summary>
        public string SystemUser { get; private set; } = Environment.UserName;

        /// <summary>
        /// Usuario del sistema
        /// </summary>
        public string AppUser { get; set; }

        /// <summary>
        /// Nombre de la computadora
        /// </summary>
        public string MachineName { get; private set; } = Environment.MachineName;
    }
}
