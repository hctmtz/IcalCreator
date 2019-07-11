namespace IcalCreator.Param
{
    /// <summary>
    /// Representa un conjunto de valores necesarios para enviar correos. 
    /// Estos valores son parámetros del sistema
    /// </summary>
    public static class EmailValues
    {
        /// <summary>
        /// Dirección del servidor SMTP
        /// </summary>
        public static string SmtpServer { get; private set; } = "Servidor SMTP";

        /// <summary>
        /// Dirección e-mail que se usará para enviar correos
        /// </summary>
        public static string EmailUser { get; private set; } = "Dirección de correo";

        /// <summary>
        /// Contraseña del e-mail
        /// </summary>
        public static string EmailPassword { get; private set; } = "Contraseña de correo";

        /// <summary>
        /// Puerto del servidor SMTP
        /// </summary>
        public static string SmtpPort { get; private set; } = "Puerto SMTP";

        /// <summary>
        /// Activar SSL en el envío de correos
        /// </summary>
        public static string SslActive { get; private set; } = "Activar SSL";

    }
}
