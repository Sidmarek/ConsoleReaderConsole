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
        [STAThread]
        static void Main(string[] args)
        {
            bool control = false;
            while (!control) 
            {
                if (Clipboard.ContainsText(TextDataFormat.Text))
                {
                    string lastCliboardString = Clipboard.GetText();
                    if (Uri.IsWellFormedUriString(lastCliboardString, UriKind.Absolute))
                    {
                        Console.WriteLine("URL " + Clipboard.GetText());
                        Process p = new Process();
                        p.StartInfo.FileName = "youtube-dl.exe";
                        p.StartInfo.Arguments = lastCliboardString;
                        p.Start();
                        control = true;
                    }
                    else
                    {
                        Console.WriteLine("Text " + Clipboard.GetText());
                    }
                    System.Threading.Thread.Sleep(5000);
                }
                else
                {
                    Console.WriteLine("no text in clipboard");
                    System.Threading.Thread.Sleep(5000);
                }

            }
        }

    }
}
