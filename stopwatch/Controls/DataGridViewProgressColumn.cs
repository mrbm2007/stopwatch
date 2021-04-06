using System;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace stopwatch
{

    public class DataGridViewProgressColumn : DataGridViewImageColumn
    {

        public DataGridViewProgressColumn()
        {
            CellTemplate = new DataGridViewProgressCell();
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
                    !value.GetType().IsAssignableFrom(typeof(DataGridViewProgressCell)))
                {
                    throw new InvalidCastException("Must be a DataGridViewProgressCell");
                }
                base.CellTemplate = value;
            }
        }


        public Color ProgressBarColor
        {
            get
            {

                if (this.ProgressBarCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }
                return this.ProgressBarCellTemplate.ProgressBarColor;

            }
            set
            {

                if (this.ProgressBarCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
                }
                this.ProgressBarCellTemplate.ProgressBarColor = value;
                if (this.DataGridView != null)
                {
                    DataGridViewRowCollection dataGridViewRows = this.DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        DataGridViewProgressCell dataGridViewCell = dataGridViewRow.Cells[this.Index] as DataGridViewProgressCell;
                        if (dataGridViewCell != null)
                        {
                            dataGridViewCell.SetProgressBarColor(rowIndex, value);
                        }
                    }
                    this.DataGridView.InvalidateColumn(this.Index);
                    // TODO: This column and/or grid rows may need to be autosized depending on their
                    //       autosize settings. Call the autosizing methods to autosize the column, rows, 
                    //       column headers / row headers as needed.
                }
            }
        }


        public int minor
        {
            set { ProgressBarCellTemplate.minor = value; }
            get { return ProgressBarCellTemplate.minor; }
        }

        public int major
        {
            set { ProgressBarCellTemplate.major = value; }
            get { return ProgressBarCellTemplate.major; }
        }

        public Color majorColor
        {
            set { ProgressBarCellTemplate.majorColor = value; }
            get { return ProgressBarCellTemplate.majorColor; }
        }
        public Color minorColor
        {
            set { ProgressBarCellTemplate.minorColor = value; }
            get { return ProgressBarCellTemplate.minorColor; }
        }
        public Color footerColor
        {
            set { ProgressBarCellTemplate.footerColor = value; }
            get { return ProgressBarCellTemplate.footerColor; }
        }
        public Color headerColor
        {
            set { ProgressBarCellTemplate.headerColor = value; }
            get { return ProgressBarCellTemplate.headerColor; }
        }


        private DataGridViewProgressCell ProgressBarCellTemplate
        {
            get
            {
                return (DataGridViewProgressCell)this.CellTemplate;
            }
        }
    }

    class DataGridViewProgressCell : DataGridViewImageCell
    {
        // Used to make custom cell consistent with a DataGridViewImageCell
        static Image emptyImage;
        // Used to remember color of the progress bar
        public Color ProgressBarColor = Color.LightBlue;
        public Color headerColor = Color.DarkOrange;
        public Color footerColor = Color.Black;
        public int minor = 6, major = 4;
        public Color minorColor = Color.Gray, majorColor = Color.LightBlue;

        static DataGridViewProgressCell()
        {
            emptyImage = emptyImage ?? new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }
        public DataGridViewProgressCell()
        {
            this.ValueType = typeof(int);
        }
        // Method required to make the Progress Cell consistent with the default Image Cell.
        // The default Image Cell assumes an Image as a value, although the value of the Progress Cell is an int.
        protected override object GetFormattedValue(object value,
            int rowIndex, ref DataGridViewCellStyle cellStyle,
            TypeConverter valueTypeConverter,
            TypeConverter formattedValueTypeConverter,
            DataGridViewDataErrorContexts context)
        {
            return emptyImage;
        }

        /*protected override void Paint(Graphics g,
            Rectangle clipBounds,
            Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates cellState,
            object value, object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            if (value == null)
            {
                if (ProgressBarColors != null && ProgressBarColors.Count == 1)
                {
                    g.FillRectangle(new SolidBrush(ProgressBarColors[0]), cellBounds);
                    return;
                }
                value = 0;
            }
            List<int> Vals = new List<int>();
            try
            {
                Vals = (List<int>)value;
            }
            catch
            {
                Vals.Add(0);
                Vals.Add((int)value);
            }


            Brush foreColorBrush = new SolidBrush(cellStyle.ForeColor);

            // Draws the cell grid
            base.Paint(g, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));

            float posX = cellBounds.X;
            float posY = cellBounds.Y;


            cellStyle.Padding = new Padding(2);
            var x = cellBounds.X + cellStyle.Padding.Left;
            var y = cellBounds.Y + cellStyle.Padding.Top;
            var w = cellBounds.Width - cellStyle.Padding.Right - cellStyle.Padding.Left;
            var h = cellBounds.Height - cellStyle.Padding.Top - cellStyle.Padding.Bottom;
            var brush = new SolidBrush(ProgressBarColor);


            var percent = 0.0;
            if (Vals.Count > 1) percent = Vals[1] / 10.0;

            if (Vals.Count == 2 && Vals[0] == 0)
            {
                if (Vals[1] > 750)
                    brush = new SolidBrush(Color.FromArgb(0x1E, 0xBA, 0x12));
                else if (Vals[1] > 350)
                    brush = new SolidBrush(Color.LightBlue);
                else brush = new SolidBrush(Color.LightSalmon);
            }
            for (int i = 0; i < Vals.Count; i += 2)
            {
                if (Vals[i] < 0)
                {
                    Vals[i + 1] += Vals[i];
                    Vals[i] = -1;
                }
                if (Vals[i] + Vals[i + 1] > 1000)
                    Vals[i + 1] = 1000 - Vals[i];
                var brush_ = brush;
                var h_ = h;
                var y_ = y;
                if (!(Vals.Count == 2 && Vals[0] == 0) && ProgressBarColors != null && ProgressBarColors.Count > i / 2)
                {
                    brush_ = new SolidBrush(ProgressBarColors[i / 2]);
                    if (ProgressBarColors[i / 2] == headerColor)
                    {
                        y_ = cellBounds.Y;
                        h_ = 3;
                    }
                    if (ProgressBarColors[i / 2] == footerColor)
                    {
                        y_ = cellBounds.Y + cellBounds.Height - 7;
                        h_ = 7;
                    }
                }
                // Draw the progress 
                g.FillRectangle(brush_,
                    x + (int)(w * Vals[i] / 1000.0),
                    y_,
                    (Int32)(w * Vals[i + 1] / 1000.0),
                    h_);
            }

            #region
            if (Vals.Count == 2 && Vals[0] == 0)
            {
                float textWidth = TextRenderer.MeasureText(percent + "%", cellStyle.Font).Width;
                float textHeight = TextRenderer.MeasureText(percent + "%", cellStyle.Font).Height;


                posX = cellBounds.X + (cellBounds.Width / 2) - textWidth / 2;
                posY = cellBounds.Y + (cellBounds.Height / 2) - textHeight / 2;
                g.DrawString(percent + "%", cellStyle.Font, foreColorBrush, posX, posY);
            }
            else
            {
                if (major != 0)
                {
                    if (minor != 0)
                    {
                        var brush_ = new SolidBrush(minorColor);
                        for (int i = 1; i < minor * major; i++)
                        {
                            g.FillRectangle(brush_,
                                x + (float)(w * i / (minor * major * 1.0)),
                                cellBounds.Y - cellBounds.Height / 7,
                                1f,
                                2 * (cellBounds.Height / 7));
                        }
                    }
                    {
                        var brush_ = new SolidBrush(majorColor);
                        for (int i = 1; i < major; i++)
                        {
                            g.FillRectangle(brush_,
                                x + (float)(w * i / (1.0 * major)),
                                cellBounds.Y,
                                1,
                                cellBounds.Height);
                        }
                    }
                }
            }
            #endregion
        }*/

        protected override void Paint(Graphics g,
            Rectangle clipBounds,
            Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates cellState,
            object value, object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            // Draws the cell grid
            base.Paint(g, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));

            ProgressMap map = null;
            if (value is ProgressMap)
                map = (ProgressMap)value;
            else if (value is double || value is int)
                map = new ProgressMap()
                {
                    max = 100,
                    Cells = new List<ProgressCell> { new ProgressCell { start = 0, length = Convert.ToDouble(value) } },
                    SimpleProgress = true,
                };
            if (map == null) return;

            float posX = cellBounds.X;
            float posY = cellBounds.Y;

            cellStyle.Padding = new Padding(2);
            var x = cellBounds.X;
            var y = cellBounds.Y;
            var w = cellBounds.Width;
            var h = cellBounds.Height;
            if (!map.SimpleProgress)
            {
                x += cellStyle.Padding.Left;
                y += cellStyle.Padding.Top;
                w -= cellStyle.Padding.Left + cellStyle.Padding.Right;
                h -= cellStyle.Padding.Bottom + cellStyle.Padding.Top;
            }

            foreach (var cell in map.Cells)
            {
                var brush = new SolidBrush(ProgressBarColor);
                try
                {
                    if (map.Cells.Count == 1
                        && map.Cells[0].start == 0
                        && map.SimpleProgress
                        && map.Cells[0].color == null)
                    {
                        brush.Dispose();
                        if (map.Cells[0].length / map.max > 0.750)
                            brush = new SolidBrush(Color.FromArgb(0x1E, 0xBA, 0x12));
                        else if (map.Cells[0].length / map.max > 0.350)
                            brush = new SolidBrush(Color.LightBlue);
                        else
                            brush = new SolidBrush(Color.LightSalmon);
                    }
                    if (cell.start < 0)
                    {
                        cell.length += cell.start;
                        cell.start = -1;
                    }
                    if (cell.start + cell.length > map.max)
                        cell.length = map.max - cell.start;
                    var h_ = h;
                    var y_ = y;
                    if (cell.header)
                    {
                        brush.Dispose();
                        brush = new SolidBrush(headerColor);
                        y_ = cellBounds.Y;
                        h_ = 3;
                    }
                    if (cell.footer)
                    {
                        brush.Dispose();
                        brush = new SolidBrush(footerColor);
                        y_ = cellBounds.Y + cellBounds.Height - 7;
                        h_ = 7;
                    }
                    if (map.color != null)
                    {
                        brush.Dispose();
                        brush = new SolidBrush(map.color.Value);
                    }
                    if (cell.color != null)
                    {
                        brush.Dispose();
                        brush = new SolidBrush(cell.color.Value);
                    }
                    var s = cell.start;
                    var d = cell.length;
                    if (map.RightToLeft)
                        s = map.max - (s + d);
                    // Draw the progress 
                    g.FillRectangle(brush,
                        x + (int)(w * s / map.max),
                        y_,
                        (int)(w * d / map.max),
                        h_);
                }
                finally
                {
                    brush.Dispose();
                }
            }

            #region
            if (map.SimpleProgress || map.text != null)
            {
                var percent = "0%";
                if (map.text != null)
                    percent = map.text;
                else if (map.Cells.Count > 0)
                    percent = (100 * map.Cells[0].length / map.max).ToString("0.#") + "%";
                float textWidth = TextRenderer.MeasureText(percent, cellStyle.Font).Width;
                float textHeight = TextRenderer.MeasureText(percent, cellStyle.Font).Height;


                posX = cellBounds.X + (cellBounds.Width / 2) - textWidth / 2;
                posY = cellBounds.Y + (cellBounds.Height / 2) - textHeight / 2;
                using (var foreColorBrush = new SolidBrush(cellStyle.ForeColor))
                    g.DrawString(percent, cellStyle.Font, foreColorBrush, posX, posY);
            }

            if (major != 0 && map.SimpleProgress == false)
            {
                if (minor != 0)
                {
                    using (var brush_ = new SolidBrush(minorColor))
                        for (int i = 1; i < minor * major; i++)
                        {
                            g.FillRectangle(brush_,
                                x + (float)(w * i / (minor * major * 1.0)),
                                cellBounds.Y - cellBounds.Height / 7,
                                1f,
                                2 * (cellBounds.Height / 7));
                        }
                }
                {
                    using (var brush_ = new SolidBrush(majorColor))
                        for (int i = 1; i < major; i++)
                        {
                            g.FillRectangle(brush_,
                                x + (float)(w * i / (1.0 * major)),
                                cellBounds.Y,
                                1,
                                cellBounds.Height);
                        }
                }
            }
            #endregion
        }

        public override object Clone()
        {
            DataGridViewProgressCell dataGridViewCell = base.Clone() as DataGridViewProgressCell;
            if (dataGridViewCell != null)
            {
                dataGridViewCell.ProgressBarColor = this.ProgressBarColor;
                dataGridViewCell.minor = this.minor;
                dataGridViewCell.major = this.major;
                dataGridViewCell.minorColor = this.minorColor;
                dataGridViewCell.majorColor = this.majorColor;
                dataGridViewCell.footerColor = this.footerColor;
                dataGridViewCell.headerColor = this.headerColor;
            }
            return dataGridViewCell;
        }

        internal void SetProgressBarColor(int rowIndex, Color value)
        {
            this.ProgressBarColor = value;
        }

    }

    public class ProgressMap
    {
        public ProgressMap()
        {
        }
        public ProgressMap(int value, Color? color = null, string text = null)
        {
            Cells.Add(new ProgressCell { start = 0, length = value, color = color });
            max = 100;
            SimpleProgress = true;
            this.text = text;
        }
        public double max = 1000;
        public bool RightToLeft = false;
        public string text = null;
        public bool SimpleProgress = false;
        public List<ProgressCell> Cells = new List<ProgressCell>();
        public Color? color = null;
    }
    public class ProgressCell
    {
        public double start = 0, length = 0;
        public Color? color = null;
        public bool footer = false, header = false;
    }
}
