using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace stopwatch.Forms
{
    public partial class Form_MRB_Git_all : Form
    {
        static DateTime LastMessageShown = DateTime.Now.AddDays(-10);
        static MyData db { get { return MyData.db; } }
        public Form_MRB_Git_all()
        {
            InitializeComponent();
          
            Column_sort.Visible = false;
            UpdateList();
            if (dataGridView1.Rows.Count == 0)
            {
                oldprojToolStripMenuItem.Checked = true;
                UpdateList();
            }
        }
        void UpdateList()
        {
            dataGridView1.Rows.Clear();
            var list = new List<object[]>();
            var rnd = new Random();
            foreach (var p in db.Projects)
                if (!p.IsHozoor && (p.code + "").Trim() != "")
                {
                    var dt = p.LastTime != null ? (p.LastTime.End - p.LastBackup).TotalDays : 0;
                    if (dt == 0 && !p.Active) continue;
                    if (dt == 0 && p.code + "" == "") continue;
                    if (!oldprojToolStripMenuItem.Checked && (p.LastTime == null || (DateTime.Now - p.LastTime.End).TotalDays > 5 * 30)) continue;
                    list.Add(new object[] {
                        dt,
                        p.Name,
                        (p.LocalPath+"*").Replace("\\*","").Replace("*",""),
                        p.LastTime!=null ? Utils.DateString( p.LastTime.End ):"",
                        p.LastBackup.Year>1900 ? Utils.DateString(p.LastBackup):"",
                        "...",
                        });
                }
            list.Sort((r1, r2) =>
            {
                if (r1[0] == r2[0]) return 0;
                if ((double)r1[0] < (double)r2[0])
                    return 1;
                return -1;
            });
            foreach (var v in list)
            {
                min = Math.Min(min, (double)v[0]);
                max = Math.Max(max, (double)v[0]);
                dataGridView1.Rows.Add(v);
            }
        }
        double min, max;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == Column_open.Index)
            {
                var p = db.GetByName(dataGridView1[column_Project.Index, e.RowIndex].Value + "");
                var frm = new Form_MRB_Git(db, p);
                frm.ObjToolStripMenuItem.Checked = ObjToolStripMenuItem.Checked;
                frm.rstToolStripMenuItem.Checked = rstToolStripMenuItem.Checked;
                frm.largfilesToolStripMenuItem.Checked = largfilesToolStripMenuItem.Checked;
                frm.Show(this);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == Column_dir.Index)
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == Column_dir.Index)
                db.GetByName(dataGridView1[column_Project.Index, e.RowIndex].Value + "").LocalPath = dataGridView1[e.ColumnIndex, e.RowIndex].Value + "";
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            while (dataGridView1.SelectedRows.Count > 0)
                dataGridView1.SelectedRows[0].Selected = false;
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                dataGridView1.Rows[i].Selected = true;
        }

        private void _30ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                dataGridView1.Rows[i].Selected = (double)dataGridView1[Column_sort.Index, i].Value >= 30;
        }

        private void _60ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                dataGridView1.Rows[i].Selected = (double)dataGridView1[Column_sort.Index, i].Value >= 60;
        }

        private void _90ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                dataGridView1.Rows[i].Selected = (double)dataGridView1[Column_sort.Index, i].Value >= 90;
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dataGridView1.SelectedRows)
            {
                var p = db.GetByName(r.Cells[column_Project.Index].Value + "");
                if ((p.LocalPath + "").Replace("\\", "").Trim() == "" || (db.settings.BackUpServer + "").Replace("\\", "").Trim() == "")
                    using (var frm = new Form_MRB_Git(db, p))
                    {
                        frm.ObjToolStripMenuItem.Checked = ObjToolStripMenuItem.Checked;
                        frm.rstToolStripMenuItem.Checked = rstToolStripMenuItem.Checked;
                        frm.baktmpasvToolStripMenuItem.Checked = baktmpasvToolStripMenuItem.Checked;
                        frm.largfilesToolStripMenuItem.Checked = largfilesToolStripMenuItem.Checked;
                        frm.groupBox2.Enabled = false;
                        frm.ShowDialog(this);
                        frm.Close();
                        if ((p.LocalPath + "").Replace("\\", "").Trim() == "" || (db.settings.BackUpServer + "").Replace("\\", "").Trim() == "") return;
                        r.Cells[Column_dir.Index].Value = (p.LocalPath + "*").Replace("\\*", "").Replace("*", "");
                    }
            }
            foreach (DataGridViewRow r in dataGridView1.SelectedRows)
            {
                using (var frm = new Form_MRB_Git(db, db.GetByName(r.Cells[column_Project.Index].Value + "")))
                {
                    frm.ObjToolStripMenuItem.Checked = ObjToolStripMenuItem.Checked;
                    frm.rstToolStripMenuItem.Checked = rstToolStripMenuItem.Checked;
                    frm.largfilesToolStripMenuItem.Checked = largfilesToolStripMenuItem.Checked;
                    frm.Show(this);
                    frm.groupBox1.Enabled = false;
                    frm.groupBox2.Enabled = false;
                    frm.button_backup_Click(this, null);
                    while (frm.backgroundWorker_back.IsBusy)
                    {
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(30);
                    }
                    frm.Hide();
                }
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateList();
            if (!oldprojToolStripMenuItem.Checked && dataGridView1.Rows.Count < 5)
            {
                oldprojToolStripMenuItem.Checked = true;
                UpdateList();
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var p = db.GetByName(dataGridView1[column_Project.Index, e.RowIndex].Value + "");
            if (e.ColumnIndex == Column_dir.Index)
            {
                folderBrowserDialog1.SelectedPath = p.LocalPath;
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = p.LocalPath = folderBrowserDialog1.SelectedPath;
            }
            else
            {
                var frm = new Form_MRB_Git(db, p);
                frm.ObjToolStripMenuItem.Checked = ObjToolStripMenuItem.Checked;
                frm.rstToolStripMenuItem.Checked = rstToolStripMenuItem.Checked;
                frm.largfilesToolStripMenuItem.Checked = largfilesToolStripMenuItem.Checked;
                frm.Show(this);
            }
        }

        private void oldprojToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void پشتیبانگیریازطرفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            پشتیبانگیریازطرفToolStripMenuItem.Enabled = false;
            try
            {
                new Form_MRB_Git(db, null).Show(this);
            }
            finally
            {
                پشتیبانگیریازطرفToolStripMenuItem.Enabled = true;
            }
        }

        private void Form_MRB_Git_all_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Save(Form1.DataFile, true);
        }

        private void Form_MRB_Git_all_Shown(object sender, EventArgs e)
        {
            if ((DateTime.Now - LastMessageShown).TotalMinutes > 5)
            {
                if (Form_msg.Show(this, "لیست پروژه ها از سرور بارگذاری شود؟"+"\r\n"+"نام و کد پروژه ها از سرور دریافت خواهد شد", btn: MessageBoxButtons.YesNo) == DialogResult.Yes)
                    if (Form_list.LoadProjectList(this))
                        UpdateList();

            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex > 0)
                try
                {
                    var v = Convert.ToDouble(dataGridView1[Column_sort.Index, e.RowIndex].Value);
                    if (v >= 3 * 30)
                        e.CellStyle.BackColor = Color.Salmon;
                    else if (v >= 2 * 30)
                        e.CellStyle.BackColor = Color.FromArgb(255, 128, 0);
                    else if (v >= 1 * 30)
                        e.CellStyle.BackColor = Color.LightSalmon;
                    else if (v >= 2)
                        e.CellStyle.BackColor = Color.Lavender;
                    else if (v > 0)
                        e.CellStyle.BackColor = Color.LightGreen;
                    else if (v == 0)
                        e.CellStyle.BackColor = Color.White;
                    else
                        e.CellStyle.BackColor = Color.MediumSpringGreen;
                }
                catch { }
        }
    }
}
