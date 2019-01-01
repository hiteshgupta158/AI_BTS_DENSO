namespace AI_BTS_DENSO
{
    partial class frmQualityCheck
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
            this.pnlHeaderControl = new System.Windows.Forms.Panel();
            this.cmbQCType = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtQCDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.lblANoticeDate = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtANoticeDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtANoticeNo = new System.Windows.Forms.TextBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlGridBottomControls = new System.Windows.Forms.Panel();
            this.pnlBottomControls = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlDataGridView = new System.Windows.Forms.Panel();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.optNone = new System.Windows.Forms.RadioButton();
            this.optAll = new System.Windows.Forms.RadioButton();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.GRN_DTL_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IS_Block = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QC_MST_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Part_Level_Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Part_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Part_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pallete_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Supplier_Batch_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pack_Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity_Remaining = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QC_Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlHeaderControl.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlGridBottomControls.SuspendLayout();
            this.pnlBottomControls.SuspendLayout();
            this.pnlDataGridView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeaderControl
            // 
            this.pnlHeaderControl.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeaderControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlHeaderControl.Controls.Add(this.cmbQCType);
            this.pnlHeaderControl.Controls.Add(this.label4);
            this.pnlHeaderControl.Controls.Add(this.dtQCDate);
            this.pnlHeaderControl.Controls.Add(this.label6);
            this.pnlHeaderControl.Controls.Add(this.lblANoticeDate);
            this.pnlHeaderControl.Controls.Add(this.label2);
            this.pnlHeaderControl.Controls.Add(this.dtInvoiceDate);
            this.pnlHeaderControl.Controls.Add(this.label5);
            this.pnlHeaderControl.Controls.Add(this.dtANoticeDate);
            this.pnlHeaderControl.Controls.Add(this.label3);
            this.pnlHeaderControl.Controls.Add(this.label8);
            this.pnlHeaderControl.Controls.Add(this.txtANoticeNo);
            this.pnlHeaderControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeaderControl.Location = new System.Drawing.Point(0, 0);
            this.pnlHeaderControl.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.pnlHeaderControl.Name = "pnlHeaderControl";
            this.pnlHeaderControl.Size = new System.Drawing.Size(1117, 56);
            this.pnlHeaderControl.TabIndex = 0;
            // 
            // cmbQCType
            // 
            this.cmbQCType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cmbQCType.Location = new System.Drawing.Point(119, 17);
            this.cmbQCType.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.cmbQCType.Name = "cmbQCType";
            this.cmbQCType.Size = new System.Drawing.Size(184, 26);
            this.cmbQCType.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 18);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "QC Type";
            // 
            // dtQCDate
            // 
            this.dtQCDate.CustomFormat = "dd-MMM-yyyy";
            this.dtQCDate.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dtQCDate.Enabled = false;
            this.dtQCDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtQCDate.Location = new System.Drawing.Point(984, 10);
            this.dtQCDate.MinDate = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.dtQCDate.Name = "dtQCDate";
            this.dtQCDate.Size = new System.Drawing.Size(122, 26);
            this.dtQCDate.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(895, 15);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 20);
            this.label6.TabIndex = 15;
            this.label6.Text = "QC Date";
            // 
            // lblANoticeDate
            // 
            this.lblANoticeDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblANoticeDate.Location = new System.Drawing.Point(742, 13);
            this.lblANoticeDate.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblANoticeDate.Name = "lblANoticeDate";
            this.lblANoticeDate.Size = new System.Drawing.Size(147, 26);
            this.lblANoticeDate.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(629, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "A Notice Date";
            // 
            // dtInvoiceDate
            // 
            this.dtInvoiceDate.CustomFormat = "dd-MMM-yyyy";
            this.dtInvoiceDate.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dtInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtInvoiceDate.Location = new System.Drawing.Point(1378, 77);
            this.dtInvoiceDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtInvoiceDate.MinDate = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.dtInvoiceDate.Name = "dtInvoiceDate";
            this.dtInvoiceDate.Size = new System.Drawing.Size(277, 26);
            this.dtInvoiceDate.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1206, 84);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Invoice Date";
            // 
            // dtANoticeDate
            // 
            this.dtANoticeDate.CustomFormat = "dd-MMM-yyyy";
            this.dtANoticeDate.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dtANoticeDate.Enabled = false;
            this.dtANoticeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtANoticeDate.Location = new System.Drawing.Point(1378, 21);
            this.dtANoticeDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtANoticeDate.MinDate = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.dtANoticeDate.Name = "dtANoticeDate";
            this.dtANoticeDate.Size = new System.Drawing.Size(277, 26);
            this.dtANoticeDate.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1206, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "A Notice Date";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(333, 18);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "A Notice No.";
            // 
            // txtANoticeNo
            // 
            this.txtANoticeNo.Location = new System.Drawing.Point(453, 14);
            this.txtANoticeNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtANoticeNo.Name = "txtANoticeNo";
            this.txtANoticeNo.Size = new System.Drawing.Size(156, 26);
            this.txtANoticeNo.TabIndex = 0;
            this.txtANoticeNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtANoticeNo_KeyPress);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMain.Controls.Add(this.pnlGridBottomControls);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.pnlHeaderControl);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlMain.Location = new System.Drawing.Point(0, 1);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1121, 688);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlGridBottomControls
            // 
            this.pnlGridBottomControls.BackColor = System.Drawing.Color.Transparent;
            this.pnlGridBottomControls.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlGridBottomControls.Controls.Add(this.pnlBottomControls);
            this.pnlGridBottomControls.Controls.Add(this.pnlDataGridView);
            this.pnlGridBottomControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlGridBottomControls.Location = new System.Drawing.Point(0, 112);
            this.pnlGridBottomControls.Name = "pnlGridBottomControls";
            this.pnlGridBottomControls.Size = new System.Drawing.Size(1117, 572);
            this.pnlGridBottomControls.TabIndex = 1;
            // 
            // pnlBottomControls
            // 
            this.pnlBottomControls.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlBottomControls.Controls.Add(this.btnCancel);
            this.pnlBottomControls.Controls.Add(this.btnSave);
            this.pnlBottomControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottomControls.Location = new System.Drawing.Point(0, 516);
            this.pnlBottomControls.Name = "pnlBottomControls";
            this.pnlBottomControls.Size = new System.Drawing.Size(1113, 52);
            this.pnlBottomControls.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(225)))));
            this.btnCancel.Location = new System.Drawing.Point(999, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 33);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(225)))));
            this.btnSave.Location = new System.Drawing.Point(896, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 33);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlDataGridView
            // 
            this.pnlDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlDataGridView.Controls.Add(this.dgvData);
            this.pnlDataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDataGridView.Location = new System.Drawing.Point(0, 0);
            this.pnlDataGridView.Name = "pnlDataGridView";
            this.pnlDataGridView.Size = new System.Drawing.Size(1113, 475);
            this.pnlDataGridView.TabIndex = 0;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GRN_DTL_ID,
            this.IS_Block,
            this.QC_MST_ID,
            this.Part_Level_Quantity,
            this.Status,
            this.chkSelect,
            this.Part_No,
            this.Part_Name,
            this.Pallete_No,
            this.Supplier_Batch_No,
            this.Pack_Size,
            this.Barcode,
            this.Quantity,
            this.Quantity_Remaining,
            this.QC_Status});
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(0, 0);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1109, 471);
            this.dgvData.TabIndex = 0;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.dateTimePicker2);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.dateTimePicker3);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 56);
            this.panel1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1117, 46);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "Select Part";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.optNone);
            this.panel2.Controls.Add(this.optAll);
            this.panel2.Location = new System.Drawing.Point(119, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(194, 37);
            this.panel2.TabIndex = 0;
            // 
            // optNone
            // 
            this.optNone.AutoSize = true;
            this.optNone.Location = new System.Drawing.Point(103, 6);
            this.optNone.Name = "optNone";
            this.optNone.Size = new System.Drawing.Size(65, 24);
            this.optNone.TabIndex = 2;
            this.optNone.TabStop = true;
            this.optNone.Text = "None";
            this.optNone.UseVisualStyleBackColor = true;
            this.optNone.CheckedChanged += new System.EventHandler(this.optNone_CheckedChanged);
            this.optNone.Click += new System.EventHandler(this.optNone_Click);
            // 
            // optAll
            // 
            this.optAll.AutoSize = true;
            this.optAll.Checked = true;
            this.optAll.Location = new System.Drawing.Point(9, 7);
            this.optAll.Name = "optAll";
            this.optAll.Size = new System.Drawing.Size(44, 24);
            this.optAll.TabIndex = 1;
            this.optAll.TabStop = true;
            this.optAll.Text = "All";
            this.optAll.UseVisualStyleBackColor = true;
            this.optAll.CheckedChanged += new System.EventHandler(this.optAll_CheckedChanged);
            this.optAll.Click += new System.EventHandler(this.optAll_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "dd-MMM-yyyy";
            this.dateTimePicker2.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(1378, 77);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker2.MinDate = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(277, 26);
            this.dateTimePicker2.TabIndex = 6;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1206, 84);
            this.label14.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(98, 20);
            this.label14.TabIndex = 4;
            this.label14.Text = "Invoice Date";
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.CustomFormat = "dd-MMM-yyyy";
            this.dateTimePicker3.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dateTimePicker3.Enabled = false;
            this.dateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker3.Location = new System.Drawing.Point(1378, 21);
            this.dateTimePicker3.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker3.MinDate = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(277, 26);
            this.dateTimePicker3.TabIndex = 5;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1206, 29);
            this.label15.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(108, 20);
            this.label15.TabIndex = 4;
            this.label15.Text = "A Notice Date";
            // 
            // GRN_DTL_ID
            // 
            this.GRN_DTL_ID.DataPropertyName = "GRN_DTL_ID";
            this.GRN_DTL_ID.HeaderText = "GRN DTL ID";
            this.GRN_DTL_ID.Name = "GRN_DTL_ID";
            this.GRN_DTL_ID.ReadOnly = true;
            this.GRN_DTL_ID.Visible = false;
            // 
            // IS_Block
            // 
            this.IS_Block.DataPropertyName = "Is_block";
            this.IS_Block.HeaderText = "Is Block";
            this.IS_Block.Name = "IS_Block";
            this.IS_Block.ReadOnly = true;
            this.IS_Block.Visible = false;
            // 
            // QC_MST_ID
            // 
            this.QC_MST_ID.DataPropertyName = "QC_MST_ID";
            this.QC_MST_ID.HeaderText = "QC MST ID";
            this.QC_MST_ID.Name = "QC_MST_ID";
            this.QC_MST_ID.ReadOnly = true;
            this.QC_MST_ID.Visible = false;
            // 
            // Part_Level_Quantity
            // 
            this.Part_Level_Quantity.DataPropertyName = "Part_Level_Quantity";
            this.Part_Level_Quantity.HeaderText = "Part Level Qty";
            this.Part_Level_Quantity.Name = "Part_Level_Quantity";
            this.Part_Level_Quantity.ReadOnly = true;
            this.Part_Level_Quantity.Visible = false;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Visible = false;
            // 
            // chkSelect
            // 
            this.chkSelect.HeaderText = "";
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.ReadOnly = true;
            this.chkSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.chkSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.chkSelect.Width = 25;
            // 
            // Part_No
            // 
            this.Part_No.DataPropertyName = "Part_No";
            this.Part_No.HeaderText = "Part No";
            this.Part_No.Name = "Part_No";
            this.Part_No.ReadOnly = true;
            this.Part_No.Width = 150;
            // 
            // Part_Name
            // 
            this.Part_Name.DataPropertyName = "Part_Name";
            this.Part_Name.HeaderText = "Part Name";
            this.Part_Name.Name = "Part_Name";
            this.Part_Name.ReadOnly = true;
            this.Part_Name.Width = 200;
            // 
            // Pallete_No
            // 
            this.Pallete_No.DataPropertyName = "PALLETE_NO";
            this.Pallete_No.HeaderText = "Pallet No";
            this.Pallete_No.Name = "Pallete_No";
            this.Pallete_No.ReadOnly = true;
            this.Pallete_No.Width = 170;
            // 
            // Supplier_Batch_No
            // 
            this.Supplier_Batch_No.DataPropertyName = "Supplier_Batch_No";
            this.Supplier_Batch_No.HeaderText = "Supplier Code";
            this.Supplier_Batch_No.Name = "Supplier_Batch_No";
            this.Supplier_Batch_No.ReadOnly = true;
            this.Supplier_Batch_No.Width = 140;
            // 
            // Pack_Size
            // 
            this.Pack_Size.DataPropertyName = "Pack_Size";
            this.Pack_Size.HeaderText = "Pack Size";
            this.Pack_Size.Name = "Pack_Size";
            this.Pack_Size.ReadOnly = true;
            this.Pack_Size.Width = 125;
            // 
            // Barcode
            // 
            this.Barcode.DataPropertyName = "Primary_Barcode";
            this.Barcode.HeaderText = "Barcode";
            this.Barcode.Name = "Barcode";
            this.Barcode.ReadOnly = true;
            this.Barcode.Visible = false;
            this.Barcode.Width = 300;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            this.Quantity.HeaderText = "Total Qty";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 120;
            // 
            // Quantity_Remaining
            // 
            this.Quantity_Remaining.DataPropertyName = "Quantity_Remaining";
            this.Quantity_Remaining.HeaderText = "Remaining Qty";
            this.Quantity_Remaining.Name = "Quantity_Remaining";
            this.Quantity_Remaining.ReadOnly = true;
            this.Quantity_Remaining.Width = 150;
            // 
            // QC_Status
            // 
            this.QC_Status.DataPropertyName = "QC_Status";
            this.QC_Status.HeaderText = "QC Status";
            this.QC_Status.Name = "QC_Status";
            this.QC_Status.ReadOnly = true;
            this.QC_Status.Width = 110;
            // 
            // frmQualityCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 689);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmQualityCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quality Check";
            this.Load += new System.EventHandler(this.frmQualityCheck_Load);
            this.pnlHeaderControl.ResumeLayout(false);
            this.pnlHeaderControl.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlGridBottomControls.ResumeLayout(false);
            this.pnlBottomControls.ResumeLayout(false);
            this.pnlDataGridView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeaderControl;
        private System.Windows.Forms.DateTimePicker dtInvoiceDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtANoticeDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtANoticeNo;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblANoticeDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtQCDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel pnlGridBottomControls;
        private System.Windows.Forms.Panel pnlDataGridView;
        private System.Windows.Forms.Panel pnlBottomControls;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label cmbQCType;
        private System.Windows.Forms.RadioButton optNone;
        private System.Windows.Forms.RadioButton optAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn GRN_DTL_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IS_Block;
        private System.Windows.Forms.DataGridViewTextBoxColumn QC_MST_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Part_Level_Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn Part_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Part_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pallete_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Supplier_Batch_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pack_Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn Barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity_Remaining;
        private System.Windows.Forms.DataGridViewTextBoxColumn QC_Status;
    }
}