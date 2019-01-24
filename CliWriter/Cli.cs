using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliToolSpace
{


    /// <summary>
    ///
    /// </summary>
    class Cli
    {
        ArrayList cmndAL; //stores all commands in AL

        //COMMAND STORAGE

        /// <summary>
        /// Constructor for Cli class, initializes ArrayList
        /// </summary>
        public Cli()
        {
            cmndAL = new ArrayList();
        }


        /// <summary>
        /// StoreCommand method adds command to ArrayList
        /// </summary>
        /// <param name="cmnd">Command to be added to ArrayList</param>
        public void StoreCommand(Command cmnd)
        {
            cmndAL.Add(cmnd);
        }


        /// <summary>
        /// ToCommand converts current ArrayList to a command object.
        /// </summary>
        /// <returns>Returns a Command object</returns>
        private Command ToCommand()
        {
            Command tmp = (Command)cmndAL[0];
            for (int i = 1; i < cmndAL.Count - 1; i++)
            {
                tmp += (Command)cmndAL[i];
            }
            return tmp;
        }


        /// <summary>
        /// ToFile takes current ArrayList and converts it to a file
        /// </summary>
        /// <param name="directory">Directory to store file</param>
        /// <param name="log">Boolean to determine if action is to be logged</param>
        /// <param name="filename">Filename to write to</param>
        public void ToFile(string directory, bool log = true, string filename = "out.bat")
        {
            Command tmp = ToCommand();
            CliWriter.CommandToFile(tmp, directory, log, filename);
        }


        /// <summary>
        /// FileToCli parses a file to Cli ArrayList
        /// </summary>
        /// <param name="path">Path to given file</param>
        public void FileToCli(string path)
        {
            Command commands = CliReader.ReadCommandsFromFile(path);
            string[] strComs = CliWriter.ParseCommands(commands);
            foreach (string strCommand in strComs)
            {
                StoreCommand(new Command(strCommand));
            }
        }

        //COMMAND STORAGE

        //COMMANDS - ALL COMMANDS MUST RETURN TYPE "Command"

        /// <summary>
        /// cd command acts as actual cd command in cli.
        /// </summary>
        /// <param name="directory">Directory to navigate to</param>
        /// <returns>Returns new command object with command for executability.</returns>
        public static Command cd(string directory)
        {
            string path;
            path = "/C cd\\ && cd " + directory;
            return new Command(path);
        }

        /// <summary>
        /// ls command acts like dir in cli
        /// </summary>
        /// <returns>Returns new command object with command</returns>
        public static Command ls()
        {
            string dir = "/C dir";
            return new Command(dir);
        }

        //COMMANDS

        //EXECUTION

        /// <summary>
        /// Execute method executes commands from cmndAL. 
        /// </summary>
        /// <param name="cmnd">Command object to be executed</param>
        /// <param name="pause">Boolean adds pause at the end of execution if true</param>
        public void Execute(bool pause = false)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Command commands = ToCommand();
            ExecuteIndividual(commands, pause);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            Console.WriteLine($"Commands executed \nTime elapsed: {elapsedTime}");
        }

        /// <summary>
        /// Execute method executes a given command. Calls Hidden or Visible based on the boolean value of command.
        /// </summary>
        /// <param name="cmnd">Command object to be executed</param>
        /// <param name="pause">Boolean adds pause at the end of execution if true</param>
        public static void ExecuteIndividual(Command cmnd, bool pause = true)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (cmnd.Hidden == true)
            {
                ExecuteCommandHidden(cmnd, pause);
            }
            else
            {
                ExecuteCommandVisible(cmnd, pause);
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            Console.WriteLine("Command \'" + cmnd + "\' executed " + "\nTime elapsed: " + elapsedTime);
        }


        /// <summary>
        /// ExecuteCommandHidden hides cmd when executing command.
        /// </summary>
        /// <param name="cmnd">Command object to be executed</param>
        /// <param name="pause">Boolean adds pause at the end of execution if true</param>
        private static void ExecuteCommandHidden(Command cmnd, bool pause = true)
        {
            CheckPause(cmnd, pause); // adds pause if true
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = cmnd.ToString();
            process.StartInfo = startInfo;
            process.Start();
        }


        /// <summary>
        /// ExecuteCommandVisible shows cmd when executing command.
        /// </summary>
        /// <param name="cmnd">Command object to be executed</param>
        /// <param name="pause">Boolean adds pause at the end of execution if true</param>
        private static void ExecuteCommandVisible(Command cmnd, bool pause = true)
        {
            CheckPause(cmnd, pause);
            System.Diagnostics.Process.Start("CMD.exe", cmnd.ToString());
        }


        /// <summary>
        /// CheckPause modifies Command object to add "and pause" if pause is true.
        /// </summary>
        /// <param name="cmnd">Command to be modified</param>
        /// <param name="pause">Boolean pause adds pause if true</param>
        /// <returns></returns>
        private static Command CheckPause(Command cmnd, bool pause)
        {
            if (pause)
            {
                cmnd.AddPause();
            }
            return cmnd;
        }

        //EXECUTION

    }
}
