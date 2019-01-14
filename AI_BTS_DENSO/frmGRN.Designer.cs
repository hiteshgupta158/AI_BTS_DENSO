namespace AI_BTS_DENSO
{
    partial class frmGRN
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlDGVnControls = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.optMixedPalletPrint = new System.Windows.Forms.RadioButton();
            this.optIndividualPrint = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblYellow = new System.Windows.Forms.Label();
            this.lblOrange = new System.Windows.Forms.Label();
            this.lblRed = new System.Windows.Forms.Label();
            this.btnPrintBarcode = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlDgv = new System.Windows.Forms.Panel();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.GRN_DTL_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Is_Block = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkPrint = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Part_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Part_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pallete_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pack_Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Supplier_Batch_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.No_Of_Print = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Block = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optANoticeNo = new System.Windows.Forms.RadioButton();
            this.optInvoiceNo = new System.Windows.Forms.RadioButton();
            this.btnSigma = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtStoNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtANoticeDate = new System.Windows.Forms.DateTimePicker();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtANoticeNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbGRNType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMaterialSource = new System.Windows.Forms.Label();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.grnPrintMode = new System.Windows.Forms.GroupBox();
            this.optDirect = new System.Windows.Forms.RadioButton();
            this.optPreview = new System.Windows.Forms.RadioButton();
            this.pnlMain.SuspendLayout();
            this.pnlDGVnControls.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnlDgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grnPrintMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMain.Controls.Add(this.pnlDGVnControls);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(4);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1121, 624);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlDGVnControls
            // 
            this.pnlDGVnControls.BackColor = System.Drawing.Color.Transparent;
            this.pnlDGVnControls.Controls.Add(this.panel4);
            this.pnlDGVnControls.Controls.Add(this.pnlDgv);
            this.pnlDGVnControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDGVnControls.Location = new System.Drawing.Point(0, 98);
            this.pnlDGVnControls.Name = "pnlDGVnControls";
            this.pnlDGVnControls.Size = new System.Drawing.Size(1117, 522);
            this.pnlDGVnControls.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.grnPrintMode);
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.lblYellow);
            this.panel4.Controls.Add(this.lblOrange);
            this.panel4.Controls.Add(this.lblRed);
            this.panel4.Controls.Add(this.btnPrintBarcode);
            this.panel4.Controls.Add(this.btnCancel);
            this.panel4.Controls.Add(this.btnSave);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 439);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1117, 83);
            this.panel4.TabIndex = 2;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.optMixedPalletPrint);
            this.groupBox2.Controls.Add(this.optIndividualPrint);
            this.groupBox2.Location = new System.Drawing.Point(280, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(244, 51);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Label Print Type";
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
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(29, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(130, 20);
            this.label10.TabIndex = 14;
            this.label10.Text = "No Label To Print";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(29, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 20);
            this.label9.TabIndex = 13;
            this.label9.Text = "Blocked Part";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(29, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(139, 20);
            this.label8.TabIndex = 12;
            this.label8.Text = "Non-Available Part";
            // 
            // lblYellow
            // 
            this.lblYellow.BackColor = System.Drawing.Color.Yellow;
            this.lblYellow.Location = new System.Drawing.Point(11, 57);
            this.lblYellow.Name = "lblYellow";
            this.lblYellow.Size = new System.Drawing.Size(10, 10);
            this.lblYellow.TabIndex = 11;
            // 
            // lblOrange
            // 
            this.lblOrange.BackColor = System.Drawing.Color.DarkOrange;
            this.lblOrange.Location = new System.Drawing.Point(11, 34);
            this.lblOrange.Name = "lblOrange";
            this.lblOrange.Size = new System.Drawing.Size(10, 10);
            this.lblOrange.TabIndex = 10;
            // 
            // lblRed
            // 
            this.lblRed.BackColor = System.Drawing.Color.Red;
            this.lblRed.Location = new System.Drawing.Point(10, 11);
            this.lblRed.Name = "lblRed";
            this.lblRed.Size = new System.Drawing.Size(10, 10);
            this.lblRed.TabIndex = 9;
            // 
            // btnPrintBarcode
            // 
            this.btnPrintBarcode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(225)))));
            this.btnPrintBarcode.Enabled = false;
            this.btnPrintBarcode.Location = new System.Drawing.Point(787, 25);
            this.btnPrintBarcode.Name = "btnPrintBarcode";
            this.btnPrintBarcode.Size = new System.Drawing.Size(114, 33);
            this.btnPrintBarcode.TabIndex = 7;
            this.btnPrintBarcode.Text = "Print Barcode";
            this.btnPrintBarcode.UseVisualStyleBackColor = false;
            this.btnPrintBarcode.Click += new System.EventHandler(this.btnPrintBarcode_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(225)))));
            this.btnCancel.Location = new System.Drawing.Point(1010, 25);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 33);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(225)))));
            this.btnSave.Location = new System.Drawing.Point(907, 25);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 33);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlDgv
            // 
            this.pnlDgv.BackColor = System.Drawing.Color.Transparent;
            this.pnlDgv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlDgv.Controls.Add(this.dgvData);
            this.pnlDgv.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDgv.Location = new System.Drawing.Point(0, 0);
            this.pnlDgv.Margin = new System.Windows.Forms.Padding(4);
            this.pnlDgv.Name = "pnlDgv";
            this.pnlDgv.Size = new System.Drawing.Size(1117, 432);
            this.pnlDgv.TabIndex = 1;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            this.dgvData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GRN_DTL_ID,
            this.STATUS,
            this.Is_Block,
            this.UOM,
            this.chkPrint,
            this.Part_No,
            this.Part_Name,
            this.Pallete_No,
            this.Pack_Size,
            this.Supplier_Batch_No,
            this.Quantity,
            this.No_Of_Print,
            this.Block});
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvData.EnableHeadersVisualStyles = false;
            this.dgvData.GridColor = System.Drawing.Color.White;
            this.dgvData.Location = new System.Drawing.Point(0, 0);
            this.dgvData.Margin = new System.Windows.Forms.Padding(4);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1113, 428);
            this.dgvData.TabIndex = 7;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            this.dgvData.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellEnter);
            // 
            // GRN_DTL_ID
            // 
            this.GRN_DTL_ID.DataPropertyName = "GRN_DTL_ID";
            this.GRN_DTL_ID.HeaderText = "GRN DTL  ID";
            this.GRN_DTL_ID.Name = "GRN_DTL_ID";
            this.GRN_DTL_ID.ReadOnly = true;
            this.GRN_DTL_ID.Visible = false;
            // 
            // STATUS
            // 
            this.STATUS.DataPropertyName = "STATUS";
            this.STATUS.HeaderText = "Status";
            this.STATUS.Name = "STATUS";
            this.STATUS.ReadOnly = true;
            this.STATUS.Visible = false;
            // 
            // Is_Block
            // 
            this.Is_Block.DataPropertyName = "IS_BLOCK";
            this.Is_Block.HeaderText = "Is block";
            this.Is_Block.Name = "Is_Block";
            this.Is_Block.ReadOnly = true;
            this.Is_Block.Visible = false;
            // 
            // UOM
            // 
            this.UOM.DataPropertyName = "UOM";
            this.UOM.HeaderText = "UOM";
            this.UOM.Name = "UOM";
            this.UOM.ReadOnly = true;
            this.UOM.Visible = false;
            // 
            // chkPrint
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle6.NullValue = false;
            this.chkPrint.DefaultCellStyle = dataGridViewCellStyle6;
            this.chkPrint.HeaderText = "";
            this.chkPrint.Name = "chkPrint";
            this.chkPrint.ReadOnly = true;
            this.chkPrint.Width = 25;
            // 
            // Part_No
            // 
            this.Part_No.DataPropertyName = "Part_No";
            this.Part_No.HeaderText = "Part No";
            this.Part_No.Name = "Part_No";
            this.Part_No.ReadOnly = true;
            this.Part_No.Width = 180;
            // 
            // Part_Name
            // 
            this.Part_Name.DataPropertyName = "Part_Name";
            this.Part_Name.HeaderText = "Part Name";
            this.Part_Name.Name = "Part_Name";
            this.Part_Name.ReadOnly = true;
            this.Part_Name.Width = 250;
            // 
            // Pallete_No
            // 
            this.Pallete_No.DataPropertyName = "PALLETE_NO";
            this.Pallete_No.HeaderText = "Pallete No";
            this.Pallete_No.Name = "Pallete_No";
            this.Pallete_No.ReadOnly = true;
            this.Pallete_No.Width = 130;
            // 
            // Pack_Size
            // 
            this.Pack_Size.DataPropertyName = "Pack_Size";
            this.Pack_Size.HeaderText = "Pack Size";
            this.Pack_Size.Name = "Pack_Size";
            this.Pack_Size.ReadOnly = true;
            this.Pack_Size.Width = 110;
            // 
            // Supplier_Batch_No
            // 
            this.Supplier_Batch_No.DataPropertyName = "Supplier_Batch_No";
            this.Supplier_Batch_No.HeaderText = "Supplier Batch No";
            this.Supplier_Batch_No.Name = "Supplier_Batch_No";
            this.Supplier_Batch_No.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 80;
            // 
            // No_Of_Print
            // 
            this.No_Of_Print.HeaderText = "No Of Barcode To Print";
            this.No_Of_Print.Name = "No_Of_Print";
            this.No_Of_Print.ReadOnly = true;
            this.No_Of_Print.Width = 140;
            // 
            // Block
            // 
            this.Block.HeaderText = "Block/Unblock Part";
            this.Block.Name = "Block";
            this.Block.ReadOnly = true;
            this.Block.Text = "Block/Unblock";
            this.Block.UseColumnTextForButtonValue = true;
            this.Block.Visible = false;
            this.Block.Width = 120;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnSigma);
            this.panel1.Controls.Add(this.btnBrowse);
            this.panel1.Controls.Add(this.txtStoNo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dtInvoiceDate);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dtANoticeDate);
            this.panel1.Controls.Add(this.txtInvoiceNo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtANoticeNo);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cmbGRNType);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblMaterialSource);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1117, 91);
            this.panel1.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(91, 15);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(519, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "*";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optANoticeNo);
            this.groupBox1.Controls.Add(this.optInvoiceNo);
            this.groupBox1.Location = new System.Drawing.Point(264, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(151, 76);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Material Source";
            // 
            // optANoticeNo
            // 
            this.optANoticeNo.AutoSize = true;
            this.optANoticeNo.Checked = true;
            this.optANoticeNo.Location = new System.Drawing.Point(6, 19);
            this.optANoticeNo.Name = "optANoticeNo";
            this.optANoticeNo.Size = new System.Drawing.Size(111, 24);
            this.optANoticeNo.TabIndex = 1;
            this.optANoticeNo.TabStop = true;
            this.optANoticeNo.Text = "A Notice No";
            this.optANoticeNo.UseVisualStyleBackColor = true;
            this.optANoticeNo.CheckedChanged += new System.EventHandler(this.optANoticeNo_CheckedChanged);
            // 
            // optInvoiceNo
            // 
            this.optInvoiceNo.AutoSize = true;
            this.optInvoiceNo.Location = new System.Drawing.Point(6, 46);
            this.optInvoiceNo.Name = "optInvoiceNo";
            this.optInvoiceNo.Size = new System.Drawing.Size(101, 24);
            this.optInvoiceNo.TabIndex = 2;
            this.optInvoiceNo.Text = "Invoice No";
            this.optInvoiceNo.UseVisualStyleBackColor = true;
            this.optInvoiceNo.CheckedChanged += new System.EventHandler(this.optInvoiceNo_CheckedChanged);
            // 
            // btnSigma
            // 
            this.btnSigma.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(225)))));
            this.btnSigma.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSigma.Location = new System.Drawing.Point(771, 13);
            this.btnSigma.Name = "btnSigma";
            this.btnSigma.Size = new System.Drawing.Size(59, 26);
            this.btnSigma.TabIndex = 5;
            this.btnSigma.Text = "Cigma";
            this.btnSigma.UseVisualStyleBackColor = false;
            this.btnSigma.Click += new System.EventHandler(this.btnSigma_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(225)))));
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(692, 14);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(76, 26);
            this.btnBrowse.TabIndex = 9;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtStoNo
            // 
            this.txtStoNo.Location = new System.Drawing.Point(112, 49);
            this.txtStoNo.Name = "txtStoNo";
            this.txtStoNo.Size = new System.Drawing.Size(145, 26);
            this.txtStoNo.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "STO No.";
            // 
            // dtInvoiceDate
            // 
            this.dtInvoiceDate.CustomFormat = "dd-MMM-yyyy";
            this.dtInvoiceDate.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dtInvoiceDate.Enabled = false;
            this.dtInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtInvoiceDate.Location = new System.Drawing.Point(946, 50);
            this.dtInvoiceDate.MinDate = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.dtInvoiceDate.Name = "dtInvoiceDate";
            this.dtInvoiceDate.Size = new System.Drawing.Size(159, 26);
            this.dtInvoiceDate.TabIndex = 6;
            this.dtInvoiceDate.ValueChanged += new System.EventHandler(this.dtInvoiceDate_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(834, 55);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
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
            this.dtANoticeDate.Location = new System.Drawing.Point(946, 14);
            this.dtANoticeDate.MinDate = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.dtANoticeDate.Name = "dtANoticeDate";
            this.dtANoticeDate.Size = new System.Drawing.Size(159, 26);
            this.dtANoticeDate.TabIndex = 5;
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.Enabled = false;
            this.txtInvoiceNo.Location = new System.Drawing.Point(541, 49);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(145, 26);
            this.txtInvoiceNo.TabIndex = 4;
            this.txtInvoiceNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInvoiceNo_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(834, 19);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "A Notice Date";
            // 
            // txtANoticeNo
            // 
            this.txtANoticeNo.Location = new System.Drawing.Point(541, 13);
            this.txtANoticeNo.Name = "txtANoticeNo";
            this.txtANoticeNo.Size = new System.Drawing.Size(145, 26);
            this.txtANoticeNo.TabIndex = 3;
            this.txtANoticeNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtANoticeNo_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 15);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 20);
            this.label6.TabIndex = 2;
            this.label6.Text = "GRN Type";
            // 
            // cmbGRNType
            // 
            this.cmbGRNType.BackColor = System.Drawing.Color.White;
            this.cmbGRNType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGRNType.FormattingEnabled = true;
            this.cmbGRNType.Items.AddRange(new object[] {
            "PO",
            "STO"});
            this.cmbGRNType.Location = new System.Drawing.Point(113, 12);
            this.cmbGRNType.Name = "cmbGRNType";
            this.cmbGRNType.Size = new System.Drawing.Size(144, 28);
            this.cmbGRNType.TabIndex = 0;
            this.cmbGRNType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbGRNType_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(422, 50);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Invoice No.";
            // 
            // lblMaterialSource
            // 
            this.lblMaterialSource.AutoSize = true;
            this.lblMaterialSource.Location = new System.Drawing.Point(422, 14);
            this.lblMaterialSource.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaterialSource.Name = "lblMaterialSource";
            this.lblMaterialSource.Size = new System.Drawing.Size(97, 20);
            this.lblMaterialSource.TabIndex = 0;
            this.lblMaterialSource.Text = "A Notice No.";
            // 
            // ofd
            // 
            this.ofd.FileName = "openFileDialog1";
            // 
            // grnPrintMode
            // 
            this.grnPrintMode.Controls.Add(this.optDirect);
            this.grnPrintMode.Controls.Add(this.optPreview);
            this.grnPrintMode.Location = new System.Drawing.Point(530, 17);
            this.grnPrintMode.Name = "grnPrintMode";
            this.grnPrintMode.Size = new System.Drawing.Size(244, 51);
            this.grnPrintMode.TabIndex = 17;
            this.grnPrintMode.TabStop = false;
            this.grnPrintMode.Text = "Pallet Print Mode";
            // 
            // optDirect
            // 
            this.optDirect.AutoSize = true;
            this.optDirect.Location = new System.Drawing.Point(128, 22);
            this.optDirect.Name = "optDirect";
            this.optDirect.Size = new System.Drawing.Size(105, 24);
            this.optDirect.TabIndex = 2;
            this.optDirect.TabStop = true;
            this.optDirect.Text = "Direct Print";
            this.optDirect.UseVisualStyleBackColor = true;
            this.optDirect.CheckedChanged += new System.EventHandler(this.optDirect_CheckedChanged);
            // 
            // optPreview
            // 
            this.optPreview.AutoSize = true;
            this.optPreview.Checked = true;
            this.optPreview.Location = new System.Drawing.Point(28, 21);
            this.optPreview.Name = "optPreview";
            this.optPreview.Size = new System.Drawing.Size(81, 24);
            this.optPreview.TabIndex = 1;
            this.optPreview.TabStop = true;
            this.optPreview.Text = "Preview";
            this.optPreview.UseVisualStyleBackColor = true;
            // 
            // frmGRN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 624);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGRN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GRN";
            this.Load += new System.EventHandler(this.frmGRN_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlDGVnControls.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pnlDgv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grnPrintMode.ResumeLayout(false);
            this.grnPrintMode.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMaterialSource;
        private System.Windows.Forms.TextBox txtANoticeNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtANoticeDate;
        private System.Windows.Forms.DateTimePicker dtInvoiceDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbGRNType;
        private System.Windows.Forms.Panel pnlDGVnControls;
        private System.Windows.Forms.Panel pnlDgv;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtStoNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Button btnSigma;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton optANoticeNo;
        private System.Windows.Forms.RadioButton optInvoiceNo;
        private System.Windows.Forms.Button btnPrintBarcode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn GRN_DTL_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Is_Block;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOM;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkPrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn Part_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Part_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pallete_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pack_Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn Supplier_Batch_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn No_Of_Print;
        private System.Windows.Forms.DataGridViewButtonColumn Block;
        private System.Windows.Forms.Label lblYellow;
        private System.Windows.Forms.Label lblOrange;
        private System.Windows.Forms.Label lblRed;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton optMixedPalletPrint;
        private System.Windows.Forms.RadioButton optIndividualPrint;
        private System.Windows.Forms.GroupBox grnPrintMode;
        private System.Windows.Forms.RadioButton optDirect;
        private System.Windows.Forms.RadioButton optPreview;
    }
}