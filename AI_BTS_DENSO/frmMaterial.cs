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
    public partial class frmMaterial : Form
    {
        //Create instance of Material_Mst table in database to perfrom CRUD operations.
        MATERIAL_MST material = new MATERIAL_MST();

        //Create instance of common class to use global/common methods
        Common.clsCommon common = new Common.clsCommon();
        public frmMaterial()
        {
            InitializeComponent();
        }

        private void frmMaterial_Load(object sender, EventArgs e)
        {
            common.GetUserAccessForCurrentScreen(this, "Material");

            common.FillActive(ref cmbActive);

            common.FillMaterialType(ref cmbMaterialType);

            common.FillUOM(ref cmbPrimaryUOM);

            common.FillUOM(ref cmbSecondaryUOM);

            //refresh the Grid view data on page load so that when user see the page after load he see the all data from table
            RefreshDataGridView();
            txtPartNo.Focus();
        }

        private void frmMaterial_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        
        //On click of "Add" button form should be clear, to enter details of new records to be added. It is useful when user double clicked on gridview and text boxes
        //contain the data of existing records, but in between user wants to add new record instead of performing Update/Delete opperation.
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Clear();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //First check if all controls are filled with correct/required data and form is valid to be submitted
                if (ValidateForm())
                {
                    //Set model with form content
                    material.PART_NO = txtPartNo.Text.Trim();
                    material.PART_NAME = txtPartName.Text.Trim();
                    material.DESCRIPTION = txtMaterialDescription.Text.Trim();
                    material.PACK_SIZE =Convert.ToInt16(txtPackSize.Text.Trim());

                    material.MATERIAL_TYPE_MST_ID = Convert.ToInt16(cmbMaterialType.SelectedValue.ToString());
                    material.PRIMARY_UOM_ID = Convert.ToInt16(cmbPrimaryUOM.SelectedValue.ToString());
                    material.SECONDARY_UOM_ID = Convert.ToInt16(cmbSecondaryUOM.SelectedValue.ToString());
                    material.BAG_QTY = Convert.ToInt32(common.ReplaceNullNumber(txtQtyPerBag.Text).ToString());
                    material.CAGE_QTY = Convert.ToInt32(common.ReplaceNullNumber(txtQtyPerCage.Text).ToString());

                    material.ACTIVE = Convert.ToInt16(cmbActive.SelectedValue.ToString());
                    bool isAddOpr = false;

                    //Save record values to database
                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        //Checks if it is new record to save or existing record to modify. if unique key value is 0(zero) it means its a new record to save else update existing record
                        if (material.MATERIAL_MST_ID == 0)
                        {
                            db.MATERIAL_MST.Add(material);
                            isAddOpr = true;
                        }
                        else
                            db.Entry(material).State = System.Data.Entity.EntityState.Modified;

                        //commit changes to database
                        db.SaveChanges();
                    }
                    //clear the form content
                    Clear();

                    //Refresh the datagrid view to depict the updated data
                    RefreshDataGridView();

                    //display a confirmation msg to user about saving the data.
                    ClsMessage.ShowDataSavedConfirmation(isAddOpr);
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
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
                //Checks if currently selected row is not the DataGridView header, but an actual data row.
                if (dgvData.CurrentRow.Index != -1)
                {
                    //Set model's primary key value to Update/Delete the coresponsing key's record
                    material.MATERIAL_MST_ID = Convert.ToInt32(dgvData.CurrentRow.Cells["Material_ID"].Value);

                    //set currently selected record's value in form controls
                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        material = db.MATERIAL_MST.Where(x => x.MATERIAL_MST_ID == material.MATERIAL_MST_ID).FirstOrDefault();
                        txtPartNo.Text = material.PART_NO;
                        txtPartName.Text = material.PART_NAME;
                        cmbMaterialType.SelectedValue = material.MATERIAL_TYPE_MST_ID;
                        cmbPrimaryUOM.SelectedValue = material.PRIMARY_UOM_ID;
                        cmbSecondaryUOM.SelectedValue = material.SECONDARY_UOM_ID;
                        cmbActive.SelectedValue = material.ACTIVE;

                        txtPackSize.Text = material.PACK_SIZE.ToString();
                        txtMaterialDescription.Text = material.DESCRIPTION;

                        txtQtyPerBag.Text = material.BAG_QTY.ToString();
                        txtQtyPerCage.Text = material.CAGE_QTY.ToString();
                    }

                    //btnDel.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }


        private void pnlControl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (material.MATERIAL_MST_ID == 0)
                    ClsMessage.ShowNoRecordSelectedErrorMessage();
                else
                {
                    if (common.ConfirmToDelete())
                    {
                        using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                        {
                            if (db.Entry(material).State == System.Data.Entity.EntityState.Detached)
                                db.MATERIAL_MST.Attach(material);
                            db.MATERIAL_MST.Remove(material);
                            db.SaveChanges();
                            RefreshDataGridView();
                            ClsMessage.ShowDataDeleteConfirmation();
                            Clear();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }


        /// <summary>
        /// Clears the data from all controls on the form.
        /// </summary>
        private void Clear()
        {
            txtPartNo.Text = "";
            txtPartName.Text = "";

            cmbMaterialType.SelectedIndex = -1;
            cmbPrimaryUOM.SelectedIndex=-1;
            cmbSecondaryUOM.SelectedIndex = -1;
            cmbActive.SelectedIndex = -1;

            txtPackSize.Text = "";
            txtMaterialDescription.Text = "";
                
            //btnDel.Enabled = false;
            material.MATERIAL_MST_ID = 0;
        }


        /// <summary>
        /// Validate the form to make sure that all controls are filled with valid data before submiting for CRUD operation.
        /// </summary>
        /// <returns>Returns True if form is valid else returns False.</returns>
        private bool ValidateForm()
        {
            bool IsFormValid = false;
            try
            {
                if (txtPartNo.Text.Trim() == "" || txtPartNo.Text == null)
                {
                    ClsMessage.ShowError("Please enter the Part No.");
                    txtPartNo.Focus();
                }
                else if (txtPartName.Text.Trim() == "" || txtPartName.Text == null)
                {
                    ClsMessage.ShowError("Please enter the Part Name.");
                    txtPartName.Focus();
                }
                else if (cmbMaterialType.Text == "")
                {
                    ClsMessage.ShowError("Please select the material type first.");
                    cmbMaterialType.Focus();
                }
                else if (cmbPrimaryUOM.Text.Trim() == "")
                {
                    ClsMessage.ShowError("Please Primary UOM Type first.");
                    cmbPrimaryUOM.Focus();
                }
                else if (cmbSecondaryUOM.Text.Trim() == "")
                {
                    ClsMessage.ShowError("Please select the Secondary UOM type first.");
                    cmbSecondaryUOM.Focus();
                }
                else if(txtPackSize.Text =="" || txtPackSize.Text == null)
                {
                    ClsMessage.ShowError("Please enter the pack size first.");
                    txtPackSize.Focus();
                }
                else if (txtMaterialDescription.Text==""|| txtMaterialDescription.Text ==null)
                {
                    ClsMessage.ShowError("Please enter the material's description first.");
                    txtMaterialDescription.Focus();
                }
                else if(cmbActive.Text=="")
                {
                    ClsMessage.ShowError("Please select the material active status first.");
                    cmbActive.Focus();
                }
                else if (!common.IsNumeric(txtQtyPerBag.Text) && txtQtyPerBag.Text.Trim() !="")
                {
                    ClsMessage.ShowError("Please enter the valid quantity per bag first.");
                    txtQtyPerBag.Focus();
                }
                else if (!common.IsNumeric(txtQtyPerCage.Text.Trim()) && txtQtyPerCage.Text.Trim() !="")
                {
                    ClsMessage.ShowError("Please enter the valid quantity per cage first.");
                    txtQtyPerCage.Focus();
                }
                else
                    IsFormValid = true;

            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return IsFormValid;
        }


        /// <summary>
        /// Refreshes the data grid view with Material table data from database.
        /// </summary>
        private void RefreshDataGridView()
        {
            try
            {
                dgvData.AutoGenerateColumns = false;
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    var query = from mtrl in db.MATERIAL_MST
                                join uom in db.UOM_MST on mtrl.PRIMARY_UOM_ID equals uom.UOM_MST_ID
                                join act in db.ACTIVE_MST on mtrl.ACTIVE equals act.ACTIVE_MST_ID
                                join mtrltp in db.MATERIAL_TYPE_MST on mtrl.MATERIAL_TYPE_MST_ID equals mtrltp.MATERIAL_TYPE_MST_ID
                                join uom1 in db.UOM_MST on mtrl.SECONDARY_UOM_ID equals uom1.UOM_MST_ID
                                select new
                                {
                                    MATERIAL_MST_ID = mtrl.MATERIAL_MST_ID,
                                    PART_NO = mtrl.PART_NO,
                                    PART_NAME = mtrl.PART_NAME,
                                    MATERIAL_TYPE = mtrltp.MATERIAL_TYPE_NAME,
                                    PRIMARY_UOM = uom.UOM_NAME,
                                    SECONDARY_UOM = uom1.UOM_NAME,
                                    PACK_SIZE = mtrl.PACK_SIZE,
                                    DESCRIPTION = mtrl.DESCRIPTION,
                                    ACTIVE = act.ACTIVE_VALUE,
                                    BAG_QTY = mtrl.BAG_QTY,
                                    CAGE_QTY = mtrl.CAGE_QTY
                                };
                    var result = query.ToList();

                    dgvData.DataSource = query.ToList();
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbActive_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbActive_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtQtyPerBag.Focus();

        }

        private void txtQtyPerBag_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtQtyPerCage.Focus();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchData(common.lstrCurrentColumnName, txtSearch.Text.Trim());
        }

        private void dgvData_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            common.lstrCurrentColumnName = dgvData.Columns[e.ColumnIndex].DataPropertyName;
        }


        public void SearchData(string pstrColumnToSearch, string pstrTextToSearch)
        {
            DataTable dt = new DataTable();
            try
            {
                if (pstrColumnToSearch == "")
                    pstrColumnToSearch = dgvData.Columns[0].DataPropertyName;


                string lstrQry = "Select MM.*, UM.UOM_NAME AS PRIMARY_UOM,UM1.UOM_NAME AS SECONDARY_UOM,MTM.MATERIAL_TYPE_NAME AS MATERIAL_TYPE From " +
                    "Material_MST MM Inner JOIN Material_Type_MST MTM on MM.Material_Type_MST_ID = MTM.Material_Type_MST_ID " + 
                    "Inner JOIN UOM_MST UM ON MM.Primary_UOM_ID = UM.UOM_MST_ID " +
                    "Inner JOIN UOM_MST UM1 ON MM.Secondary_UOM_ID = UM1.UOM_MST_ID ";
               
                if (pstrTextToSearch != "")
                {
                    lstrQry += "Where ";

                    if (pstrColumnToSearch.ToUpper() == "PRIMARY_UOM")
                        lstrQry += "UM.UOM_NAME like '" + pstrTextToSearch.Replace("'", "''") + "%'";
                    else if (pstrColumnToSearch.ToUpper() == "SECONDARY_UOM")
                        lstrQry += "UM1.UOM_NAME like '" + pstrTextToSearch.Replace("'", "''") + "%'";
                    else if (pstrColumnToSearch.ToUpper() == "MATERIAL_TYPE")
                        lstrQry += "MTM.MATERIAL_TYPE_NAME like '" + pstrTextToSearch.Replace("'", "''") + "%'";
                    else
                        lstrQry += " MM." + pstrColumnToSearch + " like '" + pstrTextToSearch.Replace("'", "''") + "%'";
                }
                    

                dt = common.getTable(lstrQry);
                dgvData.DataSource = dt;

            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            //return dt;
        }
    }
}
