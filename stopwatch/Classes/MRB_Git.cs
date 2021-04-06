using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace stopwatch
{
    public class MRB_Git : IDisposable
    {
        public List<string> local_dirs = new List<string> { @"D:\Work\Phishran Sanat\ComPad\Code" };
        public string server_dir
        {
            get
            {
                if (NetworkMode)
                    return drive + ":\\";
                else
                    return (server_root + "\\" + project_code + "\\").Replace("\\\\", "\\");
            }
        }
        public string NetworkPath = "";
        public string server_root = "";
        public string project_code = "";
        public string project_name = "";
        public string user = "";
        public bool hidden_files = false;
        public bool NetworkMode
        {
            get
            { return NetworkPath + "" != ""; }
        }
        public Progress progress;
        public bool abort = false;
        public Settings settings;


        public BackupInfo BackUp()
        {
            try
            {
                OpenBackUpDrive();
                OpennedArchives = new List<string>();
                abort = false;
                if (progress != null) progress(0, "پشتیبان گیری ...");
                var bi = new BackupInfo { time = DateTime.Now };
                var time = Utils.DateTimeString2(bi.time);

                bi.LogFile = time + " " + user + ".log";
                tictoc.tic("BackUp", true);
                tictoc.tic("init");
                if (progress != null) progress(0, "پشتیبان گیری ...");

                // list of all files must backup
                var F_local = new List<string>();
                // list of files from previuse backups : file name , file path in bckup dir (where saved) , unique id (fuid())
                var F_local_last_tag = new Dictionary<string, string[]>();
                if (local_dirs.Count == 0) return bi;

                for (int i = 0; i < local_dirs.Count; i++)
                    if (local_dirs[i].Trim() != "")
                    {
                        if (!local_dirs[i].EndsWith("\\")) local_dirs[i] += "\\";
                        var tmp = Directory.GetFiles(local_dirs[i], "*", SearchOption.AllDirectories);
                        if (tmp.Length > 0)
                            local_dirs[i] = tmp[0].Substring(0, local_dirs[i].Length);// for case (Upper, Lower)
                        F_local.AddRange(tmp);
                    }
                    else
                        local_dirs.RemoveAt(i--);
                try
                {
                    var B = GetBackUpList();
                    var Lines = File.ReadAllLines(B[B.Count - 1].LogFile);
                    foreach (var line in Lines)
                        if (!line.StartsWith("#"))
                            if (line.Trim() != "")
                            {
                                var A = line.Split(new char[] { '|' });
                                F_local_last_tag.Add(A[0], new string[] { A[1], A[2] });
                            }
                }
                catch { }

                var REGX = new List<Regex>();
                if (settings.ignor_files != null)
                    foreach (var pat in settings.ignor_files)
                        REGX.Add(new Regex(pat
                            .Replace("\\", "\\\\")
                            .Replace(".", "\\.")
                            .Replace("(", "\\(")
                            .Replace(")", "\\)")
                            .Replace("*", ".*")
                            .Replace("?", ".")));

                List<FileInfo> F_server_info = null;

                F_server_info = new List<FileInfo>();
                if (!Directory.Exists(server_dir)) Directory.CreateDirectory(server_dir);
                if (!Directory.Exists(server_dir + "files\\")) Directory.CreateDirectory(server_dir + "files\\");
                var F_server = new List<string>();
                F_server = new List<string>(Directory.GetFiles(server_dir + "files\\", "*", SearchOption.AllDirectories));
                foreach (var f in F_server)
                    F_server_info.Add(new FileInfo(f));

                tictoc.toc("init");
                var log_file_name = server_dir + Path.GetFileName(bi.LogFile);

                using (var log_file = new StreamWriter(log_file_name))
                {
                    log_file.WriteLine("# list of files from previuse backups : file name|file path in bckup dir (where saved)|unique id (LastWriteTime,CreationTime,Extension,Length)");
                    AddFiles(F_local, F_local_last_tag, REGX, bi, log_file, F_server_info, time);
                }

                bi.Save(server_dir + time + " " + user + ".inf");
                tictoc.toc("BackUp");
                return bi;
            }
            finally
            { CloseBackUpDrive(); }
        }
        void AddFiles(List<string> F_local, Dictionary<string, string[]> F_local_last_tag,
            List<Regex> REGX,
            BackupInfo bi, StreamWriter log_file,
            List<FileInfo> F_server_info,
            string time,
            string local_rel_pre = "")
        {
            if (progress != null) progress(10, "پشتیبان گیری ...");
            int ii = 0;
            foreach (var f_local in F_local)
            {
                try
                {
                    if (abort) return;
                    tictoc.tic("progress");
                    if (progress != null) progress(10 + 89.0 * ++ii / F_local.Count, "پشتیبان گیری ...");
                    var f_local_rel = f_local;
                    if (local_rel_pre + "" != "")
                        f_local_rel = local_rel_pre + f_local_rel.Substring(3);
                    else
                        foreach (var local_dir in local_dirs)
                            if (local_dir.Trim() != "" && f_local.StartsWith(local_dir))
                            {
                                f_local_rel = f_local.Replace(local_dir, "");
                                if (local_dirs.Count > 1)
                                    f_local_rel = Path.GetFileName(Path.GetDirectoryName(local_dir)) + "\\" + f_local_rel;
                                break;
                            }
                    tictoc.toc_tic("skip");
                    // check for skip
                    try
                    {
                        var f_local_ = f_local.ToLower();
                        if (f_local_.EndsWith(".tc"))
                            try
                            {
                                var letter = 'K';
                                var d = "";
                                foreach (var L in DriveInfo.GetDrives())
                                    d += L.Name[0].ToString().ToUpper();
                                while (d.Contains(letter + ""))
                                    letter = (char)(letter + 1);
                                var tc = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\TrueCrypt\\TrueCrypt.exe";
                                Process.Start(tc, "/v \"" + f_local + "\" /l " + letter + "  /h n /quit").WaitForExit();
                                var F_local2 = new List<string>(Directory.GetFiles(letter + ":\\", "*", SearchOption.AllDirectories));
                                OpennedArchives.Add(letter + "");
                                AddFiles(
                                    F_local2, F_local_last_tag,
                                    REGX,
                                    bi, log_file,
                                    F_server_info,
                                    time,
                                    f_local_rel + "\\");
                                continue;
                            }
                            catch { }
                        if (settings.ignor_obj)
                        {
                            var dir = Path.GetDirectoryName(f_local_);
                            if (f_local_.EndsWith(".pdb")
                                || f_local_.EndsWith(".obj")
                                || dir.EndsWith("\\obj")
                                || dir.EndsWith("\\obj\\debug")
                                || dir.EndsWith("\\obj\\release"))
                                continue;
                        }
                        if (f_local_.Contains("\\$tf\\")) continue;
                        if (f_local_.Contains("\\$tf1\\")) continue;
                        if (f_local_.Contains("\\$tf2\\")) continue;
                        if (f_local_.Contains("\\.vs\\")) continue;
                        if (settings.ignor_rst && f_local_.EndsWith(".rst")) continue;
                        if (settings.ignor_baktmpasv &&
                            (f_local_.EndsWith(".bak") || f_local_.EndsWith(".tmp") || f_local_.EndsWith(".asv"))) continue;
                        if (f_local_.EndsWith(".dball")) continue;
                        var skip = false;
                        foreach (var regx in REGX)
                            if (regx.IsMatch(f_local_))
                            {
                                skip = true;
                                progress(-100, new string[] { f_local, "*" }); // انتخاب کاربر
                                break;
                            }
                        if (skip) continue;
                    }
                    finally { tictoc.toc(); }
                    tictoc.tic("search1");
                    var f_local_info = new FileInfo(f_local);
                    if ((f_local_info.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    {
                        progress(-100, new string[] { f_local, "*" }); // انتخاب کاربر
                        continue;
                    }
                    if (settings.warnin_larg_files && f_local_info.Length > 1024 * 1024 * 400)
                    {
                        progress(-100, new string[] { f_local, "!" }); // حجم بالا
                        continue;
                    }
                    string saved_file = null;
                    if (f_local_info.Length > 400)
                        if (F_local_last_tag.ContainsKey(f_local_rel))
                        {
                            var last = F_local_last_tag[f_local_rel];
                            if (last[1] == fuid(f_local_info)) // same unique ids means same content in files, but files with same content may have different unique ids
                                saved_file = last[0];
                        }
                    tictoc.toc_tic("search2");
                    if (saved_file == null)
                        foreach (var f_server in F_server_info)
                        {
                            if (abort) return;
                            if (CompareFiles(f_local_info, f_server))
                            {
                                saved_file = f_server.FullName;
                                break;
                            }
                        }
                    tictoc.toc_tic("copyif");
                    if (saved_file == null)
                    {
                        var add = "";
                        while (true)
                        {
                            saved_file = server_dir + "files\\" + time + add + "-" + Path.GetFileName(f_local);
                            if (!File.Exists(saved_file))
                                break;
                            add += "-";
                        }

                        File.Copy(f_local, saved_file);
                        F_server_info.Add(new FileInfo(saved_file));
                        bi.TotalSizeAdded += f_local_info.Length;
                        bi.AddedFilesCount++;

                    }
                    tictoc.toc_tic("log");
                    log_file.WriteLine(f_local_rel + "|"
                        + saved_file.Replace(server_dir + "files\\", "") + "|"
                        + fuid(f_local_info));
                    bi.FilesCount++;
                    bi.TotalSize += f_local_info.Length;
                    tictoc.toc();
                }
                catch (Exception ex) { progress(-100, new string[] { f_local, "**" + ex.Message }); }
            }
        }
        public void Restore(BackupInfo backup, string local_dir)
        {
            try
            {
                OpenBackUpDrive();
                abort = false;
                if (progress != null) progress(0, "بازگردانی ...");
                if (!local_dir.EndsWith("\\")) local_dir += "\\";
                if (!Directory.Exists(local_dir)) Directory.CreateDirectory(local_dir);
                var server_dir = Path.GetDirectoryName(backup.LogFile) + "\\files\\";
                var F = File.ReadAllLines(backup.LogFile);
                if (progress != null) progress(5, "بازگردانی ...");
                int ii = 0;
                foreach (var f in F)
                    if (f.Trim() != "")
                    {
                        if (progress != null) progress(5 + 94.0 * ii++ / F.Length, "بازگردانی ...");
                        var A = f.Split(new char[] { '|' });
                        var f_local = local_dir + A[0];
                        var f_server = server_dir + A[1];
                        var dir = Path.GetDirectoryName(f_local);
                        if (!Directory.Exists(dir))
                            Directory.CreateDirectory(dir);
                        if (!File.Exists(f_server))
                            progress(-100, new string[] { f_local, f_server });
                        else
                            File.Copy(f_server, f_local, true);
                    }
            }
            finally
            { CloseBackUpDrive(); }
        }
        public int Delete(string backup_info_file)
        {
            try
            {
                OpenBackUpDrive();
                if (Path.GetDirectoryName(backup_info_file).ToLower() + "\\" != this.server_dir.ToLower())
                    throw new Exception("Can't delete backup from other servers");
                var backup = new BackupInfo(backup_info_file);
                var server_dir = this.server_dir + "\\files\\";
                //var time = Utils.DateTimeString2(backup.time);
                var active_links = new List<string>();
                var B = GetBackUpList();
                foreach (var b in B)
                {
                    if (b.LogFile.ToLower() != backup.LogFile.ToLower())
                    {
                        var F = File.ReadAllLines(b.LogFile);
                        foreach (var f in F)
                            if (f.Trim() != "")
                            {
                                var A = f.Split(new char[] { '|' });
                                var f_server = (server_dir + A[1]).ToLower();
                                if (!active_links.Contains(f_server))
                                    active_links.Add(f_server);
                            }
                    }
                }
                var res = 0;
                {
                    var F = File.ReadAllLines(backup.LogFile);
                    foreach (var f in F)
                        if (f.Trim() != "")
                        {
                            var A = f.Split(new char[] { '|' });
                            var f_server = (server_dir + A[1]).ToLower();
                            if (!active_links.Contains(f_server))
                            { File.Delete(f_server); res++; }
                        }
                }
                File.Delete(backup.LogFile);
                File.Delete(backup.path);
                return res;
            }
            finally
            { CloseBackUpDrive(); }
        }
        public List<BackupInfo> GetBackUpList(bool allusers = false)
        {
            try
            {
                OpenBackUpDrive();
                var res = new List<BackupInfo>();
                var user = this.user;
                if (user != "") user = " " + user;
                if (allusers) user = "";
                var F = Directory.GetFiles(server_dir, "*" + user + ".inf", SearchOption.TopDirectoryOnly);
                foreach (var f in F)
                    res.Add(new BackupInfo(f));
                return res;
            }
            catch { }
            finally
            {
                CloseBackUpDrive();
            }
            return new List<BackupInfo>();
        }
        public void CloseOpennedArchives()
        {
            var tc = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\TrueCrypt\\TrueCrypt.exe";
            foreach (var letter in OpennedArchives) try
                {
                    Process.Start(tc, "/d " + letter + " /f /s /h n /quit");
                }
                catch { }
        }
        List<string> OpennedArchives = new List<string>();
        /// <summary>
        /// a file unique id : LastWriteTime,CreationTime,Extension,Length
        /// same unique ids means same content in files, but files with same content may have different unique ids
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        static string fuid(FileInfo f)
        {
            return Utils.t2i(f.LastWriteTime) + "" + Utils.t2i(f.CreationTime) + f.Extension.ToLower() + f.Length;
        }
        public struct Settings
        {
            internal List<string> ignor_files;
            internal bool ignor_baktmpasv;
            internal bool ignor_obj;
            internal bool ignor_rst;
            internal bool warnin_larg_files;
        }
        public class BackupInfo
        {
            public BackupInfo()
            {
            }
            public BackupInfo(string fileName)
            {
                Load(fileName);
            }
            public void SaveAgain()
            {
                Save(path);
            }
            public void Save(string fileName)
            {
                InfoFile = InfoFile ?? new IniFile();
                InfoSection = InfoFile.AddSection("info");
                ProjectSection = InfoFile.AddSection("project");
                InfoSection["time"] = "" + Utils.t2i(time);
                InfoSection["Date"] = "" + Utils.DateTimeString(time);
                InfoSection["FilesCount"] = "" + FilesCount;
                InfoSection["AddedFilesCount"] = "" + AddedFilesCount;
                InfoSection["TotalSize"] = "" + TotalSize;
                InfoSection["TotalSizeAdded"] = "" + TotalSizeAdded;
                InfoSection["LogFile"] = LogFile;
                InfoFile.Save(fileName);
                path = fileName;
            }
            public void Load(string fileName)
            {
                InfoFile = new IniFile();
                InfoFile.Load(fileName);
                InfoSection = InfoFile.AddSection("info");
                ProjectSection = InfoFile.AddSection("project");
                time = Utils.i2t("0" + InfoSection["time"]);
                FilesCount = Convert.ToInt32("0" + InfoSection["FilesCount"]);
                AddedFilesCount = Convert.ToInt32("0" + InfoSection["AddedFilesCount"]);
                TotalSize = Convert.ToInt64("0" + InfoSection["TotalSize"]);
                TotalSizeAdded = Convert.ToInt64("0" + InfoSection["TotalSizeAdded"]);
                LogFile = InfoSection["LogFile"] + "";
                if (!LogFile.Contains(":") && !LogFile.StartsWith("\\\\"))
                    LogFile = Path.GetDirectoryName(fileName) + "\\" + LogFile;
                path = fileName;
            }
            public override string ToString()
            {
                return Utils.DateTimeString(time);
            }
            public DateTime time;
            public string LogFile = "";
            public string path = "";
            public IniFile InfoFile;
            public IniFile.IniSection InfoSection;
            public IniFile.IniSection ProjectSection;
            public int FilesCount = 0;
            public int AddedFilesCount = 0;
            /// <summary>
            /// bytes
            /// </summary>
            public long TotalSize = 0, TotalSizeAdded = 0;
        }
        public static bool CompareFiles(string first, string second)
        {
            return CompareFiles(new FileInfo(first), new FileInfo(second));
        }
        public static bool CompareFiles(FileInfo first, string second)
        {
            return CompareFiles(first, new FileInfo(second));
        }
        static bool CompareFiles(FileInfo first, FileInfo second)
        {
            var BYTES_TO_READ = sizeof(Int64);
            if (first.Length != second.Length)
                return false;

            int iterations = (int)Math.Ceiling((double)first.Length / BYTES_TO_READ);

            using (FileStream fs1 = first.OpenRead())
            using (FileStream fs2 = second.OpenRead())
            {
                byte[] one = new byte[BYTES_TO_READ];
                byte[] two = new byte[BYTES_TO_READ];

                for (int i = 0; i < iterations; i++)
                {
                    fs1.Read(one, 0, BYTES_TO_READ);
                    fs2.Read(two, 0, BYTES_TO_READ);

                    if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
                        return false;
                }
            }
            return true;
        }
        public delegate object Progress(double p, object state);

        /// <summary>
        /// drive leter: C, D, B , ...
        /// </summary>
        string drive = "";
        System.Windows.Forms.Timer CloseBackUpDrive_timer = null;
        internal void OpenBackUpDrive()
        {
            if (!NetworkMode) return;
            if (CloseBackUpDrive_timer != null) CloseBackUpDrive_timer.Stop();
            if (drive != "")
                try
                {
                    if (Directory.Exists(drive + ":\\"))
                        return;
                }
                catch { }
            drive = Utils.GetFreeDriveLeter();
            var res = WebServer.Default.SendData("backup_proj", "act", "start_share", "project", project_code, "proj_name", project_name);
            if (res.TrimStart().StartsWith("err:"))
                throw new Exception(res.Replace("err:","خطا:"));
            var V = res.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var host = NetworkPath;
            var path = V[0];
            var user = V[1];
            var pass = V[2];
            var result = CMD.RunBatString(
   $@"
cmdkey /add:{ host } /user:{ user } /pass:{ pass } 
net use {drive}: \\{ host }\{path} /p:no
cmdkey /add:{ host } /user:net /pass:123net
", read_result: true);
            if (result.Contains("[Error]"))
            {
                drive = "";
                throw new Exception(result.Split(new string[] { "[Error]" }, StringSplitOptions.None)[1]);
            }
        }
        internal void CloseBackUpDrive(int delay = 5 * 60 * 1000)
        {
            if (drive != "")
                try
                {
                    if (CloseBackUpDrive_timer != null) CloseBackUpDrive_timer.Stop();
                    CloseBackUpDrive_timer = new System.Windows.Forms.Timer();
                    CloseBackUpDrive_timer.Interval = delay + 1;
                    CloseBackUpDrive_timer.Tick += (ss, ee) =>
                    {
                        try
                        {
                            if (CloseBackUpDrive_timer != null) CloseBackUpDrive_timer.Stop();
                            CMD.RunBatString($"net use {drive}: /delete /Y ");
                            drive = "";
                            WebServer.Default.SendData("backup_proj", "act", "stop_share", "project", project_code);
                        }
                        catch { }
                    };
                    CloseBackUpDrive_timer.Start();
                }
                catch { }
        }
        ~MRB_Git()
        {
            CloseBackUpDrive(0);
        }

        void IDisposable.Dispose()
        {
            CloseBackUpDrive(0);
        }
    }
}
