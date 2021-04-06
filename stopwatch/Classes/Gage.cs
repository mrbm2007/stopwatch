using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Drawing;
using System.ComponentModel;
using System.Globalization;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Reflection;

namespace stopwatch
{
    [Serializable]
    class Gage
    {
        static MyData db { get { return MyData.db; } }
        public Gage()
        {
            Refresh();
            image_icon_flag = Icon_flag.آبی;
            Visible = true;
        }
        public void Refresh()
        {
            icon = icon ?? new NotifyIcon(Form1.mainForm.notifyIcon1.Container)
            {
                Text = Name,
                Icon = Icon.FromHandle(((Bitmap)Properties.Resources.signal_flag_blue).GetHicon()),
                Visible = true
            };
            icon.BalloonTipClosed += (sender, e) =>
            {
                var thisIcon = (NotifyIcon)sender;
                thisIcon.Visible = false;
                thisIcon.Dispose();
            };
            timer_delay = timer_delay ?? new Timer() { Interval = 200 };
            timer_delay.Tick -= timer_delay_Tick;
            icon.MouseMove -= icon_MouseMove;
            icon.MouseClick -= icon_MouseClick;
            icon.DoubleClick -= icon_DoubleClick;
            timer_delay.Tick += timer_delay_Tick;
            icon.MouseMove += icon_MouseMove;
            icon.MouseClick += icon_MouseClick;
            icon.DoubleClick += icon_DoubleClick;
            if (menu == null)
            {
                menu = new ContextMenuStrip();
                menu.RightToLeft = RightToLeft.Yes;
                menu.Font = Form1.mainForm.Font;
                menu.Items.Add("Stopwatch").Enabled = false;
                menu.Items.Add(new ToolStripSeparator());
                var item = menu.Items.Add("ویرایش");
                item.Font = new Font(item.Font, FontStyle.Bold);
                item.Click += icon_DoubleClick;
                menu.Items.Add("نمایش مقدار").Click += timer_delay_Tick;
                menu.Items.Add("دیباگ").Click += (ss, ee) => { debug = true; };
            }
            icon.ContextMenuStrip = menu;
            last_get = DateTime.Now.AddYears(-1);
            last_move = DateTime.Now.AddYears(-1);
        }
        #region Events
        [NonSerialized]
        Timer timer_delay;
        [NonSerialized]
        public ContextMenuStrip menu;
        void timer_delay_Tick(object sender, EventArgs e)
        {
            timer_delay.Stop();
            MessageBox.Show(Form1.mainForm, IconText(), "stopwatch");
        }
        void icon_MouseMove(object sender, MouseEventArgs e)
        {
            if ((DateTime.Now - last_move).TotalSeconds < 10) return;
            last_move = DateTime.Now;
            var str = IconText();
            icon.Text = str.Substring(0, Math.Min(60, str.Length));
            UpdateProgress();
        }
        void icon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) timer_delay.Start();
        }
        void icon_DoubleClick(object sender, EventArgs e)
        {
            timer_delay.Stop();
            new Form_Gages(this).Show();
        }

        #endregion

        public void UpdateProgress()
        {
            try
            {
                var str = IconText();
                str = str.Replace(this.Name, "").Trim();
                if (str.Contains("%"))
                    try
                    {
                        var reg = new System.Text.RegularExpressions.Regex(@"(\d+\.?\d*)\s*\%");
                        var v = (int)Convert.ToDouble(reg.Match(str).Groups[1].Value);
                        icon.Icon = Icon.FromHandle((Utils.AddProgress(image_icon, v)).GetHicon());
                    }
                    catch
                    {
                        if (image_icon != null)
                            icon.Icon = Icon.FromHandle(((Bitmap)image_icon).GetHicon());
                    }
            }
            catch { }
        }
        Image _image_icon = null;
        DateTime last_get = DateTime.Now.AddYears(-1);
        DateTime last_move = DateTime.Now.AddYears(-1);
        string last_str = "";
        string IconText()
        {
            if ((DateTime.Now - last_get).TotalSeconds > 3)
            {
                last_get = DateTime.Now;
                try
                {
                    return last_str = this.Name + "\r\n" + GetValue(db);
                }
                catch (Exception ex) { return last_str = this.Name + "\r\n" + ex.Message; }
            }
            else
                return last_str;
        }
        Icon_flag _image_icon_flag = Icon_flag.آبی;
        string _icon_file;
        internal Image image_icon
        {
            set
            {
                icon.Icon = Icon.FromHandle(((Bitmap)value).GetHicon());
                _image_icon = value;
            }
            get { return _image_icon; }
        }

        [DisplayName("نام")]
        public string Name { get; set; } = "شمارنده ";
        [DisplayName("آیکون"), DefaultValue(Icon_flag.آبی)]
        public Icon_flag image_icon_flag
        {
            set
            {
                _image_icon_flag = value;
                if (value == Icon_flag.آبی)
                    image_icon = Properties.Resources.signal_flag_blue;
                else if (value == Icon_flag.سبز)
                    image_icon = Properties.Resources.signal_flag_green;
                else if (value == Icon_flag.قرمز)
                    image_icon = Properties.Resources.signal_flag_red;
                else if (value == Icon_flag.سفید)
                    image_icon = Properties.Resources.signal_flag_white;
                else if (value == Icon_flag.زرد)
                    image_icon = Properties.Resources.signal_flag_yellow;
            }
            get { return _image_icon_flag; }
        }
        [DisplayName("آیکون دلخواه"), DefaultValue("")]
        [Editor(typeof(UI_File), typeof(UITypeEditor)), TypeConverter(typeof(TC_file))]
        public string icon_file
        {
            get { return _icon_file; }
            set
            {
                _icon_file = value;
                if ((value + "").Trim() != "")
                {
                    image_icon = new Bitmap(value);
                    image_icon_flag = Icon_flag.دلخواه;
                }
            }
        }
        [DisplayName("نمایش"), DefaultValue(true)]
        public bool Visible
        {
            get { return icon.Visible; }
            set { icon.Visible = value; }
        }
        [DisplayName("نوع خروجی"), DefaultValue(Type.ساعت)]
        public Type type { get; set; } = Type.ساعت;
        [DisplayName("فرمول خروجی"), DefaultValue("A")]
        [Description("day: روز از ماه, A,B,..,Z: پروژه ها, .str()")]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(UITypeEditor))]
        public string output { get; set; } = "A";
        public List<Condition> Conditions = new List<Condition> { new Condition() };
        [DisplayName("نتیجه")]
        public string Result
        {
            get { return GetValue(db); }
        }
        [DisplayName("دیباگ"), DefaultValue(false)]
        public bool debug { get { return false; } set { if (value) { GetValue(db, true); } } }
        [NonSerialized]
        internal NotifyIcon icon = new NotifyIcon();
        public string GetValue(MyData db, bool debug = false)
        {
            var str = "";
            foreach (var c in Conditions)
            {
                var sum = 0;
                foreach (var p in db.Projects)
                    if (p.Active)
                    {
                        foreach (var t in p.Times)
                            if (c.Satisfy(t))
                                sum += t.Duration.sec;
                    }
                str += "double " + c.Name + "=" + sum + ";//" + c.project + "\r\n";
            }
            str += "double day=" + Utils.PersianDay(DateTime.Now) + ";\r\n";
            if (output.Contains("return "))
                str += output + ";";
            else
                str += "return " + output + ";";
            str = str.Replace("Round(", "Math.Round(");
            str = str.Replace("Floor(", "Math.Floor(");
            str = str.Replace("Ceiling(", "Math.Ceiling(");
            str = str.Replace("Abs(", "Math.Abs(");
            str = str.Replace("Sin(", "Math.Sin(");
            str = str.Replace("Cos(", "Math.Cos(");
            str = str.Replace("Tan(", "Math.Tan(");
            str = str.Replace("Min(", "Math.Min(");
            str = str.Replace("Max(", "Math.Max(");
            str = str.Replace("round(", "Math.Round(");
            str = str.Replace("floor(", "Math.Floor(");
            str = str.Replace("ceiling(", "Math.Ceiling(");
            str = str.Replace("abs(", "Math.Abs(");
            str = str.Replace("sin(", "Math.Sin(");
            str = str.Replace("cos(", "Math.Cos(");
            str = str.Replace("tan(", "Math.Tan(");
            str = str.Replace("min(", "Math.Min(");
            str = str.Replace("max(", "Math.Max(");
            str = str.Replace(".str()", ".ToString(\"0.##\")");
            str = str.Replace("Math.Math.", "Math.");
            if (debug) MessageBox.Show(str);
            var v = RunCompiler(str);
            if (v is double || v is int)
            {
                if (type == Type.ساعت)
                    return new TSpan(Convert.ToInt32(v)) + "";
                else if (type == Type.درصد)
                    return (100 * Convert.ToDouble(v)).ToString("0.0#") + " %";
                else
                    return Convert.ToDouble(v).ToString("0.00#");
            }
            else
                return v.ToString();
        }

        public Gage Save(string fileName)
        {
            using (var ofile = new StreamWriter(fileName))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ofile.BaseStream, this);
                ofile.Close();
            }
            return this;
        }
        public static Gage Load(string fileName)
        {
            using (var ifile = new StreamReader(fileName))
            {
                IFormatter formatter = new BinaryFormatter();
                var g = (Gage)formatter.Deserialize(ifile.BaseStream);
                foreach (var c in g.Conditions)
                {
                    c.project = db.GetByName(c.project + "");
                    if (c.projects == null)
                    {
                        c.projects = new List<Project>();
                        if (c.project != null)
                            c.projects.Add(c.project);
                    }
                    for (int i = 0; i < c.projects.Count; i++)
                        if (c.projects[i] != null)
                            c.projects[i] = db.GetByName(c.projects[i].Name);
                }
                try
                {
                    g.Refresh();
                }
                catch { }
                try
                {
                    g.icon_file = g.icon_file;
                }
                catch { }
                try
                {
                    g.image_icon_flag = g.image_icon_flag;
                }
                catch { }
                return g;
            }
        }

        public static object RunCompiler(string str)
        {
            using (var compiler = new CSharpCodeProvider())
            {
                var references = new CompilerParameters();

                references.ReferencedAssemblies.Add("System.dll");
                references.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "").Replace("/", "\\"));
                references.GenerateInMemory = true;

                string lcCode = @"using System;
                            namespace N {  
                                    public class C {
                                        public static string str(object x)
                                        {
                                            if (x is double)
                                                return ((double)x).ToString(""0.##"");
                                            else
                                                return x.ToString();
                                        }
                                        public static string timestrsec(object x)
                                        {
                                            if (x is double || x is int || x is long)
                                            {
                                                long s = Convert.ToInt64(x);
                                                return (s / 3600) + "":"" + ((s % 3600) / 60).ToString(""00"") + "":"" + (s % 60).ToString(""00"");
                                            }
                                            else
                                                return x.ToString();
                                        }
                                        public static string timestr(object x)
                                        {
                                            if (x is double || x is int || x is long)
                                            {
                                                long s = Convert.ToInt64(x);
                                                return (s / 3600) + "":"" + ((s % 3600) / 60).ToString(""00"");
                                            }
                                            else
                                                return x.ToString();
                                        }
                                        public static long now()
                                        {
                                             return (long)(DateTime.Now-new DateTime()).TotalSeconds ;
                                        }
                                        public static long time(string str)
                                        {
                                             return (long)(stopwatch.Utils.PersianParse_DateTime(str)-new DateTime()).TotalSeconds ;
                                        }
                                        public static object calc() {
                                             " + str + @";
                                        }
                                    }
                            }"; 
                var compilerResults = compiler.CompileAssemblyFromSource(references, lcCode);
                if (compilerResults.Errors.HasErrors)
                {
                    var msg = "";
                    foreach (CompilerError err in compilerResults.Errors)
                        msg += "\r\n" + err.Line + ": " + err.ErrorText;
                    throw new Exception("Compile Error in Parser: " + msg);
                }


                var assembly = compilerResults.CompiledAssembly;
                var T = assembly.GetType("N.C");
                if (T == null)
                    throw new Exception("Compile Error in Parser");

                var res = T.GetMethod("calc").Invoke(null, null);

                return res;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public enum Type
        {
            ساعت, درصد, کلی
        }
        public enum Icon_flag
        {
            آبی, قرمز, سبز, زرد, سفید, دلخواه
        }
    }
    [Serializable]
    class Condition
    {
        static MyData db { get { return MyData.db; } }
        [DisplayName("پارامتر"), Description("برای استفاده در فرمول شمارنده")]
        [DefaultValue("A")]
        public string Name { get; set; } = "A";
        [Editor(typeof(UI_Project), typeof(UITypeEditor))]
        [DisplayName("پروژه"), TypeConverter(typeof(TC_)), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Project project { get; set; } = null;
        [Editor(typeof(UI_Projects), typeof(UITypeEditor))]
        [DisplayName("پروژه ها"), TypeConverter(typeof(TC_projects))]
        public List<Project> projects { get; set; }
        [TypeConverter(typeof(TC_))]
        [DisplayName("فعالیت"), Description("جهت جستجو در فعالیت های ثبت شده")]
        [DefaultValue("")]
        public string search { get; set; } = "";
        [TypeConverter(typeof(TC_)), DefaultValue("این ماه")]
        [DisplayName("شروع"), Description("امروز - این ماه - 95/05/12")]
        public string t1_ { get; set; } = "این ماه";
        [TypeConverter(typeof(TC_)), DefaultValue("این ماه")]
        [DisplayName("پایان"), Description("امروز - این ماه - 95/05/12")]
        public string t2_ { get; set; } = "این ماه";
        internal DateTime t1
        {
            get
            {
                var str = ("" + t1_).Trim();
                if (str == "") return DateTime.Now.AddYears(-100);
                if (str == "امروز")
                    return DateTime.Now.Date;
                if (str == "این ماه" || str == "اینماه" || str == "ماه جاری")
                    return Utils.PersianParse(Utils.DateString().Substring(0, 6) + "01");
                return Utils.PersianParse(str);
            }
        }
        internal DateTime t2
        {
            get
            {
                var str = ("" + t2_).Trim();
                if (str == "") return DateTime.Now.AddYears(+100);
                if (str == "امروز")
                    return DateTime.Now.Date.AddDays(0.9999);
                else if (str == "این ماه")
                    try
                    { return Utils.PersianParse(Utils.DateString().Substring(0, 6) + "31").AddDays(0.9999); }
                    catch
                    {
                        try
                        { return Utils.PersianParse(Utils.DateString().Substring(0, 6) + "30").AddDays(0.9999); }
                        catch
                        { return Utils.PersianParse(Utils.DateString().Substring(0, 6) + "29").AddDays(0.9999); }
                    }
                else
                    return Utils.PersianParse(str).AddDays(0.9999);
            }
        }
        [DefaultValue(Place.همه)]
        [DisplayName("مکان")]
        public Place place { get; set; }
        public bool Satisfy(Time_ t)
        {
            if (projects.Count != 0 && !projects.Contains(t.project))
                return false;
            if (t.Start < t1 || t.End > t2)
                return false;
            if (search.Trim() != "" && !t.Comment.Contains(search))
                return false;
            if (place != Place.همه)
            {
                var H = db.GetByType(hozoor: true);
                var in_ = false;
                foreach (var tt in H.Times)
                    if (tt.Overlap(t) / (t.Duration.sec + 1) > 0.5)
                    {
                        in_ = true;
                        break;
                    }
                if (place == Place.بیرون)
                    return !in_;
                else
                    return in_;
            }
            return true;
        }

        public enum Place
        {
            همه, شرکت, بیرون
        }
        public override string ToString()
        {
            return Name + ": " + project + ", " + search;
        }
    }

    class UI_Project : UITypeEditor
    {
        static MyData db { get { return MyData.db; } }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService = null;
            if (provider != null)
                editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            if (editorService != null)
            {
                var udControl = new ListBox() { Dock = DockStyle.Fill, BorderStyle = BorderStyle.None };
                udControl.RightToLeft = RightToLeft.Yes;
                udControl.Font = Form1.mainForm.Font;
                udControl.Items.Add(" - همه -");
                foreach (var p in db.Projects)
                    if (p.Active)
                        udControl.Items.Add(p);
                foreach (var p in db.Projects)
                    if (!p.Active)
                        udControl.Items.Add(p);
                udControl.SelectedIndexChanged += (ss, ee) => { editorService.CloseDropDown(); };
                udControl.SelectedItem = value;
                editorService.DropDownControl(udControl);
                if (udControl.SelectedItem is Project) return udControl.SelectedItem;
                return null;
            }

            return value;
        }
    }
    class UI_Projects : UITypeEditor
    {
        static MyData db { get { return MyData.db; } }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService = null;
            if (provider != null)
                editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            if (editorService != null)
            {
                var v = (List<Project>)value;
                if (v == null)
                {
                    v = new List<Project>();
                    v.AddRange(db.Projects);
                }
                var udControl = new CheckedListBox() { Dock = DockStyle.Fill, BorderStyle = BorderStyle.None };
                udControl.RightToLeft = RightToLeft.Yes;
                udControl.Font = Form1.mainForm.Font;

                foreach (var p in db.Projects)
                    if (p.Active)
                    {
                        var i = udControl.Items.Add(p);
                        udControl.SetItemChecked(i, v.Contains(p));
                    }
                if (udControl.Items.Count != db.Projects.Count)
                {
                    udControl.Items.Add(" -----");
                    foreach (var p in db.Projects)
                        if (!p.Active)
                        {
                            var i = udControl.Items.Add(p);
                            udControl.SetItemChecked(i, v.Contains(p));
                        }
                }
                var btn1 = new Button { Text = "همه", Location = new Point(2, 2), Size = new Size(45, 21) };
                var btn2 = new Button { Text = "هیچ", Location = new Point(2 + 2 + 45, 2), Size = new Size(45, 21) };
                var btn3 = new Button { Text = "ok", Location = new Point(2 + 2 + 45 + 2 + 45, 2), Size = new Size(45, 21) };
                btn1.Click += (s, e) =>
                {
                    for (var i = 0; i < udControl.Items.Count; i++)
                        udControl.SetItemChecked(i, true);
                };
                btn2.Click += (s, e) =>
                {
                    for (var i = 0; i < udControl.Items.Count; i++)
                        udControl.SetItemChecked(i, false);
                };
                btn3.Click += (s, e) =>
                {
                    editorService.CloseDropDown();
                };
                var panel = new Panel() { Dock = DockStyle.Fill, BorderStyle = BorderStyle.None, Height = 200, Padding = new Padding(0, 25, 0, 0) };
                panel.Controls.Add(udControl);
                panel.Controls.Add(btn1);
                panel.Controls.Add(btn2);
                panel.Controls.Add(btn3);
                editorService.DropDownControl(panel);
                /*udControl.ItemCheck += (ss, ee) =>
                {
                    if (ee.Index == 0) 
                        for (int i = 1; i < udControl.Items.Count; i++)
                            udControl.SetItemChecked(i, udControl.GetItemChecked(0)); 
                    else
                        udControl.SetItemCheckState(0, CheckState.Indeterminate);
                };*/
                var res = v ?? new List<Project>();
                res.Clear();
                foreach (var s in udControl.CheckedItems)
                    if (s is Project)
                        res.Add(s as Project);
                //if (res.Count == db.Projects.Count)
                //    res.Clear();
                return res;
            }

            return value;
        }
    }
    class UI_File : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (var fld = new OpenFileDialog()
            {
                Filter = "Image files|*.png;*.jpg;*.bmp;*.ico;*.jpge"
            })
            {
                var str = (value + "").Trim();
                if (str != "")
                    try
                    {
                        fld.FileName = Path.GetFileName(str);
                        fld.InitialDirectory = Path.GetDirectoryName(str);
                    }
                    catch { }
                if (fld.ShowDialog() == DialogResult.OK)
                    return fld.FileName;
            }
            return value;
        }
    }
    class TC_ : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((value + "").Trim() == "") return " - همه - ";
            return value + "";
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                if ((value + "").Trim() == "- همه -") return null;
                return value + "";
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
    class TC_file : TC_
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((value + "").Trim() == "") return " ...";
            else
                try
                {
                    return Path.GetFileNameWithoutExtension(value + "");
                }
                catch { }
            return value + "";
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                if ((value + "").Trim() == "...") return "";
                return value + "";
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
    class TC_projects : TypeConverter
    { 
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var v = (List<Project>)value;
            if (v == null) return " - همه - ";
            if (v.Count == MyData.db.Projects.Count) return " - همه - ";
            if (v.Count == 0) return " - هیچ - ";
            var res = "";
            foreach (var p in v)
                res += (p == v[0] ? "" : ", ") + p.Name;
            return res;
        }
    }
}