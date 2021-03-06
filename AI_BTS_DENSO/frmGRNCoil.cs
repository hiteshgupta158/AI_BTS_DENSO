﻿using System;
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
using System.Configuration;

namespace AI_BTS_DENSO
{
    public partial class frmGRNCoil : Form
    {

        //create  variable which will be used to track current row in Data Grid View
        int lintCurrentRowIndex = -1;
        OdbcConnection lOdbcCon;
        DataSet mds;
        GRN_DATA grn_data = new GRN_DATA();
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
        public frmGRNCoil()
        {
            InitializeComponent();
        }

        private void frmGRNCoil_Load(object sender, EventArgs e)
        {
            try
            {
                common.GetUserAccessForCurrentScreen(this, "GRN Coil");
                common.AddHeaderCheckBoxToDataGrid(ref dgvData);

                //common.LoadFormColors(this);
                #region Fill DropDownLists
                //Fill GRN Type Combo boxes
                common.FillGRNType(ref cmbGRNType,clsCurrentUser.Site_Name);
                #endregion
                //Clear the controls on form
                Clear();
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException  + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }

        #region SetControlFocus
        private void cmbGRNType_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtInvoiceNo_KeyPress(object sender, KeyPressEventArgs e)
        {

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
        private bool ValidateForm(bool checkPalletInfo=false)
        {
            bool IsFormValid = false;
            var regexItem = new Regex("^[a-zA-Z0-9]*$");
            try
            {
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
                else if (cmbGRNType.Text == "STO")
                {
                    if (txtStoNo.Text == "" || txtStoNo.Text == null)
                    {
                        ClsMessage.ShowError("Pleae enter the valid STO No.");
                        txtStoNo.Focus();
                    }
                }
                else if (!checkIfAllPalletInfoSaved() && checkPalletInfo)
                {
                    ClsMessage.ShowError("Please enter the Pallet Info for all the Parts first.");
                }
                else
                    IsFormValid = true;
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException  + " in Method: " + MethodBase.GetCurrentMethod());
            }
            return IsFormValid;
        }


        private bool checkIfAllPalletInfoSaved()
        {
            bool isFilled = true;
            try
            {
                foreach(DataGridViewRow currRow in dgvData.Rows)
                {
                    if (common.ReplaceNullString(currRow.Cells["PACK_SIZE"].Value) == "")
                    {
                        isFilled = false;
                        break;
                    }
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException  + " in Method: " + MethodBase.GetCurrentMethod());
            }
            return isFilled;
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
                if (ValidateForm(true))
                {
                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        grn_mst = db.GRN_MST.Where(x => x.A_NOTICE_NO == txtANoticeNo.Text.Trim()).ToList().FirstOrDefault();
                        if (grn_mst != null)
                        {
                            ClsMessage.ShowError("This A Notice No is already saved, hence cannot be saved again.");
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
                                                grn_dtl = new GRN_DTL
                                                {
                                                    GRN_MST_ID = grn_mst.GRN_MST_ID,
                                                    PART_NO = common.ReplaceNullString(currRow.Cells["Part_No"].Value.ToString().Trim()),
                                                    PART_NAME = common.ReplaceNullString(currRow.Cells["Part_Name"].Value.ToString()),
                                                    PACK_SIZE = common.ReplaceNullString(currRow.Cells["Pack_Size"].Value.ToString()),
                                                    QUANTITY = Convert.ToInt32(currRow.Cells["Quantity"].Value.ToString()),
                                                    PALLET_SIZE = currRow.Cells["PALLET_SIZE"].Value.ToString(),
                                                    STATUS = 0,
                                                    IS_BLOCK = 0,
                                                    PART_TYPE = "Coil"
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

                                    foreach (DataGridViewRow currRow in dgvData.Rows)
                                    {
                                        if (currRow.Cells["Part_No"].Value != null)
                                        {
                                            //IF current row back color is red it means part does not exist in our database and we will not save it in GRN
                                            if (currRow.DefaultCellStyle.BackColor != Color.Red)
                                            {
                                                //store values in temp variable since LINQ does not support the DATAGridViewCell.getItem
                                                string lstrTmpPartNo = common.ReplaceNullString(currRow.Cells["Part_No"].Value);
                                                string lstrTmpPackSize = common.ReplaceNullString(currRow.Cells["Pack_Size"].Value);

                                                int lstrTmpQty = Convert.ToInt32(currRow.Cells["QUANTITY"].Value.ToString());

                                                //Get GRN Deatil ID for current line to print the barcode
                                                GRN_DTL dtl = db.GRN_DTL.Where(x => x.GRN_MST_ID == grn_mst.GRN_MST_ID &&
                                                x.PART_NO == lstrTmpPartNo &&
                                                x.QUANTITY == lstrTmpQty
                                              ).ToList().FirstOrDefault();

                                                #region GENERATE GRN BARCODE
                                                //========================================================================================
                                                //Generate BarCode for each individual packsize
                                                //========================================================================================
                                                //Calculate no. of boxes for current item
                                                int NoOfBox =  Convert.ToInt16(currRow.Cells["Pack_Size"].Value.ToString());

                                                for (int CurrLabel = 1; CurrLabel <= NoOfBox; CurrLabel++)
                                                {
                                                    string lstrCurrSerial = "";
                                                    string PalletSize = currRow.Cells["Pallet_Size"].Value.ToString(); ;

                                                    if (lintSerial.ToString().Length <= 5)
                                                        lstrCurrSerial = DateTime.Today.ToString("ddMMyy") + String.Format("{0:00000}", lintSerial++);
                                                    else
                                                        lstrCurrSerial = String.Format("{0:00000}", lintSerial++);

                                                    //each box will have the quantity as per pallet size enter by user manually.
                                                    int lintCurrBoxQuantity = Convert.ToInt16(PalletSize.Split('_')[CurrLabel - 1].ToString());
                                                    
                                                    string lstrCurrBarCode = common.GenerateGRNBarCode(
                                                            common.ReplaceNullString(currRow.Cells["PART_NO"].Value.ToString().Trim()),
                                                            txtANoticeNo.Text.Trim(),
                                                            lintCurrBoxQuantity.ToString(),
                                                            lstrCurrSerial,
                                                            clsCurrentUser.Site_Name.ToString());

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
                                                        BOX_QUANTITY = lintCurrBoxQuantity
                                                    };
                                                    db.GRN_LABEL_PRINTING.Add(grn_label);
                                                }//end of FOR LOOP for current label
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
                                    LoadGRNDataInForm(common.GetGRNData_ByNoticeNo(txtANoticeNo.Text.Trim(), "Coil"), false);
                                    dgvData.Columns["chkPrint"].Visible = true;
                                    // dgvData.Columns["Block"].Visible = true; // done by putta i swapped the coding loadgrndatainform method i put first then chkprint column vissible i put next.

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


                    dgvData.DataSource = pgrn_data.lstGrn_Dtl;
                    //common.MarkBlockedPart(ref dgvData);


                    btnSave.Enabled = false;
                    btnPrintBarcode.Enabled = true;

                    if (pblnshowModifyAlert)
                        ClsMessage.ShowInfo("This A Notice No. Already exist, hence cannot be modify.");

                    btnSave.Enabled = false;
                    btnSigma.Enabled = false;
                    dgvData.Columns["chkPrint"].Visible = true;
                    btnAddPalletDetails.Enabled = false;
                }
                else
                {
                    btnSave.Enabled = true;
                    dgvData.Columns["chkPrint"].Visible = false;
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

                txtInvoiceNo.Text = mds.Tables[0].Rows[0]["Invoice_No"].ToString();

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

                                            int lintLabelToPrint = 0;

                                            if (currRow.Cells["No_Of_Print"].Value != null)
                                                lintRequestedLabelQty = Convert.ToInt32(currRow.Cells["No_Of_Print"].Value.ToString());

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
                                                        lintNoOfBox = Convert.ToInt32(currRow.Cells["Pack_Size"].Value.ToString());

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
                                                        db.SaveChanges();
                                                        DbTransacation.Commit();
                                                    }//end of inner try block inside using tansaction
                                                    catch (Exception ex)
                                                    {
                                                        DbTransacation.Rollback();
                                                        throw new Exception("Error occurred during label print of Serial No." + grnlst[currPart].TODAY_BARCODE_SERIAL);
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
                                            }//End of for loop for current part's labels print
                                        }
                                    } //End of condition to check if current row is selected or not
                                }////end of condition check for current row is not blank for part no.
                            }//end of for loop of each row in data grid view

                            ClsMessage.ShowInfo("Requested barcode labels have been printied successfully.");
                            LoadGRNDataInForm(common.GetGRNData_ByNoticeNo(txtANoticeNo.Text.Trim(), "Coil"), false);
                        }//end of using db transaction
                    }//End of using db entities
                }//
            } // end of main try block
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }

        #endregion

        #endregion

        #region Main_Form_Operations
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveGRNData();
        }


        private void btnBrowse_Click(object sender, EventArgs e)
        {

        }

        private void btnPrintBarcode_Click(object sender, EventArgs e)
        {
            if (common.IsAnyRowSelected(dgvData, "chkPrint"))
                PrintBarCodeData();
            else

                ClsMessage.ShowError("Please select at least one part to print the barcode.");
        }
        #endregion


        /// <summary>
        /// Calculates the total number of barcode to be generated for this GRN, so that we can book the barcode serial in database to avoid duplicasy
        /// </summary>
        /// <returns>No. of barcode to be generated</returns>
        private int GetNoOfBarcodeToBeGenerated()
        {
            int lintTotalBarcode = 0;
            try
            {
                foreach (DataGridViewRow currRow in dgvData.Rows)
                {
                    if (currRow.Cells["Part_No"].Value != null && currRow.DefaultCellStyle.BackColor != Color.Red)
                        lintTotalBarcode +=  Convert.ToInt32(currRow.Cells["Pack_Size"].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException  + " in Method: " + MethodBase.GetCurrentMethod());
            }
            return lintTotalBarcode;
        }

        private void optInvoiceNo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnSigma_Click(object sender, EventArgs e)
        {
            try
            {
                pnlPalletSize.Controls.Clear();
                txtNoOfPallets.Text = "";
                pnlPalletInfo.Enabled = false;

                if (ValidateForm())
                {
                    //DataSet md= cOdbc.ExecOdbcQry("");

                    if (ConfigurationSettings.AppSettings["AppEnvironment"].ToString().ToUpper() == "DEV")
                        cOdbc = new clsODBC(common.gstrSigmaConStringLocal);
                    else
                        cOdbc = new clsODBC(common.gstrSigmaConString);

                    string lstrQry = "";

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
                                lstrQry = "SELECT bd275pr.CHPLNT as plant, bd423pr.ANNNBR AS A_Notice_No,bd423pr.ANRCDT as INVOICE_DATE,bd275pr.CHCNNO as Container," +
                                            "bd275pr.CHIVNO as INVOICE_NO, bd275pr.CHPRT1 as PART_NO,bd275pr.CHORNO as OrderNo,bd275pr.CHBXNO as PALLETE_NO," +
                                            "bd275pr.CHPKQY as QTY_PER_BOX,bd275pr.CHNOPK as NO_OF_BOX,bd275pr.CHSHQY as ShippedQty,bd275pr.CHRVQY as QUANTITY," +
                                            "bd275pr.CHUOFM as UOM FROM C809EF9W.KIRESLIB.bd275pr bd275pr inner join " +
                                            "C809EF9W.KIRESLIB.bd423pr bd423pr on bd275pr.CHCNNO = bd423pr.ANCNNO WHERE bd423pr.ANNNBR = '" + txtANoticeNo.Text.Trim() + "' ";
                            }
                            else if (mds.Tables[0].Rows[0]["FTEIFG"].ToString() == "1") // dometic Details without pallet (1 is domestic Supplier)
                            {
                                lstrQry = "select bd423pr.ANPLNT as plant, bd423pr.ANNNBR AS A_Notice_No, bd423pr.ANRCDT as INVOICE_DATE, bd423pr.ANCNNO as Container," +
                                            "bd423pr.ANRFNO as INVOICE_NO, bd423pr.ANPRTN as PART_NO, '' as OrderNo,'' as PALLETE_NO, " +
                                            "'0' as QTY_PER_BOX,'0' as NO_OF_BOX,'0' as ShippedQty,bd423pr.ANRCQY as QUANTITY,bd423pr.ANPUOM as UOM " +
                                            "from C809EF9W.KIRESLIB.bd423pr bd423pr where ANNNBR='" + txtANoticeNo.Text.Trim() + "'";
                            }
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

                                foreach (DataRow currRow in mds.Tables[0].Rows)
                                {
                                    string lstrPartNo = currRow["Part_No"].ToString();
                                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                                    {
                                        MATERIAL_MST part_item = db.MATERIAL_MST.Where(x => x.PART_NO == lstrPartNo).ToList().FirstOrDefault();
                                        if (part_item != null)
                                            currRow["PART_NAME"] = part_item.PART_NAME.ToString();
                                    }
                                }
                                LoadGRNData();
                                btnSave.Enabled = true;
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
                        if (dgvData.Columns[dgvData.CurrentCell.ColumnIndex].Name.ToUpper() == "PALLET_INFO")
                        {
                            pnlPalletInfo.Enabled = true;
                            if(common.ReplaceNullString(dgvData.CurrentRow.Cells["Pack_Size"].Value) != "")
                            {
                                int lintPackSize = Convert.ToInt16(common.ReplaceNullString(dgvData.CurrentRow.Cells["Pack_Size"].Value));

                                txtNoOfPallets.Text = lintPackSize.ToString();
                                GeneratePalletSizeTextBox(lintPackSize,false);

                                string strPalletSize = common.ReplaceNullString(dgvData.CurrentRow.Cells["Pallet_Size"].Value);
                                var arr = strPalletSize.Split('_');
                                for(int lintPalletNo=1;lintPalletNo<= Convert.ToInt16(txtNoOfPallets.Text);lintPalletNo++)
                                {
                                    pnlPalletSize.Controls["txtPallet" + lintPalletNo].Text = arr[lintPalletNo - 1].ToString();
                                }
                            }
                            else
                            {
                                pnlPalletSize.Controls.Clear();
                                pnlPalletInfo.Enabled = true;
                                txtNoOfPallets.Text = "";
                                txtNoOfPallets.Focus();
                            }
                            txtNoOfPallets.Focus();
                        }
                        else if (dgvData.Columns[dgvData.CurrentCell.ColumnIndex].Name.ToUpper() == "CHKPRINT")
                        {
                            //if (dgvData.CurrentRow.Cells["IS_BLOCK"].Value != null && dgvData.CurrentRow.Cells["IS_BLOCK"].Value.ToString() == "1")
                            //{
                            //    ClsMessage.ShowError("This part is blocked hence cannot Print the Label");
                            //    dgvData.CurrentCell.Value = false;
                            //}
                            //else 
                            if (dgvData.CurrentRow.DefaultCellStyle.BackColor == Color.Yellow)
                            {
                                ClsMessage.ShowError("There is no label remaining for this part to be printed.");
                                dgvData.CurrentCell.Value = false;
                            }
                            else
                            {
                                if (Convert.ToBoolean(dgvData.CurrentCell.Value))
                                {
                                    //print checkbox is deselcted for current row
                                    dgvData.CurrentCell.Value = false;
                                    dgvData.CurrentRow.Cells["No_Of_Print"].Value = "";
                                }
                                else
                                {
                                    dgvData.CurrentCell.Value = true;

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

                                    ////in case of mix pallet (external warehouse only), since mixpallet priting will be on A4 hence wont ask for no. of label to print
                                    //if (common.ReplaceNullString(dgvData.CurrentRow.Cells["PALLETE_NO"].Value) == "" || cmbGRNType.Text == "STO")
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


                                }//end of ELSE part of if condition to check if current row is selected or deselected
                            }//end of ELSE part of if condition if current row is yellow
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException  + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }
        

        private void btnAddPalletDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (!common.IsNumeric(common.ReplaceNullString(txtNoOfPallets.Text)))
                {
                    ClsMessage.ShowError("Please enter the valid number of pallets for curent part.");
                    txtNoOfPallets.Focus();
                }
                else if (Convert.ToInt16(common.ReplaceNullString(txtNoOfPallets.Text)) ==0)
                {
                    ClsMessage.ShowError("Please enter the number of pallets greater than 0");
                    txtNoOfPallets.Focus();
                }
                else
                {
                    GeneratePalletSizeTextBox(Convert.ToInt16(txtNoOfPallets.Text),true);
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException  + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }

        private void GeneratePalletSizeTextBox(int pintNoOfPallets,bool pblnShowSavePalletButton)
        {
            try
            {
                pnlPalletSize.Controls.Clear();

                int Location_x=10;
                int Location_y=10;
                Point p;

                for (int lintPalletNo = 1; lintPalletNo <= pintNoOfPallets; lintPalletNo++)
                {
                    Location_x = 10;

                    Label lbl = new Label();
                    lbl.Name = "lblPallet" + lintPalletNo;
                    lbl.Text = "Pallet " + lintPalletNo + " (KG)";
                    lbl.Location = new Point(Location_x, Location_y);
                    Location_x += 115;
                    pnlPalletSize.Controls.Add(lbl);


                    TextBox txt = new TextBox();
                    txt.Name = "txtPallet" + lintPalletNo;
                    txt.Location = new Point(Location_x, Location_y);
                    Location_y += 40;
                    txt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPalletSize_KeyPress);
                    if (!pblnShowSavePalletButton)
                        txt.Enabled = false;

                    pnlPalletSize.Controls.Add(txt);
                    pnlPalletSize.Height = txt.Location.Y + 50;
                }

                if (pblnShowSavePalletButton)
                {
                    //Add Save Button to save Current Part's Pallet Info
                    Button btn = new Button();
                    btn.Name = "btnSavePalletInfo";
                    btn.Location = new Point(Location_x + 110, Location_y - 40);
                    btn.Text = "Save Pallet Size";
                    btn.Height = 30;
                    btn.Width = 150;
                    btn.Click += new System.EventHandler(this.btnSavePalletInfo_Click);
                    pnlPalletSize.Controls.Add(btn);
                    btn.BackColor = Color.FromArgb(100, 0, 0, 225);
                }
                pnlPalletSize.Controls["txtPallet1"].Focus();
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException  + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }

        private void btnSavePalletInfo_Click(object sender, EventArgs e)
        {
            try
            {
                int lintTotalQty = Convert.ToInt16(dgvData.CurrentRow.Cells["Quantity"].Value.ToString());
                int lintTotalPallets=0;
                int lintAllPalletsQty = 0;
                string lstrPalletSizeInfo ="";

                if (common.IsNumeric(txtNoOfPallets.Text.Trim()))
                    lintTotalPallets = Convert.ToInt16(txtNoOfPallets.Text.Trim());
                else
                {
                    ClsMessage.ShowError("Please enter the valid numeric value for No. of Pallets.");
                    txtNoOfPallets.Focus();
                    return;
                }


                for (int lintPalletNo = 1; lintPalletNo <= lintTotalPallets; lintPalletNo++)
                {

                    if (!common.IsNumeric(common.ReplaceNullString(pnlPalletSize.Controls["txtPallet" + lintPalletNo].Text.Trim())))
                    {
                        ClsMessage.ShowConfimation("Please enter valid numeric value for Pallet " + lintPalletNo);
                        pnlPalletSize.Controls["txtPallet" + lintPalletNo].Focus();
                        return;
                    }
                    else if(Convert.ToInt16(common.ReplaceNullString(pnlPalletSize.Controls["txtPallet" + lintPalletNo].Text.Trim()))==0)
                    {
                        ClsMessage.ShowConfimation("Pallet size cannot be 0 for Pallet " + lintPalletNo);
                        pnlPalletSize.Controls["txtPallet" + lintPalletNo].Focus();
                        return;
                    }
                    else
                    {
                        lintAllPalletsQty += Convert.ToInt16(pnlPalletSize.Controls["txtPallet" + lintPalletNo].Text.Trim());
                        lstrPalletSizeInfo += pnlPalletSize.Controls["txtPallet" + lintPalletNo].Text.Trim() + "_";
                    }
                }

                if (lintAllPalletsQty != lintTotalQty)
                {
                    ClsMessage.ShowError("All Pallets Quantity is not equal to toal quantity");
                    return;
                }
                else
                {
                    if (lstrPalletSizeInfo.Substring(lstrPalletSizeInfo.Length - 1) == "_")
                    {
                        lstrPalletSizeInfo = lstrPalletSizeInfo.Substring(0, lstrPalletSizeInfo.Length - 1);
                        dgvData.CurrentRow.Cells["Pallet_Size"].Value = lstrPalletSizeInfo;
                    }
                    dgvData.CurrentRow.Cells["PACK_SIZE"].Value = txtNoOfPallets.Text;
                }
                ClsMessage.ShowInfo("Pallet Information updated for current part");
                pnlPalletSize.Controls.Clear();
                txtNoOfPallets.Text = "";
                pnlPalletInfo.Enabled = false;
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException  + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }

        private void cmbGRNType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtANoticeNo.Focus();
        }

        private void txtANoticeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSave.Enabled = false;
                if (Convert.ToInt16(e.KeyChar) == 13)
                {
                    grn_data = common.GetGRNData_ByNoticeNo(txtANoticeNo.Text.Trim(),"Coil");
                    if (grn_data != null)
                    {
                        LoadGRNDataInForm(grn_data, true);
                        btnPrintBarcode.Enabled = true;
                        btnSigma.Enabled = false;
                    }
                    else
                    {
                        common.ClearDataGridViewRows(ref dgvData);
                        btnSigma.Enabled = true;
                        btnPrintBarcode.Enabled = false;
                        txtInvoiceNo.Text = "";
                    }
                }
            }
        }

        private void txtPalletSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                TextBox txt = (TextBox)sender;
                string lstrCurrPalletNo = txt.Name.Replace("txtPallet","");
                if(pnlPalletSize.Controls["txtPallet" + (Convert.ToInt16(lstrCurrPalletNo) + 1).ToString()] !=null)
                    pnlPalletSize.Controls["txtPallet" + (Convert.ToInt16(lstrCurrPalletNo) + 1).ToString() ].Focus();
                else
                {
                    pnlPalletSize.Controls["btnSavePalletInfo"].Focus();
                }
            }
        }

        private void txtNoOfPallets_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
                btnAddPalletDetails.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (common.ConfirmToExit())
                this.Close();
        }
    }
}
