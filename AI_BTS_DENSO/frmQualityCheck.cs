using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AI_BTS_DENSO.Model;
using AI_BTS_DENSO.Common;
using System.Data.SqlClient;

namespace AI_BTS_DENSO
{
    public partial class frmQualityCheck : Form
    {
        int lintCurrentRowIndex = -1;

        GRN_DATA grn_mst = null;
        GRN_DTL grn_dtl = null;
        List<GRN_LABEL_PRINTING> grn_lbl;

        QC_MST qc_mst = null;
        QC_DTL qc_dtl = null;
        QC_LABEL_PRINTING qc_lbl;


        INVENTORY_MST inv_mst = null;
        INVENTORY_DTL inv_dtl = null;

        DataTable dtGRNData = null;
        DataTable dtTblQC_data = null;

        Common.clsCommon common = new Common.clsCommon();
        public frmQualityCheck()
        {
            InitializeComponent();
        }


        #region Form Level Control
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //if (common.ConfirmToExit())
                this.Close();
        }
        

        private void cmbQCType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbQCType.Text.ToUpper() == "BATCH WISE")
            {
                //Hide checkbox column

            }
            else if (cmbQCType.Text.ToUpper() == "ITEM WISE")
            { 
                //display checkbox column
            }
        }
        #endregion

        #region Set Focus
        private void frmQualityCheck_Load(object sender, EventArgs e)
        {
            common.GetUserAccessForCurrentScreen(this, "Quality Check");
            cmbQCType.Text = "Batch Wise";
            txtANoticeNo.Focus();
            txtANoticeNo.Focus();
        }

        private void cmbQCType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 13)
                txtANoticeNo.Focus();
        }
        #endregion
        

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveQCData();
        }

        private bool IsFormValid()
        {
            bool IsFormValid = false;
            try
            {
                if (lblANoticeDate.Text == "" || string.IsNullOrEmpty(lblANoticeDate.Text))
                {
                    ClsMessage.ShowError("Please select a valid part first");
                    txtANoticeNo.Focus();
                }
                else if (dgvData.RowCount == 1)
                {
                    ClsMessage.ShowError("There is no record to save. Please select at least one record to save.");
                    txtANoticeNo.Focus();
                }
                else
                   IsFormValid = true;
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return IsFormValid;
        }


        private void SelectDeselectAllPart(bool pblnSelect)
        {
            try
            {
                foreach(DataGridViewRow currRow in dgvData.Rows)
                {
                    if (currRow.Cells["Part_Name"].Value != null)
                        currRow.Cells["chkSelect"].Value = pblnSelect;
                }
            }
            catch(Exception ex)
            {

            }
        }

        


        private bool CheckIfGRNLBLPrintedForSelectedPart()
        {
            bool isPrinted = true;
            foreach (DataGridViewRow currRow in dgvData.Rows)
            {
                if (currRow.Cells["Part_Name"].Value != null)
                {
                    if (Boolean.Parse(currRow.Cells["chkSelect"].Value.ToString()))
                    {
                        if (currRow.Cells["Status"].Value.ToString() == "0")
                        {
                            isPrinted = false;
                            break;
                        }
                    }
                }
            }

            return isPrinted;
        }

        private bool CheckIfBlockSelectedPart()
        {
            bool isBlocked = false;
            foreach (DataGridViewRow currRow in dgvData.Rows)
            {
                if (currRow.Cells["Part_Name"].Value != null)
                {
                    if (Boolean.Parse(currRow.Cells["chkSelect"].Value.ToString()))
                    {
                        if (currRow.Cells["Is_Block"].Value.ToString() == "1")
                        {
                            isBlocked = true;
                            break;
                        }
                    }
                }
            }

            return isBlocked;
        }


        private bool CheckIfSelectedPartUnderQC()
        {
            bool isUQC = false;
            foreach (DataGridViewRow currRow in dgvData.Rows)
            {
                if (currRow.Cells["Part_Name"].Value != null)
                {
                    if (Boolean.Parse(currRow.Cells["chkSelect"].Value.ToString()))
                    {
                        if (Convert.ToInt32(currRow.Cells["Quantity"].Value.ToString()) != Convert.ToInt32(currRow.Cells["Quantity_Remaining"].Value.ToString()))
                        {
                            isUQC = true;
                            break;
                        }
                    }
                }
            }
            return isUQC;
        }

        private bool ValidateForm()
        {
            bool isValid = false;
            try
            {
                if (txtANoticeNo.Text.Trim() == null || txtANoticeNo.Text.Trim() == "")
                {
                    ClsMessage.ShowError("Please Select the valid data for QC.");
                    txtANoticeNo.Focus();
                }
                else if (dgvData.Rows.Count <= 0)
                    ClsMessage.ShowError("Please select the valid data for QC first.");
                else if (optNone.Checked)
                    ClsMessage.ShowError("Please Select at least one part for QC.");
                else if (!CheckIfGRNLBLPrintedForSelectedPart())
                    ClsMessage.ShowError("GRN Labels are not printed for all of the selected parts. Hence QC Cannot be done for those parts.");
                else if (CheckIfSelectedPartUnderQC())
                    ClsMessage.ShowError(cmbQCType.Text + " QC cannot be done as any of the selected part is already under QC or QC Have been done.");
                else if (CheckIfBlockSelectedPart())
                    ClsMessage.ShowError("Any of the selected part is blocked, hence QC cannot be done");
                else
                    isValid = true;
            }
            catch (Exception ex)
            {

            }
            return isValid;
        }

        private void SaveQCData()
        {
            try
            {
                qc_mst = new QC_MST();
                qc_dtl = new QC_DTL();

                bool blnFormValid = false;
                DateTime qc_on = DateTime.Now; //taking value in variable so that it must be same for all record of this transaction 

                if(ValidateForm())
                {
                    int lintApproveQty = 0;

                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        using (var DbTransacation = db.Database.BeginTransaction())
                        {
                            try
                            {
                                foreach (DataGridViewRow currRow in dgvData.Rows)
                                {
                                    if (currRow.Cells["GRN_DTL_ID"].Value != null)
                                    {
                                        if (Boolean.Parse(currRow.Cells["chkSelect"].Value.ToString()))
                                        {
                                            if (currRow.Cells["IS_Block"].Value.ToString() == "0")
                                            {
                                                int lintGrnDtlID = Convert.ToInt32(currRow.Cells["GRN_DTL_ID"].Value.ToString());
                                                int lintTotalQuantity = Convert.ToInt32(currRow.Cells["Quantity"].Value.ToString());

                                                qc_mst = db.QC_MST.Where(x => x.GRN_DTL_ID == lintGrnDtlID).ToList().FirstOrDefault();
                                                if (qc_mst == null)
                                                    qc_mst = new QC_MST();

                                                #region SAVE DATA IN QC MST TABLE
                                                qc_mst.QUANTITY_APPROVED = lintTotalQuantity;
                                                lintApproveQty = common.ReplaceNullNumber(qc_mst.QUANTITY_APPROVED);


                                                //add the data as a new row
                                                //it means no row is added in db for this part no. and A Notice No., new row needs to be added.
                                                if (qc_mst.GRN_DTL_ID == null)
                                                {
                                                    qc_mst.GRN_DTL_ID = lintGrnDtlID;
                                                    qc_mst.PART_NO = currRow.Cells["Part_No"].Value.ToString();
                                                    qc_mst.QUANTITY_TOTAL = lintTotalQuantity;
                                                    qc_mst.A_NOTICE_NO = txtANoticeNo.Text;
                                                    qc_mst.SUPPLIER_BATCH_NO = currRow.Cells["Supplier_Batch_No"].Value.ToString();
                                                    qc_mst.QC_BY = Convert.ToInt32(clsCurrentUser.User_MST_ID);
                                                    qc_mst.QC_ON = qc_on;
                                                    qc_mst.STATUS = 2;
                                                    
                                                    db.QC_MST.Add(qc_mst);
                                                }
                                                db.SaveChanges();
                                                //Get QC_MST row id for further transactons
                                                qc_mst = db.QC_MST.Where(x => x.GRN_DTL_ID == lintGrnDtlID && x.PART_NO == qc_mst.PART_NO).ToList().FirstOrDefault();
                                                #endregion

                                                #region SAVE APPROVED QTY DATA IN INVENTORY MST TABLE
                                                //==========================================================================================================
                                                //ALL APROVED QUANTITY FOR THIS PART MUST BE ADDED IN INVENTORY MASTER AS WELL
                                                //==========================================================================================================
                                                //FIRST CHECK IF CURRENT MST ID EXISTS IN INVENTORY MST OR NOT. VERY FIRST TIME IT WONT BE EXISTING BUT WHEN 
                                                //WE WILL DO WC FOR THAT PART AGAIN AT PART LEVEL/BATCH LEVEL (IF IT WAS DONE AT LOWER LEVEL EARLIER) THEN
                                                //QTY WILL BE UPDATED otherwise new row will be added for new MST ID
                                                if (qc_mst.QUANTITY_APPROVED > 0)
                                                {
                                                    inv_mst = db.INVENTORY_MST.Where(x => x.QC_MST_ID == qc_mst.QC_MST_ID).ToList().FirstOrDefault();
                                                    //if (inv_mst != null)
                                                    //{
                                                    //    //It means data exists already for this QC MST id in inventory mst table and we need to update thw quantity.
                                                    //    inv_mst.QUANTITY = qc_mst.QUANTITY_APPROVED;
                                                    //    inv_mst.QUANTITY_REMAINING = inv_mst.QUANTITY - inv_mst.QUANTITY_ISSUED;
                                                    //}
                                                    //else
                                                    //{
                                                    //means no record exists for current QC_MST in inventory mst table  and we need to add it
                                                    inv_mst = new INVENTORY_MST
                                                    {
                                                        QC_MST_ID = qc_mst.QC_MST_ID,
                                                        PART_NO = qc_mst.PART_NO,
                                                        QUANTITY = qc_mst.QUANTITY_APPROVED,
                                                        QUANTITY_ISSUED = 0,
                                                        QUANTITY_REMAINING = qc_mst.QUANTITY_APPROVED,
                                                        STATUS = 0,
                                                        CREATED_BY = Convert.ToInt32(clsCurrentUser.User_MST_ID),
                                                        CREATED_ON = qc_on,
                                                        SITE_MST_ID = clsCurrentUser.Site_MST_ID
                                                    };
                                                    db.INVENTORY_MST.Add(inv_mst);
                                                    //}
                                                    db.SaveChanges();

                                                    //Get INV MST detail and its record id for dTL Table transacton
                                                    inv_mst = db.INVENTORY_MST.Where(x => x.QC_MST_ID == inv_mst.QC_MST_ID && x.PART_NO == inv_mst.PART_NO).ToList().FirstOrDefault();
                                                }
                                                //==========================================================================================================
                                                #endregion

                                                #region SAVE DATA IN QC DTL TABLE
                                                //==========================================================================================================
                                                //we need to calculate how many boxes were approved (as in case of partial user me select quantity in such a manner
                                                //that box will be broken
                                                int lintTotalBoxApproved = 0;

                                                grn_lbl = new List<GRN_LABEL_PRINTING>();

                                                //check if this qc is for individual or above level. if individual level then barcode will be given. else we will need to process
                                                //all remianing boxes for this part
                                                string lstrCurrBox = "";
                                                grn_lbl = db.GRN_LABEL_PRINTING.Where(x => x.GRN_DTL_ID == lintGrnDtlID && x.STATUS == 1).OrderBy(m => m.LBL_PRN_CFM).ToList();

                                                if (grn_lbl.Count > 0)
                                                {
                                                    //now check if qty aproved is for all closed boxes or open box also there.
                                                    lintTotalBoxApproved = lintApproveQty / Convert.ToInt16(currRow.Cells["Pack_Size"].Value.ToString());

                                                    //process closed approved boxes
                                                    for (int lintCurrBox = 0; lintCurrBox < lintTotalBoxApproved; lintCurrBox++)
                                                    {
                                                        //here we do not need to check that if this barcode already exists in in qc_dtl tble or not
                                                        //because at any time at least a box quantity will be processed
                                                        qc_dtl = new QC_DTL()
                                                        {
                                                            QC_MST_ID = qc_mst.QC_MST_ID,
                                                            PRIMARY_BARCODE = grn_lbl[lintCurrBox].PRIMARY_BARCODE,
                                                            QUANTITY = Convert.ToInt32(currRow.Cells["Pack_Size"].Value.ToString()),
                                                            QUANTITY_APPROVED = Convert.ToInt32(currRow.Cells["Pack_Size"].Value.ToString()),
                                                            QUANTITY_REJECTED = 0, //since this is full box hence qty approved will be as per pack size
                                                            QUANTITY_SCRAPPED = 0,
                                                            STATUS = 2,
                                                        };

                                                        db.QC_DTL.Add(qc_dtl);
                                                        db.SaveChanges();
                                                        //update status of current in GRN Table to QC Done
                                                        grn_lbl[lintCurrBox].STATUS = 3;

                                                        #region UPDATE INVENTORY DTL TABLE FOR APPROVED QTY
                                                        //=======================================================================================
                                                        //in Inventory DTL table data will always be added. becuase this table contains box level data 
                                                        //in case of coil inventory has to go at individual level because barcode generated fo each coil

                                                        string lstrCurrBarcode = grn_lbl[lintCurrBox].PRIMARY_BARCODE;
                                                        inv_dtl = common.Get_Inventory_DTL(inv_mst, lstrCurrBarcode, Convert.ToInt32(qc_dtl.QUANTITY));
                                                        db.INVENTORY_DTL.Add(inv_dtl);
                                                        //=======================================================================================
                                                        #endregion
                                                    }//end of for loop for all aproved boxes
                                                }
                                                #endregion


                                                #region Update Open Box Quantity
                                                //=======================================================================================
                                                int lintOpenApproved = lintApproveQty % Convert.ToInt16(currRow.Cells["Pack_Size"].Value.ToString());
                                                if (lintOpenApproved > 0)
                                                {
                                                    //process open approved boxes 

                                                    //here we do not need to check that if this barcode already exists in in qc_dtl tble or not
                                                    //because at any time at least a box quantity will be processed
                                                    qc_dtl = new QC_DTL()
                                                    {
                                                        QC_MST_ID = qc_mst.QC_MST_ID,
                                                        PRIMARY_BARCODE = grn_lbl[lintTotalBoxApproved].PRIMARY_BARCODE,
                                                        QUANTITY = Convert.ToInt32(grn_lbl[lintTotalBoxApproved].BOX_QUANTITY),
                                                        QUANTITY_APPROVED = Convert.ToInt32(grn_lbl[lintTotalBoxApproved].BOX_QUANTITY),
                                                        QUANTITY_REJECTED = 0, //since this is full box hence qty approved will be as per pack size
                                                        QUANTITY_SCRAPPED = 0,
                                                        STATUS = 2,
                                                    };

                                                    db.QC_DTL.Add(qc_dtl);
                                                    db.SaveChanges();
                                                    //update status of current in GRN Table to QC Done
                                                    grn_lbl[lintTotalBoxApproved].STATUS = 3;

                                                    string lstrCurrBarcode = grn_lbl[lintTotalBoxApproved].PRIMARY_BARCODE;
                                                    inv_dtl = common.Get_Inventory_DTL(inv_mst, lstrCurrBarcode, Convert.ToInt32(qc_dtl.QUANTITY));
                                                    db.INVENTORY_DTL.Add(inv_dtl);
                                                    //=======================================================================================
                                                    #endregion


                                                    #region UPDATE QC DONE STATUS IN QC_MST TABLE
                                                    //IF ALL QUANTITIES HAS BEEN PROCESSED THEN CHANGE STATUS TO QC DONE FOR THAT PART
                                                    if (qc_mst.QUANTITY_TOTAL == (qc_mst.QUANTITY_APPROVED + qc_mst.QUANTITY_REJECTED + qc_mst.QUANTITY_SCRAPPED))
                                                        qc_mst.STATUS = 2;

                                                    db.SaveChanges();
                                                    #endregion
                                                }
                                            }
                                        }
                                    }//end of if condition to check if current row in grid view is not blank
                                }//end of datagrid view for each loop
                                db.SaveChanges();
                                DbTransacation.Commit();
                            }//end of using dbtransaction's try block
                            catch (Exception ex)
                            {
                                DbTransacation.Rollback();
                                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
                                throw new Exception(ex.Message);
                            }
                        }//end of using transaction block
                    }//end of using db entities block
                    ClsMessage.ShowDataSavedConfirmation();
                    txtANoticeNo_KeyPress(null, new KeyPressEventArgs((char)13));
                    btnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ClsMessage.ShowError(ClsMessage.DATA_NOT_SAVE_ERROR_MESSAGE);
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        /// <summary>
        /// Generates the bar code for current Box for QC Operation
        /// </summary>
        /// <param name="pstrQCDtlBarCode">Barcode which got generated in during GRN in GRN Dtl Table</param>
        /// <param name="pintBarcodeQty">Quantity to be printed on barcode</param>
        /// <param name="pstrPartNo">Part to be printed on barcode</param>
        /// <param name="pintSerial">Current Serial No. of barcode (in Number format)</param>
        /// <returns></returns>
        private QC_LABEL_PRINTING GetCurrentQCLabelBarcode(AI_BTS_DENSOEntities1 pdb, string pstrGrnDtlBarCode, int pintBarcodeQty,int pintBarcodeTRype,string pstrPartNo,ref long pintSerial, DateTime pQc_On)
        {
            string lstrRtnCurrBarCode = "";
            QC_LABEL_PRINTING qc_lbl = new QC_LABEL_PRINTING();
            try
            {
                string pstrCurrSerial = "";
                using (pdb)
                {
                    
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
                qc_lbl = null;
            }
            return qc_lbl;
        }


        private void GetGRNDataForQC(string pstrBarcode)
        {
            try
            {
                //First of All check if GRN barcode printing has been done for all the boxes of all parts or not. It depends for what type of QC a user wants to do
                //IF GRN Label is printed only for one box then QC can not be done for rest of the boxes of that part and batch QC for that GRN
                //Similarly if GRN Label printing is done only for all boxes of one part then QC can be done only for all boxes of that part ONLY
                //or any individual box for that part but not for Batch.
                //If GRN Label is printed for whole Batch then any QC Can be done at batch level. 
                dtGRNData = common.getTable("exec SP_CHECK_IF_GRN_DONE '" + cmbQCType.Text + "'");
                if (dtGRNData != null && dtGRNData.Rows.Count > 0)
                {
                    ClsMessage.ShowError("Label printing for all of the boxes/parts of this GRN is not done yet hence cannot be done " + cmbQCType.Text + " QC");
                    common.ClearDataGridViewRows(ref dgvData);
                    btnSave.Enabled = false;
                }
                else
                { 
                    dtGRNData = common.getTable("exec SP_GET_GRN_DATA_FOR_QC '" + cmbQCType.Text + "','" + pstrBarcode + "','" + txtANoticeNo.Text.Trim() + "'");
                    if (dtGRNData != null && dtGRNData.Rows.Count>0)
                    {
                        txtANoticeNo.Text = dtGRNData.Rows[0]["A_NOTICE_NO"].ToString();
                        lblANoticeDate.Text = dtGRNData.Rows[0]["A_NOTICE_DATE"].ToString();

                        dgvData.AutoGenerateColumns = false;
                        dgvData.DataSource = dtGRNData;
                        btnSave.Enabled = true;

                    }
                    else
                    { 
                        ClsMessage.ShowInfo("No data found for current barcode or QC have been done for all of it's " + cmbQCType.Text);
                        btnSave.Enabled = false;
                        common.ClearDataGridViewRows(ref dgvData);
                    }
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        private void btnStartQC_Click(object sender, EventArgs e)
        {
            DataTable dtGRNData = null;
            try
            {
                if (dgvData.Rows.Count > 1)
                {
                    if (ISAnyPartRemainingForQC())
                    {
                        if (ClsMessage.ShowConfimation("Are you sure to start the QC for current Part as " + cmbQCType.Text) == DialogResult.Yes)
                        {


                            //On QC Start Status in GRN LBL Printing table would be change from printed to UQC (1 to 2)
                            //if QC for BATCH then all the Parts(compelte GRN) in GRN Label Printing table set status to 2.
                            //if QC for Full/Partial then that particular part (all boxes status has to be changed only.
                            //If QC for INDIVIDUAL then that particular box(barcode)'s status need to be changed.

                            //========================================================================================================================
                            //Update Status in GRN LBL Printing table from 1 to 2
                            //========================================================================================================================
                            common.getTable("Exec SP_UPDATE_GRN_LBL_PRINTING_QC_STATUS '" + cmbQCType.Text + "','" + txtANoticeNo.Text + "',2");
                            //========================================================================================================================



                            ////========================================================================================================================
                            ////update QC_MST & QC Details table status from 0 to 1 (Pending to In Progress)
                            ////========================================================================================================================
                            //string lstrPartNo = "";
                            //if (cmbQCType.Text.ToUpper() != "BATCH")
                            //{
                            //    if (dgvData.Rows.Count > 0)
                            //    {
                            //        if (dgvData.Rows[0].Cells["Part_No"].Value != null)
                            //        {
                            //            lstrPartNo = dgvData.Rows[0].Cells["PART_NO"].Value.ToString();
                            //        }
                            //    }
                            //}

                            //common.getTable("Exec SP_UPDATE_QC_MST_DTL_STATUS '" + cmbQCType.Text + "','" + txtANoticeNo.Text.Trim() + "','" + lstrPartNo + "',''," +
                            //    Convert.ToInt32(common.gstrCurrentUserID) + ",1");
                            ////========================================================================================================================

                            btnSave.Enabled = true;
                        }
                    }
                    else
                    {
                        ClsMessage.ShowInfo("There is no part remaining for QC.");
                        btnSave.Enabled = false;
                    }
                }
                else
                    ClsMessage.ShowError(" There is no record in list to start QC. Please select valid Part to start QC.");
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }



        /// <summary>
        /// Checks if any part or its box is remaining for QC.
        /// </summary>
        /// <returns>Returns true is any part or its box remaining for QC otherwise returns false.</returns>
        private bool ISAnyPartRemainingForQC()
        {
            bool blnPartRemaining = false;
            try
            {
                foreach(DataGridViewRow currRow in dgvData.Rows)
                {
                    if (currRow.Cells["Part_No"].Value != null)
                    {
                        if (Convert.ToInt32(currRow.Cells["Quantity_Remaining"].Value.ToString()) > 0)
                        {
                            blnPartRemaining = true;
                            break;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return blnPartRemaining;
        }
       
        private void txtANoticeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                try
                {
                    if (txtANoticeNo.Text.Trim()!="" && txtANoticeNo.Text.Trim() !=null)
                    {
                        dtTblQC_data = common.getTable("EXEC SP_GET_GRN_DATA_FOR_QC '" + txtANoticeNo.Text.Trim() + "'");
                        if (dtTblQC_data.Rows.Count>0)
                        {
                            //lblANoticeDate.Text = Convert.ToDateTime(dtTblQC_data.Rows[0]["A_NOTICE_DATE"].ToString()).ToString("dd-MMM-yy");
                            dgvData.AutoGenerateColumns = false;
                            lblANoticeDate.Text = DateTime.Parse(dtTblQC_data.Rows[0]["A_Notice_Date"].ToString()).ToString("dd-MMM-yyyy");
                            dgvData.DataSource = dtTblQC_data;
                            SelectDeselectAllPart(optAll.Checked);
                            btnSave.Enabled = true;
                        }
                        else
                        {
                            ClsMessage.ShowError("No Data found available for QC. Please Check if all labels are printed for current A Notice No");
                            common.ClearDataGridViewRows(ref dgvData);
                            btnSave.Enabled = false;
                        }
                    }
                    
                }
                catch(Exception ex)
                {
                    common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
                }
            }
        }

        
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvData.CurrentCell.OwningColumn.Name.ToUpper() == "CHKSELECT")
                {
                    dgvData.CurrentCell.Value = !Convert.ToBoolean(dgvData.CurrentCell.Value);
                    optAll.Checked = common.CheckIfAllPartSelected(ref dgvData, "Part_Name", "chkSelect");
                    optNone.Checked = common.CheckIfNoPartSelected(ref dgvData, "Part_Name", "chkSelect");
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        private void optAll_Click(object sender, EventArgs e)
        {
            SelectDeselectAllPart(true);
            cmbQCType.Text = "Batch Wise";
        }
        private void optNone_Click(object sender, EventArgs e)
        {
            SelectDeselectAllPart(false);
            cmbQCType.Text = "Item Wise";
        }

        private void optAll_CheckedChanged(object sender, EventArgs e)
        {
            if (optAll.Checked)
            {
                SelectDeselectAllPart(true);
                cmbQCType.Text = "Batch Wise";
            }
            else
                cmbQCType.Text = "Item Wise";
        }

        private void optNone_CheckedChanged(object sender, EventArgs e)
        {
            if (optNone.Checked)
            {
                SelectDeselectAllPart(false);
                cmbQCType.Text = "Item Wise";
            }
        }
    }
}