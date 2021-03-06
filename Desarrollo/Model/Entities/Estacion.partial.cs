using System.Collections.Generic;
using System.Linq;
using SimuRails.Model.Simulacion;

namespace SimuRails.Model.Entities
{
    public partial class Estacion
    {

        public Estacion(int id, string nombre)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.TiempoComprometido = 0;
            this.UltimaAtencion = 0;
            this.GenteEsperando = 0;
            this.Estacion_X_Incidente = new HashSet<Estacion_X_Incidente>();
            this.Tramo = new HashSet<Tramo>();
            this.Tramo1 = new HashSet<Tramo>();
        }

        //private List<Relaciones> _anteriores = new List<Relaciones>();
        //private List<Relaciones> _siguientes = new List<Relaciones>();
        private int _genteEsperando = 0;
        private int _ultimaAtencion = 0;
        private int _tiempoComprometido = 0;
        private int _pasajerosQueSubieronAlTren = 0;

        /*Estas propiedades no tienen que estar mapeadas en la base*/
        public int GenteEsperando
        {
            get { return _genteEsperando; }
            set { _genteEsperando = value; }
        }

        public int UltimaAtencion
        {
            get { return _ultimaAtencion; }
            set { _ultimaAtencion = value; }
        }

        public int TiempoComprometido
        {
            get { return _tiempoComprometido; }
            set { _tiempoComprometido = value; }
        }

        public int PasajerosQueSubieronAlTren { get { return _pasajerosQueSubieronAlTren; } }

        /*public List<Relaciones> RelacionesAnteriores
        {
            get { return _anteriores; }
        }*/

        /*public List<Relaciones> RelacionesSiguientes
        {
            get { return _siguientes; }
        }*/

        public List<Incidente> ListaIncidentes
        {
            get { return Estacion_X_Incidente.Select(x => x.Incidente).ToList<Incidente>(); }
        }


        public void AgregarIncidente(Incidente i)
        {
            Estacion_X_Incidente ie = new Estacion_X_Incidente();
            ie.Incidente = i;
            ie.Id_Estacion = this.Id;
            Estacion_X_Incidente.Add(ie);
        }

        /*public void agregarRelacionAnterior(Relaciones relacion)
        {
            _anteriores.Add(relacion);
        }*/

        /*public void agregarRelacionSiguiente(Relaciones relacion)
        {
            _siguientes.Add(relacion);
        }*/

        /*Retorna cual fue el tiempo de atencion de la formacion*/
        public int atenderFormacion(Formacion formacion, ref int tiempoLlegada)
        {
            //CALCULO LA LLEGADA
            if (_tiempoComprometido < tiempoLlegada)
                _tiempoComprometido = tiempoLlegada;
            else
                tiempoLlegada = _tiempoComprometido; //El tiempo de llegada se actualiza.

            //ATIENDO LOS PASAJEROS
            actualizarGenteEsperando(tiempoLlegada);

            //CALCULO EL TIEMPO DE ATENCION
            int tiempoAtencionActual = tiempoAtencion(_genteEsperando, formacion.capacidadMaxima(), formacion.TotalPasajerosEnFormacion());

            Flujo_Pasajeros fp = formacion.recibir(_genteEsperando);
            _pasajerosQueSubieronAlTren = fp.pasajerosQueSubieronAlTren;
            _genteEsperando = fp.pasajerosQueNoSubieronAlTren;

            //ACTUALIZO EL TIEMPO COMPROMETIDO Y LA ULTIMA ATENCION
            _tiempoComprometido += tiempoAtencionActual;
            _ultimaAtencion = _tiempoComprometido; //Por ahora son iguales.

            //RETORNO EL TIEMPO DE ATENCION EN LA ESTACION
            return tiempoAtencionActual;
        }

        private void actualizarGenteEsperando(int tiempoActual)
        {
            //TODO: para esto en el futuro hay que utilizar la FDP que se define en el ABM de estacion
            //Calculo de la gente que hay esperando en la estacion.
            //_genteEsperando += 20 * (tiempoActual - _ultimaAtencion);
            switch (TipoFDP)
            {
                case 0:
                    _genteEsperando = Fdp.Normal(PersonasEsperandoMin, PersonasEsperandoMax) * diferenciaUltimaAtencion(tiempoActual);

                    break;
                case 1:
                    //TODO calculo el delta en horas
                    //double horasDeEspera = ((double)(tiempoActual - _ultimaAtencion)) / 60;
                    //_genteEsperando = Fdp.Gamma(PersonasEsperandoMin, PersonasEsperandoMax, horasDeEspera);
                    break;
                case 2:
                    //TODO: Definir gente por minuto.
                    //_genteEsperando = Fdp.Poisson(_personasEsperandoMin, _personasEsperandoMax);
                    _genteEsperando += 20 * (tiempoActual - _ultimaAtencion);
                    break;
            }
        }

        private int diferenciaUltimaAtencion(int tiempoActual)
        {
            if (_ultimaAtencion == 0 && (tiempoActual - _ultimaAtencion) > 15)
                return 15;
            else
                return tiempoActual - _ultimaAtencion;
        }

        /*Retorna el tiempo que lo toma a la estacion para atener al tren y despacharlo en minutos*/
        private int tiempoAtencion(int genteEsperando, int capacidadMaximaFormacion, int pasajerosEnFormacion)
        {
            //Calculo del tiempo de atencion en la estacion.
            if ((double)genteEsperando < (double)capacidadMaximaFormacion * 0.25)
            {
                int segundosEspera = Fdp.Normal(30, 120);
                return segundosEspera / 60;
            }
            else if ((double)genteEsperando >= (double)capacidadMaximaFormacion * 0.25 && (double)genteEsperando < (double)capacidadMaximaFormacion * 0.75)
            {
                int segundosEspera = Fdp.Normal(120, 180);
                return segundosEspera / 60;
            }
            else
            {
                int segundosEspera = Fdp.Normal(180, 240);
                return segundosEspera / 60;
            }


        }
    }
}