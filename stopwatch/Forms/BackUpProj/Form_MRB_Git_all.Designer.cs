namespace stopwatch.Forms
{
    partial class Form_MRB_Git_all
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_MRB_Git_all));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column_sort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_Project = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_dir = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_last_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_backup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_open = new System.Windows.Forms.DataGridViewButtonColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ObjToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rstToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.baktmpasvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.oldprojToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.انتخابToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._30ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._60ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this._90ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.پشتیبانگیریازطرفToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_sort,
            this.column_Project,
            this.Column_dir,
            this.Column_last_time,
            this.Column_backup,
            this.Column_open});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 24;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(584, 328);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            // 
            // Column_sort
            // 
            this.Column_sort.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column_sort.HeaderText = "sort";
            this.Column_sort.Name = "Column_sort";
            this.Column_sort.Width = 51;
            // 
            // column_Project
            // 
            this.column_Project.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.column_Project.HeaderText = "پروژه";
            this.column_Project.Name = "column_Project";
            this.column_Project.ReadOnly = true;
            this.column_Project.Width = 54;
            // 
            // Column_dir
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column_dir.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column_dir.HeaderText = "آدرس محلی";
            this.Column_dir.Name = "Column_dir";
            // 
            // Column_last_time
            // 
            this.Column_last_time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column_last_time.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column_last_time.HeaderText = "آخرین فعالیت";
            this.Column_last_time.Name = "Column_last_time";
            this.Column_last_time.ReadOnly = true;
            this.Column_last_time.Width = 92;
            // 
            // Column_backup
            // 
            this.Column_backup.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column_backup.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column_backup.HeaderText = "پشتیبان";
            this.Column_backup.Name = "Column_backup";
            this.Column_backup.ReadOnly = true;
            this.Column_backup.Width = 69;
            // 
            // Column_open
            // 
            this.Column_open.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.NullValue = "...";
            this.Column_open.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column_open.HeaderText = " ";
            this.Column_open.Name = "Column_open";
            this.Column_open.ReadOnly = true;
            this.Column_open.Width = 30;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.انتخابToolStripMenuItem,
            this.toolStripMenuItem3,
            this.backupToolStripMenuItem,
            this.پشتیبانگیریازطرفToolStripMenuItem,
            this.toolStripMenuItem1,
            this.refreshToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ObjToolStripMenuItem,
            this.rstToolStripMenuItem,
            this.baktmpasvToolStripMenuItem,
            this.largfilesToolStripMenuItem,
            this.toolStripSeparator1,
            this.oldprojToolStripMenuItem});
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(228, 6);
            // 
            // oldprojToolStripMenuItem
            // 
            this.oldprojToolStripMenuItem.CheckOnClick = true;
            this.oldprojToolStripMenuItem.Name = "oldprojToolStripMenuItem";
            this.oldprojToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.oldprojToolStripMenuItem.Text = "نمایش پروژه های قدیمی";
            this.oldprojToolStripMenuItem.Click += new System.EventHandler(this.oldprojToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Enabled = false;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Padding = new System.Windows.Forms.Padding(0);
            this.toolStripMenuItem2.Size = new System.Drawing.Size(16, 20);
            this.toolStripMenuItem2.Text = "|";
            // 
            // انتخابToolStripMenuItem
            // 
            this.انتخابToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this._30ToolStripMenuItem,
            this._60ToolStripMenuItem1,
            this._90ToolStripMenuItem2,
            this.noneToolStripMenuItem});
            this.انتخابToolStripMenuItem.Image = global::stopwatch.Properties.Resources.list;
            this.انتخابToolStripMenuItem.Name = "انتخابToolStripMenuItem";
            this.انتخابToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.انتخابToolStripMenuItem.Text = "انتخاب";
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.allToolStripMenuItem.Text = "همه";
            this.allToolStripMenuItem.Click += new System.EventHandler(this.allToolStripMenuItem_Click);
            // 
            // _30ToolStripMenuItem
            // 
            this._30ToolStripMenuItem.Name = "_30ToolStripMenuItem";
            this._30ToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this._30ToolStripMenuItem.Text = "بدون پشتیبان بیش از 30 روز";
            this._30ToolStripMenuItem.Click += new System.EventHandler(this._30ToolStripMenuItem_Click);
            // 
            // _60ToolStripMenuItem1
            // 
            this._60ToolStripMenuItem1.Name = "_60ToolStripMenuItem1";
            this._60ToolStripMenuItem1.Size = new System.Drawing.Size(214, 22);
            this._60ToolStripMenuItem1.Text = "بدون پشتیبان بیش از 60 روز";
            this._60ToolStripMenuItem1.Click += new System.EventHandler(this._60ToolStripMenuItem1_Click);
            // 
            // _90ToolStripMenuItem2
            // 
            this._90ToolStripMenuItem2.Name = "_90ToolStripMenuItem2";
            this._90ToolStripMenuItem2.Size = new System.Drawing.Size(214, 22);
            this._90ToolStripMenuItem2.Text = "بدون پشتیبان بیش از 90 روز";
            this._90ToolStripMenuItem2.Click += new System.EventHandler(this._90ToolStripMenuItem2_Click);
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.noneToolStripMenuItem.Text = "هیچکدام";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Enabled = false;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Padding = new System.Windows.Forms.Padding(0);
            this.toolStripMenuItem3.Size = new System.Drawing.Size(16, 20);
            this.toolStripMenuItem3.Text = "|";
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9F);
            this.backupToolStripMenuItem.Image = global::stopwatch.Properties.Resources.ServerMode_16x16;
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(185, 20);
            this.backupToolStripMenuItem.Text = "پشتیبان گیری انتخاب شده ها";
            this.backupToolStripMenuItem.Click += new System.EventHandler(this.backupToolStripMenuItem_Click);
            // 
            // پشتیبانگیریازطرفToolStripMenuItem
            // 
            this.پشتیبانگیریازطرفToolStripMenuItem.Name = "پشتیبانگیریازطرفToolStripMenuItem";
            this.پشتیبانگیریازطرفToolStripMenuItem.Size = new System.Drawing.Size(142, 20);
            this.پشتیبانگیریازطرفToolStripMenuItem.Text = "پشتیبان گیری از طرف ...";
            this.پشتیبانگیریازطرفToolStripMenuItem.Click += new System.EventHandler(this.پشتیبانگیریازطرفToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Enabled = false;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStripMenuItem1.Size = new System.Drawing.Size(16, 20);
            this.toolStripMenuItem1.Text = "|";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Image = global::stopwatch.Properties.Resources.refresh;
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(28, 20);
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 352);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(584, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Form_MRB_Git_all
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 374);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_MRB_Git_all";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "پشتیبان گیری";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_MRB_Git_all_FormClosed);
            this.Shown += new System.EventHandler(this.Form_MRB_Git_all_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_Project;
        private System.Windows.Forms.DataGridViewButtonColumn Column_open;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_backup;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_last_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_dir;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_sort;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem انتخابToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _30ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _60ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem _90ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        internal System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ObjToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rstToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem largfilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem baktmpasvToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem oldprojToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem پشتیبانگیریازطرفToolStripMenuItem;
    }
}