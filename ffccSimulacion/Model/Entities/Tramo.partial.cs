namespace SimuRails.Model.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tramo
    {
        public Tramo ClonarRelacion()
        {
            return (Tramo)this.MemberwiseClone();
        }

        public bool relaciona(Estacion nodoInicial, Estacion nodoFinal)
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
            foreach (Incidente incidentePosible in Estacion1.ListaIncidentes)
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
