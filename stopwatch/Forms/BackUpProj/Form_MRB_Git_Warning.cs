using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace stopwatch.Forms
{
    internal partial class Form_MRB_Git_Warning : Form
    {
        public Form_MRB_Git_Warning(MRB_Git.BackupInfo bi, List<string[]> files)
        {
            InitializeComponent();
            Column_file2.Visible = false;
            this.files = files;
            this.bi = bi;
            foreach (var f in files)
                try
                {
                    var reson = "حجم بالا";
                    if (f[1].StartsWith("**"))
                        reson = "خطا";
                    else if (f[1] == "*")
                        reson = "انتخاب کاربر";
                    dataGridView1.Rows.Add(f[0],
                        f[1],
                        reson,
                        File.Exists(f[0]) ? new FileInfo(f[0]).Length / (1024 * 1024) + "" : "?");

                }
                catch { }
            while (dataGridView1.SelectedRows.Count > 0) dataGridView1.SelectedRows[0].Selected = false;
        }
        List<string[]> files;
        MRB_Git.BackupInfo bi;


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                var N = dataGridView1.SelectedRows.Count;
                progressBar1.Visible = true;
                Exception err = null;
                for (int i = N - 1; i >= 0; i--)
                    try
                    {
                        var r = dataGridView1.SelectedRows[i];
                        var saved_file = r.Cells[1].Value + "";
                        var f_local = r.Cells[0].Value + "";
                        if (File.Exists(saved_file)) continue;
                        File.Copy(f_local, saved_file);
                        if (File.Exists(saved_file))
                        {
                            var f1 = new FileInfo(f_local);
                            var f2 = new FileInfo(saved_file);
                            bi.AddedFilesCount++;
                            bi.TotalSizeAdded += f1.Length;
                            bi.SaveAgain();
                            f2.CreationTimeUtc = f1.CreationTimeUtc;
                            f2.LastWriteTimeUtc = f1.LastWriteTimeUtc;
                            f2.LastWriteTime = f1.LastWriteTime;
                            f2.CreationTime = f1.CreationTime;
                        }
                        progressBar1.Value = 1 + (int)(98 - 98.0 * i / N);
                        progressBar1.Refresh();
                        Application.DoEvents();
                        r.Selected = false;
                    }
                    catch (Exception ex) { err = ex; }
                if (err != null) throw err;
                Close();
            }
            catch { }
            finally
            {
                this.Enabled = true;
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if ((dataGridView1[Column_file2.Index, e.RowIndex].Value + "").StartsWith("**"))
                    e.CellStyle.BackColor = Color.LightSalmon;
                else if (dataGridView1[Column_file2.Index, e.RowIndex].Value + "" == "*")
                    e.CellStyle.BackColor = Color.LightBlue;
            }
            catch { }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == Column_file1.Index)
                dataGridView1[e.ColumnIndex, e.RowIndex].ToolTipText = dataGridView1[Column_file2.Index, e.RowIndex].Value + "";
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
