using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime;
using System.Linq;

namespace stopwatch
{
    public class ExcelGenerator : IDisposable
    {
        public ExcelGenerator(string FileName, int sheet = 1, bool refresh_data = false)
        {
            this.FileName = FileName;
            int rrr = 10;
            while (File.Exists(FileName) && IsFileLocked(FileName) && rrr-- > 0)
                if (Form_msg.Show(null,
                    "فایل باز است:" + "\r\n" + FileName + "\r\n" + "لطفا آن را ببندید",
                    "StopWatch", btn: MessageBoxButtons.YesNo, yesCaption: "ادامه", noCaption: "توقف") == DialogResult.No)
                {
                    HasError = true;
                    return;
                }
            if (refresh_data)
                RefreshData();
            dir = Path.GetTempPath() + "\\stp-" + Path.GetFileNameWithoutExtension(FileName) + "\\";
            Zip.UnZipFiles(FileName, dir, deleteZipFile: false);
            CurrentSheet = sheet;
        }
        public bool HasError = false;
        public static bool abort = false;
        public void RefreshData()
        {
            abort = false;
            var p = System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("excel", "\"" + FileName + "\" /s /e")
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized
            });
            p.WaitForInputIdle();
            System.Threading.Thread.Sleep(50);
            while (!p.HasExited)
            {
                p.Refresh();
                var hwnd = p.MainWindowHandle;
                if (hwnd.Equals(IntPtr.Zero))
                {
                    System.Threading.Thread.Sleep(20);
                    continue;
                }
                /*if (hwnd.Equals(IntPtr.Zero))
                {
                    var H = GetWinsOfProc(p.Id, true);
                    if (H.Count > 0)
                    {
                        hwnd = H[0];
                        if (SetForegroundWindow(hwnd) != 0)
                        {
                            SendKeys.SendWait("%{F4}");
                            SendKeys.Flush();
                        }
                    }
                }
                else*/
                p.CloseMainWindow();
                try
                {
                    p.WaitForInputIdle();
                }
                catch { System.Threading.Thread.Sleep(50); }
                if (!p.HasExited)
                    try
                    {
                        foreach (var s in GetWinsOfProcClass(p.Id))
                        {
                            ShowWindow(hwnd, SW_RESTORE);
                            if (SetForegroundWindow(hwnd) != 0 && s.EndsWith("Dialog", StringComparison.OrdinalIgnoreCase))
                            {
                                if (abort) break;
                                SendKeys.SendWait("%{F4}");
                                SendKeys.SendWait("s");
                                SendKeys.SendWait("o");
                                SendKeys.Flush();
                            }
                        }
                    }
                    catch { }

                p.WaitForExit(100);
                Application.DoEvents();
            }
        }
        #region win32 API
        const UInt32 GW_CHILD = 0x0006;
        [DllImport("user32.dll")]
        static extern IntPtr GetWindow(IntPtr hWnd, UInt32 uCmd);
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMAXIMIZED = 3;
        private const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        delegate bool Win32Callback(IntPtr hwnd, IntPtr lParam);
        [DllImport("user32.Dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumChildWindows(IntPtr parentHandle, Win32Callback callback, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);


        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        static string GetWindowText(IntPtr hwnd)
        {
            int len;
            // Window caption
            if ((len = GetWindowTextLength(hwnd)) > 0)
            {
                var sb = new StringBuilder(len + 1);
                if (GetWindowText(hwnd, sb, sb.Capacity) == 0)
                    throw new Exception(String.Format("unable to obtain window caption, error code {0}", Marshal.GetLastWin32Error()));
                return sb.ToString();
            }
            return "";
        }
        static string GetClassName(IntPtr hwnd)
        {
            int len = 256;
            var sb = new StringBuilder(len + 1);
            if (GetClassName(hwnd, sb, sb.Capacity) == 0)
                throw new Exception(String.Format("unable to obtain window caption, error code {0}", Marshal.GetLastWin32Error()));
            return sb.ToString();
        }
        List<IntPtr> GetWinsOfProc(int pid, bool not_empty_text = true)
        {
            List<IntPtr> rootWindows = GetChildWindows(IntPtr.Zero);
            List<IntPtr> dsProcRootWindows = new List<IntPtr>();
            foreach (IntPtr hWnd in rootWindows)
            {
                uint lpdwProcessId;
                GetWindowThreadProcessId(hWnd, out lpdwProcessId);
                if (lpdwProcessId == pid)
                    if (not_empty_text == false || GetWindowText(hWnd) != "")
                        dsProcRootWindows.Add(hWnd);
            }
            return dsProcRootWindows;
        }
        List<string> GetWinsOfProcText(int pid, bool not_empty_text = true)
        {
            List<IntPtr> rootWindows = GetWinsOfProc(pid, not_empty_text);
            List<string> dsProcRootWindows = new List<string>();
            foreach (IntPtr hWnd in rootWindows)
                dsProcRootWindows.Add(GetWindowText(hWnd));
            return dsProcRootWindows;
        }
        List<string> GetWinsOfProcClass(int pid, bool not_empty_text = true)
        {
            List<IntPtr> rootWindows = GetWinsOfProc(pid, not_empty_text);
            List<string> dsProcRootWindows = new List<string>();
            foreach (IntPtr hWnd in rootWindows)
                dsProcRootWindows.Add(GetClassName(hWnd));
            return dsProcRootWindows;
        }

        static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try
            {
                Win32Callback childProc = new Win32Callback(EnumWindow);
                EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }

        static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(pointer);
            List<IntPtr> list = gch.Target as List<IntPtr>;
            if (list == null)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }
            list.Add(handle);
            //  You can modify this to check to see if you want to cancel the operation, then return a null here
            return true;
        }
        public static bool IsFileLocked(string fileName)
        {
            FileStream stream = null;
            var file = new FileInfo(fileName);
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
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
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        #endregion

        public string FileName
        {
            get; private set;
        }
        public int CurrentSheet
        {
            get { return _CurrentSheet; }
            set
            {
                if (value != _CurrentSheet)
                {
                    if (CurrentSheet >= 1)
                    {
                        var sb = new StringBuilder();
                        foreach (var r in new_data)
                            r.Export(sb);
                        sheet_data = sheet_data.Replace("</sheetData>", sb + "</sheetData>");
                        new_data.Clear();
                        File.WriteAllText(dir + "xl\\worksheets\\sheet" + CurrentSheet + ".xml", sheet_data);
                    }
                    if ((_CurrentSheet = value) > 0)
                    {
                        sheet_data = File.ReadAllText(dir + "xl\\worksheets\\sheet" + CurrentSheet + ".xml").Replace("\r\n", "");
                        row_start_positions = new int[2000];
                        var last = 0;
                        for (int i = 0; i < row_start_positions.Length; i++)
                        {
                            last = sheet_data.IndexOf("<row r=\"" + (i + 1) + "\"", last);
                            if (last < 0) break;
                            row_start_positions[i] = last;
                        }
                    }
                }
            }
        }
        int[] row_start_positions = null;
        int _CurrentSheet = -1;
        string dir;
        internal string sheet_data;
        internal List<RowData> new_data = new List<RowData>();
        public class RowData
        {
            public int row;
            public Dictionary<string, string> cell_data = new Dictionary<string, string>();
            public void Export(StringBuilder sb)
            {
                sb.Append($"<row r=\"{(row + 1)}\" spans=\"1:{cell_data.Count}\" x14ac:dyDescent=\"0.25\">");
                foreach (var c in cell_data)
                    sb.Append($"<c r=\"{c.Key}\" t=\"str\"><v>{c.Value}</v></c>");
                sb.Append($"</row>");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row">0-based</param>
        /// <param name="column">0-based</param>
        /// <param name="value"></param>
        public void AddCellValue(int row, int column, string value)
        {
            var R = new_data.Find(r => r.row == row);
            if (R == null)
                new_data.Add(R = new RowData { row = row });
            R.cell_data[GetCellName(row, column)] = value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row">0-based</param>
        /// <param name="column">0-based</param>
        /// <param name="value"></param> 
        public void AddCellValue_time_minutes(int row, int column, double value)
        {
            var v = (int)Math.Floor(value);
            var h = v / 60;
            var m = v % 60;
            var s = Math.Round((value - v) * 60);
            AddCellValue_time(row, column, h.ToString("0") + ":" + m.ToString("00") + ":" + s.ToString("00"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row">0-based</param>
        /// <param name="column">0-based</param> 
        public static string GetCellName(int row, int column)
        {
            var AZ = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var N = AZ.Length;
            var cell = "";
            if (column < N)
                cell = AZ[column].ToString() + "" + (row + 1);
            else if (column < N * N)
                cell = AZ[column / N - 1].ToString() + AZ[column % N].ToString() + "" + (row + 1);
            else if (column < N * N * N)
                cell = AZ[column / (N * N) - 1].ToString() + AZ[(column / N - 1) % N].ToString() + AZ[column % N].ToString() + "" + (row + 1);
            return cell;
        }

        public void AddCellValue_time(int row, int column, string value)
        {
            var V = value.Split(new char[] { ':' });
            if (V.Length > 1)
            {
                var t = (Convert.ToInt16(V[0]) * 60 + Convert.ToInt16(V[1])) / (24 * 60.0);
                AddCellValue(row, column, t + "");
            }
            else
                AddCellValue(row, column, value);
        }

        public void ChangeSheetName(string name, int sheet = -1)
        {
            if (sheet <= 0)
                sheet = CurrentSheet;
            var book = File.ReadAllText(dir + "xl\\workbook.xml");
            int p2 = 0;
            for (int i = 1; i <= sheet; i++)
                p2 = book.IndexOf("<sheet name=\"", p2) + 13;
            var p3 = book.IndexOf("\"", p2);
            book = book.Remove(p2, p3 - p2);
            book = book.Insert(p2, name);
            p2 = book.IndexOf("<calcPr");
            if (p2 >= 0)
            {
                p3 = book.IndexOf(">", p2);
                book = book.Remove(p2, p3 - p2 + 1);
            }
            File.WriteAllText(dir + "xl\\workbook.xml", book);
        }
        public string GetSheetName(int sheet = -1)
        {
            if (sheet <= 0)
                sheet = CurrentSheet;
            var book = File.ReadAllText(dir + "xl\\workbook.xml");
            int p2 = 0;
            for (int i = 1; i <= sheet; i++)
                p2 = book.IndexOf("<sheet name=\"", p2) + 13;
            var p3 = book.IndexOf("\"", p2);
            return book.Substring(p2, p3 - p2);
        }
        public void ReplaceComment(string old_comment, string new_comment, int sheet = -1)
        {
            if (sheet <= 0)
                sheet = CurrentSheet;
            var comment_data = File.ReadAllText(dir + "xl\\comments" + sheet + ".xml");
            comment_data = comment_data.Replace(old_comment, new_comment);
            File.WriteAllText(dir + "xl\\comments" + sheet + ".xml", comment_data);
        }
        public void Close()
        {
            CurrentSheet = -1;
            {
                if (File.Exists(FileName))
                    File.Delete(FileName);
                Zip.CreateZip(dir, FileName);
            }
            try
            {
                Directory.Delete(dir, true);
            }
            catch { }
        }

        ~ExcelGenerator()
        {
            Dispose();
        }
        public void Dispose()
        {
            if (dir + "" != "" && Directory.Exists(dir))
                Close();
        }
    }

}
