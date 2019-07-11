using System.ComponentModel;

namespace IcalCreator.Models.Enums
{
    /// <summary>
    /// Representa la acción de una alarma en la agenda
    /// </summary>
    public enum AlarmAction
    {
        [Description("AUDIO")]
        Audio,
        [Description("DISPLAY")]
        Display,
        [Description("EMAIL")]
        Email,
        [Description("PROCEDURE")]
        Procedure

    }
}
