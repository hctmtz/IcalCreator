namespace IcalCreator.Ui.View
{
    partial class PeriodicSendSettingsView
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PeriodicSendSettingsView));
            this.labelIntervaloActual = new System.Windows.Forms.Label();
            this.labelConfiguracion = new System.Windows.Forms.Label();
            this.buttonAceptar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gpbox1 = new System.Windows.Forms.GroupBox();
            this.maskedTextBoxIntervalo = new System.Windows.Forms.MaskedTextBox();
            this.labelIntervalo = new System.Windows.Forms.Label();
            this.gpbox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelIntervaloActual
            // 
            this.labelIntervaloActual.AutoSize = true;
            this.labelIntervaloActual.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIntervaloActual.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelIntervaloActual.Location = new System.Drawing.Point(246, 43);
            this.labelIntervaloActual.Name = "labelIntervaloActual";
            this.labelIntervaloActual.Size = new System.Drawing.Size(136, 15);
            this.labelIntervaloActual.TabIndex = 8;
            this.labelIntervaloActual.Text = "<Configuración actual>";
            // 
            // labelConfiguracion
            // 
            this.labelConfiguracion.AutoSize = true;
            this.labelConfiguracion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelConfiguracion.Location = new System.Drawing.Point(246, 22);
            this.labelConfiguracion.Name = "labelConfiguracion";
            this.labelConfiguracion.Size = new System.Drawing.Size(121, 15);
            this.labelConfiguracion.TabIndex = 7;
            this.labelConfiguracion.Text = "Configuración actual:";
            // 
            // buttonAceptar
            // 
            this.buttonAceptar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAceptar.Location = new System.Drawing.Point(422, 105);
            this.buttonAceptar.Name = "buttonAceptar";
            this.buttonAceptar.Size = new System.Drawing.Size(70, 26);
            this.buttonAceptar.TabIndex = 2;
            this.buttonAceptar.Text = "Aceptar";
            this.buttonAceptar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Envío de notificaciones";
            // 
            // gpbox1
            // 
            this.gpbox1.Controls.Add(this.maskedTextBoxIntervalo);
            this.gpbox1.Controls.Add(this.labelIntervalo);
            this.gpbox1.Controls.Add(this.labelIntervaloActual);
            this.gpbox1.Controls.Add(this.labelConfiguracion);
            this.gpbox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbox1.Location = new System.Drawing.Point(11, 32);
            this.gpbox1.Name = "gpbox1";
            this.gpbox1.Size = new System.Drawing.Size(481, 69);
            this.gpbox1.TabIndex = 12;
            this.gpbox1.TabStop = false;
            this.gpbox1.Text = "Configuraciones";
            // 
            // maskedTextBoxIntervalo
            // 
            this.maskedTextBoxIntervalo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maskedTextBoxIntervalo.Location = new System.Drawing.Point(9, 40);
            this.maskedTextBoxIntervalo.Name = "maskedTextBoxIntervalo";
            this.maskedTextBoxIntervalo.Size = new System.Drawing.Size(158, 23);
            this.maskedTextBoxIntervalo.TabIndex = 9;
            this.maskedTextBoxIntervalo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.maskedTextBoxIntervalo.ValidatingType = typeof(int);
            // 
            // labelIntervalo
            // 
            this.labelIntervalo.AutoSize = true;
            this.labelIntervalo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelIntervalo.Location = new System.Drawing.Point(6, 22);
            this.labelIntervalo.Name = "labelIntervalo";
            this.labelIntervalo.Size = new System.Drawing.Size(176, 15);
            this.labelIntervalo.TabIndex = 5;
            this.labelIntervalo.Text = "Intervalo de tiempo en minutos:";
            // 
            // PeriodicSendSettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 139);
            this.Controls.Add(this.gpbox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAceptar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PeriodicSendSettingsView";
            this.Text = "Configuración";
            this.gpbox1.ResumeLayout(false);
            this.gpbox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button buttonAceptar;
        private System.Windows.Forms.Label labelConfiguracion;
        public System.Windows.Forms.Label labelIntervaloActual;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gpbox1;
        private System.Windows.Forms.Label labelIntervalo;
        public System.Windows.Forms.MaskedTextBox maskedTextBoxIntervalo;
    }
}

