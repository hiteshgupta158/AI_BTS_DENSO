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

namespace AI_BTS_DENSO
{
    public partial class frmQtyUpdate : Form
    {
        clsCommon common = new clsCommon();
        GRN_DATA grn_data = new GRN_DATA();
        GRN_DTL currPart;
        public frmQtyUpdate()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmQtyUpdate_Load(object sender, EventArgs e)
        {
            try
            {
                common.GetUserAccessForCurrentScreen(this, "Qty Update");
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }

        private void txtANoticeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                bool blnIsPartfound = false;
                List<GRN_DTL> lstPartApplicableToEdit = new List<GRN_DTL>();
                if (e.KeyChar == 13)
                {
                    grn_data = common.GetGRNData_ByNoticeNo(txtANoticeNo.Text.Trim(), "Parts");

                    if (grn_data.Grn_Mst != null)
                    {
                        //First get the list of part which are not part of mix pallet and whose status is 0 and part is not blocked
                        List<GRN_DTL> lstgrnDtl = grn_data.lstGrn_Dtl.Where(x => common.ReplaceNullString(x.PALLETE_NO) == "" && x.IS_BLOCK != 1 && x.STATUS==0).ToList();
                        if (lstgrnDtl.Count > 0)
                        {
                            using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                            {
                                foreach (GRN_DTL currPart in lstgrnDtl)
                                {
                                    //Check if any box is printed for this part. if any box is printed then its quantity cannot be edited.
                                    GRN_LABEL_PRINTING grn_lbl = db.GRN_LABEL_PRINTING.Where(x => x.GRN_DTL_ID == currPart.GRN_DTL_ID && x.STATUS > 0).FirstOrDefault();
                                    if (grn_lbl == null)
                                    {
                                        blnIsPartfound = true;
                                        lstPartApplicableToEdit.Add(currPart);
                                    }
                                }
                            }
                        }

                        common.FillComboBox(cmbPart, lstPartApplicableToEdit, "Part_NO", "GRN_DTL_ID");
                    }

                    if (!blnIsPartfound)
                    {
                        ClsMessage.ShowInfo("No such part exists in current GRN, which is an individual part and whose label is not printed yet and also part is not blocked.");
                        clear();
                    }
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }

        private void cmbPart_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                long lintGrnDTLID=-1;
                currPart = null;
                if (common.ReplaceNullString(cmbPart.SelectedValue)!="")
                    lintGrnDTLID = Convert.ToInt64(cmbPart.SelectedValue.ToString());

                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    currPart = db.GRN_DTL.Where(x => x.GRN_DTL_ID == lintGrnDTLID).FirstOrDefault();
                }
                    
                if(currPart!=null)
                {
                    txtAvailableQty.Text = currPart.QUANTITY.ToString();
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            try
            {
                if(Validateform())
                {
                    if (currPart != null)
                    {
                        using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                        {

                            using (var DbTransacation = db.Database.BeginTransaction())
                            {
                                try
                                {

                                    //Update quantity in grn DTL Table as old Qty + added Qty so that label will be printed as per updated total quantity
                                    #region SAVE GRN_DTL Table data
                                    //currPart = new GRN_DTL();
                                    currPart = db.GRN_DTL.Where(x => x.GRN_DTL_ID == currPart.GRN_DTL_ID).FirstOrDefault();

                                    currPart.QUANTITY = currPart.QUANTITY + Convert.ToInt32(common.ReplaceNullNumber(txtAddQty.Text));
                                    currPart.ADDED_QUANTITY = Convert.ToInt32(common.ReplaceNullNumber(txtAddQty.Text));
                                    currPart.TRANSFER_SLIP_NO = common.ReplaceNullString(txtTransferSlipNo.Text);
                                    currPart.REASON = common.ReplaceNullString(txtReason.Text);
                                    #endregion


                                    #region CREATE GRN_LABEL_PRINTING RECORDS FOR ADDED QTY
                                    GRN_LABEL_PRINTING grn_lbl = new GRN_LABEL_PRINTING();
                                    //Generate barcode for enw quantity, but check if added quantity is creating total new boxes or any open box was earlier and 
                                    //some quanity can be merged in previous open box

                                    //Calculate Total Box Quantity
                                    Int64 lintOLDQuantity = Convert.ToInt64(currPart.QUANTITY) - Convert.ToInt64(currPart.ADDED_QUANTITY);
                                    long lintOLDNoOfBox = 1;
                                    lintOLDNoOfBox = lintOLDQuantity / Convert.ToInt64(currPart.PACK_SIZE.ToString());
                                    long lintOLDOpenBoxQty = lintOLDQuantity % Convert.ToInt64(currPart.PACK_SIZE.ToString());
                                    if (lintOLDOpenBoxQty > 0)
                                        lintOLDNoOfBox += 1;

                                    //First we will check if added quantity as any open box or not if not then we will not make any change
                                    //in old boxes and will just create additional boxes
                                    if (currPart.ADDED_QUANTITY % Convert.ToInt64(currPart.PACK_SIZE.ToString()) == 0)
                                    {
                                        //case When there is no open box in new added quantity
                                        int lintNewBoxesToBeAdded = Convert.ToInt32(currPart.ADDED_QUANTITY / Convert.ToInt32(currPart.PACK_SIZE.ToString()));

                                        long lintSerial = common.GenerateBarcodeSerial("GRN", lintNewBoxesToBeAdded);

                                        for (int CurrLabel = 1; CurrLabel <= lintNewBoxesToBeAdded; CurrLabel++)
                                        {
                                            string lstrCurrSerial = "";
                                            if (lintSerial.ToString().Length <= 5)
                                                lstrCurrSerial = DateTime.Today.ToString("ddMMyy") + String.Format("{0:00000}", lintSerial++);
                                            else
                                                lstrCurrSerial = String.Format("{0:00000}", lintSerial++);

                                            int lintCurrBoxQuantity = Convert.ToInt32(currPart.PACK_SIZE);

                                            string lstrCurrBarCode = common.GenerateGRNBarCode(
                                                                                common.ReplaceNullString(currPart.PART_NO),
                                                                                txtANoticeNo.Text.Trim(),
                                                                                lintCurrBoxQuantity.ToString(),
                                                                                lstrCurrSerial,
                                                                                clsCurrentUser.Site_Name.ToString());

                                            int currBRSerial = Convert.ToInt32( CurrLabel + lintOLDNoOfBox);

                                            GRN_LABEL_PRINTING grn_label = new GRN_LABEL_PRINTING  
                                            {
                                                GRN_DTL_ID = currPart.GRN_DTL_ID,
                                                PRIMARY_BARCODE = lstrCurrBarCode,
                                                BR_SERIAL = currBRSerial,
                                                LABEL_TYPE_MST_ID = 1,
                                                STATUS = 0,
                                                CREATED_BY = clsCurrentUser.User_MST_ID,
                                                CREATED_DATE = DateTime.Now,
                                                TODAY_BARCODE_SERIAL = lstrCurrSerial,
                                                BOX_QUANTITY = lintCurrBoxQuantity,
                                            };
                                            db.GRN_LABEL_PRINTING.Add(grn_label);
                                            db.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        //Case when there is a new box in new added quantity. Here may be two cases. Old quantity may or may not
                                        //have open box, while new added quantity have open box. if old quantity do not have open box then we 
                                        //will add these new quantity boxes as it is new qty (Closed + Open). otherwise we will add quantity 
                                        Int64 lintTotalQuantity = Convert.ToInt64(currPart.QUANTITY);// - Convert.ToInt64(currPart.ADDED_QUANTITY);
                                        long lintTotalNoOfBox = 1;
                                        lintTotalNoOfBox = lintTotalQuantity / Convert.ToInt64(currPart.PACK_SIZE.ToString());
                                        long lintTotalOpenBox = lintTotalQuantity % Convert.ToInt64(currPart.PACK_SIZE.ToString());
                                        if (lintTotalOpenBox > 0)
                                            lintTotalNoOfBox += 1;

                                        //Case when Old quantity has open Boxes as well as new added quantity has open box. then merge both the open boxes
                                        //and calculate how many new boxes need to be introduced. And box quantity of old open box has to be updaed as per pack size
                                        int lintNewOpenQtyRemaining = 0;
                                        int lintNewClosedBoxRemaining = 0;
                                        int lintNewOpenBoxQty = Convert.ToInt32( currPart.ADDED_QUANTITY % Convert.ToInt32(currPart.PACK_SIZE.ToString()));
                                        int lintCurrPartSize = Convert.ToInt32(currPart.PACK_SIZE.ToString());

                                        if (lintOLDOpenBoxQty > 0 && lintNewOpenBoxQty >  0)
                                        {
                                            //Check if after mergin new open qty and old open qty is there still any open quantity remaining. or its merged in old box only.
                                            //new closed box will be separate. here we calculating for open qty only
                                            GRN_LABEL_PRINTING grn_label;
                                            //there may be maximum one box open in old quantity.
                                            grn_label = db.GRN_LABEL_PRINTING.Where(x => x.GRN_DTL_ID == currPart.GRN_DTL_ID && x.BOX_QUANTITY != lintCurrPartSize).FirstOrDefault();

                                            if (lintOLDOpenBoxQty + lintNewOpenBoxQty <= lintCurrPartSize)
                                            {
                                                //set current box quantity as per total of old open qty and new open qty.
                                                grn_label.BOX_QUANTITY = Convert.ToInt32( lintOLDOpenBoxQty + lintNewOpenBoxQty);
                                            }
                                            else
                                            {
                                                //case when after merging the old open box and new open box still some open qty is remaning.
                                                lintNewOpenQtyRemaining = Convert.ToInt32((lintOLDOpenBoxQty + lintNewOpenBoxQty)) - lintCurrPartSize;

                                                //Set Current old open box quantity as per pack size since this box is full now.
                                                grn_label.BOX_QUANTITY = lintCurrPartSize;
                                            }
                                            #region UPDATE QTY IN LABEL BARCODE
                                            var arrPrimaryBarcode = grn_label.PRIMARY_BARCODE.ToString().Split('|');
                                            grn_label.PRIMARY_BARCODE = arrPrimaryBarcode[0] + "|" + arrPrimaryBarcode[1] + "|" + grn_label.BOX_QUANTITY + "|" +
                                                                        arrPrimaryBarcode[3] + "|" + arrPrimaryBarcode[4];
                                            #endregion
                                        }
                                        else
                                        {
                                            //Case When Old quantity does not have open box but new added quantity has
                                            lintNewOpenQtyRemaining = lintNewOpenBoxQty;
                                        }

                                        lintNewClosedBoxRemaining = Convert.ToInt32(currPart.ADDED_QUANTITY / lintCurrPartSize);
                                        if (lintNewOpenQtyRemaining > 0)
                                            lintNewClosedBoxRemaining += 1;


                                        long lintSerial = common.GenerateBarcodeSerial("GRN", lintNewClosedBoxRemaining);

                                        for (int CurrLabel = 1; CurrLabel <= lintNewClosedBoxRemaining; CurrLabel++)
                                        {
                                            string lstrCurrSerial = "";
                                            if (lintSerial.ToString().Length <= 5)
                                                lstrCurrSerial = DateTime.Today.ToString("ddMMyy") + String.Format("{0:00000}", lintSerial++);
                                            else
                                                lstrCurrSerial = String.Format("{0:00000}", lintSerial++);

                                            int lintCurrBoxQuantity = Convert.ToInt32(currPart.PACK_SIZE);
                                            if(CurrLabel == lintNewClosedBoxRemaining && lintNewOpenQtyRemaining > 0 )
                                                lintCurrBoxQuantity = lintNewOpenQtyRemaining;

                                            string lstrCurrBarCode = common.GenerateGRNBarCode(
                                                                                common.ReplaceNullString(currPart.PART_NO),
                                                                                txtANoticeNo.Text.Trim(),
                                                                                lintCurrBoxQuantity.ToString(),
                                                                                lstrCurrSerial,
                                                                                clsCurrentUser.Site_Name.ToString());

                                            int currBRSerial = Convert.ToInt32(CurrLabel + lintOLDNoOfBox);

                                            GRN_LABEL_PRINTING grn_label = new GRN_LABEL_PRINTING
                                            {
                                                GRN_DTL_ID = currPart.GRN_DTL_ID,
                                                PRIMARY_BARCODE = lstrCurrBarCode,
                                                BR_SERIAL = currBRSerial,
                                                LABEL_TYPE_MST_ID = 1,
                                                STATUS = 0,
                                                CREATED_BY = clsCurrentUser.User_MST_ID,
                                                CREATED_DATE = DateTime.Now,
                                                TODAY_BARCODE_SERIAL = lstrCurrSerial,
                                                BOX_QUANTITY = lintCurrBoxQuantity,
                                            };
                                            db.GRN_LABEL_PRINTING.Add(grn_label);
                                            db.SaveChanges();
                                        }
                                    }
                                    #endregion

                                    db.SaveChanges();
                                    DbTransacation.Commit();
                                    ClsMessage.ShowDataSavedConfirmation();
                                }
                                catch(Exception ex)
                                {
                                    DbTransacation.Rollback();
                                    throw new Exception(ex.Message);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + " in Method: " + MethodBase.GetCurrentMethod());
                ClsMessage.ShowError("Error occurred while saving data. Please contact application administrator");
            }
        }

        private bool Validateform()
        {
            bool bnlIsValid = false;
            try
            {
                if (common.ReplaceNullString(txtANoticeNo.Text) == "")
                {
                    ClsMessage.ShowError("Please enter the valid A Notice/Invoice No.");
                    txtANoticeNo.Focus();
                }
                else if (common.ReplaceNullString(cmbPart.SelectedValue) == "")
                {
                    ClsMessage.ShowError("Please select the valid part no.");
                    cmbPart.Focus();
                }
                else if (common.ReplaceNullString(txtAddQty.Text) == "" || common.IsNumeric(txtAddQty.Text) == false)
                {
                    ClsMessage.ShowError("Please enter the valid qty to add");
                    txtAddQty.Focus();
                }
                else if (common.ReplaceNullString(txtTransferSlipNo.Text) == "")
                {
                    ClsMessage.ShowError("Please enter the transfer slip No.");
                    txtTransferSlipNo.Focus();
                }
                else if (common.ReplaceNullString(txtReason.Text) == "")
                {
                    ClsMessage.ShowError("Please enter the reason to transfer");
                    txtReason.Focus();
                }
                else
                    bnlIsValid = true;
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + " in Method: " + MethodBase.GetCurrentMethod());
            }
            return bnlIsValid;
        }

        private void clear()
        {
            try
            {
                txtANoticeNo.Text = "";
                cmbPart.SelectedIndex = -1;
                txtAvailableQty.Text = "";
                txtAddQty.Text = "";
                txtTransferSlipNo.Text = "";
                txtReason.Text = "";
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + " in Method: " + MethodBase.GetCurrentMethod());
            }
        }
    }
}
