namespace stopwatch.Forms
{
    partial class Form_MRB_Git
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_MRB_Git));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.checkBox_network = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_skip_pat = new System.Windows.Forms.TextBox();
            this.label_proj_name = new System.Windows.Forms.Label();
            this.textBox_user = new System.Windows.Forms.TextBox();
            this.textBox_project = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button_browse_local = new System.Windows.Forms.Button();
            this.button_browse_server = new System.Windows.Forms.Button();
            this.textBox_server = new System.Windows.Forms.TextBox();
            this.textBox_local = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_view = new System.Windows.Forms.Button();
            this.button_restoreto = new System.Windows.Forms.Button();
            this.dataGridView_backups = new System.Windows.Forms.DataGridView();
            this.Column_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_filescount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_addedfilescount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_size2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoretoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_backup = new System.Windows.Forms.Button();
            this.button_refresh = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundWorker_back = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker_restore = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ObjToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rstToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.baktmpasvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer_close = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_backups)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button_ok);
            this.groupBox1.Controls.Add(this.checkBox_network);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox_skip_pat);
            this.groupBox1.Controls.Add(this.label_proj_name);
            this.groupBox1.Controls.Add(this.textBox_user);
            this.groupBox1.Controls.Add(this.textBox_project);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.button_browse_local);
            this.groupBox1.Controls.Add(this.button_browse_server);
            this.groupBox1.Controls.Add(this.textBox_server);
            this.groupBox1.Controls.Add(this.textBox_local);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(466, 184);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "مشخصات:";
            // 
            // button_ok
            // 
            this.button_ok.Image = global::stopwatch.Properties.Resources.ok;
            this.button_ok.Location = new System.Drawing.Point(9, 14);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(29, 23);
            this.button_ok.TabIndex = 15;
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Visible = false;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // checkBox_network
            // 
            this.checkBox_network.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_network.AutoSize = true;
            this.checkBox_network.Location = new System.Drawing.Point(325, 47);
            this.checkBox_network.Name = "checkBox_network";
            this.checkBox_network.Size = new System.Drawing.Size(53, 17);
            this.checkBox_network.TabIndex = 14;
            this.checkBox_network.Text = "شبکه";
            this.checkBox_network.UseVisualStyleBackColor = true;
            this.checkBox_network.CheckedChanged += new System.EventHandler(this.checkBox_network_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Location = new System.Drawing.Point(384, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 39);
            this.label3.TabIndex = 13;
            this.label3.Text = "این فایلها\r\n پشتییان گیری\r\n نشوند:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_skip_pat
            // 
            this.textBox_skip_pat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_skip_pat.Location = new System.Drawing.Point(9, 120);
            this.textBox_skip_pat.Multiline = true;
            this.textBox_skip_pat.Name = "textBox_skip_pat";
            this.textBox_skip_pat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_skip_pat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_skip_pat.Size = new System.Drawing.Size(370, 56);
            this.textBox_skip_pat.TabIndex = 12;
            this.toolTip1.SetToolTip(this.textBox_skip_pat, "مثال:\r\nD:\\work\\proj1\\dri1\\*\r\nD:\\work\\proj1\\dri1\\test-???.exe\r\n*.rst");
            this.textBox_skip_pat.TextChanged += new System.EventHandler(this.textBox_skip_pat_TextChanged);
            // 
            // label_proj_name
            // 
            this.label_proj_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_proj_name.Location = new System.Drawing.Point(9, 17);
            this.label_proj_name.Name = "label_proj_name";
            this.label_proj_name.Size = new System.Drawing.Size(150, 19);
            this.label_proj_name.TabIndex = 11;
            this.label_proj_name.Text = "نام پروژه:";
            // 
            // textBox_user
            // 
            this.textBox_user.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_user.Location = new System.Drawing.Point(285, 16);
            this.textBox_user.Name = "textBox_user";
            this.textBox_user.ReadOnly = true;
            this.textBox_user.Size = new System.Drawing.Size(94, 21);
            this.textBox_user.TabIndex = 9;
            this.textBox_user.Text = "محمدرضا برهان پناه";
            this.textBox_user.TextChanged += new System.EventHandler(this.textBox_user_TextChanged);
            // 
            // textBox_project
            // 
            this.textBox_project.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_project.Location = new System.Drawing.Point(161, 16);
            this.textBox_project.Name = "textBox_project";
            this.textBox_project.ReadOnly = true;
            this.textBox_project.Size = new System.Drawing.Size(66, 21);
            this.textBox_project.TabIndex = 8;
            this.textBox_project.Text = "00-00-000";
            this.toolTip1.SetToolTip(this.textBox_project, "کد پروژه");
            this.textBox_project.TextChanged += new System.EventHandler(this.textBox_project_TextChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(384, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "نام کاربر:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(229, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "کد پروژه:";
            // 
            // button_browse_local
            // 
            this.button_browse_local.Image = global::stopwatch.Properties.Resources.open;
            this.button_browse_local.Location = new System.Drawing.Point(9, 73);
            this.button_browse_local.Name = "button_browse_local";
            this.button_browse_local.Size = new System.Drawing.Size(29, 21);
            this.button_browse_local.TabIndex = 5;
            this.button_browse_local.UseVisualStyleBackColor = true;
            this.button_browse_local.Click += new System.EventHandler(this.button_browse_local_Click);
            // 
            // button_browse_server
            // 
            this.button_browse_server.Image = global::stopwatch.Properties.Resources.open;
            this.button_browse_server.Location = new System.Drawing.Point(9, 44);
            this.button_browse_server.Name = "button_browse_server";
            this.button_browse_server.Size = new System.Drawing.Size(29, 21);
            this.button_browse_server.TabIndex = 4;
            this.button_browse_server.UseVisualStyleBackColor = true;
            this.button_browse_server.Click += new System.EventHandler(this.button_browse_server_Click);
            // 
            // textBox_server
            // 
            this.textBox_server.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_server.Location = new System.Drawing.Point(42, 45);
            this.textBox_server.Name = "textBox_server";
            this.textBox_server.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_server.Size = new System.Drawing.Size(277, 21);
            this.textBox_server.TabIndex = 3;
            this.textBox_server.Text = "E:\\git-test";
            this.toolTip1.SetToolTip(this.textBox_server, "آدرس محل پشتیبان گیری بدون نام پروژه");
            this.textBox_server.TextChanged += new System.EventHandler(this.textBox_server_TextChanged);
            // 
            // textBox_local
            // 
            this.textBox_local.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_local.Location = new System.Drawing.Point(42, 73);
            this.textBox_local.Multiline = true;
            this.textBox_local.Name = "textBox_local";
            this.textBox_local.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_local.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_local.Size = new System.Drawing.Size(337, 39);
            this.textBox_local.TabIndex = 2;
            this.textBox_local.Text = "D:\\Work\\Phishran Sanat\\ComPad\\Code";
            this.toolTip1.SetToolTip(this.textBox_local, "در صورت عدم نیاز به پشتیبانی عبارت \"-\" را وارد کنید");
            this.textBox_local.TextChanged += new System.EventHandler(this.textBox_local_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Location = new System.Drawing.Point(384, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "آدرس سرور :";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Location = new System.Drawing.Point(384, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "آدرس محلی :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.button_view);
            this.groupBox2.Controls.Add(this.button_restoreto);
            this.groupBox2.Controls.Add(this.dataGridView_backups);
            this.groupBox2.Controls.Add(this.button_backup);
            this.groupBox2.Controls.Add(this.button_refresh);
            this.groupBox2.Location = new System.Drawing.Point(12, 211);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(466, 149);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "فایل های پشتیبانی موجود:";
            // 
            // button_view
            // 
            this.button_view.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_view.Image = global::stopwatch.Properties.Resources.open;
            this.button_view.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_view.Location = new System.Drawing.Point(139, 20);
            this.button_view.Name = "button_view";
            this.button_view.Size = new System.Drawing.Size(114, 24);
            this.button_view.TabIndex = 14;
            this.button_view.Text = "نمایش فایل ها ...";
            this.button_view.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_view.UseVisualStyleBackColor = true;
            this.button_view.Click += new System.EventHandler(this.button_view_Click);
            // 
            // button_restoreto
            // 
            this.button_restoreto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_restoreto.Image = global::stopwatch.Properties.Resources.Save;
            this.button_restoreto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_restoreto.Location = new System.Drawing.Point(257, 20);
            this.button_restoreto.Name = "button_restoreto";
            this.button_restoreto.Size = new System.Drawing.Size(101, 24);
            this.button_restoreto.TabIndex = 13;
            this.button_restoreto.Text = "بازگردانی به ...";
            this.button_restoreto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_restoreto.UseVisualStyleBackColor = true;
            this.button_restoreto.Click += new System.EventHandler(this.button_restoreto_Click);
            // 
            // dataGridView_backups
            // 
            this.dataGridView_backups.AllowUserToAddRows = false;
            this.dataGridView_backups.AllowUserToDeleteRows = false;
            this.dataGridView_backups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_backups.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_backups.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView_backups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_backups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_date,
            this.Column_filescount,
            this.Column_addedfilescount,
            this.Column_size,
            this.Column_size2,
            this.Column_user});
            this.dataGridView_backups.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView_backups.Location = new System.Drawing.Point(9, 48);
            this.dataGridView_backups.MultiSelect = false;
            this.dataGridView_backups.Name = "dataGridView_backups";
            this.dataGridView_backups.ReadOnly = true;
            this.dataGridView_backups.RowHeadersWidth = 20;
            this.dataGridView_backups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_backups.Size = new System.Drawing.Size(448, 90);
            this.dataGridView_backups.TabIndex = 11;
            this.dataGridView_backups.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_backups_CellMouseDoubleClick);
            // 
            // Column_date
            // 
            this.Column_date.FillWeight = 131.9728F;
            this.Column_date.HeaderText = "تاریخ";
            this.Column_date.Name = "Column_date";
            this.Column_date.ReadOnly = true;
            // 
            // Column_filescount
            // 
            this.Column_filescount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column_filescount.FillWeight = 68.02721F;
            this.Column_filescount.HeaderText = "فایل";
            this.Column_filescount.Name = "Column_filescount";
            this.Column_filescount.ReadOnly = true;
            this.Column_filescount.ToolTipText = "تعداد";
            this.Column_filescount.Width = 51;
            // 
            // Column_addedfilescount
            // 
            this.Column_addedfilescount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column_addedfilescount.HeaderText = "جدید";
            this.Column_addedfilescount.Name = "Column_addedfilescount";
            this.Column_addedfilescount.ReadOnly = true;
            this.Column_addedfilescount.ToolTipText = "تعداد فایل های جدید";
            this.Column_addedfilescount.Width = 54;
            // 
            // Column_size
            // 
            this.Column_size.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column_size.HeaderText = "حجم";
            this.Column_size.Name = "Column_size";
            this.Column_size.ReadOnly = true;
            this.Column_size.ToolTipText = "MB";
            this.Column_size.Width = 54;
            // 
            // Column_size2
            // 
            this.Column_size2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column_size2.HeaderText = "افزوده";
            this.Column_size2.Name = "Column_size2";
            this.Column_size2.ReadOnly = true;
            this.Column_size2.ToolTipText = "MB";
            this.Column_size2.Width = 60;
            // 
            // Column_user
            // 
            this.Column_user.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column_user.HeaderText = "فرد";
            this.Column_user.Name = "Column_user";
            this.Column_user.ReadOnly = true;
            this.Column_user.Width = 47;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restoreToolStripMenuItem,
            this.restoretoToolStripMenuItem,
            this.listfilesToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(151, 92);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Image = global::stopwatch.Properties.Resources.Save;
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.restoreToolStripMenuItem.Text = "بازگردانی";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.button_restore_Click);
            // 
            // restoretoToolStripMenuItem
            // 
            this.restoretoToolStripMenuItem.Image = global::stopwatch.Properties.Resources.Save;
            this.restoretoToolStripMenuItem.Name = "restoretoToolStripMenuItem";
            this.restoretoToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.restoretoToolStripMenuItem.Text = "بازگردانی به ...";
            this.restoretoToolStripMenuItem.Click += new System.EventHandler(this.button_restoreto_Click);
            // 
            // listfilesToolStripMenuItem
            // 
            this.listfilesToolStripMenuItem.Image = global::stopwatch.Properties.Resources.list;
            this.listfilesToolStripMenuItem.Name = "listfilesToolStripMenuItem";
            this.listfilesToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.listfilesToolStripMenuItem.Text = "لیست فایل ها";
            this.listfilesToolStripMenuItem.Click += new System.EventHandler(this.listfilesToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::stopwatch.Properties.Resources.delete;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.deleteToolStripMenuItem.Text = "حذف";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // button_backup
            // 
            this.button_backup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_backup.Image = global::stopwatch.Properties.Resources.ServerMode_16x16;
            this.button_backup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_backup.Location = new System.Drawing.Point(362, 20);
            this.button_backup.Name = "button_backup";
            this.button_backup.Size = new System.Drawing.Size(95, 24);
            this.button_backup.TabIndex = 10;
            this.button_backup.Text = "پشتیبان گیری";
            this.button_backup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_backup.UseVisualStyleBackColor = true;
            this.button_backup.Click += new System.EventHandler(this.button_backup_Click);
            // 
            // button_refresh
            // 
            this.button_refresh.FlatAppearance.BorderSize = 0;
            this.button_refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_refresh.Image = global::stopwatch.Properties.Resources.refresh;
            this.button_refresh.Location = new System.Drawing.Point(9, 22);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(20, 20);
            this.button_refresh.TabIndex = 6;
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 371);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(493, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(123, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Visible = false;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Step = 2;
            this.toolStripProgressBar1.Visible = false;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.toolStripStatusLabel2.Image = global::stopwatch.Properties.Resources.Close;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(41, 17);
            this.toolStripStatusLabel2.Text = "لغو";
            this.toolStripStatusLabel2.Visible = false;
            this.toolStripStatusLabel2.Click += new System.EventHandler(this.toolStripStatusLabel2_Click);
            // 
            // backgroundWorker_back
            // 
            this.backgroundWorker_back.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_backup_DoWork);
            this.backgroundWorker_back.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_back_RunWorkerCompleted);
            // 
            // backgroundWorker_restore
            // 
            this.backgroundWorker_restore.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_restore_DoWork);
            this.backgroundWorker_restore.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_restore_RunWorkerCompleted);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(493, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ObjToolStripMenuItem,
            this.rstToolStripMenuItem,
            this.baktmpasvToolStripMenuItem,
            this.largfilesToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.settingsToolStripMenuItem.Text = "تنظیمات";
            // 
            // ObjToolStripMenuItem
            // 
            this.ObjToolStripMenuItem.Checked = true;
            this.ObjToolStripMenuItem.CheckOnClick = true;
            this.ObjToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ObjToolStripMenuItem.Name = "ObjToolStripMenuItem";
            this.ObjToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.ObjToolStripMenuItem.Text = "عدم ذخیره obj";
            // 
            // rstToolStripMenuItem
            // 
            this.rstToolStripMenuItem.Checked = true;
            this.rstToolStripMenuItem.CheckOnClick = true;
            this.rstToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rstToolStripMenuItem.Name = "rstToolStripMenuItem";
            this.rstToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.rstToolStripMenuItem.Text = "عدم ذخیره rst.*";
            // 
            // baktmpasvToolStripMenuItem
            // 
            this.baktmpasvToolStripMenuItem.Checked = true;
            this.baktmpasvToolStripMenuItem.CheckOnClick = true;
            this.baktmpasvToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.baktmpasvToolStripMenuItem.Name = "baktmpasvToolStripMenuItem";
            this.baktmpasvToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.baktmpasvToolStripMenuItem.Text = "عدم ذخیره bak *.tmp *.asv.*";
            // 
            // largfilesToolStripMenuItem
            // 
            this.largfilesToolStripMenuItem.Checked = true;
            this.largfilesToolStripMenuItem.CheckOnClick = true;
            this.largfilesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.largfilesToolStripMenuItem.Name = "largfilesToolStripMenuItem";
            this.largfilesToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.largfilesToolStripMenuItem.Text = "هشدار فایل های بزرگ";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // timer_close
            // 
            this.timer_close.Interval = 1000;
            // 
            // Form_MRB_Git
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 393);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_MRB_Git";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "MRB Git";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_MRB_Git_FormClosed);
            this.Shown += new System.EventHandler(this.Form_MRB_Git_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_backups)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_user;
        private System.Windows.Forms.TextBox textBox_project;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_browse_local;
        private System.Windows.Forms.Button button_browse_server;
        private System.Windows.Forms.TextBox textBox_server;
        private System.Windows.Forms.TextBox textBox_local;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_backup;
        private System.Windows.Forms.Button button_refresh;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView dataGridView_backups;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button_restoreto;
        private System.Windows.Forms.ToolStripMenuItem restoretoToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker_restore;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem listfilesToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Label label_proj_name;
        internal System.Windows.Forms.GroupBox groupBox2;
        internal System.ComponentModel.BackgroundWorker backgroundWorker_back;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        internal System.Windows.Forms.ToolStripMenuItem largfilesToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem rstToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ObjToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_skip_pat;
        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.ToolStripMenuItem baktmpasvToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox_network;
        private System.Windows.Forms.Button button_view;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_user;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_size2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_size;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_addedfilescount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_filescount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_date;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Timer timer_close;
    }
}