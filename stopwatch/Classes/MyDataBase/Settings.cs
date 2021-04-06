using System;
using System.Collections.Generic;
using System.Text;

namespace stopwatch
{

    [Serializable]
    public class Settings
    {
        public string host = @"192.168.1.1:90";
        public string host_network
        {
            get
            {
                return "\\\\" + host.Replace("http://", "").Replace("https://", "").Split(new char[] { ':', '/' }, StringSplitOptions.RemoveEmptyEntries)[0] + "\\";
            }
        } 

        public string BackUpServer = "I:\\Projects-BackUp\\";

        internal string mahane_comment = "";

        public DateTime Entered = new DateTime(1000, 1, 1);
        public bool notification = true;
        public Skin skin = Skin.Standard;
        public bool BorhanBackUpShown = false;
    }
    [Serializable]
    public enum Skin { Standard, Silver, SilverGreen, Metal, MetalOrange, MetalGreen, Mac, Page, Dark }
}
