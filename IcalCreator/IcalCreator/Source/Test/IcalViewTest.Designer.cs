namespace IcalCreator.Test
{
    partial class IcalView
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnSinDependencias = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "ICS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSinDependencias
            // 
            this.btnSinDependencias.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSinDependencias.Location = new System.Drawing.Point(173, 56);
            this.btnSinDependencias.Name = "btnSinDependencias";
            this.btnSinDependencias.Size = new System.Drawing.Size(150, 30);
            this.btnSinDependencias.TabIndex = 0;
            this.btnSinDependencias.Text = "ICS ";
            this.btnSinDependencias.UseVisualStyleBackColor = true;
            this.btnSinDependencias.Click += new System.EventHandler(this.btnSinDependencias_Click);
            // 
            // FrameIcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 102);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSinDependencias);
            this.Name = "FrameIcs";
            this.Text = "Crear archivo ICS";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btnSinDependencias;
    }
}

