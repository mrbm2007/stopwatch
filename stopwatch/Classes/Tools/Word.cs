using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace stopwatch
{
    public class WordEditor : IDisposable
    {
        string FileName;
        string dir;
        string Content = "";
        public WordEditor(string fileName)
        {

            this.FileName = fileName;
            while (ExcelEditor.IsFileLocked(FileName))
                Form_msg.Show(null, "فایل باز است:" + "\r\n" + FileName + "\r\n" + "لطفا آن را ببندید");

            dir = Path.GetTempPath() + "\\stp-" + Path.GetFileNameWithoutExtension(FileName) + "\\";
            Zip.UnZipFiles(FileName, dir, deleteZipFile: false);
            Content = File.ReadAllText(dir + "word\\document.xml");
        }
        public void Replace(string str1, string str2)
        {
            Content = Content.Replace(str1, str2);
        }
        public void Close()
        {
            if (File.Exists(FileName))
                File.Delete(FileName);
            File.WriteAllText(dir + "word\\document.xml", Content);
            Zip.CreateZip(dir, FileName);
            try
            {
                Directory.Delete(dir, true);
            }
            catch { }
        }
        public void Dispose()
        {
            Close();
        }
    }
}
