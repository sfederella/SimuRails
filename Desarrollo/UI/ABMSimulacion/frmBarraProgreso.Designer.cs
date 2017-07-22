namespace SimuRails.UI.ABMSimulacion
{
    partial class frmBarraProgreso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBarraProgreso));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelPorcentaje = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.timerAvance = new System.Windows.Forms.Timer(this.components);
            this.labelActividad = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(25, 43);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(500, 40);
            this.progressBar1.TabIndex = 0;
            // 
            // labelPorcentaje
            // 
            this.labelPorcentaje.AutoSize = true;
            this.labelPorcentaje.BackColor = System.Drawing.Color.Transparent;
            this.labelPorcentaje.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelPorcentaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPorcentaje.Location = new System.Drawing.Point(259, 92);
            this.labelPorcentaje.Name = "labelPorcentaje";
            this.labelPorcentaje.Size = new System.Drawing.Size(28, 15);
            this.labelPorcentaje.TabIndex = 1;
            this.labelPorcentaje.Text = "0 %";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(450, 98);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 33);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // timerAvance
            // 
            this.timerAvance.Tick += new System.EventHandler(this.timerAvance_Tick);
            // 
            // labelActividad
            // 
            this.labelActividad.AutoSize = true;
            this.labelActividad.Location = new System.Drawing.Point(232, 19);
            this.labelActividad.Name = "labelActividad";
            this.labelActividad.Size = new System.Drawing.Size(80, 13);
            this.labelActividad.TabIndex = 3;
            this.labelActividad.Text = "Label Actividad";
            // 
            // frmBarraProgreso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 143);
            this.Controls.Add(this.labelPorcentaje);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.labelActividad);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBarraProgreso";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cargando";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelPorcentaje;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Timer timerAvance;
        private System.Windows.Forms.Label labelActividad;
    }
}