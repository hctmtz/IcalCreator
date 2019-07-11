using System;
using System.IO;
using System.Text;

namespace IcalCreator.Log
{
    /// <summary>
    /// Contiene utilidades para crear archivos 
    /// de registros de errores
    /// </summary>
    public static class LogUtil
    {
        /// <summary>
        /// Crea un archivo de registro de errores
        /// </summary>
        /// <param name="path">Ruta dónde se guardará el archivo</param>
        /// <param name="name">Nombre del archivo</param>
        /// <param name="exception"><see cref="Exception"/> con datos del error</param>
        public static void CreateErrorLog(string path, string name, Exception exception)
        {
            ///👨‍💻 Autor: Héctor Martínez - <hector.mtz.grc@gmail.com>

            name += "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            string logPath = Path.Combine(path, name);
            TextWriter textWriter = null;

            string separator = "==================================================================================="
                                + Environment.NewLine;

            string error = separator
                         + "System: " + Environment.MachineName + Environment.NewLine
                         + "Date: " + DateTime.Now + Environment.NewLine
                         + "User: " + Environment.UserName + Environment.NewLine
                         + "Error: " + exception.Message + Environment.NewLine
                         + "Exception: " + exception.InnerException + Environment.NewLine;   

            textWriter = new StreamWriter(logPath, true, Encoding.UTF8);
            textWriter.Close();

            if (!File.Exists(logPath))
            {
                var ArchivoLog = File.Create(logPath);
                ArchivoLog.Close();
                File.AppendAllText(logPath, error + Environment.NewLine, Encoding.UTF8);
            }
            else if (File.Exists(logPath))
            {
                File.AppendAllText(logPath, error + Environment.NewLine);
            }
        }
    }
}
