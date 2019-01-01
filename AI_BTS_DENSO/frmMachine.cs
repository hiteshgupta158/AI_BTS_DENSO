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
using System.Configuration;

namespace AI_BTS_DENSO
{
    public partial class frmMachine : Form
    {
        //Create instance of Machine_Mst table in database to perfrom CRUD operations.
        MACHINE_MST machine = new MACHINE_MST();

        //Create instance of common class to use global/common methods
        Common.clsCommon common = new Common.clsCommon();
        public frmMachine()
        {
            InitializeComponent();
        }

        private void frmMachine_Load(object sender, EventArgs e)
        {
            //common.LoadFormColors(this);
            common.FillMachineType(ref cmbMachineType);
            common.FillProcess(ref cmbProcess);
            common.FillActive(ref cmbActive);

            //refresh the Grid view data on page load so that when user see the page after load he see the all data from table
            RefreshDataGridView();
            txtMachineName.Focus();
        }

        private void frmMachine_FormClosed(object sender, FormClosedEventArgs e)
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
                    machine.MACHINE_NAME = txtMachineName.Text.Trim();
                    machine.MACHINE_TYPE_MST_ID =Convert.ToInt32( cmbMachineType.SelectedValue);
                    machine.PROCESS_MST_ID = Convert.ToInt32(cmbProcess.SelectedValue);
                    machine.ACTIVE = Convert.ToInt32(cmbActive.SelectedValue);
                    machine.DESCRIPTION = txtMachineDescription.Text.Trim();

                    bool isAddOpr = false;

                    //Save record values to database
                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        //Checks if it is new record to save or existing record to modify. if unique key value is 0(zero) it means its a new record to save else update existing record
                        if (machine.MACHINE_MST_ID == 0)
                        {
                            db.MACHINE_MST.Add(machine);
                            isAddOpr = true;
                        }
                        else
                            db.Entry(machine).State = System.Data.Entity.EntityState.Modified;

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
                ClsMessage.ShowConfimation("An error occurred while saving the data. Please contact application aadministrator");
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
                    machine = new MACHINE_MST();
                    //Set model's primary key value to Update/Delete the coresponsing key's record
                    machine.MACHINE_MST_ID = Convert.ToInt32(dgvData.CurrentRow.Cells["Machine_ID"].Value);

                    //set currently selected record's value in form controls
                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        machine = db.MACHINE_MST.Where(x => x.MACHINE_MST_ID == machine.MACHINE_MST_ID).FirstOrDefault();
                        txtMachineName.Text = machine.MACHINE_NAME;
                        cmbMachineType.SelectedValue = machine.MACHINE_TYPE_MST_ID;
                        cmbProcess.SelectedValue = machine.PROCESS_MST_ID;
                        cmbActive.SelectedValue = machine.ACTIVE.ToString();
                        txtMachineDescription.Text = machine.DESCRIPTION;
                    }
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
                if (common.ConfirmToDelete())
                {
                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        if (db.Entry(machine).State == System.Data.Entity.EntityState.Detached)
                            db.MACHINE_MST.Attach(machine);
                        db.MACHINE_MST.Remove(machine);
                        db.SaveChanges();
                        RefreshDataGridView();
                        ClsMessage.ShowDataDeleteConfirmation();
                        Clear();
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
            txtMachineName.Text = "";
            cmbMachineType.SelectedIndex = -1;
            cmbProcess.SelectedIndex = -1;
            cmbActive.SelectedIndex = -1;
            txtMachineDescription.Text = "";
            machine.MACHINE_MST_ID = 0;
        }


        /// <summary>
        /// Validate the form to make sure that all controls are filled with valid data before submiting for CRUD operation.
        /// </summary>
        /// <returns>Returns True if form is valid else returns False.</returns>
        private bool ValidateForm()
        {
            bool IsFormValid = false;

            if (txtMachineName.Text.Trim() == "" || txtMachineName.Text == null)
            {
                ClsMessage.ShowError("Please enter the machine name.");
                txtMachineName.Focus();
            }
            else if(cmbMachineType.Text=="")
            {
                ClsMessage.ShowError("Please select the machine type first.");
                cmbMachineType.Focus();
            }
            else if (cmbProcess.Text == "")
            {
                ClsMessage.ShowError("Please select the process first.");
                cmbProcess.Focus();
            }
            else if (cmbActive.Text == "")
            {
                ClsMessage.ShowError("Please select the active status first.");
                cmbActive.Focus();
            }
            else if (txtMachineDescription.Text.Trim() == "" || txtMachineDescription.Text==null)
            {
                ClsMessage.ShowError("Please enter the machine description.");
                txtMachineDescription.Focus();
            }
            else
                IsFormValid = true;

            return IsFormValid;
        }


        /// <summary>
        /// Refreshes the data grid view with Machine table data from database.
        /// </summary>
        private void RefreshDataGridView()
        {
            dgvData.AutoGenerateColumns = false;
            using (AI_BTS_DENSOEntities1 db=new AI_BTS_DENSOEntities1())
            {
                var query = from a in db.MACHINE_MST
                            join b in db.PROCESS_MST on a.PROCESS_MST_ID equals b.PROCESS_MST_ID
                            join c in db.MACHINE_TYPE_MST on a.MACHINE_TYPE_MST_ID equals c.MACHINE_TYPE_MST_ID
                            join d in db.ACTIVE_MST  on a.ACTIVE equals d.ACTIVE_MST_ID
                            select new
                            {
                                MACHINE_MST_ID = a.MACHINE_MST_ID,
                                MACHINE_NAME = a.MACHINE_NAME,
                                PROCESS_NAME = b.PROCESS_NAME,
                                MACHINE_TYPE_NAME = c.MACHINE_TYPE_NAME,
                                DESCRIPTION = a.DESCRIPTION,
                                ACTIVE = d.ACTIVE_VALUE
                            };

                dgvData.DataSource = query.ToList();
            }
        }

        private void btnPrintBarcode_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
                PrintBarCodeData();
            else
                ClsMessage.ShowError("Please select at least one part to print the barcode.");
        }

        private void PrintBarCodeData()
        {
            try
            {
                if (common.ConfirmToPrintBarCode())
                {
                    //=================================================================================================
                    //For each Part in the GRN generate the Primary Barcode
                    //=================================================================================================
                    //Print coupon for Selected Row Only
                    foreach (DataGridViewRow currRow in dgvData.SelectedRows)
                    {
                        //Make sure Current row is oot blank as last row may be blank in grid view and user may select it. 
                        if (currRow.Cells["Machine_ID"].Value != null)
                        {
                            SatoPrinter p = common.InitiateSatoPrinter();
                            if (p.GetprinterStatus().StartsWith("OK"))
                            {
                                p.PrintMachineLabel(currRow.Cells["Machine_Name"].Value.ToString(), currRow.Cells["Description"].Value.ToString());
                            }
                        } //end of condition check for current row is not blank for part no.
                    }//end of for loop of each row in data grid view
                }
                ClsMessage.ShowInfo("Requested barcode labels have been printied successfully.");
            } // end of main try block
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        public void SearchData(string pstrColumnToSearch, string pstrTextToSearch)
        {
            DataTable dt = new DataTable();
            try
            {
                if (pstrColumnToSearch == "")
                    pstrColumnToSearch = dgvData.Columns[0].DataPropertyName;

                string lstrQry = "Select MM.MACHINE_MST_ID, MM.MACHINE_NAME, MM.DESCRIPTION," +
                    "MTM.MACHINE_TYPE_NAME,PM.PROCESS_NAME,AM.ACTIVE_VALUE AS ACTIVE From " +
                    "MACHINE_MST MM Inner JOIN MACHINE_TYPE_MST MTM on MM.MACHINE_TYPE_MST_ID = MTM.MACHINE_TYPE_MST_ID " + 
                    "Inner JOIN PROCESS_MST PM on MM.Process_MST_ID = PM.Process_MST_ID " +
                    "Inner JOIN ACTIVE_MST AM ON PM.ACTIVE = AM.ACTIVE_MST_ID ";

                if (pstrTextToSearch != "")
                {
                    lstrQry += "Where ";

                    if (pstrColumnToSearch.ToUpper() == "MACHINE_TYPE_NAME")
                        lstrQry += "MTM.MACHINE_TYPE_NAME like '" + pstrTextToSearch.Replace("'", "''") + "%'";
                    else if (pstrColumnToSearch.ToUpper() == "PROCESS_NAME")
                        lstrQry += "PM.PROCESS_NAME like '" + pstrTextToSearch.Replace("'", "''") + "%'";
                    else if (pstrColumnToSearch.ToUpper() == "ACTIVE")
                        lstrQry += "AM.ACTIVE_VALUE like '" + pstrTextToSearch.Replace("'", "''") + "%'";
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
        }

        private void txtSearch_TextChanged(object sender,EventArgs e)
        {
            SearchData(common.lstrCurrentColumnName, txtSearch.Text.Trim());
        }

        private void dgvData_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            common.lstrCurrentColumnName = dgvData.Columns[e.ColumnIndex].DataPropertyName;
        }
    }
}
