//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SimuRails.Model.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tramo
    {
        public int Id { get; set; }
        public int Id_Estacion_Anterior { get; set; }
        public int Id_Estacion_Siguiente { get; set; }
        public int Id_Servicio { get; set; }
        public int Distancia { get; set; }
        public int TiempoViaje { get; set; }
        public bool EstSigEsParada { get; set; }
    
        public virtual Estacion Estacion { get; set; }
        public virtual Estacion Estacion1 { get; set; }
        public virtual Servicio Servicio { get; set; }
    }
}
