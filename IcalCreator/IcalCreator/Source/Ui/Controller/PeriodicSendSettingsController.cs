using System;
using System.Windows.Forms;
using IcalCreator.Settings;
using IcalCreator.Ui.View;
using IcalCreator.Util;

namespace IcalCreator.Ui.Controller
{
    public class PeriodicSendSettingsController
    {
        private PeriodicSendSettingsView periodicSendSettingsView = new PeriodicSendSettingsView();

        public PeriodicSendSettingsController()
        {
            AddEvents();
            LoadData();
        }

        /// <summary>
        /// Agrega los eventos al controlador
        /// </summary>
        private void AddEvents()
        {
            periodicSendSettingsView.buttonAceptar.Click += new EventHandler(buttonAceptar_Click);
        }

        /// <summary>
        /// Carga la cantidad de tiempo configurada en el <see cref="Label"/> de la intefaz
        /// y formatea el mensaje según el tiempo que haya sido configurado
        /// </summary>
        private void LoadData()
        {
            AppSettingsModel settings = AppSettingsModel.Load(AppSettingsValues.SettingsFile);
            TimeSpan result = TimeSpan.FromMinutes(settings.TimeInterval);

            periodicSendSettingsView.labelIntervaloActual.Text = StringUtil.TimeSpanToString(result);
        }

        /// <summary>
        /// Evento de click del boton Aceptar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            int value;
            bool success = int.TryParse(periodicSendSettingsView.maskedTextBoxIntervalo.Text, out value);

            if (success)
            {
                AppSettingsModel settings = new AppSettingsModel()
                {
                    TimeInterval = value,
                    AppUser = Environment.UserName
                } ;

                settings.Save(AppSettingsValues.SettingsFile);
                LoadData();

                MessageBox.Show("Configuración guardada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("El valor que se ha proporcionado es inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
