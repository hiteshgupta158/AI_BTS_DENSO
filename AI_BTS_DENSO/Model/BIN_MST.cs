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
    
    public partial class BIN_MST
    {
        public int BIN_MST_ID { get; set; }
        public string BIN_NAME { get; set; }
        public Nullable<int> BIN_TYPE_MST_ID { get; set; }
        public Nullable<int> ACTIVE { get; set; }
        public string STATUS { get; set; }
    
        public virtual BIN_TYPE_MST BIN_TYPE_MST { get; set; }
    }
}
