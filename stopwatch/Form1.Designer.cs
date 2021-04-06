namespace stopwatch
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button_project = new System.Windows.Forms.Button();
            this.cMenuStrip_main = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.manageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sadidReportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.performanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gage2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapnetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.پشتیبانگیریازفایلهایپروژهToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.نمایشمکآدرسToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.checkForUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.پشتیبانگیریازپروژهToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.monitor1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendmessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ارتباطبهسرورToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_proj_select = new System.Windows.Forms.Button();
            this.button_plus = new System.Windows.Forms.Button();
            this.button_minus = new System.Windows.Forms.Button();
            this.button_EnterExit = new System.Windows.Forms.Button();
            this.button_settings = new System.Windows.Forms.Button();
            this.button_report = new System.Windows.Forms.Button();
            this.button_start = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.cMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_startpause = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.monitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.بروزرسانینسخهجدیدToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forceupdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.اوقاتشرعیToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.همگامسازیزمانباسرورToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.feadbackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer_notif = new System.Windows.Forms.Timer(this.components);
            this.timer_network = new System.Windows.Forms.Timer(this.components);
            this.timer_backup = new System.Windows.Forms.Timer(this.components);
            this.comboBox_project = new System.Windows.Forms.ComboBox();
            this.timer_notifIcon_delay = new System.Windows.Forms.Timer(this.components);
            this.timer_net = new System.Windows.Forms.Timer(this.components);
            this.timer_update = new System.Windows.Forms.Timer(this.components);
            this.textBox_time = new stopwatch.MyTextBox();
            this.progressBar_day_perform = new stopwatch.MyProgressBar();
            this.cMenuStrip_main.SuspendLayout();
            this.cMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 200;
            this.toolTip1.BackColor = System.Drawing.Color.White;
            this.toolTip1.StripAmpersands = true;
            // 
            // button_project
            // 
            this.button_project.BackColor = System.Drawing.Color.White;
            this.button_project.ContextMenuStrip = this.cMenuStrip_main;
            this.button_project.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button_project.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_project.Font = new System.Drawing.Font("Tahoma", 7F);
            this.button_project.Location = new System.Drawing.Point(2, 23);
            this.button_project.Name = "button_project";
            this.button_project.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.button_project.Size = new System.Drawing.Size(94, 24);
            this.button_project.TabIndex = 0;
            this.toolTip1.SetToolTip(this.button_project, "Comment");
            this.button_project.UseVisualStyleBackColor = false;
            this.button_project.Click += new System.EventHandler(this.button_project_Click);
            this.button_project.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_project_MouseClick);
            this.button_project.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_project_MouseClick);
            this.button_project.MouseEnter += new System.EventHandler(this.button_project_MouseEnter);
            this.button_project.MouseLeave += new System.EventHandler(this.button_project_MouseLeave);
            // 
            // cMenuStrip_main
            // 
            this.cMenuStrip_main.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cMenuStrip_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageToolStripMenuItem,
            this.addManualToolStripMenuItem,
            this.sadidReportToolStripMenuItem1,
            this.toolStripSeparator3,
            this.toolsToolStripMenuItem,
            this.toolStripSeparator5,
            this.checkForUpdateToolStripMenuItem,
            this.پشتیبانگیریازپروژهToolStripMenuItem,
            this.backUpToolStripMenuItem,
            this.toolStripSeparator7,
            this.monitor1ToolStripMenuItem,
            this.sendmessageToolStripMenuItem,
            this.ارتباطبهسرورToolStripMenuItem});
            this.cMenuStrip_main.Name = "contextMenuStrip2";
            this.cMenuStrip_main.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cMenuStrip_main.Size = new System.Drawing.Size(182, 242);
            // 
            // manageToolStripMenuItem
            // 
            this.manageToolStripMenuItem.Image = global::stopwatch.Properties.Resources.report;
            this.manageToolStripMenuItem.Name = "manageToolStripMenuItem";
            this.manageToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.manageToolStripMenuItem.Text = "مدیریت ساعت کاری";
            this.manageToolStripMenuItem.Click += new System.EventHandler(this.manageToolStripMenuItem_Click);
            // 
            // addManualToolStripMenuItem
            // 
            this.addManualToolStripMenuItem.Image = global::stopwatch.Properties.Resources.Add;
            this.addManualToolStripMenuItem.Name = "addManualToolStripMenuItem";
            this.addManualToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.addManualToolStripMenuItem.Text = "افزودن ساعت کاری";
            this.addManualToolStripMenuItem.Click += new System.EventHandler(this.addManualyToolStripMenuItem_Click);
            // 
            // sadidReportToolStripMenuItem1
            // 
            this.sadidReportToolStripMenuItem1.Image = global::stopwatch.Properties.Resources.report;
            this.sadidReportToolStripMenuItem1.Name = "sadidReportToolStripMenuItem1";
            this.sadidReportToolStripMenuItem1.Size = new System.Drawing.Size(181, 22);
            this.sadidReportToolStripMenuItem1.Text = "گزارش ماهانه";
            this.sadidReportToolStripMenuItem1.Click += new System.EventHandler(this.sadidReportToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(178, 6);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.performanceToolStripMenuItem,
            this.projectPanelToolStripMenuItem,
            this.gage2ToolStripMenuItem,
            this.mapnetToolStripMenuItem,
            this.پشتیبانگیریازفایلهایپروژهToolStripMenuItem,
            this.نمایشمکآدرسToolStripMenuItem});
            this.toolsToolStripMenuItem.Image = global::stopwatch.Properties.Resources.Toolbox_16x16;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.toolsToolStripMenuItem.Text = "ابزارها:";
            // 
            // performanceToolStripMenuItem
            // 
            this.performanceToolStripMenuItem.Image = global::stopwatch.Properties.Resources.Demo_Complex_Validation_Settings;
            this.performanceToolStripMenuItem.Name = "performanceToolStripMenuItem";
            this.performanceToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.performanceToolStripMenuItem.Text = "عملکرد";
            this.performanceToolStripMenuItem.Click += new System.EventHandler(this.progressBar_day_perform_Click);
            // 
            // projectPanelToolStripMenuItem
            // 
            this.projectPanelToolStripMenuItem.Image = global::stopwatch.Properties.Resources.list;
            this.projectPanelToolStripMenuItem.Name = "projectPanelToolStripMenuItem";
            this.projectPanelToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.projectPanelToolStripMenuItem.Text = "پنل پروژه ها";
            this.projectPanelToolStripMenuItem.Click += new System.EventHandler(this.projectPanelToolStripMenuItem_Click);
            // 
            // gage2ToolStripMenuItem
            // 
            this.gage2ToolStripMenuItem.Image = global::stopwatch.Properties.Resources.list;
            this.gage2ToolStripMenuItem.Name = "gage2ToolStripMenuItem";
            this.gage2ToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.gage2ToolStripMenuItem.Text = "شمارنده ها";
            this.gage2ToolStripMenuItem.Click += new System.EventHandler(this.gageToolStripMenuItem_Click);
            // 
            // mapnetToolStripMenuItem
            // 
            this.mapnetToolStripMenuItem.Image = global::stopwatch.Properties.Resources.ServerMode_16x16;
            this.mapnetToolStripMenuItem.Name = "mapnetToolStripMenuItem";
            this.mapnetToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.mapnetToolStripMenuItem.Text = "ایجاد درایو شبکه";
            this.mapnetToolStripMenuItem.Click += new System.EventHandler(this.mapnetToolStripMenuItem_Click);
            // 
            // پشتیبانگیریازفایلهایپروژهToolStripMenuItem
            // 
            this.پشتیبانگیریازفایلهایپروژهToolStripMenuItem.Image = global::stopwatch.Properties.Resources.report;
            this.پشتیبانگیریازفایلهایپروژهToolStripMenuItem.Name = "پشتیبانگیریازفایلهایپروژهToolStripMenuItem";
            this.پشتیبانگیریازفایلهایپروژهToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.پشتیبانگیریازفایلهایپروژهToolStripMenuItem.Text = "پشتیبان گیری از فایل های پروژه";
            this.پشتیبانگیریازفایلهایپروژهToolStripMenuItem.Click += new System.EventHandler(this.projectBackUpToolStripMenuItem_Click);
            // 
            // نمایشمکآدرسToolStripMenuItem
            // 
            this.نمایشمکآدرسToolStripMenuItem.Name = "نمایشمکآدرسToolStripMenuItem";
            this.نمایشمکآدرسToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.نمایشمکآدرسToolStripMenuItem.Text = "نمایش مک آدرس";
            this.نمایشمکآدرسToolStripMenuItem.Click += new System.EventHandler(this.نمایشمکآدرسToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(178, 6);
            // 
            // checkForUpdateToolStripMenuItem
            // 
            this.checkForUpdateToolStripMenuItem.Image = global::stopwatch.Properties.Resources.update;
            this.checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
            this.checkForUpdateToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.checkForUpdateToolStripMenuItem.Text = "بروز رسانی";
            this.checkForUpdateToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdateToolStripMenuItem_Click);
            // 
            // پشتیبانگیریازپروژهToolStripMenuItem
            // 
            this.پشتیبانگیریازپروژهToolStripMenuItem.Image = global::stopwatch.Properties.Resources.report;
            this.پشتیبانگیریازپروژهToolStripMenuItem.Name = "پشتیبانگیریازپروژهToolStripMenuItem";
            this.پشتیبانگیریازپروژهToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.پشتیبانگیریازپروژهToolStripMenuItem.Text = "پشتیبان گیری از پروژه";
            this.پشتیبانگیریازپروژهToolStripMenuItem.Click += new System.EventHandler(this.projectBackUpToolStripMenuItem_Click);
            // 
            // backUpToolStripMenuItem
            // 
            this.backUpToolStripMenuItem.Image = global::stopwatch.Properties.Resources.Save;
            this.backUpToolStripMenuItem.Name = "backUpToolStripMenuItem";
            this.backUpToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.backUpToolStripMenuItem.Text = "پشتیبان گیری";
            this.backUpToolStripMenuItem.Visible = false;
            this.backUpToolStripMenuItem.Click += new System.EventHandler(this.backUpToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(178, 6);
            // 
            // monitor1ToolStripMenuItem
            // 
            this.monitor1ToolStripMenuItem.Image = global::stopwatch.Properties.Resources.BO_Users;
            this.monitor1ToolStripMenuItem.Name = "monitor1ToolStripMenuItem";
            this.monitor1ToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.monitor1ToolStripMenuItem.Text = "مشاهده کاربران";
            this.monitor1ToolStripMenuItem.Visible = false;
            this.monitor1ToolStripMenuItem.Click += new System.EventHandler(this.monitor1ToolStripMenuItem_Click);
            // 
            // sendmessageToolStripMenuItem
            // 
            this.sendmessageToolStripMenuItem.Image = global::stopwatch.Properties.Resources.gnome_document_send;
            this.sendmessageToolStripMenuItem.Name = "sendmessageToolStripMenuItem";
            this.sendmessageToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.sendmessageToolStripMenuItem.Text = "ارسال پیام و فایل";
            this.sendmessageToolStripMenuItem.Click += new System.EventHandler(this.sendmessageToolStripMenuItem_Click);
            // 
            // ارتباطبهسرورToolStripMenuItem
            // 
            this.ارتباطبهسرورToolStripMenuItem.Name = "ارتباطبهسرورToolStripMenuItem";
            this.ارتباطبهسرورToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.ارتباطبهسرورToolStripMenuItem.Text = "ارتباط به سرور";
            this.ارتباطبهسرورToolStripMenuItem.Visible = false;
            this.ارتباطبهسرورToolStripMenuItem.Click += new System.EventHandler(this.ارتباطبهسرورToolStripMenuItem_Click);
            // 
            // button_proj_select
            // 
            this.button_proj_select.BackColor = System.Drawing.Color.White;
            this.button_proj_select.ContextMenuStrip = this.cMenuStrip_main;
            this.button_proj_select.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button_proj_select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_proj_select.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button_proj_select.Location = new System.Drawing.Point(95, 23);
            this.button_proj_select.Name = "button_proj_select";
            this.button_proj_select.Size = new System.Drawing.Size(14, 24);
            this.button_proj_select.TabIndex = 9;
            this.button_proj_select.Text = "▼";
            this.toolTip1.SetToolTip(this.button_proj_select, "انتخاب پروژه");
            this.button_proj_select.UseVisualStyleBackColor = false;
            this.button_proj_select.Click += new System.EventHandler(this.button_proj_select_Click);
            this.button_proj_select.MouseEnter += new System.EventHandler(this.button_project_MouseEnter);
            this.button_proj_select.MouseLeave += new System.EventHandler(this.button_project_MouseLeave);
            // 
            // button_plus
            // 
            this.button_plus.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button_plus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_plus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button_plus.ForeColor = System.Drawing.Color.White;
            this.button_plus.Image = global::stopwatch.Properties.Resources.arrow_top;
            this.button_plus.Location = new System.Drawing.Point(97, 0);
            this.button_plus.Name = "button_plus";
            this.button_plus.Size = new System.Drawing.Size(14, 11);
            this.button_plus.TabIndex = 6;
            this.button_plus.TabStop = false;
            this.button_plus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.button_plus, "+1 min (Middle for +10)");
            this.button_plus.UseVisualStyleBackColor = false;
            this.button_plus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_plus_MouseClick);
            // 
            // button_minus
            // 
            this.button_minus.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button_minus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_minus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button_minus.ForeColor = System.Drawing.Color.White;
            this.button_minus.Image = global::stopwatch.Properties.Resources.arrow_down;
            this.button_minus.Location = new System.Drawing.Point(97, 10);
            this.button_minus.Name = "button_minus";
            this.button_minus.Size = new System.Drawing.Size(14, 11);
            this.button_minus.TabIndex = 7;
            this.button_minus.TabStop = false;
            this.button_minus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.button_minus, "-1 min (Middle for -10)");
            this.button_minus.UseVisualStyleBackColor = false;
            this.button_minus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_minus_MouseClick);
            // 
            // button_EnterExit
            // 
            this.button_EnterExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(250)))), ((int)(((byte)(200)))));
            this.button_EnterExit.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button_EnterExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_EnterExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button_EnterExit.Image = global::stopwatch.Properties.Resources.door_out;
            this.button_EnterExit.Location = new System.Drawing.Point(29, 48);
            this.button_EnterExit.Name = "button_EnterExit";
            this.button_EnterExit.Size = new System.Drawing.Size(26, 26);
            this.button_EnterExit.TabIndex = 2;
            this.toolTip1.SetToolTip(this.button_EnterExit, "ثبت ورود و خروج");
            this.button_EnterExit.UseVisualStyleBackColor = false;
            this.button_EnterExit.Click += new System.EventHandler(this.button_EnterExit_Click);
            this.button_EnterExit.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_EnterExit_MouseClick);
            this.button_EnterExit.MouseEnter += new System.EventHandler(this.button_project_MouseEnter);
            this.button_EnterExit.MouseLeave += new System.EventHandler(this.button_project_MouseLeave);
            // 
            // button_settings
            // 
            this.button_settings.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button_settings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_settings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button_settings.Image = global::stopwatch.Properties.Resources.gear_blue__1_;
            this.button_settings.Location = new System.Drawing.Point(83, 48);
            this.button_settings.Name = "button_settings";
            this.button_settings.Size = new System.Drawing.Size(26, 26);
            this.button_settings.TabIndex = 4;
            this.toolTip1.SetToolTip(this.button_settings, "تنظیمات");
            this.button_settings.UseVisualStyleBackColor = false;
            this.button_settings.Click += new System.EventHandler(this.button_settings_Click);
            this.button_settings.MouseEnter += new System.EventHandler(this.button_project_MouseEnter);
            this.button_settings.MouseLeave += new System.EventHandler(this.button_project_MouseLeave);
            // 
            // button_report
            // 
            this.button_report.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button_report.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_report.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button_report.Image = global::stopwatch.Properties.Resources.poll_blue;
            this.button_report.Location = new System.Drawing.Point(56, 48);
            this.button_report.Name = "button_report";
            this.button_report.Size = new System.Drawing.Size(26, 26);
            this.button_report.TabIndex = 3;
            this.toolTip1.SetToolTip(this.button_report, "گزارش");
            this.button_report.UseVisualStyleBackColor = false;
            this.button_report.Click += new System.EventHandler(this.button_report_Click);
            this.button_report.MouseEnter += new System.EventHandler(this.button_project_MouseEnter);
            this.button_report.MouseLeave += new System.EventHandler(this.button_project_MouseLeave);
            // 
            // button_start
            // 
            this.button_start.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button_start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button_start.Image = global::stopwatch.Properties.Resources.start;
            this.button_start.Location = new System.Drawing.Point(2, 48);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(26, 26);
            this.button_start.TabIndex = 1;
            this.toolTip1.SetToolTip(this.button_start, "شروع");
            this.button_start.UseVisualStyleBackColor = false;
            this.button_start.Click += new System.EventHandler(this.button_Start_Click);
            this.button_start.MouseEnter += new System.EventHandler(this.button_project_MouseEnter);
            this.button_start.MouseLeave += new System.EventHandler(this.button_project_MouseLeave);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 4000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.ContextMenuStrip = this.cMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "StopWatch";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            this.notifyIcon1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseMove);
            // 
            // cMenuStrip1
            // 
            this.cMenuStrip1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripMenuItem_startpause,
            this.stopToolStripMenuItem,
            this.toolStripSeparator2,
            this.monitorToolStripMenuItem,
            this.chatToolStripMenuItem,
            this.gageToolStripMenuItem,
            this.toolStripSeparator6,
            this.settingsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.اوقاتشرعیToolStripMenuItem,
            this.همگامسازیزمانباسرورToolStripMenuItem,
            this.feadbackToolStripMenuItem,
            this.backupToolStripMenuItem1,
            this.toolStripSeparator8,
            this.exitToolStripMenuItem});
            this.cMenuStrip1.Name = "contextMenuStrip1";
            this.cMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cMenuStrip1.Size = new System.Drawing.Size(208, 330);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.openToolStripMenuItem.Image = global::stopwatch.Properties.Resources.log;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.openToolStripMenuItem.Text = "پنجره اصلی";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripMenuItem_startpause
            // 
            this.toolStripMenuItem_startpause.Image = global::stopwatch.Properties.Resources.start;
            this.toolStripMenuItem_startpause.Name = "toolStripMenuItem_startpause";
            this.toolStripMenuItem_startpause.Size = new System.Drawing.Size(207, 22);
            this.toolStripMenuItem_startpause.Text = "شروع";
            this.toolStripMenuItem_startpause.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Image = global::stopwatch.Properties.Resources.stop;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.stopToolStripMenuItem.Text = "توقف";
            this.stopToolStripMenuItem.Visible = false;
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(204, 6);
            // 
            // monitorToolStripMenuItem
            // 
            this.monitorToolStripMenuItem.Image = global::stopwatch.Properties.Resources.BO_Users;
            this.monitorToolStripMenuItem.Name = "monitorToolStripMenuItem";
            this.monitorToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.monitorToolStripMenuItem.Text = "مشاهده کاربران";
            this.monitorToolStripMenuItem.Click += new System.EventHandler(this.monitor1ToolStripMenuItem_Click);
            // 
            // chatToolStripMenuItem
            // 
            this.chatToolStripMenuItem.Image = global::stopwatch.Properties.Resources.gnome_document_send;
            this.chatToolStripMenuItem.Name = "chatToolStripMenuItem";
            this.chatToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.chatToolStripMenuItem.Text = "ارسال پیام و فایل";
            this.chatToolStripMenuItem.Click += new System.EventHandler(this.sendmessageToolStripMenuItem_Click);
            // 
            // gageToolStripMenuItem
            // 
            this.gageToolStripMenuItem.Image = global::stopwatch.Properties.Resources.list;
            this.gageToolStripMenuItem.Name = "gageToolStripMenuItem";
            this.gageToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.gageToolStripMenuItem.Text = "شمارنده";
            this.gageToolStripMenuItem.Click += new System.EventHandler(this.gageToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(204, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = global::stopwatch.Properties.Resources.settings;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.settingsToolStripMenuItem.Text = "تنظیمات";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.button_settings_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.بروزرسانینسخهجدیدToolStripMenuItem,
            this.forceupdateToolStripMenuItem});
            this.toolStripMenuItem1.Image = global::stopwatch.Properties.Resources.update;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(207, 22);
            this.toolStripMenuItem1.Text = "بروز رسانی";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.checkForUpdateToolStripMenuItem_Click);
            // 
            // بروزرسانینسخهجدیدToolStripMenuItem
            // 
            this.بروزرسانینسخهجدیدToolStripMenuItem.Name = "بروزرسانینسخهجدیدToolStripMenuItem";
            this.بروزرسانینسخهجدیدToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.بروزرسانینسخهجدیدToolStripMenuItem.Text = "بروز رسانی نسخه جدید";
            this.بروزرسانینسخهجدیدToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdateToolStripMenuItem_Click);
            // 
            // forceupdateToolStripMenuItem
            // 
            this.forceupdateToolStripMenuItem.Name = "forceupdateToolStripMenuItem";
            this.forceupdateToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.forceupdateToolStripMenuItem.Text = "بروز رسانی اجباری";
            this.forceupdateToolStripMenuItem.Click += new System.EventHandler(this.forceupdateToolStripMenuItem_Click);
            // 
            // اوقاتشرعیToolStripMenuItem
            // 
            this.اوقاتشرعیToolStripMenuItem.Image = global::stopwatch.Properties.Resources.pt;
            this.اوقاتشرعیToolStripMenuItem.Name = "اوقاتشرعیToolStripMenuItem";
            this.اوقاتشرعیToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.اوقاتشرعیToolStripMenuItem.Text = "اوقات شرعی";
            this.اوقاتشرعیToolStripMenuItem.Click += new System.EventHandler(this.اوقاتشرعیToolStripMenuItem_Click);
            // 
            // همگامسازیزمانباسرورToolStripMenuItem
            // 
            this.همگامسازیزمانباسرورToolStripMenuItem.Name = "همگامسازیزمانباسرورToolStripMenuItem";
            this.همگامسازیزمانباسرورToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.همگامسازیزمانباسرورToolStripMenuItem.Text = "همگام سازی زمان با سرور";
            this.همگامسازیزمانباسرورToolStripMenuItem.Visible = false;
            this.همگامسازیزمانباسرورToolStripMenuItem.Click += new System.EventHandler(this.همگامسازیزمانباسرورToolStripMenuItem_Click);
            // 
            // feadbackToolStripMenuItem
            // 
            this.feadbackToolStripMenuItem.Image = global::stopwatch.Properties.Resources.ok;
            this.feadbackToolStripMenuItem.Name = "feadbackToolStripMenuItem";
            this.feadbackToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.feadbackToolStripMenuItem.Text = "انتقاد و پیشنهاد";
            this.feadbackToolStripMenuItem.Click += new System.EventHandler(this.feedbackToolStripMenuItem_Click);
            // 
            // backupToolStripMenuItem1
            // 
            this.backupToolStripMenuItem1.Image = global::stopwatch.Properties.Resources.Save;
            this.backupToolStripMenuItem1.Name = "backupToolStripMenuItem1";
            this.backupToolStripMenuItem1.Size = new System.Drawing.Size(207, 22);
            this.backupToolStripMenuItem1.Text = "پشتیبان گیری";
            this.backupToolStripMenuItem1.Click += new System.EventHandler(this.backUpToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(204, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::stopwatch.Properties.Resources.Close;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.exitToolStripMenuItem.Text = "خروج";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // timer_notif
            // 
            this.timer_notif.Interval = 20000;
            this.timer_notif.Tick += new System.EventHandler(this.timer_notif_Tick);
            // 
            // timer_network
            // 
            this.timer_network.Enabled = true;
            this.timer_network.Interval = 60000;
            this.timer_network.Tick += new System.EventHandler(this.timer_network_Tick);
            // 
            // timer_backup
            // 
            this.timer_backup.Enabled = true;
            this.timer_backup.Interval = 20000;
            this.timer_backup.Tick += new System.EventHandler(this.timer_backup_Tick);
            // 
            // comboBox_project
            // 
            this.comboBox_project.DropDownWidth = 120;
            this.comboBox_project.FormattingEnabled = true;
            this.comboBox_project.Location = new System.Drawing.Point(-37, 26);
            this.comboBox_project.Name = "comboBox_project";
            this.comboBox_project.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.comboBox_project.Size = new System.Drawing.Size(146, 21);
            this.comboBox_project.TabIndex = 10;
            this.comboBox_project.Visible = false;
            // 
            // timer_notifIcon_delay
            // 
            this.timer_notifIcon_delay.Interval = 5000;
            this.timer_notifIcon_delay.Tick += new System.EventHandler(this.timer_notifIcon_delay_Tick);
            // 
            // timer_net
            // 
            this.timer_net.Enabled = true;
            this.timer_net.Interval = 10000;
            this.timer_net.Tick += new System.EventHandler(this.timer_net_Tick);
            // 
            // timer_update
            // 
            this.timer_update.Enabled = true;
            this.timer_update.Interval = 30000;
            this.timer_update.Tick += new System.EventHandler(this.timer_update_Tick);
            // 
            // textBox_time
            // 
            this.textBox_time.BackColor = System.Drawing.Color.PaleTurquoise;
            this.textBox_time.BorderColor = System.Drawing.Color.PowderBlue;
            this.textBox_time.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_time.ContextMenuStrip = this.cMenuStrip_main;
            this.textBox_time.Enabled = false;
            this.textBox_time.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.textBox_time.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBox_time.Location = new System.Drawing.Point(2, 1);
            this.textBox_time.Name = "textBox_time";
            this.textBox_time.ReadOnly = true;
            this.textBox_time.Size = new System.Drawing.Size(96, 21);
            this.textBox_time.TabIndex = 5;
            this.textBox_time.Text = "00:00:00";
            this.textBox_time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_time.MouseEnter += new System.EventHandler(this.Form1_MouseEnter);
            // 
            // progressBar_day_perform
            // 
            this.progressBar_day_perform.BackColor = System.Drawing.Color.White;
            this.progressBar_day_perform.Cursor = System.Windows.Forms.Cursors.Hand;
            this.progressBar_day_perform.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar_day_perform.Location = new System.Drawing.Point(2, 76);
            this.progressBar_day_perform.Name = "progressBar_day_perform";
            this.progressBar_day_perform.Size = new System.Drawing.Size(107, 4);
            this.progressBar_day_perform.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar_day_perform.TabIndex = 8;
            this.toolTip1.SetToolTip(this.progressBar_day_perform, "کارایی");
            this.progressBar_day_perform.Click += new System.EventHandler(this.progressBar_day_perform_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(111, 80);
            this.ContextMenuStrip = this.cMenuStrip_main;
            this.Controls.Add(this.button_proj_select);
            this.Controls.Add(this.textBox_time);
            this.Controls.Add(this.button_plus);
            this.Controls.Add(this.button_minus);
            this.Controls.Add(this.button_EnterExit);
            this.Controls.Add(this.button_settings);
            this.Controls.Add(this.button_report);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.button_project);
            this.Controls.Add(this.comboBox_project);
            this.Controls.Add(this.progressBar_day_perform);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(127, 119);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(2, 2, 2, 0);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stopwatch";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Click += new System.EventHandler(this.progressBar_day_perform_Click);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            this.MouseEnter += new System.EventHandler(this.Form1_MouseEnter);
            this.cMenuStrip_main.ResumeLayout(false);
            this.cMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_report;
        private System.Windows.Forms.Timer timer1;
        internal System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer2;
        private MyTextBox textBox_time;
        private System.Windows.Forms.Button button_settings;
        internal System.Windows.Forms.Button button_EnterExit;
        internal System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip cMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Timer timer_notif;
        private System.Windows.Forms.ContextMenuStrip cMenuStrip_main;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_startpause;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Button button_plus;
        private System.Windows.Forms.Button button_minus;
        private System.Windows.Forms.ToolStripMenuItem feadbackToolStripMenuItem;
        private System.Windows.Forms.Timer timer_network;
        private System.Windows.Forms.ToolStripMenuItem backUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sadidReportToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addManualToolStripMenuItem;
        private MyProgressBar progressBar_day_perform;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem projectPanelToolStripMenuItem;
        public System.Windows.Forms.Button button_project;
        private System.Windows.Forms.Timer timer_backup;
        private System.Windows.Forms.ToolStripMenuItem performanceToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox_project;
        public System.Windows.Forms.Button button_proj_select;
        private System.Windows.Forms.ToolStripMenuItem manageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem gageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.Timer timer_notifIcon_delay;
        private System.Windows.Forms.ToolStripMenuItem gage2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monitor1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.Timer timer_net;
        private System.Windows.Forms.ToolStripMenuItem monitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forceupdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapnetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendmessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem پشتیبانگیریازفایلهایپروژهToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem پشتیبانگیریازپروژهToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem بروزرسانینسخهجدیدToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ارتباطبهسرورToolStripMenuItem;
        private System.Windows.Forms.Timer timer_update;
        private System.Windows.Forms.ToolStripMenuItem نمایشمکآدرسToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem اوقاتشرعیToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem همگامسازیزمانباسرورToolStripMenuItem;
    }
}

