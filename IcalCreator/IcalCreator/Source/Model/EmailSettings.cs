namespace IcalCreator.Models
{
    /// <summary>
    /// Representa las configuraciones necesarias para enviar un e-mail
    /// </summary>
    public class EmailSettings
    {
        /// <summary>
        /// Representa las configuraciones necesarias para enviar un e-mail
        /// </summary>
        public EmailSettings()
        {
            ///Desactivado por defecto
            ActivarSsl = false;
            //👨‍💻 Autor: Héctor Martínez - <hector.mtz.grc@gmail.com>
        }

        /// <summary>
        /// Nombre de usuario del servidor de correo 
        /// que se usará para enviar
        /// </summary>
        public string NombreUsuario { get; set; }

        /// <summary>
        /// Contraseña de usuario del servidor de 
        /// correo que se usará para enviar
        /// </summary>
        public string ContrasenaUsuario { get; set; }

        /// <summary>
        /// Dirección SMTP del servidor de correo
        /// </summary>
        public string ServidorSmtp { get; set; }

        /// <summary>
        /// Puerto SMTP
        /// </summary>
        public int PuertoSmtp { get; set; }

        /// <summary>
        /// Activar envío seguro a través de SSL.
        /// En caso afirmativo <see langword="true"/>, 
        /// en caso negativo <see langword="false"/> 
        /// </summary>
        public bool ActivarSsl { get; set; }

        /// <summary>
        /// Destinatario del correo
        /// </summary>
        public string Destinatario { get; set; }

        /// <summary>
        /// Remitente del correo
        /// </summary>
        public string Remitente { get; set; }

        /// <summary>
        /// Cuerpo del mensaje 
        /// </summary>
        public string CuerpoMensaje { get; set; }

        /// <summary>
        /// Asunto del mensaje 
        /// </summary>
        public string AsuntoMensaje { get; set; }



    }
}