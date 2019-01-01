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
    public partial class frmCategory : Form
    {
        //Create instance of PROCESS_Type_Mst table in database to perfrom CRUD operations.
        CATEGORY_MST Category  = new CATEGORY_MST();

        //Create instance of common class to use global/common methods
        Common.clsCommon common = new Common.clsCommon();
        public frmCategory()
        {
            InitializeComponent();
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {
            common.GetUserAccessForCurrentScreen(this, "Category");
            //refresh the Grid view data on page load so that when user see the page after load he see the all data from table
            RefreshDataGridView();
            txtCategoryName.Focus();
        }

        private void frmCategory_FormClosed(object sender, FormClosedEventArgs e)
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
                    Category.CATEGORY_NAME = txtCategoryName.Text.Trim();
                    Category.DESCRIPTION = txtCategoryDescription.Text.Trim();

                    bool isAddOpr = false;

                    //Save record values to database
                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        //Checks if it is new record to save or existing record to modify. if unique key value is 0(zero) it means its a new record to save else update existing record
                        if (Category.CATEGORY_MST_ID == 0)
                        {
                            db.CATEGORY_MST.Add(Category);
                            isAddOpr = true;
                        }
                        else
                            db.Entry(Category).State = System.Data.Entity.EntityState.Modified;

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


        private void dgvData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //Checks if currently selected row is not the DataGridView header, but an actual data row.
                if (dgvData.CurrentRow.Index != -1)
                {
                    //Set model's primary key value to Update/Delete the coresponsing key's record
                    Category.CATEGORY_MST_ID = Convert.ToInt32(dgvData.CurrentRow.Cells["Category_ID"].Value);

                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        Category = db.CATEGORY_MST.Where(x => x.CATEGORY_MST_ID == Category.CATEGORY_MST_ID).FirstOrDefault();
                        txtCategoryName.Text = Category.CATEGORY_NAME;
                        txtCategoryDescription.Text = Category.DESCRIPTION;
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
                if (Category.CATEGORY_MST_ID == 0)
                    ClsMessage.ShowNoRecordSelectedErrorMessage();
                else
                {
                    if (common.ConfirmToDelete())
                    {
                        using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                        {
                            if (db.Entry(Category).State == System.Data.Entity.EntityState.Detached)
                                db.CATEGORY_MST.Attach(Category);
                            db.CATEGORY_MST.Remove(Category);
                            db.SaveChanges();
                            RefreshDataGridView();
                            ClsMessage.ShowDataDeleteConfirmation();
                            Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }


        /// <summary>
        /// Clears the data from all controls on the form.
        /// </summary>
        private void Clear()
        {
            txtCategoryName.Text = "";
            txtCategoryDescription.Text = "";
            //btnDel.Enabled = false;
            Category.CATEGORY_MST_ID = 0;
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
                if (txtCategoryName.Text.Trim() == "" || txtCategoryName.Text == null)
                    ClsMessage.ShowError("Please enter the Category name.");
                else if (txtCategoryDescription.Text.Trim() == "" || txtCategoryDescription.Text == null)
                    ClsMessage.ShowError("Please enter the Category description.");
                else
                    IsFormValid = true;
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }

            return IsFormValid;
        }


        /// <summary>
        /// Refreshes the data grid view with PROCESS_Type table data from database.
        /// </summary>
        private void RefreshDataGridView()
        {
            try
            {
                dgvData.AutoGenerateColumns = false;
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    dgvData.DataSource = db.CATEGORY_MST.ToList<CATEGORY_MST>();
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        private void dgvData_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            common.lstrCurrentColumnName = dgvData.Columns[e.ColumnIndex].DataPropertyName;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            common.SearchData(ref dgvData, "CATEGORY_MST", common.lstrCurrentColumnName, txtSearch.Text.Trim());
        }
    }
}
