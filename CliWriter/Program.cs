using System;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Collections;

namespace CliToolSpace
{
    class _Main
    {
        static void Main(string[] args)
        {
            Cli commandLine = new Cli();
            commandLine.FileToCli(@"C:\Users\Faizaan Chishtie\Desktop\dump\installer.bat");
            commandLine.Execute();
            commandLine.ToFile(@"C:\Users\Faizaan Chishtie\Desktop\dump\");
            Pause();
        }

        public static void Pause()
        {
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }
    }
}
