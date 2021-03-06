//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AI_BTS_DENSO.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class MATERIAL_MST
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MATERIAL_MST()
        {
            this.FG_BOM = new HashSet<FG_BOM>();
        }
    
        public int MATERIAL_MST_ID { get; set; }
        public string PART_NO { get; set; }
        public string PART_NAME { get; set; }
        public Nullable<int> MATERIAL_TYPE_MST_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<int> PRIMARY_UOM_ID { get; set; }
        public Nullable<int> SECONDARY_UOM_ID { get; set; }
        public Nullable<int> PACK_SIZE { get; set; }
        public Nullable<int> ACTIVE { get; set; }
        public Nullable<int> CAGE_QTY { get; set; }
        public Nullable<int> BAG_QTY { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FG_BOM> FG_BOM { get; set; }
        public virtual UOM_MST UOM_MST { get; set; }
        public virtual UOM_MST UOM_MST1 { get; set; }
    }
}
