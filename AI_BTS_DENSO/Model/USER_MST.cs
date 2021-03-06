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
    
    public partial class USER_MST
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER_MST()
        {
            this.GRN_LABEL_PRINTING = new HashSet<GRN_LABEL_PRINTING>();
            this.GRN_MST = new HashSet<GRN_MST>();
            this.QC_MST = new HashSet<QC_MST>();
            this.QC_REJECTION_NOTE = new HashSet<QC_REJECTION_NOTE>();
            this.STO_MST = new HashSet<STO_MST>();
            this.PICKING_MST = new HashSet<PICKING_MST>();
        }
    
        public int USER_MST_ID { get; set; }
        public string LOGIN_ID { get; set; }
        public string USER_NAME { get; set; }
        public string USER_PASSWORD { get; set; }
        public Nullable<int> USER_ROLE_MST_ID { get; set; }
        public Nullable<int> ACTIVE { get; set; }
        public Nullable<int> SITE_MST_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GRN_LABEL_PRINTING> GRN_LABEL_PRINTING { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GRN_MST> GRN_MST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QC_MST> QC_MST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QC_REJECTION_NOTE> QC_REJECTION_NOTE { get; set; }
        public virtual SITE_MST SITE_MST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STO_MST> STO_MST { get; set; }
        public virtual USER_ROLE_MST USER_ROLE_MST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PICKING_MST> PICKING_MST { get; set; }
    }
}
