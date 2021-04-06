namespace Tools
{
    partial class Form_error
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_error));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label_err = new System.Windows.Forms.Label();
            this.border1 = new System.Windows.Forms.Panel();
            this.label_detail = new System.Windows.Forms.Label();
            this.button_continue = new System.Windows.Forms.Button();
            this.button_sendReport = new System.Windows.Forms.Button();
            this.button_detail = new System.Windows.Forms.Button();
            this.bWorker_mail = new System.ComponentModel.BackgroundWorker();
            this.progressBar_mail = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(36, 29);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label_err
            // 
            this.label_err.AutoSize = true;
            this.label_err.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label_err.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.label_err.Location = new System.Drawing.Point(57, 8);
            this.label_err.MaximumSize = new System.Drawing.Size(360, 60);
            this.label_err.Name = "label_err";
            this.label_err.Size = new System.Drawing.Size(122, 15);
            this.label_err.TabIndex = 12;
            this.label_err.Text = "Some thing is wrong! ";
            // 
            // border1
            // 
            this.border1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.border1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.border1.Location = new System.Drawing.Point(9, 94);
            this.border1.Name = "border1";
            this.border1.Size = new System.Drawing.Size(417, 1);
            this.border1.TabIndex = 11;
            this.border1.Text = "kryptonBorderEdge1";
            // 
            // label_detail
            // 
            this.label_detail.AutoSize = true;
            this.label_detail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label_detail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.label_detail.Location = new System.Drawing.Point(0, 0);
            this.label_detail.MaximumSize = new System.Drawing.Size(400, 1000);
            this.label_detail.Name = "label_detail";
            this.label_detail.Size = new System.Drawing.Size(54, 15);
            this.label_detail.TabIndex = 10;
            this.label_detail.Text = "Aaaa aaa";
            // 
            // button_continue
            // 
            this.button_continue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_continue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_continue.Location = new System.Drawing.Point(364, 66);
            this.button_continue.Name = "button_continue";
            this.button_continue.Size = new System.Drawing.Size(66, 24);
            this.button_continue.TabIndex = 6;
            this.button_continue.Text = "Continue";
            this.button_continue.UseVisualStyleBackColor = true;
            this.button_continue.Click += new System.EventHandler(this.kButton_continue_Click);
            // 
            // button_sendReport
            // 
            this.button_sendReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_sendReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_sendReport.Location = new System.Drawing.Point(281, 66);
            this.button_sendReport.Name = "button_sendReport";
            this.button_sendReport.Size = new System.Drawing.Size(80, 24);
            this.button_sendReport.TabIndex = 7;
            this.button_sendReport.Text = "Send Report";
            this.button_sendReport.UseVisualStyleBackColor = true;
            this.button_sendReport.Click += new System.EventHandler(this.kButton_sendReport_Click);
            // 
            // button_detail
            // 
            this.button_detail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_detail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_detail.Location = new System.Drawing.Point(230, 66);
            this.button_detail.Name = "button_detail";
            this.button_detail.Size = new System.Drawing.Size(48, 24);
            this.button_detail.TabIndex = 8;
            this.button_detail.Text = "Details";
            this.button_detail.UseVisualStyleBackColor = true;
            this.button_detail.Click += new System.EventHandler(this.kCheckButton_detail_Click);
            // 
            // bWorker_mail
            // 
            this.bWorker_mail.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bWorker_mail_DoWork);
            this.bWorker_mail.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bWorker_mail_RunWorkerCompleted);
            // 
            // progressBar_mail
            // 
            this.progressBar_mail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_mail.Location = new System.Drawing.Point(282, 85);
            this.progressBar_mail.Name = "progressBar_mail";
            this.progressBar_mail.Size = new System.Drawing.Size(78, 4);
            this.progressBar_mail.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar_mail.TabIndex = 9;
            this.progressBar_mail.Value = 5;
            this.progressBar_mail.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.label_detail);
            this.panel1.Location = new System.Drawing.Point(9, 101);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(417, 93);
            this.panel1.TabIndex = 14;
            // 
            // Form_error
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 203);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.progressBar_mail);
            this.Controls.Add(this.button_detail);
            this.Controls.Add(this.button_sendReport);
            this.Controls.Add(this.button_continue);
            this.Controls.Add(this.border1);
            this.Controls.Add(this.label_err);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "Form_error";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Error";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Form_error_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button  button_detail;
        private System.Windows.Forms.Button button_sendReport;
        private System.Windows.Forms.Button button_continue;
        private System.Windows.Forms.Label label_detail;
        private System.Windows.Forms.Panel border1;
        private System.Windows.Forms.Label label_err;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.ComponentModel.BackgroundWorker bWorker_mail;
        private System.Windows.Forms.ProgressBar progressBar_mail;
        private System.Windows.Forms.Panel panel1;
    }
}

