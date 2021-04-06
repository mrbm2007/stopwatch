using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime;

namespace stopwatch
{
    public class ExcelEditor : IDisposable
    {
        public ExcelEditor(string FileName, int sheet = 1, bool read_only = false, bool refresh_data = false)
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
            this.read_only = read_only;
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

        bool read_only = false;
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
                        File.WriteAllText(dir + "xl\\worksheets\\sheet" + CurrentSheet + ".xml", sheet_data);
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
        List<string> _shared_strings = null;
        List<string> shared_strings
        {
            get
            {
                if (_shared_strings == null)
                {
                    var str = File.ReadAllText(dir + "xl\\sharedStrings.xml").Replace("<t/>", "<t></t>");//.Replace(" xml:space=\"preserve\"", "");
                    var M = System.Text.RegularExpressions.Regex.Matches(str, "<t[^>]*>([^<]*)</t>");
                    _shared_strings = new List<string>();
                    foreach (System.Text.RegularExpressions.Match m in M)
                    {
                        var s_ = m.Groups[1].Value;
                        if (s_.StartsWith(">")) s_ = s_.Substring(1);
                        _shared_strings.Add(s_);
                    }

                    return _shared_strings;
                }

                return _shared_strings;
            }
        }
        string link_data
        {
            get
            {
                return File.ReadAllText(dir + "xl\\worksheets\\_rels\\sheet" + CurrentSheet + ".xml.rels");
            }
            set
            {
                File.WriteAllText(dir + "xl\\worksheets\\_rels\\sheet" + CurrentSheet + ".xml.rels", value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row">0-based</param>
        /// <param name="column">0-based</param>
        /// <param name="value"></param>
        public void SetCellValue(int row, int column, string value)
        {
            SetCellValue(GetCellName(row, column), value, row);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row">0-based</param>
        /// <param name="column">0-based</param>
        /// <param name="value"></param>
        public void SetCellValue_time_minutes(int row, int column, double value)
        {
            var v = (int)Math.Round(value);
            SetCellValue_time(row, column, (v / 60).ToString("0") + ":" + (v % 60).ToString("00"));
        }
        public void SetCellValue_time_minutes(string cell, double value)
        {
            var v = (int)Math.Floor(value);
            var h = v / 60;
            var m = v % 60;
            var s = Math.Round((value - v) * 60);
            SetCellValue_time(cell, h.ToString("0") + ":" + m.ToString("00") + ":" + s.ToString("00"));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row">0-based</param>
        /// <param name="column">0-based</param>
        /// <param name="value"></param>
        public void SetCellValue_time(int row, int column, string value)
        {
            SetCellValue_time(GetCellName(row, column), value);
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
        public void SetCellValue(string cell, string value, int hint_row = -1)
        {
            try
            {
                if (read_only) throw new Exception("Excel file is opened in read only mode and can't be modified!");
                var s0 = (hint_row >= 0 && hint_row < row_start_positions.Length) ? row_start_positions[hint_row] : 0;
                var p1 = sheet_data.IndexOf("r=\"" + cell + "\"", s0);
                if (p1 < 0)
                    p1 = sheet_data.IndexOf("r=\"" + cell + "\"", 0);
                {
                    var p4 = sheet_data.IndexOf("/>", p1);
                    var p5 = sheet_data.IndexOf("<", p1);
                    if (p4 < p5)
                    {
                        var sw = new StringBuilder();
                        sw.Append(sheet_data, 0, p4);
                        sw.Append(" t=\"str\"><v>" + value + "</v></c>");
                        sw.Append(sheet_data, p4 + 2, sheet_data.Length - (p4 + 2));
                        sheet_data = sw.ToString();
                        //sheet_data = sheet_data.Remove(p4, 2);
                        //sheet_data = sheet_data.Insert(p4, " t=\"str\"><v>" + value + "</v></c>");
                        return;
                    }
                }
                var p2 = sheet_data.IndexOf("<v>", p1) + 3;
                var p3 = sheet_data.IndexOf("</v>", p2);

                {
                    var sw = new StringBuilder();
                    sw.Append(sheet_data, 0, p2);
                    sw.Append(value);
                    sw.Append(sheet_data, p3, sheet_data.Length - p3);
                    sheet_data = sw.ToString();
                }

                //sheet_data = sheet_data.Remove(p2, p3 - p2);
                //sheet_data = sheet_data.Insert(p2, value);

                p2 = sheet_data.IndexOf("t=\"s\"", p1) + 3;
                if (p2 > p3) return;
                p3 = sheet_data.IndexOf("\"", p2);
                {
                    var sw = new StringBuilder();
                    sw.Append(sheet_data, 0, p2);
                    sw.Append("str");
                    sw.Append(sheet_data, p3, sheet_data.Length - p3);
                    sheet_data = sw.ToString();
                }
                //sheet_data = sheet_data.Remove(p2, p3 - p2);
                //sheet_data = sheet_data.Insert(p2, "str");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Excell.SetCellValue: (" + cell + ") : " + ex.Message);
                //throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="column">0-based</param>
        public void SetColWidth(int column, double width)
        {
            column++;
            sheet_data = sheet_data.Replace("</cols>", "<col min=\"" + column + "\" max=\"" + column + "\" width=\"" + width + "\"  customWidth=\"1\"/></cols>");
        }
        public void AddHyperLink(string cell, string link)
        {
            if (read_only) throw new Exception("Excel file is opened in read only mode and can't be modified!");
            var links = link_data;
            int id = 1;
            while (links.Contains("Id=\"rId" + id + "\"")) id++;
            link_data = links.Replace("</Relationships>", "<Relationship Id=\"rId" + id
                + "\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/hyperlink\" "
                + "Target=\"" + link + "\" TargetMode=\"External\"/></Relationships>");
            sheet_data = sheet_data.Replace("</hyperlinks>", "<hyperlink ref=\"" + cell + "\" r:id=\"rId" + id + "\"/></hyperlinks>");
        }
        public void SetCellStyle(string cell, object style)
        {
            if (read_only) throw new Exception("Excel file is opened in read only mode and can't be modified!");
            var p = sheet_data.IndexOf("r=\"" + cell + "\"");
            var p2 = sheet_data.IndexOf("s=\"", p) + 3;
            var p3 = sheet_data.IndexOf("\"", p2 + 1);
            var p4 = sheet_data.IndexOf(">", p);
            if (p4 < p2)
            {
            }
            else
            {
                sheet_data = sheet_data.Remove(p2, p3 - p2);
                sheet_data = sheet_data.Insert(p2, style + "");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row">0-based</param>
        /// <param name="column">0-based</param>
        /// <param name="value"></param>
        public void SetCellStyle(int row, int column, string value)
        {
            SetCellStyle(GetCellName(row, column), value);
        }
        public void SetCellValue_time(string cell, string value)
        {
            if (read_only) throw new Exception("Excel file is opened in read only mode and can't be modified!");
            var V = value.Split(new char[] { ':' });
            if (V.Length > 1)
            {
                var t = (Convert.ToInt16(V[0]) * 60 + Convert.ToInt16(V[1])) / (24 * 60.0);
                SetCellValue(cell, t + "");
            }
            else
                SetCellValue(cell, value);
        }
        public string GetCellStyle(string cell)
        {
            var p1 = sheet_data.IndexOf("r=\"" + cell + "\"");
            if (p1 < 0) return "";
            var p2 = sheet_data.IndexOf("s=\"", p1) + 3;
            var p3 = sheet_data.IndexOf("\"", p2 + 1);
            var p4 = sheet_data.IndexOf(">", p1);
            if (p4 < p2)
                return "";
            else
                return sheet_data.Substring(p2, p3 - p2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row">0-based</param>
        /// <param name="column">0-based</param> 
        public string GetCellStyle(int row, int column)
        {
            return GetCellStyle(GetCellName(row, column));
        }
        public string GetCellValue(string cell, int hint_row = -1)
        {
            var s0 = (hint_row >= 0 && hint_row < row_start_positions.Length) ? row_start_positions[hint_row] : 0;
            var p1 = sheet_data.IndexOf("r=\"" + cell + "\"", s0);
            if (p1 < 0) return "";
            var p2 = sheet_data.IndexOf("/>", p1);
            var p3 = sheet_data.IndexOf("<", p1);
            if (p2 < p3)
                return "";
            var p4 = sheet_data.IndexOf("t=\"s\"", p1);
            if (p4 < p3)
            {
                p2 = sheet_data.IndexOf("<v>", p1) + 3;
                p3 = sheet_data.IndexOf("</v>", p2);
                var s = Convert.ToInt16(sheet_data.Substring(p2, p3 - p2));
                return shared_strings[s];
            }
            else
            {
                p2 = sheet_data.IndexOf("<v>", p1) + 3;
                p3 = sheet_data.IndexOf("</v>", p2);
                return sheet_data.Substring(p2, p3 - p2);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row">0-based</param>
        /// <param name="column">0-based</param> 
        public string GetCellValue(int row, int column, bool throw_not_fond_error = true)
        {
            try
            {
                return GetCellValue(GetCellName(row, column), row);
            }
            catch
            {
                if (throw_not_fond_error) throw;
            }
            return null;
        }

        public void ChangeSheetName(string name, int sheet = -1)
        {
            if (read_only) throw new Exception("Excel file is opened in read only mode and can't be modified!");
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
            if (read_only) throw new Exception("Excel file is opened in read only mode and can't be modified!");
            if (sheet <= 0)
                sheet = CurrentSheet;
            var comment_data = File.ReadAllText(dir + "xl\\comments" + sheet + ".xml");
            comment_data = comment_data.Replace(old_comment, new_comment);
            File.WriteAllText(dir + "xl\\comments" + sheet + ".xml", comment_data);
        }
        public void Close()
        {
            CurrentSheet = -1;
            if (!read_only)
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

        ~ExcelEditor()
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
