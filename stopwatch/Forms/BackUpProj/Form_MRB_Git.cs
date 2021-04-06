using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace stopwatch.Forms
{
    public partial class Form_MRB_Git : Form
    {
        public Form_MRB_Git(MyData db, Project project)
        {
            try
            {
                InitializeComponent();
                this.project = project;
                this.db = db;
                textBox_server.Text = db.settings.BackUpServer;
                height_full = this.Height;
                if (project != null)
                {
                    textBox_project.Text = project.code;
                    git.user = textBox_user.Text = db.user.ID;
                    textBox_local.Text = project.LocalPath;
                    textBox_skip_pat.Text = project.BackUpSkipPattern;
                    if (db.settings.BackUpServer + "" == "" || db.settings.BackUpServer + "" == "\\")
                        checkBox_network.Checked = true;
                    textBox_skip_pat_TextChanged(this, null);
                }
                else
                {
                    if (WebServer.Default.IsConnected)
                    {
                        Height = 130;
                        textBox_project.ReadOnly = false;
                        textBox_user.ReadOnly = false;
                        checkBox_network.Checked = true;
                        checkBox_network.Enabled = false;
                        textBox_project.Text = "";
                        git.user = textBox_user.Text = "";
                        textBox_user.AutoCompleteCustomSource.Clear();
                        textBox_user.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        textBox_user.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        Users = WebServer.Default.GetUserList();
                        foreach (IniFile.IniSection user in Users.Sections)
                        {
                            textBox_user.AutoCompleteCustomSource.Add(" " + user["name"].Trim());
                            textBox_user.AutoCompleteCustomSource.Add(user["name"].Trim());
                        }
                        textBox_user.Text = db.user.name;
                        button_ok.Visible = true;
                    }
                    else
                    {
                        Form_msg.Show(this, new Exception("خطا در اتصال به شبکه! این قابلیت فقط تحت شبکه در دسترس است"));
                        timer_close.Start();
                    }
                }
            }
            finally
            { ignor_events = false; }
            textBox_server_TextChanged(this, null);
        }
        IniFile Users;
        public int height_full;
        bool ignor_events = true;
        Project _project;
        Project project
        {
            get
            { return _project; }
            set
            {
                _project = value;
                if (_project != null)
                {
                    textBox_project.Text = _project.code;
                    label_proj_name.Text = _project.Name;
                }
            }
        }
        MyData db;
        MRB_Git git = new MRB_Git();

        private void button_browse_server_Click(object sender, EventArgs e)
        {
            try { folderBrowserDialog1.SelectedPath = textBox_server.Text; } catch { }
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                textBox_server.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button_browse_local_Click(object sender, EventArgs e)
        {
            try { folderBrowserDialog1.SelectedPath = textBox_local.Text; } catch { }
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (textBox_local.Text.Trim() == "" || textBox_local.Text.Trim() == "\\" || textBox_local.Text.Trim() == "-")
                    textBox_local.Text = folderBrowserDialog1.SelectedPath;
                else if (!new List<string>(textBox_local.Lines).Contains(folderBrowserDialog1.SelectedPath))
                    textBox_local.Text += "\r\n" + folderBrowserDialog1.SelectedPath;
            }
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            if (ignor_events) return;
            if (project == null) return;
            try
            {
                git.OpenBackUpDrive();
            }
            catch (Exception ex)
            {
                Form_msg.Show(this, new Exception("خطا در باز کردن درایو شبکه" + " :\r\n" + ex.Message, ex));
                return;
            }
            dataGridView_backups.Rows.Clear();
            var B = git.GetBackUpList(true);
            foreach (var b in B)
            {
                var s = "";
                if (b.TotalSize > (1024.0 * 1024) * 1024)
                    s = Math.Round(100 * b.TotalSize / (1024.0 * 1024 * 1024)) / 100 + " GB";
                else
                    s = Math.Round(10 * b.TotalSize / (1024.0 * 1024)) / 10 + " MB";
                var s2 = "";
                if (b.TotalSizeAdded > (1024.0 * 1024) * 1024)
                    s2 = Math.Round(100 * b.TotalSizeAdded / (1024.0 * 1024 * 1024)) / 100 + " GB";
                else
                    s2 = Math.Round(10 * b.TotalSizeAdded / (1024.0 * 1024)) / 10 + " MB";
                dataGridView_backups.Rows.Insert(0,
                    b,
                    b.FilesCount,
                    b.AddedFilesCount,
                    s,
                    s2,
                    b.InfoSection["user"]);
                if (b.InfoSection["user"] != textBox_user.Text)
                    foreach (DataGridViewCell c in dataGridView_backups.Rows[0].Cells)
                        c.Style.BackColor = Color.LightGray;
            }
            if (dataGridView_backups.Rows.Count > 0)
                dataGridView_backups.Rows[0].Selected = true;
        }

        private void textBox_server_TextChanged(object sender, EventArgs e)
        {
            if (ignor_events) return;
            try
            {
                ignor_events = true;
                if (!textBox_server.Text.Trim().EndsWith("\\")) textBox_server.Text += "\\";
                git.server_root = (db.settings.BackUpServer = textBox_server.Text.Trim());
                git.project_code = textBox_project.Text.Trim();
                git.project_name = label_proj_name.Text.Trim();
                git.NetworkPath = checkBox_network.Checked ? db.settings.host_network.Replace("\\", "") : "";
                errorProvider1.SetError(textBox_server, textBox_server.Text.Replace("\\", "").Trim() == "" ? "آدرس را وارد کنید" : "");
                if (checkBox_network.Checked) errorProvider1.SetError(textBox_server, "");
                button_refresh_Click(sender, e);
            }
            catch { }
            finally { ignor_events = false; }
        }

        private void textBox_local_TextChanged(object sender, EventArgs e)
        {
            project.LocalPath = textBox_local.Text;
            var tmp = project.LocalPath.Replace("\r", "").Replace("\n", "").Replace("\\", "").Replace("--", "-").Replace("--", "-").Trim();
            if (tmp != "-" && tmp != "")
                git.local_dirs = new List<string>(project.LocalPath.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            else
                git.local_dirs = new List<string>();
            errorProvider1.SetError(textBox_local, tmp == "" ? "آدرس را وارد کنید" : "");
        }

        private void textBox_user_TextChanged(object sender, EventArgs e)
        {
            git.user = textBox_user.Text;
        }

        internal void button_backup_Click(object sender, EventArgs e)
        {
            if (textBox_local.Text.Trim() == "")
            {
                Form_msg.Show(this, "آدرس محلی را وارد کنید");
                return;
            }
            try
            {
                git.OpenBackUpDrive();
            }
            catch (Exception ex)
            {
                Form_msg.Show(this, new Exception("خطا در باز کردن درایو شبکه" + " : " + ex.Message, ex));
                return;
            }
            button_backup.Enabled = false;
            backgroundWorker_back.RunWorkerAsync();
        }

        private void button_restore_Click(object sender, EventArgs e)
        {
            if (dataGridView_backups.SelectedRows.Count == 0) return;
            try
            {
                var tmp = textBox_local.Text.Replace("\r", "").Replace("\n", "").Replace("\\", "").Replace("--", "-").Replace("--", "-").Trim();

                if (tmp == "" || tmp == "-")
                {
                    Form_msg.Show(this, "آدرس محلی را وارد کنید");
                    return;
                }
                var b = dataGridView_backups.SelectedRows[0].Cells[0].Value as MRB_Git.BackupInfo;
                if (b.InfoSection["user"] != textBox_user.Text) return;
                if (Form_msg.Show(this, "فایل موجود در آدرس محلی کاملا حذف شود؟" + "\r\n" + textBox_local.Text.Trim(),
                    btn: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Directory.Delete(textBox_local.Text.Trim() + "\\", true);
                }
                backgroundWorker_restore.RunWorkerAsync(new object[] { b, textBox_local.Text.Trim() });
            }
            catch { }
        }

        private void button_restoreto_Click(object sender, EventArgs e)
        {
            if (dataGridView_backups.SelectedRows.Count == 0) return;
            try
            {
                var b = dataGridView_backups.SelectedRows[0].Cells[0].Value as MRB_Git.BackupInfo;
                if (b.InfoSection["user"] != textBox_user.Text) return;
                try { folderBrowserDialog1.SelectedPath = textBox_local.Text; } catch { }
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    backgroundWorker_restore.RunWorkerAsync(new object[] { b, folderBrowserDialog1.SelectedPath });
                }
            }
            catch { }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Form_msg.Show(this, "با حذف یک پشتیبانی، فایل مربوطه نیز حذف خواهند شد. آیا ادامه می دهید؟", btn: MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var b = dataGridView_backups.SelectedRows[0].Cells[0].Value as MRB_Git.BackupInfo;
                var n = git.Delete(b.path);
                button_refresh_Click(this, null);
                Form_msg.Show(this, n + " " + "فایل پاک شد");
            }
        }

        List<string[]> warning_files;
        MRB_Git.BackupInfo last_backup_info;
        DateTime last_UpdateProgress = DateTime.Now.AddDays(-1);
        void UpdateProgress(double p, string state)
        {
            if ((DateTime.Now - last_UpdateProgress).TotalMilliseconds < 500) return;
            last_UpdateProgress = DateTime.Now;
            if (InvokeRequired) Invoke(new UpdateProgress_Delegate(_UpdateProgress), p, state + "");
            else _UpdateProgress(p, state + "");
        }
        void _UpdateProgress(double p, string state)
        {
            if (!toolStripProgressBar1.Visible)
            {
                toolStripProgressBar1.Visible = true;
                toolStripStatusLabel1.Visible = true;
                toolStripStatusLabel2.Visible = true;
            }
            toolStripProgressBar1.Value = Math.Max(0, Math.Min(100, (int)p));
            toolStripStatusLabel1.Text = state + "";
        }
        delegate void UpdateProgress_Delegate(double p, string state);
        private void backgroundWorker_backup_DoWork(object sender, DoWorkEventArgs e)
        {
            var empty_dir = false;
            try
            {
                if (textBox_local.Text.Trim() == "-")
                {
                    var dir = Path.GetTempPath() + "\\temp_proj_dir\\";
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                    try
                    {
                        File.WriteAllText(dir + "empty.txt", "");
                    }
                    catch { }
                    git.local_dirs = new List<string> { dir };
                    empty_dir = true;
                }
                var sw = new System.Diagnostics.Stopwatch(); sw.Start();
                warning_files = new List<string[]>();
                git.progress = (p, state) =>
                {
                    if (p == -100)
                        warning_files.Add((string[])state);
                    else
                        UpdateProgress(p, state + "");
                    return null;
                };
                git.settings.ignor_obj = ObjToolStripMenuItem.Checked;
                git.settings.ignor_rst = rstToolStripMenuItem.Checked;
                git.settings.ignor_baktmpasv = baktmpasvToolStripMenuItem.Checked;
                git.settings.warnin_larg_files = largfilesToolStripMenuItem.Checked;
                last_backup_info = git.BackUp();

                if (last_backup_info.InfoSection == null)
                    last_backup_info.InfoSection = last_backup_info.InfoFile.AddSection("info");
                last_backup_info.InfoSection["server"] = git.server_dir;
                last_backup_info.InfoSection["local"] = project.LocalPath.Replace("\r", "").Replace("\n", " ; ");
                last_backup_info.InfoSection["BackUpSkipPattern"] = project.BackUpSkipPattern.Replace("\r", "").Replace("\n", " ; ");
                last_backup_info.InfoSection["machine"] = Environment.UserName + "@" + Environment.MachineName;
                last_backup_info.InfoSection["user"] = textBox_user.Text; ;
                last_backup_info.InfoSection["backup-duration"] = new TSpan((int)sw.Elapsed.TotalSeconds).ToString(true);
                last_backup_info.ProjectSection["name"] = project.Name;
                last_backup_info.ProjectSection["backup_by"] = db.user.ID;
                last_backup_info.ProjectSection["code"] = project.code;
                last_backup_info.ProjectSection["manager"] = project.Manager;
                last_backup_info.ProjectSection["total-time"] = project.SumTimes().ToString(false);
                if (project.Times.Count > 0)
                {
                    last_backup_info.ProjectSection["first-date"] = Utils.DateString(project.Times[0].Start);
                    last_backup_info.ProjectSection["last-date"] = Utils.DateString(project.LastTime.End);
                }
                last_backup_info.SaveAgain();
                project.LastBackup = DateTime.Now;
                {
                    var proj_name = new IniFile();
                    var file = git.server_dir + "\\..\\نام پروژه ها.ini";
                    if (File.Exists(file))
                        proj_name.Load(file);
                    var sec = proj_name.AddSection(db.user.name.Replace(" ", "_"));
                    sec.AddKey(project.code.Replace(" ", "_"), project.Name);
                    if (proj_name.changed)
                        proj_name.Save(file);

                }
            }
            catch (Exception ex)
            { e.Result = ex; }
            finally
            {
                if (empty_dir)
                    textBox_local.Text = "-";
            }
        }

        private void backgroundWorker_back_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
            toolStripStatusLabel2.Visible = false;
            button_refresh_Click(sender, e);
            button_backup.Enabled = true;
            if (e.Result is Exception)
                Form_msg.Show(this, e.Result as Exception, "StopWatch - BackUp");
            if (warning_files != null && warning_files.Count > 0)
            {
                new Form_MRB_Git_Warning(last_backup_info, warning_files).ShowDialog();
                button_refresh_Click(sender, e);
            }
            git.CloseOpennedArchives();
        }

        private void backgroundWorker_restore_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                warning_files = new List<string[]>();
                git.progress = (p, state) =>
                {
                    var act1 = new Act(() =>
                    {
                        if (p == -100)
                            warning_files.Add((string[])state);
                        else
                        {
                            toolStripProgressBar1.Visible = true;
                            toolStripStatusLabel1.Visible = true;
                            toolStripStatusLabel2.Visible = true;
                            toolStripProgressBar1.Value = Math.Min(100, (int)p);
                            toolStripStatusLabel1.Text = state + "";
                        }
                    });
                    if (InvokeRequired) Invoke(act1);
                    else act1();
                    return null;
                };
                git.Restore((e.Argument as object[])[0] as MRB_Git.BackupInfo, (e.Argument as object[])[1] as string);
            }
            catch (Exception ex)
            { e.Result = ex; }
        }

        private void backgroundWorker_restore_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
            toolStripStatusLabel2.Visible = false;
            if (e.Result is Exception)
                Form_msg.Show(this, e.Result as Exception, "StopWatch - Restore");
            if (warning_files != null && warning_files.Count > 0)
            {
                var res = "";
                foreach (var f in warning_files)
                    res += f[0] + "\r\n";
                Form_msg.Show(this, "فایل های زیر کپی نشدند، زیرا از سرور حذف شده اند" + "\r\n\r\n" + res, icon: MessageBoxIcon.Error, rtl: RightToLeft.No);
            }
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            if (Form_msg.Show(this, "لغو عملیات؟", btn: MessageBoxButtons.YesNo) == DialogResult.Yes)
                git.abort = true;
        }

        private void listfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textBox_project_TextChanged(object sender, EventArgs e)
        {
            textBox_server_TextChanged(this, null);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            var dir = textBox_server.Text + "\\" + textBox_project.Text;
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            System.Diagnostics.Process.Start(dir);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(textBox_local.Text);
        }

        private void textBox_skip_pat_TextChanged(object sender, EventArgs e)
        {
            project.BackUpSkipPattern = textBox_skip_pat.Text;
            git.settings.ignor_files = new List<string>(project.BackUpSkipPattern.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));

        }

        private void checkBox_network_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                Refresh();
                Application.DoEvents();
                button_browse_server.Enabled =
                textBox_server.Enabled =
                    !checkBox_network.Checked;
                git.CloseBackUpDrive();
                if (checkBox_network.Checked)
                {
                    var res = User.Check(db.user.ID, db.user.PASS);
                    if (res == "ok")
                    {
                        textBox_server.Text = "";
                        textBox_server_TextChanged(this, null);
                    }
                    else
                    {
                        Form_msg.Show(this, "خطا در اتصال به شبکه" + "\r\n'" + res + "'");
                        new Form_user(db.user?.ID, db.user?.PASS).ShowDialog();
                        res = User.Check(db.user.ID, db.user.PASS);
                        if (res == "ok")
                        {
                            textBox_server.Text = "";
                            textBox_server_TextChanged(this, null);
                        }
                        else
                            checkBox_network.Checked = false;
                    }
                }
            }
            finally
            {
                this.Enabled = true;
            }
        }
        private void Form_MRB_Git_FormClosed(object sender, FormClosedEventArgs e)
        {
            git.CloseBackUpDrive(0);
        }

        private void Form_MRB_Git_Shown(object sender, EventArgs e)
        {
            if (project != null)
                button_refresh_Click(sender, e);
        }

        private void button_view_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    git.OpenBackUpDrive();
                }
                catch (Exception ex)
                {
                    Form_msg.Show(this, new Exception("خطا در باز کردن درایو شبکه" + " : " + ex.Message, ex));
                    return;
                }
                if (dataGridView_backups.SelectedRows.Count == 0) return;
                var b = dataGridView_backups.SelectedRows[0].Cells[0].Value as MRB_Git.BackupInfo;
                if (b.InfoSection["user"] != textBox_user.Text)
                {
                    var U = WebServer.Default.GetUserInfo(textBox_user.Text);
                    if (U.GetSection(0)["level"] != "مدیر_مرکز" &&
                        U.GetSection(0)["level"] != "مدیران" &&
                        U.GetSection(0)["level"] != "کنترل" &&
                        U.GetSection(0)["level"] != "مدیر_فنی")
                        return;
                }
                new Forms.BackUpProj.Form_MRB_Git_view(git, b).ShowDialog();
            }
            finally
            {
                git.CloseBackUpDrive();
            }
        }

        private void dataGridView_backups_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button_view.PerformClick();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (!textBox_user.AutoCompleteCustomSource.Contains(textBox_user.Text.Trim()))
                {
                    Form_msg.Show(this, new Exception("نام کاربر درست وارد نشده است!"));
                    return;
                }
                else
                {
                    foreach (IniFile.IniSection u in Users.Sections)
                        if ((u["name"] + "").Trim() == git.user)
                        {
                            git.user = textBox_user.Text = (u["id"] + "").Trim();
                            break;
                        }
                }
                if (textBox_project.Text.Trim() == "")
                {
                    Form_msg.Show(this, new Exception("کد پروژه درست وارد نشده است!"));
                    return;
                }
                project = db.GetByCode(textBox_project.Text.Trim()) ??
                    new Project()
                    {
                        code = textBox_project.Text.Trim()
                    };
                label_proj_name.Text = project.Name;
                button_refresh_Click(this, null);
                Height = height_full;
                button_ok.Visible = false;
                textBox_project.ReadOnly = true;
                textBox_user.ReadOnly = true;
            }
            catch { }
        }
    }
}
