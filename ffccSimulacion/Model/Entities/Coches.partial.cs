namespace ffccSimulacion.Model.Entities
{
    using Simulacion;

    public partial class Coches
    {
        private int _pasajerosSentados = 0;
        private int _pasajerosParados = 0;

        /*Estos 2 campos no estan y no deben estar mapeados en la base de datos*/
        public int PasajerosSentados
        {
            get { return _pasajerosSentados; }
            set { _pasajerosSentados = value; }
        }

        public int PasajerosParados
        {
            get { return _pasajerosParados; }
            set { _pasajerosParados = value; }
        }

        public int asientosRestantes()
        {
            return CantidadAsientos - _pasajerosSentados;
        }

        public int capacidadLegalRestante()
        {
            return MaximoLegalPasajeros - _pasajerosSentados - _pasajerosParados;
        }

        public int capacidadMaximaRestante()
        {
            return CapacidadMaximaPasajeros - _pasajerosSentados - _pasajerosParados;
        }

        public Coches ClonarCoche()
        {
            return (Coches)this.MemberwiseClone(); ;
        }

        public int recibir(int pasajerosASubir)
        {
            int exceso = pasajerosASubir;

            //PRIMERO SE SIENTAN LA MAYOR CANTIDAD POSIBLE DE PASAJEROS
            int pasajerosASentar;
            if (exceso >= asientosRestantes())
            {
                pasajerosASentar = asientosRestantes();
            }
            else
            {
                pasajerosASentar = exceso;
            }
            exceso -= pasajerosASentar;
            _pasajerosSentados += pasajerosASentar;

            //EL RESTO DE LOS PASAJEROS VAN PARADOS HASTA LA CAPACIDAD LEGAL
            int pasajerosAEntrar;
            if (exceso >= capacidadLegalRestante())
            {
                pasajerosAEntrar = capacidadLegalRestante();
            }
            else
            {
                pasajerosAEntrar = exceso;
            }
            exceso -= pasajerosAEntrar;
            _pasajerosParados += pasajerosAEntrar;

            //EL RESTO DE LOS PASAJEROS VAN PARADOS HASTA LA CAPACIDAD MAXIMA
            if (exceso >= capacidadMaximaRestante())
            {
                pasajerosAEntrar = capacidadMaximaRestante();
            }
            else
            {
                pasajerosAEntrar = exceso;
            }
            exceso -= pasajerosAEntrar;
            _pasajerosParados += pasajerosAEntrar;

            //EL RESTO DE LOS PASAJEROS NO PODRA ENTRAR
            return exceso;
        }

        /*Procesa el desenso de pasajeros en el coche. Retorna la cantidad de pasajeros total que desendio del coche*/
        public int DesenderPasajeros()
        {
            /*Random rnd = new Random();
            int genteSentadaDesciende = rnd.Next(1, 20);
            int genteParadaDesciende = rnd.Next(1, 20);

            if (genteSentadaDesciende >= _pasajerosSentados)
                _pasajerosSentados = 0;
            else
                _pasajerosSentados = _pasajerosSentados - genteSentadaDesciende;

            if (genteParadaDesciende >= _pasajerosParados)
                _pasajerosParados = 0;
            else
                _pasajerosParados = _pasajerosParados - genteParadaDesciende;

            return genteParadaDesciende + genteSentadaDesciende;*/

            //TODO: No se considera el vaciado de personas en la terminal.
            int totalPasajeros = _pasajerosSentados + _pasajerosParados;
            int porcentajeABajar = Fdp.Normal(10, 100);
            int pasajerosABajar = totalPasajeros * porcentajeABajar / 100;
            int restanBajar = pasajerosABajar;
            if (restanBajar >= _pasajerosParados)
            {
                //TODOS LOS PASAJEROS PARADOS BAJAN
                restanBajar -= _pasajerosParados;
                _pasajerosParados = 0;
                //PUEDE HABER MAS PASAJEROS PARA BAJAR
                if (restanBajar > 0)
                {
                    _pasajerosSentados -= pasajerosABajar;
                    restanBajar = 0;
                }
            }
            else
            {
                //BAJA SOLO PARTE DE LOS PASAJEROS PARADOS, NINGUNO DE LOS SENTADOS
                _pasajerosSentados -= restanBajar;
                restanBajar = 0;
            }
            return pasajerosABajar;
        }
    }
}
