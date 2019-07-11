using System;
using System.Runtime.InteropServices;
using IcalCreator.Controller;
using IcalCreator.Ui.Controller;

namespace IcalCreator.Init
{
    /// <summary>
    /// Punto de partida de la aplicación en modo gráfico y por línea de comandos
    /// </summary>
    public class InitApp
    {
        /// <summary>
        /// Ésta biblioteca y sus respectivos métodos y variables 
        /// permiten escribir en consola la salida del programa.
        /// Una aplicación basada en WindowsForms no muestra la 
        /// salida de consola si se ejecuta desde CommandPrompt.
        /// </summary>
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        private static string CommandlineArgument = String.Empty;
        private static int ParamValue;
        private static CirugiaEventGenerator icalGenerator = new CirugiaEventGenerator();

        /// <summary>
        /// Inicia la aplicación en modo gráfico
        /// </summary>
        private static void StartAppView()
        {
            PeriodicSendController periodicSendController = new PeriodicSendController();
        }

        [STAThread]
        public static void Main(string[] args)
        {
            AttachConsole(ATTACH_PARENT_PROCESS);

            if (args.Length == 1)
            {
                CommandlineArgument = args[0];

                if (!int.TryParse(CommandlineArgument, out ParamValue))
                {
                    Console.Clear();
                    Console.WriteLine("El parámetro debe ser de un valor númerico");
                }
                else
                {
                    icalGenerator.SendEventCommandLine(ParamValue);
                }
            }
            else if (args.Length < 1 || args == null)
            {
                Console.WriteLine("Parámetro no proporcionado o nulo");
                ///Modo interfaz gráfica
                StartAppView();
            }
            else if (args.Length > 1)
            {
                Console.Clear();
                Console.WriteLine("Se requiere sólo un parámetro");
            }
        }
    }
}
