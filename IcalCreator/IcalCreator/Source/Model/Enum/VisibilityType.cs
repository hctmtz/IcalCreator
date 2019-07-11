using System.ComponentModel;

namespace IcalCreator.Models.Enums
{
    /// <summary>
    /// Representa la visibilidad de un evento en la agenda
    /// </summary>
    public enum VisibilityType
    {
        /// <summary>
        /// Evento con visibilidad configurada cómo privado
        /// </summary>
        [Description("PRIVATE")]
        Privado,
        /// <summary>
        /// Evento con visibilidad configurada cómo público
        /// </summary>
        [Description("PUBLIC")]
        Publico
    }
}
