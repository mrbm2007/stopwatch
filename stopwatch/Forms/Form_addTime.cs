using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace stopwatch
{
    public partial class Form_addTime : Form
    {
        static MyData db { get { return MyData.db; } }
        public Form_addTime(Project proj = null)
        {
            try
            {
                InitializeComponent();
                dataGridView1.Rows.Add();
                (dataGridView1.Columns[0] as DataGridViewComboBoxColumn).Items.Clear();
                {
                    Column_day_in_week.Items.Clear();
                    for (int i = -6; i <= 6; i++)
                        Column_day_in_week.Items.Add(Utils.DayString(DateTime.Now.AddDays(i)) + (i > 0 ? " " : ""));
                }
                foreach (var p in db.ActiveProjects)
                    (dataGridView1.Columns[0] as DataGridViewComboBoxColumn).Items.Add(p.Name);
                if (proj == null)
                    dataGridView1[0, 0].Value = db.project.Name;
                else
                    dataGridView1[0, 0].Value = proj.Name;
                try
                {
                    dataGridView1[1, 0].Value = Utils.DayString(DateTime.Now);
                    dataGridView1[2, 0].Value = Utils.DateString(DateTime.Now);
                }
                catch { }
                try
                {
                    dataGridView1[3, 0].Value = Utils.TimeString(DateTime.Now.AddHours(-1));
                    dataGridView1[4, 0].Value = Utils.TimeString(DateTime.Now);
                }
                catch { }
                dataGridView1[5, 0].Value = "1:00";
                dataGridView1[6, 0].Value = db.project.LastComment;
            }
            finally
            {
                ignorEvents = false;
            }
        }

        bool ignorEvents = true;

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (ignorEvents) return;
            try
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++) dataGridView1[i, 0].ErrorText = "";
                var t = new Time_(db.GetByName(dataGridView1[0, 0].Value.ToString()));
                try
                {
                    var date = Utils.PersianParse(dataGridView1[2, 0].Value.ToString());
                    try
                    {
                        if (!(dataGridView1[3, 0].Value + "").Contains(":"))
                            dataGridView1[3, 0].Value += ":00";
                        var start = dataGridView1[3, 0].Value.ToString().Split(new char[] { ':' });
                        t.Start = new DateTime(date.Year, date.Month, date.Day,
                            Convert.ToInt16(start[0]), Convert.ToInt16(start[1]), 0);
                    }
                    catch { dataGridView1[3, 0].ErrorText = "?"; }
                }
                catch { dataGridView1[2, 0].ErrorText = "?"; }
                try
                {
                    if (!(dataGridView1[5, 0].Value + "").Contains(":"))
                        dataGridView1[5, 0].Value += ":00";
                    t.Duration = new TSpan(dataGridView1[5, 0].Value.ToString());
                }
                catch { dataGridView1[5, 0].ErrorText = "?"; }
                t.Comment = dataGridView1[6, 0].Value.ToString().TrimStart();
                if (db.project == t.project)
                {
                    if (Form1.Stop())
                        MessageBox.Show(this, "Current time work stopped!");
                }
                {
                    if (!t.project.IsHozoor)
                        foreach (var p in db.Projects)
                            if (p.Active && !p.IsHozoor && p.Times.Count > 0 && Utils.Last(p.Times).Start.AddDays(30) > DateTime.Now)
                            {
                                foreach (var t2 in p.Times)
                                    if (t2.Start.AddDays(30) > DateTime.Now & t2.Duration.sec > 5)
                                        if (t.Overlap(t2) > 60)
                                            if (Form_msg.Show(this, "با ساعت کاری زیر هم پوشانی دارد:\r\n" + p.Name + ": " + Utils.DateTimeString(t2.Start) + " - " + Utils.TimeString(t2.End) + "\r\n" + "با این وجود ادامه می دهید؟", btn: MessageBoxButtons.YesNo) == DialogResult.No)
                                                return;
                            }
                    if (t.project.Times.Count == 0 || Utils.Last(t.project.Times).Start <= t.Start)
                        t.project.Times.Add(t);
                    else
                        for (int i = 0; i < t.project.Times.Count; i++)
                            if (t.project.Times[i].Start > t.Start)
                            {
                                t.project.Times.Insert(i, t);
                                break;
                            }
                    t.project.LastComment = t.Comment;
                }
                Utils.Log("++ Add: " + t.project.Name + " - " + Utils.DateTimeString(t.Start) + " - " + t.Duration.ToString(true));
                db.Save(Form1.DataFile);
                Close();
            }
            catch { dataGridView1[0, 0].ErrorText = "?"; }
        }



        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
                if (dataGridView1.RowCount > 0)
                    try
                    {
                        var p = db.GetByName(dataGridView1[0, 0].Value + "");
                        dataGridView1[6, 0].Value = p.LastComment;
                    }
                    catch { dataGridView1[0, 0].ErrorText = "?"; }
            if (ignorEvents) return;
            if (e.ColumnIndex == Column_day_in_week.Index)
                try
                {
                    var i = Column_day_in_week.Items.IndexOf(dataGridView1[e.ColumnIndex, e.RowIndex].Value + "");
                    if (i >= 0)
                    {
                        dataGridView1[Column_date.Index, e.RowIndex].Value = Utils.DateString(DateTime.Now.AddDays(i - 6));
                    }
                }
                catch { }
            for (int i = 3; i <= 5; i++) dataGridView1[i, 0].ErrorText = "";
            if (e.ColumnIndex == 2)// تاریخ
                try
                {
                    ignorEvents = true;
                    var date = Utils.PersianParse(dataGridView1[2, 0].Value.ToString());
                    var Start = new DateTime(date.Year, date.Month, date.Day, 1, 1, 0);
                    dataGridView1[1, 0].Value = Utils.DayString(Start);
                    UpdateProgresssBar(date);
                }
                catch { dataGridView1[2, 0].ErrorText = "?"; }
                finally { ignorEvents = false; }
            if (e.ColumnIndex == 3 || e.ColumnIndex == 4) // شروع - پایان
                try
                {
                    if (!(dataGridView1[3, 0].Value + "").Contains(":"))
                        dataGridView1[3, 0].Value += ":00";
                    if (!(dataGridView1[4, 0].Value + "").Contains(":"))
                        dataGridView1[4, 0].Value += ":00";
                    ignorEvents = true;
                    var date = Utils.PersianParse(dataGridView1[2, 0].Value.ToString());
                    var start = dataGridView1[3, 0].Value.ToString().Split(new char[] { ':' });
                    var Start = new DateTime(date.Year, date.Month, date.Day, Convert.ToInt16(start[0]), Convert.ToInt16(start[1]), 0);
                    var end = dataGridView1[4, 0].Value.ToString().Split(new char[] { ':' });
                    var End = new DateTime(date.Year, date.Month, date.Day, Convert.ToInt16(end[0]), Convert.ToInt16(end[1]), 0);
                    var Duration = new TSpan() { sec = (int)(End - Start).TotalSeconds };
                    dataGridView1[5, 0].Value = Duration.ToString();
                }
                catch { dataGridView1[e.ColumnIndex, 0].ErrorText = "?"; }
                finally { ignorEvents = false; }
            else if (e.ColumnIndex == 5) // مدت
                try
                {
                    if (!(dataGridView1[5, 0].Value + "").Contains(":"))
                        dataGridView1[5, 0].Value += ":00";
                    if (!(dataGridView1[3, 0].Value + "").Contains(":"))
                        dataGridView1[3, 0].Value += ":00";
                    var date = Utils.PersianParse(dataGridView1[2, 0].Value.ToString());
                    var start = dataGridView1[3, 0].Value.ToString().Split(new char[] { ':' });
                    var Start = new DateTime(date.Year, date.Month, date.Day, Convert.ToInt16(start[0]), Convert.ToInt16(start[1]), 0);
                    var Duration = new TSpan(dataGridView1[5, 0].Value.ToString());
                    var End = Start.AddSeconds(Duration.sec);
                    dataGridView1[4, 0].Value = Utils.TimeString(End);
                }
                catch { dataGridView1[5, 0].ErrorText = "?"; }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 6)
            {
                var p = db.GetByName(dataGridView1[0, 0].Value.ToString());
                var tex = e.Control as TextBox;
                tex.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                tex.AutoCompleteSource = AutoCompleteSource.CustomSource;
                tex.AutoCompleteCustomSource.Clear();
                foreach (var t in p.Times)
                    if (!tex.AutoCompleteCustomSource.Contains(t.Comment))
                    {
                        tex.AutoCompleteCustomSource.Add(t.Comment);
                        tex.AutoCompleteCustomSource.Add(" " + t.Comment);
                    }
            }
        }

        int day_seconds = 24 * 60 * 60;
        internal void UpdateProgresssBar(DateTime? date = null)
        {
            if (date == null)
                date = Utils.PersianParse(dataGridView1[2, 0].Value.ToString());
            dataGridView2.Rows.Clear();
            var color = Color.Lime;
            var map = new ProgressMap { RightToLeft = true };
            foreach (var p in db.ActiveProjects)
                if (!p.IsHozoor)
                    foreach (var t in p.Times)
                        if (t.Start.Date == date)
                        {
                            if (t.Duration.sec < 0) t.Duration.sec = 0;
                            map.Cells.Add(new ProgressCell
                            {
                                start = 1 + (t.Start - t.Start.Date).TotalSeconds * 1000 / day_seconds,
                                length = t.Duration.sec * 1000 / day_seconds,
                                color = color
                            });
                        }
            {
                var p = db.GetByType(hozoor: true);
                foreach (var t in p.Times)
                    if (t.Start.Date == date)
                    {
                        if (t.Duration.sec < 0) t.Duration.sec = 0;
                        map.Cells.Add(new ProgressCell
                        {
                            start = 1 + (t.Start - t.Start.Date).TotalSeconds * 1000 / day_seconds,
                            length = t.Duration.sec * 1000 / day_seconds,
                            header = true
                        });
                    }
            }
            map.Cells.Add(new ProgressCell
            {
                start = (DateTime.Now - DateTime.Now.Date).TotalSeconds * 1000 / day_seconds,
                length = 2,
                color = Color.Red
            });
            dataGridView2.Rows.Add(map);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form_addTime_Shown(object sender, EventArgs e)
        {
            UpdateProgresssBar();
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var date = Utils.PersianParse(dataGridView1[2, 0].Value.ToString());
            var p = 1000 - (1000.0 * e.Location.X) / dataGridView2[0, 0].Size.Width;
            var map = dataGridView2[0, 0].Value as ProgressMap;
            Project H = null;// db.GetByType(hozoor: true);
            var enter = 0.0;
            var exit = 1000.0;
            if (H != null && H.LastTime.Start.Date == date.Date)
            {
                enter = 1000 * (H.LastTime.Start - date.Date).TotalMinutes / (24 * 60);
                exit = 1000 * (H.LastTime.End - date.Date).TotalMinutes / (24 * 60);
            }
            var dt = 0.1;
            var start = p;
            for (; start > 0; start -= dt)
            {
                var i = MapIndexOf(map, start);
                if (i >= 0) break;
                if (start >= enter && start - dt < enter) break;
            }
            var end = p;
            for (; end < 999; end += dt)
            {
                var i = MapIndexOf(map, end);
                if (i >= 0) break;
                if (end <= exit && end + dt > exit) break;
            }
            if ((end - start) < 1 || (end - start) > 500) return;
            var t1 = date.AddSeconds(60 * Math.Ceiling(day_seconds * start / 60000.0));
            var t2 = date.AddSeconds(60 * Math.Floor(day_seconds * end / 60000.0));
            if (date == DateTime.Now.Date && t2 > DateTime.Now && t1 < DateTime.Now) t2 = DateTime.Now;
            if (Form_msg.Show(this,
                Utils.TimeString(t1) + " - " + Utils.TimeString(t2) + "\r\nآیا از این زمان ها استفاده می کنید؟",
                "زمان خالی",
                btn: MessageBoxButtons.YesNo)
                       == DialogResult.Yes)
            {
                dataGridView1[3, 0].Value = Utils.TimeString(t1);
                dataGridView1[4, 0].Value = Utils.TimeString(t2);
            }
        }
        int MapIndexOf(ProgressMap map, double i)
        {
            for (int j = 0; j < map.Cells.Count; j++)
                if (!map.Cells[j].footer && !map.Cells[j].header)
                    if (map.Cells[j].start <= i && i <= map.Cells[j].start + map.Cells[j].length)
                        return j;
            return -1;
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == Column_day_in_week.Index) e.Cancel = true;
        }

        private void امروزToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1[Column_date.Index, 0].Value = Utils.DateString(DateTime.Now);
        }

        private void دیروزToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1[Column_date.Index, 0].Value = Utils.DateString(DateTime.Now.AddDays(-1));
        }

        private void فرداToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1[Column_date.Index, 0].Value = Utils.DateString(DateTime.Now.AddDays(1));
        }

        private void روزبعدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = Utils.PersianParse(dataGridView1[Column_date.Index, 0].Value + "");
            dataGridView1[Column_date.Index, 0].Value = Utils.DateString(d.AddDays(1));
        }

        private void روزقبلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = Utils.PersianParse(dataGridView1[Column_date.Index, 0].Value + "");
            dataGridView1[Column_date.Index, 0].Value = Utils.DateString(d.AddDays(-1));
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == Column_date.Index && e.Button == MouseButtons.Middle)
            {
                if (e.Delta > 0)
                    روزبعدToolStripMenuItem_Click(sender, null);
                if (e.Delta < 0)
                    روزقبلToolStripMenuItem_Click(sender, null);
            }
        }
    }
}