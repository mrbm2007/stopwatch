using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace Error_Handler
{
    public partial class ErrorHandler : Component
    {
        public ErrorHandler()
        {
            InitializeComponent();
        }
        private bool _enabled = true;
        private Form _owner;

        //public static ErrorHandler Default = new ErrorHandler();
        public static void handel(Exception ex,string message="")
        {
            new ErrorHandler().Handel(ex, message:message);
        }

        private string _title= "StopWatch";
        private string _filePath = "Errors.Log";
        private bool _internalCheck = false;
        private bool _saveToFile = false;

        Tools.Form_error frm_err = new Tools.Form_error();

        [Category("Message"), DisplayName("Title")]
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }

        [DisplayName("Owner Form")]
        public Form owner
        {
            set { 
                _owner = value;
            }
            get { return _owner; }
        }

        [Category("E-Mail"), DisplayName("SMTP Host")]
        public string smtpHost
        {
            set { frm_err. SMTP_server = value; }
            get { return frm_err.SMTP_server; }
        }
        [Category("E-Mail"), DisplayName("SMTP Port")]
        public int smtpPort
        {
            set { frm_err.SMTP_port = value; }
            get { return frm_err.SMTP_port; }
        }
        [Category("E-Mail"), DisplayName("From")]
        public string smtpMailFrom
        {
            set { frm_err.SMTP_user = value; }
            get { return frm_err.SMTP_user; }
        }
        [Category("E-Mail"), DisplayName("From Password")]
        public string smtpMailFromPass
        {
            set { frm_err.SMTP_pass = value; }
            get { return frm_err.SMTP_pass; }
        }
        [Category("E-Mail"), DisplayName("To")]
        public string smtpMailTo
        {
            set { frm_err.SMTP_to = value; }
            get { return frm_err.SMTP_to; }
        }
        [Category("E-Mail")]
        public string ProjectName
        {
            set { frm_err.ProjectName = value; }
            get { return frm_err.ProjectName; }
        }

        [Category("File"), DisplayName("Save to file")]
        public bool saveToFile
        {
            set { _saveToFile = value; }
            get { return _saveToFile; }
        }
        [Category("File"), DisplayName("File Path")]
        public string filePath
        {
            set { _filePath = value; }
            get { return _filePath; }
        }

        [DisplayName("Internal Check for error")]
        public bool internalCheck
        {
            set { _internalCheck = value; }
            get { return _internalCheck; }
        }
        [DisplayName("Enabled")]
        public bool enabled
        {
            set { _enabled = value; }
            get { return _enabled; }
        }

        public void CleanLogFile()
        {
            try
            {
                var oFile = new StreamWriter(filePath, true);
                oFile.Close();
            }
            catch (Exception _ex)
            {
                if (internalCheck)
                    (new ErrorHandler() { title = "Internal Error" }).Handel(_ex);
            }
        }

        public static List <string> lookedFiles = new List<string>();
        public void Handel(Exception exp, string Additional = "", string title = null,string message ="")
        {
            if (!enabled)
                return;
            if (title == null)
                title = _title;
            if(saveToFile)
            try
            {
                while (lookedFiles.Contains(filePath))
                    Application.DoEvents();
                lookedFiles.Add(filePath);
                using (var file = new StreamWriter(filePath, true))
                {
                    file.WriteLine("-----------------------------------------");
                    file.WriteLine(title);
                    file.WriteLine(DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString());
                    file.WriteLine(message==""? exp.Message:message);
                    file.WriteLine("--");
                    file.WriteLine(exp.StackTrace);
                    file.WriteLine("-----------------------------------------");
                    file.Close();
                }
                lookedFiles.Remove(filePath);
            }
            catch (Exception _ex)
            {
                if (internalCheck)
                    (new ErrorHandler() { title = "Internal Error" }).Handel(_ex);
                lookedFiles.Remove(filePath);
            }
            if (frm_err.Visible)
            {
                frm_err.Text += "!";
                return;
            }
            frm_err.expception = exp;
            frm_err.message = message;
            frm_err.Text = title;
            frm_err.Additional = Additional;
            frm_err.ShowDialog();
        }

    }
}
