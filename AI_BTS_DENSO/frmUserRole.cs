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
    public partial class frmUserRole : Form
    {
        List<CheckBox> CheckBoxes = new List<CheckBox>();
        List<SCREEN_LIST> Screen_lst;
        USER_ROLE_MST user_role = new USER_ROLE_MST();
        List<USER_ROLE_ACCESS_MST> userRole_Lst = new List<Model.USER_ROLE_ACCESS_MST>();
        USER_ROLE_ACCESS_MST user_role_access = new USER_ROLE_ACCESS_MST();
        //Create instance of common class to use global/common methods
        Common.clsCommon common = new Common.clsCommon();
        string lstrCurrAction = "Add";

        public frmUserRole()
        {
            InitializeComponent();
        }


        private void CreatecheckBox()
        {
            try
            {
                using (AI_BTS_DENSOEntities1 DB = new AI_BTS_DENSOEntities1())
                {
                    Screen_lst = DB.SCREEN_LIST.Where(x=> x.ACTIVE==1).ToList();
                    int lntInitialTop = 50;
                    foreach (SCREEN_LIST currScreen in Screen_lst)
                    {
                        Label lbl =new Label();
                        lbl.Text = currScreen.SCREEN_NAME;
                        lbl.Left = 15;
                        lbl.Top = lntInitialTop;

                        int lntInitialLeft = 235;
                        for (int i = 0; i < 3; i++)
                        {
                            CheckBox chk = new CheckBox();
                            chk.Top = lntInitialTop;
                            chk.Left = lntInitialLeft;
                            if(i==0)
                                chk.Name = "chk" + currScreen.SCREEN_NAME + "Read";
                            else if(i==1)
                                chk.Name = "chk" + currScreen.SCREEN_NAME + "Add";
                            else if(i==2)
                                chk.Name = "chk" + currScreen.SCREEN_NAME + "Delete";
                            chk.Width = 20;

                            this.pnlAccess.Controls.Add(lbl);
                            this.pnlAccess.Controls.Add(chk);
                            CheckBoxes.Add(chk);
                            lntInitialLeft+= 100;
                        }
                        lntInitialTop += 25;
                    }
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }
        private void frmUserRole_Load(object sender, EventArgs e)
        {
            common.GetUserAccessForCurrentScreen(this, "User Role");
            CreatecheckBox();
            common.FillUserRole(ref cmbUserRole);
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //if(lstrCurrAction=="Add")
            //{
            //    txtUserRole.Text = "";
            //    txtDescription.Text = "";
            //    txtUserRole.Visible = false;
            //    common.FillUserRole(ref cmbUserRole);
            //    cmbUserRole.Visible = true;
            //    lstrCurrAction = "Modify";

            //}
            //else
            //{
            //    txtUserRole.Text = "";
            //    txtDescription.Text = "";
            //    txtUserRole.Visible = true;
            //    cmbUserRole.Visible = false;
            //    lstrCurrAction = "Add";
            //    CheckUncheckAllCheckBoxes(false);
            //}

            Clear(lstrCurrAction);
            
        }

        private void Clear(string pstrAction)
        {
            try
            {
                if (lstrCurrAction == "Add")
                {
                    txtUserRole.Text = "";
                    txtDescription.Text = "";
                    txtUserRole.Visible = false;
                    common.FillUserRole(ref cmbUserRole);
                    cmbUserRole.Visible = true;
                    lstrCurrAction = "Modify";

                }
                else
                {
                    txtUserRole.Text = "";
                    txtDescription.Text = "";
                    txtUserRole.Visible = true;
                    cmbUserRole.Visible = false;
                    lstrCurrAction = "Add";
                    CheckUncheckAllCheckBoxes(false);
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveUserARole();
        }

        /// <summary>
        /// Saves User Role and access allowed to this role on the different screens/modules of application.
        /// </summary>
        private void SaveUserARole()
        {
            try
            {
                string lstrUserRole="";

                if (txtUserRole.Visible)
                    lstrUserRole = txtUserRole.Text.Trim();
                else
                    lstrUserRole = cmbUserRole.Text;

                if (lstrUserRole == "" || lstrUserRole == null)
                {
                    ClsMessage.ShowError("Please enter the User Role first.");
                    txtUserRole.Focus();
                }
                else if (txtDescription.Text.Trim() == "" || txtDescription.Text.Trim() == null)
                {
                    ClsMessage.ShowError("Please enter the User Role's description first.");
                    txtDescription.Focus();
                }
                else
                {
                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        using (var DbTransacation = db.Database.BeginTransaction())
                        {
                            try
                            {
                                #region SAVE USER ROLE
                                //=======================================================================================
                                user_role = db.USER_ROLE_MST.Where(x => x.USER_ROLE == lstrUserRole).ToList().FirstOrDefault();
                                if (user_role != null)
                                {
                                    if(lstrCurrAction.ToUpper()=="ADD")
                                    {
                                        ClsMessage.ShowError("Current User role already exists. Please enter different user role.");
                                        return;
                                    }
                                }

                                if(user_role==null)
                                    user_role = new USER_ROLE_MST(); // done by putta user role object was coming as null. thats the reason that error was coming.


                                user_role.USER_ROLE = lstrUserRole;
                                user_role.DESCRIPTION = txtDescription.Text.Trim();

                                if (user_role.USER_ROLE_MST_ID == 0)
                                    db.USER_ROLE_MST.Add(user_role);
                                

                                db.SaveChanges();
                                //=======================================================================================
                                #endregion

                                #region SAVE USER ROLE ACCESS
                                //=======================================================================================
                                //Fetch UserRole_MST_ID from USER_ROLE_MST Table
                                user_role = db.USER_ROLE_MST.Where(x => x.USER_ROLE == lstrUserRole).ToList().FirstOrDefault();


                                foreach (SCREEN_LIST currScreen in Screen_lst)
                                {
                                    USER_ROLE_ACCESS_MST tmp_user_role_accss = db.USER_ROLE_ACCESS_MST.Where(x => x.SCREEN_NAME == currScreen.SCREEN_NAME &&
                                                                                x.USER_ROLE_MST_ID == user_role.USER_ROLE_MST_ID).ToList().FirstOrDefault();


                                    if (tmp_user_role_accss == null)
                                        tmp_user_role_accss = new USER_ROLE_ACCESS_MST();


                                    tmp_user_role_accss.USER_ROLE_MST_ID = user_role.USER_ROLE_MST_ID;
                                    tmp_user_role_accss.SCREEN_NAME = currScreen.SCREEN_NAME;

                                    CheckBox chk = (CheckBox)pnlAccess.Controls["chk" + currScreen.SCREEN_NAME + "Read"];
                                    tmp_user_role_accss.READ_ACCESS = common.GetAccessValue(chk.Checked);

                                    chk = (CheckBox)pnlAccess.Controls["chk" + currScreen.SCREEN_NAME + "Add"];
                                    tmp_user_role_accss.ADD_ACCESS = common.GetAccessValue(chk.Checked);


                                    chk = (CheckBox)pnlAccess.Controls["chk" + currScreen.SCREEN_NAME + "Delete"];
                                    tmp_user_role_accss.DELETE_ACCESS = common.GetAccessValue(chk.Checked);

                                    //check if access details for current user and current screen are saved in database or not/ if saved then modify the current record else add new



                                    if (tmp_user_role_accss.USER_ROLE_ACCESS_MST_ID == 0)
                                        db.USER_ROLE_ACCESS_MST.Add(tmp_user_role_accss);


                                    db.SaveChanges();

                                }
                                //=======================================================================================
                                #endregion

                                DbTransacation.Commit();
                                if (lstrCurrAction.ToUpper() == "ADD")
                                    ClsMessage.ShowDataSavedConfirmation(true);
                                else
                                    ClsMessage.ShowDataSavedConfirmation(false);

                                Clear("Modify");
                            }
                            catch (Exception ex)
                            {
                                ClsMessage.ShowError("Some error occurred during save");
                                DbTransacation.Rollback();
                                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }//end of save function


        private void GetUserRoleData()
        {
            try
            {
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    var qry = from a in db.USER_ROLE_MST
                              join b in db.USER_ROLE_ACCESS_MST on a.USER_ROLE_MST_ID equals b.USER_ROLE_MST_ID
                              join c in db.SCREEN_LIST on b.SCREEN_NAME equals c.SCREEN_NAME
                              where a.USER_ROLE == cmbUserRole.Text && c.ACTIVE==1
                              select new
                              {
                                  USER_ROLE_MST_ID = a.USER_ROLE_MST_ID,
                                  USER_ROLE = a.USER_ROLE,
                                  DESCRIPTION = a.DESCRIPTION,
                                  USEER_ROLE_ACCESS_MST_ID=b.USER_ROLE_ACCESS_MST_ID,
                                  SCREEN_NAME = b.SCREEN_NAME,
                                  READ_ACCESS=b.READ_ACCESS,
                                  ADD_ACCESS = b.ADD_ACCESS,
                                  DELETE_ACCESS = b.DELETE_ACCESS
                              };
                    

                    var result = qry.ToList();
                    if (result.Count > 0)
                    {
                        txtDescription.Text = result[0].DESCRIPTION.ToString();

                        foreach (var screen in result)
                        {
                            CheckBox chk = (CheckBox)pnlAccess.Controls["chk" + screen.SCREEN_NAME + "Read"];
                            chk.Checked = common.SetAccessValue(Convert.ToInt16(screen.READ_ACCESS.ToString()));

                            chk = (CheckBox)pnlAccess.Controls["chk" + screen.SCREEN_NAME + "Add"];
                            chk.Checked = common.SetAccessValue(Convert.ToInt16(screen.ADD_ACCESS.ToString()));

                            chk = (CheckBox)pnlAccess.Controls["chk" + screen.SCREEN_NAME + "Delete"];
                            chk.Checked = common.SetAccessValue(Convert.ToInt16(screen.DELETE_ACCESS.ToString()));
                        }
                    }
                    else
                    {
                        //ClsMessage.ShowInfo("No Record found for current user role");
                        CheckUncheckAllCheckBoxes(false);
                        txtDescription.Text = "";
                    }
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        /// <summary>
        ///Check/Uncheck all checked boxes 
        /// </summary>
        /// <param name="chkStatus">Status to be set for all Checke Boxes</param>
        private void CheckUncheckAllCheckBoxes(bool chkStatus)
        {
            try
            {
                foreach(SCREEN_LIST currScreen in Screen_lst)
                {
                    CheckBox chk = (CheckBox)pnlAccess.Controls["chk" + currScreen.SCREEN_NAME + "Read"];
                    chk.Checked = chkStatus;

                    chk = (CheckBox)pnlAccess.Controls["chk" + currScreen.SCREEN_NAME + "Add"];
                    chk.Checked = chkStatus;

                    chk = (CheckBox)pnlAccess.Controls["chk" + currScreen.SCREEN_NAME + "Delete"];
                    chk.Checked = chkStatus;
                }
            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        private void cmbUserRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetUserRoleData();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            CheckUncheckAllCheckBoxes(true);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {

        }
        
    }
}
