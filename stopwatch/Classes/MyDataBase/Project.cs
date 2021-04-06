using System;
using System.Collections.Generic;
using System.Text;

namespace stopwatch
{
    [Serializable]
    public class Project
    {
        public string Name;
        public string LastComment = "فعالیت جاری ...";
        public Time_ LastTime { get { if (Times.Count > 0) return Times[Times.Count - 1]; return null; } }
        public DateTime LastBackup = new DateTime(1900, 1, 1);
        public List<Time_> Times = new List<Time_>();
        public override string ToString()
        {
            return Name;
        }
        public void SortTimes()
        {
            for (int i = 0; i < Times.Count; i++)
                for (int j = i + 1; j < Times.Count; j++)
                    if (Times[j].Start < Times[i].Start)
                    {
                        var tmp = Times[i];
                        Times[i] = Times[j];
                        Times[j] = tmp;
                    }
        }
        public int Wage = 0;
        public string Manager = "";
        public bool Active = true;

        public string code;
        public string LocalPath = "";
        public string BackUpSkipPattern = "";

        public List<Time_> TimesAt(DateTime date1, DateTime date2)
        {
            var res = new List<Time_>();
            foreach (var t in Times)
                if (date1 <= t.Start && t.End <= date2)
                    res.Add(t);
            return res;
        }

        public bool IsHozoor
        {
            get { return Name == "ورود-خروج" || Name == "ورود خروج" || Name == "حضور"; }
        }
        public bool IsOmoorJari
        {
            get { return Name.StartsWith("امور جاری") || Name.StartsWith("امورجاری") || Name.StartsWith("امور-جاری"); }
        }

        public bool IsRunning(int tolerance = 0)
        {
            if (LastTime == null) return false;
            return (DateTime.Now - LastTime.End).TotalSeconds < tolerance;
        }
        internal TSpan SumTimes()
        {
            var res = new TSpan(0);
            foreach (var t in Times)
                res += t.Duration;
            return res;
        }
        internal TSpan SumTimes(DateTime date1, DateTime date2)
        {
            var res = new TSpan(0);
            foreach (var t in TimesAt(date1, date2))
                res += t.Duration;
            return res;
        }
    }
}
