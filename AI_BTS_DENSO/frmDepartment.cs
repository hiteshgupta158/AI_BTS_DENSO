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
    public partial class frmDepartment : Form
    {
        //Create instance of Department_Mst table in database to perfrom CRUD operations.
        DEPARTMENT_MST department = new DEPARTMENT_MST();

        //Create instance of common class to use global/common methods
        clsCommon common = new clsCommon();
        public frmDepartment()
        {
            InitializeComponent();
        }

        private void frmDepartment_Load(object sender, EventArgs e)
        {
            common.GetUserAccessForCurrentScreen(this, "Department");
            common.FillActive(ref cmbActive);

            //refresh the Grid view data on page load so that when user see the page after load he see the all data from table
            RefreshDataGridView();
            txtDepartmentName.Focus();
        }

        private void frmDepartment_FormClosed(object sender, FormClosedEventArgs e)
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
                    department.DEPARTMENT_NAME = txtDepartmentName.Text.Trim();
                    department.ACTIVE = Convert.ToInt16(common.GetActiveComboValue(cmbActive.Text.Trim()));

                    bool isAddOpr = false;

                    //Save record values to database
                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        //Checks if it is new record to save or existing record to modify. if unique key value is 0(zero) it means its a new record to save else update existing record
                        if (department.DEPARTMENT_MST_ID == 0)
                        {
                            db.DEPARTMENT_MST.Add(department);
                            isAddOpr = true;
                        }
                        else
                            db.Entry(department).State = System.Data.Entity.EntityState.Modified;

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
            catch (Exception ex)
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
                    department.DEPARTMENT_MST_ID = Convert.ToInt32(dgvData.CurrentRow.Cells["Department_ID"].Value);

                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        department = db.DEPARTMENT_MST.Where(x => x.DEPARTMENT_MST_ID == department.DEPARTMENT_MST_ID).FirstOrDefault();
                        txtDepartmentName.Text = department.DEPARTMENT_NAME;
                        cmbActive.SelectedValue = department.ACTIVE_MST.ACTIVE_MST_ID;
                    }

                    //btnDel.Enabled = true;
                }
            }
            catch (Exception ex)
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
                if (department.DEPARTMENT_MST_ID == 0)
                    ClsMessage.ShowNoRecordSelectedErrorMessage();
                else
                {

                    if (common.ConfirmToDelete())
                    {
                        using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                        {
                            if (db.Entry(department).State == System.Data.Entity.EntityState.Detached)
                                db.DEPARTMENT_MST.Attach(department);
                            db.DEPARTMENT_MST.Remove(department);
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
            txtDepartmentName.Text = "";
            cmbActive.SelectedIndex = -1;
            department.DEPARTMENT_MST_ID = 0;
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
                if (txtDepartmentName.Text.Trim() == "" || txtDepartmentName.Text == null)
                    ClsMessage.ShowError("Please enter the department name.");
                else if (cmbActive.Text.Trim() == "")
                    ClsMessage.ShowError("Please enter the department description.");
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
        /// Refreshes the data grid view with Department table data from database.
        /// </summary>
        private void RefreshDataGridView()
        {
            try
            {
                dgvData.AutoGenerateColumns = false;
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    var query = from a in db.DEPARTMENT_MST
                                join b in db.ACTIVE_MST
                                on a.ACTIVE equals b.ACTIVE_MST_ID
                                select new
                                {
                                    DEPARTMENT_MST_ID = a.DEPARTMENT_MST_ID,
                                    DEPARTMENT_NAME = a.DEPARTMENT_NAME,
                                    ACTIVE = b.ACTIVE_VALUE,
                                    ACTIVE_MST_ID = b.ACTIVE_MST_ID
                                };

                    dgvData.DataSource = query.ToList();
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
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


                string lstrQry = "Select DEPARTMENT_MST_ID,DEPARTMENT_NAME,AM.ACTIVE_MST_ID,AM.ACTIVE_VALUE AS ACTIVE from DEPARTMENT_MST DM INNER JOIN ACTIVE_MST AM on DM.Active = AM.ACTIVE_MST_ID";
                if (pstrTextToSearch != "")
                {
                    lstrQry += " where ";

                    if (pstrColumnToSearch.ToUpper() == "ACTIVE")
                        lstrQry += "AM.ACTIVE_VALUE like '" + pstrTextToSearch.Replace("'", "''") + "%'"; 
                    else
                        lstrQry += pstrColumnToSearch + " like '" + pstrTextToSearch.Replace("'", "''") + "%'";
                }

                dt = common.getTable(lstrQry);
                dgvData.DataSource = dt;

            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }
    }
}
