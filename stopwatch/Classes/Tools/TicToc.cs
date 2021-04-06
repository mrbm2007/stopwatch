using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace stopwatch
{
    public class tictoc
    {
        public static bool Enabled = Utils.Debug;
        /// <summary>
        /// tag -> Stopwatch
        /// </summary>
        static Dictionary<string, Stopwatch> sw = new Dictionary<string, Stopwatch>();
        /// <summary>
        /// tag -> master's tag
        /// </summary>
        static Dictionary<string, string> masters = new Dictionary<string, string>();
        static Stack<string> last_tags = new Stack<string>();
        static string current_master = null;
        public static Stopwatch tic(string tag = "", bool IsMaster = false)
        {
            if (!Enabled) return null;
            last_tags.Push(tag);
            if (!sw.ContainsKey(tag))
            {
                sw[tag] = new Stopwatch();
                sw[tag].Reset();
            }
            if (IsMaster)
                current_master = tag;
            else if (current_master != null)
            {
                if (!masters.ContainsKey(tag))
                    masters[tag] = current_master;
            }
            sw[tag].Start();
            return sw[tag];
        }
        public static void Reset(string tag)
        {
            if (!Enabled) return;
            sw[tag].Reset();
        }
        public static double toc(string tag = null)
        {
            if (!Enabled) return 0;
            try
            {
                if (tag == null)
                    tag = last_tags.Pop();
                else
                    last_tags.Pop();
            }
            catch { tag = ""; }
            if (!sw.ContainsKey(tag)) return 0;
            sw[tag].Stop();
            if (current_master == tag) current_master = null;
            return sw[tag].ElapsedMilliseconds;
        }
        public static Stopwatch toc_tic(string tag_next = "")
        {
            toc();
            return tic(tag_next);
        }
        public static void Clear()
        {
            if (!Enabled) return;
            sw.Clear();
        }
        public static void Alert()
        {
            if (!Enabled || sw.Count == 0) return;
            var res = "";
            foreach (var kv in sw)
            {
                var r = kv.Key + ": " + (kv.Value.ElapsedTicks / (0.001 * Stopwatch.Frequency)).ToString("0.##") + " ms ";
                if (masters.ContainsKey(kv.Key))
                {
                    var master = sw[masters[kv.Key]].ElapsedTicks;
                    var p = 100.0 * kv.Value.ElapsedTicks / master;
                    r = ("".PadRight((int)Math.Round(p / 5), '.')).PadRight(20) + "| " + r;
                    r += " (" + p.ToString("0.###") + "% of " + masters[kv.Key] + ")";
                }
                res += r + "\r\n";
            }
            System.Windows.Forms.MessageBox.Show(res);
        }
    }
}
