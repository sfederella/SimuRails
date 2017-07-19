using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace ffccSimulacion.UI.Ayuda
{
    public partial class frmAyuda : Form
    {
        public frmAyuda()
        {
            InitializeComponent();
        }

        private void btnAyudaCerrar_Click(object sender, EventArgs e)
        {
            pnlAyuda.Controls.Clear();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = Application.StartupPath + "\\Resources\\Manual.pdf";
            Process.Start(path);
        }       
    }
}
