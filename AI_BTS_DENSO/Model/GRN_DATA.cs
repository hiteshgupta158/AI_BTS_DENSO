﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI_BTS_DENSO.Model;

namespace AI_BTS_DENSO.Model
{
    class GRN_DATA
    {
        public string GRN_TYPE { get; set; }

        public string GRN_SITE { get; set; }

        public GRN_MST Grn_Mst { get; set; }

        public List<GRN_DTL> lstGrn_Dtl { get; set; }
    }
    
    class QC_REJECTED_DATA
    {
        public QC_MST qc_mst { get; set; }

        public string Part_Name { get; set; }
        
        public List<QC_LABEL_PRINTING> lst_QC_Lbl { get; set; }
    }
}
