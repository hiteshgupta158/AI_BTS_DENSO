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
    public partial class frmProcess : Form
    {
        //Create instance of Process_Mst table in database to perfrom CRUD operations.
        PROCESS_MST process = new PROCESS_MST();

        //Create instance of common class to use global/common methods
        Common.clsCommon common = new Common.clsCommon();
        public frmProcess()
        {
            InitializeComponent();
        }

        private void frmProcess_Load(object sender, EventArgs e)
        {
            common.GetUserAccessForCurrentScreen(this, "Process");
            //refresh the Grid view data on page load so that when user see the page after load he see the all data from table

            common.FillProcessType(ref cmbProcessType);
            common.FillFinishedGoods(ref cmbFG);
            common.FillActive(ref cmbActive);

            RefreshDataGridView();
            txtProcessName.Focus();
        }

        private void frmProcess_FormClosed(object sender, FormClosedEventArgs e)
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
                    process.PROCESS_NAME = txtProcessName.Text.Trim();
                    process.PROCESS_TYPE_MST_ID = Convert.ToInt32(cmbProcessType.SelectedValue.ToString());
                    process.FG_MST_ID = Convert.ToInt32(cmbFG.SelectedValue.ToString());
                    process.ACTIVE = Convert.ToInt32(cmbActive.SelectedValue.ToString());//  Convert.ToInt16(common.GetActiveComboValue(cmbActive.Text.Trim()));
                    process.SEQUENCE = txtSequence.Text.Trim();

                    bool blnIsAddAction = false;

                    //Save record values to database
                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        //Checks if it is new record to save or existing record to modify. if unique key value is 0(zero) it means its a new record to save else update existing record
                        if (process.PROCESS_MST_ID == 0)
                        {
                            db.PROCESS_MST.Add(process);
                            blnIsAddAction = true;
                        }
                        else
                            db.Entry(process).State = System.Data.Entity.EntityState.Modified;

                        //commit changes to database
                        db.SaveChanges();
                    }
                    //clear the form content
                    Clear();

                    //Refresh the datagrid view to depict the updated data
                    RefreshDataGridView();

                    //display a confirmation msg to user about saving the data.
                    ClsMessage.ShowDataSavedConfirmation(blnIsAddAction);
                }
            }
            catch (Exception ex)
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
                    process = new PROCESS_MST();

                    //Set model's primary key value to Update/Delete the coresponsing key's record
                    process.PROCESS_MST_ID = Convert.ToInt32(dgvData.CurrentRow.Cells["Process_ID"].Value);

                    //set currently selected record's value in form controls
                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        process = db.PROCESS_MST.Where(x => x.PROCESS_MST_ID == process.PROCESS_MST_ID).FirstOrDefault();
                        txtProcessName.Text = process.PROCESS_NAME;
                        cmbProcessType.SelectedValue = process.PROCESS_TYPE_MST_ID;
                        cmbFG.SelectedValue = process.FG_MST_ID;
                        cmbActive.SelectedValue = process.ACTIVE;
                        txtSequence.Text = process.SEQUENCE;
                    }
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
                if (process.PROCESS_MST_ID == 0)
                    ClsMessage.ShowNoRecordSelectedErrorMessage();
                else
                {
                    if (common.ConfirmToDelete())
                    {
                        using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                        {
                            if (db.Entry(process).State == System.Data.Entity.EntityState.Detached)
                                db.PROCESS_MST.Attach(process);
                            db.PROCESS_MST.Remove(process);
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
            txtProcessName.Text = "";
            cmbProcessType.SelectedIndex = -1;
            cmbFG.SelectedIndex = -1;
            cmbActive.SelectedIndex = -1; ;
            txtSequence.Text = "";
            process.PROCESS_MST_ID = 0;
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
                if (txtProcessName.Text.Trim() == "" || txtProcessName.Text == null)
                {
                    ClsMessage.ShowError("Please enter the process name.");
                    txtProcessName.Focus();
                }
                else if (cmbProcessType.Text == "")
                {
                    ClsMessage.ShowError("Please select the process type first.");
                    cmbProcessType.Focus();
                }
                else if (cmbFG.Text == "")
                {
                    ClsMessage.ShowError("Please select the Finished Goods first.");
                    cmbFG.Focus();
                }
                else if (cmbActive.Text.Trim() == "")
                {
                    ClsMessage.ShowError("Please select the active status of process first.");
                    cmbActive.Focus();
                }
                else if(txtSequence.Text.Trim() =="" || txtSequence.Text==null)
                {
                    ClsMessage.ShowError("Please enter the process sequence first.");
                    txtSequence.Focus();
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
        /// Refreshes the data grid view with Process table data from database.
        /// </summary>
        private void RefreshDataGridView()
        {
            try
            {
                dgvData.AutoGenerateColumns = false;
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    var query = from a in db.PROCESS_MST
                                join b in db.PROCESS_TYPE_MST on a.PROCESS_TYPE_MST_ID equals b.PROCESS_TYPE_MST_ID
                                join c in db.FG_MST on a.FG_MST_ID equals c.FG_MST_ID
                                join e in db.ACTIVE_MST on a.ACTIVE equals e.ACTIVE_MST_ID
                                select new
                                {
                                    PROCESS_MST_ID = a.PROCESS_MST_ID,
                                    PROCESS_NAME = a.PROCESS_NAME,
                                    PROCESS_TYPE_NAME = b.PROCESS_TYPE_NAME,
                                    FG_NAME=c.FG_NAME,
                                    ACTIVE = e.ACTIVE_VALUE,
                                    SEQUENCE = a.SEQUENCE,
                                };

                    dgvData.DataSource = query.ToList();
                }
            }
            catch (Exception ex)
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
            SearchData(common.lstrCurrentColumnName, txtSearch.Text.Trim());
        }

        public void SearchData(string pstrColumnToSearch, string pstrTextToSearch)
        {
            DataTable dt = new DataTable();
            try
            {
                if (pstrColumnToSearch == "")
                    pstrColumnToSearch = dgvData.Columns[0].DataPropertyName;

                string lstrQry = "Select PM.*, PTM.PROCESS_TYPE_NAME,FG.FG_NAME, AM.ACTIVE_VALUE AS ACTIVE From " +
                    "PROCESS_MST PM Inner JOIN PROCESS_Type_MST PTM on PM.Process_Type_MST_ID = PTM.Process_Type_MST_ID " +
                    "Inner JOIN FG_MST FG ON PM.FG_MST_ID = FG.FG_MST_ID " +
                    "Inner JOIN ACTIVE_MST AM ON PM.ACTIVE = AM.ACTIVE_MST_ID ";

                if (pstrTextToSearch != "")
                {
                    lstrQry += "Where ";

                    if (pstrColumnToSearch.ToUpper() == "PROCESS_TYPE_NAME")
                        lstrQry += "PTM.PROCESS_TYPE_NAME like '" + pstrTextToSearch.Replace("'", "''") + "%'";
                    else if (pstrColumnToSearch.ToUpper() == "FG_NAME")
                        lstrQry += "FG.FG_NAME like '" + pstrTextToSearch.Replace("'", "''") + "%'";
                    else if (pstrColumnToSearch.ToUpper() == "ACTIVE")
                        lstrQry += "AM.ACTIVE_VALUE like '" + pstrTextToSearch.Replace("'", "''") + "%'";
                    else
                        lstrQry += " PM." + pstrColumnToSearch + " like '" + pstrTextToSearch.Replace("'", "''") + "%'";
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
