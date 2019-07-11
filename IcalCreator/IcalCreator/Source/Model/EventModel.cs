using System;
using System.Collections.Generic;
using Ical.Net.DataTypes;
using IcalCreator.Models.Enums;

namespace IcalCreator.Models
{
    /// <summary>
    /// Representa un evento que se agenda en el calendario
    /// </summary>
    public class EventModel
    {
        /// <summary>
        /// Representa un evento que se agenda en el calendario
        /// </summary>
        public EventModel()
        {
            //👨‍💻 Autor: Héctor Martínez - <hector.mtz.grc@gmail.com>
        }

        /// <summary>
        /// Fecha de inicio del evento en la agenda
        /// </summary>
        public DateTime FechaInicio{get; set;}

        /// <summary>
        /// Fecha de fin del evento en la agenda
        /// </summary>
        public DateTime FechaFin { get; set; }

        /// <summary>
        /// ¿El evento durará todo el día? 
        /// <see langword="true"/> en caso afirmativo, <see langword="false"/> en caso negativo
        /// </summary>
        public bool TodoElDia { get; set; }

        /// <summary>
        /// Secuencia del evento; representa la cantidad 
        /// de revisiones que tiene el evento. Un evento 
        /// recién creado tiene una valor de secuencia predeterminado de cero
        /// </summary>
        public int Secuencia { get; set; } = 0;

        /// <summary>
        /// Transparencia del evento en la agenda. 
        /// Véase <see cref="TransparencyType"/> para más información
        /// </summary>
        public TransparencyType Transparencia { get; set; }

        /// <summary>
        /// Descripción del evento en la agenda
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Prioridad del evento en la agenda. El rango límite 
        /// es de 0 a 9; siendo el 9 la prioridad más baja y 1 
        /// la más alta mientras que 0 es una prioridad no definida. 
        /// Véase <see cref="PriorityType"/> para más información
        /// </summary>
        public PriorityType Prioridad { get; set; }

        /// <summary>
        /// Visibilidad del evento en la agenda. 
        /// Véase <see cref="VisibilityType"/> para más información
        /// </summary>
        public VisibilityType Visibilidad { get; set; }

        /// <summary>
        /// Ubicación del evento
        /// </summary>
        public string Ubicacion { get; set; }

        /// <summary>
        /// Resumen del evento en la agenda
        /// </summary>
        public string Resumen { get; set; }
        
        /// <summary>
        /// Nombre del organizador del evento
        /// </summary>
        public string NombreOrganizador { get; set; }

        /// <summary>
        /// Correo del organizador del evento
        /// </summary>
        public Uri CorreoOrganizador { get; set; }

        /// <summary>
        /// Asistentes del evento. 
        /// Véase <see cref="Asistentes.Asistentes"/> para más información
        /// </summary>
        public List<Asistentes> Asistentes { get; set; }

        /// <summary>
        /// Acción que debería mostrarse en la agenda. 
        /// Véase <see cref="AlarmAction.AlarmAction"/> para más información
        /// </summary>
        public AlarmAction AccionAlarma { get; set; }

        /// <summary>
        /// Lapso de tiempo previo para avisar sobre el evento. 
        /// Véase <see cref="AlertType.AlertType"/> para más información
        /// </summary>
        public Trigger TipoAlerta { get; set; }

        /// <summary>
        /// Información sobre la alarma
        /// </summary>
        public string ResumenAlarma { get; set; }

        
    }
}
