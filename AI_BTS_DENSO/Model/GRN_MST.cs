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
    
    public partial class GRN_MST
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GRN_MST()
        {
            this.GRN_DTL = new HashSet<GRN_DTL>();
        }
    
        public int GRN_MST_ID { get; set; }
        public Nullable<int> SITE_MST_ID { get; set; }
        public string A_NOTICE_NO { get; set; }
        public Nullable<System.DateTime> A_NOTICE_DATE { get; set; }
        public string INVOICE_NO { get; set; }
        public Nullable<System.DateTime> INVOICE_DATE { get; set; }
        public Nullable<int> GRN_TYPE_MST_ID { get; set; }
        public Nullable<int> CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string STO_NO { get; set; }
        public string CONTAINER_NO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GRN_DTL> GRN_DTL { get; set; }
        public virtual SITE_MST SITE_MST { get; set; }
        public virtual USER_MST USER_MST { get; set; }
        public virtual GRN_TYPE_MST GRN_TYPE_MST { get; set; }
    }
}
