using System;
using System.Drawing;
using System.Windows.Forms;

namespace stopwatch
{
    public class MyProgressBar : ProgressBar
    {
        public MyProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var rec = e.ClipRectangle;
            var w = rec.Width;
            //if (ProgressBarRenderer.IsSupported)
            //    ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            e.Graphics.FillRectangle(Brushes.WhiteSmoke, 0, 1, rec.Width, rec.Height);

            rec.Width = (int)((((double)Value - (double)Minimum) / ((double)Maximum - (double)Minimum)) * (w - 2)) - 2;
            rec.Height = rec.Height - 1;
            e.Graphics.FillRectangle(Brushes.DarkTurquoise, 1, 1, rec.Width, rec.Height);
            // e.Graphics.FillRectangle(Brushes.LightBlue, 1, 1 + rec.Width, w - rec.Width, rec.Height);
        }
    }
}
