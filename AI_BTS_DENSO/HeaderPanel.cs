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
    public partial class HeaderPanel : Panel
    {
        public HeaderPanel()
        {
            InitializeComponent();
        }

        private void HeaderPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            if (this.ClientRectangle.Height > 0 && this.ClientRectangle.Width > 0)
            {
                LinearGradientBrush lgb = new LinearGradientBrush(this.ClientRectangle, Color.Transparent, Color.FromArgb(50, Color.Blue), LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(lgb, this.ClientRectangle);
            }
        }
    }
}
