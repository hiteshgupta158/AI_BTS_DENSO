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
    public partial class frmLogin : Form
    {
        USER_MST user_mst = new USER_MST();
        clsCommon common = new clsCommon();
        ZTCrypto.ZTCrypto crypto = new ZTCrypto.ZTCrypto();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {


                if (txtLoginID.Text.Trim() == "" || txtLoginID.Text.Trim() == null)
                {
                    ClsMessage.ShowError("Please enter the Login ID first.");
                    txtLoginID.Focus();
                }
                if (txtPassword.Text.Trim() == "" || txtPassword.Text.Trim() == null)
                {
                    ClsMessage.ShowError("Please enter the Password first.");
                    txtPassword.Focus();
                }
                if (cmbSite.Text.Trim() == "")
                {
                    ClsMessage.ShowError("Please select the user first.");
                    cmbSite.Focus();
                }
                else
                {
                    using (AI_BTS_DENSOEntities1 db = new AI_BTS_DENSOEntities1())
                    {
                        string pwd = crypto.Encrypt(txtPassword.Text.Trim());
                        int lintUsersite = Convert.ToInt16(cmbSite.SelectedValue.ToString());

                        var result = (from a in db.USER_MST
                                      join b in db.USER_ROLE_MST on a.USER_ROLE_MST_ID equals b.USER_ROLE_MST_ID
                                      join c in db.SITE_MST on a.SITE_MST_ID equals c.SITE_MST_ID
                                      where (a.LOGIN_ID == txtLoginID.Text.Trim() && a.USER_PASSWORD == pwd)
                                   select new
                                   {
                                       USER_MST_ID = a.USER_MST_ID,
                                       Login_ID = a.LOGIN_ID,
                                       USER_Name = a.USER_NAME,
                                       SITE_MST_ID = a.SITE_MST_ID,
                                       SITE_NAME = c.SITE_NAME,
                                       USER_ROLE_MST_ID = a.USER_ROLE_MST_ID,
                                       USER_ROLE = b.USER_ROLE
                                   }).ToList();

                        if(result.Count == 0)
                        {
                            ClsMessage.ShowError("Invalid credentials. Please enter correct Login ID/Password.");
                            txtLoginID.Focus();
                        }
                        else if(result[0].SITE_MST_ID != lintUsersite)
                        {
                            ClsMessage.ShowError("User is not authorised for selected site.");
                            cmbSite.Focus();
                        }
                        else
                        {
                            clsCurrentUser.User_MST_ID = result[0].USER_MST_ID;
                            clsCurrentUser.Login_ID = result[0].Login_ID;
                            clsCurrentUser.User_Name = result[0].USER_Name;
                            clsCurrentUser.User_Role_MST_ID =Convert.ToInt16(result[0].USER_ROLE_MST_ID.ToString());
                            clsCurrentUser.User_Role = result[0].USER_ROLE;
                            clsCurrentUser.Site_MST_ID = Convert.ToInt16(result[0].SITE_MST_ID);
                            clsCurrentUser.Site_Name = result[0].SITE_NAME;


                            frmMain frmmain = new frmMain();
                            frmmain.ShowDialog();
                            this.Close();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ClsMessage.ShowError("An error occurred while trying to login. Please contact your site administrator.");
                common.WriteLog("Error Message: " + ex.Message + " Inner Excception " + ex.InnerException + ex.StackTrace.Substring(0, ex.StackTrace.IndexOf("(")));
            }
        }

        private void txtLoginID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtPassword.Focus();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                cmbSite.Focus();
        }
        
        private void frmLogin_Load(object sender, EventArgs e)
        {
            common.FillSite(ref cmbSite);
        }

        private void cmbSite_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnLogin_Click(sender, e);
        }
    }
}
