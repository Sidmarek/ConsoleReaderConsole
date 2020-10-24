using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleReaderConsole
{
    class Program
    {
        private static string saveDirPathYoutube = string.Empty;
        private static string saveDirPathFacebook = string.Empty;
        private static string saveDirPathOther = string.Empty;
        private static string youTubeDLPath = string.Empty;

        public static string YouTubeDLPath { get => youTubeDLPath; set => youTubeDLPath = value; }
        public static string SaveDirPathFacebook { get => saveDirPathFacebook; set => saveDirPathFacebook = value; }
        public static string SaveDirPathYoutube { get => saveDirPathYoutube; set => saveDirPathYoutube = value; }
        public static string SaveDirPathOther { get => saveDirPathOther; set => saveDirPathOther = value; }

        [STAThread]
        static void Main(string[] args)
        {
            readConfig();
            bool control = false;
            string lastCliboardString = string.Empty;
            while (!control) 
            {
                if (Clipboard.ContainsText(TextDataFormat.Text))
                {
                    if(Clipboard.GetText() == lastCliboardString)
                    {
                        continue;
                    }
                    else
                    {
                        lastCliboardString = Clipboard.GetText();
                    }
                    if (Uri.IsWellFormedUriString(lastCliboardString, UriKind.Absolute))
                    {
                        Console.WriteLine("URL " + Clipboard.GetText());
                        Process p = new Process();
                        p.StartInfo.FileName =  youTubeDLPath +  "youtube-dl.exe";
                        p.StartInfo.Arguments = lastCliboardString + " --output " + SaveDirPathOther + "%(title)s.%(ext)s";
                        if (lastCliboardString.Contains("youtube.com"))
                        {
                            p.StartInfo.Arguments = lastCliboardString + " --output " + SaveDirPathYoutube + "%(title)s.%(ext)s";
                        }
                        if (lastCliboardString.Contains("facebook.com"))
                        {
                            p.StartInfo.Arguments = lastCliboardString + " --output " + SaveDirPathFacebook + "%(title)s.%(ext)s";
                        }
                        p.Start();
                    }
                    else
                    {
                        Console.WriteLine("Text " + Clipboard.GetText());
                    }
                }

            }
        }

        private static void readConfig()
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines("config.txt");

                foreach (var line in lines)
                {
                    var splitedLine = line.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    if (splitedLine[0].Length <= 0)
                        Console.WriteLine("Invalid config argument check if is it not onyly whitespace of new line.");
                    else
                    {
                        if (splitedLine[0].Contains("YouTubeDL"))
                        {
                            YouTubeDLPath = splitedLine[1];
                        }
                        if (splitedLine[0].Contains("YoutubeDir"))
                        {
                            SaveDirPathYoutube = splitedLine[1];
                        }
                        if (splitedLine[0].Contains("FacebookDir"))
                        {
                            SaveDirPathFacebook = splitedLine[1];
                        }
                        if (splitedLine[0].Contains("OtherDir"))
                        {
                            SaveDirPathFacebook = splitedLine[1];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in reading config.txt check if is located next to this program and that has all paths written as in original txt file.");
                System.Threading.Thread.Sleep(5000);
                throw e;
            }
        }
    }
}
