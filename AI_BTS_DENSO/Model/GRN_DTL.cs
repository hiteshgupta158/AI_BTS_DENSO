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
    
    public partial class GRN_DTL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GRN_DTL()
        {
            this.GRN_LABEL_PRINTING = new HashSet<GRN_LABEL_PRINTING>();
            this.QC_MST = new HashSet<QC_MST>();
        }
    
        public int GRN_DTL_ID { get; set; }
        public Nullable<int> GRN_MST_ID { get; set; }
        public string PART_NO { get; set; }
        public string PART_NAME { get; set; }
        public Nullable<int> QUANTITY { get; set; }
        public string SUPPLIER_BATCH_NO { get; set; }
        public string PACK_SIZE { get; set; }
        public Nullable<int> STATUS { get; set; }
        public Nullable<int> IS_BLOCK { get; set; }
        public string PALLETE_NO { get; set; }
        public string PALLET_SIZE { get; set; }
        public string PART_TYPE { get; set; }
        public string PALLETE_NO_BARCODE { get; set; }
    
        public virtual GRN_MST GRN_MST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GRN_LABEL_PRINTING> GRN_LABEL_PRINTING { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QC_MST> QC_MST { get; set; }
    }
}
