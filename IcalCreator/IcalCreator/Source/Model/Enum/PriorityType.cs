namespace IcalCreator.Models.Enums
{
    /// <summary>
    /// Representa el nivel de prioridad de un evento en la agenda
    /// </summary>
    public enum PriorityType
    {
        /// <summary>
        /// Prioridad no definida
        /// </summary>
        NoDefinida = 0,
        /// <summary>
        /// La prioridad más alta
        /// </summary>
        LaMasAlta = 1,
        /// <summary>
        /// Prioridad muy alta
        /// </summary>
        MuyAlta = 2,
        /// <summary>
        /// Prioridad alta
        /// </summary>
        Alta = 3,
        /// <summary>
        /// Prioridad media a alta 
        /// </summary>
        MediaAlta = 4,
        /// <summary>
        /// Prioridad media
        /// </summary>
        Media = 5,
        /// <summary>
        /// Prioridad media a baja
        /// </summary>
        MediaBaja = 6,
        /// <summary>
        /// Prioridad baja
        /// </summary>
        Baja = 7,
        /// <summary>
        /// Prioridad muy baja
        /// </summary>
        MuyBaja = 8,
        /// <summary>
        /// La prioridad más baja
        /// </summary>
        LaMasBaja = 9
    }
}
