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
    
    public partial class PICKING_DTL
    {
        public int PICKING_DTL_ID { get; set; }
        public Nullable<int> PICKING_MST_ID { get; set; }
        public string PART_NO { get; set; }
        public Nullable<int> PACK_SIZE { get; set; }
        public Nullable<int> QUANTITY { get; set; }
        public Nullable<int> LOCATION_MST_ID { get; set; }
        public string BOX_BARCODE { get; set; }
        public Nullable<int> STATUS { get; set; }
        public Nullable<int> PICKED_BY { get; set; }
        public Nullable<System.DateTime> PICKED_ON { get; set; }
    
        public virtual LOCATION_MST LOCATION_MST { get; set; }
    }
}
