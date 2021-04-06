using System;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace stopwatch
{
    public class DataGridViewDateColumn : DataGridViewColumn
    {

        public DataGridViewDateColumn()
        {
            CellTemplate = new DataGridViewDateCell();
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(DataGridViewDateCell)))
                {
                    throw new InvalidCastException("Must be a DataGridViewDateCell");
                }
                base.CellTemplate = value;
            }
        }
    }
    public class DataGridViewDateCell : DataGridViewImageCell
    {
        public override Type EditType
        {
            get
            {
                return typeof(ComboBox);
            }
        }
        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            return new TextBox() { Text = "D" };
        }
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            graphics.DrawString(DateTime.Now + "", cellStyle.Font,
                new SolidBrush(cellStyle.ForeColor), cellBounds.X, cellBounds.Y);
        }
        public override object Clone()
        {
            DataGridViewDateCell res = base.Clone() as DataGridViewDateCell;
            if (res != null)
            {
            }
            return res;
        }
    }
}
