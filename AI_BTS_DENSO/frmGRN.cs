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
using SATOPrinterAPI;
using SATOPrinterInterface;
using ZTCommon;
using System.IO;
using System.Data.Odbc;
using System.Reflection;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using System.Collections;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using AI_BTS_DENSO.Reports;
using ThoughtWorks.QRCode;

namespace AI_BTS_DENSO
{
    public partial class frmGRN : Form
    {
        //create  variable which will be used to track current row in Data Grid View
        int lintCurrentRowIndex = -1;
        OdbcConnection lOdbcCon;
        DataSet mds;
        GRN_DATA grn_data = new GRN_DATA();
        Hashtable htPalletPrinted = new Hashtable();
        Hashtable htPalletSerial = new Hashtable();

        //create a variable to store value of currently selected part from part list to add/update in grid view
        //string /*lstrCurrentMateriaID*/ = "";

        //Create instance of common class to use global/common methods
        AI_BTS_DENSO.Common.clsCommon common = new Common.clsCommon();
        clsExcel cExcel;



        GRN_MST grn_mst = new GRN_MST();
        GRN_DTL grn_dtl = new GRN_DTL();
        GRN_LABEL_PRINTING grn_label = new GRN_LABEL_PRINTING();

        //All GRN Data will also pushed in QC too with status as 0 (pending). At the time of QC only status will be changed
        QC_MST qc_mst = new QC_MST();
        QC_DTL qc_dtl = new QC_DTL();
        clsODBC cOdbc;


        int mintNoOfMixPallete = 0;
        Hashtable htMixPalletBoxQuantity = new Hashtable();
        public frmGRN()
        {
            InitializeComponent();
        }

        private void frmGRN_Load(object sender, EventArgs e)
        {
            //common.LoadFormColors(this);
            //cOdbc = new clsODBC(common.gstrSigmaConString);
            common.GetUserAccessForCurrentScreen(this, "GRN");
            common.AddHeaderCheckBoxToDataGrid(ref dgvData);
            #region Fill DropDownLists
            //Fill GRN Type Combo boxes
            common.FillGRNType(ref cmbGRNType,clsCurrentUser.Site_Name);
            #endregion
            //Clear the controls on form
            Clear();
        }

        #region SetControlFocus
        private void cmbGRNType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                if (cmbGRNType.Text != "PO")
                    txtStoNo.Focus();
                else
                    txtANoticeNo.Focus();
            }
        }

        private void txtInvoiceNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 13)
                dtInvoiceDate.Focus();
        }
        #endregion

        #region Others
        private void dtInvoiceDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtInvoiceDate.Value > DateTime.Today)
            {
                ClsMessage.ShowError("Invoice Date cannot be greater than current date.");
                dtInvoiceDate.Value = DateTime.Today;
            }
        }

        private void txtANoticeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnSave.Enabled = false;
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                grn_data = common.GetGRNData_ByNoticeNo(txtANoticeNo.Text.Trim(),"Parts");
                if (grn_data != null)
                {
                    LoadGRNDataInForm(grn_data, true);
                    btnPrintBarcode.Enabled = true;
                    btnSigma.Enabled = false;
                    btnBrowse.Enabled = false;
                    groupBox1.Enabled = false;
                }
                else
                {
                    common.ClearDataGridViewRows(ref dgvData);
                    btnBrowse.Enabled = true;
                    btnSigma.Enabled = true;
                    btnPrintBarcode.Enabled = false;
                    txtInvoiceNo.Text = "";
                    groupBox1.Enabled = true;
                }
            }
        }
        #endregion

        #region User_Defined_Methods

        #region Clear_Content
        /// <summary>
        /// Clears the data from all controls on the form.
        /// </summary>
        private void Clear()
        {
            cmbGRNType.SelectedIndex = -1;

            txtANoticeNo.Text = "";
            txtInvoiceNo.Text = "";
            txtStoNo.Text = "";
            dtANoticeDate.Value = DateTime.Today.Date;
            dtInvoiceDate.Value = DateTime.Today.Date;
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
            var regexItem = new Regex("^[a-zA-Z0-9]*$");

            if (cmbGRNType.Text == "" || cmbGRNType.Text == null)
            {
                ClsMessage.ShowError("Please select the valid GRN Type");
                cmbGRNType.Focus();
            }
            else if (txtANoticeNo.Text.Trim() == "" || txtANoticeNo.Text == null)
            {
                ClsMessage.ShowError("Please enter the A Notice No.");
                txtANoticeNo.Focus();
            }
            else if (!regexItem.IsMatch(txtANoticeNo.Text))
            {
                ClsMessage.ShowError("Please enter the valid A Notice No. it cannot contain special character.");
                cmbGRNType.Focus();
            }
            //else if (txtInvoiceNo.Text.Trim() == "" || txtInvoiceNo.Text == null)
            //{
            //    ClsMessage.ShowError("Please enter the valid Invoice No.");
            //    txtInvoiceNo.Focus();
            //}
            else if (cmbGRNType.Text == "STO")
            {
                if (txtStoNo.Text == "" || txtStoNo.Text == null)
                {
                    ClsMessage.ShowError("Pleae enter the valid STO No.");
                    txtStoNo.Focus();
                }
            }
            else
                IsFormValid = true;

            return IsFormValid;
        }
        #endregion

        #region CRUD_Operations
        /// <summary>
        /// Save current GRN Data to database.
        /// </summary>
        private void SaveGRNData()
        {
            try
            {
                grn_mst = new GRN_MST();
                grn_dtl = new GRN_DTL();
                grn_label = new GRN_LABEL_PRINTING();


                qc_mst = new QC_MST();
                qc_dtl = new QC_DTL();

                //Validate if all fields on form are filled correctly.
                if (ValidateForm())
                {
                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        grn_mst = db.GRN_MST.Where(x => x.A_NOTICE_NO == txtANoticeNo.Text.Trim()).ToList().FirstOrDefault();
                        if (grn_mst != null)
                        {
                            if (optANoticeNo.Checked)
                                ClsMessage.ShowError("This A Notice No is already saved, hence cannot be saved again.");
                            else
                                ClsMessage.ShowError("This Invoice No is already saved, hence cannot be saved again.");
                            btnSave.Enabled = false;
                        }
                        else
                        {
                            //Start transaction to save data in database.
                            using (var DbTransacation = db.Database.BeginTransaction())
                            {
                                try
                                {
                                    //No of barcode to be generated.  Get serial count to book in database to avaid duplicasy in case of multiple users
                                    int lintTotalSerialCount = GetNoOfBarcodeToBeGenerated();

                                    
                                    mintNoOfMixPallete = GetNoOfMixPalletBarcodeToBeGenerated();
                                    //Get quantity of total boxes in a pallet (multiple parts)
                                    if (mintNoOfMixPallete > 0)
                                    { 
                                        GetMixPalletBOXQuantity();
                                    }

                                    //in case of invoice(mixed pallet) a notice no. and invoice no. would be same.
                                    if (optInvoiceNo.Checked)
                                        txtInvoiceNo.Text = txtANoticeNo.Text.Trim();

                                    grn_mst = new GRN_MST();
                                    #region SaveGRN_Master
                                    //Set GRN Mster data to save
                                    grn_mst.SITE_MST_ID = clsCurrentUser.Site_MST_ID;
                                    grn_mst.GRN_TYPE_MST_ID = Convert.ToInt32(cmbGRNType.SelectedValue.ToString());
                                    grn_mst.A_NOTICE_NO = txtANoticeNo.Text.Trim();
                                    grn_mst.A_NOTICE_DATE = dtANoticeDate.Value;
                                    grn_mst.INVOICE_NO = txtInvoiceNo.Text.ToString();
                                    grn_mst.INVOICE_DATE = dtInvoiceDate.Value;
                                    grn_mst.CREATED_BY = Convert.ToInt32(clsCurrentUser.User_MST_ID);
                                    grn_mst.CREATED_DATE = DateTime.Now;
                                    grn_mst.STO_NO = txtStoNo.Text.Trim();
                                    grn_mst.CONTAINER_NO = "";
                                    //add Current grn master object to db context
                                    db.GRN_MST.Add(grn_mst);

                                    //Save new Grn Master data to database. but transaction is not commited yet.
                                    db.SaveChanges();
                                    #endregion

                                    #region Fetch GRN_Master_ID
                                    //Now fetch the GRN_MST_ID (GRN MASTER AUTO ID) generated for this GRN Master record to save in GRN Detail table
                                    grn_mst = db.GRN_MST.Where(x => x.A_NOTICE_NO == grn_mst.A_NOTICE_NO).ToList().FirstOrDefault();
                                    #endregion

                                    #region SaveGRN_Details
                                    //=====================================================================================================
                                    //Save all rows of GRN to GRN Detail Table. 
                                    //=====================================================================================================
                                    //1. Add all row of grid view to list
                                    foreach (DataGridViewRow currRow in dgvData.Rows)
                                    {
                                        //Check id current row of datagrid view is not null. Normally last row is always null.
                                        if (currRow.Cells["Part_No"].Value != null)
                                        {
                                            //IF current row back color is re it means part does not exist in our database and we will not save it in GRN
                                            if (currRow.DefaultCellStyle.BackColor != Color.Red)
                                            {
                                                string lstrPalletBarcode = (string) htPalletSerial[currRow.Cells["PALLETE_NO"].Value.ToString()];
                                                grn_dtl = new GRN_DTL
                                                {
                                                    GRN_MST_ID = grn_mst.GRN_MST_ID,
                                                    PART_NO = common.ReplaceNullString(currRow.Cells["Part_No"].Value.ToString().Trim()),
                                                    PART_NAME = common.ReplaceNullString(currRow.Cells["Part_Name"].Value.ToString()),
                                                    PACK_SIZE = common.ReplaceNullString(currRow.Cells["Pack_Size"].Value.ToString()),
                                                    SUPPLIER_BATCH_NO = common.ReplaceNullString(currRow.Cells["Supplier_Batch_No"].Value),
                                                    QUANTITY = Convert.ToInt32(currRow.Cells["Quantity"].Value.ToString()),
                                                    PALLETE_NO = common.ReplaceNullString(currRow.Cells["PALLETE_NO"].Value),
                                                    STATUS = 0,
                                                    IS_BLOCK = 0,
                                                    PART_TYPE = "Parts",
                                                    PALLETE_NO_BARCODE =lstrPalletBarcode
                                                };

                                                db.GRN_DTL.Add(grn_dtl);
                                                db.SaveChanges();
                                            }
                                        }
                                    }
                                    //=================================================================================================
                                    #endregion

                                    #region Generate Barcode
                                    //=================================================================================================
                                    //For each Part in the GRN generate the Primary Barcode
                                    //=================================================================================================

                                    long lintSerial = common.GenerateBarcodeSerial("GRN", lintTotalSerialCount);

                                    //if (mintNoOfMixPallete > 0)
                                        

                                    foreach (DataGridViewRow currRow in dgvData.Rows)
                                    {
                                        if (currRow.Cells["Part_No"].Value != null)
                                        {
                                            //IF current row back color is re it means part does not exist in our database and we will not save it in GRN
                                            if (currRow.DefaultCellStyle.BackColor != Color.Red)
                                            {
                                                //store values in temp variable since LINQ does not support the DATAGridViewCell.getItem
                                                string lstrTmpPartNo = common.ReplaceNullString(currRow.Cells["Part_No"].Value);
                                                string lstrTmpPackSize = common.ReplaceNullString(currRow.Cells["Pack_Size"].Value);
                                                string lstrPalletNo = common.ReplaceNullString(currRow.Cells["PALLETE_NO"].Value); // created by putta
                                                string lstrTmpSupplierBatch = "";
                                                //may be supplier no. column not exist in excel or erp. hence save only if exists else blank
                                                if (currRow.Cells["Supplier_Batch_No"].Value != null)
                                                    lstrTmpSupplierBatch = common.ReplaceNullString(currRow.Cells["Supplier_Batch_No"].Value);

                                                int lstrTmpQty = Convert.ToInt32(currRow.Cells["QUANTITY"].Value.ToString());

                                                //Get GRN Deatil ID for current line to print the barcode
                                                GRN_DTL dtl = db.GRN_DTL.Where(x => x.GRN_MST_ID == grn_mst.GRN_MST_ID &&
                                                x.PART_NO == lstrTmpPartNo &&
                                                //x.PACK_SIZE == lstrTmpPackSize && //COMMENTED THIS as pack size may vary for same part no in same GRN/A Notice No.
                                                x.SUPPLIER_BATCH_NO == lstrTmpSupplierBatch &&
                                                x.QUANTITY == lstrTmpQty && 
                                                x.PALLETE_NO== lstrPalletNo
                                              ).ToList().FirstOrDefault(); // / modified by putta

                                                #region GENERATE GRN BARCODE
                                                //========================================================================================
                                                //Generate BarCode for each individual packsize
                                                //========================================================================================
                                                //Calculate no. of boxes for current item
                                                int NoOfBox = Convert.ToInt32(currRow.Cells["Quantity"].Value.ToString()) / Convert.ToInt32(currRow.Cells["Pack_Size"].Value.ToString());

                                                //Check if there are any quantity which is not as per open box (for ex: in a standard packsize of 100 we receive 540 qty. then 5 box will be closed
                                                //while one box will be for 40 qty i.e. open box

                                                int lintOpenBox = Convert.ToInt32(currRow.Cells["Quantity"].Value.ToString()) % Convert.ToInt32(currRow.Cells["Pack_Size"].Value.ToString());
                                                if (lintOpenBox > 0)
                                                    NoOfBox += 1;


                                                for (int CurrLabel = 1; CurrLabel <= NoOfBox; CurrLabel++)
                                                {
                                                    string lstrCurrSerial = "";
                                                    string lstrcurrMixPalleteSerial = "";

                                                    if (lintSerial.ToString().Length <= 5)
                                                        lstrCurrSerial = DateTime.Today.ToString("ddMMyy") + String.Format("{0:00000}", lintSerial++);
                                                    else
                                                        lstrCurrSerial = String.Format("{0:00000}", lintSerial++);
                                                    
                                                    //each box will have the quantity as per packsize. but for openbox (if any quantity is open then box quantity must be open quantity
                                                    int lintCurrBoxQuantity = Convert.ToInt32(currRow.Cells["Pack_Size"].Value.ToString());

                                                    if (lintOpenBox > 0)
                                                    {
                                                        //last box will be open box
                                                        if (CurrLabel == NoOfBox)
                                                            lintCurrBoxQuantity = lintOpenBox;
                                                    }

                                                    string lstrCurrBarCode = common.GenerateGRNBarCode(
                                                                            common.ReplaceNullString(currRow.Cells["PART_NO"].Value.ToString().Trim()),
                                                                            txtANoticeNo.Text.Trim(),
                                                                            lintCurrBoxQuantity.ToString(),
                                                                            lstrCurrSerial,
                                                                            clsCurrentUser.Site_Name.ToString());

                                                    string lstrMixPalleteBarCode = "";

                                                    if (common.ReplaceNullString(currRow.Cells["PALLETE_NO"].Value) != "")
                                                        lstrMixPalleteBarCode= (string)htPalletSerial[currRow.Cells["PALLETE_NO"].Value.ToString()];

                                                    grn_label = new GRN_LABEL_PRINTING
                                                    {
                                                        GRN_DTL_ID = dtl.GRN_DTL_ID,
                                                        PRIMARY_BARCODE = lstrCurrBarCode,
                                                        BR_SERIAL = CurrLabel,
                                                        LABEL_TYPE_MST_ID = 1,
                                                        STATUS = 0,
                                                        CREATED_BY = clsCurrentUser.User_MST_ID,
                                                        CREATED_DATE = DateTime.Now,
                                                        TODAY_BARCODE_SERIAL = lstrCurrSerial,
                                                        BOX_QUANTITY = lintCurrBoxQuantity,
                                                        PALLETE_NO_BARCODE = lstrMixPalleteBarCode
                                                    };
                                                    
                                                    db.GRN_LABEL_PRINTING.Add(grn_label);
                                                }//end of FOR LOOP for current label
                                                //lintMixPalleteSerial++;
                                                #endregion
                                            }//end of IF CONDITION for checking if current back row is not red
                                        }//end of IF CONDITION FOR checking that current row(part_no) is not blank
                                    }//end of FOR LOOP for each grid view row

                                    //3. Save changes in GRN Details table
                                    db.SaveChanges();
                                    //=====================================================================================================
                                    #endregion

                                    //commit the transaction as data has been saved successfully in both the tables master and chiled table
                                    //And no error occured during the save operation.
                                    DbTransacation.Commit();
                                    ClsMessage.ShowDataSavedConfirmation();
                                    btnSave.Enabled = false;
                                    btnPrintBarcode.Enabled = true;
                                    dgvData.Columns["chkPrint"].Visible = true;
                                    dgvData.Columns["Block"].Visible = true;
                                    LoadGRNDataInForm(common.GetGRNData_ByNoticeNo(txtANoticeNo.Text.Trim(),"Parts"), false);
                                }//end of inner try block
                                catch (Exception ex)
                                {
                                    //Roll back the transaction as error occurrend dusring save operation.
                                    DbTransacation.Rollback();
                                    ClsMessage.ShowError(ClsMessage.DATA_NOT_SAVE_ERROR_MESSAGE);
                                    common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException  + " in Method: " + MethodBase.GetCurrentMethod());
                                }
                            }
                        }//end of else part of checking if a notice already shaved
                    }//end of using aidenso entities
                }//end of if condition for validate form
            }//end of outer try catch
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException  + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }


        /// <summary>
        /// Loads the GRN_Data into Form controls and grid view after fetching from database
        /// </summary>
        /// <param name="pgrn_data">Data object containing the GRN Data</param>
        private void LoadGRNDataInForm(GRN_DATA pgrn_data, bool pblnshowModifyAlert)
        {
            try
            {
                if (pgrn_data.GRN_SITE != "" && pgrn_data.GRN_SITE != null)
                {
                    dgvData.AutoGenerateColumns = false;
                    txtInvoiceNo.Text = pgrn_data.Grn_Mst.INVOICE_NO.ToString();
                    dtInvoiceDate.Value = DateTime.Parse(pgrn_data.Grn_Mst.INVOICE_DATE.Value.ToString("dd-MMM-yyyy"));
                    dtANoticeDate.Value = DateTime.Parse(pgrn_data.Grn_Mst.A_NOTICE_DATE.Value.ToString("dd-MMM-yyyy"));
                    txtStoNo.Text = common.ReplaceNullString(pgrn_data.Grn_Mst.STO_NO);
                    cmbGRNType.SelectedValue = pgrn_data.Grn_Mst.GRN_TYPE_MST_ID;

                    if (pgrn_data.Grn_Mst.A_NOTICE_NO == pgrn_data.Grn_Mst.INVOICE_NO)
                        optInvoiceNo.Checked = true;
                    else
                        optANoticeNo.Checked = true;


                    dgvData.DataSource = pgrn_data.lstGrn_Dtl;
                    common.MarkBlockedPart(ref dgvData, true);


                    btnSave.Enabled = false;
                    btnPrintBarcode.Enabled = true;

                    if (pblnshowModifyAlert)
                        ClsMessage.ShowInfo("This A Notice No. Already exist, hence cannot be modify.");

                    btnSave.Enabled = false;
                    btnBrowse.Enabled = false;
                    btnSigma.Enabled = false;
                    dgvData.Columns["chkPrint"].Visible = true;
                    dgvData.Columns["Block"].Visible = true;
                }
                else
                {
                    btnSave.Enabled = true;
                    dgvData.Columns["chkPrint"].Visible = false;
                    dgvData.Columns["Block"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException  + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }


        private void LoadGRNData()
        {
            bool blnIfPartNotExist = false;
            try
            {
                //We have already made sure in common function that data table has the data rows. now we need to make mapp the
                //DataTable columns with grid view columns to populate data.if amy column is not populated it means either it do not have
                //data or column name header is not correct in excel file.
                dgvData.AutoGenerateColumns = false;
                cmbGRNType.SelectedItem = mds.Tables[0].Rows[0]["Site_MST_ID"].ToString();

                if (optInvoiceNo.Checked)
                    txtInvoiceNo.Text = txtANoticeNo.Text.Trim();
                else
                    txtInvoiceNo.Text = mds.Tables[0].Rows[0]["INVOICE_NO"].ToString();

                dtANoticeDate.Value = DateTime.Today;
                DateTime dt = new DateTime();

                //bool bl = DateTime.TryParse(mds.Tables[0].Rows[0]["INVOICE_DATE"].ToString(), out dt);
                dt = common.FormatDate(mds.Tables[0].Rows[0]["INVOICE_DATE"].ToString());

                dtInvoiceDate.Value = dt;// DateTime.Parse( mds.Tables[0].Rows[0]["INVOICE_DATE"].ToString());

                dgvData.DataSource = mds.Tables[0];

                //Now verify that all part/material received from excel file or ERP exists in our database as well
                //otherwise mark that row as red in gridview.
                foreach (DataGridViewRow currRow in dgvData.Rows)
                {
                    //mak sure we have data in current row its not blank. it will always be in last row of data grid view

                    if (currRow.Cells["Part_No"].Value != null)
                    {
                        using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                        {
                            string strPart = currRow.Cells["Part_No"].Value.ToString();
                            MATERIAL_MST part = common.CheckIfPartExists(strPart);
                            if (part == null)
                            {
                                currRow.DefaultCellStyle.BackColor = Color.Red;
                                blnIfPartNotExist = true;
                            }
                            //if above color changes in code then change it at save event also as row to save are selected based on back color.
                            //which rows will have backcolor as red will not be saved in database. and their barcode will also not be generated
                        }
                    }
                }
                //if there is any row whose part does not exist in our database then show the message to user.
                if (blnIfPartNotExist)
                    ClsMessage.ShowInfo("Part displayed in RED rows will not be saved as this part doesn't exists in database.");
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException  + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }


        private void PrintBarCodeData()
        {
            try
            {
                if (common.ConfirmToPrintBarCode())
                {
                    grn_label = null;
                    htPalletPrinted.Clear();
                    //=================================================================================================
                    //For each Part in the GRN generate the Primary Barcode
                    //=================================================================================================
                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {

                        List<GRN_LABEL_PRINTING> grnlst;

                        //Print coupon for Selected Row Only
                        foreach (DataGridViewRow currRow in dgvData.Rows)
                        {
                            //Make sure Current row is oot blank as last row may be blank in grid view and user may select it. 
                            if (currRow.Cells["Part_No"].Value != null)
                            {
                                if (currRow.Cells["chkPrint"].Value != null && Boolean.Parse(currRow.Cells["chkPrint"].Value.ToString()) == true)
                                {
                                    //Make sure that barcode label label for all of its box are not printind yet. as we are not giving reprinting option right now.
                                    //Lateron when we will give the option for reprinting then we will disable this condition and will ask user about serial no. of label
                                    //that of which serial he wants the coupon.
                                    if (currRow.Cells["STATUS"].Value.ToString() != "1") //status 1 means that label for all boxes of this part have been printed
                                    {
                                        if (currRow.Cells["IS_BLOCK"].Value.ToString() != "1")
                                        {
                                            //Get the list of Boxes for this part whose label are not printed yet. First we need to get the boxes whose label are remaining for priting.
                                            //although user will also give the quantity that how many boxes should be printed but we need to consider following scenarios
                                            //1. User enter 5 quantity to print out of total 10 boxes and no coupon printed yet. in this case first 5 coupon (1-5) will be printed
                                            //2. User enter 5 quantity but 5 coupons already printed in that case coupon serial 6-10 qill be printed
                                            //3. User enter 5 quantity but 3 coupons already printied in that case 5 coupons from 4-8 will be printed
                                            //4. User enter 5 quantity but 7 coupons already printed in that case only 3 coupon printed 8-10
                                            long lstrCurrGRNDTLID = Convert.ToInt64(currRow.Cells["GRN_DTL_ID"].Value.ToString());

                                            //Get list of all barcode label for current part which are not printed yet
                                            grnlst = db.GRN_LABEL_PRINTING.Where(x => x.GRN_DTL_ID == lstrCurrGRNDTLID && x.STATUS == 0).OrderBy(m => m.BR_SERIAL).ToList();


                                            if (grnlst.Count > 0)
                                            {
                                                int lintRemainingLabelQty = grnlst.Count;
                                                int lintRequestedLabelQty = 0;

                                                if (currRow.Cells["PALLETE_NO"].Value.ToString() == "" || optIndividualPrint.Checked)
                                                {
                                                    int lintLabelToPrint = 0;

                                                    if (currRow.Cells["No_Of_Print"].Value != null)
                                                        lintRequestedLabelQty = common.ReplaceNullNumber(currRow.Cells["No_Of_Print"].Value.ToString());

                                                    //this is case when user did not requested the quantity. OR user Requested More than equal to remaining qty. Means he wants to print all remaining labels
                                                    if (lintRequestedLabelQty == 0 || lintRequestedLabelQty >= lintRemainingLabelQty)
                                                        lintLabelToPrint = lintRemainingLabelQty;
                                                    else if (lintRequestedLabelQty < lintRemainingLabelQty)
                                                        lintLabelToPrint = lintRequestedLabelQty;

                                                    int lintNoOfBox = 1;

                                                    for (int currPart = 0; currPart < lintLabelToPrint; currPart++)
                                                    {
                                                        using (var DbTransacation = db.Database.BeginTransaction())
                                                        {
                                                            try
                                                            {
                                                                lintNoOfBox = Convert.ToInt32(currRow.Cells["Quantity"].Value.ToString()) / Convert.ToInt32(currRow.Cells["Pack_Size"].Value.ToString());

                                                                int lintOpenBox = Convert.ToInt32(currRow.Cells["Quantity"].Value.ToString()) % Convert.ToInt32(currRow.Cells["Pack_Size"].Value.ToString());
                                                                if (lintOpenBox > 0)
                                                                    lintNoOfBox += 1;
                                                                //Printing individual Barcode
                                                                common.PrinGrnBarCode(
                                                                        currRow.Cells["PART_NO"].Value.ToString(),
                                                                        currRow.Cells["PART_NAME"].Value.ToString(),
                                                                        txtInvoiceNo.Text.Trim(),
                                                                        txtANoticeNo.Text.Trim(),
                                                                        grnlst[currPart].BOX_QUANTITY.ToString(),
                                                                        grnlst[currPart].TODAY_BARCODE_SERIAL.ToString(),
                                                                        (grnlst[currPart].BR_SERIAL.ToString() + "/" + lintNoOfBox).ToString(),
                                                                        clsCurrentUser.Site_Name.ToString()
                                                                );

                                                                grnlst[currPart].STATUS = 1;
                                                                grnlst[currPart].PRINT_TYPE = "Individual";
                                                                db.SaveChanges();
                                                                DbTransacation.Commit();
                                                            }//end of inner try block inside using tansaction
                                                            catch (Exception ex)
                                                            {
                                                                DbTransacation.Rollback();
                                                                throw new Exception("Error occured while printing label for Serial " + grnlst[currPart].TODAY_BARCODE_SERIAL);
                                                            }
                                                        }
                                                    }
                                                }//end of checking if pallet no is not blank
                                                else
                                                {
                                                    using (var DbTransacation = db.Database.BeginTransaction())
                                                    {
                                                        try
                                                        {
                                                            string lstrPalletNo = common.ReplaceNullString(currRow.Cells["PALLETE_NO"].Value);
                                                            if (!htPalletPrinted.ContainsKey(lstrPalletNo))
                                                            {
                                                                common.PrintPallet(txtANoticeNo.Text.Trim(), lstrPalletNo);
                                                                htPalletPrinted.Add(lstrPalletNo, "1");

                                                                //Add Pallet No. to Mixed STO Pallet Table for STO Purpose
                                                                #region Add_Mix_Pallet_STO_Data
                                                                MIXED_PALLET_STO mix_pallet_sto = new MIXED_PALLET_STO();
                                                                mix_pallet_sto.MIXED_PALLET_NO = lstrPalletNo;
                                                                mix_pallet_sto.GRN_DTL_ID = Convert.ToInt32(common.ReplaceNullString(currRow.Cells["GRN_DTL_ID"].Value));
                                                                mix_pallet_sto.GRN_PRINTING_TIME = DateTime.Now;
                                                                db.MIXED_PALLET_STO.Add(mix_pallet_sto);
                                                                #endregion
                                                            }

                                                            long grn_dtl_id = Convert.ToInt64(currRow.Cells["GRN_DTL_ID"].Value.ToString());

                                                            //Update Print Status and Print type as "Mix Pallet" of for all boxes for current part
                                                            List<GRN_LABEL_PRINTING> lstGrn_Lbl_Print = db.GRN_LABEL_PRINTING.Where(x => x.GRN_DTL_ID == grn_dtl_id).ToList();
                                                            foreach (GRN_LABEL_PRINTING currLabel in lstGrn_Lbl_Print)
                                                            {
                                                                currLabel.STATUS = 1;
                                                                currLabel.PRINT_TYPE = "Mix Pallet";
                                                            }
                                                            db.SaveChanges();
                                                            DbTransacation.Commit();
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            DbTransacation.Rollback();
                                                            throw new Exception("Error occurred while printing mix pallet #: " + currRow.Cells["PALLETE_NO"].Value);
                                                        }
                                                    }
                                                }
                                                #region Update Status of GRN_DTL
                                                //We need to check if all lbel of current part have been printed then status has to be change in GRN_DTL Table also
                                                //if requested qty is >= remaining quantity  it means all label of current part have been printed.
                                                if (lintRequestedLabelQty >= lintRemainingLabelQty || lintRequestedLabelQty == 0)
                                                {
                                                    long currGrnDTLID = Convert.ToInt64(currRow.Cells["GRN_DTL_ID"].Value.ToString());
                                                    grn_dtl = db.GRN_DTL.Where(g => g.GRN_DTL_ID == currGrnDTLID).ToList().FirstOrDefault();
                                                    grn_dtl.STATUS = 1;
                                                    currRow.Cells["Status"].Value = "1";
                                                }
                                                #endregion
                                                db.SaveChanges();
                                            }//End of cndition check if current grnlbl list has any lable for printing.
                                        }// end of checking if part is Not blocked
                                    }//end of condition STATUS check condition
                                } //End of condition to check if current row is selected or not
                            }////end of condition check for current row is not blank for part no.
                        }//end of for loop of each row in data grid view
                        db.SaveChanges();
                        ClsMessage.ShowInfo("Requested barcode labels have been printied successfully.");
                        LoadGRNDataInForm(common.GetGRNData_ByNoticeNo(txtANoticeNo.Text.Trim(), "Parts"), false);
                    }//end of using db transaction
                }//End of using db entities
            } // end of main try block
            catch (Exception ex)
            {
                ClsMessage.ShowError("Error occurred while printing label. Please contact your application administrator.");
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }

        
        //private string GetCurrentPartPrintType(DataGridViewRow pCurrRow)
        //{
        //    string lstrPrintType = "Mix Pallet";
        //    try
        //    {
        //        if (common.ReplaceNullString(pCurrRow.Cells["PALLETE_NO"].Value) == "" || optIndividualPrint.Checked)
        //            lstrPrintType = "Individual";
        //    }
        //    catch (Exception ex)
        //    {
        //        common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + " in Method: " + MethodBase.GetCurrentMethod());
        //        throw new Exception("Error in printing mixed Pallet:" + ex.Message);
        //    }
        //    return lstrPrintType;
        //}
        #endregion

        #endregion

        #region Main_Form_Operations
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (common.ConfirmToExit())
                this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveGRNData();
        }


        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateForm())
                {
                    //DataSet md= cOdbc.ExecOdbcQry("");
                    mds = common.GetExcelData(ref ofd);
                    if (mds != null)
                    {
                        LoadGRNData();
                        btnSave.Enabled = true;
                        btnBrowse.Enabled = false;
                        btnSigma.Enabled = false;
                    }
                    else
                    {
                        btnBrowse.Enabled = true;
                        btnSigma.Enabled = true;
                        btnSave.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException  + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }
        private void btnPrintBarcode_Click(object sender, EventArgs e)
        {
            //  if (optANoticeNo.Checked)
            // {
            if (common.IsAnyRowSelected(dgvData,"chkPrint"))
            {
                //when user has selected individual label printing then he cannot print multiple mix pallets at a time
                //hence need to check if individual label type is selected then user should not be able to print multiple mixed pallets
                if (optIndividualPrint.Checked && CheckIfMultipleMixPalletSelected())
                    ClsMessage.ShowError("Multiple mixed pallet cannot be printed for individual label type. Either select label print type as mixed pallet or print one mixed pallet label at a time.");
                else
                    PrintBarCodeData();
            }
            else
                ClsMessage.ShowError("Please select at least one part to print the barcode.");
            // }
            // else
            // { }
        }
        #endregion

        private void dgvData_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvData.Columns[e.ColumnIndex].Name.ToUpper() == "NO_OF_PRINT")
            //{
            //    dgvData.BeginEdit(false);
            //}
        }

        private void btnSigma_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateForm())
                {
                    //DataSet md= cOdbc.ExecOdbcQry("");

                    if (ConfigurationSettings.AppSettings["AppEnvironment"].ToString().ToUpper() == "DEV")
                        cOdbc = new clsODBC(common.gstrSigmaConStringLocal);
                    else
                        cOdbc = new clsODBC(common.gstrSigmaConString);

                    string lstrQry = "";

                    if (optANoticeNo.Checked)
                    {
                        //Non-DNJP Concept
                        lstrQry = "Select  FTEIFG  from C809EF9W.KIRESLIB.bm401pr bm401pr where FTSUPC=(select distinct ANSUPC " +
                                  "from C809EF9W.KIRESLIB.bd423pr bd423pr where ANNNBR='" + txtANoticeNo.Text.Trim() + "')"; // get sepplier code

                        if (ConfigurationSettings.AppSettings["AppEnvironment"].ToString().ToUpper() == "DEV")
                            lstrQry = lstrQry.ToUpper().Replace("C809EF9W.KIRESLIB.", "");

                        mds = cOdbc.ExecOdbcQry(lstrQry, cOdbc.odbcConnection);

                        if (mds != null)
                        {
                            if (mds.Tables[0].Rows.Count > 0)
                            {
                                if (mds.Tables[0].Rows[0]["FTEIFG"].ToString() == "2") //Export details with pallet (2 is export supplier)
                                {
                                    lstrQry = "SELECT bd275pr.CHPLNT as plant,'" + txtANoticeNo.Text.Trim() + "' AS A_Notice_No,bd275pr.CHCNDT as INVOICE_DATE," +
                                              "bd275pr.CHCNNO as Container, bd275pr.CHIVNO as INVOICE_NO, bd275pr.CHPRT1 as PART_NO,bd275pr.CHORNO as OrderNo," +
                                              "bd275pr.CHBXNO as PALLETE_NO,bd275pr.CHPKQY as QTY_PER_BOX,bd275pr.CHNOPK as NO_OF_BOX,bd275pr.CHSHQY as ShippedQty," +
                                              "bd275pr.CHRVQY as QUANTITY,bd275pr.CHUOFM as UOM FROM C809EF9W.KIRESLIB.bd275pr bd275pr " +
                                              "Where bd275pr.CHCNNO in (SELECT ANCNNO FROM C809EF9W.KIRESLIB.bd423pr bd423pr WHERE bd423pr.ANNNBR = '" +
                                              txtANoticeNo.Text.Trim() + "')";

                                    //lstrQry = "SELECT Distinct bd275pr.CHPLNT as plant, bd423pr.ANNNBR AS A_Notice_No,bd423pr.ANRCDT as INVOICE_DATE,bd275pr.CHCNNO as Container," +
                                    //          "bd275pr.CHIVNO as INVOICE_NO, bd275pr.CHPRT1 as PART_NO,bd275pr.CHORNO as OrderNo,bd275pr.CHBXNO as PALLETE_NO," +
                                    //          "bd275pr.CHPKQY as QTY_PER_BOX,bd275pr.CHNOPK as NO_OF_BOX,bd275pr.CHSHQY as ShippedQty,bd275pr.CHRVQY as QUANTITY," +
                                    //          "bd275pr.CHUOFM as UOM FROM C809EF9W.KIRESLIB.bd275pr bd275pr inner join " +
                                    //          "C809EF9W.KIRESLIB.bd423pr bd423pr on bd275pr.CHCNNO = bd423pr.ANCNNO WHERE bd423pr.ANNNBR = '" + txtANoticeNo.Text.Trim() + "' ";
                                }
                                else if (mds.Tables[0].Rows[0]["FTEIFG"].ToString() == "1") // dometic Details without pallet (1 is domestic Supplier)
                                {
                                    lstrQry = "select bd423pr.ANPLNT as plant, bd423pr.ANNNBR AS A_Notice_No, bd423pr.ANRCDT as INVOICE_DATE, bd423pr.ANCNNO as Container," +
                                              "bd423pr.ANRFNO as INVOICE_NO, bd423pr.ANPRTN as PART_NO, '' as OrderNo,'' as PALLETE_NO, " +
                                              "'0' as QTY_PER_BOX,'0' as NO_OF_BOX,'0' as ShippedQty,bd423pr.ANRCQY as QUANTITY,bd423pr.ANPUOM as UOM " +
                                              "from C809EF9W.KIRESLIB.bd423pr bd423pr where ANNNBR='" + txtANoticeNo.Text.Trim() + "'";
                                }
                            }
                        }
                        //else
                        //    ClsMessage.ShowError("Could not found data in cigma for current A Notice No.");
                        //lstrQry = "SELECT BD423PR.ANNNBR AS A_NOTICE_NO, BD423PR.ANSUPC AS SUPPLIER_BATCH_NO, BD423PR.ANRCDT AS INVOICE_DATE, BD423PR.ANRCQY AS QUANTITY," +
                        //        "BD423PR.ANRFNO AS INVOICE_NO,BD423PR.ANPRTN AS PART_NO FROM C809EF9W.KIRESLIB.BD423PR BD423PR WHERE BD423PR.ANNNBR = '" + txtANoticeNo.Text.Trim() + "'";
                    }
                    else
                    {    //DNJP Concept
                        lstrQry = "SELECT BD223PR.CHPLNT,BD223PR.CHCNNO,BD223PR.CHIVNO AS INVOICE_NO,BD223PR.CHPRT1 AS PART_NO,BD223PR.CHBXNO AS PALLETE_NO," +
                                  "BD223PR.CHPKQY AS QTY_PER_BOX,BD223PR.CHNOPK AS NO_OF_BOX,BD223PR.CHSHQY AS QUANTITY,BD223PR.CHDPDT AS INVOICE_DATE " +
                                  "FROM BD223PR BD223PR Where BD223PR.CHIVNO = '" + txtANoticeNo.Text.Trim() + "'";
                    }
                    if (ConfigurationSettings.AppSettings["AppEnvironment"].ToString().ToUpper() == "DEV")
                        lstrQry = lstrQry.ToUpper().Replace("C809EF9W.KIRESLIB.", "");

                    mds = cOdbc.ExecOdbcQry(lstrQry, cOdbc.odbcConnection);

                    if (mds != null)
                    {
                        if (mds.Tables.Count > 0)
                        {
                            //In case of SIGMA all columns will not be generated from A Sigma, hence we will need to add them
                            //SITE_MST_ID
                            DataColumn dc = new DataColumn("SITE_MST_ID", typeof(System.String));
                            dc.DefaultValue = clsCurrentUser.Site_MST_ID;
                            mds.Tables[0].Columns.Add(dc);

                            //GRN_TYPE_MST_ID
                            //SITE_MST_ID
                            dc = new DataColumn("GRN_TYPE_MST_ID", typeof(System.String));
                            dc.DefaultValue = cmbGRNType.Text;
                            mds.Tables[0].Columns.Add(dc);


                            //PART NAME
                            mds.Tables[0].Columns.Add("PART_NAME");
                            mds.Tables[0].Columns.Add("PACK_SIZE", typeof(System.Int32));

                            if (!mds.Tables[0].Columns.Contains("UOM"))
                                mds.Tables[0].Columns.Add("UOM", typeof(System.String));

                            mds.Tables[0].Columns.Add("BAG_QTY", typeof(System.Int32));
                            mds.Tables[0].Columns.Add("CAGE_QTY", typeof(System.Int32));

                            foreach (DataRow currRow in mds.Tables[0].Rows)
                            {
                                string lstrPartNo = currRow["Part_No"].ToString();
                                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                                {
                                    MATERIAL_MST part_item = db.MATERIAL_MST.Where(x => x.PART_NO == lstrPartNo).ToList().FirstOrDefault();
                                    if (part_item != null)
                                    {
                                        currRow["PART_NAME"] = part_item.PART_NAME.ToString();
                                        currRow["UOM"] = part_item.UOM_MST.UOM_NAME.ToString();
                                        currRow["BAG_QTY"] = Convert.ToInt32(common.ReplaceNullNumber(part_item.BAG_QTY.ToString()));
                                        currRow["CAGE_QTY"] = Convert.ToInt32(common.ReplaceNullNumber(part_item.CAGE_QTY.ToString()));

                                        if (currRow["UOM"].ToString().ToUpper() == "KG" || currRow["UOM"].ToString().ToUpper() == "KILO GRAM")
                                        {
                                            if (Convert.ToInt32(currRow["BAG_QTY"].ToString()) > 0)
                                            {
                                                currRow["PACK_SIZE"] = Convert.ToInt32(currRow["CAGE_QTY"].ToString()) / Convert.ToInt32(currRow["BAG_QTY"].ToString());
                                                currRow["QUANTITY"] = Convert.ToInt32(currRow["QUANTITY"].ToString()) / Convert.ToInt32(currRow["BAG_QTY"].ToString());
                                            }
                                        }
                                        else
                                            currRow["PACK_SIZE"] = Convert.ToInt32(part_item.PACK_SIZE.ToString());
                                    }
                                }
                            }
                            LoadGRNData();
                            btnSave.Enabled = true;
                            //btnSigma.Enabled = false;
                        }
                        else
                        {
                            ClsMessage.ShowError("No record found for current A Notice No./Invoice No.");
                            //common.ClearDataGridViewRows(ref dgvData);
                            btnSave.Enabled = false;
                        }
                    }
                    else
                    {
                        ClsMessage.ShowError("No record found for current A Notice No./Invoice No.");
                        //common.ClearDataGridViewRows(ref dgvData);
                        btnSave.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ClsMessage.ShowError("Could not fetch the data from sigma. Error occurred. Kindly contact your application administrator.");
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException  + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (dgvData.CurrentRow.Cells["Part_No"].Value != null)
                    {
                        string lstrPartNo = dgvData.CurrentRow.Cells["Part_No"].Value.ToString();
                        if (dgvData.Columns[dgvData.CurrentCell.ColumnIndex].Name.ToUpper() == "BLOCK")
                        {
                            if (dgvData.CurrentRow.Cells["Is_Block"].Value.ToString() != "1")
                            {
                                if (ClsMessage.ShowConfimation("Are you sure to block this part from this A Notice No.") == DialogResult.Yes)
                                {
                                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                                    {
                                        var query = from a in db.GRN_MST
                                                    join b in db.GRN_DTL on a.GRN_MST_ID equals b.GRN_MST_ID
                                                    join c in db.GRN_LABEL_PRINTING on b.GRN_DTL_ID equals c.GRN_DTL_ID
                                                    where a.A_NOTICE_NO == txtANoticeNo.Text.Trim() && b.PART_NO == lstrPartNo && c.STATUS == 1
                                                    select new
                                                    {
                                                        GRN_MST_ID = a.GRN_MST_ID,
                                                        GRN_DTL_ID = b.GRN_DTL_ID
                                                    };

                                        var result = query.ToList();

                                        if (result != null)
                                        {
                                            if (result.Count == 0)
                                            {
                                                var query1 = from a in db.GRN_MST
                                                             join b in db.GRN_DTL on a.GRN_MST_ID equals b.GRN_MST_ID
                                                             where a.A_NOTICE_NO == txtANoticeNo.Text.Trim() && b.PART_NO == lstrPartNo
                                                             select new
                                                             {
                                                                 GRN_MST_ID = a.GRN_MST_ID,
                                                                 GRN_DTL_ID = b.GRN_DTL_ID
                                                             };

                                                var res1 = query1.ToList();
                                                if (res1 != null)
                                                {
                                                    long grn_dtl_id = res1[0].GRN_DTL_ID;
                                                    GRN_DTL tmp_grn_dtl = db.GRN_DTL.Where(x => x.GRN_DTL_ID == grn_dtl_id).ToList().FirstOrDefault();
                                                    tmp_grn_dtl.IS_BLOCK = 1;
                                                    db.SaveChanges();
                                                    dgvData.CurrentRow.Cells["Is_Block"].Value = "1";
                                                    dgvData.CurrentRow.DefaultCellStyle.BackColor = Color.Orange;
                                                    ClsMessage.ShowInfo("Selected part has been blocked successfully.");
                                                }
                                            }
                                            else
                                                ClsMessage.ShowError("Barcode label for this product is already printed, hence cannot be blocked now");
                                        }
                                        else
                                            ClsMessage.ShowError("Barcode label for this product is already printed, hence cannot be blocked now");
                                    }
                                }
                            }
                            else
                            {
                                if (ClsMessage.ShowConfimation("Are you sure to unblock this part from this A Notice No.") == DialogResult.Yes)
                                {
                                    //IF PART IS BLOCK THEN UNBLOCK IT (ONLY IF qc FOR ANY PART/BOX OF THIS grn IS NOT DONE)
                                    //CHECK IF QC IS DONE FOR ANY OF THIS grn'S PART/BOX
                                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                                    {
                                        var qry = from a in db.QC_MST
                                                  join b in db.QC_DTL on a.QC_MST_ID equals b.QC_MST_ID
                                                  where a.A_NOTICE_NO == txtANoticeNo.Text.Trim() && b.STATUS >= 1
                                                  select new
                                                  {
                                                      QC_MST_ID = a.QC_MST_ID
                                                  };

                                        var result = qry.ToList();

                                        if (result.Count > 0)
                                        {
                                            ClsMessage.ShowError("QC for any part/box of this GRN is done hence, cannot unblock this part.");
                                        }
                                        else
                                        {
                                            long grn_dtl_id = Convert.ToInt64(dgvData.CurrentRow.Cells["GRN_DTL_ID"].Value.ToString());
                                            GRN_DTL tmp_grn_dtl = db.GRN_DTL.Where(x => x.GRN_DTL_ID == grn_dtl_id).ToList().FirstOrDefault();
                                            tmp_grn_dtl.IS_BLOCK = 0;
                                            db.SaveChanges();
                                            dgvData.CurrentRow.Cells["Is_Block"].Value = "0";

                                            //set backcolor of datagrid view to normal
                                            dgvData.CurrentRow.DefaultCellStyle.BackColor = Color.White;
                                            ClsMessage.ShowInfo("Selected part has been unblocked successfully.");
                                        }
                                    }
                                }
                            }
                        }
                        else if (dgvData.Columns[dgvData.CurrentCell.ColumnIndex].Name.ToUpper() == "CHKPRINT")
                        {
                            if (dgvData.CurrentRow.Cells["IS_BLOCK"].Value != null && dgvData.CurrentRow.Cells["IS_BLOCK"].Value.ToString() == "1")
                            {
                                ClsMessage.ShowError("This part is blocked hence cannot Print the Label");
                                dgvData.CurrentCell.Value = false;
                            }
                            else if (dgvData.CurrentRow.DefaultCellStyle.BackColor == Color.Yellow)
                            {
                                ClsMessage.ShowError("There is no label remaining for this part to be printed.");
                                dgvData.CurrentCell.Value = false;
                            }
                            else
                            {
                                //Check if current Part is a Mixed Pallet or not. if Yes then Select/Deselect all parts belonging to current mix Pallet
                                if (common.ReplaceNullString(dgvData.CurrentRow.Cells["PALLETE_NO"].Value) != "")
                                    SelectDeselectAllPartOfCurrentMixPallet(common.ReplaceNullString(dgvData.CurrentRow.Cells["PALLETE_NO"].Value.ToString()), Convert.ToBoolean(dgvData.CurrentCell.Value),dgvData.CurrentRow.Index);

                                

                                //dgvData.CurrentCell.Value = !Convert.ToBoolean(dgvData.CurrentCell.Value); 


                                if (Convert.ToBoolean(dgvData.CurrentCell.Value))
                                {
                                    //print checkbox is deselcted for current row
                                    dgvData.CurrentCell.Value = false;
                                    dgvData.CurrentRow.Cells["No_Of_Print"].Value = "";
                                }
                                else
                                {
                                    dgvData.CurrentCell.Value = true;
                                    
                                    //if any multipartMixPallet is slected then print label option should be selected as mixed pallet
                                    if (common.CheckIfMultiPartMixPalletSelected(dgvData))
                                        optMixedPalletPrint.Checked = true;

                                    int lintLabelRemainingToPrint = 0;

                                    //Get No. of label remaining for printing
                                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                                    {
                                        var query = from a in db.GRN_MST
                                                    join b in db.GRN_DTL on a.GRN_MST_ID equals b.GRN_MST_ID
                                                    join c in db.GRN_LABEL_PRINTING on b.GRN_DTL_ID equals c.GRN_DTL_ID
                                                    where a.A_NOTICE_NO == txtANoticeNo.Text.Trim() && b.PART_NO == lstrPartNo && c.STATUS == 0
                                                    select new
                                                    {
                                                        GRN_MST_ID = a.GRN_MST_ID,
                                                        GRN_DTL_ID = b.GRN_DTL_ID
                                                    };

                                        var result = query.ToList();

                                        if (result != null)
                                            lintLabelRemainingToPrint = result.Count;
                                    }


                                    string strNoOfLabelToPrint = "";

                                    //in case of mix pallet (external warehouse only), since mixpallet priting will be on A4 hence wont ask for no. of label to print
                                    if (common.ReplaceNullString(dgvData.CurrentRow.Cells["PALLETE_NO"].Value) == "" || cmbGRNType.Text == "STO")
                                        strNoOfLabelToPrint = Interaction.InputBox("Please enter the no. of Label you want to print for this part. Total Remaining label for printing : " + lintLabelRemainingToPrint, "GRN Label Printing");


                                    if (common.IsNumeric(strNoOfLabelToPrint))
                                    {

                                        //check if user enter qty of ;abel more than remaining then asked him to enter not more than remaining labels
                                        if (lintLabelRemainingToPrint < Convert.ToInt32(strNoOfLabelToPrint))
                                        {
                                            ClsMessage.ShowError("No. of label to print cannot exceed the remaining labels.");
                                            dgvData.CurrentRow.Cells["No_Of_Print"].Value = "";
                                            dgvData.CurrentCell.Value = false;
                                        }
                                        else
                                            dgvData.CurrentRow.Cells["No_Of_Print"].Value = strNoOfLabelToPrint;
                                    }
                                    else
                                    {
                                        if (strNoOfLabelToPrint != "")
                                        {
                                            ClsMessage.ShowError("Please enter the valid no. of label to print.");
                                            dgvData.CurrentCell.Value = false;
                                            dgvData.CurrentRow.Cells["No_Of_Print"].Value = "";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException  + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }


        /// <summary>
        /// Checks if any part belonging to Mix Pallet is selected or not.
        /// </summary>
        /// <returns></returns>
        private bool CheckIfMultipleMixPalletSelected()
        {
            bool isSelected = false;
            try
            {
                Hashtable htSelectedPallet = new Hashtable();
                foreach (DataGridViewRow currRow in dgvData.Rows)
                {
                    if (Boolean.Parse(common.ReplaceNullString(currRow.Cells["chkPRINT"].Value)) == true)
                    {
                        if (common.ReplaceNullString(currRow.Cells["PALLETE_NO"].Value) != "")
                        {
                            if (!htSelectedPallet.ContainsKey(common.ReplaceNullString(currRow.Cells["PALLETE_NO"].Value)))
                            {
                                htSelectedPallet.Add(common.ReplaceNullString(currRow.Cells["PALLETE_NO"].Value), "1");
                                if (htSelectedPallet.Keys.Count > 1)
                                {
                                    isSelected = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException  + " in Method: " + MethodBase.GetCurrentMethod());
            }
            return isSelected;
        }


        /// <summary>
        /// Select/Deselect all part of current mixed pallet
        /// </summary>
        private void SelectDeselectAllPartOfCurrentMixPallet(string pstrPalletNo,bool isSelect,int currRowIndex)
        {
            try
            {
                foreach (DataGridViewRow currRow in dgvData.Rows)
                {
                    if (common.ReplaceNullString(currRow.Cells["PALLETE_NO"].Value) == pstrPalletNo)
                    {
                        ///blocked part or any part whose label have been printed wont be selected 
                        if (currRow.DefaultCellStyle.BackColor != Color.Orange && currRow.DefaultCellStyle.BackColor != Color.Yellow)
                        {
                            //not changing the value of current row as it will be changed in next step (as a common process)
                            if (currRow.Index != currRowIndex)
                                currRow.Cells["chkPrint"].Value = !isSelect; //common.GetAccessValue(!isSelect);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException  + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }

        /// <summary>
        /// Calculates the total number of barcode to be generated for this GRN, so that we can book the barcode serial in database to avoid duplicasy
        /// </summary>
        /// <returns>No. of barcode to be generated</returns>
        private int GetNoOfBarcodeToBeGenerated()
        {
            int lintTotalBarcode = 0;
            try
            {
                foreach(DataGridViewRow currRow in dgvData.Rows)
                {
                    if(currRow.Cells["Part_No"].Value != null && currRow.DefaultCellStyle.BackColor != Color.Red)
                    {
                        //calculate no. of closed box for this item
                        lintTotalBarcode += Convert.ToInt32(currRow.Cells["Quantity"].Value.ToString()) / Convert.ToInt32(currRow.Cells["Pack_Size"].Value.ToString());

                        //check if there any box which is not closed (where quantity is not as per packsize (fox ex. in pack size of 100 qty is 990 then there will be 10 boxes total)
                        if (Convert.ToInt32(currRow.Cells["Quantity"].Value.ToString()) % Convert.ToInt32(currRow.Cells["Pack_Size"].Value.ToString()) > 0)
                        {
                            lintTotalBarcode += 1;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException  + " in Method: " + MethodBase.GetCurrentMethod());
            }
            return lintTotalBarcode;
        }


        /// <summary>
        /// Gets total number of mix pallete in any GRN
        /// </summary>
        /// <returns></returns>
        private int GetNoOfMixPalletBarcodeToBeGenerated()
        {
            int lintTotalMixPalleteBarcode = 0;
            try
            {
                if(mds!=null)
                {
                    if(mds.Tables.Count> 0)
                    {
                        if (mds.Tables[0].Rows.Count>0)
                        {
                            DataTable dtCount = mds.Tables[0].DefaultView.ToTable(true, "PALLETE_NO");
                            if(dtCount!= null)
                            {
                                lintTotalMixPalleteBarcode= dtCount.Select("PALLETE_NO <> ''").Count();
                                //lintTotalMixPalleteBarcode = dtCount.Rows.Count;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException  + " in Method: " + MethodBase.GetCurrentMethod());
            }
            return lintTotalMixPalleteBarcode;
        }

        private int GetMixPalletBOXQuantity()
        {
            int lintTotalMixPalleteBarcode = 0;
            string lstrMixPalleteBarCode = "";
            string lstrcurrMixPalleteSerial = "";
            long lintMixPalleteSerial = 1;

            try
            {
                if (mds != null)
                {
                    if (mds.Tables.Count > 0)
                    {
                        if (mds.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtCount = mds.Tables[0].DefaultView.ToTable(true, "PALLETE_NO").Select("PALLETE_NO <> ''").CopyToDataTable();
                            if (dtCount != null)
                            {
                                lintMixPalleteSerial = common.GenerateBarcodeSerial("Mix Pallete", mintNoOfMixPallete);

                                foreach (DataRow currDtRow in dtCount.Rows)
                                {
                                    //here we will not compute the no. of box as per pack size but we will considre no. of actual boxes in pallete as per data from sigma
                                    object lintBOXCount =  mds.Tables[0].Compute("Sum(NO_OF_BOX)", "PALLETE_NO = '" + currDtRow["PALLETE_NO"].ToString() + "'");
                                    htMixPalletBoxQuantity.Add(currDtRow["PALLETE_NO"].ToString(), lintBOXCount);

                                    if (lintMixPalleteSerial.ToString().Length <= 5)
                                        lstrcurrMixPalleteSerial = DateTime.Today.ToString("ddMMyy") + String.Format("{0:00000}", lintMixPalleteSerial++);
                                    else
                                        lstrcurrMixPalleteSerial = String.Format("{0:00000}", lintMixPalleteSerial++);

                                    lstrMixPalleteBarCode = common.GenerateGRNBarCode(
                                                                                common.ReplaceNullString(currDtRow["PALLETE_NO"].ToString().Trim()),
                                                                                txtANoticeNo.Text.Trim(),
                                                                                htMixPalletBoxQuantity[currDtRow["PALLETE_NO"].ToString()].ToString(),
                                                                                lstrcurrMixPalleteSerial,
                                                                                clsCurrentUser.Site_Name.ToString());

                                    htPalletSerial.Add(currDtRow["PALLETE_NO"].ToString(), lstrMixPalleteBarCode);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException  + " in Method: " + MethodBase.GetCurrentMethod());
            }
            return lintTotalMixPalleteBarcode;
        }

        private void optInvoiceNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.optInvoiceNo.Checked)
            {
                lblMaterialSource.Text = "Invoice No.";
                txtInvoiceNo.Text = txtANoticeNo.Text.Trim();
            }
        }

        private void optANoticeNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.optANoticeNo.Checked)
                lblMaterialSource.Text = "A Notice No.";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void optIndividualPrint_CheckedChanged(object sender, EventArgs e)
        {
            if(common.CheckIfMultiPartMixPalletSelected(dgvData))
                optMixedPalletPrint.Checked = true;
        }
    }
}
