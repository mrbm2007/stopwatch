using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace stopwatch
{
    public class CMD
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="visible"></param>
        /// <param name="wait_for_result"></param>
        /// <param name="timeout">in ms, when wait_for_result is true</param>
        /// <returns></returns>
        public static string Run(string command,
            string user = null,
            string password = null,
            bool visible = true,
            bool wait_for_result = true, int timeout = 10 * 1000)
        {
            command = command.Trim();
            var args = "";
            var p1 = command.IndexOf(" ");
            if (p1 > 0)
            {
                args = command.Substring(p1 + 1);
                command = command.Substring(0, p1);
            }
            var p = new Process();
            p.StartInfo.FileName = command;
            p.StartInfo.Arguments = args;
            if (wait_for_result)
            {
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
            }
            else
            {
                p.StartInfo.WindowStyle = visible ? ProcessWindowStyle.Normal : ProcessWindowStyle.Hidden;
            }
            if (user != null)
            {
                p.StartInfo.UserName = user;
                var ssPwd = new System.Security.SecureString();
                for (int x = 0; x < password.Length; x++)
                    ssPwd.AppendChar(password[x]);
                p.StartInfo.Password = ssPwd;
            }
            p.Start();
            if (!wait_for_result) return "";
            for (int i = 0; i < timeout / 100; i++)
            {
                if (p.HasExited)
                    return p.StandardOutput.ReadToEnd() + p.StandardError.ReadToEnd();
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(100);
            }
            return "!!!  time-out  !!!\r\n" + p.StandardOutput.ReadToEnd() + p.StandardError.ReadToEnd();
        }
        public static string UserCode
        {
            get
            {
                return (MyData.db.user.ID + "@" + Environment.MachineName).Replace(" ", "_").Replace("\t", "_").Replace("=", "_").Replace(",", "_");
            }
        }
        public static int CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target, string pattern = null, long size_less_than = long.MaxValue)
        {
            Regex reg = null;
            if (pattern + "" != "")
                reg = new Regex(pattern
                            .Replace("\\", "\\\\")
                            .Replace(".", "\\.")
                            .Replace("(", "\\(")
                            .Replace(")", "\\)")
                            .Replace("*", ".*")
                            .Replace("?", "."));
            var errors = 0;
            foreach (DirectoryInfo dir in source.GetDirectories())
                errors += CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            foreach (FileInfo file in source.GetFiles())
                try
                {
                    if (reg == null || reg.IsMatch(file.FullName))
                        if (file.Length < size_less_than)
                            file.CopyTo(Path.Combine(target.FullName, file.Name), true);
                }
                catch
                {
                    errors++;
                }
            return errors;
        }
        public delegate void SendResultEvent(string result, bool done);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="user">for 'all' mode commands</param>
        public static void Run(IniFile.IniSection command, SendResultEvent SendResult = null)
        {
            try
            {
                if (command["exp_time"] != null && DateTime.Now > Utils.PersianParse_DateTime(command["exp_time"]))
                {
                    SendResult?.Invoke("! expired !", true);
                    return;
                }
                else if (command["start_time"] != null && DateTime.Now < Utils.PersianParse_DateTime(command["start_time"]))
                {
                    SendResult?.Invoke("! not yet !", false);
                    return;
                }
                else if (command["action", "act"] + "" != "")
                {
                    var act = command["action", "act"].Trim().ToLower();

                    if (act == "server" && command["server"] != null)
                        MyData.db.settings.host = command["server"].Trim();
                    else if (act == "update")
                        Form1.mainForm.CheckUpdate(command["force"] != null);
                    else if (act == "command_interval")
                        Form1.mainForm.get_command_interval = Convert.ToInt32(command["interval"]);
                    else if (act == "backup")
                        Form1.mainForm.BackUp();

                    else if (act == "copy")
                    {
                        var from = command["from", "source"];
                        var to = command["to", "dist", "destination"];
                        if (Directory.Exists(from))
                        {
                            var max_size = long.MaxValue;
                            try
                            {
                                max_size = Convert.ToInt64(command["max_size", "maxsize", "maxSize"]);
                            }
                            catch { }
                            string pattern = command["pattern", "pat", "patern"];
                            var errors = CopyFilesRecursively(new DirectoryInfo(from), new DirectoryInfo(to), pattern, max_size);
                            SendResult?.Invoke("Directory copied" + (errors > 0 ? errors + " Errors" : ""), true);
                        }
                        else if (File.Exists(from))
                        {
                            Directory.CreateDirectory(to);
                            File.Copy(from, to + "\\" + Path.GetFileName(command["from"]));
                            SendResult?.Invoke("File copied", true);
                        }
                        else
                            SendResult?.Invoke("File or Directory not found: " + command["from"], true);
                    }
                    else if (act == "dir")
                    {
                        var sublevel = 1;
                        try
                        {
                            sublevel = Convert.ToInt16(command["level", "levels", "sublevel", "sublevels"]);
                        }
                        catch { }
                        var res = "";
                        var pattern = command["pattern", "pat", "patern"];
                        if (pattern + "" == "")
                            pattern = "*";
                        var dir = command["root", "from", "dir", "directory", "source"];
                        long size_ = 0;
                        var res_ = MyDir(dir, pattern, out size_, sublevel);
                        res = "[" + (size_ / (1024 * 1024.0)).ToString("0.0") + " MB]     Dir of " + dir + "\r\n" + res_;
                        SendResult?.Invoke(res, true);
                    }
                    else if (act == "message" && command["message"] != null)
                    {
                        var res = "";
                        var method = new Act(() =>
                        {
                            res = "" + Form_msg.Show(null, command["message"].Trim(),
                               command["title"] ?? "StopWatch",
                               icon: System.Windows.Forms.MessageBoxIcon.Information,
                               btn: command["button"] + "" == "yesno" ? System.Windows.Forms.MessageBoxButtons.YesNo : System.Windows.Forms.MessageBoxButtons.OK);
                        });
                        if (Form1.mainForm.InvokeRequired)
                            Form1.mainForm.Invoke(method);
                        else
                            method();
                        SendResult?.Invoke(res, true);
                    }

                    MyData.db.Save(Form1.DataFile);
                }
                else if (command["cmd"] + "" != "")
                {
                    var res = CMD.Run(
                        "cmd /c \"" + command["cmd"] + "\"",
                        command["user"],
                        command["pass"],
                        (command["visible"] + "").Trim().ToLower() == "true",
                        (command["wait"] + "true").Trim().ToLower().StartsWith("true")
                        );
                    SendResult?.Invoke(res, true);
                }
                else
                {
                    var res = CMD.Run(
                        command["run"] + "",
                        command["user"],
                        command["pass"],
                        (command["visible"] + "").Trim().ToLower() == "true",
                        (command["wait"] + "true").Trim().ToLower().StartsWith("true")
                        );
                    SendResult?.Invoke(res, true);
                }
            }
            catch (Exception ex)
            {
                SendResult?.Invoke("Error: " + ex.Message, false);
            }
            SendResult?.Invoke("", true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="pattern"></param>
        /// <param name="size">in bytes</param>
        /// <param name="sublevels"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        static string MyDir(string root, string pattern, out long size, int sublevels, int level = 0)
        {
            var res = "";
            size = 0;
            var D = Directory.GetDirectories(root, "*", SearchOption.TopDirectoryOnly);
            var F = Directory.GetFiles(root, pattern, SearchOption.TopDirectoryOnly);
            foreach (var d in D)
                try
                {
                    var res_ = "";
                    long size_ = 0;
                    if (sublevels > 1)
                        res_ = MyDir(d, pattern, out size_, sublevels - 1, level + 1);
                    size += size_;
                    res += ("[" + (size_ / (1024 * 1024.0)).ToString("0.00") + "]    ").PadRight(13) + "".PadRight(level * 4) + ">".PadRight((level), '.') + Path.GetFileName(d);
                    res += "\r\n" + res_ + "\r\n<";
                }
                catch { }
            foreach (var f3 in F)
            {
                var size_ = new FileInfo(f3).Length;
                size += size_;
                res += ("{" + (size_ / (1024 * 1024.0)).ToString("0.00") + "}    ").PadRight(13) + "".PadRight(level * 4) + Path.GetFileName(f3) + "\r\n";
            }
            return res;
        }
        public static string RunBatString(string bat, bool visible = false, bool wait = false, bool read_result = false)
        {
            var tmp = (Path.GetTempPath() + "\\bat.bat").Replace("\\\\", "\\");
            var res = "";
            Process p;
            File.WriteAllText(tmp, bat + "\r\n" + (visible ? "pause" : $@"(goto) 2>null & del ""{tmp}"" /A") + "\r\n");
            if (read_result)
            {
                p = Process.Start(new ProcessStartInfo("cmd")
                {
                    WindowStyle = ProcessWindowStyle.Minimized,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                });
                p.StandardInput.WriteLine("@echo off");
                p.StandardInput.WriteLine(bat);
                p.StandardInput.Close();
                p.WaitForExit();
                res = p.StandardOutput.ReadToEnd();
                var err = p.StandardError.ReadToEnd().Trim();
                if (err != "")
                    res += "\r\n[Error]: " + err;

            }
            else
            {
                p = Process.Start(new ProcessStartInfo(tmp) { WindowStyle = visible ? ProcessWindowStyle.Normal : ProcessWindowStyle.Hidden });
                if (wait) p.WaitForExit();
            }
            return res;
        }
        public static List<string> ListUsers(string group = "", bool tolower = false)
        {
            var str = "";
            if (group.Trim() == "")
                str = Run("net user");
            else
                str = Run("net localgroup \"" + group + "\"");
            var p1 = str.IndexOf("---\r\n") + 5;
            var p2 = str.IndexOf("The command ");
            str = str.Substring(p1, p2 - p1);
            var res = new List<string>(str.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            res.Remove("Administrator");
            res.Remove("___VMware_Conv_SA___");
            res.Remove("UpdatusUser");
            res.Remove("Guest");
            if (tolower)
                for (int i = 0; i < res.Count; i++)
                    res[i] = res[i].ToLower();
            return res;
        }
        public static List<string> ListLoggedOnUsers()
        {
            var str = Run("query user");
            var L = str.Split(new char[] { '\n' });
            var res = new List<string>();
            foreach (var line in L)
                if (line.Trim().StartsWith(">"))
                    res.Add(line.Trim().Replace(">", "").Split(new char[] { ' ' })[0].ToLower());
            return res;
        }
    }
}
