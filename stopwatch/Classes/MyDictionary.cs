using System;
using System.Collections.Generic;

namespace stopwatch
{

    public class MyDictionary<Tk, Tv> where Tv : new()
    {
        public List<Tk> Keys = new List<Tk>();
        public List<Tv> Vals = new List<Tv>();
        public Tv this[Tk key]
        {
            get
            {
                var i = Keys.IndexOf(key);
                if (i >= 0)
                    return Vals[i];
                else if (DictOption.AutoGenerateIfNotExist)
                {
                    Keys.Add(key);
                    Vals.Add(new Tv());
                    return Vals[Vals.Count - 1];
                }
                else
                {
                    throw new Exception("Key not found in dictionary: '" + key + "'");
                }
            }
            set
            {
                var i = Keys.IndexOf(key);
                if (i >= 0)
                    Vals[i] = value;
                else
                {
                    Keys.Add(key);
                    Vals.Add(value);
                }
            }
        }

        internal void Clear()
        {
            Keys.Clear();
            Vals.Clear();
        }

        public void Sort(CompareKey comparer = null)
        {
            comparer = comparer ?? comparerKey;
            for (int i = 0; i < Keys.Count; i++)
                for (int j = i + 1; j < Keys.Count; j++)
                    if (comparer(Keys[i], Keys[j]))
                    {
                        var tmp1 = Keys[i];
                        Keys[i] = Keys[j];
                        Keys[j] = tmp1;
                        var tmp2 = Vals[i];
                        Vals[i] = Vals[j];
                        Vals[j] = tmp2;
                    }
        }
        public void SortByValues(CompareVal comparer = null)
        {
            comparer = comparer ?? comparerVal;
            for (int i = 0; i < Keys.Count; i++)
                for (int j = i + 1; j < Keys.Count; j++)
                    if (comparer(Vals[i], Vals[j]))
                    {
                        var tmp1 = Keys[i];
                        Keys[i] = Keys[j];
                        Keys[j] = tmp1;
                        var tmp2 = Vals[i];
                        Vals[i] = Vals[j];
                        Vals[j] = tmp2;
                    }
        }
        public delegate bool CompareKey(Tk a, Tk b);
        public delegate bool CompareVal(Tv a, Tv b);
        static CompareKey comparerKey = (o1, o2) => string.Compare(o1 + "", o2 + "") > 0;
        static CompareVal comparerVal = (o1, o2) => string.Compare(o1 + "", o2 + "") > 0;
    }
    public class MyDictionary<Tk>
    {
        public List<Tk> Keys = new List<Tk>();
        public List<string> Vals = new List<string >();
        public string this[Tk key]
        {
            get
            {
                var i = Keys.IndexOf(key);
                if (i >= 0)
                    return Vals[i];
                else if (DictOption.AutoGenerateIfNotExist)
                {
                    Keys.Add(key);
                    Vals.Add("");
                    return Vals[Vals.Count - 1];
                }
                else
                {
                    throw new Exception("Key not found in dictionary: '" + key + "'");
                }
            }
            set
            {
                var i = Keys.IndexOf(key);
                if (i >= 0)
                    Vals[i] = value;
                else
                {
                    Keys.Add(key);
                    Vals.Add(value);
                }
            }
        }

        internal void Clear()
        {
            Keys.Clear();
            Vals.Clear();
        }
    }

    class DictOption
    {
        [ThreadStatic]
        public static bool AutoGenerateIfNotExist = true;
    }
}