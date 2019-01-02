using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Reflection;
using AI_BTS_DENSO.Model;
using System.IO; 
using System.Data;
using ZTCommon;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Configuration;
using System.Collections;

namespace AI_BTS_DENSO.Common
{
       
    class clsCommon
    {
        public ZTCommon.clsCommon ztCommon = new ZTCommon.clsCommon();
        private string mstrErrorLogfile = Application.StartupPath + "\\" + Application.ProductName + "_Error.txt";
        private string mstrLogfile = Application.StartupPath + "\\" + Application.ProductName + ".txt";
        private clsExcel cExcel;
        public string gstrSigmaConString = "DSN=" + ConfigurationSettings.AppSettings["DSN_NAME"].ToString() + 
                                           ";Uid=" + ConfigurationSettings.AppSettings["DSN_UID"].ToString() + 
                                           ";Pwd=" + ConfigurationSettings.AppSettings["DSN_PWD"].ToString() +";";

        public string gstrSigmaConStringLocal = "DSN=GRN_SIGMA;Uid=dbuser;Pwd=Dbtest@123;";
        public string lstrCurrentColumnName = "";

        public clsCommon()
        {
           
        }


        /// <summary>
        /// Gets the current form View name on behalf of button clicked in left navigation menu by user.
        /// </summary>
        /// <param name="pintSelectedLeftMenuIndex">Tag no. of selected button in left navigation menu.</param>
        /// <returns>Returns the form to be opened.</returns>
        public Form GetCurrentFormView(int pintSelectedLeftMenuIndex)
        {
            Form lfrm = new Form();
            try
            {
                switch(pintSelectedLeftMenuIndex)
                {
                    case 101:
                        lfrm = new frmLocation();
                        break;
                    case 102:
                        lfrm = new frmBinType();
                        break;
                    case 103:
                        lfrm = new frmProcessType();
                        break;
                    case 104:
                        lfrm = new frmMachineType();
                        break;
                    case 105:
                        lfrm = new frmMaterialType();
                        break;
                    case 106:
                        lfrm = new frmDepartment();
                        break;
                    case 107:
                        lfrm = new frmFinishedGoods();
                        break;
                    case 108:
                        lfrm = new frmCategory();
                        break;
                    case 109:
                        lfrm = new frmUOM();
                        break;
                    case 110:
                        lfrm = new frmProcess();
                        break;
                    case 111:
                        lfrm = new frmMachine();
                        break;
                    case 112:
                        lfrm = new frmMaterial();
                        break;
                    case 114:
                        lfrm = new frmUserRole();
                        break;
                    case 115:
                        lfrm = new frmUser();
                        break;
                    case 117:
                        lfrm = new frmGRN();
                        break;
                    case 118:
                        lfrm = new frmQualityCheck();
                        break;
                    case 119:
                        lfrm = new frmSTO();
                        break;
                    case 120:
                        lfrm = new frmGRNCoil();
                        break;
                    case 121:
                        lfrm = new frmReprint();
                        break;
                    case 122:
                        lfrm = new frmQtyUpdate();
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0,ex.StackTrace.IndexOf("(")));
            }
            return lfrm;
        }


        /// <summary>
        /// set the Form and its control styles (backcolor, fore color, font size etc on Form load).
        /// </summary>
        /// <param name="pFrm">Form whose style need to be set</param>
        public void LoadFormColors(Form pFrm)
        {
            Color c = new Color();
           
            pFrm.BackColor = Color.FromArgb(200, 10, 34);

            foreach (Control currentControl in pFrm.Controls)
            {
                if (currentControl.Name.ToUpper()=="PNLMAIN")
                {
                    currentControl.BackColor = Color.FromArgb(100, 155, 44, 225);
                    Panel pnl = (Panel)currentControl;
                    pnl.BorderStyle = BorderStyle.Fixed3D;
                }
                else if(currentControl.GetType() == typeof(TextBox))
                {
                    currentControl.BackColor = Color.Red;
                }
            }
        }


        /// <summary>
        /// Confirmation Dialogue to confirm from user for record deletetion
        /// </summary>
        /// <returns>Confirmation: if user really wants to delete data or not</returns>
        public bool ConfirmToDelete()
        {
            return ClsMessage.ShowConfirmToDetele() == DialogResult.Yes;
        }

        /// <summary>
        /// Confirmation Dialogue to confirm from user for printing barcode of selected record
        /// </summary>
        /// <returns>Confirmation: if user really wants to delete data or not</returns>
        public bool ConfirmToPrintBarCode()
        {
            return ClsMessage.ShowConfirmToPrintBarCode() == DialogResult.Yes;
        }

        /// <summary>
        /// Confirms from user to exit from any view.
        /// </summary>
        /// <returns>Returns true if yous Select "Yes" to exit else returns false</returns>
        public bool ConfirmToExit()
        {
            return ClsMessage.ShowConfirmToExit() == DialogResult.Yes;
        }


        public string GetActiveComboValue(string pCurrVal)
        {
            if (pCurrVal.ToUpper() == "YES")
                return "1";
            else
                return "0";
        }


        public string SetActiveComboValue(string pCurrVal)
        {
            if (pCurrVal.ToUpper() == "1")
                return "Yes";
            else
                return "No";
        }


        #region FillComboBox

        public void FillComboBox(ComboBox comboBox, object dataSource, string DisplayMember, string ValueMember)
        {
            try
            {
                comboBox.DataSource = dataSource;
                comboBox.DisplayMember = DisplayMember;
                comboBox.ValueMember = ValueMember;
                comboBox.SelectedIndex = -1;
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }


        /// <summary>
        /// Filles the drop down list for Active combo values (Yes/No) available in system
        /// </summary>
        /// <param name="cmb">Drop down control needs to be filled</param>
        public void FillActive(ref ComboBox cmb)
        {
            try
            {
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    FillComboBox(cmb, db.ACTIVE_MST.ToList(), "ACTIVE_VALUE", "ACTIVE_MST_ID");
                }
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }



        /// <summary>
        /// Filles the drop down list for Material type available in system
        /// </summary>
        /// <param name="cmb">Drop down control needs to be filled</param>
        public void FillMaterialType(ref ComboBox cmb)
        {
            try
            {
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    FillComboBox(cmb, db.MATERIAL_TYPE_MST.ToList(), "MATERIAL_TYPE_NAME", "MATERIAL_TYPE_MST_ID");
                }
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }



        /// <summary>
        /// Filles the drop down list for Material type available in system
        /// </summary>
        /// <param name="cmb">Drop down control needs to be filled</param>
        public void FillUOM(ref ComboBox cmb)
        {
            try
            {
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    FillComboBox(cmb, db.UOM_MST.ToList(), "UOM_NAME", "UOM_MST_ID");
                }
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        /// <summary>
        /// Filles the drop down list for Machine type available in system
        /// </summary>
        /// <param name="cmb">Drop down control needs to be filled</param>
        public void FillMachineType(ref ComboBox cmb)
        {
            try
            {
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    FillComboBox(cmb, db.MACHINE_TYPE_MST.ToList(), "MACHINE_TYPE_NAME", "MACHINE_TYPE_MST_ID");

                }
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }


        /// <summary>
        /// Filles the drop down list for Process available in system
        /// </summary>
        /// <param name="cmb">Drop down control needs to be filled</param>
        public void FillProcess(ref ComboBox cmb)
        {
            try
            {
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    FillComboBox(cmb, db.PROCESS_MST.ToList(), "PROCESS_NAME", "PROCESS_MST_ID");
                }
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }



        /// <summary>
        /// Filles the drop down list for Process type available in system
        /// </summary>
        /// <param name="cmb">Drop down control needs to be filled</param>
        public void FillProcessType(ref ComboBox cmb)
        {
            try
            {
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    FillComboBox(cmb, db.PROCESS_TYPE_MST.ToList(), "PROCESS_TYPE_NAME", "PROCESS_TYPE_MST_ID");

                }
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }


        /// <summary>
        /// Filles the drop down list for finished goods available in system
        /// </summary>
        /// <param name="cmb">Drop down control needs to be filled</param>
        public void FillFinishedGoods(ref ComboBox cmb)
        {
            try
            {
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    FillComboBox(cmb, db.FG_MST.ToList(), "FG_NAME", "FG_MST_ID");

                }
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }


        /// <summary>
        /// Filles the drop down list for Categories available in system
        /// </summary>
        /// <param name="cmb">Drop down control needs to be filled</param>
        public void FillCategory(ref ComboBox cmb)
        {
            try
            {
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    FillComboBox(cmb, db.CATEGORY_MST.ToList(), "CATEGORY_NAME", "CATEGORY_MST_ID");
                }
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }


        /// <summary>
        /// Filles the drop down list for Finished Goods available in system
        /// </summary>
        /// <param name="cmb">Drop down control needs to be filled</param>
        public void FillSite(ref ComboBox cmb)
        {
            try
            {
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    FillComboBox(cmb, db.SITE_MST.Where(x=> x.ACTIVE == 1).ToList() , "SITE_NAME", "SITE_MST_ID");
                }
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }


        /// <summary>
        /// Filles the DropdownList of User's Role
        /// </summary>
        /// <param name="cmb">Drop Down control which will be filled with User Roles</param>
        public void FillUserRole(ref ComboBox cmb)
        {
            try
            {
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    FillComboBox(cmb, db.USER_ROLE_MST.ToList(), "User_Role", "User_Role_MST_ID");
                }
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        /// <summary>
        /// Filles the drop down list for Finished Goods available in system
        /// </summary>
        /// <param name="cmb">Drop down control needs to be filled</param>
        public void FillGRNType(ref ComboBox cmb,string pstrSiteName)
        {
            try
            {
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    //if(pstrSiteName.ToUpper() == "OWH")
                        FillComboBox(cmb, db.GRN_TYPE_MST.Where(x=> x.GRN_TYPE_NAME=="PO").ToList(), "GRN_TYPE_NAME", "GRN_TYPE_MST_ID");
                    //else
                        //FillComboBox(cmb, db.GRN_TYPE_MST.ToList(), "GRN_TYPE_NAME", "GRN_TYPE_MST_ID");
                }
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        #endregion

        /// <summary>
        /// Checks a value if it is numeric or not
        /// </summary>
        /// <param name="val">Value to check if it is numeric or not</param>
        /// <returns>Returns true id value is numeric otherwise rerurns false.</returns>
        public bool IsNumeric(string val)
        {
            try
            {
                int rs;
                if (val != "" && val != null)
                {
                    if (int.TryParse(val, out rs))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
                return false;
            }
        }

        /// <summary>
        /// Replace a null string with blank
        /// </summary>
        /// <param name="pstrVal">String to be replaced</param>
        /// <returns>Returns blank if source string is null otherwise actual string value</returns>
        public string ReplaceNullString(object pstrVal)
        {
            string strVal = "";
            try
            {
                if(pstrVal!=null)
                    strVal = pstrVal.ToString();
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return strVal;
        }

        /// <summary>
        /// Replace a null number to zero (0)
        /// </summary>
        /// <param name="pstrVal">Value to check for null</param>
        /// <returns>Returns the 0 if value is null else return numeric value.</returns>
        public int ReplaceNullNumber(object pstrVal)
        {
            int lintVal = 0;
            try
            {
                if (pstrVal != null)
                    lintVal = Convert.ToInt32(pstrVal);
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return lintVal;
        }


        public bool ReplaceNullBoolean(object pstrVal)
        {
            bool blnVal = false;
            try
            {
                if (pstrVal != null)
                    blnVal = Boolean.Parse(pstrVal.ToString());
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return blnVal;
        }

        /// <summary>
        /// Get the list of Material/Parts from database
        /// </summary>
        /// <param name="pstrSearchVal">Search val which needs to be matched for filtered data</param>
        /// <returns>Returns the list of Part/Material as a result from database</returns>
        public List<MATERIAL_MST> GetMaterialList(string pstrSearchVal="")
        {
            List<MATERIAL_MST> PartList = new List<Model.MATERIAL_MST>();
            try
            {
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    PartList = db.MATERIAL_MST.Where(x=> x.PART_NO.StartsWith(pstrSearchVal)).ToList<MATERIAL_MST>();
                }
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return PartList;
        }

        /// <summary>
        /// Generates the GRN Barcode for each part received in a GRN
        /// </summary>
        /// <param name="pstrPartNo">Part No of the Material/Part</param>
        /// <param name="pstrANoticeNo">A Noticeno. in which part is received</param>
        /// <param name="pstrQty">Quantity of parts received</param>
        /// <param name="pstrLocation">Location of part</param>
        /// <param name="pstrSerial">Serial No. of part</param>
        /// <returns>Returns the unique barcode for each part received in GRN</returns>
        public string GenerateGRNBarCode(string pstrPartNo,string pstrANoticeNo,string pstrQty,string pstrTodaySerial,string pstrLocation)
        {
            string lstrBarCode = "";
            try
            {   
                lstrBarCode = pstrPartNo + "|" + pstrANoticeNo + "|" + pstrQty + "|" + pstrLocation + "|" + pstrTodaySerial;
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }

            return lstrBarCode;
        }


        public void PrinGrnBarCode(string pstrPartNo, string pstrPartName, string pstrInvoiceNo, string pstrANoticeNo, string pstrQty, string pstrTodaySerial, string pstrLabelSerial, string pstrLocation)
        {
            try
            {
                SatoPrinter p = InitiateSatoPrinter();

                if (p.GetprinterStatus().StartsWith("OK"))
                {
                    p.printGRNLabel(pstrPartNo, pstrPartName, pstrInvoiceNo, pstrANoticeNo, pstrQty, pstrLabelSerial, DateTime.Now, pstrLocation, pstrTodaySerial);
                }
                else
                {
                    ClsMessage.ShowError("Could not connect with printer. Please check if printer is ready to print.");
                    throw new Exception("Error Occurred Not able to connect with Printer.");
                }
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
                throw new Exception("Error Occurred during Printing.");
            }
        }

        /// <summary>
        /// Gets GRN Master Details by Notice No.
        /// </summary>
        /// <param name="pstrANoticeNo">Notice No. for which GRN Master details need to be fetched</param>
        /// <returns>Returns object of GRN Master fetched from database</returns>
        public GRN_DATA GetGRNData_ByNoticeNo(string pstrANoticeNo,string pstrPartType)
        {
            //Created GRN_DATA Class to get the join fields data as default query.toList() passes field as per original class 
            //structure only and join fields's value do not passs. For ex. in GRN_MST SITE_Name does not pass but SITE_ID
            GRN_DATA grn_data = new GRN_DATA();
            try
            {
                if (pstrANoticeNo != "" && pstrANoticeNo != null)
                {
                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        var query = from a in db.GRN_MST
                                    join b in db.GRN_TYPE_MST on a.GRN_TYPE_MST_ID equals b.GRN_TYPE_MST_ID
                                    join c in db.SITE_MST on a.SITE_MST_ID equals c.SITE_MST_ID
                                    join d in db.GRN_DTL on a.GRN_MST_ID equals d.GRN_MST_ID
                                    where a.A_NOTICE_NO == pstrANoticeNo && d.PART_TYPE == pstrPartType
                                    select new
                                    {
                                        GRN_MST_ID = a.GRN_MST_ID,
                                        //GRN Master Join fields data
                                        GRN_TYPE_NAME = b.GRN_TYPE_NAME.ToString(),
                                        GRN_TYPE_MST_ID = a.GRN_TYPE_MST_ID,
                                        SITE = c.SITE_NAME,
                                        //GRN Master Table's fields data
                                        A_NOTICE_NO = a.A_NOTICE_NO,
                                        A_NOTICE_DATE = a.A_NOTICE_DATE,
                                        INVOICE_NO = a.INVOICE_NO,
                                        INVOICE_DATE = a.INVOICE_DATE,
                                        //GRN DetailsTable's field data
                                        GRN_DTL_ID = d.GRN_DTL_ID,
                                        PART_NO = d.PART_NO,
                                        PART_NAME = d.PART_NAME,
                                        PACK_SIZE = d.PACK_SIZE,
                                        QUANTITY = d.QUANTITY,
                                        STATUS = d.STATUS,
                                        IS_BLOCK=d.IS_BLOCK,
                                        PALLETE_NO= d.PALLETE_NO,
                                        PALLET_SIZE = d.PALLET_SIZE
                                    };

                        var result = query.ToList();

                        if (result.Count > 0)
                        {
                            GRN_MST grn_mst = new GRN_MST();
                            GRN_DTL grn_dtl;
                            List<GRN_DTL> lstgrn_dtl = new List<GRN_DTL>();

                            grn_data.GRN_TYPE = result[0].GRN_TYPE_NAME.ToString();
                            grn_data.GRN_SITE = result[0].SITE.ToString();

                            grn_mst.GRN_MST_ID = result[0].GRN_MST_ID;
                            grn_mst.A_NOTICE_NO = result[0].A_NOTICE_NO;
                            grn_mst.A_NOTICE_DATE = DateTime.Parse(result[0].A_NOTICE_DATE.ToString());
                            grn_mst.INVOICE_NO = result[0].INVOICE_NO;
                            grn_mst.INVOICE_DATE = DateTime.Parse(result[0].INVOICE_DATE.ToString());
                            grn_mst.GRN_TYPE_MST_ID = result[0].GRN_TYPE_MST_ID;

                            grn_data.Grn_Mst = grn_mst;

                            for (int currPart = 0; currPart < result.Count; currPart++)
                            {
                                grn_dtl = new GRN_DTL();
                                grn_dtl.GRN_DTL_ID = result[currPart].GRN_DTL_ID;
                                grn_dtl.GRN_MST_ID = result[currPart].GRN_MST_ID;
                                grn_dtl.PART_NO = result[currPart].PART_NO;
                                grn_dtl.PART_NAME = result[currPart].PART_NAME;
                                grn_dtl.PACK_SIZE = result[currPart].PACK_SIZE;
                                grn_dtl.QUANTITY = result[currPart].QUANTITY;
                                grn_dtl.STATUS = result[currPart].STATUS;
                                grn_dtl.IS_BLOCK = result[currPart].IS_BLOCK;
                                grn_dtl.PALLETE_NO = result[currPart].PALLETE_NO;
                                grn_dtl.PALLET_SIZE = result[currPart].PALLET_SIZE;

                                //Add current Part details to List
                                lstgrn_dtl.Add(grn_dtl);
                            }

                            if (lstgrn_dtl != null)
                                grn_data.lstGrn_Dtl = lstgrn_dtl;
                        }
                        else
                        {
                            grn_data = null;
                            ClsMessage.ShowInfo("No data found for selected A Notice No.");
                        }
                    }
                }
                else
                {
                    ClsMessage.ShowError("Please enter the A Notice No. first");
                }
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return grn_data;
        }

        /// <summary>
        /// Gets GRN Details data for A Notice NO. and Part No.
        /// </summary>
        /// <param name="pstrNoticeNo">A Notice No. for which data has to be fetched.</param>
        /// <param name="PartNo">Part No. for which data has to be fetched.</param>
        /// <returns>Returns GRN Details data</returns>
        public GRN_DTL GetGRNDTLDetails_ByPartNoNoticeNO (string pstrNoticeNo,string pstrPartNo)
        {
            GRN_DTL grn_dtl = new GRN_DTL();
           try
            {
                grn_dtl = null;
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    grn_dtl = db.GRN_DTL.Where(x => x.GRN_MST.A_NOTICE_NO == pstrNoticeNo && x.PART_NO == pstrPartNo).ToList().FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return grn_dtl;
        }



        /// <summary>
        /// Writes the Logs of application
        /// </summary>
        /// <param name="lstrMessage">Message to write in log file.</param>
        public void WriteLog(string lstrMessage)
        {
            StreamWriter sw=null;
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append(DateTime.Now + ": " + lstrMessage + "\r\n") ;
                // flush every 20 seconds as you do it
                File.AppendAllText(Application.StartupPath + "\\Error_log.txt", sb.ToString());
                //ClsMessage.ShowError("An error occurred during the operation,\r\nError Message: " + lstrMessage);
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            finally
            {
                sb.Clear();
            }
        }


        /// <summary>
        /// Gets the data from slected Excel file into a dataset to be processed/import data into datagridview
        /// </summary>
        /// <param name="ofd"></param>
        /// <returns></returns>
        public DataSet GetExcelData(ref OpenFileDialog ofd)
        {
            DataSet lds = null;
            try
            {
                ofd.FileName = "";
                ofd.Multiselect = false;
                ofd.CheckFileExists = true;

                ofd.ShowDialog();

                //user must be able to select only one file

                if (ofd.FileName == "" || ofd.FileName == null)
                    ClsMessage.ShowError("No File is selected, hence data cannot be processed.\nPlease select the valid Excel file first");
                else
                {
                    FileInfo fInfo = new FileInfo(ofd.FileName);
                    if (fInfo.Extension.ToUpper() != ".XLS" && fInfo.Extension.ToUpper() != ".XLSX")
                    {
                        ClsMessage.ShowError("Please select the valid Excel file");
                    }
                    else
                    {
                        cExcel = new clsExcel(ofd.FileName);
                        DataTable ltbl = cExcel.GetAllWorkSheetName();
                        if(ltbl.Rows.Count>0)
                        {
                            string WorksheetName = ltbl.Rows[0]["Table_Name"].ToString();
                            lds = cExcel.ExecXlsQry("Select * from [" + WorksheetName + "]");
                            if (lds == null)
                                ClsMessage.ShowError("There is no data to load. Please verify that source file contains the valid data");
                            else if (lds.Tables[0].Rows.Count == 0)
                            {
                                ClsMessage.ShowError("There is no data to load. Please verify that source file contains the valid data");
                                lds = null;
                            }
                        }
                        else
                        {
                            ClsMessage.ShowError("No data sheet found in Excel file. Data cannot be processed.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return lds;
        }


        /// <summary>
        /// Checks if a part exists in database or not.
        /// </summary>
        /// <param name="pstrPartNo">Part no. need to be checked</param>
        /// <returns>Returns part object if exists otherwise returns null.</returns>
        public MATERIAL_MST CheckIfPartExists(string pstrPartNo)
        {
            MATERIAL_MST part = null;
            try
            {
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    part = db.MATERIAL_MST.Where(x => x.PART_NO == pstrPartNo).ToList().FirstOrDefault();
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return part;
        }


        /// <summary>
        /// Generates the Serial no. of a barcode label
        /// </summary>
        /// <param name="pstrBarcodeType">Type of Barcode for which serial has to be generated</param>
        /// <returns>Returns the serial No.</returns>
        public long GenerateBarcodeSerial(string pstrBarcodeType, int pintTotalSerialCount)
        {
            long lintSerialNo = 0;
            try
            {
                if (pstrBarcodeType.ToUpper() == "GRN" || pstrBarcodeType.ToUpper() == "MIX PALLETE")
                {
                    using (AI_BTS_DENSOEntities1 DB = new Model.AI_BTS_DENSOEntities1())
                    {
                        using (var dbTransaction = DB.Database.BeginTransaction())
                        {
                            try
                            {
                                SERIAL_CONTROL_MST serial_mst = DB.SERIAL_CONTROL_MST.Where(x => x.SERIAL_DATE == DbFunctions.TruncateTime(DateTime.Today) &&
                                                                x.TARGET_MST == pstrBarcodeType).OrderBy(m => m.TODAY_BARCODE_SERIAL).ToList().LastOrDefault();
                                if (serial_mst != null)
                                    lintSerialNo = Convert.ToInt64(serial_mst.TODAY_BARCODE_SERIAL) + 1;
                                else
                                    lintSerialNo = 1;

                                if (serial_mst == null)
                                {
                                    SERIAL_CONTROL_MST srl = new SERIAL_CONTROL_MST
                                    {
                                        SERIAL_DATE = DateTime.Today,
                                        TODAY_BARCODE_SERIAL = pintTotalSerialCount,
                                        TARGET_MST = pstrBarcodeType
                                    };
                                    DB.SERIAL_CONTROL_MST.Add(srl);
                                }
                                else
                                    serial_mst.TODAY_BARCODE_SERIAL = serial_mst.TODAY_BARCODE_SERIAL + pintTotalSerialCount;

                                DB.SaveChanges();
                                dbTransaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                dbTransaction.Rollback();
                                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
                            }
                        }
                    }
                }
                //else if (pstrBarcodeType.ToUpper() == "QC_LBL")
                //{
                //    using (AI_BTS_DENSOEntities1 DB = new Model.AI_BTS_DENSOEntities1())
                //    {
                //        QC_LABEL_PRINTING qc_lbl = DB.QC_LABEL_PRINTING.Where(x => DbFunctions.TruncateTime(x.QC_ON.Value) == DbFunctions.TruncateTime(DateTime.Today)).ToList().LastOrDefault();
                //        if (qc_lbl != null)
                //            lstrSerialNo = Convert.ToInt64(qc_lbl.TODAY_BARCODE_SERIAL) + 1;
                //        else
                //            lstrSerialNo = 1;
                //    }
                //}
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return lintSerialNo;
        }


        /// <summary>
        /// Use to execute any command in database using data adapter. Also used to execute stored Procedure
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public DataTable getTable(string qry)
        {
            DataTable dt = new DataTable();
            try
            {
                using (AI_BTS_DENSOEntities1 DB = new Model.AI_BTS_DENSOEntities1())
                {
                    var connction = DB.Database.Connection;
                    {
                        var command = connction.CreateCommand();
                        command.CommandText = qry;
                        SqlDataAdapter adapt = new SqlDataAdapter(command.CommandText, command.Connection.ConnectionString);
                        adapt.Fill(dt);
                    }
                }
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
                throw new Exception("Error Occured during database Operation. Message: " + ex.Message);
            }
            return dt;
        }


        public void ClearDataGridViewRows(ref DataGridView pDgv)
        {
            try
            {
                if (pDgv.Rows.Count >= 0)
                    pDgv.DataSource = null;
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        public bool IsCurrentPartCoil(string pstrPartNo)
        {
            bool lblnIsCurrPartCoil = false;
            try
            {
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    MATERIAL_MST mat_mst = db.MATERIAL_MST.Where(x => x.PART_NO == pstrPartNo).ToList().FirstOrDefault();
                    if (mat_mst!=null)
                    {
                        if(mat_mst.PART_NAME.ToString().ToUpper().IndexOf("COIL") >= 0)
                        {
                            lblnIsCurrPartCoil = true;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }

            return lblnIsCurrPartCoil;
        }


        public QC_LABEL_PRINTING Get_QC_Label(QC_DTL pqc_dtl,ref long lintSerial,DataGridViewRow currRow,string pstrANoticeNo, int pintQty, int pintPack_Size, DateTime pqc_on, int pintBarCodeType)
        {
            string lstrCurrSerial = "";
            QC_LABEL_PRINTING lqc_lbl = null;
            try
            {
                if (lintSerial.ToString().Length < 7)
                    lstrCurrSerial = DateTime.Today.ToString("ddMMyy") + String.Format("{0:00000}", lintSerial++);
                else
                    lstrCurrSerial = (lintSerial++).ToString();


                string lstrCurrBarCode = GenerateGRNBarCode(
                        ReplaceNullString(currRow.Cells["PART_NO"].Value),
                        pstrANoticeNo,
                        pintQty.ToString(),
                        lstrCurrSerial,
                        clsCurrentUser.Site_Name.ToString());

                lqc_lbl = new QC_LABEL_PRINTING
                {
                    QC_DTL_ID = pqc_dtl.QC_DTL_ID,
                    PARENT_BARCODE = pqc_dtl.PRIMARY_BARCODE,
                    QC_BARCODE = lstrCurrBarCode,
                    BARCODE_TYPE = pintBarCodeType,
                    QUANTITY = pintQty,
                    PACK_SIZE = pintPack_Size,
                    TODAY_BARCODE_SERIAL = lstrCurrSerial,
                    STATUS = 0, //0: CREATED, 1: PRINTED
                    QC_ON = pqc_on,
                    QC_BY = Convert.ToInt32(clsCurrentUser.Site_MST_ID)
                };
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return lqc_lbl;
        }


        public INVENTORY_DTL Get_Inventory_DTL(INVENTORY_MST pInv_mst,string pstrBarcode, int pintQty)
        {
            INVENTORY_DTL lInv_dtl = null;
            try
            {
                lInv_dtl = new INVENTORY_DTL
                {
                    INV_MST_ID = pInv_mst.INV_MST_ID,
                    P_BARCODE = "",
                    C_BARCODE = pstrBarcode, //grn_lbl[lintCurrBox].PRIMARY_BARCODE, //here since barcode not chaged of grn due to full box approval hence qc barcode will remain same
                    QUANTITY = pintQty,// qc_dtl.QUANTITY,
                    QUANTITY_ISSUED = 0,
                    QUANTITY_REMAINING = pintQty,
                    STATUS = 1
                };
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return lInv_dtl;
        }

        public bool CheckIfPartQuantityExistisInStock(DataGridView pDgvData, string pstrPart_No, int pintRequestedQty,int pintCurrentModifyQty)
        {
            bool lblnStockExists = false;
            try
            {
                int lintQuantityRemaining = 0;
                int lintPreviousRequestedQty = 0;

                //first of all check if current part is lready added in the form or not. if yes then that quantity will also be added in addition to currently requested quantity
                foreach (DataGridViewRow currRow in pDgvData.Rows)
                {
                    if (currRow.Cells["Part_No"].Value != null)
                    {
                        if (currRow.Cells["Part_No"].Value.ToString() == pstrPart_No)
                        {
                            lintPreviousRequestedQty += Convert.ToInt32(currRow.Cells["Quantity"].Value.ToString());
                        }
                    }
                }

                //In case of edit mode
                int lintTotalRequestedQty = lintPreviousRequestedQty + Convert.ToInt32(pintRequestedQty);

                if (pintCurrentModifyQty > 0)
                    lintTotalRequestedQty = lintTotalRequestedQty - pintCurrentModifyQty;


                //using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                //{
                //    var result = db.INVENTORY_MST.GroupBy(o => o.PART_NO).Select(g => new { Part_No = g.Key, Quantity_Remaining = g.Sum(i => i.QUANTITY_REMAINING)})
                //            .Where(x=> x.Part_No == pstrPart_No);
                //    foreach(var group in result)
                //    {
                //        lintQuantityRemaining =Convert.ToInt32(group.Quantity_Remaining);
                //    }
                //}

                DataTable dt = getTable("Select Part_No,Sum(Quantity_Remaining) as Quantity_Remaining from Inventory_MST " +
                    " where Part_No = '" + pstrPart_No + "' AND SITE_MST_ID ='" + clsCurrentUser.Site_MST_ID + "' Group By Part_No");

                if(dt!=null)
                {
                    if(dt.Rows.Count>0)
                    {
                        lintQuantityRemaining=ReplaceNullNumber(dt.Rows[0]["Quantity_Remaining"].ToString());
                    }
                }

                if (lintQuantityRemaining >= lintTotalRequestedQty)
                    lblnStockExists = true;
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return lblnStockExists;
        }
        

        public DateTime FormatDate(string strDate)
        {
            DateTime lrtnDt=DateTime.Parse("01/01/1900");
            try
            {
                DateTime dt;
                if (!DateTime.TryParse(strDate,out dt))
                {
                    string lstrYear = strDate.Substring(0, 4);
                    string lstrMonth = strDate.Substring(4, 2);
                    string lstrDate = strDate.Substring(6, 2);
                    lrtnDt = new DateTime(Convert.ToInt16(lstrYear), Convert.ToInt16(lstrMonth), Convert.ToInt16(lstrDate));
                }
                else
                {
                    lrtnDt =DateTime.Parse(strDate);
                }
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return lrtnDt;
        }

        /// <summary>
        /// Checks if a specidic column (Checkbox Type) in any row of datagridview is checked or not
        /// </summary>
        /// <param name="pGrid">DataGridView to check for column selection</param>
        /// <param name="pstrColumnTocheck">Column need to be checked for selection</param>
        /// <returns>Returns if no row is checked for desired column else returns false</returns>
        public bool IsAnyRowSelected(DataGridView pGrid,string pstrColumnTocheck)
        {
            bool isSelected = false;
            try
            {
                foreach(DataGridViewRow currRow in pGrid.Rows)
                {
                    if (currRow.Cells[pstrColumnTocheck].Value!=null)
                    {
                        if(Boolean.Parse(currRow.Cells[pstrColumnTocheck].Value.ToString()))
                        {
                            isSelected = true;
                            break;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return isSelected;
        }

        public void PrintPallet(string pstrANoticeNo, string pstrPalletNo)
        {
            try
            {
                string str = "SP_GET_PALLET_LIST '" + pstrANoticeNo + "', '" + pstrPalletNo + "'";
                DataTable ldt = getTable(str);

                rptGRN rptgrn = new rptGRN();
                frmPalleteInvoiceReport frmrptViewer = new frmPalleteInvoiceReport(rptgrn, ldt);
                frmrptViewer.ShowDialog();
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + " in Method: " + MethodBase.GetCurrentMethod());
                throw new Exception("Error in printing mixed Pallet:" + ex.Message);
            }
        }



        public void MarkBlockedPart(ref DataGridView pDgv, bool pblnCheckRemainingPrint)
        {
            try
            {
                foreach(DataGridViewRow currRow in pDgv.Rows)
                {
                    if(currRow.Cells[pDgv.Columns[0].Name].Value != null)
                    {

                        if (currRow.Cells["Is_Block"].Value.ToString() == "1")
                            currRow.DefaultCellStyle.BackColor = Color.Orange;

                        //==================================================================================================================
                        //Mark row for part whose all labels have been printed
                        //==================================================================================================================
                        //check in BarCodeLabel Printing if any box of current part is remaining for  quantity. If there is no part remaining for quantity
                        if (pblnCheckRemainingPrint)
                        {
                            long lngGrnDtlID = Convert.ToInt64(currRow.Cells["GRN_DTL_ID"].Value.ToString());
                            using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                            {
                                GRN_DTL tmpdtl = db.GRN_DTL.Where(x => x.GRN_DTL_ID == lngGrnDtlID && x.STATUS == 0).ToList().FirstOrDefault();
                                if (tmpdtl == null)
                                {
                                    currRow.DefaultCellStyle.BackColor = Color.Yellow;
                                }
                            }
                        }
                        //==================================================================================================================
                    }
                }
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        public int GetAccessValue(bool chkVal)
        {
            int lintRtnVal = 0;
            try
            {
                if (chkVal)
                    lintRtnVal = 1;
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return lintRtnVal;

        }

        public bool SetAccessValue(int AccessVal)
        {
            bool lblnRtnVal = false;
            try
            {
                if (AccessVal == 1)
                    lblnRtnVal = true;
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return lblnRtnVal;

        }

        private void ShowToolTip(Form currForm)
        {
            try
            {
                ToolTip ttNew = new ToolTip();
                ToolTip ttSave = new ToolTip();
                ToolTip ttDelete = new ToolTip();

                if (currForm.Controls["pnlMain"].Controls["headerPanel2"].Controls["btnSave"] != null)
                    ttSave.SetToolTip(currForm.Controls["pnlMain"].Controls["headerPanel2"].Controls["btnSave"], "Save");

                if (currForm.Controls["pnlMain"].Controls["headerPanel2"].Controls["btnAdd"] != null)
                    ttNew.SetToolTip(currForm.Controls["pnlMain"].Controls["headerPanel2"].Controls["btnAdd"], "New");

                if (currForm.Controls["pnlMain"].Controls["headerPanel2"].Controls["btnDel"] != null)
                    ttDelete.SetToolTip(currForm.Controls["pnlMain"].Controls["headerPanel2"].Controls["btnDel"], "Delete");
                //if (currForm.Controls["pnlMain"].Controls["headerPanel2"].Controls["btnDel"] != null)
                //    currForm.Controls["pnlMain"].Controls["headerPanel2"].Controls["btnDel"].Enabled = clsCurrentUser.DeleteAccess; ;
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        public  void GetUserAccessForCurrentScreen(Form currFom,string pstrCurrScreenName)
        {
            try
            {
                ShowToolTip(currFom);

                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    var query = from a in db.USER_ROLE_MST
                                join b in db.USER_ROLE_ACCESS_MST on a.USER_ROLE_MST_ID equals b.USER_ROLE_MST_ID
                                where a.USER_ROLE_MST_ID == clsCurrentUser.User_Role_MST_ID && b.SCREEN_NAME == pstrCurrScreenName //clsCurrentUser.User_Role_MST_ID
                                select new
                                {
                                    SCREEN_NAME = b.SCREEN_NAME,
                                    READ_ACCESS = b.READ_ACCESS,
                                    ADD_ACCESS = b.ADD_ACCESS,
                                    DELETE_ACCESS = b.DELETE_ACCESS
                                };

                    var result = query.ToList();

                    if (result.Count > 0)
                    {
                        clsCurrentUser.ReadAccess =  SetAccessValue(Convert.ToInt16(result[0].READ_ACCESS.ToString()));
                        clsCurrentUser.AddAccess = SetAccessValue(Convert.ToInt16(result[0].ADD_ACCESS.ToString()));
                        clsCurrentUser.DeleteAccess = SetAccessValue(Convert.ToInt16(result[0].DELETE_ACCESS.ToString()));

                        if (!clsCurrentUser.ReadAccess)
                        {
                            ClsMessage.ShowReadAccessErrorMessage();
                            currFom.Close();
                        }

                        if (currFom.Controls["pnlMain"].Controls["headerPanel2"].Controls["btnSave"] !=null)
                            currFom.Controls["pnlMain"].Controls["headerPanel2"].Controls["btnSave"].Enabled = clsCurrentUser.AddAccess;

                        if (currFom.Controls["pnlMain"].Controls["headerPanel2"].Controls["btnDel"] != null)
                            currFom.Controls["pnlMain"].Controls["headerPanel2"].Controls["btnDel"].Enabled = clsCurrentUser.DeleteAccess; ;
                    }
                    else
                    {
                        ClsMessage.ShowReadAccessErrorMessage();
                        currFom.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        public void AddHeaderCheckBoxToDataGrid(ref DataGridView pDgv, int LocX=48,int LocY=15)
        {
            try
            {
                CheckBox headerCheckBox = new CheckBox();

                //Find the Location of Header Cell.
                Point headerCellLocation = pDgv.GetCellDisplayRectangle(0, -1, true).Location;

                //Place the Header CheckBox in the Location of the Header Cell.
                headerCheckBox.Location = new Point(headerCellLocation.X + LocX, headerCellLocation.Y + LocY);
                headerCheckBox.Name = "chkHeader";
                headerCheckBox.BackColor = Color.Transparent;
                headerCheckBox.Size = new Size(18, 18);
                
                //Assign Click event to the Header CheckBox.

                headerCheckBox.CheckedChanged += new System.EventHandler(headerCheckBox_CheckedChanged);
                pDgv.Controls.Add(headerCheckBox);
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }


        private void headerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkHeader = (CheckBox)sender;
                DataGridView lDgv = (DataGridView) chkHeader.Parent;
                lDgv.EndEdit();

                foreach(DataGridViewRow currRow in lDgv.Rows)
                {
                    //select only those rows whose labels are remaining for printing and which are not blocked
                    if(currRow.DefaultCellStyle.BackColor != Color.Yellow && currRow.DefaultCellStyle.BackColor != Color.Orange)
                    {
                        currRow.Cells[lDgv.CurrentCell.OwningColumn.Name].Value = chkHeader.Checked;
                    }
                }

                //in GRN Form if checkbox is checked and in gridview any multipart mixed pallet is checked  then print label option must be mixed pallet
                Form frmgrn = (Form) chkHeader.Parent.Parent.Parent.Parent.Parent;
                if (frmgrn.Name=="frmGRN")
                {
                    if(chkHeader.Checked)
                    {
                        if(CheckIfMultiPartMixPalletSelected(lDgv))
                        {
                            RadioButton rdb = (RadioButton)frmgrn.Controls["pnlMain"].Controls["pnlDGVnControls"].Controls["Panel4"].Controls["groupBox2"].Controls["optMixedPalletPrint"];
                            if (rdb != null)
                                rdb.Checked = true;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }


        /// <summary>
        /// Checks if any such mix pallet is selected which contains multiple parts in GRN Form for printing
        /// </summary>
        /// <param name="pDgv">Data grid view containing records</param>
        /// <returns>Returns true if any mix pallet with multiple part is selected</returns>
        public bool CheckIfMultiPartMixPalletSelected(DataGridView pDgv)
        {
            bool isChecked = false;
            Hashtable htSelectedMixPallet = new Hashtable();
            try
            {
                foreach(DataGridViewRow currDGRow in pDgv.Rows)
                {
                    if(ReplaceNullString(currDGRow.Cells["PALLETE_NO"].Value)!="")
                    {
                        if (ReplaceNullString(currDGRow.Cells["chkPrint"].Value) != "")
                        {
                            if (Boolean.Parse(ReplaceNullString(currDGRow.Cells["chkPrint"].Value)) == true)
                            {
                                if (!htSelectedMixPallet.ContainsKey(ReplaceNullString(currDGRow.Cells["PALLETE_NO"].Value)))
                                    htSelectedMixPallet.Add(ReplaceNullString(currDGRow.Cells["PALLETE_NO"].Value), "1");
                                else
                                {
                                    isChecked = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return isChecked;
        }

        public string GetConfigKey(string pstrKey)
        {
            string lstrVal = "";
            try
            {
                if (ConfigurationSettings.AppSettings[pstrKey] != null)
                    lstrVal = ReplaceNullString( ConfigurationSettings.AppSettings[pstrKey].ToString());
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return lstrVal;
        }


        public SatoPrinter InitiateSatoPrinter()
        {
            SatoPrinter rtnPrinter=null;
            try
            {
                rtnPrinter= new SatoPrinter(SATOPrinterAPI.Printer.InterfaceType.TCPIP, GetConfigKey("PrinterIP"), GetConfigKey("PrinterPort"));
            }
            catch(Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return rtnPrinter;
        }


        public void SearchData(ref DataGridView pDgv,string pstrTableName, string pstrColumnToSearch,string pstrTextToSearch)
        {
            DataTable dt = new DataTable();
            try
            {
                if (pstrColumnToSearch == "")
                    pstrColumnToSearch = pDgv.Columns[0].DataPropertyName;


                string lstrQry = "Select * from " + pstrTableName;
                if (pstrTextToSearch != "")
                { 
                    lstrQry += " where " + pstrColumnToSearch + " like '" + pstrTextToSearch.Replace("'","''") + "%'";
                }
                dt = getTable(lstrQry);
                pDgv.DataSource = dt;

            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        /// <summary>
        /// Checks if all records of a grid are selected
        /// </summary>
        /// <param name="pDgv">DatagridView to check for all records selection</param>
        /// <param name="pstrColTocheckIfBlank">Column name of a grid view to check if a row is not blank</param>
        /// <param name="pstrChkcolName">Column Name containing Checkbox to select/desect the row</param>
        /// <returns>Returns True if all parts are selected</returns>
        public bool CheckIfAllPartSelected(ref DataGridView pDgv, string pstrColTocheckIfBlank, string pstrChkcolName)
        {
            bool blnChecked = true;
            try
            {
                foreach (DataGridViewRow currRow in pDgv.Rows)
                {
                    if (currRow.Cells[pstrChkcolName].Value != null)
                    {
                        if (Boolean.Parse(currRow.Cells[pstrChkcolName].Value.ToString()) == false)
                        {
                            blnChecked = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return blnChecked;
        }

        public bool CheckIfNoPartSelected(ref DataGridView pDgv, string pstrColTocheckIfBlank, string pstrChkcolName)
        {
            bool blnChecked = true;
            try
            {
                foreach (DataGridViewRow currRow in pDgv.Rows)
                {
                    if (currRow.Cells[pstrColTocheckIfBlank].Value != null)
                    {
                        if (Boolean.Parse(currRow.Cells[pstrChkcolName].Value.ToString()) == true)
                        {
                            blnChecked = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return blnChecked;
        }
    }
}

