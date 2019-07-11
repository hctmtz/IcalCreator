using System;
using System.IO;

namespace IcalCreator.Settings
{
    /// <summary>
    /// Representa los parámetros de los archivos de configuración 
    /// </summary>
    public static class AppSettingsValues
    {
        /// <summary>
        /// Directorio dónde estará ubicado el archivo de configuración
        /// </summary>
        private static string BaseFileDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".IcalCreator");

        /// <summary>
        /// Nombre del archivo de configuración
        /// </summary>
        private static string Filename { get; set; } = "IcalCreatorSettings.json";

        /// <summary>
        /// Crea el directorio en dónde se guardará el 
        /// archivo de configuración y regresa la ruta 
        /// en dónde está el directorio recien creado. 
        /// El directorio debería quedar configurado como «Oculto»
        /// </summary>
        /// <returns><see cref="string"/> con la ruta al directorio que contiene el archivo de configuración</returns>
        private static string GetBaseDirectory()
        {
            Directory.CreateDirectory(BaseFileDirectory);
            DirectoryInfo directoryInfo = new DirectoryInfo(BaseFileDirectory);

            ///Verifica si el directorio está oculto, si no, se oculta
            if ((directoryInfo.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
            {
                directoryInfo.Attributes |= FileAttributes.Hidden;
            }

            return BaseFileDirectory;
        }

        /// <summary>
        /// Ruta completa del directorio y el archivo de configuración
        /// </summary>
        public static string SettingsFile { get; private set; } = Path.Combine(GetBaseDirectory(), Filename);

        /// <summary>
        /// Ruta del directorio de configuración
        /// </summary>
        public static string SettingsDirectory { get; private set; } = GetBaseDirectory();

    }
}
