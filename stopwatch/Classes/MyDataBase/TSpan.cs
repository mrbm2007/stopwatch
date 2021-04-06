using System;
using System.Collections.Generic;
using System.Text;

namespace stopwatch
{

    [Serializable]
    public class TSpan
    {
        public TSpan() { }
        public TSpan(string str)
        {
            try
            {
                var persianDateMatch = System.Text.RegularExpressions.Regex.Match(str, @"^(\d+):(\d+):(\d+)$");
                var hour = int.Parse(persianDateMatch.Groups[1].Value);
                var minute = int.Parse(persianDateMatch.Groups[2].Value);
                var secound = int.Parse(persianDateMatch.Groups[3].Value);
                sec = hour * 3600 + minute * 60 + secound;
            }
            catch
            {
                var persianDateMatch = System.Text.RegularExpressions.Regex.Match(str, @"^(\d+):(\d+)$");
                var hour = int.Parse(persianDateMatch.Groups[1].Value);
                var minute = int.Parse(persianDateMatch.Groups[2].Value);
                sec = hour * 3600 + minute * 60;
            }
        }
        public TSpan(int sec)
        {
            this.sec = sec;
        }
        /// <summary>
        /// total seconds
        /// </summary>
        public int sec = 0;
        public TSpan Add(TSpan d)
        {
            Add(d.sec);
            return this;
        }
        public void Add(int sec = 1)
        {
            this.sec += sec;
        }
        public int Hour { get { return sec / 3600; } }
        public int Minute { get { return (sec % 3600) / 60; } }
        public int Second { get { return ((sec % 3600) % 60); } }
        public int TotalSecond { get { return sec; } set { sec = value; } }
        public override string ToString()
        {
            return Hour.ToString("00") + ":" + Minute.ToString("00") + ":" + Second.ToString("00");
        }
        public string ToString(bool sec)
        {
            return Hour.ToString("00") + ":" + Minute.ToString("00") + (sec ? ":" + Second.ToString("00") : "");
        }
        public static explicit operator TSpan(string s)
        {
            return new TSpan(s);
        }

        public static TSpan operator +(TSpan a, TSpan b)
        {
            return new TSpan(a.sec + b.sec);
        }
    }
}
