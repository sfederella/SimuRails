using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using SimuRails.Model.Simulacion;

namespace SimuRails.Model.Entities
{
    public partial class Servicio
    {
        private List<Parada> _paradas = new List<Parada>(); //Estaciones en las que para la formacion.
        private List<int> _programacion; //Tiempos de salida de la terminal.
    
        /*Estas propiedades no estan ni deben estar mapeadas en la base*/
        public List<Parada> Paradas
        {
            get { return _paradas; }
        }

        public List<int> Programacion
        {
            get { return _programacion; }
            set { _programacion = value; }
        }

        public Estacion Desde { get; set; }

        public Estacion Hasta { get; set; }

        /*Dada una estacion retorna aquella relacion donde dicha estacion es la estacion anterior*/
        public Tramo BuscarRelacionSiguiente(Estacion n)
        {
            return Tramo.Where(x => x.Id_Estacion_Anterior == n.Id).FirstOrDefault();
        }

        public Tramo BuscarRelacionAnterior(Estacion n)
        {
            return Tramo.Where(x => x.Id_Estacion_Siguiente == n.Id).FirstOrDefault();
        }

        /*A partir de la lista de relaciones retorna aquella estacion que es el origen del servicio*/
        public Estacion BuscarEstacionDesde()
        {
            Tramo relacionActual = Tramo.First();
            Estacion estacionActual = relacionActual.Estacion;
            while (relacionActual != null)
            {
                relacionActual = BuscarRelacionAnterior(estacionActual);
                if (relacionActual != null)
                    estacionActual = relacionActual.Estacion;
            }

            return estacionActual;
        }

        public Estacion BuscarEstacionHasta()
        {
            Tramo relacionActual = Tramo.First();
            Estacion estacionActual = relacionActual.Estacion1;
            while (relacionActual != null)
            {
                relacionActual = BuscarRelacionSiguiente(estacionActual);
                if (relacionActual != null)
                    estacionActual = relacionActual.Estacion1;
            }

            return estacionActual;
        }

        public void CargarParadas()
        {
            Estacion estacionActual = Desde;
            agregarParada(estacionActual, true);
            Tramo relacionActual = BuscarRelacionSiguiente(estacionActual);
            while (estacionActual != Hasta)
            {
                estacionActual = relacionActual.Estacion1;
                agregarParada(estacionActual, relacionActual.EstSigEsParada == true);
                relacionActual = BuscarRelacionSiguiente(estacionActual);
            }
            relacionActual = BuscarRelacionAnterior(Hasta);
            agregarParada(Hasta, relacionActual.EstSigEsParada == true);
        }

        public void agregarParada(Estacion nodo, bool para)
        {
            Parada parada = new Parada();
            parada.nodo = nodo;
            parada.para = para;
            _paradas.Add(parada);
        }

        public void ConfigurarFormacionesDelServicio()
        {
            foreach (Servicio_X_Formacion sf in Servicio_X_Formacion)
                sf.Formacion.ConfigurarFormacion();
        }

        /*Esta funcion configura todos los campos que no vienen por base y que dependen del conjunto de relaciones definido para el servicio*/
        public void ConfigurarServicio(int tiempoFinal)
        {
            this.Desde = BuscarEstacionDesde();
            this.Hasta = BuscarEstacionHasta();
            //ConfigurarEstaciones();
            CargarParadas();
            ConfigurarFormacionesDelServicio();
            CargarProgramacion(tiempoFinal);
        }

        /*Permite agregar un tipo de formacion que prestara el servicio*/
        public void AgregarFormacionDispoble(Formacion unaFormacion)
        {
            Servicio_X_Formacion sf = new Servicio_X_Formacion();
            sf.Id_Servicio = this.Id;
            sf.Formacion = unaFormacion;
            Servicio_X_Formacion.Add(sf);

            //_formacionesDisponibles.Add(unaFormacion);
        }

        /*Retorna cada vez una instancia nueva (replica) de alguna de las formaciones disponibles para prestar un servicio*/
        public Formacion GetFormacionRandom()
        {
            List<Servicio_X_Formacion> formacionesDisponibles = Servicio_X_Formacion.ToList<Servicio_X_Formacion>();
            Random rd = new Random();
            int num = rd.Next(0, formacionesDisponibles.Count - 1);
            Formacion unaFormacion = formacionesDisponibles[num].Formacion;
            return unaFormacion.ClonarFormacion();
        }

        public void agregarHorarioSalida(int horarioSalida)
        {
            _programacion.Add(horarioSalida);
            //No es necesario ordenar, devuelvo el menor luego.
        }

        public void agregarProgramacionSalida(List<int> programacionSalida)
        {
            _programacion = programacionSalida;
            /*foreach (int horarioSalida in programacionSalida)
            {
                _programacion.Add(horarioSalida);
            }*/
            //No es necesario ordenar, devuelvo el menor luego.
        }

        public int proximoHorarioSalida()
        {
            //Obtiene la proxima salida para este servicio.
            if (_programacion.Count != 0)
            {
                return _programacion.Min();
            }
            
            //return 1000;
            return 2147483646; //valor teorico del High Value
        }

        public void removerSalida(int salida)
        {
            //Remueve de la programacion una salida ya utilizada.
            _programacion.Remove(salida);
        }

        public Tramo relacionEntre(Estacion nodoInicial, Estacion nodoFinal)
        {
            foreach (Tramo relacion in Tramo)
            {
                if (relacion.relaciona(nodoInicial, nodoFinal))
                {
                    return relacion;
                }
            }
            throw new ApplicationException("No se encontró relacion entre nodos.");
        }

        public bool ServicioEsValido(List<Tramo> sr)
        {
            Tramo relacionPivote = sr.FirstOrDefault();
            Tramo relacionActual = relacionPivote;
            int contadorRelaciones = 0;

            if (relacionPivote == null)
                return false;

            while (relacionActual != null)
            {
                contadorRelaciones++;
                relacionActual = sr.Where(x => x.Estacion.Id == relacionActual.Estacion1.Id).FirstOrDefault();
            }

            //relacionActual = relacionPivote;
            relacionActual = sr.Where(x => x.Estacion1.Id == relacionPivote.Estacion.Id).FirstOrDefault();
            while(relacionActual!= null)
            {
                contadorRelaciones++;
                relacionActual = sr.Where(x => x.Estacion1.Id == relacionActual.Estacion.Id).FirstOrDefault();
            }

            if (sr.Count == contadorRelaciones)
                return true;
            else
                return false;
        }

        private void CargarProgramacion(int tiempoFinal)
        {
            List<int> listProgramacionRelativo = new List<int>();
            string[] listProgramacionStr = ProgramacionStr.Split(';');
            foreach (string horarioStr in listProgramacionStr)
            {
                DateTime horario = DateTime.ParseExact(horarioStr, "HH:mm", CultureInfo.InvariantCulture);
                listProgramacionRelativo.Add(horario.Hour * 60 + horario.Minute);
            };

            _programacion = new List<int>();
            var acumDiasMinutos = 0;
            while (acumDiasMinutos <= tiempoFinal)
            {
                foreach (int horarioRelativo in listProgramacionRelativo)
                {
                    _programacion.Add(horarioRelativo + acumDiasMinutos);
                }
                acumDiasMinutos += 24 * 60;
            }

        }
    }
}
