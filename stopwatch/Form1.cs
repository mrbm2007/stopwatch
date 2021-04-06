using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Text.RegularExpressions;
using Prayer_Time_Project;

namespace stopwatch
{
    public partial class Form1 : Form
    {
        #region Form
        public Form1()
        {
            InitializeComponent();
            /*notifyIcon1.BalloonTipClosed += (sender, e) =>
            {
                var thisIcon = (NotifyIcon)sender;
                thisIcon.Visible = false;
                thisIcon.Dispose();
            };*/
            ارتباطبهسرورToolStripMenuItem.Visible = Utils.Debug;
            w = ClientSize.Width;
            h = ClientSize.Height;
            //w = 111;
            //h = 83;
            mainForm = this;
            button_project.MouseEnter += (s, e) => { button_project.Focus(); };
            button_project.MouseWheel += (s, e) =>
            {
                if (e.X > 0 && e.Y > 0 && e.X < button_project.Width && e.Y < button_project.Height)
                    try
                    {
                        state = State.stopped;
                        var P = db.ActiveProjects;
                        P.Remove(db.GetByType(hozoor: true));
                        int i = 0;
                        for (; i < P.Count; i++)
                            if (P[i] == db.project) break;
                        if (e.Delta > 0)
                            i++;
                        else
                            i--;
                        if (i < 0) i += P.Count;
                        if (i > P.Count - 1) i -= P.Count;
                        db.project = P[i];
                        db.project.LastTime.Comment = db.project.LastComment;
                        toolTip1.SetToolTip(button_project, db.project.Name + "\r\n" + db.project.LastComment);
                        button_project.Text = db.project.Name;
                    }
                    catch { }
            };
            button_report.DoubleClick += sadidReportToolStripMenuItem_Click;

        }

        public Sunisoft.IrisSkin.SkinEngine skin = null;
        internal System.ComponentModel.IContainer Components
        {
            get { return this.components; }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            try
            {
                //notifyIcon1.ShowBalloonTip(100, "stopwatch", "Starting... ", ToolTipIcon.None);
                try
                {
                    if (File.Exists(DataFile))
                        db.Load(DataFile);
                    else
                    {
                        var backs = Directory.GetFiles(Utils.dir, "backup-??.??.??-*[*]*.stopwatch");
                        if (Program.AppSetting.FirstUse && backs.Length == 0)
                        {
                            db = new MyData();
                            db.Save(DataFile, true);
                        }
                        else
                        {
                            if (Form_msg.Show(this,
                            "فایل اطلاعات برنامه پیدا نشد:" + "\r\n" + DataFile
                            + "\r\n" + "اگر اولین بار است، یک فایل داده جدید ایجاد خواهد شد."
                            + "\r\n" + "آیا قبلا از این برنامه در سیستم تان استفاده می کرده اید؟",
                            btn: MessageBoxButtons.YesNo,
                            icon: MessageBoxIcon.Warning) == DialogResult.Yes)
                                db.Load(DataFile);
                            else
                            {
                                db = new MyData();
                                db.Save(DataFile, true);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error_Handler.ErrorHandler.handel(ex, "Error in Load data (@Start): " + ex.Message);
                    var loaded = false;
                    try
                    {
                        var backs = Directory.GetFiles(Utils.dir, "backup-??.??.??-*[*]*.stopwatch");
                        if (backs.Length > 0)
                        {
                            var files = new List<string>();
                            var times = new List<DateTime>();
                            var reg = new System.Text.RegularExpressions.Regex(@"backup-(\d\d\.\d\d\.\d\d)-(\d\d)\.(\d\d) \[.+] \.stopwatch");
                            foreach (var b in backs)
                            {
                                var M = reg.Match(b);
                                if (M.Groups.Count > 3)
                                {
                                    var Hour = Convert.ToInt32(M.Groups[2].Value);
                                    var Minute = Convert.ToInt32(M.Groups[3].Value);
                                    var time = Utils.PersianParse(M.Groups[1].Value + "", Hour, Minute, 0);
                                    int j = 0;
                                    for (int i = 0; i < times.Count; i++)
                                        if (times[i] > time)
                                            j = i + 1;
                                    times.Insert(j, time);
                                    files.Insert(j, b);
                                }
                            }
                            {
                                int i = 0;
                                for (; i < files.Count; i++)
                                {
                                    if (MyData.LoadTest(files[i]))
                                        break;
                                }
                                if (i < files.Count &&
                                    Form_msg.Show(this,
                                         "خطایی هنگام بارگذاری اطلاعات رخ داده است." + "\r\n" +
                                         "آیا مایل هستید از آخرین فایل پشتیبان استفاده شود؟" + "\r\n" +
                                         Path.GetFileNameWithoutExtension(files[i]),
                                         btn: MessageBoxButtons.YesNo,
                                         icon: MessageBoxIcon.Warning
                                     ) == DialogResult.Yes)
                                {
                                    db.Load(files[i]);
                                    loaded = true;
                                }
                            }
                        }
                    }
                    catch { loaded = false; }
                    if (!loaded)
                        if (Form_msg.Show(this,
                            "اکیدا توصیه می شود از برنامه خارج شده و برنامه را مجددا باز کنید تا اطلاعاتتان از دست نرود.\r\nآیا می خواهید برنامه بسته شود؟",
                            btn: MessageBoxButtons.YesNo,
                            icon: MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            db = null;
                            Close();
                        }
                }
                Form_settings.ReApplySkin();
                Utils.DoWithDelay(() =>
                {
                    try
                    {
                        //if (skin != null) skin.Active = skin.Active;
                        using (var frm = new Form() { FormBorderStyle = FormBorderStyle.None, Width = 0 })
                        {
                            frm.Show();
                            frm.Hide();
                        }
                    }
                    catch { }
                }, 500);
                try
                {
                    this.TopMost = Program.AppSetting.ontop;
                    timer_notif.Enabled = db.settings.notification;
                    if (db.project != null)
                        db.project = db.GetByName(db.project?.Name);
                    if (db.project == null && db.ActiveProjects.Count > 1)
                    {
                        db.project = db.ActiveProjects[0];
                    }
                    button_project.Text = db.project?.Name + "";
                    toolTip1.SetToolTip(button_project, db.project?.Name + "\r\n" + db.project?.LastComment);
                    button_start.Enabled = true;
                }
                catch { button_project.Text = "???"; }
                Utils.Log();
                Utils.Log("### opened " + Version);
                try
                {
                    if (!Utils.Debug)
                    {
                        Text = "Stopwatch " + Version;
                    }
                    else
                    {
                        Text = "! Debug !";
                        notifyIcon1.Icon = Icon.FromHandle(((Bitmap)Properties.Resources.delete).GetHicon());
                    }
                    toolTip1.SetToolTip(button_settings, "Setting\r\n" + Text);
                }
                catch { }
                UpdateProgress();
                RegHotKey();
            }
            finally
            {
                Program.AppSetting.FirstUse = false;
                Program.AppSetting.Save();
            }
            db.settings = db.settings ?? new Settings();
            if (db.GetByType(hozoor: true) == null)
                db.Projects.Add(new Project() { Name = "ورود-خروج", LastComment = "شرکت", Active = true });
            button_EnterExit.BackColor =
                 db.settings.Entered.Year == 1000 ? Color.White : Color.FromArgb(0x98, 0xFC, 0xA9);
            try
            {
                Form_Gages.InitGages();
            }
            catch { }

            try
            {
                if (db.resume_project_at_startup != null)
                {
                    db.project = db.resume_project_at_startup;
                    state = State.stopped;
                    button_Start_Click(this, null);
                }
                if (db.open_chat_users != null)
                    foreach (var c in db.open_chat_users)
                    {
                        if (c + "" != "")
                            new Form_chat(c).Show();
                    }
            }
            catch { }
            finally
            {
                db.resume_project_at_startup = null;
                db.open_chat_users = null;
            }
        }
        void RegHotKey()
        {
            try
            {
                var hk = Program.AppSetting.start_hotkey;
                if (hk == null)
                {
                    hk = Program.AppSetting.start_hotkey = new HotKey();
                    hk.key = '1';
                    hk.alt = true;
                }
                hk.pressed = (s) =>
                {
                    if (state == State.paused || state == State.stopped)
                        button_Start_Click(this, null);
                    if (!Visible)
                        Show();
                    Form1_MouseEnter(this, null);
                };
                if (Program.AppSetting.HotKey)
                    hk.Register();
                hk = Program.AppSetting.pause_hotkey;
                if (hk == null)
                {
                    hk = Program.AppSetting.pause_hotkey = new HotKey();
                    hk.key = '2';
                    hk.alt = true;
                }
                hk.pressed = (s) =>
                {
                    button_EnterExit_Click(this, null);
                    if (!Visible)
                        Show();
                    Form1_MouseEnter(this, null);
                };
                if (Program.AppSetting.HotKey)
                    hk.Register();
                hk = Program.AppSetting.stop_hotkey;
                if (hk == null)
                {
                    hk = Program.AppSetting.stop_hotkey = new HotKey();
                    hk.key = '3';
                    hk.alt = true;
                }
                hk.pressed = (s) =>
                {
                    if (state == State.running)
                        button_Start_Click(this, null);
                    if (!Visible)
                        Show();
                    Form1_MouseEnter(this, null);
                };
                if (Program.AppSetting.HotKey)
                    hk.Register();
            }
            catch { }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.AppSetting.Save();
            if (db != null) db.Save(DataFile, true);

            if (timer1.Enabled)
                state = State.stopped;
            try { if (NeedBackUp) BackUp(); } catch { }
            Utils.Log(String.Format("### closed ({0})", Text));
            try
            {
                Program.AppSetting.stop_hotkey.UnRegister();
                Program.AppSetting.start_hotkey.UnRegister();
            }
            catch { }
            {
                notifyIcon1.Visible = false;
                notifyIcon1.Dispose();
            }
            Form_Gages.SaveGages();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Utils.Debug)
                return;
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
                notifyIcon1.ShowBalloonTip(200, "Stopwatch", " ", ToolTipIcon.Info);
            }
        }
        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            if (FormBorderStyle == FormBorderStyle.None)
            {
                FormBorderStyle = FormBorderStyle.FixedToolWindow;
                Opacity = 1;
                TransparencyKey = Color.Empty;
                button_project.Visible = true;
                ClientSize = new Size(w, h);
                Padding = new Padding(2);
                textBox_time.Dock = DockStyle.None;
                timer2.Start();
                try
                {
                    if (skin != null) skin.Active = skin.Active;
                }
                catch { }
                UpdateLast();
                GC.Collect();
            }
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 100)
            {
                var ver_cur = Utils.Version2Int(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
                var ver = (int)m.WParam;
                if (ver == ver_cur)
                {
                    notifyIcon1.ShowBalloonTip(2000, "stopwatch", "I am here!", ToolTipIcon.Info);
                    notifyIcon1_MouseDoubleClick(this, null);
                }
                else if (ver > ver_cur)
                {
                    Program.AppSetting.Save();
                    if (db != null) db.Save(DataFile);
                    Application.Exit();
                }
            }
        }
        int w, h;

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (Application.OpenForms.Count > 1) return;
            int x = Cursor.Position.X, y = Cursor.Position.Y;
            if (x < Left - 10 || x > Right + 10 || y < Top - 40 || y > Bottom + 10)
            {
                button_project.Focus();
                Refresh();
                FormBorderStyle = FormBorderStyle.None;
                Opacity = .75;
                //TransparencyKey = textBox_time.BackColor;
                button_project.Visible = false;
                Padding = new Padding(0);
                textBox_time.Dock = DockStyle.Fill;
                Width = 80;
                Height = textBox_time.Height + 3;
                timer2.Stop();
            }
            timer2.Interval = 2500;
            RunCommands();
        }
        #endregion

        #region Update
        DateTime last_check_update_done = DateTime.Now.AddDays(-1);
        [MethodImpl(MethodImplOptions.Synchronized)]
        internal void CheckUpdate(bool force = false)
        {
            var ver = 0;
            var th = new Thread(() =>
            {
                ver = WebServer.Default.GetLastVersionInfo();
                last_check_update_done = DateTime.Now;
            });
            th.Start();
            for (int i = 0; i < 300; i++)
            {
                if (!th.IsAlive) break;
                Thread.Sleep(100);
                Application.DoEvents();
            }
            th = new Thread(() =>
            {
                var act1 = new Act(() =>
                {
                    try
                    {
                        var file = "";
                        var ver_cur = Utils.Version2Int(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
                        if (ver_cur < Math.Abs(ver) || force)
                        {
                            var auto = Program.AppSetting.auto_update;
                            if (ver < 0)
                                auto = false;
                            if (auto || Form_msg.Show(null,
                                "نسخه جدیدی از برنامه در دسترس است" + "\r\n"
                                + Utils.Int2Version(Math.Abs(ver)) + "\r\n"
                                + "بروز رسانی انجام شود?", btn: MessageBoxButtons.YesNo) == DialogResult.Yes)
                                try
                                {
                                    if (!auto)
                                        notifyIcon1.ShowBalloonTip(500, "Update", "Download file from server", ToolTipIcon.Info);
                                    file = WebServer.Default.GetUpdateSetup(ver, ver_cur);
                                }
                                catch (Exception ex)
                                {
                                    notifyIcon1.ShowBalloonTip(500, "Update", "Error in Download file from server: " + ex.Message, ToolTipIcon.Error);
                                }
                            else
                                file = "";
                        }
                        else
                            file = "";

                        if (file != "")
                        {
                            if (!Program.AppSetting.auto_update)
                                notifyIcon1.ShowBalloonTip(500, "Update", "Installing", ToolTipIcon.Info);
                            if (db != null)
                            {
                                try
                                {
                                    db.open_chat_users = new string[50];
                                    int i = 0;
                                    foreach (var f in Application.OpenForms)
                                        if (f is Form_chat)
                                            db.open_chat_users[i++] = ((Form_chat)f).name_to + "";
                                }
                                catch { }
                                if (state == State.running)
                                    db.resume_project_at_startup = db.project;
                                db.Save(DataFile);
                            }
                            Process.Start(file, "/VERYSILENT");
                            Application.Exit();
                        }
                    }
                    catch { }
                });
                if (InvokeRequired)
                    Invoke(act1);
                else
                    act1();
            });
            th.Start();
        }

        private void forceupdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckUpdate(true);
        }
        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckUpdate();
        }
        #endregion

        #region Data management
        static MyData db
        {
            get { return MyData.db; }
            set { MyData.db = value; }
        }
        public static Form1 mainForm;
        internal static string DataFile
        {
            get
            {
                return Utils.dir + "data";
            }
        }

        long skiper = 0;
        /// <summary>
        /// for lag calculation
        /// </summary>
        DateTime last_sample;
        /// <summary>
        /// ms
        /// </summary>
        double lag;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (db.project.LastTime != currentTime)
            {
                state = State.stopped;
                timer1.Stop();
                return;
            }
            if (DateTime.Now.Day != db.project.LastTime.Start.Day
                || (DateTime.Now.Hour > 6 && db.project.LastTime.Start.Hour < 6)
                || (DateTime.Now.Hour > 12 && db.project.LastTime.Start.Hour < 12)
                || (DateTime.Now.Hour > 18 && db.project.LastTime.Start.Hour < 18))
            {
                state = State.stopped;
                timer1.Stop();
                button_Start_Click(sender, e);
                return;
            }
            lag += (DateTime.Now - last_sample).TotalMilliseconds - 1000;
            db.project.LastTime.Duration.Add();
            last_sample = DateTime.Now;
            if (lag >= 1000)
            {
                db.project.LastTime.Duration.Add();
                lag -= 1000;
            }
            textBox_time.Text = db.project.LastTime.Duration.ToString();
            if (ProjPanel != null)
                ProjPanel.label1.Text = Text;

            if (skiper % 60 == 0) // 1 min
            {
                if (Utils.GetIdleTime() > 60 * 15)
                {
                    timer1.Stop();
                    Program.SetForegroundWindow(this.Handle);
                    int idel = (int)Utils.GetIdleTime();
                    db.project.LastTime.Duration.Add(-idel);
                    textBox_time.Text = db.project.LastTime.Duration.ToString();
                    using (var frm = new Form_idel(idel))
                    {
                        frm.ShowDialog(this);
                        db.project.LastTime.Duration.Add(frm.dt);
                        if (frm.dt.TotalSecond <= 0)
                            Stop();
                        else
                        {
                            last_sample = DateTime.Now;
                            lag = 0;
                            timer1.Start();
                        }
                    }
                }
                if (skiper % 15 * 60 == 0)// 15 min
                {
                    NeedNetBackUp = NeedBackUp = true;
                    try
                    {
                        Program.AppSetting.Save();
                        db.Save(DataFile);
                    }
                    catch (Exception ex)
                    {
                        Error_Handler.ErrorHandler.handel(ex, "Error in save data (@timer): " + ex.Message);
                        try { BackUp(); } catch { }
                    }
                }
                if (skiper % 5 * 60 == 0)// 5 min
                {
                    UpdateProgress();
                }
            }
            if (skiper++ > long.MaxValue - 100)
                skiper = 0;
        }

        #endregion

        #region user performance  
        internal void _UpdateProgress()
        {
            Form_EnterExit.UpdateHozorEndTime();
            progressBar_day_perform.Value = 0;
            try
            {
                var date_start = (DateTime.Now.AddHours(-6)).Date.AddHours(6);
                var mid_date = date_start.AddHours(12);
                var T = 0;
                foreach (var p in db.ActiveProjects)
                    if (!p.IsHozoor)
                        foreach (var t in p.Times)
                        {
                            if (Math.Abs((t.Start - mid_date).TotalMinutes) < 20 * 60)
                            {
                                var day = 24 * 60 * 60.0;
                                var start = (t.Start - date_start).TotalSeconds;
                                double durat = t.Duration.sec;
                                if (start < 0) { durat += start; start = 0; }
                                if (durat + start > day) durat = day - start;
                                if (durat < 0) durat = 0;
                                T += (int)durat;
                            }
                        }
                var max_ = Program.AppSetting.PerformMax;
                if (date_start.DayOfWeek == DayOfWeek.Thursday)
                    max_ = Program.AppSetting.PerformMax2;
                if (date_start.DayOfWeek == DayOfWeek.Friday)
                    max_ = Program.AppSetting.PerformMax3;
                if (max_ == 0) max_ = 8 * 3600;
                progressBar_day_perform.Value = Math.Min(100, (100 * T) / max_);
                Text = progressBar_day_perform.Value + "% : " + new TSpan() { sec = T }.ToString(false);
                if (Utils.Debug) Text = "! DEBUG ! - close";
                toolTip1.SetToolTip(progressBar_day_perform, Text + " / " + new TSpan(Program.AppSetting.PerformMax).ToString(false) + " for today");
                if (ProjPanel != null)
                    ProjPanel.progressBar1.Value = progressBar_day_perform.Value;
                state = state;
                UpdateLast();
            }
            catch { }
        }
        internal void UpdateProgress()
        {
            var invoker = new MethodInvoker(_UpdateProgress);
            ThreadPool.QueueUserWorkItem((x) =>
            {
                if (this.InvokeRequired)
                    this.Invoke(invoker);
                else
                    invoker();
            });
        }
        public void UpdateLast()
        {
            if (state == State.stopped)
            {
                var date = DateTime.Now.Date;
                var dt = -1;
                foreach (var p in db.ActiveProjects)
                    if (!p.IsHozoor)
                        if (p.Times.Count > 0
                            && p.LastTime.End.Date == date
                            && (DateTime.Now - p.LastTime.End).TotalSeconds > dt)
                            dt = (int)(DateTime.Now - p.LastTime.End).TotalSeconds;
                textBox_time.Font = new Font(textBox_time.Font.FontFamily, 7.5f, FontStyle.Regular);
                textBox_time.Text = new TSpan(dt).ToString(false) + " ago";
            }
        }
        private void progressBar_day_perform_Click(object sender, EventArgs e)
        {
            Form_EnterExit.UpdateHozorEndTime();
            (new Form_performance()).Show();
            /*using (var frm = new Forms.Form_performance())
            {
                frm.ShowDialog();
            }*/
        }
        #endregion

        #region Button Click
        internal static bool send_notif = true;
        internal void button_Start_Click(object sender, EventArgs e)
        {
            if (send_notif)
            {
                if (Utils.PersianDay(DateTime.Now) == 30 && (DateTime.Now - Program.AppSetting.Last_Report_Save).TotalDays > 10)
                    notifyIcon1.ShowBalloonTip(15000, "ارسال ساعت کاری", "لطفا ساعات کاری ماهانه خود را ارسال کنید", ToolTipIcon.Info);
                else
                    send_notif = false;
            }
            if (state == State.running)
            {
                timer1.Stop();
                state = State.paused;
            }
            else if (state == State.stopped)
            {
                if (db.project == null)
                {
                    button_start.Enabled = false;
                    return;
                }
                db.project.Times.Add(new Time_(db.project));
                state = State.running;
                skiper = 0;
                last_sample = DateTime.Now;
                lag = 0;
                timer1.Start();
                currentTime = db.project.LastTime;
            }
            else
            {
                state = State.running;
                last_sample = DateTime.Now;
                lag = 0;
                timer1.Start();
            }
            UpdateProgress();
        }
        private void button_EnterExit_Click(object sender, EventArgs e)
        {
            var H = db.GetByType(hozoor: true);
            if (H == null)
            {
                H = new Project() { Name = "ورود-خروج", LastComment = "شرکت", Active = true };
                db.Projects.Add(H);
            }
            new Form_EnterExit().ShowDialog();
            button_EnterExit.BackColor =
                db.settings.Entered.Year == 1000 ? Color.White : Color.FromArgb(0x98, 0xFC, 0xA9);
            SetNetState();
        }
        private void button_EnterExit_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var H = db.GetByType(hozoor: true);
                if (H == null)
                {
                    H = new Project() { Name = "ورود-خروج", LastComment = "سدید", Active = true };
                    db.Projects.Add(H);
                }
                H.Active = true;
                using (var frm = new Form_addTime())
                {
                    frm.dataGridView1[0, 0].Value = "ورود-خروج";
                    frm.ShowDialog();
                }
            }
        }
        private void button_settings_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            (new Form_settings()).Show(this);
            RegHotKey();
            timer2.Start();
            timer_notif.Enabled = db.settings.notification;
            button_project.Text = db.project != null ? db.project.Name : "- !!! -";
            Program.AppSetting.Save();
        }
        Time_ currentTime;
        private void button_project_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            try
            {
                var tmp = db.project;
                (new Form_list()).Show(this);
                if (tmp != db.project)
                {
                    if (state != State.stopped)
                        state = State.stopped;
                    db.Save(DataFile);
                }
                if (db.project != null)
                {
                    if (db.project != null && db.project.LastTime != null && state != State.stopped)
                        db.project.LastTime.Comment = db.project.LastComment;
                    button_project.Text = db.project.Name;
                    toolTip1.SetToolTip(button_project, db.project.Name + "\r\n" + db.project.LastComment);
                    button_start.Enabled = true;
                }
                else
                {
                    button_project.Text = "- ??? -";
                    toolTip1.SetToolTip(button_project, "Not selected");
                    button_start.Enabled = !true;
                }
            }
            finally { timer2.Start(); }
        }
        private void button_report_Click(object sender, EventArgs e)
        {
            new Form_report_mahane().Show(this);
        }
        private void sadidReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Form_report_mahane()).Show();
        }

        private void button_plus_MouseClick(object sender, MouseEventArgs e)
        {
            if (db.project != null && state != State.stopped)
            {
                var dt = 60;
                if (e.Button == MouseButtons.Right)
                    dt = 0;
                else if (e.Button == MouseButtons.Middle)
                    dt = 10 * 60;
                MinusPlus(dt);
            }
        }
        private void button_minus_MouseClick(object sender, MouseEventArgs e)
        {
            if (db.project != null && state != State.stopped && db.project.LastTime.Duration.sec > 60)
            {
                var dt = -60;
                if (e.Button == MouseButtons.Right)
                    dt = 0;
                else if (e.Button == MouseButtons.Middle)
                    dt = -10 * 60;
                MinusPlus(dt);
            }
        }
        DateTime plus_check = DateTime.Now.AddHours(-1);
        void MinusPlus(int dt)
        {
            var t = db.project.LastTime;
            var start_ = t.Start.AddSeconds(-dt);
            if (dt > 0 && DateTime.Now > plus_check)
                foreach (var p in db.Projects)
                    if (p.Active && !p.IsHozoor && p.Times.Count > 0 && Utils.Last(p.Times).Start.AddDays(1) > DateTime.Now)
                    {
                        foreach (var t2 in p.Times)
                            if (t2.Start.AddHours(10) > DateTime.Now && t2 != t && t2.Duration.sec > 5 && t2.End > start_)
                                if (Form_msg.Show(this, "با ساعت کاری زیر همپوشانی دارد:\r\n"
                                    + p.Name + ": " + Utils.TimeString(t2.Start) + " - " + Utils.TimeString(t2.End) + "\r\n"
                                    + "ساعت کاری فعلی پس از تغییر:" + "\r\n"
                                    + Utils.TimeString(start_) + " - " + Utils.TimeString(t.End) + "\r\n"
                                    + "با این وجود ادامه می دهید؟", btn: MessageBoxButtons.YesNo) == DialogResult.No)
                                    return;
                                else
                                    plus_check = DateTime.Now.AddSeconds(30);
                    }
            t.Duration.sec += dt;
            t.Start = start_;
            textBox_time.Text = t.Duration.ToString();
            textBox_time.Focus();
            UpdateProgress();
        }
        private void button_project_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
                timer_notif_Tick(null, null);
        }
        private void button_proj_select_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            try
            {
                comboBox_project.Items.Clear();
                foreach (var p in db.ActiveProjects)
                    if (!p.IsHozoor)
                        comboBox_project.Items.Add(p);
                comboBox_project.SelectedItem = db.project;
                comboBox_project.DroppedDown = true;
                while (comboBox_project.DroppedDown)
                {
                    Application.DoEvents();
                    Thread.Sleep(100);
                }
                if (comboBox_project.SelectedItem != null)
                {
                    if (db.project != comboBox_project.SelectedItem) state = State.stopped;
                    db.project = comboBox_project.SelectedItem as Project;
                    if (db.project != null && db.project.LastTime != null && state != State.stopped)
                        db.project.LastTime.Comment = db.project.LastComment;
                    button_project.Text = db.project.Name;
                    toolTip1.SetToolTip(button_project, db.project.Name + "\r\n" + db.project.LastComment);
                    toolTip1.Show(db.project.LastComment, button_project, 1000);
                    button_start.Enabled = true;
                }
            }
            finally
            {
                timer2.Start();
            }
        }


        private void button_project_MouseEnter(object sender, EventArgs e)
        {
            (sender as Button).FlatAppearance.BorderColor = Color.DarkTurquoise;
            if (sender != button_project)
                (sender as Button).FlatAppearance.BorderSize = 2;
        }

        private void button_project_MouseLeave(object sender, EventArgs e)
        {
            (sender as Button).FlatAppearance.BorderColor = Color.Gray;
            (sender as Button).FlatAppearance.BorderSize = 1;
        }
        #endregion

        #region State
        public static bool Stop()
        {
            (Application.OpenForms[0] as Form1).timer1.Stop();
            if ((Application.OpenForms[0] as Form1).state != State.stopped)
            {
                (Application.OpenForms[0] as Form1).state = State.stopped;
                return true;
            }
            return false;
        }

        State state_ = State.stopped;
        State state
        {
            get { return state_; }
            set
            {
                if (value == State.paused)
                    value = State.stopped;
                var changed = state_ != value;
                if (changed)
                {
                    last_set_statue = DateTime.Now.AddDays(-100);
                    SetNetState();
                    GC.Collect();
                }
                state_ = value;
                Program.AppSetting.Save();
                button_start.BackColor = Color.White;
                if (value == State.running)
                {
                    if (changed)
                    {
                        textBox_time.BackColor = Color.White;
                        button_start.Image =
                            toolStripMenuItem_startpause.Image = Properties.Resources.pause;
                        if (notifyIcon1.Icon != null) notifyIcon1.Icon.Dispose();
                        notifyIcon1.Icon = Icon.FromHandle((Utils.AddProgress(Properties.Resources.resume, progressBar_day_perform.Value)).GetHicon());
                        textBox_time.Font = new Font(textBox_time.Font.FontFamily, 9, FontStyle.Bold);
                        textBox_time.Text = "00:00:00";
                    }
                    button_start.BackColor = Color.FromArgb(0x98, 0xFC, 0xA9);
                    toolTip1.SetToolTip(button_start, toolStripMenuItem_startpause.Text = "توقف " /*+ Program.AppSetting.pause_hotkey*/);
                    if (changed) Utils.Log(">> running - " + db.project);
                }
                else if (value == State.paused)
                {
                    if (changed)
                    {
                        textBox_time.BackColor = Color.FromArgb(255, 255, 192);
                        button_start.Image =
                        toolStripMenuItem_startpause.Image = Properties.Resources.resume;
                        if (notifyIcon1.Icon != null) notifyIcon1.Icon.Dispose();
                        notifyIcon1.Icon = Icon.FromHandle((Utils.AddProgress(Properties.Resources.pause, progressBar_day_perform.Value)).GetHicon());
                    }
                    toolTip1.SetToolTip(button_start, toolStripMenuItem_startpause.Text = "ادامه " /*+ Program.AppSetting.start_hotkey*/);

                    Utils.Log("|| paused - " + db.project);
                }
                else
                {
                    if (changed)
                    {
                        textBox_time.BackColor = Color.FromArgb(224, 224, 224);
                        button_start.Image =
                        toolStripMenuItem_startpause.Image = Properties.Resources.start;
                        if (notifyIcon1.Icon != null) notifyIcon1.Icon.Dispose();
                        notifyIcon1.Icon = Icon.FromHandle((Utils.AddProgress(Properties.Resources.stop, progressBar_day_perform.Value)).GetHicon());
                    }
                    toolTip1.SetToolTip(button_start, toolStripMenuItem_startpause.Text = "شروع " /*+ Program.AppSetting.start_hotkey*/);
                    Utils.Log("-- stoped - " + db.project);
                }
            }
        }
        public enum State
        {
            stopped, running, paused
        }
        #endregion

        #region notifyIcon
        DateTime baloonTip = DateTime.Now;
        private void notifyIcon1_MouseMove(object sender, MouseEventArgs e)
        {
            notifyIcon1.Text = "StopWatch\r\n"
                + Utils.DateString() + " - " + Utils.DayString() + "\r\n"
                + DateTime.Now.ToLongDateString();
            if (!db.settings.notification) return;
            if ((DateTime.Now - baloonTip).TotalSeconds > 10)
                try
                {
                    baloonTip = DateTime.Now;
                    notifyIcon1.ShowBalloonTip(500,
                        button_project.Text,
                            toolTip1.GetToolTip(progressBar_day_perform).Replace("for today", "") + "\n"
                            + toolTip1.GetToolTip(button_project) + "\n"
                            + Utils.DateString(DateTime.Now) + " " + Utils.DayString(),
                        ToolTipIcon.Info);
                    new System.Threading.Timer((s) =>
                    {
                        notifyIcon1.Visible = false;
                        notifyIcon1.Visible = true;
                    }, null, 5000, Timeout.Infinite);
                }
                catch { }
        }
        Form tmpform;
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            baloonTip = DateTime.Now.AddSeconds(-1000);
            timer2.Interval = 5000;
            timer2.Stop();
            timer2.Start();
            Form1_MouseEnter(this, null);
            UpdateProgress();
            Program.SetForegroundWindow(this.Handle);
            {
                if (tmpform != null)
                    tmpform.Close();
                var screenCapture = new Bitmap(
                        Screen.PrimaryScreen.Bounds.Width,
                        Screen.PrimaryScreen.Bounds.Height);
                {
                    using (var g = Graphics.FromImage(screenCapture))
                    {
                        g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                         Screen.PrimaryScreen.Bounds.Y,
                                         0, 0,
                                         screenCapture.Size,
                                         CopyPixelOperation.SourceCopy);
                    }
                    tmpform = new Form()
                    {
                        FormBorderStyle = FormBorderStyle.None,
                        Top = 0,
                        Left = 0,
                        Width = Screen.PrimaryScreen.Bounds.Width,
                        Height = Screen.PrimaryScreen.Bounds.Height,
                        ShowInTaskbar = false
                    };
                    tmpform.Controls.Add(new PictureBox() { Image = Utils.MakeGrayscale(screenCapture, e == null), Dock = DockStyle.Fill });
                    var t = new System.Windows.Forms.Timer() { Enabled = true, Interval = e == null ? 1000 : 400 };
                    t.Tick += (s, e_) =>
                    {
                        if (tmpform != null)
                            tmpform.Close();
                        t.Dispose();
                        tmpform.Dispose();
                    };
                    tmpform.Show();
                }
            }
            Program.SetForegroundWindow(this.Handle);
            Show();
        }
        private void timer_notif_Tick(object sender, EventArgs e)
        {
            timer_notif.Interval = 1000 * 60 * 15;
            if (db.settings.notification)
                try
                {
                    notifyIcon1.ShowBalloonTip(500, button_project.Text, state
                        + (state != State.stopped ? "\r\n" + textBox_time.Text : "")
                        + "\r\n" + toolTip1.GetToolTip(button_project),
                        state == State.running ? ToolTipIcon.Info : (state == State.paused ? ToolTipIcon.Warning : ToolTipIcon.Error));
                }
                catch { }
            if (!timer1.Enabled)
                UpdateProgress();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1_MouseDoubleClick(sender, null);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public static string Version = GetVersion();
        public static string GetVersion()
        {
            string ver = "ver";
            try
            {
                ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
            catch { }
            return ver;
        }
        private void feedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("mailto:mrbm2007@gmail.com?subject=StopWatch-" + Version + "-"
                + Environment.MachineName + "." + MyData.db.user.name + "");
        }
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state = State.stopped;
            timer1.Stop();
        }
        private void gageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form_Gages().Show();
        }
        private void timer_notifIcon_delay_Tick(object sender, EventArgs e)
        {
            if (!db.settings.notification) return;
            notifyIcon1.Visible = false;
            notifyIcon1.Visible = true;
            try
            {
                Form_Gages.InitGages();
            }
            catch { }
            timer_notifIcon_delay.Stop();
        }
        private void monitor1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetNetState();
            new Form_users().Show();
        }
        #endregion

        #region BackUP
        DateTime last_net_back_up_done = DateTime.Now.AddDays(-1);
        private void timer_network_Tick(object sender, EventArgs e)
        {
            GC.Collect();
            ThreadPool.QueueUserWorkItem((x) =>
            {
                try
                {
                    timer_network.Interval = 1000 * 60 * 60 * 2;
                    if (NeedNetBackUp)
                        BackUp_OnServer();
                }
                catch { timer_network.Interval = 1000 * 60 * 60; }
            });
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void BackUp_OnServer(bool silent = true)
        {
            ThreadPool.QueueUserWorkItem((x) =>
            {
                try
                {
                    if (WebServer.Default.Check() == "ok")
                        if (User.Check(db.user.ID, db.user.PASS) + "" != "ok")
                        {
                            var act = new Act(() =>
                            {
                                foreach (var f in Application.OpenForms)
                                    if (f is Forms.Form_user)
                                        return;
                                var frm = new Forms.Form_user(db.user.ID, db.user.PASS);
                                frm.ShowDialog(this);
                                db.user.ID = frm.id;
                                db.user.PASS = frm.pass;
                            });
                            if (InvokeRequired)
                                Invoke(act);
                            else act();
                        }
                }
                catch { }
                try
                {
                    var res = WebServer.Default.SendBackUp(NeedNetBackUp ? db : null);
                    if (res)
                        NeedNetBackUp = false;
                    if (!silent)
                        Form_msg.Show(null, res ? "NetBackUp done" : "NetBackUp failed");
                }
                catch { }
            });
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        internal void BackUp()
        {
            new Thread(new ThreadStart(
            () =>
            {
                BackUpBinary(Utils.dir);
                BackUpStr(Utils.dir);
            })).Start();
        }
        void BackUpBinary(string dir)
        {
            var fileName = dir + "backup-"
                + Utils.DateString(DateTime.Now).Replace("/", ".") + "-"
                + Utils.TimeString(DateTime.Now).Replace(":", ".") + " [" + Version.PadLeft(9) + "] .stopwatch";
            MyData.db.Save(fileName, compressed: true);
            Utils.Log(">.>.>. Backup: " + fileName);

            try
            {
                var B = Directory.GetFiles(Utils.dir, "backup-*.stopwatch");
                int i = B.Length;
                foreach (var b in B)
                    if (i-- < 200)
                        break;
                    else if ((DateTime.Now - File.GetCreationTime(b)).TotalDays > 60)
                        File.Delete(b);
            }
            catch { }
        }
        void BackUpStr(string dir, bool summary = false)
        {
            try
            {
                var B = Directory.GetFiles(Utils.dir, "BackUp-*.txt");
                foreach (var b in B)
                    File.Delete(b);
            }
            catch { }
            return;
            /*
            var str = "";
            {
                var date_ = Utils.DateString(DateTime.Now.AddDays(5 - 2)).Substring(0, 5);
                var ym = date_.Split(new char[] { '/' });
                var date1 = DateTime.Now;
                if (Convert.ToInt16(ym[1]) > 1)
                    date1 = Utils.PersianParse(ym[0] + "/" + (Convert.ToInt16(ym[1]) - 1) + "/26");
                else
                    date1 = Utils.PersianParse((Convert.ToInt16(ym[0]) - 1) + "/12/26");
                var date2 = Utils.PersianParse(ym[0] + "/" + (Convert.ToInt16(ym[1])) + "/25");
                date1 = new DateTime(date1.Year, date1.Month, date1.Day, 0, 0, 0, 0);
                date2 = new DateTime(date2.Year, date2.Month, date2.Day, 23, 59, 59, 900);
                str += "###########################################\r\n";
                str += "      Summary for " + date_ + " - " + Utils.DateTimeString(DateTime.Now) + "\r\n";
                var sum_sum = 0.0;
                foreach (var p in db.ActiveProjects)
                    if (p.Times.Count > 0)
                    {
                        str += "    >>> " + p.Name + ": ";
                        int sum = 0;
                        foreach (var t in p.Times)
                            if (t.Start >= date1 && t.Start <= date2)
                                sum += t.Duration.sec;
                        sum_sum += sum * p.Wage / 3600.0;
                        str += new TSpan() { sec = sum } + "  -  $" + (sum * p.Wage / 3600.0).ToString("0.###") + " \r\n";
                    }
                str += "    Total: $" + sum_sum.ToString("0.###") + "\r\n";
            }
            if (summary)
            {
                var name = Environment.UserName + "(" + db.user.name + ")@" + Environment.MachineName + "." + Environment.UserName + (Utils.Debug ? " -!debug!" : "");
                File.WriteAllText(dir + "\\" + name + ".txt", str);
                return;
            }

            str += "###########################################\r\n";
            foreach (var p in db.Projects)
                if (p.Times.Count > 0)
                {
                    str += "    >>> " + p.Name + ":\r\n";
                    foreach (var t in p.Times)
                        if (t.Duration.sec > 60)
                            str += Utils.DateString(t.Start) + "\t" + t.Duration + "\r\n";
                    str += "\r\n";
                }
            var file = dir + "\\BackUp-"
                + Utils.DateString(DateTime.Now).Replace("/", ".") + "-"
                + Utils.TimeString(DateTime.Now).Replace(":", ".") + " ["
                + Version.PadLeft(9) + "] " + (Utils.Debug ? "-!debug!" : "") + ".txt";
            File.WriteAllText(file, str);
            try
            {
                var B = Directory.GetFiles(Utils.dir, "BackUp-*.txt");
                int i = B.Length;
                foreach (var b in B)
                    if (i-- < 50)
                        break;
                    else if ((DateTime.Now - File.GetCreationTime(b)).TotalDays > 20)
                        File.Delete(b);
            }
            catch { }*/
        }

        private void backUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem((x) =>
            {
                bool local = false, net = false;
                try
                {
                    BackUp();
                    local = true;
                }
                catch (Exception ex)
                {
                    Error_Handler.ErrorHandler.handel(ex, "Error in local backup: " + ex.Message);
                }
                try
                {
                    BackUp_OnServer(false);
                    net = true;
                }
                catch (Exception ex)
                {
                    Error_Handler.ErrorHandler.handel(ex, "Error in network backup: " + ex.Message);
                }
                if (local || net) try
                    {
                        MessageBox.Show(this, "Backup saved.\r\nLocal: " + (local ? "OK" : "Failed"),
                            "Stopwatch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    { }
                //if (local)
                //    Process.Start(Utils.dir);
            });
        }

        private void addManualyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new Form_addTime(db.project))
            {
                frm.ShowDialog(this);
                UpdateProgress();
            }
        }
        /// <summary>
        /// is there new times from last local backup?
        /// </summary>
        bool NeedBackUp = true;
        /// <summary>
        /// is there new times from last net backup?
        /// </summary>
        bool NeedNetBackUp = true;
        private void timer_backup_Tick(object sender, EventArgs e)
        {
            timer_backup.Interval = 59 * 60 * 1000;
            if (NeedBackUp)
                try
                {
                    BackUp();
                    NeedBackUp = false;
                }
                catch { }
            try
            {
                BackUp_OnServer();
            }
            catch { }
        }

        #endregion

        #region Project Panel

        public Form_projectPanel ProjPanel;
        private void projectPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (projectPanelToolStripMenuItem.Checked)
            {
                if (ProjPanel != null)
                    ProjPanel.Close();
                projectPanelToolStripMenuItem.Checked = false;
            }
            else
            {
                ProjPanel = new Form_projectPanel();
                ProjPanel.Show();
                projectPanelToolStripMenuItem.Checked = true;
            }
        }

        #endregion

        #region Azan
        System.Threading.Timer azan_timer1 = new System.Threading.Timer(AzanNotif1);
        System.Threading.Timer azan_timer2 = new System.Threading.Timer(AzanNotif2);
        System.Threading.Timer azan_timer3 = new System.Threading.Timer(AzanNotif3);
        System.Threading.Timer azan_timer4 = new System.Threading.Timer(AzanNotif4);
        public static void AzanNotif1(object e)
        {
            var act = new Act(() =>
              {
                  Forms.Form_notif.SHOW("اذان ظهر", "اذان ظهر",
                      "ده دقیقه تا اذان ظهر باقی است" + "\r\n" + new PrayerTimes().AzanZohr.ToString("H:mm"),
                      color: Forms.Form_notif.Colors.orange,
                      position: Forms.Form_notif.Position.TopCenter,
                      use_wait_list: false);
                  if (string.IsNullOrEmpty(Program.AppSetting.city))
                      try
                      {
                          Form_msg.Show(null, "لطفا شهر خود را برای تعیین اوقات شرعی انتخاب کنید", "اوقات شرعی");
                          mainForm.button_settings_Click(null, null);
                      }
                      catch { }
                      finally
                      {
                          Program.AppSetting.city = " ";
                      }
              });
            if (Form1.mainForm.InvokeRequired)
                Form1.mainForm.Invoke(act);
            else
                act();
        }
        public static void AzanNotif2(object e)
        {
            var act = new Act(() =>
            {
                Forms.Form_notif.SHOW("اذان ظهر",
                    "اذان ظهر", "اذان ظهر به افق تهران" + "\r\n" + new PrayerTimes().AzanZohr.ToString("H:mm"),
                    color: Forms.Form_notif.Colors.green,
                    position: Forms.Form_notif.Position.TopCenter,
                    use_wait_list: false);
            });
            if (Form1.mainForm.InvokeRequired)
                Form1.mainForm.Invoke(act);
            else
                act();
        }
        public static void AzanNotif3(object e)
        {
            var act = new Act(() =>
            {
                Forms.Form_notif.SHOW("اذان مغرب", "اذان مغرب",
                    "ده دقیقه تا اذان مغرب باقی است" + "\r\n" + new PrayerTimes().AzanMaghreb.ToString("H:mm"),
                    color: Forms.Form_notif.Colors.orange,
                    position: Forms.Form_notif.Position.TopCenter,
                    use_wait_list: false);
            });
            if (Form1.mainForm.InvokeRequired)
                Form1.mainForm.Invoke(act);
            else
                act();
        }
        public static void AzanNotif4(object e)
        {
            var act = new Act(() =>
            {
                Forms.Form_notif.SHOW("اذان مغرب",
                    "اذان مغرب", "اذان مغرب به افق تهران" + "\r\n" + new PrayerTimes().AzanMaghreb.ToString("H:mm"),
                    color: Forms.Form_notif.Colors.green,
                    position: Forms.Form_notif.Position.TopCenter,
                    use_wait_list: false);
            });
            if (Form1.mainForm.InvokeRequired)
                Form1.mainForm.Invoke(act);
            else
                act();
        }

        private void اوقاتشرعیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var P = new Prayer_Time_Project.PrayerTimes();
            var now = DateTime.Now;
            var badi = P.AzanSobh;
            var badi_ = "اذان صبح";
            if (now > badi)
            {
                badi = P.Tolue;
                badi_ = "طلوع آفتاب";
            }
            if (now > badi)
            {
                badi = P.AzanZohr;
                badi_ = "اذان ظهر";
            }
            if (now > badi)
            {
                badi = P.Ghroub;
                badi_ = "غروب";
            }
            if (now > badi)
            {
                badi = P.AzanMaghreb;
                badi_ = "اذان مغرب";
            }
            if (now > badi)
            {
                badi = P.NimeShab;
                badi_ = "نیمه شب";
            }
            Form_msg.Show(this,
                Utils.DayString(DateTime.Now) + " \t " + Utils.DateString() + " \t\t (شهر: " + Properties.Settings.Default.city + ")\r\n\r\n" +
                "اذان صبح:      " + P.AzanSobh.ToString("H:mm") + "          " +
                "طلوع آفتاب:      " + P.Tolue.ToString("H:mm") + "\r\n" +
                "اذان ظهر:      " + P.AzanZohr.ToString("H:mm") + "          " +
                "غروب:            " + P.Ghroub.ToString("H:mm") + "\r\n" +
                "اذان مغرب:    " + P.AzanMaghreb.ToString("H:mm") + "         " +
                "نیمه شب:      " + P.NimeShab.ToString("H:mm") +
                "\r\n\r\nتا " + badi_ + " " + (badi - now).Hours + " ساعت و " + (badi - now).Minutes.ToString("00") + " دقیقه"
                );
        }
        #endregion

        #region Server
        private void timer_net_Tick(object sender, EventArgs e)
        {
            if (Form1.mainForm != null)
                try
                {
                    var t = new PrayerTimes().AzanZohr - DateTime.Now;
                    // t = new TimeSpan(0, 0, 1);
                    azan_timer1.Change(Math.Max(-1, (int)(t).TotalMilliseconds - 10 * 60 * 1000), Timeout.Infinite);
                    azan_timer2.Change(Math.Max(-1, (int)(t).TotalMilliseconds), Timeout.Infinite);
                    t = new PrayerTimes().AzanMaghreb - DateTime.Now;
                    azan_timer3.Change(Math.Max(-1, (int)(t).TotalMilliseconds - 10 * 60 * 1000), Timeout.Infinite);
                    azan_timer4.Change(Math.Max(-1, (int)(t).TotalMilliseconds), Timeout.Infinite);
                }
                catch { }
            CheckMessages();
            SetNetState();
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        void CheckMessages()
        {
            ThreadPool.QueueUserWorkItem((x) =>
            {

                Exception error = null;
                try
                {
                    var L = WebServer.Default.ChatCheck();
                    if (L != null)
                        foreach (var name in L)
                            if (name.Trim() != "" && name.Trim().StartsWith(":") /*&& name.Trim() != (":")*/)
                            {
                                var name_ = name.Substring(1);
                                if (name_ == "")
                                    name_ = "سیستم";
                                Application.DoEvents();
                                /*var act1 = new Act(() =>
                                 {
                                     try
                                     {
                                         for (int i = 0; i < Application.OpenForms.Count; i++)
                                         {
                                             var frm = Application.OpenForms[i];
                                             if (frm is Form_chat && frm.Text.EndsWith(" - " + name_.Trim()))
                                             {
                                                 frm.BringToFront();
                                                 return;
                                             }
                                         }
                                         new Form_chat(name_).Show();
                                     }
                                     catch { }
                                 });*/
                                var MSG = WebServer.Default.GetNewMessages(name_);
                                var act1 = new Act(() =>
                                {
                                    try
                                    {
                                        foreach (IniFile.IniSection msg in MSG.Sections)
                                            try
                                            {
                                                var color = Forms.Form_notif.Colors.green;
                                                if (msg["file"] + "" != "")
                                                    color = Forms.Form_notif.Colors.blue;
                                                else if ((msg["msg"] + "").Contains("Error:"))
                                                    color = Forms.Form_notif.Colors.red;
                                                else if ((msg["msg"] + "").Contains("اخطار:"))
                                                    color = Forms.Form_notif.Colors.orange;
                                                else if ((msg["msg"] + "").Contains("<span><a href=``/project/"))
                                                    color = Forms.Form_notif.Colors.orange;
                                                var link = WebServer.Default.server + "chat/chat?unid=" + msg["sender_unid"];
                                                if (msg["file"] + "" != "")
                                                    link = WebServer.Default.server + "chat/dl_file?file=" + msg["file"];
                                                Forms.Form_notif.SHOW(
                                                name_,
                                                name_,
                                                msg["msg"] + (msg["file"] + "" == "" ? "" : ("\r\nفایل : " + Path.GetFileName(msg["file"]))),
                                                link,
                                                onRead: (ss, ee) => { WebServer.Default.SendData("chat", "act", "set_seen", "chid", msg["UniqueID"]); },
                                                color: color
                                                );
                                            }
                                            catch { }
                                    }
                                    catch { }
                                });
                                if (this.InvokeRequired)
                                    this.Invoke(act1);
                                else act1();
                                //return;
                            }
                }
                catch (Exception ex) { error = ex; }
                if (this.IsDisposed) return;
                if (this.InvokeRequired)
                    this.Invoke(new Action<bool>(act_set_net_timer), error != null);
                else
                    act_set_net_timer(error != null);
                if (error != null)
                    Form_msg.Show(null, error, "Error in chat");
            });
        }

        private void mapnetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var host = db.settings.host_network.Replace("\\", "");
            var user = "net";
            var pass = "123net";
            CMD.RunBatString(
 $@"
net use * /delete 
y 
cmdkey /add:{ host } /user:{ user } /pass:{ pass } 
net use * \\{ host }\shared /savecred /p:yes 
net use * \\{ host }\softwares ", visible: false, wait: false);
        }

        DateTime last_set_statue = DateTime.Now.AddDays(-1000);
        DateTime last_set_proj_backup = DateTime.Now.AddDays(-1000);
        DateTime last_run_commands = DateTime.Now.AddDays(-1000);
        /// <summary>
        /// sec
        /// </summary>
        internal int get_command_interval = 120;
        DateTime last_check_internet = DateTime.Now.AddDays(-1000);
        internal bool hozoor_remined = true;
        internal bool auto_create_net_drives = true;
        internal bool AllowWireLessOnNet = true;
        internal bool AllowInternetOnNet
        {
            get
            {
                if (MyData.db.settings.host.Contains("toba.ir"))
                    return true;
                if (MyData.db.settings.host.Contains("192.168.88.220"))
                    return true;
                if (MyData.db.settings.host.Contains("172.31.128.97"))
                    return true;
                return false;
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        internal void SetNetState()
        {
            if (!Utils.Debug && (DateTime.Now - last_set_statue).TotalSeconds < Math.Min(get_command_interval, 20)) return;
            last_set_statue = DateTime.Now;
            ThreadPool.QueueUserWorkItem((x) =>
            {
                try
                {
                    if (User.Check(db.user.ID, db.user.PASS) + "" == "ok")
                    {
                        var res_ = WebServer.Default.SendData("action",
                           "act", "status",
                           "ver", GetVersion(),
                           "system", Environment.MachineName + " . " + Environment.UserName,
                           "hozoor", db.settings.Entered.Year != 1000 ? "1" : "0");
                        if (res_.StartsWith("ok:"))
                            db.user.name = res_.Substring(3);
                    }
                    else return;

                    try
                    {
                        if (!Utils.Debug && !AllowWireLessOnNet)
                        {
                            var res = CMD.Run("netsh wlan disconnect", visible: false, wait_for_result: true);
                            if (res.Trim() != "")
                            {
                                if (!res.ToLower().Contains("no wireless"))
                                    if (!res.ToLower().Contains("is not running"))
                                        if (!res.ToLower().Contains("!!! time-out"))
                                        {
                                            WebServer.Default.SendMessage("[" + AccessLevel.مدیر_فنی + "]",
                                            "اخطار: اتصال به وایرلس در شبکه" + "\r\n" +
                                            Environment.MachineName + "." + Environment.UserName + " (" + db.user?.name + ")" + "\r\n"
                                            + res + "\r\n" + "{این هشدار به صورت اتوماتیک به سرپرستان ارسال شده است}");
                                        }
                            }
                        }
                    }
                    catch { }
                    if ((DateTime.Now - last_check_internet).TotalMinutes > 2)
                        try
                        {
                            last_check_internet = DateTime.Now;
                            if (!Utils.Debug && !AllowInternetOnNet)
                            {
                                var wb = System.Net.HttpWebRequest.Create("http://www.google.com");
                                wb.Timeout = 5 * 1000;
                                using (var sr = new StreamReader(wb.GetResponse().GetResponseStream()))
                                    sr.ReadToEnd();
                                WebServer.Default.SendMessage("[" + AccessLevel.مدیر_فنی + "]",
                                                "اخطار: اتصال به اینترنت در شبکه" + "\r\n" +
                                                Environment.MachineName + "." + Environment.UserName + " (" + db.user?.name + ")" + "\r\n"
                                                + "{این هشدار به صورت اتوماتیک به سرپرستان ارسال شده است}");

                                var res = CMD.Run("netsh wlan disconnect", visible: false, wait_for_result: true);
                                var act = new Act(() =>
                                {
                                    try
                                    {
                                        notifyIcon1.ShowBalloonTip(5000, "stopwatch", "در هنگام اتصال به شبکه نباید به اینترنت متصل باشید", ToolTipIcon.Warning);
                                    }
                                    catch { }
                                });
                                if (this.InvokeRequired) Invoke(act);
                                else act();

                            }
                        }
                        catch { }

                    if (auto_create_net_drives)
                        try
                        {
                            mapnetToolStripMenuItem_Click(this, null);
                            auto_create_net_drives = false;
                        }
                        catch { }

                    if (hozoor_remined && !Utils.Debug)
                        try
                        {
                            var act1 = new Act(() =>
                            {
                                try
                                {
                                    hozoor_remined = false;
                                    if (db.settings.Entered.Year == 1000 && (DateTime.Now - db.GetByType(hozoor: true).LastTime.End).TotalMinutes > 5)
                                        if (Form_msg.Show(this, "می خواهید حضورتان را ثبت کنید؟", btn: MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        {
                                            new Form_EnterExit().ShowDialog();
                                            button_EnterExit.BackColor =
                                                db.settings.Entered.Year == 1000 ? Color.White : Color.FromArgb(0x98, 0xFC, 0xA9);
                                            hozoor_remined = true;
                                        }
                                        else
                                            hozoor_remined = false;
                                }
                                catch { }
                            });
                            if (this.IsDisposed) return;
                            if (InvokeRequired) Invoke(act1);
                            else act1();
                        }
                        catch { }
                    if (!db.settings.BorhanBackUpShown)
                    {
                        Process.Start(WebServer.Default.server + "user/borhan_backup");
                        db.settings.BorhanBackUpShown = true;
                    }
                    if ((DateTime.Now - last_set_proj_backup).TotalMinutes > 30)
                    {
                        last_set_proj_backup = DateTime.Now;
                        var b_ini = new IniFile();
                        foreach (var p in db.ActiveProjects)
                        {
                            if (p.Times.Count > 0 && (p.code + "").Trim() != "" && (p.LocalPath + "").Trim() != "-" && !p.IsHozoor)
                            {
                                var b_sec = b_ini.AddSection(p.code);
                                b_sec["name"] = p.Name;
                                b_sec["manager"] = p.Manager;
                                b_sec["last_time"] = Utils.DateTimeString(p.LastTime.End);
                                b_sec["last_backup"] = Utils.DateTimeString(p.LastBackup);
                                if (p.LastBackup.Year < 2000)
                                    b_sec["state"] = "بدون پشتیبان, ";
                                if ((DateTime.Now - p.LastTime.End).TotalDays > 60)
                                    b_sec["state"] += "پروژه قدیمی, ";
                                if ((p.LastTime.End - p.LastBackup).TotalDays > 25)
                                    b_sec["state"] += "نیاز به پشتیبان گیری";
                                if ((2 * (p.LastTime.End - p.LastBackup).TotalDays + 1 * (DateTime.Now - p.LastBackup).TotalDays) / 3 > 45)
                                    b_sec["state"] += "(مهم)";
                            }
                        }
                        WebServer.Default.SendData("backup_proj", "act", "state", "data", b_ini.SaveString());
                    }

                    RunCommands();
                }
                catch { }
            });
        }
        void act_set_net_timer(bool error)
        {
            try
            {
                if (this.IsDisposed) return;
                if (error) timer_net.Interval += 30 * 1000;
                else if (timer_net.Interval > 25.1 * 1000) timer_net.Interval -= 5 * 1000;

                timer_net.Interval = (int)Math.Max(Math.Min(timer_net.Interval, 10 * 60 * 1000), 25 * 1000);
            }
            catch { }
        }

        private void sendmessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form_chat().Show();
        }

        private void projectBackUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Forms.Form_MRB_Git_all().Show();
        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Form_report()).Show();
        }

        private void ارتباطبهسرورToolStripMenuItem_Click(object sender, EventArgs e)
        {
            last_check_update_done = DateTime.Now.AddDays(-1);
            last_net_back_up_done = DateTime.Now.AddDays(-1);
            last_run_commands = DateTime.Now.AddDays(-1);
            last_check_internet = DateTime.Now.AddDays(-1);
            last_set_proj_backup = DateTime.Now.AddDays(-1);
            last_set_statue = DateTime.Now.AddDays(-1);
            WebServer.Default.last_check = DateTime.Now.AddDays(-1);
            //string id = "", pass = "";
            //User.GetUserPassFromSite(ref id, ref pass);
            SetNetState();
            CheckMessages();
        }

        private void timer_update_Tick(object sender, EventArgs e)
        {
            timer_update.Interval = 15 * 60 * 1000;
            try
            {
                ThreadPool.QueueUserWorkItem((x) => CheckUpdate(false));
            }
            catch { }
        }

        private void نمایشمکآدرسToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var str = CMD.Run("ipconfig /all", timeout: 2000);
            var w_mac = "";
            var E_mac = "";
            var reg = new Regex(@"\r\n\r\n\S");
            var reg2 = new Regex(@"\sPhysical\sAddress[\.\s]+:\s*([^\r]+)");
            if (!str.Contains("\r"))
            {
                reg = new Regex(@"\n\n\S");
                reg2 = new Regex(@"\sPhysical\sAddress[\.\s]+:\s*([^\n]+)");
            }
            var items = reg.Split(str);
            foreach (var L in items)
            {
                try
                {
                    if (L.StartsWith("thernet adapter Ethernet:"))
                    {
                        E_mac = reg2.Match(L).Groups[1].Value.Trim().Replace("-", ":");
                    }
                    if (L.StartsWith("ireless LAN") && !L.ToLower().Contains(" virtual "))
                    {
                        w_mac = reg2.Match(L).Groups[1].Value.Trim().Replace("-", ":");
                    }
                }
                catch { }
            }
            var frm = new Form
            {
                FormBorderStyle = FormBorderStyle.FixedToolWindow,
                Width = 190,
                Height = 150,
                Padding = new Padding(10),
                StartPosition = FormStartPosition.CenterScreen,
                Text = "MAC Address:",
            };
            frm.Controls.Add(new TextBox { Text = E_mac, Dock = DockStyle.Top, TextAlign = HorizontalAlignment.Center });
            frm.Controls.Add(new Label { Text = "Ethernet:", Dock = DockStyle.Top });
            frm.Controls.Add(new Panel { Height = 2, BackColor = Color.Black, Dock = DockStyle.Top });
            frm.Controls.Add(new Panel { Height = 10, Dock = DockStyle.Top });
            frm.Controls.Add(new TextBox { Text = w_mac, Dock = DockStyle.Top, TextAlign = HorizontalAlignment.Center });
            frm.Controls.Add(new Label { Text = "Wireless:", Dock = DockStyle.Top });

            frm.Show(this);
        }

        private void همگامسازیزمانباسرورToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        void RunCommands()
        {
            System.Threading.ThreadPool.QueueUserWorkItem((x) =>
            {
                try
                {
                    // commands 
                    if ((DateTime.Now - last_run_commands).TotalSeconds < get_command_interval)
                        return;
                    if (!WebServer.Default.IsConnected)
                        return;
                    last_run_commands = DateTime.Now;
                    {
                        var ini = WebServer.Default.GetCommands(false);
                        /*var sec1 = ini.AddSection("dir1");
                        sec1.AddKey("action", "dir");
                        sec1.AddKey("root", @"E:\Flodyn Case");
                        sec1.AddKey("level", "66");*/

                        foreach (IniFile.IniSection s in ini.Sections)
                            if (s["done"] + "" == "")
                            {
                                var c = s.Name;
                                CMD.Run(s, (res, done) => { WebServer.Default.SetCommandsRes(c, "", res, done, false); });
                            }
                    }
                    {
                        var user = CMD.UserCode;
                        var ini = WebServer.Default.GetCommands(true);
                        foreach (IniFile.IniSection s in ini.Sections)
                            if (s[user + "_done"] + "" == "")
                            {
                                var c = s.Name;
                                CMD.Run(s, (res, done) => { WebServer.Default.SetCommandsRes(c, user, res, done, true); });
                            }
                    }
                }
                catch { }
            });
        }
        #endregion

    }
}