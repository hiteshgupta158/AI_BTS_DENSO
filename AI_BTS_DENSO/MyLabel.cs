using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace AccessPark
{
    public partial class MyLabel : Label
    {
        public MyLabel()
        {
            InitializeComponent();
        }

        private void MyLabel_MouseEnter(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            lbl.Font = new Font("Cambria", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            Graphics g = lbl.CreateGraphics();
            lbl.Refresh();
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.DrawRectangle(new Pen(new SolidBrush(Color.Navy)), lbl.ClientRectangle);
            LinearGradientBrush lgb = new LinearGradientBrush(lbl.ClientRectangle, Color.Transparent, Color.FromArgb(100, Color.Blue), LinearGradientMode.Vertical);
            g.FillRectangle(lgb, lbl.ClientRectangle);
        }

        private void MyLabel_MouseLeave(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            lbl.Font = new Font("Cambria", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            lbl.Refresh();
            
        }

        private void MyLabel_MouseDown(object sender, MouseEventArgs e)
        {
            Label lbl = sender as Label;
            Graphics g = lbl.CreateGraphics();
            lbl.Refresh();
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.DrawRectangle(new Pen(new SolidBrush(Color.Navy)), lbl.ClientRectangle);
            LinearGradientBrush lgb = new LinearGradientBrush(lbl.ClientRectangle, Color.Transparent, Color.FromArgb(150,Color.Blue), LinearGradientMode.Vertical);
            g.FillRectangle(lgb, lbl.ClientRectangle);
        }

        private void MyLabel_MouseUp(object sender, MouseEventArgs e)
        {
            Label lbl = sender as Label;
            Graphics g = lbl.CreateGraphics();
            lbl.Refresh();
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.DrawRectangle(new Pen(new SolidBrush(Color.Navy)), lbl.ClientRectangle);
            LinearGradientBrush lgb = new LinearGradientBrush(lbl.ClientRectangle, Color.Transparent, Color.FromArgb(50,Color.Blue), LinearGradientMode.Vertical);
            g.FillRectangle(lgb, lbl.ClientRectangle);
        }

        private void MyLabel_Paint(object sender, PaintEventArgs e)
        {
            Label lbl=(sender as Label);
            
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            Rectangle rect = new Rectangle(lbl.ClientRectangle.X, lbl.ClientRectangle.Y, lbl.ClientRectangle.Width-1, lbl.ClientRectangle.Height);
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Navy)), rect);
            LinearGradientBrush lgb = new LinearGradientBrush(lbl.ClientRectangle, Color.Transparent, Color.FromArgb(50,Color.Blue), LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(lgb, lbl.ClientRectangle);
        }
    }
}
