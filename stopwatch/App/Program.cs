using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Drawing.Text;
using System.Drawing;

namespace stopwatch
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Properties.Settings.Default.NeedUpgrade)
            {
                try
                {
                    Properties.Settings.Default.Upgrade();
                    Properties.Settings.Default.NeedUpgrade = false;
                    Properties.Settings.Default.Save();
                }
                catch { }
            }
            AppDomain.CurrentDomain.AssemblyResolve += delegate (object sender, ResolveEventArgs e)
            {
                AssemblyName requestedName = new AssemblyName(e.Name);
                return null;
            };
            try
            {
                Registry.CurrentUser.OpenSubKey("Control Panel\\International", true).SetValue("sDecimal", ".");
            }
            catch { }
            try
            {
                Process.Start(new ProcessStartInfo("sc.exe", "config wuauserv start= disabled") { WindowStyle = ProcessWindowStyle.Hidden });
            }
            catch { }
#if FATER
            company = Company.Fater;
#endif
            try
            {
                foreach (InputLanguage L in InputLanguage.InstalledInputLanguages)
                    if (L.Culture.EnglishName.ToLower().Contains("persian"))
                        InputLanguage.CurrentInputLanguage = L;
            }
            catch { }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            #region Command Line Args
            var coms = Environment.GetCommandLineArgs();
            if (coms[coms.Length - 1] == "/_admin_")
            {
                if (coms[1].ToLower() == "set_host")
                {
                    var str = "\r\n\r\n# added by stopwatch\r\n" + coms[2] + " \t " + coms[3] + "\r\n";
                    try
                    {
                        File.AppendAllText(@"C:\Windows\System32\drivers\etc\hosts", str);
                    }
                    catch { }
                    try
                    {
                        File.AppendAllText(@"C:\Windows\System32\drivers\etc\hosts.ics", str);
                    }
                    catch { }
                }
                return;
            }
            if (coms.Length > 2)
            {
                if (coms[1].ToLower() == "view")
                {
                    View(coms[2]);
                    return;
                }
                if (coms[1].ToLower() == "convert_all")
                {
                    ConvertToStr(coms[2]);
                    Form_msg.Show(null, "فایل مورد نظر با فرمت متنی ذخیره شد.");
                    return;
                }
                if (coms[1].ToLower() == "convert")
                {
                    ConvertToStr(coms[2], false);
                    Form_msg.Show(null, "فایل مورد نظر با فرمت متنی ذخیره شد.");
                    return;
                }
                if (coms[1].ToLower() == "convert_prev")
                {
                    ConvertToStr(coms[2], false, true);
                    Form_msg.Show(null, "فایل مورد نظر با فرمت متنی ذخیره شد.");
                    return;
                }
                if (coms[1].ToLower() == "/sendto")
                {
                    try
                    {
                        MyData.db.Load(Form1.DataFile);
                        var frm = new Form_chat(null, true);
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.ShowDialog();
                        if (frm.selected_user + "" != "")
                        {
                            for (int i = 2; i < coms.Length; i++)
                            {
                                if (Directory.Exists(coms[i]))
                                {
                                    var file_ = Path.GetTempPath() + "\\" + Path.GetFileName(coms[i]) + ".zip";
                                    Zip.CreateZip(coms[i], file_);
                                    Thread.Sleep(200);
                                    frm.send_file(frm.selected_user, file_, true);
                                    Thread.Sleep(200);
                                    try
                                    {
                                        File.Delete(file_);
                                    }
                                    catch { }
                                }
                                else if (File.Exists(coms[i]))
                                    frm.send_file(frm.selected_user, coms[i]);
                            }
                            new Form_chat(frm.selected_user).ShowDialog();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\r\n" + WebServer.Default.server, "Error in send to:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }
            }
            #endregion

            if (!Utils.Debug)
            {
                AppDomain.CurrentDomain.UnhandledException += Program.ErrorHandeler;
                Application.ThreadException += Program.ErrorHandeler;
                try
                {
                    var p1 = Process.GetCurrentProcess();
                    var P = Process.GetProcessesByName(p1.ProcessName);
                    foreach (var p in P) try
                        {
                            if (p.Id != p1.Id)
                                if (Path.GetFileName(p.Modules[0].FileName).ToLower() ==
                                    Path.GetFileName(p1.Modules[0].FileName).ToLower())
                                {
                                    /*if (p.Modules[0].FileName.ToLower() != p1.Modules[0].FileName.ToLower())
                                        p.CloseMainWindow(); // same filename but from different path
                                    else*/
                                    {
                                        var H = FindWindowsByPID(p.Id);
                                        var ver = Utils.Version2Int(Assembly.GetExecutingAssembly().GetName().Version.ToString());
                                        foreach (var h in H)
                                            if (SendMessage(h, 100, ver, 100) != 0)
                                            {
                                                Thread.Sleep(500);
                                                break;
                                            }
                                        if (!p.HasExited) Thread.Sleep(1500);
                                        if (!p.HasExited) Thread.Sleep(1500);
                                        if (!p.HasExited) Thread.Sleep(1500);
                                        if (!p.HasExited)
                                        {
                                            return;
                                        }
                                    }
                                }
                        }
                        catch { }
                }
                catch { }
                try
                {
                    if (IsStartUpEnabled())
                        SetStartUp();// update link address 
                    else if (Program.AppSetting.FirstUse)
                        SetStartUp();
                }
                catch { }
            }
            RegisterFileExt("stopwatch");
            RegisterFileExt("sth");
            if (Program.AppSetting.PerformMax == 0)
                Program.AppSetting.PerformMax = 8 * 3600;
            if (Program.AppSetting.PerformMax2 == 0)
                Program.AppSetting.PerformMax2 = 6 * 3600;
            if (Program.AppSetting.PerformMax3 == 0)
                Program.AppSetting.PerformMax3 = 4 * 3600;
            /*if (Utils.Debug)
            {
                Application.Run(new Form_manage());
                return;
            }*/
            Application.Run(new Form1());
            //tictoc.Alert();
        }

        public static PrivateFontCollection pf;
        public static void ApplyFont(Control c)
        {
            try
            {
                if (pf == null)
                {
                    pf = new PrivateFontCollection();
                    pf.AddFontFile("BNAZANB.ttf");
                }
                c.Font = new Font(Program.pf.Families[0], c.Font.Size, c.Font.Style, c.Font.Unit);
            }
            catch { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ext">without dot</param>
        public static void RegisterFileExt(string ext)
        {
            try
            {
                var key = Registry.CurrentUser.OpenSubKey("Software\\Classes", true);
                key = key.CreateSubKey("." + ext);
                {
                    var key2 = key.CreateSubKey("DefaultIcon");
                    key2.SetValue("", Application.ExecutablePath + ",0");
                }
                key = key.CreateSubKey("shell");
                {
                    var key2 = key.CreateSubKey("  View in stopwatch");
                    key2 = key2.CreateSubKey("command");
                    key2.SetValue("", "\"" + Application.ExecutablePath + "\"" + " view \"%1\"");
                }
                {
                    var key2 = key.CreateSubKey(" Convert To String Format");
                    key2 = key2.CreateSubKey("command");
                    key2.SetValue("", "\"" + Application.ExecutablePath + "\"" + " convert_all \"%1\"");
                }
                {
                    var key2 = key.CreateSubKey("Convert current month");
                    key2 = key2.CreateSubKey("command");
                    key2.SetValue("", "\"" + Application.ExecutablePath + "\"" + " convert \"%1\"");
                }
                {
                    var key2 = key.CreateSubKey("Convert previous month");
                    key2 = key2.CreateSubKey("command");
                    key2.SetValue("", "\"" + Application.ExecutablePath + "\"" + " convert_prev \"%1\"");
                }
            }
            catch { }
        }
        public static Properties.Settings AppSetting { get { return Properties.Settings.Default; } }
        public static void SetStartUp()
        {
            if (Utils.Debug) return;
            try
            {
                var RegStartUp = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                var link = Application.ExecutablePath;
                RegStartUp.SetValue("stopwatch", link);
            }
            catch
            {
                var RegStartUp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                var link = Application.ExecutablePath;
                RegStartUp.SetValue("stopwatch", link);
            }
        }
        public static void RemoveStartUp()
        {
            if (Utils.Debug) return;
            try
            {
                var RegStartUp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                RegStartUp.DeleteValue("stopwatch");
            }
            catch { }
        }
        public static bool IsStartUpEnabled()
        {
            var RegStartUp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            return RegStartUp.GetValue("stopwatch") != null;
        }
        public static void View(string filename)
        {
            WebServer.Offline = true;
            MyData.ReadOnlyMode = true;
            Application.Run(new Form_report(given_db: new MyData().Load(filename)));
        }

        public static void ConvertToStr(string fileName, bool all = true, bool previous = false)
        {
            MyData db = new MyData();
            db.Load(fileName);
            var date1 = DateTime.Now.AddYears(-1000);
            if (!all)
            {
                date1 = Utils.PersianParse(Utils.PersianYear(DateTime.Now) + "/" + (Utils.PersianMonth(DateTime.Now) + (previous ? -1 : 0)) + "/1");
            }
            var str = Utils.DateString(DateTime.Now) + " - " + Utils.TimeString(DateTime.Now) + "\r\nformat:\r\n    >>> PROJECT NAME\r\n  Date   Duration >> ( start1 - duration1, start2 - duration2,  start3 - duration3, ...)\r\n\r\n";
            var day = DateTime.Now.Date;
            foreach (var p in db.Projects)
                if (p.Times.Count > 0)
                {
                    str += "    >>> " + p.Name + " [" + p.code + "]:\r\n";
                    var times_of_day = new List<Time_>();
                    foreach (var t in p.Times)
                        if (t.Start >= date1)
                        {
                            if (t.Start.Date != day)
                            {
                                if (times_of_day.Count > 0)
                                {
                                    var sum = 0;
                                    foreach (var t_d in times_of_day)
                                        sum += t_d.Duration.sec;
                                    if (sum > 10)
                                    {
                                        str += Utils.DateString(day).PadLeft(10, ' ') + "  " + new TSpan(sum) + "   >> (  ";
                                        foreach (var t_d in times_of_day)
                                            str += Utils.TimeString(t_d.Start) + " - " + t_d.Duration + ", ";
                                        str += "  )\r\n";
                                    }
                                }
                                times_of_day.Clear();
                                day = t.Start.Date;
                            }
                            times_of_day.Add(t);
                        }
                    str += "\r\n";
                }
            File.WriteAllText(fileName + ".txt", str);
        }

        #region win32 API
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, long wParam, long lParam);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [DllImport("user32.dll")]
        public static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        public static List<IntPtr> FindWindowsByPID(int pid)
        {
            var found = new List<IntPtr>();
            EnumWindows(delegate (IntPtr wnd, IntPtr param)
            {
                uint pid_ = 0;
                GetWindowThreadProcessId(wnd, out pid_);
                if (pid_ == pid)
                    found.Add(wnd);
                return true;
            }, IntPtr.Zero);
            return found;
        }
        #endregion

        public static Company company = Company.Sadid;
        public enum Company
        {
            Sadid,
            Fater
        }

        internal static void ErrorHandeler(object sender, UnhandledExceptionEventArgs e)
        {
            Error_Handler.ErrorHandler.handel((Exception)e.ExceptionObject, "[UnhandledException] >> " + ((Exception)e.ExceptionObject).Message);
            if (e.IsTerminating)
                Application.Exit();
        }
        internal static void ErrorHandeler(object sender, ThreadExceptionEventArgs e)
        {
            Error_Handler.ErrorHandler.handel(e.Exception, "[ThreadException] >> " + e.Exception.Message);
        }

        internal static string PASS = "-sw-";



        internal static bool IsRunAsAdmin()
        {
            var id = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(id);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        internal static bool StartAsAdmin(string command_lines)
        {
            // Launch itself as administrator
            var proc = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = Application.ExecutablePath,
                Arguments = command_lines + " /_admin_",
                Verb = "runas",
            };
            try
            {
                Process.Start(proc);
                return true;
            }
            catch { }
            return false;
        }

    }

}
