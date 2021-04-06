using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Text;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.IO.Compression;

namespace stopwatch
{
    [Serializable]
    public class MyData
    {
        public static bool ReadOnlyMode = false;
        public static MyData db = new MyData();

        public List<Project> Projects = new List<Project>();
        public List<Project> ActiveProjects
        {
            get
            {
                var res = new List<Project>();
                foreach (var p in Projects)
                    if (p.Active)
                        res.Add(p);
                return res;
            }
        }
        /// <summary>
        /// current project
        /// </summary> 
        public Project project;
        public Project resume_project_at_startup = null;
        public string[] open_chat_users = null;
        public Project GetByName(string name)
        {
            name = name.ToLower().Trim();
            foreach (var p in Projects)
                if (p.Name.Trim().ToLower() == name)
                    return p;
            return null;
        }
        public Project GetByCode(string code, bool include_hozoor = true)
        {
            code = code.ToLower().Trim();
            foreach (var p in Projects)
                if ((p.code + "").Trim().ToLower() == code && (include_hozoor || !p.IsHozoor))
                    return p;
            return null;
        }
        public Project GetByType(bool omoorjary = false, bool hozoor = false)
        {
            foreach (var p in Projects)
                if (p.IsOmoorJari == omoorjary && p.IsHozoor == hozoor)
                    return p;
            return null;
        }
        public bool IsEmpty()
        {
            foreach (var p in Projects)
                if (p.Times.Count > 0)
                    return false;
            return true;
        }
        static void SafelyRename(string fileName_from, string fileName_to)
        {
            var fileName_del = fileName_to + ".delete";
            if (File.Exists(fileName_del))
                File.Delete(fileName_del);
            if (File.Exists(fileName_to))
                File.Move(fileName_to, fileName_del);
            File.Move(fileName_from, fileName_to);
            if (File.Exists(fileName_del))
                File.Delete(fileName_del);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static bool LoadTest(string fileName)
        {
            try
            {
                Deserialize(fileName);
                return true;
            }
            catch { }
            return false;
        }
        static void Serialize(string PathName, object data, bool compressed = false)
        {
            if (compressed)
                using (var s = File.Create(PathName))
                {
                    using (var gs = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(s))
                    {
                        gs.PutNextEntry(new ICSharpCode.SharpZipLib.Zip.ZipEntry("data"));
                        var bf = new BinaryFormatter();
                        bf.Serialize(gs, data);
                    }
                }
            else
                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, data);
                    using (var ofile = new StreamWriter(PathName))
                        stream.WriteTo(ofile.BaseStream);
                }
        }
        static object Deserialize(string PathName)
        {
            // not compressed (try)
            using (var s = File.OpenRead(PathName))
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    var res = (MyData)formatter.Deserialize(s);
                    return res;
                }
                catch { }

            // compressed
            using (var s = File.OpenRead(PathName))
            using (var gs = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(s))
            {
                gs.GetNextEntry();
                var bf = new BinaryFormatter();
                return bf.Deserialize(gs);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public MyData Load(string fileName, DateTime? start = null, DateTime? end = null)
        {
            var res = (MyData)Deserialize(fileName);
            AfterLoad(res, start, end);
            return this;
        }
        public MyData Clone()
        {
            var f = Path.GetTempFileName();
            Save(f, true);
            var res = new MyData();
            res.Load(f);
            return res;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Save(string fileName, bool evenIfEmptye = false, DateTime? start = null, DateTime? end = null, bool compressed = false)
        {
            if (ReadOnlyMode) return;
            if (evenIfEmptye || !IsEmpty())
            {
                Serialize(fileName + ".tmp", this, compressed);
                if (start == null && end == null)
                    SafelyRename(fileName + ".tmp", fileName);
                else
                {
                    var db = new MyData(); // a clone of data
                    db.Load(fileName + ".tmp", start, end);
                    db.Save(fileName, evenIfEmptye, compressed: compressed);
                }
            }
        }
        void AfterLoad(MyData res, DateTime? start = null, DateTime? end = null)
        {
            Projects = res.Projects;
            settings = res.settings ?? new Settings();
            user = res.user ?? new User();

            if (start != null || end != null)
            {
                end = end ?? DateTime.Now.AddYears(5000);
                start = start ?? DateTime.Now.AddYears(-5000);
                foreach (var p in Projects)
                    for (int i = 0; i < p.Times.Count; i++)
                        if (p.Times[i].Start > end || p.Times[i].End < start)
                            p.Times.RemoveAt(i--);
            }

            {// re-link
                foreach (var p in Projects)
                {
                    if (p.LastBackup.Year < 1900) p.LastBackup = new DateTime(1900, 1, 1);
                    if (p.LastBackup.Year > 3000) p.LastBackup = new DateTime(1900, 1, 1);
                    foreach (var t in p.Times)
                        t.project = p;
                }
                try
                {
                    if (res.project != null)
                        project = GetByName(res.project.Name);
                }
                catch { project = null; }
                try
                {
                    resume_project_at_startup = GetByName(res.resume_project_at_startup?.Name ?? "");
                }
                catch { resume_project_at_startup = null; }
            }
            var all_deactive = true;
            foreach (var p in Projects)
                if (p.Active)
                {
                    all_deactive = false;
                    break;
                }
            if (all_deactive)
                foreach (var p in Projects)
                    p.Active = true;
            if (MyData.db == this && GetByType(hozoor: true) == null)
                Projects.Add(new Project() { Name = "ورود-خروج" });

#pragma warning disable 0612
            /// for old versions ~3.3.2.22  
            //user.name = settings.real_name + "";
            // if (settings.Server == "smalavi-pc")
            // settings.host = "192.168.1.1:90";
#pragma warning restore 0612


            try
            {
                var coms = Environment.GetCommandLineArgs();
                if (coms.Length > 2)
                {

                    if (coms[1].ToLower() == "/host")
                    {
                        res.settings.host = coms[2];
                    }
                }
            }
            catch { }
        }

        public Settings settings = new Settings();
        public User user = new User();
    }

}
