namespace AI_BTS_DENSO
{
    partial class frmFinishedGoods
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFinishedGoods));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.headerPanel2 = new AccessPark.HeaderPanel();
            this.btnExp = new System.Windows.Forms.Button();
            this.btmImp = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblAssetLastModifiedDate = new System.Windows.Forms.Label();
            this.pnlDgv = new System.Windows.Forms.Panel();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.FG_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FG_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FG_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.txtFGDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFGType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFGName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.headerPanel2.SuspendLayout();
            this.pnlDgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.pnlControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMain.Controls.Add(this.headerPanel2);
            this.pnlMain.Controls.Add(this.pnlDgv);
            this.pnlMain.Controls.Add(this.pnlControl);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(891, 405);
            this.pnlMain.TabIndex = 0;
            // 
            // headerPanel2
            // 
            this.headerPanel2.BackColor = System.Drawing.Color.Silver;
            this.headerPanel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("headerPanel2.BackgroundImage")));
            this.headerPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.headerPanel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.headerPanel2.Controls.Add(this.btnExp);
            this.headerPanel2.Controls.Add(this.btmImp);
            this.headerPanel2.Controls.Add(this.btnSave);
            this.headerPanel2.Controls.Add(this.txtSearch);
            this.headerPanel2.Controls.Add(this.lblSearch);
            this.headerPanel2.Controls.Add(this.btnDel);
            this.headerPanel2.Controls.Add(this.btnAdd);
            this.headerPanel2.Controls.Add(this.lblAssetLastModifiedDate);
            this.headerPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel2.Location = new System.Drawing.Point(0, 0);
            this.headerPanel2.Name = "headerPanel2";
            this.headerPanel2.Size = new System.Drawing.Size(887, 52);
            this.headerPanel2.TabIndex = 1;
            // 
            // btnExp
            // 
            this.btnExp.BackColor = System.Drawing.Color.White;
            this.btnExp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExp.BackgroundImage")));
            this.btnExp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExp.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExp.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExp.Location = new System.Drawing.Point(192, 0);
            this.btnExp.Name = "btnExp";
            this.btnExp.Size = new System.Drawing.Size(48, 48);
            this.btnExp.TabIndex = 4;
            this.btnExp.UseVisualStyleBackColor = false;
            this.btnExp.Visible = false;
            // 
            // btmImp
            // 
            this.btmImp.BackColor = System.Drawing.Color.White;
            this.btmImp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btmImp.BackgroundImage")));
            this.btmImp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btmImp.Dock = System.Windows.Forms.DockStyle.Left;
            this.btmImp.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btmImp.Location = new System.Drawing.Point(144, 0);
            this.btmImp.Name = "btmImp";
            this.btmImp.Size = new System.Drawing.Size(48, 48);
            this.btmImp.TabIndex = 3;
            this.btmImp.UseVisualStyleBackColor = false;
            this.btmImp.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSave.Location = new System.Drawing.Point(96, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(48, 48);
            this.btnSave.TabIndex = 0;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(713, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(167, 20);
            this.txtSearch.TabIndex = 5;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.Navy;
            this.lblSearch.Location = new System.Drawing.Point(664, 5);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(43, 15);
            this.lblSearch.TabIndex = 23;
            this.lblSearch.Text = "Search";
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.Color.White;
            this.btnDel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDel.BackgroundImage")));
            this.btnDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDel.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDel.Location = new System.Drawing.Point(48, 0);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(48, 48);
            this.btnDel.TabIndex = 2;
            this.btnDel.UseVisualStyleBackColor = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.White;
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAdd.Location = new System.Drawing.Point(0, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(48, 48);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblAssetLastModifiedDate
            // 
            this.lblAssetLastModifiedDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAssetLastModifiedDate.AutoSize = true;
            this.lblAssetLastModifiedDate.BackColor = System.Drawing.Color.Transparent;
            this.lblAssetLastModifiedDate.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssetLastModifiedDate.ForeColor = System.Drawing.Color.Navy;
            this.lblAssetLastModifiedDate.Location = new System.Drawing.Point(428, 5);
            this.lblAssetLastModifiedDate.Name = "lblAssetLastModifiedDate";
            this.lblAssetLastModifiedDate.Size = new System.Drawing.Size(0, 15);
            this.lblAssetLastModifiedDate.TabIndex = 26;
            // 
            // pnlDgv
            // 
            this.pnlDgv.BackColor = System.Drawing.Color.Transparent;
            this.pnlDgv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlDgv.Controls.Add(this.dgvData);
            this.pnlDgv.Location = new System.Drawing.Point(353, 58);
            this.pnlDgv.Name = "pnlDgv";
            this.pnlDgv.Size = new System.Drawing.Size(526, 333);
            this.pnlDgv.TabIndex = 2;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dgvData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FG_Name,
            this.Description,
            this.FG_TYPE,
            this.FG_ID});
            this.dgvData.GridColor = System.Drawing.Color.White;
            this.dgvData.Location = new System.Drawing.Point(13, 3);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(501, 323);
            this.dgvData.TabIndex = 0;
            this.dgvData.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_ColumnHeaderMouseClick);
            this.dgvData.DoubleClick += new System.EventHandler(this.dgvData_DoubleClick);
            // 
            // FG_Name
            // 
            this.FG_Name.DataPropertyName = "FG_Name";
            this.FG_Name.HeaderText = "Finished Goods Name";
            this.FG_Name.Name = "FG_Name";
            this.FG_Name.ReadOnly = true;
            this.FG_Name.Width = 191;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // FG_TYPE
            // 
            this.FG_TYPE.DataPropertyName = "FG_TYPE";
            this.FG_TYPE.HeaderText = "Finished Goods Type";
            this.FG_TYPE.Name = "FG_TYPE";
            this.FG_TYPE.ReadOnly = true;
            // 
            // FG_ID
            // 
            this.FG_ID.DataPropertyName = "FG_MST_ID";
            this.FG_ID.HeaderText = "Finished Goods ID";
            this.FG_ID.Name = "FG_ID";
            this.FG_ID.ReadOnly = true;
            this.FG_ID.Visible = false;
            // 
            // pnlControl
            // 
            this.pnlControl.BackColor = System.Drawing.Color.Transparent;
            this.pnlControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlControl.Controls.Add(this.txtFGDescription);
            this.pnlControl.Controls.Add(this.label1);
            this.pnlControl.Controls.Add(this.cmbFGType);
            this.pnlControl.Controls.Add(this.label3);
            this.pnlControl.Controls.Add(this.txtFGName);
            this.pnlControl.Controls.Add(this.label2);
            this.pnlControl.Location = new System.Drawing.Point(6, 58);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(341, 333);
            this.pnlControl.TabIndex = 0;
            this.pnlControl.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlControl_Paint);
            // 
            // txtFGDescription
            // 
            this.txtFGDescription.Location = new System.Drawing.Point(135, 128);
            this.txtFGDescription.MaxLength = 0;
            this.txtFGDescription.Multiline = true;
            this.txtFGDescription.Name = "txtFGDescription";
            this.txtFGDescription.Size = new System.Drawing.Size(192, 92);
            this.txtFGDescription.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(11, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Description";
            // 
            // cmbFGType
            // 
            this.cmbFGType.BackColor = System.Drawing.Color.White;
            this.cmbFGType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFGType.FormattingEnabled = true;
            this.cmbFGType.Items.AddRange(new object[] {
            "Finished",
            "Intermediate"});
            this.cmbFGType.Location = new System.Drawing.Point(137, 93);
            this.cmbFGType.Name = "cmbFGType";
            this.cmbFGType.Size = new System.Drawing.Size(190, 21);
            this.cmbFGType.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(13, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Finished Goods Type";
            // 
            // txtFGName
            // 
            this.txtFGName.Location = new System.Drawing.Point(137, 62);
            this.txtFGName.MaxLength = 0;
            this.txtFGName.Name = "txtFGName";
            this.txtFGName.Size = new System.Drawing.Size(190, 20);
            this.txtFGName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(13, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Finished Goods Name";
            // 
            // frmFinishedGoods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 405);
            this.Controls.Add(this.pnlMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFinishedGoods";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FinishedGoods";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmFinishedGoods_FormClosed);
            this.Load += new System.EventHandler(this.frmFinishedGoods_Load);
            this.pnlMain.ResumeLayout(false);
            this.headerPanel2.ResumeLayout(false);
            this.headerPanel2.PerformLayout();
            this.pnlDgv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.pnlControl.ResumeLayout(false);
            this.pnlControl.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Panel pnlDgv;
        private AccessPark.HeaderPanel headerPanel2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblAssetLastModifiedDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFGName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExp;
        private System.Windows.Forms.Button btmImp;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.ComboBox cmbFGType;
        private System.Windows.Forms.TextBox txtFGDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn FinishedGoods_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn FG_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn FG_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn FG_ID;
    }
}