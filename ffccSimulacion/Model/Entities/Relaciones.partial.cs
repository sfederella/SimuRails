namespace ffccSimulacion.Model.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Relaciones
    {
        public Relaciones ClonarRelacion()
        {
            return (Relaciones)this.MemberwiseClone();
        }

        public bool relaciona(Estaciones nodoInicial, Estaciones nodoFinal)
        {
            //TODO Revisar si la relacion es bidireccional.
            if ((Id_Estacion_Anterior == nodoInicial.Id && Id_Estacion_Siguiente == nodoFinal.Id) || (Id_Estacion_Anterior == nodoFinal.Id && Id_Estacion_Siguiente == nodoInicial.Id))
            {
                return true;
            }
            return false;
        }

        public int demoraPorAccidentes()
        {
            int totalDemora = 0;
            foreach (Incidentes incidentePosible in Estaciones1.ListaIncidentes)
            {
                if (incidentePosible.Ocurre())
                {
                    totalDemora += incidentePosible.TiempoDemora;
                }
            }
            return totalDemora;
        }
    }
}
