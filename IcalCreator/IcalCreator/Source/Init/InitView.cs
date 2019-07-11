using System;
using System.Windows.Forms;
using IcalCreator.Test;

namespace IcalCreator.Init
{

    /// <summary>
    /// Punto de partida de la aplicación en modo gráfico
    /// </summary>
    public static class InitView
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new IcalView());
        }
    }
}
