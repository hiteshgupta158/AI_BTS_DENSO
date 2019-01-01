using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using AI_BTS_DENSO.Reports;
using AI_BTS_DENSO.Common;
using ThoughtWorks.QRCode;
using ThoughtWorks.QRCode.Codec;

namespace AI_BTS_DENSO
{
    public partial class frmPalleteInvoiceReport : Form
    {
        ReportDocument rd;// = new ReportDocument();
        DataTable dt;// = new DataTable();
        clsCommon common = new clsCommon();

        public frmPalleteInvoiceReport(ReportDocument prd ,DataTable pdt)
        {
            InitializeComponent();
            rd = prd;
            dt = pdt;
        }

        private void frmPalleteInvoiceReport_Load(object sender, EventArgs e)
        {
            try
            {
                rd.SetDataSource(dt);
                ParameterFields pms = new ParameterFields();
                //======================================================================
                //Label value for Report Title
                //======================================================================
                ParameterField REPORT_TITLE = new ParameterField();
                ParameterDiscreteValue dval = new ParameterDiscreteValue();
                REPORT_TITLE.Name = "rptTitle";

                dval.Value = "DENSO, BANGALORE";

                REPORT_TITLE.CurrentValues.Add(dval);
                pms.Add(REPORT_TITLE);
                //======================================================================


                //======================================================================
                //Label value for invoice No. or A_Notice No.
                //======================================================================
                ParameterField A_NOTICE_NO_LBL = new ParameterField();
                ParameterDiscreteValue dval1 = new ParameterDiscreteValue();
                A_NOTICE_NO_LBL.Name = "ANoticeNoLbl";

                if (dt.Rows[0]["A_NOTICE_NO"].ToString() == dt.Rows[0]["INVOICE_NO"].ToString())
                    dval1.Value = "Invoice No.:";
                else
                    dval1.Value = "A Notice No.: ";

                A_NOTICE_NO_LBL.CurrentValues.Add(dval1);
                pms.Add(A_NOTICE_NO_LBL);
                //======================================================================


                //======================================================================
                //Label value for invoice Date
                //======================================================================
                ParameterField A_NOTICE_DT_LBL = new ParameterField();
                //ParameterDiscreteValue dval1 = new ParameterDiscreteValue();
                A_NOTICE_DT_LBL.Name = "ANoticeDTLbl";

                if (dt.Rows[0]["A_NOTICE_NO"].ToString() == dt.Rows[0]["INVOICE_NO"].ToString())
                    dval1.Value = "Invoice Date.:";
                else
                    dval1.Value = "A Notice Date.: ";

                A_NOTICE_DT_LBL.CurrentValues.Add(dval1);
                pms.Add(A_NOTICE_DT_LBL);
                //============================================================================


                //============================================================================
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;

                try
                {
                    int scale = Convert.ToInt16(4);
                    qrCodeEncoder.QRCodeScale = scale;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid size!", "Barcode-Printing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    int version = Convert.ToInt16(5);
                    qrCodeEncoder.QRCodeVersion = version;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid version !", "Barcode-Printing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;

                String data = dt.Rows[0]["PALLETE_NO_BARCODE"].ToString();
                Bitmap image = new Bitmap(qrCodeEncoder.Encode(data));

                image.Save("C:\\Windows\\Temp\\PicQRCode.png",System.Drawing.Imaging.ImageFormat.Png);


                //============================================================================
                crystalReportViewer1.ParameterFieldInfo = pms;

                crystalReportViewer1.ReportSource = rd;
            }
            catch(Exception ex)
            {
                ClsMessage.ShowError("Error occurred during Crystal repot printing. Please contact you application administrator.");
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
                throw new Exception("Error occurred during Mix Pallet Printing");
            }
        }
    }
}
