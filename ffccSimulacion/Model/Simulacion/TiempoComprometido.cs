using System;
using System.Collections.Generic;
using System.Linq;
using SimuRails.Model.Entities;
using SimuRails.UI.ABMSimulacion;

namespace SimuRails.Model.Simulacion
{
    public class TiempoComprometido
    {
        private int _tiempoInicial;
        private int _tiempoFinal;
        private Trazas _traza;
        private frmBarraProgreso _barraProgreso;
        /*Este diccionario se utiliza para mantener de manera comun los tiempos comprometidos, la gentes esperando y la hora de ultima atencion de todas las
         estaciones que existan en la simulacion. De otra forma cada estacion es una instancia distinta y por lo tanto sus campos son siempre 0 aunque se trate de la misma 
         estacion. Preguntar a Pablo*/

        //La clave que se utiliza es el id de la estacion
        private Dictionary<int, Estaciones> _estaciones_TC = new Dictionary<int, Estaciones>();
        private List<ResultadoServicio> _resultadosServicios = new List<ResultadoServicio>();

        public TiempoComprometido(int tiempoInicial, int tiempoFinal, Trazas unaTraza, frmBarraProgreso barraProgreso)
        {
            TiempoInicial = tiempoInicial;
            TiempoFinal = tiempoFinal;
            Traza = unaTraza;
            _barraProgreso = barraProgreso;
        }

        #region Propiedades

        public int TiempoInicial
        {
            get { return _tiempoInicial; }
            set { _tiempoInicial = value; }
        }

        /*El tiempo final se expresa en minutos*/
        public int TiempoFinal
        {
            get { return _tiempoFinal; }
            set { _tiempoFinal = value; }
        }

        public Trazas Traza
        {
            get { return _traza; }
            set { _traza = value; }
        }

        public List<ResultadoServicio> ResultadosServicios { get { return _resultadosServicios; } }

        #endregion

        #region Metodos

        public void EjecutarSimulacion()
        {
            //EJECUCION
            int tiempoActual;
            Formaciones formacionActual;
            Servicios servicioActual;
            int inicioFormacion;

            cargarServicios();

            //VARIABLES DE SALIDA
            //TODO variables de salida

            // Inicialización de tiempoActual ( = 0), servicioActual ( = primer servicio configurado) y formaciónActual ( = Random del servicioActual)
            actualizarSiguienteServicio(out tiempoActual, out servicioActual); //Actualiza el tiempo actual a partir del primer servicio a prestar.
            inicioFormacion = tiempoActual;
            formacionActual = servicioActual.GetFormacionRandom(); //Se asigna la formacion que realizara el servicio actual

            _barraProgreso.Label = "Ejecutando simulación...";
            _barraProgreso.Porcentaje = tiempoActual * 100.0 / TiempoFinal;
            Console.WriteLine("tiempoActual={0} | tiempoFinal={1}", tiempoActual, TiempoFinal);

            while (tiempoActual < TiempoFinal)
            {
                // Inicializo la variable que recorre el puntero de nodos. Empieza en la terminal.
                Estaciones estacionActual = servicioActual.Desde;
                //El tren sale a la hora indicada en la programacion del servicio.
                int tiempoSalidaProximaEstacion = tiempoActual;
                //TODO atencion en la terminal.

                ResultadoFormacion result = new ResultadoFormacion();
                result.id_formacion = formacionActual.Id;
                result.nombreFormacion = formacionActual.NombreFormacion;

                ResultadoServicio resultServicioActual = null;

                foreach (ResultadoServicio resultServicio in _resultadosServicios)
                {
                    if (resultServicio.nombre.Equals(servicioActual.Nombre))
                    {
                        resultServicioActual = resultServicio;
                        resultServicioActual.flagCapLegal = true;
                        Console.WriteLine("Servicio: {0}", servicioActual.Nombre);
                    }
                }

                //ATENCION DE UNA FORMACION
                while (estacionActual != servicioActual.Hasta)
                {
                                        
                    //Obtengo el camino a recorrer hasta la próxima estación.
                    Relaciones relacionActual = servicioActual.Relaciones.Where(x => x.Id_Estacion_Anterior == estacionActual.Id).First();

                    //Busco la siguiente estación en el recorrido.
                    Estaciones siguienteEstacion = relacionActual.Estaciones1;

                    //La relacion me indica la distancia del viaje
                    result.distanciaTotalRecorrida += relacionActual.Distancia;
                    resultServicioActual.distanciaTotalRecorrida += relacionActual.Distancia;

                    //La relacion me indica el tiempo de viaje.
                    int tiempoViaje = relacionActual.TiempoViaje;
                    result.tiempoTotalEnMovimiento += tiempoViaje;
                    resultServicioActual.tiempoTotalEnMovimiento += tiempoViaje;

                    //Calculo el tiempo de demora en el viaje.
                    int demoraPorAccidentesEnViaje = relacionActual.demoraPorAccidentes();
                    result.tiempoTotalDemoradoIncidente += demoraPorAccidentesEnViaje;
                    resultServicioActual.tiempoTotalDemoradoIncidente += demoraPorAccidentesEnViaje;

                    //Llego al proximo nodo al sumar el tiempo de viaje y las demoras. El tiempoSalidaProximaEstacion es:
                    //  1. tiempoActual si la estación es la primera del servicio
                    //  2. La hora en que partio de la estación anterior para todos los otros casos
                    int tiempoLlegadaProximaEstacion = tiempoViaje + demoraPorAccidentesEnViaje + tiempoSalidaProximaEstacion;

                    if (!_estaciones_TC.ContainsKey(siguienteEstacion.Id))
                    {
                        _estaciones_TC.Add(siguienteEstacion.Id, siguienteEstacion);
                    }

                    siguienteEstacion = _estaciones_TC[siguienteEstacion.Id];

                    // Devuelve el TA en minutos, y si el tiempo comprometido de la estación es mayor a la hora en la que el tren llega, 
                    // modifica la hora por el valor del Tiempo Comprometido y luego actualiza el tiempo comprometido (sumando TA).
                    int tiempoAtencion = siguienteEstacion.atenderFormacion(formacionActual, ref tiempoLlegadaProximaEstacion);
                    result.tiempoTotalDemoradoAtencion += tiempoAtencion;
                    resultServicioActual.tiempoTotalDemoradoAtencion += tiempoAtencion;



                    result.pasajerosTotalesTransportados += siguienteEstacion.PasajerosQueSubieronAlTren;
                    resultServicioActual.pasajerosTotalesTransportados += siguienteEstacion.PasajerosQueSubieronAlTren;

                    int totalSobrecargaLegal = formacionActual.cantidadSuperoCapLegal();
                    if (totalSobrecargaLegal > 0)
                    {
                        result.vecesSuperoCapLegal++;
                        resultServicioActual.aumentarCapLegal();
                        resultServicioActual.totalSobrecargaLegal += totalSobrecargaLegal;
                    }

                    int totalPasajerosParados = formacionActual.TotalPasajerosParados();
                    if (totalPasajerosParados > 0)
                    {
                        resultServicioActual.totalPasajerosParados += totalPasajerosParados;
                    }
                    else
                    {
                        result.vecesNoHabiaPasajerosParados++;
                        resultServicioActual.vecesNoHabiaPasajerosParados++;
                    }

                    //Actualizo el nodo que será el inicial en la siguiente iteracion.
                    estacionActual = siguienteEstacion;

                    //Actualizo el tiempo de la formacion para la siguiente iteracion.
                    tiempoSalidaProximaEstacion = tiempoLlegadaProximaEstacion + tiempoAtencion;

                    Console.WriteLine("tiempoViaje={0} | demoraPorAccidenteEnViaje={1} | tiempoLlegadaProximaEstacion={2} | tiempoAtencion={3} | noHayPasajerosParados={4}", tiempoViaje, demoraPorAccidentesEnViaje, tiempoLlegadaProximaEstacion, tiempoAtencion, formacionActual.HayPasajerosParados());
                }

                resultServicioActual.consumoDiesel += formacionActual.consumoDiesel(result);

                resultServicioActual.consumoElectrico += formacionActual.consumoElectrico(result);

                resultServicioActual.tiempoTotal += (tiempoSalidaProximaEstacion - tiempoActual);

                Console.WriteLine("tiempoViaje={0} | demoraPorAccidenteEnViaje={1} | tiempoTotal={2} | tiempoAtencion={3} | noHayPasajerosParados={4}", resultServicioActual.tiempoTotalEnMovimiento, resultServicioActual.tiempoTotalDemoradoIncidente, resultServicioActual.tiempoTotal, resultServicioActual.tiempoTotalDemoradoAtencion, resultServicioActual.vecesNoHabiaPasajerosParados);

                resultServicioActual.resultadosFormaciones.Add(result);

                //Actualiza el tiempo actual a partir del proximo servicio a prestar.
                actualizarSiguienteServicio(out tiempoActual, out servicioActual);
                //Se asigna la formacion que realizara el servicio actual
                formacionActual = servicioActual.GetFormacionRandom();

                _barraProgreso.Porcentaje = tiempoActual * 100.0 / TiempoFinal;
                Console.WriteLine("tiempoActual={0}", tiempoActual);
            }
            //TODO faltan las variables de salida
            Console.WriteLine("Fin Algoritmo");
        }

        private void cargarServicios()
        {
            foreach(Servicios servicio in Traza.ServiciosDisponibles)
            {
                int cantidadEstaciones = 0;
                foreach(Parada parada in servicio.Paradas)
                {
                    cantidadEstaciones++;
                }
                //_tiempoFinal - _tiempoInicial no es correcto porque cada servicio puede arrancar a diferente hora. Esa es hora de la simluacion
                _resultadosServicios.Add(new ResultadoServicio(servicio.Nombre, cantidadEstaciones, _tiempoFinal - _tiempoInicial));
            }
        }

        private void actualizarSiguienteServicio(out int siguienteSalida, out Servicios siguienteServicio)
        {
            siguienteServicio = Traza.ServiciosDisponibles[0];
            siguienteSalida = siguienteServicio.proximoHorarioSalida();
            int auxHorarioSalida;
            foreach (Servicios srv in Traza.ServiciosDisponibles)
            {
                auxHorarioSalida = srv.proximoHorarioSalida();
                if (auxHorarioSalida < siguienteSalida)
                {
                    siguienteSalida = auxHorarioSalida;
                    siguienteServicio = srv;
                }
            }
            siguienteServicio.removerSalida(siguienteSalida); //Al haber usado el horario siguienteSalida lo remuevo del servicio.
        }

        private long calcularIntervalo()
        {
            DateTime tiempo = DateTime.Now;

            return tiempo.Hour * tiempo.Minute * tiempo.Second * tiempo.Millisecond;
        }

        #endregion
    }
}
