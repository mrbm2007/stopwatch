using System;
using System.Drawing;
using System.Windows.Forms;

namespace stopwatch
{
    public class MyTextBox : TextBox
    {
        public MyTextBox()
            : base()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            BorderColor = Color.Black;
        }
        public Color BorderColor
        {
            get; set;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var cr = e.ClipRectangle;
            using (var penBorder = new Pen(BorderColor, 1))
            {
                var rectBorder = new Rectangle(cr.X, cr.Y, cr.Width - 1, cr.Height - 1);
                e.Graphics.DrawRectangle(penBorder, rectBorder);

                var textRec = new Rectangle(cr.X + 1, cr.Y + 2, cr.Width - 1, cr.Height - 1);
                TextRenderer.DrawText(e.Graphics, Text, Font, textRec, ForeColor, BackColor, 
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.Top);
            }
        }
    }
}
