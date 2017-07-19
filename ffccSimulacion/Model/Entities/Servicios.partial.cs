namespace ffccSimulacion.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Simulacion;
    using System.Globalization;

    public partial class Servicios
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

        public Estaciones Desde { get; set; }

        public Estaciones Hasta { get; set; }

        /*public void LimpiarListaLINQParaPoderGuardar()
        {
            _relaciones = new EntitySet<Relacion>();
            _listaFormaciones_LINQ = new EntitySet<Servicio_X_Formacion>();
        }*/

        /*A partir de la lista de relaciones definidas para el servicio se configuran las distitnas estaciones*/
        /*public void ConfigurarEstaciones()
        {
            Relaciones r;
            Estaciones nodoRelacion;

            //Se cargan todas las relaciones siguientes de los nodos. Se recorre de atras para adelante
            Estaciones nodoActual = this.Desde;
            while (nodoActual != null)
            {
                nodoRelacion = nodoActual;
                while (nodoRelacion != this.Hasta)
                {
                    r = BuscarRelacionSiguiente(nodoRelacion);
                    nodoActual.agregarRelacionSiguiente(r);
                    nodoRelacion = r.Estaciones1;
                }

                if (nodoActual != this.Hasta)
                {
                    r = BuscarRelacionSiguiente(nodoActual);
                    nodoActual = r.Estaciones1;
                }
                else
                    nodoActual = null;
            }

            nodoActual = this.Hasta;
            while (nodoActual != null)
            {
                nodoRelacion = nodoActual;
                while (nodoRelacion != this.Desde)
                {
                    r = BuscarRelacionAnterior(nodoRelacion);
                    nodoActual.agregarRelacionAnterior(r);
                    nodoRelacion = r.Estaciones;
                }

                if (nodoActual != this.Desde)
                {
                    r = BuscarRelacionAnterior(nodoActual);
                    nodoActual = r.Estaciones;
                }
                else
                    nodoActual = null;
            }
        }*/

        /*Dada una estacion retorna aquella relacion donde dicha estacion es la estacion anterior*/
        public Relaciones BuscarRelacionSiguiente(Estaciones n)
        {
            return Relaciones.Where(x => x.Id_Estacion_Anterior == n.Id).FirstOrDefault();
        }

        public Relaciones BuscarRelacionAnterior(Estaciones n)
        {
            return Relaciones.Where(x => x.Id_Estacion_Siguiente == n.Id).FirstOrDefault();
        }

        /*A partir de la lista de relaciones retorna aquella estacion que es el origen del servicio*/
        public Estaciones BuscarEstacionDesde()
        {
            Relaciones relacionActual = Relaciones.First();
            Estaciones estacionActual = relacionActual.Estaciones;
            while (relacionActual != null)
            {
                relacionActual = BuscarRelacionAnterior(estacionActual);
                if (relacionActual != null)
                    estacionActual = relacionActual.Estaciones;
            }

            return estacionActual;
        }

        public Estaciones BuscarEstacionHasta()
        {
            Relaciones relacionActual = Relaciones.First();
            Estaciones estacionActual = relacionActual.Estaciones1;
            while (relacionActual != null)
            {
                relacionActual = BuscarRelacionSiguiente(estacionActual);
                if (relacionActual != null)
                    estacionActual = relacionActual.Estaciones1;
            }

            return estacionActual;
        }

        public void CargarParadas()
        {
            Estaciones estacionActual = Desde;
            agregarParada(estacionActual, true);
            Relaciones relacionActual = BuscarRelacionSiguiente(estacionActual);
            while (estacionActual != Hasta)
            {
                estacionActual = relacionActual.Estaciones1;
                agregarParada(estacionActual, relacionActual.Est_Sig_Es_Parada == 1);
                relacionActual = BuscarRelacionSiguiente(estacionActual);
            }
            relacionActual = BuscarRelacionAnterior(Hasta);
            agregarParada(Hasta, relacionActual.Est_Sig_Es_Parada == 1);
        }

        public void agregarParada(Estaciones nodo, bool para)
        {
            Parada parada = new Parada();
            parada.nodo = nodo;
            parada.para = para;
            _paradas.Add(parada);
        }

        public void ConfigurarFormacionesDelServicio()
        {
            foreach (Servicios_X_Formaciones sf in Servicios_X_Formaciones)
                sf.Formaciones.ConfigurarFormacion();
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
        public void AgregarFormacionDispoble(Formaciones unaFormacion)
        {
            Servicios_X_Formaciones sf = new Servicios_X_Formaciones();
            sf.Id_Servicio = this.Id;
            sf.Formaciones = unaFormacion;
            Servicios_X_Formaciones.Add(sf);

            //_formacionesDisponibles.Add(unaFormacion);
        }

        /*Retorna cada vez una instancia nueva (replica) de alguna de las formaciones disponibles para prestar un servicio*/
        public Formaciones GetFormacionRandom()
        {
            List<Servicios_X_Formaciones> formacionesDisponibles = Servicios_X_Formaciones.ToList<Servicios_X_Formaciones>();
            Random rd = new Random();
            int num = rd.Next(0, formacionesDisponibles.Count - 1);
            Formaciones unaFormacion = formacionesDisponibles[num].Formaciones;
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

        public Relaciones relacionEntre(Estaciones nodoInicial, Estaciones nodoFinal)
        {
            foreach (Relaciones relacion in Relaciones)
            {
                if (relacion.relaciona(nodoInicial, nodoFinal))
                {
                    return relacion;
                }
            }
            throw new ApplicationException("No se encontró relacion entre nodos.");
        }

        /*public Estaciones proximoNodo(Estaciones nodoActual)
        {
            foreach (Relaciones proximaRelacion in nodoActual.RelacionesSiguientes)
            {
                foreach (Parada parada in _paradas)
                {
                    if (proximaRelacion.Id_Estacion_Siguiente == parada.nodo.Id)
                    {
                        return parada.nodo;
                    }
                }
            }
            foreach (Relaciones proximaRelacion in nodoActual.RelacionesAnteriores)
            {
                foreach (Parada parada in _paradas)
                {
                    if (proximaRelacion.Id_Estacion_Anterior == parada.nodo.Id)
                    {
                        return parada.nodo;
                    }
                }
            }
            throw new ApplicationException("Error de configuración de nodos del servicio.");
        }*/

        public bool ServicioEsValido(List<Relaciones> sr)
        {
            Relaciones relacionPivote = sr.FirstOrDefault();
            Relaciones relacionActual = relacionPivote;
            int contadorRelaciones = 0;

            if (relacionPivote == null)
                return false;

            while (relacionActual != null)
            {
                contadorRelaciones++;
                relacionActual = sr.Where(x => x.Estaciones.Id == relacionActual.Estaciones1.Id).FirstOrDefault();
            }

            //relacionActual = relacionPivote;
            relacionActual = sr.Where(x => x.Estaciones1.Id == relacionPivote.Estaciones.Id).FirstOrDefault();
            while(relacionActual!= null)
            {
                contadorRelaciones++;
                relacionActual = sr.Where(x => x.Estaciones1.Id == relacionActual.Estaciones.Id).FirstOrDefault();
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
