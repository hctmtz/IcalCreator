namespace IcalCreator.Ui.View
{ 
    partial class PeriodicSendView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PeriodicSendView));
            this.labelProximaEjecucion = new System.Windows.Forms.Label();
            this.labelTiempoRestante = new System.Windows.Forms.Label();
            this.labelTitulo = new System.Windows.Forms.Label();
            this.timerTiempoRestante = new System.Windows.Forms.Timer(this.components);
            this.checkBoxActivarProceso = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelProximaEjecucion
            // 
            this.labelProximaEjecucion.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProximaEjecucion.Image = ((System.Drawing.Image)(resources.GetObject("labelProximaEjecucion.Image")));
            this.labelProximaEjecucion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelProximaEjecucion.Location = new System.Drawing.Point(13, 56);
            this.labelProximaEjecucion.Name = "labelProximaEjecucion";
            this.labelProximaEjecucion.Size = new System.Drawing.Size(171, 21);
            this.labelProximaEjecucion.TabIndex = 0;
            this.labelProximaEjecucion.Text = "Estado de proceso:";
            this.labelProximaEjecucion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTiempoRestante
            // 
            this.labelTiempoRestante.AutoSize = true;
            this.labelTiempoRestante.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTiempoRestante.Location = new System.Drawing.Point(155, 56);
            this.labelTiempoRestante.Name = "labelTiempoRestante";
            this.labelTiempoRestante.Size = new System.Drawing.Size(29, 20);
            this.labelTiempoRestante.TabIndex = 1;
            this.labelTiempoRestante.Text = "<>";
            // 
            // labelTitulo
            // 
            this.labelTitulo.AutoSize = true;
            this.labelTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitulo.Location = new System.Drawing.Point(13, 9);
            this.labelTitulo.Name = "labelTitulo";
            this.labelTitulo.Size = new System.Drawing.Size(244, 20);
            this.labelTitulo.TabIndex = 2;
            this.labelTitulo.Text = "Envío de notificaciones de eventos";
            // 
            // timerTiempoRestante
            // 
            this.timerTiempoRestante.Enabled = true;
            this.timerTiempoRestante.Interval = 1000;
            // 
            // checkBoxActivarProceso
            // 
            this.checkBoxActivarProceso.AutoSize = true;
            this.checkBoxActivarProceso.Font = new System.Drawing.Font("Segoe UI Semilight", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxActivarProceso.Location = new System.Drawing.Point(17, 32);
            this.checkBoxActivarProceso.Name = "checkBoxActivarProceso";
            this.checkBoxActivarProceso.Size = new System.Drawing.Size(219, 21);
            this.checkBoxActivarProceso.TabIndex = 3;
            this.checkBoxActivarProceso.Text = "Activar proceso de envío periódico";
            this.checkBoxActivarProceso.UseVisualStyleBackColor = true;
            // 
            // PeriodicSendView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 87);
            this.Controls.Add(this.checkBoxActivarProceso);
            this.Controls.Add(this.labelTitulo);
            this.Controls.Add(this.labelTiempoRestante);
            this.Controls.Add(this.labelProximaEjecucion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PeriodicSendView";
            this.Text = "Proceso envío de notificaciones";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelProximaEjecucion;
        private System.Windows.Forms.Label labelTitulo;
        public System.Windows.Forms.Label labelTiempoRestante;
        public System.Windows.Forms.Timer timerTiempoRestante;
        public System.Windows.Forms.CheckBox checkBoxActivarProceso;
    }
}