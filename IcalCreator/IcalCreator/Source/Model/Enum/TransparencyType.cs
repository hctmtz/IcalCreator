using System.ComponentModel;

namespace IcalCreator.Models.Enums
{
    /// <summary>
    /// Representa el tipo de transparencia de un evento en la agenda
    /// </summary>
    public enum TransparencyType
    {
        /// <summary>
        /// Evento con transparencia configurada cómo transparente
        /// </summary>
        [Description("TRANSPARENT")]
        Transparent,
        /// <summary>
        /// Evento con transparencia configurada cómo opaco
        /// </summary>
        [Description("OPAQUE")]
        Opaque
    }
}
