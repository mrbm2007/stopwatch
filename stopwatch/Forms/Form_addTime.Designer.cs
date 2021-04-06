namespace stopwatch
{
    partial class Form_addTime
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_addTime));
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column_proj = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column_day_in_week = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_end = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_dur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.دیروزToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.امروزToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.فرداToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.روزبعدToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.روزقبلToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column8 = new stopwatch.DataGridViewProgressColumn();
            this.dataGridViewProgressColumn1 = new stopwatch.DataGridViewProgressColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // button_ok
            // 
            this.button_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ok.Image = global::stopwatch.Properties.Resources.ok;
            this.button_ok.Location = new System.Drawing.Point(635, 7);
            this.button_ok.Margin = new System.Windows.Forms.Padding(0);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(22, 21);
            this.button_ok.TabIndex = 10;
            this.toolTip1.SetToolTip(this.button_ok, "Ok");
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cancel.Image = global::stopwatch.Properties.Resources.Close;
            this.button_cancel.Location = new System.Drawing.Point(635, 28);
            this.button_cancel.Margin = new System.Windows.Forms.Padding(0);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(22, 21);
            this.button_cancel.TabIndex = 11;
            this.toolTip1.SetToolTip(this.button_cancel, "Cancel");
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_proj,
            this.Column_day_in_week,
            this.Column_date,
            this.Column_start,
            this.Column_end,
            this.Column_dur,
            this.Column_comment});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(7, 7);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 23;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.Size = new System.Drawing.Size(625, 43);
            this.dataGridView1.TabIndex = 12;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // Column_proj
            // 
            this.Column_proj.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column_proj.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column_proj.DropDownWidth = 120;
            this.Column_proj.FillWeight = 55F;
            this.Column_proj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Column_proj.HeaderText = "پروژه";
            this.Column_proj.Name = "Column_proj";
            this.Column_proj.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column_proj.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column_day_in_week
            // 
            this.Column_day_in_week.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column_day_in_week.FillWeight = 280.8511F;
            this.Column_day_in_week.HeaderText = "روز";
            this.Column_day_in_week.Name = "Column_day_in_week";
            this.Column_day_in_week.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column_day_in_week.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column_day_in_week.Width = 75;
            // 
            // Column_date
            // 
            this.Column_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column_date.FillWeight = 63.82978F;
            this.Column_date.HeaderText = "تاریخ";
            this.Column_date.Name = "Column_date";
            this.Column_date.Width = 65;
            // 
            // Column_start
            // 
            this.Column_start.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column_start.FillWeight = 63.82978F;
            this.Column_start.HeaderText = "شروع";
            this.Column_start.Name = "Column_start";
            this.Column_start.Width = 61;
            // 
            // Column_end
            // 
            this.Column_end.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column_end.FillWeight = 63.82978F;
            this.Column_end.HeaderText = "پایان";
            this.Column_end.Name = "Column_end";
            this.Column_end.Width = 54;
            // 
            // Column_dur
            // 
            this.Column_dur.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column_dur.FillWeight = 63.82978F;
            this.Column_dur.HeaderText = "مدت";
            this.Column_dur.Name = "Column_dur";
            this.Column_dur.Width = 55;
            // 
            // Column_comment
            // 
            this.Column_comment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Column_comment.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column_comment.HeaderText = "توضیحات";
            this.Column_comment.Name = "Column_comment";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.دیروزToolStripMenuItem,
            this.امروزToolStripMenuItem,
            this.فرداToolStripMenuItem,
            this.toolStripSeparator1,
            this.روزبعدToolStripMenuItem,
            this.روزقبلToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(111, 120);
            // 
            // دیروزToolStripMenuItem
            // 
            this.دیروزToolStripMenuItem.Image = global::stopwatch.Properties.Resources.arrow_top;
            this.دیروزToolStripMenuItem.Name = "دیروزToolStripMenuItem";
            this.دیروزToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.دیروزToolStripMenuItem.Text = "دیروز";
            this.دیروزToolStripMenuItem.Click += new System.EventHandler(this.دیروزToolStripMenuItem_Click);
            // 
            // امروزToolStripMenuItem
            // 
            this.امروزToolStripMenuItem.Image = global::stopwatch.Properties.Resources.CheckUp;
            this.امروزToolStripMenuItem.Name = "امروزToolStripMenuItem";
            this.امروزToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.امروزToolStripMenuItem.Text = "امروز";
            this.امروزToolStripMenuItem.Click += new System.EventHandler(this.امروزToolStripMenuItem_Click);
            // 
            // فرداToolStripMenuItem
            // 
            this.فرداToolStripMenuItem.Image = global::stopwatch.Properties.Resources.arrow_down;
            this.فرداToolStripMenuItem.Name = "فرداToolStripMenuItem";
            this.فرداToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.فرداToolStripMenuItem.Text = "فردا";
            this.فرداToolStripMenuItem.Click += new System.EventHandler(this.فرداToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(107, 6);
            // 
            // روزبعدToolStripMenuItem
            // 
            this.روزبعدToolStripMenuItem.Name = "روزبعدToolStripMenuItem";
            this.روزبعدToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.روزبعدToolStripMenuItem.Text = "روز بعد";
            this.روزبعدToolStripMenuItem.Click += new System.EventHandler(this.روزبعدToolStripMenuItem_Click);
            // 
            // روزقبلToolStripMenuItem
            // 
            this.روزقبلToolStripMenuItem.Name = "روزقبلToolStripMenuItem";
            this.روزقبلToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.روزقبلToolStripMenuItem.Text = "روز قبل";
            this.روزقبلToolStripMenuItem.Click += new System.EventHandler(this.روزقبلToolStripMenuItem_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 7.75F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8});
            this.dataGridView2.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView2.EnableHeadersVisualStyles = false;
            this.dataGridView2.Location = new System.Drawing.Point(7, 56);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidth = 23;
            this.dataGridView2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView2.Size = new System.Drawing.Size(625, 38);
            this.dataGridView2.TabIndex = 13;
            this.dataGridView2.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView2_CellMouseClick);
            // 
            // Column8
            // 
            this.Column8.footerColor = System.Drawing.Color.Black;
            this.Column8.headerColor = System.Drawing.Color.DarkOrange;
            this.Column8.HeaderText = "ساعت کاری ثبت شده - 0:00 تا 24:00  >>>";
            this.Column8.major = 4;
            this.Column8.majorColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Column8.minor = 6;
            this.Column8.minorColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Column8.Name = "Column8";
            this.Column8.ProgressBarColor = System.Drawing.Color.Green;
            this.Column8.ReadOnly = true;
            // 
            // dataGridViewProgressColumn1
            // 
            this.dataGridViewProgressColumn1.footerColor = System.Drawing.Color.Black;
            this.dataGridViewProgressColumn1.headerColor = System.Drawing.Color.DarkOrange;
            this.dataGridViewProgressColumn1.HeaderText = "ساعت کاری ثبت شده - 0:00 تا 24:00";
            this.dataGridViewProgressColumn1.major = 4;
            this.dataGridViewProgressColumn1.majorColor = System.Drawing.Color.LightBlue;
            this.dataGridViewProgressColumn1.minor = 6;
            this.dataGridViewProgressColumn1.minorColor = System.Drawing.Color.Gray;
            this.dataGridViewProgressColumn1.Name = "dataGridViewProgressColumn1";
            this.dataGridViewProgressColumn1.ProgressBarColor = System.Drawing.Color.Green;
            this.dataGridViewProgressColumn1.ReadOnly = true;
            this.dataGridViewProgressColumn1.Width = 622;
            // 
            // Form_addTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 101);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_addTime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "افزودن ساعت کاری جدید";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Form_addTime_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        internal System.Windows.Forms.DataGridView dataGridView1;
        internal System.Windows.Forms.DataGridView dataGridView2;
        private DataGridViewProgressColumn dataGridViewProgressColumn1;
        private DataGridViewProgressColumn Column8;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem دیروزToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem امروزToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem فرداToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem روزبعدToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem روزقبلToolStripMenuItem;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column_proj;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column_day_in_week;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_start;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_end;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_dur;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_comment;
    }
}