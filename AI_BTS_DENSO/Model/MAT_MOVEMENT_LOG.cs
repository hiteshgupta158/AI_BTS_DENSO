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
    
    public partial class MAT_MOVEMENT_LOG
    {
        public int ID { get; set; }
        public Nullable<int> LOCATION_MST_ID { get; set; }
        public string MAT_BARCODE { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_ON { get; set; }
        public Nullable<int> SITE_MST_ID { get; set; }
        public Nullable<int> STATUS { get; set; }
    
        public virtual LOCATION_MST LOCATION_MST { get; set; }
    }
}
