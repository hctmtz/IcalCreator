using System;
using IcalCreator.Models.Enums;

namespace IcalCreator.Models
{
    /// <summary>
    /// Modelo que representa los datos de un asistente a un evento de la agenda
    /// </summary>
    public class Asistentes
    {
        /// <summary>
        /// Modelo que representa los datos de un asistente a un evento de la agenda
        /// </summary>
        public Asistentes()
        {
            ///Datos por defecto
            Rsvp = false;
        }

        /// <summary>
        /// Nombre del asistente
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Estado de la participación en el evento
        /// </summary>
        public EstadoParticipacion EstadoParticipacion { get; set; }

        /// <summary>
        /// Especifica si el organizador del evento espera una respuesta 
        /// de parte de parte de los participantes o asistentes del evento. 
        /// Se espera una respuesta o confirmación: <see langword="true"/>; 
        /// No se espera una respuesta o confirmación: <see langword="false"/>. 
        /// Véase <see cref="EstadoParticipacion.EstadoParticipacion"/> para más información
        /// </summary>
        public bool Rsvp { get; set; }
        
        /// <summary>
        /// Dirección de correo del asistente
        /// </summary>
        public Uri Correo { get; set; }
    }
}
