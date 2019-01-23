using CliToolSpace;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliToolSpace
{


    class CliReader
    {
        //READ FROM FILE

        /// <summary>
        /// ReadCommandsFromFile method reads commands from a given bat file/text file, converts to Command object.
        /// </summary>
        /// <param name="path">Path for file to read</param>
        /// <returns>Returns new Command object with commands from file</returns>
        public static Command ReadCommandsFromFile(string path)
        {
            ErrorHandler.PathCheck(path);
            string strCom = File.ReadAllText(path, Encoding.UTF8);
            string[] strComAr = strCom.Split('\n');
            strComAr = strComAr.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray(); //remove any whitespace or null indeces in array
            strCom = "";
            for (int i = 0; i < strComAr.Length; i++)
            {
                string tmp = strComAr[i];
                if (i == 0)
                {
                    strCom += tmp;
                }
                else
                {
                    strCom += " & " + tmp;
                }
            }
            return new Command(strCom);
        }


        /// <summary>
        /// StartFile method starts file at given path.
        /// </summary>
        /// <param name="path">Path of file to start</param>
        public static void StartFile(string path)
        {
            ErrorHandler.PathCheck(path);
            Process.Start(path);
        }

        //READ FROM FILE

    }
}
