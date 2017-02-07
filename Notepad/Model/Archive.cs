using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Notepad.Model
{
    public class Archive
    {
        public String content { get; set; }
        public String path { get; set; }

        public Archive(String path, String content = "")
        {
            this.path = path;
            this.content = content;
        }

        public static String[] ReadFileToArray(String Path)
        {
            List<String> lines = new List<String>();

            using(StreamReader file =  new StreamReader(Path)){
                String line = String.Empty;
                while((line = file.ReadLine()) != null)
                {
                    lines.Add(line);
                }
                file.Close();
                return lines.ToArray();
            }
        }

        public static String ReadFileToString(String Path)
        {
            String lineFinal = String.Empty;

            using (StreamReader file = new StreamReader(Path))
            {
                String line = String.Empty;
                while ((line = file.ReadLine()) != null)
                {
                    lineFinal = String.Concat(lineFinal, Environment.NewLine, line);
                }
                file.Close();
                return lineFinal;
            }
        }

        public static Archive Read(String path)
        {
            return new Archive(path, ReadFileToString(path));
        }

        public Boolean Save()
        {
            return Archive.Save(this.path, this.content);
        }

        public Boolean Save(String path)
        {
            return Archive.Save(path, this.content);
        }

        public  static Boolean Save(Archive archive)
        {
            return Archive.Save(archive.path, archive.content);
        }

        public static Boolean Save(String path, String content)
        {
            try
            {
                using (StreamWriter outputFile = new StreamWriter(path))
                {
                     outputFile.WriteLine(content);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
