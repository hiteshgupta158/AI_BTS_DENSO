namespace AI_BTS_DENSO
{
    partial class frmReprint
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.optAll = new System.Windows.Forms.RadioButton();
            this.optMixedPalletPrint = new System.Windows.Forms.RadioButton();
            this.optIndividualPrint = new System.Windows.Forms.RadioButton();
            this.txtANoticeNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlGridControl = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.pnlDgv = new System.Windows.Forms.Panel();
            this.pnlPartList = new System.Windows.Forms.Panel();
            this.dgvPartList = new System.Windows.Forms.DataGridView();
            this.chkPrint = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TODAY_BARCODE_SERIAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BR_SERIAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Primary_Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pallete_No_Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Print_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LBL_PRN_CFM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Box_Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.lblAssetLastModifiedDate = new System.Windows.Forms.Label();
            this.GRN_DTL_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Is_Block = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Part_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Part_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pallete_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pack_Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Supplier_Batch_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Printed_Label_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Is_Reprinted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnlGridControl.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlDgv.SuspendLayout();
            this.pnlPartList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMain.Controls.Add(this.pnlTop);
            this.pnlMain.Controls.Add(this.pnlGridControl);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(929, 623);
            this.pnlMain.TabIndex = 0;
            this.pnlMain.Click += new System.EventHandler(this.pnlMain_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Transparent;
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlTop.Controls.Add(this.groupBox2);
            this.pnlTop.Controls.Add(this.txtANoticeNo);
            this.pnlTop.Controls.Add(this.label7);
            this.pnlTop.Location = new System.Drawing.Point(9, 0);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(909, 55);
            this.pnlTop.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.optAll);
            this.groupBox2.Controls.Add(this.optMixedPalletPrint);
            this.groupBox2.Controls.Add(this.optIndividualPrint);
            this.groupBox2.Location = new System.Drawing.Point(409, -1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(298, 51);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Label Print Type";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // optAll
            // 
            this.optAll.AutoSize = true;
            this.optAll.Location = new System.Drawing.Point(245, 22);
            this.optAll.Name = "optAll";
            this.optAll.Size = new System.Drawing.Size(44, 24);
            this.optAll.TabIndex = 3;
            this.optAll.TabStop = true;
            this.optAll.Text = "All";
            this.optAll.UseVisualStyleBackColor = true;
            // 
            // optMixedPalletPrint
            // 
            this.optMixedPalletPrint.AutoSize = true;
            this.optMixedPalletPrint.Location = new System.Drawing.Point(128, 22);
            this.optMixedPalletPrint.Name = "optMixedPalletPrint";
            this.optMixedPalletPrint.Size = new System.Drawing.Size(111, 24);
            this.optMixedPalletPrint.TabIndex = 2;
            this.optMixedPalletPrint.TabStop = true;
            this.optMixedPalletPrint.Text = "Mixed Pallet";
            this.optMixedPalletPrint.UseVisualStyleBackColor = true;
            this.optMixedPalletPrint.CheckedChanged += new System.EventHandler(this.optMixedPalletPrint_CheckedChanged);
            // 
            // optIndividualPrint
            // 
            this.optIndividualPrint.AutoSize = true;
            this.optIndividualPrint.Checked = true;
            this.optIndividualPrint.Location = new System.Drawing.Point(28, 21);
            this.optIndividualPrint.Name = "optIndividualPrint";
            this.optIndividualPrint.Size = new System.Drawing.Size(93, 24);
            this.optIndividualPrint.TabIndex = 1;
            this.optIndividualPrint.TabStop = true;
            this.optIndividualPrint.Text = "Individual";
            this.optIndividualPrint.UseVisualStyleBackColor = true;
            this.optIndividualPrint.CheckedChanged += new System.EventHandler(this.optIndividualPrint_CheckedChanged);
            // 
            // txtANoticeNo
            // 
            this.txtANoticeNo.Location = new System.Drawing.Point(197, 13);
            this.txtANoticeNo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtANoticeNo.MaxLength = 0;
            this.txtANoticeNo.Name = "txtANoticeNo";
            this.txtANoticeNo.Size = new System.Drawing.Size(183, 26);
            this.txtANoticeNo.TabIndex = 0;
            this.txtANoticeNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtANoticeNo_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Navy;
            this.label7.Location = new System.Drawing.Point(10, 16);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(179, 20);
            this.label7.TabIndex = 18;
            this.label7.Text = "A Notice No./Invoice No.";
            // 
            // pnlGridControl
            // 
            this.pnlGridControl.BackColor = System.Drawing.Color.Transparent;
            this.pnlGridControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlGridControl.Controls.Add(this.panel2);
            this.pnlGridControl.Controls.Add(this.pnlDgv);
            this.pnlGridControl.Location = new System.Drawing.Point(9, 65);
            this.pnlGridControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlGridControl.Name = "pnlGridControl";
            this.pnlGridControl.Size = new System.Drawing.Size(909, 549);
            this.pnlGridControl.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 490);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(905, 55);
            this.panel2.TabIndex = 13;
            this.panel2.Enter += new System.EventHandler(this.panel2_Enter);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(225)))));
            this.btnCancel.Location = new System.Drawing.Point(796, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 33);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(225)))));
            this.btnPrint.Location = new System.Drawing.Point(693, 9);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(97, 33);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // pnlDgv
            // 
            this.pnlDgv.BackColor = System.Drawing.Color.Transparent;
            this.pnlDgv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlDgv.Controls.Add(this.pnlPartList);
            this.pnlDgv.Controls.Add(this.dgvData);
            this.pnlDgv.Location = new System.Drawing.Point(0, -2);
            this.pnlDgv.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlDgv.Name = "pnlDgv";
            this.pnlDgv.Size = new System.Drawing.Size(901, 489);
            this.pnlDgv.TabIndex = 1;
            // 
            // pnlPartList
            // 
            this.pnlPartList.Controls.Add(this.dgvPartList);
            this.pnlPartList.Location = new System.Drawing.Point(580, 54);
            this.pnlPartList.Name = "pnlPartList";
            this.pnlPartList.Size = new System.Drawing.Size(314, 426);
            this.pnlPartList.TabIndex = 1;
            this.pnlPartList.Visible = false;
            // 
            // dgvPartList
            // 
            this.dgvPartList.AllowUserToAddRows = false;
            this.dgvPartList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPartList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkPrint,
            this.TODAY_BARCODE_SERIAL,
            this.BR_SERIAL,
            this.Primary_Barcode,
            this.Pallete_No_Barcode,
            this.Print_Type,
            this.LBL_PRN_CFM,
            this.Box_Quantity});
            this.dgvPartList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPartList.Location = new System.Drawing.Point(0, 0);
            this.dgvPartList.Name = "dgvPartList";
            this.dgvPartList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPartList.Size = new System.Drawing.Size(314, 426);
            this.dgvPartList.TabIndex = 0;
            this.dgvPartList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPartList_CellClick);
            this.dgvPartList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPartList_CellContentClick);
            this.dgvPartList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPartList_CellDoubleClick);
            this.dgvPartList.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvPartList_Scroll);
            this.dgvPartList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvPartList_KeyPress);
            this.dgvPartList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvPartList_KeyUp);
            this.dgvPartList.Leave += new System.EventHandler(this.dgvPartList_Leave);
            // 
            // chkPrint
            // 
            this.chkPrint.HeaderText = "";
            this.chkPrint.Name = "chkPrint";
            this.chkPrint.ReadOnly = true;
            this.chkPrint.Width = 20;
            // 
            // TODAY_BARCODE_SERIAL
            // 
            this.TODAY_BARCODE_SERIAL.DataPropertyName = "TODAY_BARCODE_SERIAL";
            this.TODAY_BARCODE_SERIAL.HeaderText = "Today Barcode Serial";
            this.TODAY_BARCODE_SERIAL.Name = "TODAY_BARCODE_SERIAL";
            this.TODAY_BARCODE_SERIAL.ReadOnly = true;
            this.TODAY_BARCODE_SERIAL.Width = 250;
            // 
            // BR_SERIAL
            // 
            this.BR_SERIAL.DataPropertyName = "BR_SERIAL";
            this.BR_SERIAL.HeaderText = "BR SERIAL";
            this.BR_SERIAL.Name = "BR_SERIAL";
            this.BR_SERIAL.ReadOnly = true;
            this.BR_SERIAL.Visible = false;
            // 
            // Primary_Barcode
            // 
            this.Primary_Barcode.DataPropertyName = "Primary_Barcode";
            this.Primary_Barcode.HeaderText = "Primary Barcode";
            this.Primary_Barcode.Name = "Primary_Barcode";
            this.Primary_Barcode.ReadOnly = true;
            this.Primary_Barcode.Visible = false;
            this.Primary_Barcode.Width = 290;
            // 
            // Pallete_No_Barcode
            // 
            this.Pallete_No_Barcode.DataPropertyName = "Pallete_No_Barcode";
            this.Pallete_No_Barcode.HeaderText = "Pallet Barcode";
            this.Pallete_No_Barcode.Name = "Pallete_No_Barcode";
            this.Pallete_No_Barcode.ReadOnly = true;
            this.Pallete_No_Barcode.Visible = false;
            this.Pallete_No_Barcode.Width = 290;
            // 
            // Print_Type
            // 
            this.Print_Type.DataPropertyName = "Print_Type";
            this.Print_Type.HeaderText = "Print Type";
            this.Print_Type.Name = "Print_Type";
            this.Print_Type.ReadOnly = true;
            this.Print_Type.Visible = false;
            // 
            // LBL_PRN_CFM
            // 
            this.LBL_PRN_CFM.DataPropertyName = "LBL_PRN_CFM";
            this.LBL_PRN_CFM.HeaderText = "LBL PRN CFM";
            this.LBL_PRN_CFM.Name = "LBL_PRN_CFM";
            this.LBL_PRN_CFM.ReadOnly = true;
            this.LBL_PRN_CFM.Visible = false;
            // 
            // Box_Quantity
            // 
            this.Box_Quantity.DataPropertyName = "Box_Quantity";
            this.Box_Quantity.HeaderText = "Box Quantity";
            this.Box_Quantity.Name = "Box_Quantity";
            this.Box_Quantity.ReadOnly = true;
            this.Box_Quantity.Visible = false;
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
            this.GRN_DTL_ID,
            this.STATUS,
            this.Is_Block,
            this.Part_No,
            this.Part_Name,
            this.Pallete_No,
            this.Pack_Size,
            this.Supplier_Batch_No,
            this.Quantity,
            this.Printed_Label_Type,
            this.Is_Reprinted});
            this.dgvData.GridColor = System.Drawing.Color.White;
            this.dgvData.Location = new System.Drawing.Point(4, 5);
            this.dgvData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(889, 475);
            this.dgvData.TabIndex = 0;
            this.dgvData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellDoubleClick);
            this.dgvData.Enter += new System.EventHandler(this.dgvData_Enter);
            // 
            // lblAssetLastModifiedDate
            // 
            this.lblAssetLastModifiedDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAssetLastModifiedDate.AutoSize = true;
            this.lblAssetLastModifiedDate.BackColor = System.Drawing.Color.Transparent;
            this.lblAssetLastModifiedDate.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssetLastModifiedDate.ForeColor = System.Drawing.Color.Navy;
            this.lblAssetLastModifiedDate.Location = new System.Drawing.Point(-359, 8);
            this.lblAssetLastModifiedDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAssetLastModifiedDate.Name = "lblAssetLastModifiedDate";
            this.lblAssetLastModifiedDate.Size = new System.Drawing.Size(0, 15);
            this.lblAssetLastModifiedDate.TabIndex = 26;
            // 
            // GRN_DTL_ID
            // 
            this.GRN_DTL_ID.DataPropertyName = "GRN_DTL_ID";
            this.GRN_DTL_ID.HeaderText = "GRN DTL ID";
            this.GRN_DTL_ID.Name = "GRN_DTL_ID";
            this.GRN_DTL_ID.ReadOnly = true;
            this.GRN_DTL_ID.Visible = false;
            // 
            // STATUS
            // 
            this.STATUS.DataPropertyName = "Status";
            this.STATUS.HeaderText = "Status";
            this.STATUS.Name = "STATUS";
            this.STATUS.ReadOnly = true;
            this.STATUS.Visible = false;
            // 
            // Is_Block
            // 
            this.Is_Block.DataPropertyName = "Is_Block";
            this.Is_Block.HeaderText = "Is Block";
            this.Is_Block.Name = "Is_Block";
            this.Is_Block.ReadOnly = true;
            this.Is_Block.Visible = false;
            // 
            // Part_No
            // 
            this.Part_No.DataPropertyName = "Part_No";
            this.Part_No.HeaderText = "Part Number";
            this.Part_No.Name = "Part_No";
            this.Part_No.ReadOnly = true;
            this.Part_No.Width = 200;
            // 
            // Part_Name
            // 
            this.Part_Name.DataPropertyName = "Part_Name";
            this.Part_Name.HeaderText = "Part Name";
            this.Part_Name.Name = "Part_Name";
            this.Part_Name.ReadOnly = true;
            this.Part_Name.Width = 280;
            // 
            // Pallete_No
            // 
            this.Pallete_No.DataPropertyName = "Pallete_No";
            this.Pallete_No.HeaderText = "Pallet No";
            this.Pallete_No.Name = "Pallete_No";
            this.Pallete_No.ReadOnly = true;
            this.Pallete_No.Width = 150;
            // 
            // Pack_Size
            // 
            this.Pack_Size.DataPropertyName = "Pack_Size";
            this.Pack_Size.HeaderText = "Pack Size";
            this.Pack_Size.Name = "Pack_Size";
            this.Pack_Size.ReadOnly = true;
            this.Pack_Size.Width = 130;
            // 
            // Supplier_Batch_No
            // 
            this.Supplier_Batch_No.DataPropertyName = "Supplier_Batch";
            this.Supplier_Batch_No.HeaderText = "Supplier Batch No";
            this.Supplier_Batch_No.Name = "Supplier_Batch_No";
            this.Supplier_Batch_No.ReadOnly = true;
            this.Supplier_Batch_No.Width = 180;
            // 
            // Quantity
            // 
            this.Quantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Quantity.DataPropertyName = "Quantity";
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.MinimumWidth = 100;
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // Printed_Label_Type
            // 
            this.Printed_Label_Type.DataPropertyName = "Print_Type";
            this.Printed_Label_Type.HeaderText = "Printed Label Type";
            this.Printed_Label_Type.Name = "Printed_Label_Type";
            this.Printed_Label_Type.ReadOnly = true;
            this.Printed_Label_Type.Width = 160;
            // 
            // Is_Reprinted
            // 
            this.Is_Reprinted.HeaderText = "Is Reprinted";
            this.Is_Reprinted.Name = "Is_Reprinted";
            this.Is_Reprinted.ReadOnly = true;
            this.Is_Reprinted.Visible = false;
            // 
            // frmReprint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 623);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReprint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reprint";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmReprint_FormClosed);
            this.Load += new System.EventHandler(this.frmReprint_Load);
            this.Click += new System.EventHandler(this.frmReprint_Click);
            this.pnlMain.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pnlGridControl.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlDgv.ResumeLayout(false);
            this.pnlPartList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlGridControl;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.TextBox txtANoticeNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnlDgv;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblAssetLastModifiedDate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel pnlPartList;
        private System.Windows.Forms.DataGridView dgvPartList;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton optMixedPalletPrint;
        private System.Windows.Forms.RadioButton optIndividualPrint;
        private System.Windows.Forms.RadioButton optAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkPrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn TODAY_BARCODE_SERIAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn BR_SERIAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn Primary_Barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pallete_No_Barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Print_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn LBL_PRN_CFM;
        private System.Windows.Forms.DataGridViewTextBoxColumn Box_Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn GRN_DTL_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Is_Block;
        private System.Windows.Forms.DataGridViewTextBoxColumn Part_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Part_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pallete_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pack_Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn Supplier_Batch_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Printed_Label_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Is_Reprinted;
    }
}