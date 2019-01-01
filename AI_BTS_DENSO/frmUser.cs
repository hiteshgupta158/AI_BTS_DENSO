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
using System.Text.RegularExpressions;

namespace AI_BTS_DENSO
{
    public partial class frmUser : Form
    {
        Common.clsCommon common = new Common.clsCommon();
        ZTCrypto.ZTCrypto crypto = new ZTCrypto.ZTCrypto();
        USER_MST user_mst = new USER_MST();



        public frmUser()
        {
            InitializeComponent();
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            try 
            {
                common.GetUserAccessForCurrentScreen(this,"USER");

                common.FillUserRole(ref cmbUserRole);
                common.FillSite(ref cmbSite);
                common.FillActive(ref cmbActive);

                RefreshDataGridView();
                Clear();
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveUser();
        }

        private bool ValidateForm()
        {
            bool isValid = false;
            var regexItem = new Regex("^[a-zA-Z0-9]*$");
            try
            {
                if (txtUserName.Text.Trim() == "" || txtUserName.Text.Trim() == null)
                {
                    ClsMessage.ShowError("Please enter the User Name first.");
                    txtUserName.Focus();
                }
                else if (txtLoginID.Text.Trim() == "" || txtLoginID.Text.Trim() == null)
                {
                    ClsMessage.ShowError("Please enter the Login ID first.");
                    txtLoginID.Focus();
                }
                else if (!regexItem.IsMatch(txtLoginID.Text))
                {
                    ClsMessage.ShowError("Please enter the valid Login ID. it cannot contain special character.");
                    txtLoginID.Focus();
                }
                else if (txtPassword.Text.Trim() == "" || txtPassword.Text.Trim() == null)
                {
                    ClsMessage.ShowError("Please enter the Password first.");
                    txtPassword.Focus();
                }
                else if (txtConfirmPassword.Text.Trim() == "" || txtConfirmPassword.Text.Trim() == null)
                {
                    ClsMessage.ShowError("Please enter the Confirm Password first.");
                    txtConfirmPassword.Focus();
                }
                else if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
                {
                    ClsMessage.ShowError("Password and Confirm Password are not same.");
                    txtPassword.Focus();
                }
                else if (cmbUserRole.Text.Trim() == "")
                {
                    ClsMessage.ShowError("Please select the User Role first.");
                    cmbUserRole.Focus();
                }
                else if (cmbSite.Text.Trim() == "")
                {
                    ClsMessage.ShowError("Please select the User Site first.");
                    cmbSite.Focus();
                }
                else if (cmbActive.Text.Trim() == "")
                {
                    ClsMessage.ShowError("Please select the User Active Status first.");
                    cmbActive.Focus();
                }
                else
                    isValid = true;

            }
            catch(Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
            return isValid;
        }

        private void SaveUser()
        {
            try
            {
                if(ValidateForm())
                {
                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        //user_mst = new USER_MST();
                        user_mst.USER_NAME = txtUserName.Text.Trim();
                        user_mst.LOGIN_ID = txtLoginID.Text.Trim();
                        user_mst.USER_PASSWORD = crypto.Encrypt(txtPassword.Text.Trim());
                        user_mst.USER_ROLE_MST_ID = Convert.ToInt16(cmbUserRole.SelectedValue.ToString());
                        user_mst.SITE_MST_ID = Convert.ToInt16(cmbSite.SelectedValue.ToString());
                        user_mst.ACTIVE = Convert.ToInt16(cmbActive.SelectedValue.ToString());


                        var res = (from a in db.USER_MST where (a.LOGIN_ID == txtLoginID.Text.Trim() && a.USER_MST_ID != user_mst.USER_MST_ID)
                                   select new
                                   {
                                        Login_ID = a.LOGIN_ID
                                   }).ToList().Count;

                        if (res > 0)
                        {
                            ClsMessage.ShowError("This Login ID already exists. Please enter different Login ID.");
                            return;
                        }

                        bool lstrAddAction = false;
                        if (user_mst.USER_MST_ID == 0)
                        { 
                            db.USER_MST.Add(user_mst);
                            lstrAddAction = true;
                        }
                        else
                            db.Entry(user_mst).State = System.Data.Entity.EntityState.Modified;

                        db.SaveChanges();
                        ClsMessage.ShowDataSavedConfirmation(lstrAddAction);
                        Clear();
                        RefreshDataGridView();
                    }
                }   
            }
            catch(Exception ex)
            {
                ClsMessage.ShowError(ClsMessage.DATA_NOT_SAVE_ERROR_MESSAGE);
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        /// <summary>
        /// Refreshes the data grid view with Machine table data from database.
        /// </summary>
        private void RefreshDataGridView()
        {
            try
            {
                dgvData.AutoGenerateColumns = false;
                using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                {
                    var query = from a in db.USER_MST
                                join b in db.USER_ROLE_MST on a.USER_ROLE_MST_ID equals b.USER_ROLE_MST_ID
                                join c in db.ACTIVE_MST on a.ACTIVE equals c.ACTIVE_MST_ID
                                join d in db.SITE_MST on a.SITE_MST_ID equals d.SITE_MST_ID
                                select new
                                {
                                    User_MST_ID = a.USER_MST_ID,
                                    USER_NAME = a.USER_NAME,
                                    LOGIN_ID = a.LOGIN_ID,
                                    USER_ROLE = b.USER_ROLE,
                                    USER_ROLE_MST_ID = b.USER_ROLE_MST_ID,
                                    USER_PASSWORD = a.USER_PASSWORD,
                                    SITE_NAME = d.SITE_NAME,
                                    SITE_MST_ID = d.SITE_MST_ID,
                                    ACTIVE_MST_ID = c.ACTIVE_MST_ID,
                                    ACTIVE_VALUE = c.ACTIVE_VALUE
                                };

                    var result = query.ToList();

                    dgvData.DataSource = result;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Clear()
        {
            try
            {
                txtUserName.Text = "";
                txtLoginID.Text = "";
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";
                cmbUserRole.SelectedIndex = -1;
                cmbSite.SelectedIndex = -1;
                cmbActive.SelectedIndex = -1;
                user_mst.USER_MST_ID = 0;
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
                    user_mst.USER_MST_ID = Convert.ToInt32(dgvData.CurrentRow.Cells["User_MST_ID"].Value);

                    //set currently selected record's value in form controls
                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        user_mst = db.USER_MST.Where(x => x.USER_MST_ID == user_mst.USER_MST_ID).FirstOrDefault();
                        txtUserName.Text = user_mst.USER_NAME;
                        txtLoginID.Text = user_mst.LOGIN_ID;
                        txtPassword.Text = crypto.Decrypt(user_mst.USER_PASSWORD);
                        txtConfirmPassword.Text = crypto.Decrypt(user_mst.USER_PASSWORD);
                        cmbUserRole.SelectedValue = user_mst.USER_ROLE_MST_ID;
                        cmbSite.SelectedValue = user_mst.SITE_MST_ID;
                        cmbActive.SelectedValue = user_mst.ACTIVE;
                    }
                }
            }
            catch (Exception ex)
            {
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (user_mst.USER_MST_ID == 0)
                    ClsMessage.ShowNoRecordSelectedErrorMessage();
                else
                {
                    if (common.ConfirmToDelete())
                    {

                        using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                        {
                            if (db.Entry(user_mst).State == System.Data.Entity.EntityState.Detached)
                                db.USER_MST.Attach(user_mst);

                            db.USER_MST.Remove(user_mst);
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
                ClsMessage.ShowDeleteOperationErrorMessage();
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }
    }
}
