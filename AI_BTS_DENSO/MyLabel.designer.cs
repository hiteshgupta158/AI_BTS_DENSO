namespace AccessPark
{
    partial class MyLabel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MyLabel
            // 
            this.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Size = new System.Drawing.Size(200, 50);
            this.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.MouseLeave += new System.EventHandler(this.MyLabel_MouseLeave);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MyLabel_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MyLabel_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MyLabel_MouseUp);
            this.MouseEnter += new System.EventHandler(this.MyLabel_MouseEnter);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
