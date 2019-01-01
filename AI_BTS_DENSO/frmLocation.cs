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
    public partial class frmLocation : Form
    {
        //Create instance of Location_Mst table in database to perfrom CRUD operations.
        LOCATION_MST location = new LOCATION_MST();

        //Create instance of common class to use global/common methods
        Common.clsCommon common = new Common.clsCommon();
        public frmLocation()
        {
            InitializeComponent();
        }

        private void frmLocation_Load(object sender, EventArgs e)
        {
            common.GetUserAccessForCurrentScreen(this, "Location");
            
            //refresh the Grid view data on page load so that when user see the page after load he see the all data from table
            RefreshDataGridView();
            txtLocationName.Focus();
            
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
                    location.LOCATION_NAME = txtLocationName.Text.Trim();
                    location.DESCRIPTION = txtLocationDescription.Text.Trim();
                    bool isAddOpr = false;

                    //Save record values to database
                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        //Checks if it is new record to save or existing record to modify. if unique key value is 0(zero) it means its a new record to save else update existing record
                        if (location.LOCATION_MST_ID == 0)
                        {
                            db.LOCATION_MST.Add(location);
                            isAddOpr = true;
                        }
                        else
                            db.Entry(location).State = System.Data.Entity.EntityState.Modified;

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
                    location.LOCATION_MST_ID = Convert.ToInt32(dgvData.CurrentRow.Cells["Location_ID"].Value);

                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        location = db.LOCATION_MST.Where(x => x.LOCATION_MST_ID == location.LOCATION_MST_ID).FirstOrDefault();
                        txtLocationName.Text = location.LOCATION_NAME;
                        txtLocationDescription.Text = location.DESCRIPTION;
                    }

                    //btnDel.Enabled = true;
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
                if (location.LOCATION_MST_ID == 0)
                    ClsMessage.ShowNoRecordSelectedErrorMessage();
                else
                {
                    if (common.ConfirmToDelete())
                    {
                        using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                        {
                            if (db.Entry(location).State == System.Data.Entity.EntityState.Detached)
                                db.LOCATION_MST.Attach(location);
                            db.LOCATION_MST.Remove(location);
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
                ClsMessage.ShowDeleteOperationErrorMessage();
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }


        /// <summary>
        /// Clears the data from all controls on the form.
        /// </summary>
        private void Clear()
        {
            txtLocationName.Text = "";
            txtLocationDescription.Text = "";
            //btnDel.Enabled = false;
            location.LOCATION_MST_ID = 0;
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
                if (txtLocationName.Text.Trim() == "" || txtLocationName.Text == null)
                    ClsMessage.ShowError("Please enter the location name.");
                else if (txtLocationDescription.Text.Trim() == "" || txtLocationDescription.Text == null)
                    ClsMessage.ShowError("Please enter the location description.");
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
        /// Refreshes the data grid view with Location table data from database.
        /// </summary>
        private void RefreshDataGridView()
        {
            try
            {
                dgvData.AutoGenerateColumns = false;
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    dgvData.DataSource = db.LOCATION_MST.ToList<LOCATION_MST>();
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        private void btnPrintBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.SelectedRows.Count > 0)
                    PrintBarCodeData();
                else
                    ClsMessage.ShowError("Please select at least one part to print the barcode.");
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
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
                    List<LOCATION_MST> location_mst;

                    //Print coupon for Selected Row Only
                    foreach (DataGridViewRow currRow in dgvData.SelectedRows)
                    {
                        //Make sure Current row is oot blank as last row may be blank in grid view and user may select it. 
                        if (currRow.Cells["Location_ID"].Value != null)
                        {
                            SatoPrinter p = common.InitiateSatoPrinter();
                            if (p.GetprinterStatus().StartsWith("OK"))
                            {
                                p.PrinLocationBarCode(currRow.Cells["Location_Name"].Value.ToString());
                            }
                        } //end of condition check for current row is not blank for part no.
                    }//end of for loop of each row in data grid view
                    ClsMessage.ShowInfo("Requested barcode labels have been printied successfully.");
                }
            } // end of main try block
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            common.SearchData(ref dgvData,"LOCATION_MST",common.lstrCurrentColumnName,txtSearch.Text.Trim());
        }

        private void dgvData_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            common.lstrCurrentColumnName = dgvData.Columns[e.ColumnIndex].DataPropertyName;
        }
    }
}
