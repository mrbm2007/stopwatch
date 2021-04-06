using System;
using System.Collections.Generic;
using System.Text;

namespace stopwatch 
{

    [Serializable]
    public class Time_
    {
        public Time_(Project project)
        {
            Start = DateTime.Now;
            this.project = project;
            if (project != null)
                Comment = project.LastComment;
        }
        public Project project;
        public DateTime Start;
        public DateTime End
        {
            get { return Start.AddSeconds(Duration.sec); }
            set { Duration.sec = (int)(value - Start).TotalSeconds; }
        }
        public TSpan Duration = new TSpan();
        public string Comment = "";
        public bool Contains(DateTime t, int tolerance_sec = 0)
        {
            if (tolerance_sec == 0)
                return Start <= t && t <= Start.AddSeconds(Duration.sec);
            else
                return Start.AddSeconds(-tolerance_sec) <= t && t <= End.AddSeconds(tolerance_sec);
        }
        public bool Contains(Time_ t, int tolerance_sec = 0)
        {
            return Contains(t.Start, tolerance_sec) && Contains(t.End, tolerance_sec);
        }
        public bool Equals(Time_ t2, double tolerance_percentage = 15)
        {
            var t1 = this;
            var D = Math.Max(Math.Abs((t1.End - t1.Start).TotalSeconds), Math.Abs((t2.End - t2.Start).TotalSeconds)) + 1;
            return ((Utils.Min(t1.End, t2.End) - Utils.Max(t1.Start, t2.Start)).TotalSeconds / D > (1 - tolerance_percentage / 100.0));
        }
        public Time_ Subscription(Time_ t)
        {
            var res = new Time_(null);
            if (Start > t.Start) res.Start = Start;
            else res.Start = t.Start;
            if (End > t.End) res.End = t.End;
            else res.End = End;
            if (res.Duration.sec < 0) res.Duration.sec = 0;
            return res;
        }
        public List<Time_> NotSubscriptionWith(List<Time_> T)
        {
            var res = new List<Time_> { this };
            foreach (var t in T)
                for (int i = 0; i < res.Count; i++)
                    if (res[i].Subscription(t).Duration.sec != 0)
                    {
                        var r1 = new Time_(null);
                        var r2 = new Time_(null);
                        if (res[i].Start < t.Start) { r1.Start = res[i].Start; r1.End = t.Start; }
                        if (res[i].End > t.End) { r2.Start = t.End; r2.End = res[i].End; }
                        res.RemoveAt(i--);
                        if (r1.Duration.sec > 0) res.Insert(++i, r1);
                        if (r2.Duration.sec > 0) res.Insert(++i, r2);
                    }
            return res;
        }

        /// <summary>
        /// seconds
        /// </summary>
        /// <param name="t2"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public double Overlap(Time_ t2)
        {
            var res = (Utils.Min(this.End, t2.End) - Utils.Max(this.Start, t2.Start)).TotalSeconds;
            if (res < 0) return 0;
            return res;
        }
        public override string ToString()
        {
            return Utils.DateString(Start) + ": " + Utils.TimeString(Start) + " - " + Utils.TimeString(End);
        }
    }
}
