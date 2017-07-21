namespace SimuRails.Model.Entities
{
    using System.Collections.Generic;
    using Simulacion;


    public partial class Formaciones
    {
        private List<Coches> _listaCoches = new List<Coches>();

        /*Esta propiedad no esta ni tiene que estar mapeada en la base*/
        public List<Coches> ListaCoches
        {
            get { return _listaCoches; }
        }

        public void ConfigurarFormacion()
        {
            if (_listaCoches.Count == 0)
            {
                foreach (Formaciones_X_Coches fc in Formaciones_X_Coches)
                    for (int i = 0; i < fc.VecesRepetido; i++)
                        _listaCoches.Add(fc.Coches.ClonarCoche());
            }
        }

        public void agregarCoche(Coches coche, int vecesRepetido)
        {
            /*Es necesario esto para poder guardar luego los nuevos coches que se vallan agregando a una formacion ya existente*/
            Formaciones_X_Coches fc = new Formaciones_X_Coches();
            fc.Coches = coche;
            fc.VecesRepetido = vecesRepetido;
            fc.Id_Formacion = this.Id;
            Formaciones_X_Coches.Add(fc);

            for (int i = 0; i < vecesRepetido; i++)
                _listaCoches.Add(coche);
        }

        /*Esta funcion retorna una nueva instancia de formacion exactamente igual a si misma. CLONA LA FORMACION*/
        public Formaciones ClonarFormacion()
        {
            return (Formaciones)this.MemberwiseClone();
        }

        public int pasajerosSentados()
        {
            int pasajerosSentados = 0;

            foreach (Coches c in _listaCoches)
                pasajerosSentados += c.PasajerosSentados;

            return pasajerosSentados;
        }

        public int pasajerosParados()
        {
            int pasajerosParados = 0;

            foreach (Coches c in _listaCoches)
                pasajerosParados += c.PasajerosParados;

            return pasajerosParados;
        }

        public int cantidadAsientos()
        {
            int cantidadAsientos = 0;

            foreach (Coches c in _listaCoches)
                cantidadAsientos += c.CantidadAsientos;

            return cantidadAsientos;
        }

        public int capacidadLegal()
        {
            int capacidadLegal = 0;

            foreach (Coches c in _listaCoches)
                capacidadLegal += c.MaximoLegalPasajeros;

            return capacidadLegal;
        }

        public int capacidadMaxima()
        {
            int capacidadMaxima = 0;

            foreach (Coches c in _listaCoches)
                capacidadMaxima += c.CapacidadMaximaPasajeros;

            return capacidadMaxima;
        }

        public Flujo_Pasajeros recibir(int genteASubir)
        {
            int exceso = genteASubir;

            foreach (Coches c in _listaCoches)
            {
                c.DesenderPasajeros();
                if (exceso > 0)
                    exceso = c.recibir(exceso);
            }

            Flujo_Pasajeros fp = new Flujo_Pasajeros();
            fp.pasajerosQueSubieronAlTren = genteASubir - exceso;
            fp.pasajerosQueNoSubieronAlTren = exceso;

            return fp; //Se retorna la cantidad de gente que no pudo subir.
        }

        public int TotalPasajerosEnFormacion()
        {
            int totalPasajeros = 0;
            foreach (Coches c in _listaCoches)
                totalPasajeros += (c.PasajerosParados + c.PasajerosSentados);

            return totalPasajeros;
        }

        public bool FormacionSuperoCapLegal()
        {
            return TotalPasajerosEnFormacion() >= capacidadLegal();
        }

        public int cantidadSuperoCapLegal()
        {
            return TotalPasajerosEnFormacion() - capacidadLegal();
        }

        public bool HayPasajerosParados()
        {
            foreach (Coches c in _listaCoches)
            {
                if (c.PasajerosParados != 0)
                    return true;
            }

            return false;
        }

        public int TotalPasajerosParados()
        {
            int total = 0;
            foreach (Coches c in _listaCoches)
            {
                if (c.PasajerosParados != 0)
                    total += c.PasajerosParados;
            }

            return total;
        }

        public double consumoDiesel(ResultadoFormacion resultadoFormacion)
        {
            double consumoDiesel = 0;
            foreach (Coches coche in _listaCoches)
            {
                if (coche.EsLocomotora == 1)
                {
                    if (coche.TipoConsumo == (int)TipoConsumo.Disel)
                    {
                        consumoDiesel += (double)coche.ConsumoMovimiento * resultadoFormacion.tiempoTotalEnMovimiento + (double)coche.ConsumoParado * (resultadoFormacion.tiempoTotalDemoradoAtencion + resultadoFormacion.tiempoTotalDemoradoIncidente);
                    }
                }
            }

            return consumoDiesel;
        }

        public double consumoElectrico(ResultadoFormacion resultadoFormacion)
        {
            double consumoElectrico = 0;
            foreach (Coches coche in _listaCoches)
            {
                if (coche.EsLocomotora == 1)
                {
                    if (coche.TipoConsumo == (int)TipoConsumo.Electrico)
                    {
                        consumoElectrico += (double)coche.ConsumoMovimiento * resultadoFormacion.tiempoTotalEnMovimiento + (double)coche.ConsumoParado * (resultadoFormacion.tiempoTotalDemoradoAtencion + resultadoFormacion.tiempoTotalDemoradoIncidente);
                    }
                }

            }
            return consumoElectrico;
        }
    }
}
