using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace stopwatch
{
    [Serializable]
    public class User
    {
        public string name;
        [Obsolete("use ID")]
        public string id;
        [Obsolete("use PASS")]
        public string pass;
#pragma warning disable 
        public string ID
        {
            get
            {
                try
                {
                    if (id == null)
                        return null;
                    if (!id.StartsWith("©"))
                        ID = id;
                    return Utils.MyUnHash(id.Substring(1));
                }
                catch { }
                return null;
            }
            set
            {
                id = "©" + Utils.MyHash(value);
            }
        }
        static string pass_chk = "©" + Utils.MyHash(Environment.MachineName) + "©";
        public string PASS
        {
            get
            {
                try
                {
                    if (pass != null && !pass.StartsWith("©"))
                        PASS = pass;
                    if (pass == null || !pass.StartsWith(pass_chk))
                        return "_wrong_system_";
                    return Utils.MyUnHash(pass.Substring(pass_chk.Length));
                }
                catch { }
                return null;
            }
            set
            {
                pass = "©" + Utils.MyHash(Environment.MachineName) + "©" + Utils.MyHash(value);
            }
        }
#pragma warning restore 
        internal static bool GetUserPassFromSite(ref string id, ref string pass)
        {
            try
            {
                if ((WebServer.Default.Check() + "").StartsWith("Error:"))
                    return false;
                var co = Utils.ReadFile(GetChromeCookiePath());
                var reg = new System.Text.RegularExpressions.Regex(@"_stop\*watch_(........)_stop\*watch_");//_stop*watch_code_stop*watch_
                var m = reg.Match(co);
                var code = m.Groups[1].Value;
                var D = WebServer.Default.SendData("sw/login", "act", "get_current_user", "app_code", code).Trim();
                var A = D.Split('\n');
                if (A[0] == "ok" && ((id + "").Trim().Length != 10 || A[1] == id))
                {
                    id = MyData.db.user.ID = A[1];
                    pass = MyData.db.user.PASS = Utils.MyUnHash(A[2]);
                    WebServer.Default.ResetCookie();

                    Check(id, pass, false);
                    Form1.mainForm.notifyIcon1.ShowBalloonTip(500, MyData.db.user.name, "شما وارد حساب کاربری خود شدید", System.Windows.Forms.ToolTipIcon.Info);
                    WebServer.Default.SendMessage(MyData.db.user.ID, "شما به طور اتوماتیک وارد حساب کاربری خود شدید، سیستم: " + Environment.MachineName + "-" + Environment.UserName);
                    try
                    {
                        foreach (var frm in Application.OpenForms)
                        {
                            if (frm is Forms.Form_user)
                            {
                                (frm as Forms.Form_user).textBox_id.Text = id;
                                (frm as Forms.Form_user).textBox_pass.Text = pass;
                                (frm as Forms.Form_user).Close_();
                            }
                        }
                    }
                    catch { }
                    return true;
                }
            }
            catch { }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pass"></param>
        /// <param name="name"></param>
        /// <returns>ok if ok, null if can not connect to server, a string if error</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string Check(string id, string pass, bool try_to_get_from_server = true)
        {
            if (try_to_get_from_server && (id == "" || pass == ""))
                GetUserPassFromSite(ref id, ref pass);

            if (id != "" && pass != "")
            {
                try
                { CheckNID(id); }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                try
                {
                    if ((WebServer.Default.Check() + "").StartsWith("Error:"))
                        return null;
                    var res = WebServer.Default.SendData("sw/login", "act", "check", "id", id, "pass", pass).Trim();
                    if (res.StartsWith("ok:"))
                    {
                        MyData.db.user.name = res.Substring(3);
                        MyData.db.user.ID = id;
                        MyData.db.user.PASS = pass;
                        foreach (var frm in Application.OpenForms)
                        {
                            if (frm is Form_settings)
                                (frm as Form_settings).SetName(MyData.db.user.name);
                        }
                        return "ok";
                    }
                    else
                    {
                        if (try_to_get_from_server)
                            if (GetUserPassFromSite(ref id, ref pass))
                                return Check(id, pass, false);

                        if (res.Length > 200) res = res.Substring(0, 200);
                        return res;
                    }
                }
                catch { return null; }
            }
            return " کد و رمز نباید خالی باشد";
        }
        public static string CheckNID(string inputID)
        {
            return inputID;
            /*if (inputID.Length != 10)
                throw new Exception("کد ملی صحیح نیست");
            else
            {
                int all;
                int end = Convert.ToInt16(inputID[9].ToString());

                all = Convert.ToInt16(inputID[0] + "") * 10 + Convert.ToInt16(inputID[1] + "") * 9 + Convert.ToInt16(inputID[2] + "") * 8 + Convert.ToInt16(inputID[3] + "") * 7 + Convert.ToInt16(inputID[4] + "") * 6 + Convert.ToInt16(inputID[5] + "") * 5 + Convert.ToInt16(inputID[6] + "") * 4 + Convert.ToInt16(inputID[7] + "") * 3 + Convert.ToInt16(inputID[8] + "") * 2;
                all = all % 11;
                if (all >= 2)
                    all = 11 - all;
                if (end != all)
                    throw new Exception("کد ملی صحیح نیست");
                else
                    return inputID;
            }*/
        }
        public string last_hid = Utils.GetHID();
        public AccessLevel access_level;
        public List<string> AccessToProjects;

        private static string GetChromeCookiePath()
        {
            string s = Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData);
            s += @"\Google\Chrome\User Data\Default\cookies";

            if (!File.Exists(s))
                return string.Empty;

            return s;
        }

    }
}
