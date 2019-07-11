using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IcalCreator.Controller;
using IcalCreator.Models;
using IcalCreator.Settings;
using IcalCreator.Ui.View;
using IcalCreator.Util;

namespace IcalCreator.Ui.Controller
{
    /// <summary>
    /// Controlador de la vista <see cref="PeriodicSendView"/>
    /// </summary>
    public class PeriodicSendController
    {
        private ToolTip toolTip = new ToolTip();
        private PeriodicSendView periodicSendView = new PeriodicSendView();
        private DateTime startTime = DateTime.Now;
        private CirugiaEventGenerator icalGenerator = new CirugiaEventGenerator();

        public PeriodicSendController()
        {
            AddEvents();
        }

        /// <summary>
        /// Agrega los eventos <see cref="EventHandler"/> a la vista
        /// </summary>
        private void AddEvents()
        {
            periodicSendView.timerTiempoRestante.Tick += new EventHandler(timerTiempoRestante_Tick);
            periodicSendView.checkBoxActivarProceso.CheckedChanged += new EventHandler(checkBoxActivarProceso_Checked);
        }

        /// <summary>
        /// Evento <see cref="EventHandler"/> del componente <see cref="PeriodicSendView.checkBoxActivarProceso"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxActivarProceso_Checked(object sender, EventArgs e)
        {
            startTime = DateTime.Now;

            if (periodicSendView.checkBoxActivarProceso.Checked)
            {
                periodicSendView.labelTiempoRestante.Text = "<>";

            }
            else
            {
                periodicSendView.labelTiempoRestante.Text = "<>";

            }
        }

        /// <summary>
        /// Evento <see cref="EventHandler"/> del componente <see cref="PeriodicSendView.timerTiempoRestante"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerTiempoRestante_Tick(object sender, EventArgs e)
        {
            int value = GetIntervalTime();

            if (periodicSendView.checkBoxActivarProceso.Checked)
            {
                if(value <= 0)
                {
                    SendByTimeInterval(30);
                }
                else
                {
                    SendByTimeInterval(value);
                }
            }
        }

        /// <summary>
        /// Regresa el valor de intervalo de tiempo
        /// </summary>
        /// <returns>Intervalo de tiempo</returns>
        private int GetIntervalTime()
        {
            AppSettingsModel settings = AppSettingsModel.Load(AppSettingsValues.SettingsFile);
            return settings.TimeInterval;
        }

        /// <summary>
        /// Genera y envía los eventos pendientes. 
        /// Retorna <see langword="true"/> si lo hace correctamente,
        /// en caso contrario retorna retorna <see langword="false"/>
        /// </summary>
        /// <returns>
        /// Retorna <see langword="true"/> si lo hace correctamente,
        /// en caso contrario retorna retorna <see langword="false"/>
        /// </returns>
        private bool Send()
        {
            ///Cirugías programadas
            CirugiaEventGenerator cirugiaEventGenerator = new CirugiaEventGenerator();
            List<Evento> EventoCirugia = cirugiaEventGenerator.GetEvents();

            foreach (var eventos in EventoCirugia)
            {
                cirugiaEventGenerator.SendEvent(eventos.FolioEvento);
            }

            ///Citas médicas
            CitaEventGenerator citaEventGenerator = new CitaEventGenerator();
            List<Evento> EventoCita = citaEventGenerator.GetEvents();

            foreach (var eventos in EventoCita)
            {
                citaEventGenerator.SendEvent(eventos.FolioEvento);
            }

            return true;
        }

        /// <summary>
        /// Calcula el tiempo restante en base al <paramref name="intervalo"/> 
        /// y lo compara con la hora actual
        /// </summary>
        /// <param name="intervalo">Cantidad de tiempo en segundos</param>
        private void SendByTimeInterval(long intervalo)
        {
            long SegundosTranscurridos = (long) (DateTime.Now - startTime).TotalSeconds;
            intervalo *= 60;
            long SegundosRestantes = intervalo - SegundosTranscurridos;

            if (SegundosRestantes <= 0)
            {
                periodicSendView.timerTiempoRestante.Enabled = false;
                periodicSendView.labelTiempoRestante.Text = "Enviando"; 

                if (Send())
                {
                    periodicSendView.labelTiempoRestante.Text = "Terminado";
                    periodicSendView.timerTiempoRestante.Enabled = true;
                    periodicSendView.Refresh();

                    startTime = DateTime.Now;
                }
            }
            else
            {
                TimeSpan result = TimeSpan.FromSeconds(SegundosRestantes);
                string fromTimeString = StringUtil.TimeSpanToString(result);
                periodicSendView.labelTiempoRestante.Text = string.Format("Próxima ejecución: {0}", fromTimeString);
            }
        }

    }
}
