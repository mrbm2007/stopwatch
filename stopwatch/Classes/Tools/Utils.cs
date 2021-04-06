using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Management;
using System.Security.AccessControl;
using System.Security.Cryptography;

namespace stopwatch
{
    public class Utils
    {
        internal static void Log()
        {
            try
            {
                File.AppendAllText(dir + "log.txt", "\r\n");
            }
            catch { }
        }
        static string last_log_txt = "";
        internal static void Log(string txt)
        {
            try
            {
                if (new FileInfo(dir + "log.txt").Length > 0.1 * 1024 * 1024)
                {
                    var str = File.ReadAllText(dir + "log.txt");
                    File.WriteAllText(dir + "log-" + Utils.DateTimeString().Replace("/", "-").Replace(":", "-") + ".txt", str.Substring(0, str.Length - 5000));
                    File.WriteAllText(dir + "log.txt", str.Substring(str.Length - 5200));
                }
                if (last_log_txt == txt)
                    File.AppendAllText(dir + "log.txt",
                        String.Format("\t\t   {0}", DateTime.Now.ToShortTimeString())
                        + "\r\n");
                else
                    File.AppendAllText(dir + "log.txt",
                        String.Format("{0} - {1} : {2}", DateString(), DateTime.Now.ToShortTimeString(), last_log_txt == txt ? "" : txt)
                        + "\r\n");
                last_log_txt = txt;
            }
            catch { }
        }

        static string dir_ = "";
        public static bool Debug
        {
            get
            {
                if (Application.ExecutablePath.ToLower().Contains("\\bin\\release\\"))
                    return true;
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }
        public static string dir
        {
            get
            {
                if (dir_ == "")
                {
                    dir_ = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\stopwatch\\";
                    if (Debug)
                        dir_ += "debug\\";
                    if (!Directory.Exists(dir_))
                        Directory.CreateDirectory(dir_);

                }
                return dir_;
            }
        }
        internal static void SaveProjTime(string proj, TimeSpan dt, bool backup = false, string msg = "")
        {
            if (dt.Ticks == 0)
                return;
            if (backup)
            {
                proj += " - " + DateString().Replace("/", "-") + " [";
                for (int i = 1; i < 100; i++)
                    if (!File.Exists(proj + String.Format("{0:00}", i + "].txt")))
                    {
                        proj += String.Format("{0:00}", i + "]");
                        break;
                    }
            }
            File.WriteAllText(proj + ".txt",
                 DateString() + ", " +
                DateTime.Now.ToShortTimeString() + " >>>> " +
                String.Format("{0:00}:{1:00}:{2:00}", dt.Days * 24 + dt.Hours, dt.Minutes, dt.Seconds) +
                "\r\n" + msg);
        }

        private static System.Globalization.PersianCalendar _persianCalendar;
        static System.Globalization.PersianCalendar persianCalendar
        {
            get { return _persianCalendar = _persianCalendar ?? new System.Globalization.PersianCalendar(); }
        }
        /// <summary>
        /// 1,2,...,12
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int PersianMonth(DateTime d)
        {
            return persianCalendar.GetMonth(d);
        }
        public static readonly List<string> PersianMonthNames = new List<string> { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };

        public static string PersianMonthName(int m)
        {
            return PersianMonthNames[m - 1];
        }
        /// <summary>
        /// day of mounth: 1..31
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int PersianDay(DateTime d)
        {
            return persianCalendar.GetDayOfMonth(d);
        }
        public static string MonthName(DateTime d)
        {
            var Names = new string[] {
                "فروردین",
                "اردیبهشت",
                "خرداد",
                "تیر",
                "مرداد",
                "شهریور",
                "مهر",
                "آبان",
                "آذر",
                "دی",
                "بهمن",
                "اسفند",};
            return Names[persianCalendar.GetMonth(d) - 1];
        }
        public static string DateString(DateTime? d = null)
        {
            return DateString(d ?? DateTime.Now);
        }
        public static string DateString()
        {
            return DateString(DateTime.Now);
        }
        public static string DateTimeString(DateTime? d = null) { return DateString(d) + " " + TimeString(d); }
        public static string DateString(DateTime d)
        {
            var pc = persianCalendar;
            return (pc.GetYear(d) % 100).ToString("00") + "/"
                + pc.GetMonth(d).ToString("00") + "/"
                + pc.GetDayOfMonth(d).ToString("00");
        }
        public static string DateTimeString2(DateTime d)
        {
            var pc = persianCalendar;
            return (pc.GetYear(d) % 100).ToString("00") + "."
                + pc.GetMonth(d).ToString("00") + "."
                + pc.GetDayOfMonth(d).ToString("00") + "-" +
                d.ToString("HH.mm.ss");
        }
        public static int PersianYear(DateTime d)
        {
            return persianCalendar.GetYear(d);
        }
        public static DateTime AddPersianMonths(DateTime d, int n = 1)
        {
            return persianCalendar.AddMonths(d, n);
        }
        public static DateTime AddPersianDays(DateTime d, int n = 1)
        {
            return persianCalendar.AddDays(d, n);
        }
        public static DateTime PersianParse(string str, int hour = 0, int minute = 0, int second = 0)
        {
            str = str.Trim();
            var persianDateMatch = System.Text.RegularExpressions.Regex.Match(
                str, @"^(\d+)/(\d+)/(\d+)$");
            if (persianDateMatch.Groups.Count < 4)
                persianDateMatch = System.Text.RegularExpressions.Regex.Match(
                    str, @"^(\d+)\-(\d+)\-(\d+)$");
            if (persianDateMatch.Groups.Count < 4)
                persianDateMatch = System.Text.RegularExpressions.Regex.Match(
                    str, @"^(\d+)\.(\d+)\.(\d+)$");
            var year = int.Parse(persianDateMatch.Groups[1].Value);
            if (year < 100)
            {
                if (year > 50)
                    year += 1300;
                else if (year < 50)
                    year += 1400;
            }
            var month = int.Parse(persianDateMatch.Groups[2].Value);
            var day = int.Parse(persianDateMatch.Groups[3].Value);
            return persianCalendar.ToDateTime(year, month, day, hour, minute, second, 0);
        }
        public static DateTime PersianParse_DateTime(string str)
        {
            try
            {
                str = str.Trim();
                var persianDateMatch = System.Text.RegularExpressions.Regex.Match(
                    str, @"^(\d+)/(\d+)/(\d+)\s+(\d+):(\d+)$");
                if (persianDateMatch.Groups.Count < 5)
                    return PersianParse(str);

                var year = int.Parse(persianDateMatch.Groups[1].Value);
                if (year < 100)
                {
                    if (year > 50)
                        year += 1300;
                    else if (year < 50)
                        year += 1400;
                }
                var month = int.Parse(persianDateMatch.Groups[2].Value);
                var day = int.Parse(persianDateMatch.Groups[3].Value);
                var hour = int.Parse(persianDateMatch.Groups[4].Value);
                var minute = int.Parse(persianDateMatch.Groups[5].Value);
                return persianCalendar.ToDateTime(year, month, day, hour, minute, 0, 0);
            }
            catch
            {
                return PersianParse(str);
            }
        }
        public static string DayString()
        {
            return DayString(DateTime.Now);
        }
        public static string DayString(DateTime d)
        {
            var day = (int)persianCalendar.GetDayOfWeek(d);
            return new string[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه", "شنبه" }[day];
        }
        public static string TimeString(DateTime? d = null)
        {
            return (d ?? DateTime.Now).ToString("HH:mm");
        }
        public static string TimeString(TimeSpan dt)
        {
            return String.Format("{0:00}:{1:00}", dt.Days * 24 + dt.Hours, dt.Minutes);
        }
        public static DateTime Max(DateTime d1, DateTime d2)
        {
            return d1 > d2 ? d1 : d2;
        }
        public static DateTime Min(DateTime d1, DateTime d2)
        {
            return d1 < d2 ? d1 : d2;
        }
        internal static string GetFileHash(string file)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(file))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream));
                }
            }
        }
        public static string GetFreeDriveLeter(char from = 'V')
        {
            var allDrives = DriveInfo.GetDrives();
            var d = from;
            repeat:
            foreach (var d_ in allDrives)
                if (d_.Name.ToUpper()[0] == d)
                {
                    d = (char)((int)d - 1);
                    goto repeat;
                }
            return d + "";
        }
        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            public uint cbSize;
            public int dwTime;
        }
        /// <summary>
        /// Idle Time in seconds 
        /// </summary>
        /// <returns></returns>
        internal static double GetIdleTime()
        {
            var lii = new LASTINPUTINFO() { cbSize = (uint)Marshal.SizeOf(typeof(LASTINPUTINFO)) };
            GetLastInputInfo(ref lii);
            var res2 = (Environment.TickCount - lii.dwTime) / 1000.0;
            return res2;
        }
        [DllImport("shell32.dll", EntryPoint = "#261", CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern void GetUserTilePath(string username, UInt32 whatever, System.Text.StringBuilder picpath, int maxLength);

        internal static string Number_fr2en(string v)
        {
            return v
                .Replace("٠", "0")
                .Replace("١", "1")
                .Replace("٢", "2")
                .Replace("٣", "3")
                .Replace("٤", "4")
                .Replace("٥", "5")
                .Replace("٦", "6")
                .Replace("٧", "7")
                .Replace("٨", "8")
                .Replace("٩", "9")

                .Replace("۰", "0")
                .Replace("۱", "1")
                .Replace("۲", "2")
                .Replace("۳", "3")
                .Replace("۴", "4")
                .Replace("۵", "5")
                .Replace("۶", "6")
                .Replace("۷", "7")
                .Replace("۸", "8")
                .Replace("۹", "9")
                ;


        }

        public static string GetUserTilePath(string username)
        {   // username: use null for current user
            var sb = new System.Text.StringBuilder(1000);
            GetUserTilePath(username, 0x80000000, sb, sb.Capacity);
            return sb.ToString();
        }

        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
        private extern static bool InternetGetConnectedState(ref InternetConnectionState_e lpdwFlags, int dwReserved);

        [Flags]
        enum InternetConnectionState_e : int
        {
            INTERNET_CONNECTION_MODEM = 0x1,
            INTERNET_CONNECTION_LAN = 0x2,
            INTERNET_CONNECTION_PROXY = 0x4,
            INTERNET_RAS_INSTALLED = 0x10,
            INTERNET_CONNECTION_OFFLINE = 0x20,
            INTERNET_CONNECTION_CONFIGURED = 0x40
        }

        public static bool IsInternet()
        {
            InternetConnectionState_e flags = 0;
            bool isConnected = InternetGetConnectedState(ref flags, 0);
            return flags != InternetConnectionState_e.INTERNET_CONNECTION_OFFLINE;
        }

        public static Image GetUserImage()
        {
            return Image.FromFile(GetUserTilePath(Environment.UserName));
        }
        public static Bitmap MakeGrayscale(Bitmap original, bool hot)
        {
            var newBitmap = new Bitmap(original.Width, original.Height);

            using (var g = Graphics.FromImage(newBitmap))
            {
                float F = 0.5f;
                var colorMatrix = new ColorMatrix(
                   new float[][]
                          {
                             new float[] {F, F, 1f, 0, 0},
                             new float[] {F, F, 1f, 0, 0},
                             new float[] {F, F, 1f, 0, 0},
                             new float[] {0, 0, 0, 1, 0},
                             new float[] {0, 0, 0, 0, 1}
                          });
                if (hot)
                    colorMatrix = new ColorMatrix(
                          new float[][]
                                  {
                                     new float[] {F, 0, 0, 0, 0},
                                     new float[] {F, 0, 0, 0, 0},
                                     new float[] {F, 0, 0, 0, 0},
                                     new float[] {0, 0, 0, 1, 0},
                                     new float[] {0, 0, 0, 0, 1}
                                  });

                var attributes = new ImageAttributes();

                attributes.SetColorMatrix(colorMatrix);
                g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                   0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            }
            return newBitmap;
        }

        public class PictureBox_ : PictureBox
        {
            protected override void WndProc(ref Message m)
            {
                const uint WM_NCHITTEST = 0x84;
                const int HTTRANSPARENT = -1;

                if (!DesignMode && m.Msg == WM_NCHITTEST)
                {
                    m.Result = new IntPtr(HTTRANSPARENT);
                    return;
                }
                base.WndProc(ref m);
            }
        }
        public static void ExportFile(byte[] resource, string fileName, bool overwrite = true)
        {
            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            if (overwrite || !File.Exists(fileName))
                using (var binWriter = new BinaryWriter(File.Open(fileName, FileMode.Create)))
                {
                    binWriter.Write(resource);
                    binWriter.Close();
                }
        }

        public static bool IsEnglish(string str)
        {
            var A = "abcdefghijklmnopqrtsuvwxyzABCDEFGHIJKLMNOPQRTSUVWXYZ";
            foreach (var a in A)
                if (str.Contains(a + "")) return true;
            return false;
        }

        public static T Last<T>(List<T> list)
        {
            return list[list.Count - 1];
        }

        public static Bitmap AddProgress(Image img, int value)
        {
            img = (Image)(new Bitmap(img, new Size(16, 16)));
            var g = Graphics.FromImage(img);
            Color color = Color.Lime;
            if (value < 75)
                color = Color.Blue;
            else if (value < 50)
                color = Color.Yellow;
            else if (value < 25)
                color = Color.Orange;
            g.FillRectangle(new SolidBrush(color), 0, 16 - (int)Math.Round((value * 16) / 100.0), 3, 16);
            return (Bitmap)img;
        }
        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);

        internal static string MyHash(string s, int length = -1)
        {
            if (s == null || s == "") return s;
            string pat = "quFSeCEL8OcAaJvRVk69pDz01r25WZnmKNoGb 34UXydfgHTPtYBjlwsM7xQh";
            string rep = "oPpQq RrShJjGgHXxYyZz0123tUuVvWwsTCcDdEAaBKkLlMeFfbmNnO456789";
            s = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(s));
            var CH = s.ToCharArray();
            var s2 = "";
            for (int i = 0; i < s.Length; i++)
            {
                int j = pat.IndexOf(s[i]);
                s2 += (j >= 0) ? rep[j] : s[i];
            }
            return s2;
        }
        internal static string MyUnHash(string s)
        {
            if (s == null || s == "") return s;
            string rep = "quFSeCEL8OcAaJvRVk69pDz01r25WZnmKNoGb 34UXydfgHTPtYBjlwsM7xQh";
            string pat = "oPpQq RrShJjGgHXxYyZz0123tUuVvWwsTCcDdEAaBKkLlMeFfbmNnO456789";
            var CH = s.ToCharArray();
            var s2 = "";
            for (int i = 0; i < s.Length; i++)
            {
                int j = pat.IndexOf(s[i]);
                s2 += (j >= 0) ? rep[j] : s[i];
            }
            s2 = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(s2));
            return s2;
        }
        public static string md5(string str)
        {
            byte[] textBytes = System.Text.Encoding.Default.GetBytes(str);
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider cryptHandler;
                cryptHandler = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] hash = cryptHandler.ComputeHash(textBytes);
                string ret = "";
                foreach (byte a in hash)
                    ret += ((a < 16) ? "0" : "") + a.ToString("x");
                return ret;
            }
            catch
            {
                throw;
            }
        }

        static string _hid = null;
        public static string GetHID()
        {
            if (_hid + "" != "") return _hid;
            string id = "";
            try
            {
                var A = new ManagementObjectSearcher("Select ProcessorId From Win32_processor").Get();
                foreach (var a in A)
                {
                    id += a["ProcessorId"] + ""; break;
                }
            }
            catch { id += "e r r 1"; }
            try
            {
                var A = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard").Get();
                foreach (var a in A)
                {
                    id += a["SerialNumber"] + "";
                    break;
                }
            }
            catch { id += "e r r 2"; }
            id = md5(id + "-" + Environment.UserName + "-" + Environment.MachineName).PadRight(32);
            var CH = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var res = "";
            for (int i = 0; i < 8; i++)
                res += CH[((int)id[i] + (int)id[i + 8] + (int)id[i + 16] + (int)id[i + 24]) % CH.Length];
            return _hid = res;
        }

        /// <summary>
        /// 000.00.00.000 -> xxxxxxxxxx
        /// </summary>
        /// <param name="ver"></param>
        /// <returns></returns>
        public static int Version2Int(string ver)
        {
            var A1 = ver.Split(new char[] { '.' });
            return Convert.ToInt32(A1[0].PadLeft(3, '0') + A1[1].PadLeft(2, '0') + A1[2].PadLeft(2, '0') + A1[3].PadLeft(3, '0'));
        }
        public static string Int2Version(int ver)
        {
            return $"{(ver / 10000000) % 1000}.{(ver / 100000) % 100}.{(ver / 1000) % 100}.{ver % 1000}";
        }

        /// <summary>
        /// Adds an ACL entry on the specified file for the specified account.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="account"></param>
        /// <param name="rights"></param>
        /// <param name="controlType"></param> 
        public static void AddDirSecurity(string fileName, string account,
            FileSystemRights rights, AccessControlType controlType)
        {
            var fSecurity = Directory.GetAccessControl(fileName);

            fSecurity.AddAccessRule(new FileSystemAccessRule(account, rights,
                 InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, controlType));

            Directory.SetAccessControl(fileName, fSecurity);

        }

        public static string ReadFile(string file)
        {
            using (var stream = new FileInfo(file).Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(stream))
                return sr.ReadToEnd();
        }
        /// <summary>
        /// wait while file is locked or timeout
        /// </summary>
        /// <param name="file"></param>
        /// <param name="timeout">in ms</param>
        /// <returns></returns>
        public static void WaitForFile(string file, int timeout = 1000)
        {
            var dt = 50;
            var n = timeout / dt;
            FileStream stream = null;
            try
            {
                for (int i = 0; i < n; i++)
                    try
                    {
                        stream = new FileInfo(file).Open(FileMode.Open, FileAccess.Read, FileShare.None);
                        break;
                    }
                    catch { System.Threading.Thread.Sleep(dt); }
            }
            finally
            {
                if (stream != null) stream.Close();
            }
        }
        public static bool IsFileLocked(string file)
        {
            FileStream stream = null;
            try
            {
                stream = new FileInfo(file).Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null) stream.Close();
            }
            return false;
        }

        public static bool IsNotSetString(string string2Check)
        {
            return (string2Check == "" || string2Check == null);
        }
        public static DateTime i2t(long i)
        {
            return _TIME_.AddSeconds(i);
        }
        public static DateTime i2t(string i)
        {
            return _TIME_.AddSeconds(Convert.ToInt64(i));
        }
        public static long t2i(DateTime? t = null)
        {
            if (t == null) t = DateTime.Now;
            return (long)(t - _TIME_).Value.TotalSeconds;
        }
        public static DateTime _TIME_ = new DateTime(2017, 3, 1);

        static System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        private static Dictionary<int, Timer> timers = new Dictionary<int, Timer>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Do"></param>
        /// <param name="interval">delay in milliseconds</param>
        /// <param name="e">optional argument</param>
        public static Timer DoWithDelay(Act Do, int interval, int abort_id = -1, object e = null, int repeat = 1)
        {
            Timer t;
            if (abort_id >= 0 && timers.ContainsKey(abort_id))
            {
                t = timers[abort_id];
                t.Stop();
            }
            else
            {
                t = new Timer();
                if (abort_id >= 0)
                    timers.Add(abort_id, t);
            }
            t.Interval = interval;
            var i = 1;
            t.Tick += (s, e_) =>
            {
                Do();
                if (++i > repeat || !t.Enabled)
                {
                    t.Stop();
                    t.Dispose();
                    timers.Remove(abort_id);
                }
            };
            t.Start();
            return t;
        }

    }
    public delegate void Act();

}