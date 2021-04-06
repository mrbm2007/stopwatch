using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace stopwatch
{

    public class WebServer
    {
        public static bool Offline = false;
        public static WebServer Default = new WebServer();
        public string name
        {
            get
            {
                return MyData.db.user.name;
            }
        }
        public string pass
        {
            get
            {
                try
                {
                    if (MyData.db?.user?.PASS != null && MyData.db.user.PASS.Length != 0 && MyData.db.user.PASS.Length != 32)
                        MyData.db.user.PASS = Utils.md5(MyData.db.user.PASS);
                }
                catch { }
                return MyData.db.user.PASS;
            }
        }
        public string id
        {
            get
            {
                return MyData.db.user.ID;
            }
        }
        /// <summary>
        ///  ends with /
        ///  as: http://borhan:90/
        /// </summary>
        public string server
        {
            get
            {
                if (MyData.db.settings.host + "" == "192.168.1.1:90") MyData.db.settings.host = "borhan";
                if (MyData.db.settings.host + "" == "borhan:90") MyData.db.settings.host = "borhan";
                if (MyData.db.settings.host + "" == "") MyData.db.settings.host = "borhan";
                if (MyData.db.settings.host + "" == "borhan") MyData.db.settings.host = "www.fater.info";

                var A = MyData.db.settings.host
                   .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var a = A[0];
                if (!a.EndsWith("/")) a = a + "/";
                if (!a.StartsWith("http://") && !a.StartsWith("https://")) a = "http://" + a;
                return a;
            }
        }
        public bool IsUserDefined()
        {
            return pass + "" != "" && name + "" != "" && (id + "").Trim().Length > 4;
        }

        NameValueCollection PrepareParams(params string[] data)
        {
            var has_id = false;
            var values = new NameValueCollection();
            var all = "";
            for (int i = 0; i < data.Length - 1; i += 2)
            {
                if (data[i] == "id")
                    has_id = true;
                values.Add(data[i], data[i + 1]);
                all += data[i + 1];
            }
            if (!has_id)
            {
                values.Add("name", name);
                values.Add("pass", pass);
                values.Add("id", id);
            }
            return values;
        }
        public CookieContainer cookie = new CookieContainer();
        public void ResetCookie()
        {
            cookie = new CookieContainer();
        }
        public class WebClientEx : WebClient
        {
            public WebClientEx(CookieContainer container)
            {
                this.container = container;
            }

            public CookieContainer CookieContainer
            {
                get { return container; }
                set { container = value; }
            }

            private CookieContainer container = new CookieContainer();

            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest r = base.GetWebRequest(address);
                var request = r as HttpWebRequest;
                if (request != null)
                {
                    request.CookieContainer = container;
                }
                return r;
            }

            protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
            {
                WebResponse response = base.GetWebResponse(request, result);
                ReadCookies(response);
                return response;
            }

            protected override WebResponse GetWebResponse(WebRequest request)
            {
                WebResponse response = base.GetWebResponse(request);
                ReadCookies(response);
                return response;
            }

            private void ReadCookies(WebResponse r)
            {
                var response = r as HttpWebResponse;
                if (response != null)
                {
                    CookieCollection cookies = response.Cookies;
                    container.Add(cookies);
                }
            }
        }
        public string SendData(string page, params string[] data)
        {
            if (Offline) return "";
            lock (cookie)
            {
                try
                {
                    if (!page.Contains("/")) page = "sw/" + page;
                    var values = PrepareParams(data);
                    using (var wb = new WebClientEx(cookie))
                    {
                        var res = Encoding.UTF8.GetString(wb.UploadValues(server + page, "POST", values));
                        if (res.StartsWith("err:"))
                            throw new Exception("خطا در سرور: \r\n" + res.Replace("err:", ""));
                        return res;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;// new Exception(server + page, ex);
                }
            }
        }

        public void DownloadFile(string page, string filepath, params string[] data)
        {
            if (Offline) return;
            lock (cookie)
            {
                if (!page.Contains("/")) page = "sw/" + page;
                var values = PrepareParams(data);
                using (var wb = new WebClientEx(cookie))
                {
                    var res_b = wb.UploadValues(server + page, "POST", values);
                    if (res_b.Length < 1500)
                    {
                        var res = Encoding.UTF8.GetString(res_b);
                        if (res.StartsWith("err:"))
                            throw new Exception("Error in server: " + res);
                    }
                    File.WriteAllBytes(filepath, res_b);
                }
            }
        }
        public string SendFile(string page, string file, params string[] data)
        {
            if (Offline) return "";
            if (!page.Contains("/")) page = "sw/" + page;
            var url = server + page;
            var values = PrepareParams(data);
            var request = WebRequest.Create(url);
            request.Method = "POST";
            var boundary = "---------------------------" + DateTime.Now.Ticks.ToString();
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            boundary = "--" + boundary;

            using (var requestStream = request.GetRequestStream())
            {
                // Write the values
                foreach (string name in values.Keys)
                {
                    var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.ASCII.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", name, Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes(values[name] + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                }

                // Write the files
                {
                    var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes("Content-Disposition: form-data; name=\"" + Path.GetFileName(file) + "\"; filename=\"" + file + "\"\r\n");
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.ASCII.GetBytes("Content-Type: application/octet-stream\r\n\r\n");
                    requestStream.Write(buffer, 0, buffer.Length);
                    var B = File.ReadAllBytes(file);
                    requestStream.Write(B, 0, B.Length);
                    buffer = Encoding.ASCII.GetBytes(Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                }

                var boundaryBuffer = Encoding.ASCII.GetBytes(boundary + "--");
                requestStream.Write(boundaryBuffer, 0, boundaryBuffer.Length);
            }

            using (var response = request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            {
                using (var stream = new StreamReader(responseStream))
                {
                    var res = stream.ReadToEnd();
                    if (res.StartsWith("err:"))
                        throw new Exception("Error in server: " + res);
                    return res;
                }
            }
        }

        internal DateTime last_check = DateTime.Now.AddDays(-1);
        string last_check_res = "";

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string Check(int timeout = 2000, bool check_IsUserDefined = true)
        {
            lock (WebServer.Default)
            {
                //if (check_IsUserDefined && !IsUserDefined()) return "Error: no user name or pass!";
                if ((DateTime.Now - last_check).TotalSeconds < 15)
                    return last_check_res;
                try
                {
                    var wb = HttpWebRequest.Create(server + "sw/check");
                    wb.Timeout = timeout;
                    using (var sr = new StreamReader(wb.GetResponse().GetResponseStream()))
                        return last_check_res = sr.ReadToEnd().Trim();
                }
                catch (Exception ex)
                {
                    return last_check_res = "Error: " + ex.Message;
                }
                finally
                {
                    last_check = DateTime.Now;
                }
            }
        }
        public static bool Check(string server, int timeout = 2000)
        {
            try
            {
                var wb = HttpWebRequest.Create(server + "sw/check");
                wb.Timeout = timeout;
                using (var sr = new StreamReader(wb.GetResponse().GetResponseStream()))
                    return "ok" == sr.ReadToEnd().Trim();
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool IsConnected
        {
            get
            {
                var res = Check() == "ok";
                if (!res)
                    AutoFindServer();
                return res;
            }
        }

        #region users
        public IniFile GetUserList()
        {
            if (!IsConnected) throw new Exception("Server not available : " + server);
            var res = SendData("users", "act", "list");
            return new IniFile().LoadString(res);
        }
        public IniFile GetUserInfo(string name)
        {
            if (!IsConnected) throw new Exception("Server not available : " + server);
            var res = SendData("users", "act", "list", "getuserinfo", name);
            return new IniFile().LoadString(res);
        }
        public bool SetUserInfo(IniFile user)
        {
            try
            {
                if (!IsConnected) return false;
                if (name == "") return false;
                var info_ = user.SaveString();
                var res = SendData("users", "act", "setinfo", "info", info_);
                try
                {
                    var info = new IniFile().LoadString(res).GetSection(0);
                    MyData.db.user.access_level = (AccessLevel)Enum.Parse(typeof(AccessLevel), info["level"]);
                    var _AccessToProjects = new List<string>();
                    var A = info["AccessToProjects"].Split(new char[] { '\r', '\n', ';', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    _AccessToProjects.AddRange(A);
                    MyData.db.user.AccessToProjects = _AccessToProjects;
                }
                catch { }
                return true;
            }
            catch { return false; }
        }
        public bool SetState(string user, string state)
        {
            try
            {
                if (IsUserDefined())
                {
                    var res = SendData("users", "act", "setstate", "user", user, "state", state);
                    return true;
                }
            }
            catch { }
            return false;
        }


        #endregion

        #region backup
        public MyData GetBackUp(string user)
        {
            var tmp = Path.GetTempFileName();
            try
            {
                DownloadFile("backup", tmp, "act", "get", "user", user);
                var db = new MyData();
                db.Load(tmp);
                return db;
            }
            catch
            {
                return null;
            }
            finally
            {
                try
                {
                    File.Delete(tmp);
                }
                catch { }
            }
        }
        public bool SendBackUp(MyData db)
        {
            try
            {
                if (!IsConnected) return false;
            }
            catch { return false; }
            var tmp = Path.GetTempFileName();
            try
            {
                if (db == null)
                    SendData("backup", "act", "set").Trim().StartsWith("err");
                else
                {
                    db.Save(tmp, compressed: true);
                    SendFile("backup", tmp, "act", "set").Trim();
                }
                return true;
            }
            catch { return false; }
            finally
            {
                try { File.Delete(tmp); } catch { }
            }
        }
        #endregion

        #region update
        DateTime last_check_borhansoft = DateTime.Now.AddDays(-10);
        public int GetLastVersionInfo()
        {
            try
            {
                if (!IsConnected)
                {
                    if ((DateTime.Now - last_check_borhansoft).TotalMinutes > 15)
                        using (var wc = new WebClient())
                        {
                            last_check_borhansoft = DateTime.Now;
                            return -Convert.ToInt32(wc.DownloadString("http://ae.sharif.edu/~borhanpanah/sw/ver.txt?" + DateTime.Now.Ticks).Replace(" ", ""));
                        }
                    return 0;
                }
                return Convert.ToInt32(SendData("update", "act", "check"));
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return 0;
        }
        public string GetUpdateSetup(int ver, int old_ver)
        {
            var ver_ = Utils.Int2Version(Math.Abs(ver));
            if (ver < 0)
            {
                Process.Start("http://ae.sharif.edu/~borhanpanah/sw/StopWatch-" + ver_ + ".exe");
                return "";
                /*using (var wb = new WebClient())
                {
                    var f = Path.GetTempPath() + "\\" + "stopwatch-" + ver_ + ".exe";
                    wb.DownloadFile("http://ae.sharif.edu/~borhanpanah/sw/StopWatch-" + ver_ + ".exe", f);
                    return f;
                }*/
            }
            else
                return GetUpdateSetup(ver_, Utils.Int2Version(old_ver));
        }
        public string GetUpdateSetup(string ver, string old_ver)
        {
            try
            {
                var f = Path.GetTempPath() + "\\" + "stopwatch-" + ver + ".exe";
                DownloadFile("update", f, "act", "get", "ver", ver, "old_ver", old_ver);
                return f;
            }
            catch { }
            return "";
        }
        #endregion

        #region time list
        public bool SendTimeList(string fileName, DateTime date)
        {
            return SendFile("timelist", fileName, "act", "save", "date", Utils.t2i(date) + "").StartsWith("ok");
        }
        #endregion

        #region chat
        DateTime chat_reset_last_time = DateTime.Now.AddYears(-10);
        DateTime chat_last_time = DateTime.Now.AddYears(-10);
        Dictionary<string, int> chat_last_id = new Dictionary<string, int>();
        public string[] ChatCheck()
        {
            try
            {
                if ((DateTime.Now - chat_reset_last_time).TotalMinutes > 150)
                {
                    chat_last_time = DateTime.Now.AddYears(-10);
                    chat_last_id = new Dictionary<string, int>();
                    chat_reset_last_time = DateTime.Now;
                }
                if (!IsConnected) return new string[0];
                if (IsUserDefined())
                {
                    var res = SendData("chat", "act", "check", "chat_last_time", chat_last_time.Ticks + "").Trim().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    chat_last_time = DateTime.Now;
                    return res;
                }
            }
            catch { }
            return new string[0];
        }
        public void SendMessage(string to, string msg, string file = null)
        {
            var res = "";
            if (file != null)
                res = SendFile("chat", file, "act", "send", "to", to, "msg", msg);
            else
                res = SendData("chat", "act", "send", "to", to, "msg", msg);
        }
        public IniFile GetMessages(string to, IniFile ini = null)
        {
            if (ini == null) ini = new IniFile();
            ini.changed = false;
            var res = SendData("chat", "act", "get", "to", to, "msg_id", ini.Sections.Count > 0 ? ini.GetSection(ini.Sections.Count - 1).Name : "");
            ini.LoadString(res, true);
            return ini;
        }

        public IniFile GetNewMessages(string to)
        {
            var ini = new IniFile();
            ini.changed = false;
            if (!chat_last_id.ContainsKey(to))
                chat_last_id[to] = -1;
            var t = chat_last_id[to];
            var res = SendData("chat", "act", "get", "to", to, "id_old", t + "", "unreads", "1");
            ini.LoadString(res);
            foreach (IniFile.IniSection s in ini.Sections)
                try
                {
                    if (Convert.ToInt32(s["UniqueID"] + "") > chat_last_id[to])
                        chat_last_id[to] = Convert.ToInt32(s["UniqueID"] + "");
                }
                catch { }
            return ini;
        }
        public void GetMessageFile(string message_id, string local_filepath)
        {
            if (local_filepath + "" == "") return;
            var local_file_tmp = Path.GetTempFileName();

            DownloadFile("chat", local_file_tmp, "act", "getfile", "id", message_id);

            File.Copy(local_file_tmp, local_filepath, true);

            if (File.Exists(local_filepath))
            {
                SendData("chat", "act", "getfile_done", "id", message_id);
            }
            return;
        }
        public bool DidChat(string to)
        {
            if (IsUserDefined())
                return SendData("chat", "act", "did", "to", to) == "1";
            return false;
        }
        public Image GetUserPic(string name)
        {
            if (!IsConnected) throw new Exception("Server not available : " + server);
            var file = Path.GetTempPath() + "\\" + name + ".jpg";
            try
            {
                DownloadFile("chat", file, "act", "get_pic", "user", name);
                var img = new Bitmap(file);
                return img;
            }
            finally
            {
                try
                {
                    File.Delete(file);
                }
                catch { }
            }
        }
        #endregion

        #region commands
        public IniFile GetCommands(bool all)
        {
            if (IsUserDefined())
            {
                var ini = new IniFile();
                ini.LoadString(SendData("commands", "act", all ? "get_all" : "get"));
                return ini;
            }
            return new IniFile();
        }

        public IniFile GetProjList()
        {
            var i = new IniFile();
            if (IsUserDefined())
            {
                var res = SendData("action", "act", "projlist");
                if (res.StartsWith("ok:"))
                {
                    res = res.Substring(3);
                    i.LoadString(res);
                }
            }
            return i;
        }

        public void SetCommandsRes(string command, string user, string res, bool done, bool all)
        {
            SendData("commands", "act", all ? "set_all" : "set", "result", IniFile.Base64Encode(res), "done", done ? "1" : "0", "command", command, "user", user);
        }
        #endregion


        public class ServerInfo
        {
            public string protocol = "http";
            public string IP = null;
            public string domain = null;
            public bool SaveInHosts
            {
                get
                {
                    if (domain != null && IP != null)
                        if (!Check(address_Domain))
                            return true;
                    return false;
                }
            }
            public string address_IP => $"{protocol}://{IP}/";
            public string address_Domain => $"{protocol}://www.{domain}/";
            public string server_address
            {
                get
                {
                    if (IP != null)
                        return address_IP;
                    return address_Domain;
                }
            }
        }
        static DateTime Last_AutoFindServer = DateTime.Now.AddDays(-10);
        public static void AutoFindServer()
        {
            try
            {
                System.Threading.ThreadPool.QueueUserWorkItem((x) =>
                {
                    lock (Default)
                    {
                        if ((DateTime.Now - Last_AutoFindServer).TotalHours < 15) return;
                        var ServerList = new List<ServerInfo>
                    {
                        new  ServerInfo { domain="192.168.88.220"   },
                        new  ServerInfo { IP="172.31.128.97", domain="toba.ir"   },
                        new  ServerInfo { IP="192.168.88.220", domain="toba.ir"   },
                        //new  ServerInfo { domain="fater.info" },
                        new  ServerInfo { IP="192.168.1.1", domain="fater.info"   },
                        new  ServerInfo { domain="borhan:90"   },
                        new  ServerInfo { domain="localhost"   },
                        new  ServerInfo { domain="localhost:90"   },
                    };
                        foreach (var si in ServerList)
                            if (Check(si.server_address))
                            {
                                MyData.db.settings.host = si.server_address;
                                if (si.SaveInHosts)
                                {
                                    if (Form_msg.Show(null, "تنظیمات سرور برای دسترسی با نام زیر اعمال شود؟" + "\r\n" + si.domain, btn: System.Windows.Forms.MessageBoxButtons.YesNo)
                                        == System.Windows.Forms.DialogResult.Yes)
                                        Program.StartAsAdmin($"set_host {si.IP} {si.domain}");
                                    else
                                        MyData.db.settings.host = si.address_IP;
                                }
                                break;
                            }
                        Last_AutoFindServer = DateTime.Now;
                    }
                });
            }
            catch { }
        }
    }
}
