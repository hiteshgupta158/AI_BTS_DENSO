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

namespace AI_BTS_DENSO
{
    public partial class frmSTO : Form
    {
        //Create instance of MRN_MST table in database to perfrom CRUD operations.
         MRN_MST mrn_mst = new MRN_MST();
        string lstrCurrPartNo = "";
        int lintCurrModifyQuantity;

        //Create instance of common class to use global/common methods
        Common.clsCommon common = new Common.clsCommon();
        public frmSTO()
        {
            InitializeComponent();
        }
        
        private void frmSTO_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        
        //On click of "Add" button form should be clear, to enter details of new records to be added. It is useful when user double clicked on gridview and text boxes
        //contain the data of existing records, but in between user wants to add new record instead of performing Update/Delete opperation.
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearCurrentRecord();
        }

        /// <summary>
        /// User clicks the data grid view to modify or delete the reords
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvData_DoubleClick(object sender, EventArgs e)
        {

        }

        private void SelectCurrentRawData()
        {
            try
            {
                //Checks if currently selected row is not the DataGridView header, but an actual data row.
                if (dgvData.CurrentRow.Index != -1)
                {
                    //set currently selected record's value in form controls
                    txtPart_No.Text = dgvData.CurrentRow.Cells["Part_No"].Value.ToString();
                    txtQty.Text = dgvData.CurrentRow.Cells["Quantity"].Value.ToString();
                    //btnDel.Enabled = true;
                    lintCurrModifyQuantity =Convert.ToInt32(txtQty.Text);
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }
        
        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.Rows.Count > 0)
                {
                    if (dgvData.SelectedRows.Count > 0)
                    {
                        if (ClsMessage.ShowConfirmToDetele() == DialogResult.Yes)
                        {
                            dgvData.Rows.Remove(dgvData.CurrentRow);
                        }
                    }
                    else
                        ClsMessage.ShowInfo("Please select at least one record to delete.");
                }
                else
                    ClsMessage.ShowInfo("There is no row to delete.");
            }
            catch (Exception ex)
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
            ClearCurrentRecord();
            common.ClearDataGridViewRows(ref dgvData);
        }

        private void ClearCurrentRecord()
        {
            txtCanban.Text = "";
            txtPart_No.Text = "";
            txtQty.Text = "";
            //btnDel.Enabled = false;
            mrn_mst.MRN_MST_ID = 0;
            txtCanban.Focus();
            pnlPartList.Visible = false;
            lintCurrModifyQuantity = 0;
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validate the form to make sure that all controls are filled with valid data before submiting for CRUD operation.
        /// </summary>
        /// <returns>Returns True if form is valid else returns False.</returns>
        private bool ValidateForm()
        {
            bool IsFormValid = false;
            try
            {
                IsFormValid = true;
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return IsFormValid;
        }

        private bool IsValidateCurrentRecord()
        {
            bool isRecordValid = false;
            try
            {
                if (lstrCurrPartNo == "")
                {
                    ClsMessage.ShowError("Please enter the valid Part No.");
                    txtPart_No.Focus();
                }
                else if (!common.IsNumeric(txtQty.Text))
                {
                    ClsMessage.ShowError("Please enter the valid quantity first");
                    txtPart_No.Focus();
                }
                else
                    isRecordValid = true;
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return isRecordValid;
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //if (common.ConfirmToExit())
                this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AddCurrentRecord();
        }

        private void btnFormSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.Rows.Count >= 1)
                {
                    //First check if all controls are filled with correct/required data and form is valid to be submitted
                    if (ValidateForm())
                    {
                        string lstrMRNNO = "MRN-1";
                        //Save record values to database
                        using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                        {
                            //Get New STO No.
                            #region Get MRN NO
                            MRN_MST max_mrn_no = db.MRN_MST.OrderByDescending(x => x.MRN_NO).FirstOrDefault();
                            if (max_mrn_no != null)
                            {
                                if (common.ReplaceNullString(max_mrn_no.MRN_NO) != "")
                                    lstrMRNNO = "MRN-" + (Convert.ToInt64(max_mrn_no.MRN_NO.ToString().Substring(lstrMRNNO.IndexOf("MRN-") + 4))+1).ToString();
                            }
                            #endregion
                            foreach (DataGridViewRow currRow in dgvData.Rows)
                            {
                                if (currRow.Cells["Part_No"].Value != null)
                                {
                                    mrn_mst = new MRN_MST
                                    {
                                        MRN_NO = lstrMRNNO,
                                        PART_NO = currRow.Cells["Part_No"].Value.ToString(),
                                        QUANTITY = common.ReplaceNullNumber(currRow.Cells["Quantity"].Value),
                                        KANBAN_TEXT = common.ReplaceNullString(currRow.Cells["Kanban_Text"].Value),
                                        STATUS = 0,
                                        CREATED_BY = Convert.ToInt32(clsCurrentUser.User_MST_ID),
                                        CREATED_ON = DateTime.Today,
                                        SITE_MST_ID =clsCurrentUser.Site_MST_ID
                                    };
                                    db.MRN_MST.Add(mrn_mst);
                                }
                            }//end of foreach loop for each row in gridview
                            //commit changes to database
                            db.SaveChanges();
                            //display a confirmation msg to user about saving the data.
                            lblMRNNo.Text = lstrMRNNO;
                            ClsMessage.ShowDataSavedConfirmation();
                            lblMRNNo.Text = "";
                            btnSave.Enabled = false;
                            dgvData.Rows.Clear();
                            //clear the form content
                            Clear();
                            
                        }//end of using db block
                    }//end of validate form condition
                }//end of if condition to check if there is any record to save
                else
                    ClsMessage.ShowError("There is no record to save. Please enter at least one record to save.");
            }
            catch (Exception ex)
            {
                ClsMessage.ShowError("Error occurred while saving data. Please contact your application administrator.");
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        #region Hide Part List
        private void dgvPartList_Leave(object sender, EventArgs e)
        {
            pnlPartList.Visible = false;
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pnlPartList.Visible = false;
        }

        private void pnlMain_Click(object sender, EventArgs e)
        {
            pnlPartList.Visible = false;
        }

        private void frmSTO_Click(object sender, EventArgs e)
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

        private void txtPart_No_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadPartList();
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        private void LoadPartList()
        {
            try
            {
                lstrCurrPartNo = ""; //Reset Currpart No. as it should be set only on selection from part list
                pnlPartList.Visible = true;
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    dgvPartList.AutoGenerateColumns = false;
                    if (txtPart_No.Text != "")
                        dgvPartList.DataSource = db.MATERIAL_MST.Where(x => x.PART_NO.StartsWith(txtPart_No.Text.Trim())).ToList();
                    else
                        dgvPartList.DataSource = db.MATERIAL_MST.ToList();
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        private void LoadPendingMixPallet()
        {
            try
            {
                dgvMixPallet.AutoGenerateColumns = false;
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    List<MIXED_PALLET_STO> mix_pallet_sto = db.MIXED_PALLET_STO.Where(x => x.STO_NO == null || x.STO_NO== "").ToList();
                    dgvMixPallet.DataSource = mix_pallet_sto;
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }


        private void LoadPendingSTO()
        {
            try
            {
                dgvPendingSTO.AutoGenerateColumns = false;
                DataTable dt = common.getTable("Select distinct STO_No,Format(STO_TIME,'dd-MMM-yyyy') as STO_TIME,Case When IsNull(IS_Picked,0)= 0 " +
                    " then 'No' Else 'Yes' End as IS_PICKED From Mixed_Pallet_STO Where isNull(IS_Dispatched,0)=0 AND IsNull(STO_NO,'') <> '' Order by STO_Time");
                dgvPendingSTO.DataSource = dt;

                //using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                //{
                //    List<MIXED_PALLET_STO> mix_pallet_sto = db.MIXED_PALLET_STO.Where(x => x.STO_NO == null || x.STO_NO == "").ToList();
                //    dgvMixPallet.DataSource = mix_pallet_sto;
                //}
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }


        private void LoadPendingMRN()
        {
            try
            {
                dgvPendingMRN.AutoGenerateColumns = false;
                string strQry = "Select distinct MRN_No,Format(CREATED_ON,'dd-MMM-yyyy') as CREATED_ON," +
                    "Case When IsNull(Is_Picked,0)= 0  then 'No' Else 'Yes' End as IS_MRN_PICKED From " +
                    "MRN_MST Where isNull(Dispatch_By,'')='' AND IsNull(MRN_NO,'') <> '' AND " +
                    "SITE_MST_ID =" + clsCurrentUser.Site_MST_ID + "  Order by CREATED_ON";
                DataTable dt = common.getTable(strQry);
                dgvPendingMRN.DataSource = dt;
            }
            catch (Exception ex)
            {
                ClsMessage.ShowError("Error occurred while fetching Pendding MRN Data.");
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
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
                    txtPart_No.Text = dgvPartList.CurrentRow.Cells["PartNo"].Value.ToString();
                    lstrCurrPartNo = txtPart_No.Text;
                    txtQty.Focus();
                    pnlPartList.Visible = false;
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        private void AddCurrentRecord()
        {
            try
            {
                if (IsValidateCurrentRecord())
                {
                    if (common.CheckIfPartQuantityExistisInStock(dgvData, txtPart_No.Text.Trim(),Convert.ToInt32( txtQty.Text.Trim()),lintCurrModifyQuantity))
                    {
                        //if current mode is edit mode than modify the existing record
                        if (lintCurrModifyQuantity > 0)
                        {
                            dgvData.CurrentRow.Cells["Part_No"].Value = txtPart_No.Text.Trim();
                            dgvData.CurrentRow.Cells["Quantity"].Value = txtQty.Text.Trim();
                        }
                        else
                            dgvData.Rows.Add(txtPart_No.Text.Trim(), txtQty.Text.Trim());

                        ClearCurrentRecord();
                    }
                    else
                        ClsMessage.ShowError("Requested qauntity for this part is not available in stock.");
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        
        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectCurrentRawData();
        }

        #region Set Focus
        private void txtPart_No_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                pnlPartList.Visible = true;
                dgvPartList.Focus();
            }
        }
        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt64(e.KeyChar) == 13)
                btnSave.Focus();
        }
        #endregion
        private void dgvPartList_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void frmSTO_Load(object sender, EventArgs e)
        {
            common.GetUserAccessForCurrentScreen(this,"STO");
            tabControl1.TabPages[0].Focus();
            common.AddHeaderCheckBoxToDataGrid(ref dgvMixPallet, 3, 6);
            LoadPendingMixPallet();
        }

        private void txtPart_No_Click(object sender, EventArgs e)
        {
            LoadPartList();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelMix_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnlGridControl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvMixPallet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex != -1)
                {
                    if (dgvMixPallet.CurrentCell.OwningColumn.Name.ToUpper() == "CHKSELECT")
                    {
                        dgvMixPallet.CurrentCell.Value = !(common.ReplaceNullBoolean(dgvMixPallet.CurrentCell.Value));
                    }
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        private void btnSaveMix_Click(object sender, EventArgs e)
        {
            string lstrSTONO = "STO-1";
            try
            {
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {

                    //Get New STO No.
                    #region Get STO NO
                    MIXED_PALLET_STO max_sto_no = db.MIXED_PALLET_STO.OrderByDescending(x => x.STO_NO).FirstOrDefault();
                    if(max_sto_no!=null)
                    {
                        if(common.ReplaceNullString(max_sto_no.STO_NO)!= "")
                            lstrSTONO = "STO-" + (Convert.ToInt64(lstrSTONO.Substring(lstrSTONO.IndexOf("STO-") + 4))+1).ToString();
                    }
                    #endregion


                    foreach (DataGridViewRow currRow in dgvMixPallet.Rows)
                    {
                        if (common.ReplaceNullBoolean(currRow.Cells["chkSelect"].Value))
                        {
                            long sto_mst_id =Convert.ToInt64( common.ReplaceNullString(currRow.Cells["STO_MST_ID"].Value));
                            MIXED_PALLET_STO mix_pallet_sto = db.MIXED_PALLET_STO.Where(x => x.STO_MST_ID == sto_mst_id).FirstOrDefault();
                            if(mix_pallet_sto!=null)
                            {
                                mix_pallet_sto.STO_NO = lstrSTONO;
                                mix_pallet_sto.STO_TIME = DateTime.Now;
                            }
                        }
                    }
                    db.SaveChanges();
                    lblStoNo.Text = lstrSTONO;
                    ClsMessage.ShowInfo("STO No. has been generated successfully.");
                    LoadPendingMixPallet();
                    lblStoNo.Text = "";
                }
            }
            catch (Exception ex)
            {
                ClsMessage.ShowError("Error occurred during saving data. Please contact application administrator.");
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        private void btnCancelPendingSTO_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc =(sender) as TabControl;
            if (tc.SelectedTab.Name.ToUpper() == "TABPAGE1")
            {
                //common.AddHeaderCheckBoxToDataGrid(ref dgvMixPallet, 3, 6);
                LoadPendingMixPallet();
            }
            else if (tc.SelectedTab.Name.ToUpper() == "TABPAGE3")
            {
                LoadPendingSTO();
            }
            else if (tc.SelectedTab.Name.ToUpper() == "TABPAGE4")
            {
                LoadPendingMRN();
            }
        }

        private void txtCanban_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (common.ReplaceNullString(txtCanban.Text) == "")
                    {
                        ClsMessage.ShowError("Please scan the canban first.");
                        txtCanban.Focus();
                    }
                    else
                    {
                        var arrCanban = txtCanban.Text.Trim().Split(' ');
                        int currTextIndex = 0;
                        string lstrPartNo = "";
                        int lintQty = 0;
                        //travers for each item splited by white space and stored in array. 
                        //There will be many items with white space only as there are multiple spaces in string.
                        //We need to fetch item at index 2 and 3 for part No and Qty respectively
                        foreach (string strVal in arrCanban)
                        {
                            if (strVal.Trim() != "")
                            {
                                currTextIndex += 1;
                                //Part No value is at index 2 in canban string
                                if (currTextIndex == 2)
                                    lstrPartNo = strVal;

                                //Qty value is at index 3 in canban string
                                if (currTextIndex == 3)
                                    lintQty = common.ReplaceNullNumber(strVal);

                                //We dont need any other value for the string hence exit fom loop
                                if (currTextIndex == 3)
                                    break;
                            }
                        }

                        if (lstrPartNo != "")
                        {
                            txtPart_No.Text = lstrPartNo;
                            txtQty.Text = lintQty.ToString();
                            if (dgvPartList.Rows.Count >= 1)
                            {
                                if(common.ReplaceNullNumber(lintQty) <= 0)
                                {
                                    ClsMessage.ShowError("Quantity should be a valid quantity or more than 0.");
                                    return;
                                }

                                if (common.CheckIfPartQuantityExistisInStock(dgvData, txtPart_No.Text.Trim(), lintQty,0))
                                {
                                    dgvData.Rows.Add(lstrPartNo, lintQty, txtCanban.Text.Trim());
                                    pnlPartList.Visible = false;
                                    ClearCurrentRecord();
                                }
                                else
                                    ClsMessage.ShowError("Requested qauntity for this part is not available in stock.");
                            }
                            else
                                ClsMessage.ShowError("Current part does not exists in database. Please add this part in database first.");
                        }
                        else
                            ClsMessage.ShowError("Could not retreive Part details from canban. Please contact your application administrator.");
                    }
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        private void btnCancelPendingMRN_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
