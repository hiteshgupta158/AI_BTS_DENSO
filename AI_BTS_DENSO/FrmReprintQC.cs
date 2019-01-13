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
using AI_BTS_DENSO.Model;
using System.Reflection;
using System.Collections;
using SATOPrinterAPI;
using SATOPrinterInterface;

namespace AI_BTS_DENSO
{
    public partial class frmReprintQC : Form
    {
        SATOPrinterAPI.Printer printer = new SATOPrinterAPI.Printer();

        List<QC_REJECTED_DATA> lstQC_Rejected_Data = new List<QC_REJECTED_DATA>();
        QC_MST qc_mst = new QC_MST();
        QC_DTL qc_dtl = new QC_DTL();
        string lstrCurrPartNo = "";
        bool isIndividualLabelPrinted = false;

        int lintQuantity=1;
        int lintPackSize=1;
        string lstrPartNo="";
        string lstrPartName = "";
        string lstrANoticeNo = "";
        string lstrInvoiceNo = "";
        string lstrPalletNo = "";
        Hashtable htPalletPrinted = new Hashtable();


        //Create instance of common class to use global/common methods
        Common.clsCommon common = new Common.clsCommon();
        public frmReprintQC()
        {
            InitializeComponent();
        }


        private void frmReprint_Load(object sender, EventArgs e)
        {
            common.GetUserAccessForCurrentScreen(this, "Reprint");
            //common.AddHeaderCheckBoxToDataGrid(ref dgvPartList,45,8);
            txtANoticeNo.Focus();
        }

        private void frmReprint_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void txtANoticeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                btnPrint.Enabled = false;
                if (Convert.ToInt16(e.KeyChar) == 13)
                {
                    lstQC_Rejected_Data = common.GetQCRejectedData_ByNoticeNo(txtANoticeNo.Text.Trim());
                    if (lstQC_Rejected_Data != null)
                    {
                        LoadQCRejectedDataInForm(lstQC_Rejected_Data);
                        btnPrint.Enabled = true;
                    }
                    else
                    {
                        common.ClearDataGridViewRows(ref dgvData);
                        btnPrint.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SelectCurrentRawData();
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        ///// <summary>
        ///// User clicks the data grid view to modify or delete the reords
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void dgvData_DoubleClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        SelectCurrentRawData();
        //    }
        //    catch(Exception ex)
        //    {
        //        common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
        //    }
        //}


        private void SelectCurrentRawData()
        {
            try
            {
                //Checks if currently selected row is not the DataGridView header, but an actual data row.
                if (dgvData.CurrentRow.Index != -1)
                {
                    LoadLabelList(dgvData.CurrentRow);
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }
        
       

        #region Clear Contents
        /// <summary>
        /// Clears the data from all controls on the form.
        /// </summary>
        private void Clear()
        {
            txtANoticeNo.Text = "";
            common.ClearDataGridViewRows(ref dgvData);
            txtANoticeNo.Focus();
        }

        #endregion

        #region HIDE PART LIST
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //if (common.ConfirmToExit())
                this.Close();
        }
        
        private void dgvPartList_Leave(object sender, EventArgs e)
        {
            pnlPartList.Visible = false;
        }

        private void pnlMain_Click(object sender, EventArgs e)
        {
            pnlPartList.Visible = false;
        }

        private void frmReprint_Click(object sender, EventArgs e)
        {
            pnlPartList.Visible = false;
        }
        
        private void panel2_Enter(object sender, EventArgs e)
        {
            pnlPartList.Visible = false;
        }
        private void dgvData_Enter(object sender, EventArgs e)
        {
            pnlPartList.Visible = false;
        }
        #endregion


        /// <summary>
        /// Loads the GRN_Data into Form controls and grid view after fetching from database
        /// </summary>
        /// <param name="pgrn_data">Data object containing the GRN Data</param>
        private void LoadQCRejectedDataInForm(List<QC_REJECTED_DATA> pData)
        {
            try
            {
                if (pData.Count > 0)
                {
                    dgvData.AutoGenerateColumns = false;
                    dgvData.Rows.Clear();
                    //dgvData.DataSource = pgrn_data.lstGrn_Dtl;
                    foreach (QC_REJECTED_DATA currPart in pData)
                    {
                        using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                        {
                            //GRN_LABEL_PRINTING grn_lbl = db.GRN_LABEL_PRINTING.Where(x => x.GRN_DTL_ID== currPart.GRN_DTL_ID && x.STATUS == 1).FirstOrDefault();
                            if (currPart.qc_mst != null)
                            {
                                dgvData.Rows.Add(currPart.qc_mst.QC_MST_ID, currPart.qc_mst.PART_NO, currPart.Part_Name, currPart.qc_mst.GRN_DTL.PACK_SIZE,currPart.qc_mst.A_NOTICE_NO);
                            }
                        }
                    }

                    if (dgvData.Rows.Count == 0)
                    {
                        ClsMessage.ShowInfo("No label has been printed yet for any rejected part of this Invoice/A Notice No. Kindly print the label using GRN form first.");
                        return;
                    }
                    //common.MarkBlockedPart(ref dgvData, true);
                    btnPrint.Enabled = true;

                }
                else
                {
                    ClsMessage.ShowInfo("No part got rejected of this Invoice/A Notice No.");
                    btnPrint.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }

        private void LoadLabelList(DataGridViewRow pCurrRow)
        {
            QC_REJECTED_DATA qc_rejected_data = new QC_REJECTED_DATA();
            pnlPartList.Visible = true;
            lstrANoticeNo = common.ReplaceNullString(pCurrRow.Cells["A_NOTICE_NO"].Value);
            //lstrInvoiceNo = grn_data.Grn_Mst.INVOICE_NO;
            lstrPartNo = common.ReplaceNullString(pCurrRow.Cells["Part_No"].Value);
            lstrPartName = common.ReplaceNullString(pCurrRow.Cells["Part_Name"].Value);
            //lintQuantity = common.ReplaceNullNumber(pCurrRow.Cells["Quantity"].Value);
            lintPackSize = common.ReplaceNullNumber(pCurrRow.Cells["Pack_Size"].Value);
            lstrCurrPartNo = common.ReplaceNullString(pCurrRow.Cells["Part_No"].Value); ; //Reset Currpart No. as it should be set only on selection from part list
            pnlPartList.Visible = true;
            using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
            {
                DataGridViewSelectedRowCollection lstSelectedPartList = dgvPartList.SelectedRows;

                dgvPartList.AutoGenerateColumns = false;
                long lstrQC_MST_ID = Convert.ToInt64(common.ReplaceNullString(pCurrRow.Cells["QC_MST_ID"].Value));
                //List<QC_LABEL_PRINTING> lstQC_LBL_DTL = db.GRN_LABEL_PRINTING.Where(x => x.GRN_DTL_ID == lstrGRN_DTL_ID && x.STATUS > 0).ToList();

                QC_MST currPart = db.QC_MST.Where(x => x.QC_MST_ID == lstrQC_MST_ID).FirstOrDefault();

                //Get currently selected part qc rejection data object
                foreach (QC_REJECTED_DATA c_REJECTED_DATA in lstQC_Rejected_Data)
                {
                    if (c_REJECTED_DATA.qc_mst.QC_MST_ID == lstrQC_MST_ID)
                    {
                        qc_rejected_data = c_REJECTED_DATA;
                        break;
                    }
                }


                if (qc_rejected_data.lst_QC_Lbl.Count > 0)
                {
                    dgvPartList.DataSource = qc_rejected_data.lst_QC_Lbl;
                    //No. of total label/label no. and inspector name (QC BY User Name) of current part rejected/approved has to be calculated for current label for prn.
                    foreach(DataGridViewRow currLbl in dgvPartList.Rows )
                    {
                        //Get object of current Label
                        long lintCurrLblID = Convert.ToInt64(common.ReplaceNullNumber(currLbl.Cells["QC_LBL_ID"].Value));
                        QC_LABEL_PRINTING lbl_print = qc_rejected_data.lst_QC_Lbl.Where(x => x.QC_LBL_ID == lintCurrLblID).FirstOrDefault();

                        //Get Insepector Name
                        currLbl.Cells["Inspector_Name"].Value = db.USER_MST.Where(x => x.USER_MST_ID == lbl_print.QC_BY).FirstOrDefault().USER_NAME;

                        currLbl.Cells["Total_Label"].Value = lbl_print.QC_DTL.QC_LABEL_PRINTING.Where(x => x.BARCODE_TYPE == lbl_print.BARCODE_TYPE).ToList().Count;

                        //Current Label serial no. means its ?/total No. of label for approval/rejection
                        currLbl.Cells["BR_Serial"].Value = "";

                    }
                }
                else
                    ClsMessage.ShowError("No label has been printed for this part yet, hence no label can be reprinted. Please print the label using QC (HHT) module first.");

                //if this function is called due to selection of Print All option then select 
                //all boxes of part list grid view by default
                DataGridViewRowCollection lstGridViewRows;
                //in case user selected some rows manually to print and checkALL option is not checked
                if (lstSelectedPartList.Count > 0)
                {
                    for (int currQCLBL = 0; currQCLBL < lstSelectedPartList.Count; currQCLBL++)
                    {
                        //select only those rows whose labels are remaining for printing and which are not blocked
                        //if (lstSelectedPartList[currQCLBL].DefaultCellStyle.BackColor != Color.Yellow && lstSelectedPartList[currQCLBL].DefaultCellStyle.BackColor != Color.Orange)
                        //{
                            //currRow.Cells[dgvPartList.CurrentCell.OwningColumn.Name].Value = true;
                        lstSelectedPartList[currQCLBL].Selected = true;
                        //}
                    }
                }
            }
        }

        private void dgvPartList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectCurrentPart();
        }

        private void SelectCurrentPart()
        {
            try
            {
                if (dgvPartList.CurrentRow.Index != -1)
                {
                    pnlPartList.Visible = false;
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }
        


        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.SelectedRows.Count > 0)
                {
                    if (ClsMessage.ShowConfimation("Are you sure to print the labels for selected part(s)") == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow currRow in dgvData.SelectedRows)
                        {
                            PrintIndividualPart(currRow);
                        }
                    }
                }
                else
                    ClsMessage.ShowError("Select at least one part to print the label.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred during printing. Please contact application administrator");
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }

        private void PrintIndividualPart(DataGridViewRow pCurrRow)
        {
            try
            {
                if (dgvData.SelectedRows.Count > 1)
                {
                    dgvPartList.SelectAll();
                }

                if (dgvPartList.SelectedRows.Count>0)
                {
                    foreach (DataGridViewRow currLabel in dgvPartList.SelectedRows)
                    {
                        //Printing individual Barcode
                        string lstrBoxQty = common.ReplaceNullString(currLabel.Cells["Box_Quantity"].Value);
                        string lstrTodaySerialNumber = common.ReplaceNullString(currLabel.Cells["TODAY_BARCODE_SERIAL"].Value);
                        string lstrBRSerial = common.ReplaceNullString(currLabel.Cells["BR_SERIAL"].Value);
                        string lstrRejectionNo = common.ReplaceNullString(currLabel.Cells["Total_Label"].Value);
                        DateTime ldtQCDateTime = DateTime.Parse(common.ReplaceNullString(currLabel.Cells["QC_ON"].Value));
                        string lstrInspectorName = common.ReplaceNullString(currLabel.Cells["Inspector_Name"].Value);
                        string lstrSerial= common.ReplaceNullString(currLabel.Cells["Today_Barcode_Serial"].Value);

                        if (currLabel.Cells["Barcode_Type"].ToString() == "1")
                            printRejectionLabel(lstrPartNo,lstrPartName,lstrInvoiceNo,lstrANoticeNo,lintQuantity.ToString(), lstrRejectionNo,
                                ldtQCDateTime, lstrInspectorName,"", lstrSerial);
                        else
                            printApproveLabel(lstrPartNo,lstrPartName,lstrInvoiceNo,lstrANoticeNo,lintQuantity.ToString(),"", 
                                ldtQCDateTime,lstrInspectorName,"",lstrTodaySerialNumber);
                    }
                }
                else
                    ClsMessage.ShowError("Please select at least one label to print.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred during Individual Label print. for Part No. " + lstrPalletNo + " of A Notice No./Invoice No." + lstrANoticeNo + " Error: " + ex.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="partNo"></param>
        /// <param name="partName"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="ANoticeNo"></param>
        /// <param name="quantity"></param>
        /// <param name="RejectionNo"></param>
        /// <param name="RejectionDateTime"></param>
        /// <param name="Inspector"></param>
        /// <param name="location"></param>
        /// <param name="serialNo"></param>
        public void printRejectionLabel(string partNo, string partName, string invoiceNo, string ANoticeNo, string quantity, string RejectionNo, DateTime RejectionDateTime, string Inspector, string location, string serialNo)
        {
            string sbpl = "";
            try
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader("Rejection.Prn", Encoding.UTF8))
                {
                    sbpl = reader.ReadToEnd();
                    reader.Close();
                }

                sbpl = sbpl.Replace("{VARPARTNO}", partNo);
                sbpl = sbpl.Replace("{VARPARTNAME}", partName);
                sbpl = sbpl.Replace("{VARINVOICENO}", invoiceNo);
                sbpl = sbpl.Replace("{VARANOTICENO}", ANoticeNo);
                sbpl = sbpl.Replace("{VARQTY}", quantity);
                sbpl = sbpl.Replace("{VARREJNO}", RejectionNo);
                sbpl = sbpl.Replace("{VARDATE}", RejectionDateTime.ToString("dd-MM-yy"));
                sbpl = sbpl.Replace("{VARTIME}", RejectionDateTime.ToString("HH:mm"));
                sbpl = sbpl.Replace("{VARINSPECTOR}", Inspector);
                string barcode = string.Format("{0}|{1}|{2}|{3}|{4}", partNo, ANoticeNo, quantity, location, serialNo);
                sbpl = sbpl.Replace("{VARBARCODELEN}", barcode.Length.ToString());
                sbpl = sbpl.Replace("{VARBARCODE}", barcode);
                sbpl = sbpl.Replace("{VARBARCODEDATA1}", string.Format("{0}", serialNo));

                if (sbpl == "")
                {
                    //  throw new InfoException("Printing data can not be empty.");
                }

                byte[] sbplByte = SATOPrinterAPI.Utils.StringToByteArray(sbpl);
                if (printer.GetPrinterStatus().ToString().ToUpper() == "OK")
                    printer.Send(sbplByte);
                else
                    throw new Exception(printer.GetPrinterStatus().ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Printing Error : " + ex.Message);
            }
        }


        /// <summary>
        /// Print Approval Labels for approved part of current box(In case of Partial jection/approval of current box)
        /// </summary>
        /// <param name="partNo"></param>
        /// <param name="partName"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="ANoticeNo"></param>
        /// <param name="quantity"></param>
        /// <param name="BinInfo"></param>
        /// <param name="ApprovalDateTime"></param>
        /// <param name="Inspector"></param>
        /// <param name="location"></param>
        /// <param name="serialNo"></param>
        public void printApproveLabel(string partNo, string partName, string invoiceNo, string ANoticeNo, string quantity, string BinInfo, DateTime ApprovalDateTime, string Inspector, string location, string serialNo)
        {
            string sbpl = "";
            try
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader("Approval.Prn", Encoding.UTF8))
                {
                    sbpl = reader.ReadToEnd();
                    reader.Close();
                }

                sbpl = sbpl.Replace("{VARPARTNO}", partNo);
                sbpl = sbpl.Replace("{VARPARTNAME}", partName);
                sbpl = sbpl.Replace("{VARINVOICENO}", invoiceNo);
                sbpl = sbpl.Replace("{VARANOTICENO}", ANoticeNo);
                sbpl = sbpl.Replace("{VARQTY}", quantity);
                sbpl = sbpl.Replace("{VARBININFO}", BinInfo);
                sbpl = sbpl.Replace("{VARDATE}", ApprovalDateTime.ToString("dd-MM-yy"));
                sbpl = sbpl.Replace("{VARTIME}", ApprovalDateTime.ToString("HH:mm"));
                sbpl = sbpl.Replace("{VARINSPECTOR}", Inspector);
                string barcode = string.Format("{0}|{1}|{2}|{3}|{4}", partNo, ANoticeNo, quantity, location, serialNo);
                sbpl = sbpl.Replace("{VARBARCODELEN}", barcode.Length.ToString());
                sbpl = sbpl.Replace("{VARBARCODE}", barcode);
                sbpl = sbpl.Replace("{VARBARCODEDATA1}", string.Format("{0}", serialNo));
                if (sbpl == "")
                {
                    //  throw new InfoException("Printing data can not be empty.");
                }
                byte[] sbplByte = SATOPrinterAPI.Utils.StringToByteArray(sbpl);
                if (printer.GetPrinterStatus().ToString().ToUpper() == "OK")
                    printer.Send(sbplByte);
                else
                    throw new Exception(printer.GetPrinterStatus().ToString());
            }
            catch (Exception ex)
            {
                //ClsMessage.ShowError(ex.Message);
                throw new Exception("Printing Error : " + ex.Message);
            }
        }
    }
}
