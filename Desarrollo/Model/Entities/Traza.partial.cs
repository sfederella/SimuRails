using System.Linq;
using System.Collections.Generic;

namespace SimuRails.Model.Entities
{

    public partial class Traza
    {
        /*Estas propiedades no estan ni deben estar mapeadas en la base*/
        public List<Servicio> ServiciosDisponibles
        {
            get { return Traza_X_Servicio.Select(x => x.Servicio).ToList<Servicio>(); }
        }

        /*public void LimpiarListaLINQParaPoderGuardar()
        {
            _listaServicios_LINQ = new EntitySet<Traza_X_Servicio>();
        }*/

        public void ConfigurarLosServiciosDeLaTraza(int tiempoFinal)
        {
            foreach (Traza_X_Servicio ts in Traza_X_Servicio)
                ts.Servicio.ConfigurarServicio(tiempoFinal);
        }

        public void AgregarServicio(Servicio unServicio)
        {
            Traza_X_Servicio ts = new Traza_X_Servicio();
            ts.Id = this.Id;
            ts.Servicio = unServicio;
            Traza_X_Servicio.Add(ts);
        }
    }
}
