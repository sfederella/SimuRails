using ffccSimulacion.Model.Simulacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffccSimulacion.Model.Entities
{
    public class ResultadoServicio
    {
        public List<ResultadoFormacion> resultadosFormaciones;

        public string nombre;
        public int cantidadEstaciones;
        public int duracionSimulacion;

        public double porcentajeSobrecarga;
        public double promedioDemoraIncidentes;
        public double promedioPasajeros;
        public double promedioDemoraAtencion;
        public double costoKm;
        public double costoPasajero;
        public double consumoElectricoKm;
        public double consumoDieselKm;
        public double consumoElectricoPasajero;
        public double consumoDieselPasajero;

        public int distanciaTotalRecorrida;
        public int tiempoTotalEnMovimiento;
        public int tiempoTotalDemoradoIncidente;
        public int tiempoTotalDemoradoAtencion;
        public int pasajerosTotalesTransportados;
        public int vecesSuperoCapLegal;
        public int vecesNoHabiaPasajerosParados;

        public bool flagCapLegal;
        public int tiempoTotal;
        public int totalPasajerosParados;
        public int totalSobrecargaLegal;
        public double consumoElectrico;
        public double consumoDiesel;

        public ResultadoServicio(string nombre, int cantidadEstaciones, int duracion)
        {
            this.nombre = nombre;
            this.cantidadEstaciones = cantidadEstaciones;
            this.duracionSimulacion = duracion;
            this.resultadosFormaciones = new List<ResultadoFormacion>();

            this.distanciaTotalRecorrida = 0;
            this.tiempoTotalEnMovimiento = 0;
            this.tiempoTotalDemoradoIncidente = 0;
            this.tiempoTotalDemoradoAtencion = 0;
            this.pasajerosTotalesTransportados = 0;
            this.vecesSuperoCapLegal = 0;
            this.vecesNoHabiaPasajerosParados = 0;

            this.flagCapLegal = true;
            this.tiempoTotal = 0;
            this.totalPasajerosParados = 0;
            this.totalSobrecargaLegal = 0;
    }

        public List<ResultadoFormacion> ResultadosFormaciones
        {
            get { return resultadosFormaciones; }
            set { resultadosFormaciones = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public void aumentarCapLegal()
        {
            if (this.flagCapLegal)
            {
                this.vecesSuperoCapLegal++;
                this.flagCapLegal = false;
            }
        }

    }
}
