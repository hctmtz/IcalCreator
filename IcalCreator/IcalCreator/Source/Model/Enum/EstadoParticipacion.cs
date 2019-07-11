using System.ComponentModel;

namespace IcalCreator.Models.Enums
{
    /// <summary>
    /// Representa el estado de participación de un evento en la agenda
    /// </summary>
    public enum EstadoParticipacion
    {
        /// <summary>
        /// Evento con estado de participación configurada cómo «Aceptado»
        /// </summary>
        [Description("ACCEPTED")]
        Accepted,
        /// <summary>
        /// Evento con estado de participación configurada cómo «Rechazado»
        /// </summary>
        [Description("DECLINED")]
        Declined,
        /// <summary>
        /// Evento con estado de participación configurada cómo «Tentativo»
        /// </summary>
        [Description("TENTATIVE")]
        Tentative,
        /// <summary>
        /// Evento con estado de participación configurada cómo «Delegado»
        /// </summary>
        [Description("DELEGATED")]
        Delegated,
        /// <summary>
        /// Evento con estado de participación configurada cómo «Completado»
        /// </summary>
        [Description("COMPLETED")]
        Completed,
        /// <summary>
        /// Evento con estado de participación configurada cómo «En proceso»
        /// </summary>
        [Description("IN-PROCESS")]
        InProcess
    }
}
