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
    
    public partial class Incidente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Incidente()
        {
            this.Estacion_X_Incidente = new HashSet<Estacion_X_Incidente>();
        }
    
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int ProbabilidadOcurrencia { get; set; }
        public int TiempoDemora { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Estacion_X_Incidente> Estacion_X_Incidente { get; set; }
    }
}