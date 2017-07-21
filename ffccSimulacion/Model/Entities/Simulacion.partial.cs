namespace SimuRails.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Simulacion;
    using UI.ABMSimulacion;

    public partial class Simulaciones
    {
        //private List<ResultadoFormacion> _resultadosFormacionesSimulacion;
        private List<ResultadoServicio> _resultadosServiciosSimulacion;

        public Simulaciones(){}

        public int _estrategiaDeSimulacion { get; set; }

        public frmBarraProgreso BarraProgreso { get; set; }
        public List<ResultadoServicio> ResultadosServiciosSimulacion { get { return _resultadosServiciosSimulacion; } }

        /*Este metodo configura todas las listas y propiedades del escenario que se va a simular por lo tento es necesario ejecutar este metodo antes de 
         correr la simulacion propiamente dicha*/
        public void ConfigurarSimulador()
        {
            Trazas.ConfigurarLosServiciosDeLaTraza(Tiempo_Final);
            _estrategiaDeSimulacion = 0;
        }

        public void EjecutarSimulacion()
        {
            /*Esta validaciones son necesarias porque antes de ejecutar el algoritmo de simulacion es necesario configurar el entorno*/
            string errorMsj = "";
            if (this.Trazas == null)
                errorMsj += "El simulador no tiene una traza asignada.\n";
            if (this.Tiempo_Final == 0)
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
                    TiempoComprometido tiempoComprometido = new TiempoComprometido(0, Tiempo_Final, Trazas, BarraProgreso);
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
