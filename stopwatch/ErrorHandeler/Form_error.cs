using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace Tools
{
    public partial class Form_error : Form
    {
        public Form_error(string Title = "")
        {
            InitializeComponent();
        }
        int h = 93;

        public Exception expception
        {
            set
            {
                if (value != null)
                {
                    Error = value.Message;
                    Detail = value.StackTrace;
                }
            }
        }

        private void kCheckButton_detail_Click(object sender, EventArgs e)
        {
            if (button_detail.BackColor == SystemColors.Control)
            {
                button_detail.BackColor = SystemColors.ControlDark;
                ClientSize = new Size(ClientSize.Width, h + label_detail.PreferredHeight + 25);
            }
            else
            {
                button_detail.BackColor = SystemColors.Control;
                ClientSize = new Size(ClientSize.Width, h);
            }
        }

        private void kButton_continue_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string HTMLformat(string str)
        {
            var res = str;
            res = res.Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\r\n", "<br/>")
                .Replace("\n", "<br/>")
                .Replace("<br/>", "\r\n<br/>");
            return "<span>" + res + "</span>";
        }
        private void kButton_sendReport_Click(object sender, EventArgs e)
        {
            try
            {
                button_sendReport.Enabled = false;
                progressBar_mail.Visible = true;
                bWorker_mail.RunWorkerAsync(
                    "<b style=\"color:red\"> Error:</b><br>"
                    + HTMLformat(Error)
                    + "<br><hr><b style=\"color:blue\"> Detail:</b><br>"
                    + HTMLformat(Detail)
                    + "<br><hr><b style=\"color:green\"> Additional data:</b><br>"
                    + HTMLformat(Additional));
            }
            catch (Exception)
            {
                button_sendReport.BackColor = Color.OrangeRed;
                button_sendReport.Enabled = true;
                progressBar_mail.Visible = false;
            }
        }

        internal string SMTP_server = "smtp.gmail.com";
        internal int SMTP_port = 587;
        internal string SMTP_user = "mrbm.projects@gmail.com";
        internal string SMTP_pass = "zxcvbnmmnbvcxz1";
        internal string SMTP_to = "mrbm.2007@gmail.com";

        internal string Error
        {
            get { return label_err.Text; }
            set { label_err.Text = value; }
        }
        internal string Detail
        {
            get { return label_detail.Text; }
            set { label_detail.Text = value; }
        }
        internal string Additional = "";
        internal string ProjectName = "StopWatch";

        internal static string User
        {
            get { return (stopwatch.MyData.db.user.name == "" ? Environment.UserName : stopwatch.MyData.db.user.name); }
        }
        private void bWorker_mail_DoWork(object sender, DoWorkEventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            var body = (String)e.Argument;
            try
            {
                var fromAddress = new MailAddress(SMTP_user, "ErrorHandler");
                var toAddress = new MailAddress(SMTP_to);
                var subject = Text + ", " + User + "@" + Environment.MachineName + "{" + ProjectName + "}" + " [ErrorHanler] ";
                var smtp = new SmtpClient
                {
                    Host = SMTP_server,
                    Port = SMTP_port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, SMTP_pass)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = (body
                            + "<br><center><b style=\"color:blue\"><hr>"
                            + Assembly.GetExecutingAssembly().GetName().ToString()
                            + "<br><hr>Sent By ErrorHandler from "
                            + User + "@" + Environment.MachineName
                            + "</b></center>")
                })
                {
                    for (int i = 4; i < 10; i++)
                        try
                        {
                            smtp.Send(message);
                            break;
                        }
                        catch
                        {
                            smtp.Credentials = new NetworkCredential(fromAddress.Address, "zxcvbnmmnbvcxz" + i);
                        }
                    button_sendReport.BackColor = Color.LawnGreen;
                }
            }
            catch (Exception)
            {
                button_sendReport.BackColor = Color.OrangeRed;
                button_sendReport.Enabled = true;
                progressBar_mail.Visible = false;
            }
        }

        private void bWorker_mail_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar_mail.Visible = false;
        }

        private void Form_error_Shown(object sender, EventArgs e)
        {
            button_sendReport.Enabled = true;
            button_detail.BackColor =
            button_sendReport.BackColor = SystemColors.Control;
            progressBar_mail.Visible = false;

            ClientSize = new Size(ClientSize.Width, h);
        }

        public string message { get { return Error; } set { if (value != "") Error = value; } }
    }
}