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

namespace AI_BTS_DENSO
{
    public partial class frmMain : Form
    {
        int SelectedRowIndex = 0;
        public int SelectedLeftMenuIndex = 0;
        Form currentFormView;
        clsCommon common;
        public frmMain()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            common = new clsCommon();
        }

        private void headerPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Mastertabpnl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblLeftMenu_Click(object sender, EventArgs e)
        {
            //Set the Main form Title with current navigation level
            lblSiteMap.Text = "Batch Traceablity --> " + (sender as Label).Text.Trim();

            //Make the "header panel 2" visible on click of left menu label to add/delete/modify the grid data and dock it to the top
            this.pnlData.Visible = true;
            SelectedLeftMenuIndex = Convert.ToInt16((sender as Label).Tag);

            
            //Initiate object of respected form which is currently selected in left menu
            currentFormView = common.GetCurrentFormView(Convert.ToInt16((sender as Label).Tag));

            currentFormView.ShowDialog();
        }
        
        private void lblSiteMap_Click_1(object sender, EventArgs e)
        {

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //commented for the time being. will enable this code fo go live.
            //DialogResult DR = ClsMessage.ShowConfimation("Do you want to exit?");
            //if (DR == DialogResult.No)
            //{
            //    e.Cancel = true;
            //    return;
            //}
            Application.Exit();
        }
    }
}
