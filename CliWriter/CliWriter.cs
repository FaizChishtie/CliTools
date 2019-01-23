using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliToolSpace
{


    /// <summary>
    /// CliWriter class holds a library of static methods that interact with the command line using Command objects.
    /// </summary>
    class CliWriter
    {

        //WRITE TO FILE

        /// <summary>
        /// Creates a new file if file doesn't exist.
        /// </summary>
        /// <param name="path">Path to new file</param>
        private static void CreateFile(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    using (FileStream fs = File.Create(path))
                    {
                        ;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        /// <summary>
        /// Log function logs all information passed through into current_directory/log.txt.
        /// </summary>
        /// <param name="info">Information to be logged</param>
        /// <param name="log">Boolean determining whether to log or not</param>
        /// <returns></returns>
        private static string Log(string info, bool log) //log each creation
        {
            if (!log) { return ""; } //if logging is turned off
            string path = @Directory.GetCurrentDirectory() + "/log.txt";
            CreateFile(path);
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(path, true))
            {
                file.WriteLine(info);
            }
            return info;
        }


        /// <summary>
        /// GetTimestamp function returns a timestamp for Log and CommandToFile methods.
        /// </summary>
        /// <param name="now">DateTime object to create time stamp off</param>
        /// <returns></returns>
        private static string GetTimestamp(DateTime now) //helper method
        {
            return now.ToString("yyyy-MM-dd  HH:mm:ss");
        }


        /// <summary>
        /// ParseCommands function splits given commands at the - and.
        /// </summary>
        /// <param name="commands">Command object with commands to be split at - and</param>
        /// <returns>Returns a string array of commands</returns>
        public static string[] ParseCommands(Command commands)
        {
            string strCommands = commands.ToString();
            return strCommands.Split('&');
        }


        /// <summary>
        /// CommandToFile sends a given Command object to a specified file
        /// </summary>
        /// <param name="commands">Command object to be sent to file</param>
        /// <param name="directory">Directory to write file to</param>
        /// <param name="log">Boolean determines whether log should be written to</param>
        /// <param name="filename">Name of file to be written to</param>
        public static void CommandToFile(Command commands, string directory, bool log = true, string filename = "cli_generated_commands.txt") //call log method
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string timeStamp = GetTimestamp(DateTime.Now);
            string info = "::CLI GENERATED DOCUMENT\n::User " + userName + "\n::Filename - " + filename + "\n::Directory - " + directory + "\n::Date - " + timeStamp + "\n";
            string path = @directory + "/" + filename;
            CreateFile(path);
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(path, true))
            {
                foreach (string cmnd in ParseCommands(commands))
                {
                    file.WriteLine(cmnd.Trim().Replace("/C", ""));
                }
            }
        }

        //WRITE TO FILE

    }

}
