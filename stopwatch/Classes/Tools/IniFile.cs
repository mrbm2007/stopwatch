//  Programmer: Ludvik Jerabek
//        Date: 08\23\2010
//     Purpose: Allow INI manipulation in .NET

using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

// IniFile class used to read and write ini files by loading the file into memory
public class IniFile
{
    // List of IniSection objects keeps track of all the sections in the INI file
    private Dictionary<string, IniSection> m_sections;
    public bool changed = false;

    // Public constructor
    public IniFile()
    {
        m_sections = new Dictionary<string, IniSection>(comparer: new compare_str());
    }

    public static IniFile SaveAsIni(object obj, string section = "data")
    {
        var T = obj.GetType();
        var F = T.GetProperties();
        var ini = new IniFile();
        var sec = ini.AddSection(section);
        foreach (var f in F)
        {
            var v = f.GetValue(obj, null);
            if (v is int)
                sec[f.Name] = v + "";
            else if (v is Enum)
                sec[f.Name] = v + "";
            else if (v is uint)
                sec[f.Name] = v + "";
            else if (v is short)
                sec[f.Name] = v + "";
            else if (v is ushort)
                sec[f.Name] = v + "";
            else if (v is long)
                sec[f.Name] = v + "";
            else if (v is ulong)
                sec[f.Name] = v + "";
            else if (v is string)
                sec[f.Name] = v + "";
            else if (v is char)
                sec[f.Name] = v + "";
            else if (v is bool)
                sec[f.Name] = v + "";
        }
        return ini;
    }
    public static void LoadFromIni(object obj, IniFile ini, string section = "data")
    {
        var T = obj.GetType();
        var F = T.GetProperties();
        var sec = ini.GetSection(section);
        foreach (var f in F)
        {
            if (f.PropertyType == typeof(int))
                f.SetValue(obj, Convert.ToInt32(sec[f.Name]), null);
            else if (f.PropertyType.IsSubclassOf(typeof(Enum)))
                f.SetValue(obj, Enum.Parse(f.PropertyType, sec[f.Name]), null);
            else if (f.PropertyType == typeof(uint))
                f.SetValue(obj, Convert.ToUInt32(sec[f.Name]), null);
            else if (f.PropertyType == typeof(short))
                f.SetValue(obj, Convert.ToInt16(sec[f.Name]), null);
            else if (f.PropertyType == typeof(ushort))
                f.SetValue(obj, Convert.ToUInt16(sec[f.Name]), null);
            else if (f.PropertyType == typeof(long))
                f.SetValue(obj, Convert.ToInt64(sec[f.Name]), null);
            else if (f.PropertyType == typeof(ulong))
                f.SetValue(obj, Convert.ToUInt64(sec[f.Name]), null);
            else if (f.PropertyType == typeof(string))
                f.SetValue(obj, sec[f.Name] + "", null);
            else if (f.PropertyType == typeof(char))
                f.SetValue(obj, (sec[f.Name] + "\0")[0], null);
            else if (f.PropertyType == typeof(bool))
                f.SetValue(obj, Convert.ToBoolean(sec[f.Name]), null);
        }
    }
    public IniFile LoadString(string str, bool bMerge = false, string section_pre_expresion = "")
    {
        if (!bMerge)
        {
            RemoveAllSections();
        }
        changed = false;
        //  Clear the object... 
        IniSection tempsection = null;
        Regex regexcomment = new Regex("^([\\s]*#.*)", (RegexOptions.Singleline | RegexOptions.IgnoreCase));
        Regex regexsection = new Regex("\\[[\\s]*([^\\[\\s].*[^\\s\\]])[\\s]*\\]", (RegexOptions.Singleline | RegexOptions.IgnoreCase));
        Regex regexkey = new Regex("^\\s*([^=\\s]*)[^=]*=(.*)", (RegexOptions.Singleline | RegexOptions.IgnoreCase));
        var Lines = str.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in Lines)
        {
            if (line != String.Empty)
            {
                Match m = null;
                if (regexcomment.Match(line).Success)
                {
                    m = regexcomment.Match(line);
                    //  Trace.WriteLine(string.Format("Skipping Comment: {0}", m.Groups[0].Value));
                }
                else if (regexsection.Match(line).Success)
                {
                    m = regexsection.Match(line);
                    //  Trace.WriteLine(string.Format("Adding section [{0}]", section_pre_expresion + m.Groups[1].Value));
                    tempsection = AddSection(section_pre_expresion + m.Groups[1].Value);
                }
                else if (regexkey.Match(line).Success && tempsection != null)
                {
                    m = regexkey.Match(line);
                    //  Trace.WriteLine(string.Format("Adding Key [{0}]=[{1}]", m.Groups[1].Value, m.Groups[2].Value));
                    tempsection.AddKey(m.Groups[1].Value).Value = m.Groups[2].Value;
                }
                else if (tempsection != null)
                {
                    //  Handle Key without value
                    //      Trace.WriteLine(string.Format("Adding Key [{0}]", line));
                    tempsection.AddKey(line);
                }
                else
                {
                    //  This should not occur unless the tempsection is not created yet...
                    //    Trace.WriteLine(string.Format("Skipping unknown type of data: {0}", line));
                }
            }
        }
        if (!bMerge)
            changed = false;
        return this;
    }
    /// <summary>
    ///  Loads the Reads the data in the ini file into the IniFile object
    /// </summary>
    /// <param name="sFileName"></param>
    /// <param name="bMerge"></param>
    /// <param name="section_pre_expresion"></param>
    /// <param name="retray">if error, 20ms</param>
    /// <returns></returns>
    public IniFile Load(string sFileName, bool bMerge = false, string section_pre_expresion = "", int retray = 0)
    {
        for (int i = 0; i <= retray; i++)
            try
            {
                using (var stream = File.Open(last_path = sFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var sr = new StreamReader(stream))
                    return LoadString(sr.ReadToEnd(), bMerge, section_pre_expresion);
            }
            catch
            {
                if (i == retray) throw;
                System.Threading.Thread.Sleep(50);
            }
        return null;
    }

    public string SaveString_()
    {
        return Base64Encode(SaveString());
    }
    public void LoadString_(string str, bool bMerge = false, string section_pre_expresion = "")
    {
        LoadString(Base64Decode(str), bMerge, section_pre_expresion);
    }
    public string SaveString()
    {
        var sb = new System.Text.StringBuilder();
        foreach (IniSection s in Sections)
        {
            //  Trace.WriteLine(string.Format("Writing Section: [{0}]", s.Name));
            sb.Append(string.Format("[{0}]", s.Name) + "\r\n");
            foreach (IniSection.IniKey k in s.Keys)
            {
                if (k.Value != String.Empty)
                {
                    //    Trace.WriteLine(string.Format("Writing Key: {0}={1}", k.Name, k.Value));
                    sb.Append(string.Format("{0}={1}", k.Name, k.Value) + "\r\n");
                }
                else
                {
                    //    Trace.WriteLine(string.Format("Writing Key: {0}", k.Name));
                    sb.Append(string.Format("{0}", k.Name) + "\r\n");
                }
            }
            sb.Append("\r\n");
        }
        changed = false;
        return sb.ToString();
    }
    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
    /// <summary>
    /// Used to save the data back to the file or your choice
    /// </summary>
    /// <param name="sFileName"></param>
    /// <param name="retray">if error, 20ms</param>
    public void Save(string sFileName, int retray = 0)
    {
        for (int i = 0; i <= retray; i++)
            try
            {
                File.WriteAllText(last_path = sFileName, SaveString());
            }
            catch
            {
                if (i == retray) throw;
                System.Threading.Thread.Sleep(50);
            }
    }
    string last_path = "";
    public void Save(int retray = 0)
    {
        Save(last_path, retray);
    }

    // Gets all the sections names
    public System.Collections.ICollection Sections
    {
        get
        {
            return m_sections.Values;
        }
    }

    // Adds a section to the IniFile object, returns a IniSection object to the new or existing object
    public IniSection AddSection(string sSection)
    {
        IniSection s = null;
        sSection = sSection.Trim();
        // Trim spaces
        if (m_sections.ContainsKey(sSection))
        {
            s = (IniSection)m_sections[sSection];
        }
        else
        {
            s = new IniSection(this, sSection);
            m_sections[sSection] = s;
            changed = true;
        }
        return s;
    }

    // Removes a section by its name sSection, returns trus on success
    public bool RemoveSection(string sSection)
    {
        sSection = sSection.Trim();
        return RemoveSection(GetSection(sSection));
    }
    public bool RemoveSection(int i)
    {
        foreach (var k in m_sections.Keys)
            if (i-- == 0)
            {
                m_sections.Remove(k);
                return true;
            }
        return false;
    }

    // Removes section by object, returns trus on success
    public bool RemoveSection(IniSection Section)
    {
        if (Section != null)
        {
            try
            {
                m_sections.Remove(Section.Name);
                changed = true;
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        return false;
    }

    //  Removes all existing sections, returns trus on success
    public bool RemoveAllSections()
    {
        m_sections.Clear();
        return (m_sections.Count == 0);
    }

    // Returns an IniSection to the section by name, NULL if it was not found
    public IniSection GetSection(string sSection)
    {
        sSection = sSection.Trim();
        // Trim spaces
        if (m_sections.ContainsKey(sSection))
        {
            return (IniSection)m_sections[sSection];
        }
        return null;
    }
    public IniSection GetSection(int i)
    {
        int j = 0;
        foreach (var sec in m_sections)
            if (j++ == i) return sec.Value;
        return null;
    }

    //  Returns a KeyValue in a certain section
    public string GetKeyValue(string sSection, string sKey)
    {
        IniSection s = GetSection(sSection);
        if (s != null)
        {
            IniSection.IniKey k = s.GetKey(sKey);
            if (k != null)
            {
                return k.Value;
            }
        }
        return String.Empty;
    }

    // Sets a KeyValuePair in a certain section
    public bool SetKeyValue(string sSection, string sKey, string sValue)
    {
        IniSection s = AddSection(sSection);
        if (s != null)
        {
            IniSection.IniKey k = s.AddKey(sKey);
            if (k != null)
            {
                if (k.Value != sValue)
                {
                    k.Value = sValue;
                    changed = true;
                }
                return true;
            }
        }
        return false;
    }

    // Renames an existing section returns true on success, false if the section didn't exist or there was another section with the same sNewSection
    public bool RenameSection(string sSection, string sNewSection)
    {
        //  Note string trims are done in lower calls.
        bool bRval = false;
        IniSection s = GetSection(sSection);
        if (s != null)
        {
            bRval = s.SetName(sNewSection);
        }
        return bRval;
    }

    // Renames an existing key returns true on success, false if the key didn't exist or there was another section with the same sNewKey
    public bool RenameKey(string sSection, string sKey, string sNewKey)
    {
        //  Note string trims are done in lower calls.
        IniSection s = GetSection(sSection);
        if (s != null)
        {
            IniSection.IniKey k = s.GetKey(sKey);
            if (k != null)
            {
                return k.SetName(sNewKey);
            }
        }
        return false;
    }

    private class compare_str : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {

            if (x == null && x == null)
                return true;
            else if (x == null || x == null)
                return false;
            return x.Equals(y, StringComparison.InvariantCultureIgnoreCase);
        }

        public int GetHashCode(string obj)
        {
            return (obj + "").ToLower().GetHashCode();
        }
    }
    // IniSection class 
    public class IniSection
    {
        public void Save(string fileName)
        {
            m_pIniFile.Save(fileName);
        }
        public void Save()
        {
            m_pIniFile.Save();
        }
        public string SaveString()
        {
            return m_pIniFile.SaveString();
        }
        public IniSection Load(string sFileName, bool bMerge = false)
        {
            m_pIniFile = m_pIniFile.Load(sFileName, bMerge);
            var sec = m_pIniFile.AddSection(m_sSection);
            this.m_keys = sec.m_keys;
            return this;
        }
        public IniSection LoadString(string str, bool bMerge = false)
        {
            m_pIniFile = m_pIniFile.LoadString(str, bMerge);
            var sec = m_pIniFile.AddSection(m_sSection);
            this.m_keys = sec.m_keys;
            return this;
        }
        public bool changed { get { return m_pIniFile.changed; } }


        //  IniFile IniFile object instance
        private IniFile m_pIniFile;
        //  Name of the section
        private string m_sSection;
        //  List of IniKeys in the section
        private Dictionary<string, IniKey> m_keys;

        // Constructor so objects are internally managed
        protected internal IniSection(IniFile parent, string sSection)
        {
            m_pIniFile = parent;
            m_sSection = sSection;
            m_keys = new Dictionary<string, IniKey>(comparer: new compare_str());
        }
        public IniSection(string name = "data")
        {
            m_sSection = name;
            m_keys = new Dictionary<string, IniKey>(comparer: new compare_str());
            m_pIniFile = new IniFile();
            m_pIniFile.m_sections.Add(name, this);
        }

        // Returns and hashtable of keys associated with the section
        public System.Collections.ICollection Keys
        {
            get
            {
                return m_keys.Values;
            }
        }

        public string this[string key]
        {
            get { return GetValue(key); }
            set { AddKey(key, value); }
        }
        /// <summary>
        /// get a not-null value from given keys, checks one by one in order
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[params string[] key]
        {
            get
            {
                foreach (var k in key)
                {
                    var res = GetValue(k);
                    if (res != null) return res;
                }
                return null;
            }
        }

        // Returns the section name
        public string Name
        {
            get
            {
                return m_sSection;
            }
        }
        public IniFile IniFile
        {
            get
            {
                return m_pIniFile;
            }
        }

        // Adds a key to the IniSection object, returns a IniKey object to the new or existing object
        public IniKey AddKey(string sKey, object value = null)
        {
            sKey = (sKey + "").Trim();
            IniSection.IniKey k = null;
            if (sKey.Length != 0)
            {
                sKey = sKey
                    .Replace("\r\n", "┘")
                    .Replace("\n", "┘")
                    .Replace("[", "┤")
                    .Replace("]", "├");
                if (m_keys.ContainsKey(sKey))
                {
                    k = (IniKey)m_keys[sKey];
                }
                else
                {
                    k = new IniSection.IniKey(this, sKey);
                    m_keys[sKey] = k;
                    m_pIniFile.changed = true;
                }
                if (value != null)
                    k.SetValue(value + "");
            }

            return k;
        }

        // Removes a single key by string
        public bool RemoveKey(string sKey)
        {
            return RemoveKey(GetKey(sKey));
        }

        // Removes a single key by IniKey object
        public bool RemoveKey(IniKey Key)
        {
            if (Key != null)
            {
                try
                {
                    m_keys.Remove(Key.Name);
                    m_pIniFile.changed = true;
                    return true;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }
            return false;
        }

        // Removes all the keys in the section
        public bool RemoveAllKeys()
        {
            m_keys.Clear();
            return (m_keys.Count == 0);
        }

        // Returns a IniKey object to the key by name, NULL if it was not found
        public IniKey GetKey(string sKey)
        {
            sKey = sKey.Trim();
            if (m_keys.ContainsKey(sKey))
            {
                return (IniKey)m_keys[sKey];
            }
            return null;
        }

        public string GetValue(string sKey)
        {
            sKey = sKey.Trim();
            if (m_keys.ContainsKey(sKey))
            {
                return (((IniKey)m_keys[sKey])?.Value + "")
                    .Replace("┘", "\r\n")
                    .Replace("┘", "\n")
                    .Replace("┤", "[")
                    .Replace("├", "]");
            }
            return null;
        }

        // Sets the section name, returns true on success, fails if the section
        // name sSection already exists
        public bool SetName(string sSection)
        {
            sSection = sSection.Trim();
            if (sSection.Length != 0)
            {
                // Get existing section if it even exists...
                IniSection s = m_pIniFile.GetSection(sSection);
                if (s != this && s != null) return false;
                try
                {
                    // Remove the current section
                    m_pIniFile.m_sections.Remove(m_sSection);
                    // Set the new section name to this object
                    m_pIniFile.m_sections[sSection] = this;
                    if (sSection != m_sSection)
                        m_pIniFile.changed = true;
                    // Set the new section name
                    m_sSection = sSection;
                    return true;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }
            return false;
        }

        // Returns the section name
        public string GetName()
        {
            return m_sSection;
        }

        // IniKey class
        public class IniKey
        {
            //  Name of the Key
            private string m_sKey;
            //  Value associated
            private string m_sValue;
            //  Pointer to the parent CIniSection
            private IniSection m_section;

            // Constuctor so objects are internally managed
            protected internal IniKey(IniSection parent, string sKey)
            {
                m_section = parent;
                m_sKey = sKey;
            }

            // Returns the name of the Key
            public string Name
            {
                get
                {
                    return m_sKey;
                }
            }

            // Sets or Gets the value of the key
            public String Value
            {
                get
                {
                    return m_sValue;
                }
                set
                {
                    if (value != m_sValue)
                    {
                        m_sValue = value;
                        m_section.m_pIniFile.changed = true;
                    }
                }
            }

            // Sets the value of the key
            public void SetValue(string sValue)
            {
                if (sValue != m_sValue)
                {
                    m_section.m_pIniFile.changed = true;
                    m_sValue = sValue;
                }
            }
            // Returns the value of the Key
            public string GetValue()
            {
                return m_sValue;
            }

            // Sets the key name
            // Returns true on success, fails if the section name sKey already exists
            public bool SetName(string sKey)
            {
                sKey = sKey.Trim();
                if (sKey.Length != 0)
                {
                    IniKey k = m_section.GetKey(sKey);
                    if (k != this && k != null) return false;
                    try
                    {
                        // Remove the current key
                        m_section.m_keys.Remove(m_sKey);
                        // Set the new key name to this object
                        m_section.m_keys[sKey] = this;
                        if (sKey != m_sKey)
                            m_section.m_pIniFile.changed = true;
                        // Set the new key name
                        m_sKey = sKey;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex.Message);
                    }
                }
                return false;
            }

            // Returns the name of the Key
            public string GetName()
            {
                return m_sKey;
            }
        } // End of IniKey class
    } // End of IniSection class

    internal void AppendKeys(IniFile ini)
    {
        foreach (IniSection sec in ini.Sections)
        {
            var sec_ = GetSection(sec.Name);
            if (sec_ != null)
            {
                foreach (IniSection.IniKey k in sec.Keys)
                    sec_.AddKey(k.Name, k.Value);
            }
        }
    }
} // End of IniFile class


public class IniSection : IniFile.IniSection
{
}

