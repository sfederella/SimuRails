using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ffccSimulacion.UI.ABMSimulacion
{
    public partial class frmBarraProgreso : Form
    {
        double _porcentaje;
        string _label;
        public bool estado = false;
        Thread _hilo;

        public double Porcentaje { get { return _porcentaje; } set { _porcentaje = value; } }
        public string Label { get { return _label; } set { _label = value; } }

        public frmBarraProgreso(Thread hilo)
        {
            InitializeComponent();
            _hilo = hilo;
            _porcentaje = 0.0;
            timerAvance.Start();
        }

        private void actualizar()
        {
            labelActividad.Text = _label;

            int des = Convert.ToInt32(Math.Truncate(_porcentaje));
            if (des < 100)
            {
                labelPorcentaje.Text = des.ToString() + " %";
                progressBar1.Value = des;
            }
            else
            {
                labelPorcentaje.Text = "100 %";
                estado = true;
                Close();
            }
        }

        private void timerAvance_Tick(object sender, EventArgs e)
        {
            this.actualizar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            _hilo.Abort();
            estado = false;
            Close();
        }

    }
}
