namespace AI_BTS_DENSO
{
    partial class frmQtyUpdate
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTransferSlipNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAddQty = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAvailableQty = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbPart = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtANoticeNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.panel6);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(453, 412);
            this.pnlMain.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.txtReason);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtTransferSlipNo);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtAddQty);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtAvailableQty);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbPart);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtANoticeNo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(6, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(437, 330);
            this.panel1.TabIndex = 0;
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(181, 244);
            this.txtReason.MaxLength = 250;
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(220, 70);
            this.txtReason.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(17, 250);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Reason";
            // 
            // txtTransferSlipNo
            // 
            this.txtTransferSlipNo.Location = new System.Drawing.Point(181, 199);
            this.txtTransferSlipNo.Name = "txtTransferSlipNo";
            this.txtTransferSlipNo.Size = new System.Drawing.Size(220, 23);
            this.txtTransferSlipNo.TabIndex = 4;
            this.txtTransferSlipNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(17, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Transfer Slip no.";
            // 
            // txtAddQty
            // 
            this.txtAddQty.Location = new System.Drawing.Point(181, 155);
            this.txtAddQty.Name = "txtAddQty";
            this.txtAddQty.Size = new System.Drawing.Size(220, 23);
            this.txtAddQty.TabIndex = 3;
            this.txtAddQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(17, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Add Quantity";
            // 
            // txtAvailableQty
            // 
            this.txtAvailableQty.Location = new System.Drawing.Point(181, 113);
            this.txtAvailableQty.Name = "txtAvailableQty";
            this.txtAvailableQty.ReadOnly = true;
            this.txtAvailableQty.Size = new System.Drawing.Size(220, 23);
            this.txtAvailableQty.TabIndex = 2;
            this.txtAvailableQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(17, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Available Quantity";
            // 
            // cmbPart
            // 
            this.cmbPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPart.FormattingEnabled = true;
            this.cmbPart.Location = new System.Drawing.Point(181, 67);
            this.cmbPart.Name = "cmbPart";
            this.cmbPart.Size = new System.Drawing.Size(220, 24);
            this.cmbPart.TabIndex = 1;
            this.cmbPart.SelectedIndexChanged += new System.EventHandler(this.cmbPart_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(17, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Select Part No.";
            // 
            // txtANoticeNo
            // 
            this.txtANoticeNo.Location = new System.Drawing.Point(181, 27);
            this.txtANoticeNo.Name = "txtANoticeNo";
            this.txtANoticeNo.Size = new System.Drawing.Size(220, 23);
            this.txtANoticeNo.TabIndex = 0;
            this.txtANoticeNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtANoticeNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtANoticeNo_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(17, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "A Notice / Invoice No.";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.btnSave);
            this.panel6.Controls.Add(this.btnCancel);
            this.panel6.Location = new System.Drawing.Point(6, 339);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(437, 63);
            this.panel6.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(225)))));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(152, 9);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(129, 41);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(225)))));
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(292, 10);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(129, 41);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmQtyUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 412);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmQtyUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GRN Qty Update";
            this.Load += new System.EventHandler(this.frmQtyUpdate_Load);
            this.pnlMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtANoticeNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbPart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAvailableQty;
        private System.Windows.Forms.TextBox txtAddQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTransferSlipNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label label6;
    }
}