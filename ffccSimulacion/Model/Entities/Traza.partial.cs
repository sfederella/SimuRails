using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuRails.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Data.Linq.Mapping;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public partial class Trazas
    {
        /*Estas propiedades no estan ni deben estar mapeadas en la base*/
        public List<Servicios> ServiciosDisponibles
        {
            get { return Trazas_X_Servicios.Select(x => x.Servicios).ToList<Servicios>(); }
        }

        /*public void LimpiarListaLINQParaPoderGuardar()
        {
            _listaServicios_LINQ = new EntitySet<Traza_X_Servicio>();
        }*/

        public void ConfigurarLosServiciosDeLaTraza(int tiempoFinal)
        {
            foreach (Trazas_X_Servicios ts in Trazas_X_Servicios)
                ts.Servicios.ConfigurarServicio(tiempoFinal);
        }

        public void AgregarServicio(Servicios unServicio)
        {
            Trazas_X_Servicios ts = new Trazas_X_Servicios();
            ts.Id = this.Id;
            ts.Servicios = unServicio;
            Trazas_X_Servicios.Add(ts);
        }
    }
}
