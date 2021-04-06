using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;

namespace stopwatch
{
    public class Zip
    {
        public static void ZipFiles(String inputFolderPath, String outputFilePath, String password = "", string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            var Files = Directory.GetFiles(inputFolderPath, searchPattern, SearchOption.AllDirectories);
            using (var oZipStream = new ZipOutputStream(File.Create(outputFilePath))) // create zip stream
            {
                if (password != "")
                    oZipStream.Password = password;
                oZipStream.SetLevel(9); // maximum compression
                foreach (var file in Files) // for each file, generate a zipentry
                {
                    oZipStream.PutNextEntry(new ZipEntry(file.Substring(inputFolderPath.Length)) { IsUnicodeText = true });
                    using (var ostream = File.OpenRead(file))
                    {
                        var obuffer = new Byte[(int)ostream.Length];
                        ostream.Read(obuffer, 0, obuffer.Length);
                        ostream.Close();
                        oZipStream.Write(obuffer, 0, obuffer.Length);
                    }
                }
                oZipStream.Finish();
                oZipStream.Close();
            }
        }
        public static void ZipFiles(String[] Files, String outputFilePath, String password = "", String[] Names = null)
        {
            using (var oZipStream = new ZipOutputStream(File.Create(outputFilePath)))
            { // create zip stream
                if (password != "")
                    oZipStream.Password = password;
                oZipStream.SetLevel(9); // maximum compression 
                for (int i = 0; i < Files.Length; i++) // for each file, generate a zipentry
                {
                    var z = new ZipEntry(Names == null ? Path.GetFileName(Files[i]) : Names[i]) { IsUnicodeText = true };
                    oZipStream.PutNextEntry(z); 
                    var ostream = File.OpenRead(Files[i]);
                    var obuffer = new Byte[(int)ostream.Length];
                    ostream.Read(obuffer, 0, obuffer.Length);
                    ostream.Close();
                    oZipStream.Write(obuffer, 0, obuffer.Length);
                }
                oZipStream.Finish();
                oZipStream.Close();
            }
        }

        public static void UnZipFiles(String zipFilePath, String outputFolder, String password = "", bool deleteZipFile = false)
        {
            using (var s = new ZipInputStream(File.OpenRead(zipFilePath)))
            {
                if (password != "")
                    s.Password = password;
                if (outputFolder != "" && !Directory.Exists(outputFolder))
                    Directory.CreateDirectory(outputFolder);
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    theEntry.IsUnicodeText = true;
                    var directoryName = outputFolder;
                    var fileName = Path.GetFileName(theEntry.Name);
                    if (directoryName != "")
                        Directory.CreateDirectory(directoryName);
                    if (fileName != "" && theEntry.Name.IndexOf(".ini") < 0)
                    {
                        var fullPath = directoryName + "\\" + theEntry.Name;
                        fullPath = fullPath.Replace("\\ ", "\\");
                        var fullDirPath = Path.GetDirectoryName(fullPath);
                        if (!Directory.Exists(fullDirPath)) Directory.CreateDirectory(fullDirPath);
                        using (var streamWriter = File.Create(fullPath))
                        {
                            var data = new Byte[2048];
                            int size = data.Length;
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                    streamWriter.Write(data, 0, size);
                                else
                                    break;
                            }
                            streamWriter.Close();
                        }
                    }
                }
                s.Close();
                if (deleteZipFile)
                    File.Delete(zipFilePath);
            }
        }

        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="stZipPath">path of the archive wanted</param>
        /// <param name="stDirToZip">path of the directory we want to create, without ending backslash</param>
        public static void CreateZip(string directoryToZip, string zipFilePath)
        {
            var filenames = Directory.GetFiles(directoryToZip, "*.*", SearchOption.AllDirectories);
            using (var s = new ZipOutputStream(File.Create(zipFilePath)))
            {
                s.SetLevel(9);// 0 - store only to 9 - means best compression

                var buffer = new byte[4096];

                foreach (var file in filenames)
                {
                    var relativePath = file.Substring(directoryToZip.Length).TrimStart('\\');
                    var entry = new ZipEntry(relativePath) { DateTime = DateTime.Now, IsUnicodeText = true };
                    s.PutNextEntry(entry);

                    using (var fs = File.OpenRead(file))
                    {
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }
                s.Finish();
                s.Close();
            }
        }
    }

}