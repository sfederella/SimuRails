using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SimuRails.Model.Simulacion;
using SimuRails.UI.ABMSimulacion;

namespace SimuRails.Model.Entities
{
    public partial class Simulacion
    {
        //private List<ResultadoFormacion> _resultadosFormacionesSimulacion;
        private List<ResultadoServicio> _resultadosServiciosSimulacion;

        public Simulacion(){}

        public int _estrategiaDeSimulacion { get; set; }

        public frmBarraProgreso BarraProgreso { get; set; }
        public List<ResultadoServicio> ResultadosServiciosSimulacion { get { return _resultadosServiciosSimulacion; } }

        /*Este metodo configura todas las listas y propiedades del escenario que se va a simular por lo tento es necesario ejecutar este metodo antes de 
         correr la simulacion propiamente dicha*/
        public void ConfigurarSimulador()
        {
            Traza.ConfigurarLosServiciosDeLaTraza(TiempoFinal);
            _estrategiaDeSimulacion = 0;
        }

        public void EjecutarSimulacion()
        {
            /*Esta validaciones son necesarias porque antes de ejecutar el algoritmo de simulacion es necesario configurar el entorno*/
            string errorMsj = "";
            if (this.Traza == null)
                errorMsj += "El simulador no tiene una traza asignada.\n";
            if (this.TiempoFinal == 0)
                errorMsj += "No hay asignado ningún tiempo de simulación.\n";

            if(string.IsNullOrEmpty(errorMsj))
            {
               try
               {
                   this.ConfigurarSimulador();
               }
               catch (Exception exc)
               {
                   MessageBox.Show("Ocurrió un error durante el proceso de configuración del escenario.\n" + exc.ToString());
                   return;
               }
            }
            else
            {
                MessageBox.Show(errorMsj);
                return;
            }

            switch (_estrategiaDeSimulacion)
            {
                case(0):
                    TiempoComprometido tiempoComprometido = new TiempoComprometido(0, TiempoFinal, Traza, BarraProgreso);
                    tiempoComprometido.EjecutarSimulacion();
                    //_resultadosFormacionesSimulacion = tiempoComprometido.ResultadosFormaciones;
                    _resultadosServiciosSimulacion = tiempoComprometido.ResultadosServicios;
                    //MessageBox.Show("Done !");
                    break;
                case(1):
                    //Evento a evento
                    break;
            }
        }
    }
}
