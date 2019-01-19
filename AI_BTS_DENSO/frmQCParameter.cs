using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AI_BTS_DENSO.Common;
using System.IO;
using AI_BTS_DENSO.Model;
using ZTCommon;

namespace AI_BTS_DENSO
{
    public partial class frmQCParameter : Form
    {
        AI_BTS_DENSO.Common.clsCommon common = new AI_BTS_DENSO.Common.clsCommon();
        clsExcel clsexcel;

        public frmQCParameter()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                ofd.ShowDialog();
                ofd.Filter = "Excel Files (*.xls)|*.xls|Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                if (ofd.FileName != "")
                {
                    ofd.CheckFileExists = true;
                    string lstrFileName = ofd.FileName;

                    if (File.Exists(lstrFileName))
                    {
                        clsexcel = new clsExcel(lstrFileName);
                        if (clsexcel != null)
                        {
                            DataTable xlsSchema = clsexcel.xlsdbo.GetSchema();

                            DataTable dtDBColumn = common.getTable("select Name from sys.columns where object_id= OBJECT_ID('QC_PARAMETER')");
                            if(dtDBColumn.Rows.Count>0)
                            {

                            }
                        }
                    }
                    else
                        ClsMessage.ShowError("Selected file does not exists.");
                }
                else
                    ClsMessage.ShowInfo("No File is selected");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
