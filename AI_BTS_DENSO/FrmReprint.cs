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

namespace AI_BTS_DENSO
{
    public partial class frmReprint : Form
    {
        GRN_DATA grn_data = new GRN_DATA();
        GRN_DTL grn_dtl = new GRN_DTL();
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
        public frmReprint()
        {
            InitializeComponent();
        }


        private void frmReprint_Load(object sender, EventArgs e)
        {
            common.GetUserAccessForCurrentScreen(this, "Reprint");
            common.AddHeaderCheckBoxToDataGrid(ref dgvPartList,45,8);
            txtANoticeNo.Focus();
        }

        private void frmReprint_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        
        /// <summary>
        /// User clicks the data grid view to modify or delete the reords
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (common.ReplaceNullString(dgvData.CurrentRow.Cells["PALLETE_NO"].Value)=="")
                {
                    if (optIndividualPrint.Checked)
                        SelectCurrentRawData();
                    else
                        ClsMessage.ShowInfo("You can print only Individual label for this part.");
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }


        private void SelectCurrentRawData()
        {
            try
            {
                //Checks if currently selected row is not the DataGridView header, but an actual data row.
                if (dgvData.CurrentRow.Index != -1)
                {
                    if (common.ReplaceNullString(dgvData.CurrentRow.Cells["Is_Block"].Value) != "1")
                    {
                        LoadLabelList(dgvData.CurrentRow);
                    }
                    else
                        ClsMessage.ShowError("Current Part is block hence, its label cannot be printed");
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //if (common.ConfirmToExit())
                this.Close();
        }
        
        #region Hide Part List
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

        private void panel1_Click(object sender, EventArgs e)
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


        private void LoadLabelList(DataGridViewRow pCurrRow)
        {
            if (optAll.Checked)
                pnlPartList.Visible = true;
            lstrANoticeNo = grn_data.Grn_Mst.A_NOTICE_NO;
            lstrInvoiceNo = grn_data.Grn_Mst.INVOICE_NO;
            lstrPartNo = common.ReplaceNullString(pCurrRow.Cells["Part_No"].Value);
            lstrPartName = common.ReplaceNullString(pCurrRow.Cells["Part_Name"].Value);
            lstrPalletNo = common.ReplaceNullString(pCurrRow.Cells["Pallete_No"].Value);

            lintQuantity = common.ReplaceNullNumber(pCurrRow.Cells["Quantity"].Value);
            lintPackSize = common.ReplaceNullNumber(pCurrRow.Cells["Pack_Size"].Value);

            lstrCurrPartNo = ""; //Reset Currpart No. as it should be set only on selection from part list
            pnlPartList.Visible = true;
            using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
            {
                dgvPartList.AutoGenerateColumns = false;
                long lstrGRN_DTL_ID = Convert.ToInt64(common.ReplaceNullString(pCurrRow.Cells["GRN_DTL_ID"].Value));
                List<GRN_LABEL_PRINTING> lstGrn_LBL_DTL = db.GRN_LABEL_PRINTING.Where(x => x.GRN_DTL_ID == lstrGRN_DTL_ID && x.STATUS > 0).ToList();

                if (lstGrn_LBL_DTL.Count != 0)
                    dgvPartList.DataSource = lstGrn_LBL_DTL;
                else
                    ClsMessage.ShowError("No label has been printed for this part yet, hence no label can be reprint. Please print the label using GRN module first.");

                //if this function is called due to selection of Print All option then select 
                //all boxes of part list grid view by default
                foreach (DataGridViewRow currRow in dgvPartList.Rows)
                {
                    //if (optAll.Checked)
                    //{
                        //select only those rows whose labels are remaining for printing and which are not blocked
                        if (currRow.DefaultCellStyle.BackColor != Color.Yellow && currRow.DefaultCellStyle.BackColor != Color.Orange)
                        {
                            currRow.Cells[dgvPartList.CurrentCell.OwningColumn.Name].Value = true;
                        }
                    //}
                }
                CheckBox chkHeader = (CheckBox)dgvPartList.Controls["chkHeader"];
                chkHeader.Checked = true;
            }
            if (optAll.Checked)
                pnlPartList.Visible = false;
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

        
        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                //check if individual label printed for current mixed pallet. then only allow the user to print individual labels
                if (common.ReplaceNullString(dgvData.CurrentRow.Cells["PALLETE_NO"].Value) != "")
                    isIndividualLabelPrinted = IsIndividualLabelPrinted(dgvData.CurrentRow);

                if (common.ReplaceNullString(dgvData.CurrentRow.Cells["PALLETE_NO"].Value) == "" || isIndividualLabelPrinted)
                {
                    if (optIndividualPrint.Checked)
                        SelectCurrentRawData();
                    else
                    {
                        if(optAll.Checked)
                            ClsMessage.ShowInfo("Label for all boxes will be printed for this part (Individual Label). If you want to print partial labels then select 'Individual' option.");
                        else
                            ClsMessage.ShowInfo("You can print only Individual label for this part.");
                    }
                }
                else
                {
                    dgvPartList.DataSource = null;
                    pnlPartList.Visible = false;
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        private bool IsIndividualLabelPrinted(DataGridViewRow pCurrRow)
        {
            bool rtnPrinted = false;
            try
            {
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    long lstrGrnDtlID = Convert.ToInt64(pCurrRow.Cells["GRN_DTL_ID"].Value.ToString());
                    GRN_LABEL_PRINTING grnlbl = db.GRN_LABEL_PRINTING.Where(x => x.GRN_DTL_ID == lstrGrnDtlID && x.STATUS == 1 && x.PRINT_TYPE == "Individual").FirstOrDefault();
                    if (grnlbl != null)
                        rtnPrinted = true;
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return rtnPrinted;
        }


        #region Set Focus
        
        private void txtANoticeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                btnPrint.Enabled = false;
                if (Convert.ToInt16(e.KeyChar) == 13)
                {
                    grn_data = common.GetGRNData_ByNoticeNo(txtANoticeNo.Text.Trim(), "Parts");
                    if (grn_data != null)
                    {
                        LoadGRNDataInForm(grn_data);
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
        #endregion


        /// <summary>
        /// Loads the GRN_Data into Form controls and grid view after fetching from database
        /// </summary>
        /// <param name="pgrn_data">Data object containing the GRN Data</param>
        private void LoadGRNDataInForm(GRN_DATA pgrn_data)
        {
            try
            {
                if (pgrn_data.GRN_SITE != "" && pgrn_data.GRN_SITE != null)
                {
                    dgvData.AutoGenerateColumns = false;
                    dgvData.Rows.Clear();
                    //dgvData.DataSource = pgrn_data.lstGrn_Dtl;
                    foreach(GRN_DTL currPart in pgrn_data.lstGrn_Dtl)
                    {
                        using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                        {
                            GRN_LABEL_PRINTING grn_lbl = db.GRN_LABEL_PRINTING.Where(x => x.GRN_DTL_ID== currPart.GRN_DTL_ID && x.STATUS == 1).FirstOrDefault();
                            if(grn_lbl!=null)
                            {
                                dgvData.Rows.Add(currPart.GRN_DTL_ID, currPart.STATUS, currPart.IS_BLOCK, currPart.PART_NO,
                                    currPart.PART_NAME, currPart.PALLETE_NO, currPart.PACK_SIZE, currPart.SUPPLIER_BATCH_NO, currPart.QUANTITY, grn_lbl.PRINT_TYPE);
                            }
                        }
                    }

                    if (dgvData.Rows.Count == 0)
                    {
                        ClsMessage.ShowInfo("No Label has been printed yet for any part of this Invoice/A Notice No. Kindly print the label using GRN form first.");
                        return;
                    }
                    common.MarkBlockedPart(ref dgvData, true);

                    btnPrint.Enabled = true;

                }
                else
                {
                    btnPrint.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }

        private void dgvPartList_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvPartList_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void dgvPartList_KeyUp(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress=true;
            //if(e.KeyData==Keys.Enter)
            //    SelectCurrentPart();
        }

        private void dgvPartList_Scroll(object sender, ScrollEventArgs e)
        {
            int oldval = e.OldValue;
            int diff = e.NewValue - e.OldValue;
            CheckBox chkHeader = (CheckBox)dgvPartList.Controls["chkHeader"];
            Point newLoc = new Point((chkHeader.Location.X - diff), chkHeader.Location.Y);
            chkHeader.Location = newLoc;
            if (newLoc.X < 40)
                chkHeader.Visible = false;
            else
                chkHeader.Visible = true;
        }

        private void dgvPartList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (dgvPartList.CurrentRow.Cells["LBL_PRN_CFM"].Value != null)
                    {
                        if (dgvPartList.CurrentCell.OwningColumn.Name.ToUpper() == "CHKPRINT")
                        {
                            long lngLBL_PRN_CFM = Convert.ToInt64(dgvPartList.CurrentRow.Cells["LBL_PRN_CFM"].Value.ToString());
                            string lstrCurrPrintType = "Individual";
                            if (optMixedPalletPrint.Checked)
                                lstrCurrPrintType = "Mix Pallet";

                            if (dgvPartList.CurrentRow.Cells["Print_Type"].Value.ToString().ToUpper() != lstrCurrPrintType.ToUpper())
                            {
                                ClsMessage.ShowError("You cannot print the " + lstrCurrPrintType + " label for selected item as "
                                    + common.ReplaceNullString(dgvPartList.CurrentRow.Cells["Print_Type"].Value) + " label has already printed for it.");

                                 dgvPartList.CurrentCell.Value = Boolean.Parse(dgvPartList.CurrentCell.Value.ToString());
                            }
                            else
                                dgvPartList.CurrentCell.Value = !common.ReplaceNullBoolean(dgvPartList.CurrentCell.Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }

        private void dgvPartList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            
        }

        private void optIndividualPrint_CheckedChanged(object sender, EventArgs e)
        {
            UncheckAllLabels();
        }

        private void optMixedPalletPrint_CheckedChanged(object sender, EventArgs e)
        {
            UncheckAllLabels();
        }

        private void UncheckAllLabels()
        {
            try
            {
                foreach (DataGridViewRow currRow in dgvPartList.Rows)
                    currRow.Cells[dgvPartList.CurrentCell.OwningColumn.Name].Value = false;
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClsMessage.ShowConfimation("Are you sure to print the labels for selected part") == DialogResult.Yes)
                {
                    if (optIndividualPrint.Checked)
                    {
                        //Print label only for Individual part
                        if (common.ReplaceNullString(dgvData.CurrentRow.Cells["PALLETE_NO"].Value) == "" || IsIndividualLabelPrinted(dgvData.CurrentRow))
                        {
                            LoadLabelList(dgvData.CurrentRow);
                            PrintIndividualPart(dgvData.CurrentRow);
                        }
                        else
                            ClsMessage.ShowInfo("This is not an individual Part. You cannot print individual label for it.");
                    }
                    else if (optMixedPalletPrint.Checked)
                    {
                        //print label only for selected Pallet
                        if (common.ReplaceNullString(dgvData.CurrentRow.Cells["PALLETE_NO"].Value) != "" && !IsIndividualLabelPrinted(dgvData.CurrentRow))
                            PrintMixPallet(dgvData.CurrentRow);
                        else
                        {
                            if (common.ReplaceNullString(dgvData.CurrentRow.Cells["PALLETE_NO"].Value) == "")
                                ClsMessage.ShowError("This is an individual part. Cannot print mix pallet label for it.");
                            else
                                ClsMessage.ShowError("Individual labels have already printed for this part, Hence cannot print mix pallet label for it.");
                        }
                    }
                    else if (optAll.Checked)
                    {
                        ResetReprintColumn();

                        //Print label for all parts/mixed pallet in one go
                        foreach (DataGridViewRow currRow in dgvData.Rows)
                        {
                            //print label only for those part which are not blocked
                            if (common.ReplaceNullString(currRow.Cells["Is_Block"].Value) != "1")
                            {
                                if (common.ReplaceNullString(currRow.Cells["Is_Reprinted"].Value) == "")
                                {
                                    if (common.ReplaceNullString(currRow.Cells["PALLETE_NO"].Value) == "" || IsIndividualLabelPrinted(currRow))
                                    {
                                        LoadLabelList(currRow);
                                        PrintIndividualPart(currRow);
                                    }
                                    else
                                        PrintMixPallet(currRow);
                                }
                                currRow.Cells["Is_Reprinted"].Value = "1";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred during printing. Please contact application administrator");
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }


        // Reset the reprint status of mix pallet, so that If user wants to reprint the mix pallet once again without unloading the form
        //he can print it. Beecause with Is-Reprint status =1 mic pallet will not be printed. This status is set to one so that, Crystal report 
        //wont display multiple time for same mix pallet as those parts will also be listed in grid view
        /// <summary>
        /// Reset the reprint status of mix pallet
        /// </summary>
        private void ResetReprintColumn()
        {
            try
            {
                foreach (DataGridViewRow currRow in dgvData.Rows)
                {
                    currRow.Cells["Is_Reprinted"].Value = "";
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }

        private void PrintIndividualPart(DataGridViewRow pCurrRow)
        {
            try
            {
                if (common.IsAnyRowSelected(dgvPartList, "chkPrint"))
                {
                    int lintNoOfBox = 1;
                    lintNoOfBox = lintQuantity / lintPackSize;
                    int lintOpenBox = lintQuantity % lintPackSize;
                    if (lintOpenBox > 0)
                        lintNoOfBox += 1;

                    foreach (DataGridViewRow currLabel in dgvPartList.Rows)
                    {
                        if (common.ReplaceNullBoolean(currLabel.Cells["CHKPRINT"].Value) == true)
                        {
                            //Printing individual Barcode
                            string lstrBoxQty = common.ReplaceNullString(currLabel.Cells["Box_Quantity"].Value);
                            string lstrTodaySerialNumber = common.ReplaceNullString(currLabel.Cells["TODAY_BARCODE_SERIAL"].Value);
                            string lstrBRSerial = common.ReplaceNullString(currLabel.Cells["BR_SERIAL"].Value);

                            common.PrinGrnBarCode(
                                    lstrPartNo, lstrPartName, lstrInvoiceNo, lstrANoticeNo,
                                    lstrBoxQty, lstrTodaySerialNumber, (lstrBRSerial + "/" + lintNoOfBox).ToString(),
                                    clsCurrentUser.Site_Name.ToString()
                                );
                        }
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

        private void PrintMixPallet(DataGridViewRow pCurrRow)
        {
            try
            {
                if (common.ReplaceNullString(dgvData.CurrentRow.Cells["Is_Block"].Value) != "1")
                {
                    lstrANoticeNo = grn_data.Grn_Mst.A_NOTICE_NO; ;
                    lstrPalletNo = common.ReplaceNullString(pCurrRow.Cells["Pallete_No"].Value);
                    common.PrintPallet(lstrANoticeNo, lstrPalletNo);

                    //Change Reprint status of all parts of current pallet so that we wont print mix pallet 
                    //print again for current part's pallet
                    foreach (DataGridViewRow currRow in dgvData.Rows)
                    {
                        if(common.ReplaceNullString( pCurrRow.Cells["PALLETE_NO"].Value) == common.ReplaceNullString(currRow.Cells["PALLETE_NO"].Value))
                        {
                            currRow.Cells["Is_Reprinted"].Value = "1";
                        }
                    }
                }
                else
                    ClsMessage.ShowError("Current Part is block hence, its label cannot be printed");
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred during Individual Label print. for Part No. " + lstrPalletNo + " of A Notice No./Invoice No." + lstrANoticeNo + " Error: " + ex.Message);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
