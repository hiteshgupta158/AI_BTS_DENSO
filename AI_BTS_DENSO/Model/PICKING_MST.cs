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
    
    public partial class PICKING_MST
    {
        public int PICK_MST_ID { get; set; }
        public Nullable<int> STO_MST_ID { get; set; }
        public string PART_NO { get; set; }
        public Nullable<int> REQUESTED_QTY { get; set; }
        public Nullable<int> STATUS { get; set; }
        public Nullable<int> PICKED_QTY { get; set; }
        public Nullable<int> PICKED_BY { get; set; }
        public Nullable<System.DateTime> PICKED_ON { get; set; }
        public string PALET_NO { get; set; }
        public Nullable<int> MRN_MST_ID { get; set; }
    
        public virtual MIXED_PALLET_STO MIXED_PALLET_STO { get; set; }
        public virtual MRN_MST MRN_MST { get; set; }
        public virtual USER_MST USER_MST { get; set; }
    }
}
