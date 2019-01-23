using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliToolSpace
{

    /// <summary>
    /// Command class holds commands to be executed by command line.
    /// </summary>
    class Command
    {

        //Class variables
        private string command;


        /// <summary>
        /// Constructor for Command object with parameters.
        /// </summary>
        /// <param name="command">String of batch file code</param>
        /// <param name="hidden">Boolean that toggles the visibility of script execution</param>
        public Command(string command, bool hidden = false)
        {
            this.command = command;
            if (!IsCorrectFormat(command))
            {
                Format();
            }
            this.Hidden = hidden;
        }


        /// <summary>
        /// Default constructor for Command object, usually used if default object is needed.
        /// </summary>
        public Command() //default constructor
        {
            command = "/C ";
            Hidden = false;
        }


        /// <summary>
        /// Overridden ToString method for Command object.
        /// </summary>
        /// <returns>Returns the command this Command object is storing</returns>
        public override string ToString()
        {
            return command;
        }


        /// <summary>
        /// IsCorrectFormat method determines whether a command begins with a /C so that it is executable.
        /// </summary>
        /// <param name="_command">Command to be tested</param>
        /// <returns>Returns true if is correct format</returns>
        private static bool IsCorrectFormat(string _command) //check if command format is okay
        {
            int indBs = _command.IndexOf("/");
            int indC = _command.IndexOf("C");
            int comparison = indC - indBs; //if not found, indexOf returns -1
            return comparison == 1;
        }


        /// <summary>
        /// Private Format method formats a command by adding /C at front.
        /// </summary>
        private void Format()
        {
            command = "/C " + command; //concatenation of "/C" and current command
        }


        /// <summary>
        /// Public Format method creates a new Command object and runs private Format method. Static method to enable "Command.Format(arg)" functionality.
        /// </summary>
        /// <param name="command">Command to be formatted</param>
        /// <returns>Returns formatted command</returns>
        public static string Format(string command)
        {
            Command c = new Command(command);
            return c.ToString();
        }


        /// <summary>
        /// Hidden parameter getter and setter.
        /// </summary>
        public bool Hidden { get; set; }


        /// <summary>
        /// Setter for the command parameter.
        /// </summary>
        /// <param name="oCmnd">Other command to replace current command</param>
        public void setCommand(string oCmnd)
        {
            command = oCmnd;
        }

        /// <summary>
        /// Getter for the command parameter.
        /// </summary>
        public string GetCommand()
        {
            return command;
        }

        /// <summary>
        /// AddPause method adds an - and pause - to any command
        /// </summary>
        /// <returns>Returns this command object</returns>
        public Command AddPause()
        {
            command = command + " & pause";
            return this;
        }


        /// <summary>
        /// Command overloaded + operator concatenates two Command objects by using the and operator. Command a executes to completion before b.
        /// </summary>
        /// <param name="alp">Command to be executed first</param>
        /// <param name="blp">Command to be executed second</param>
        /// <returns>Returns a new command object with both commands</returns>
        public static Command operator +(Command alp, Command blp)
        {
            //DELETEM
            //Console.WriteLine(alp + " - " + blp);
            bool hidden = alp.Hidden || blp.Hidden;
            string bCmnd = blp.command;
            if (IsCorrectFormat(bCmnd))
            {
                blp.setCommand(bCmnd.Substring(bCmnd.IndexOf("C") + 1)); //remove /C if exists
            }
            string cmndOut = alp.ToString() + " & " + blp.ToString();
            return new Command(cmndOut, hidden);
        }

        /// <summary>
        /// Append method directly adds (using - and) the command parameter of one command(rhs) to another(lhs).
        /// </summary>
        /// <param name="iCommand">Command to be appended to</param>
        /// <param name="oCommand">Command to be appended</param>
        /// <returns>Returns Command object with both command parameters appended</returns>
        public static Command Append(Command iCommand, Command oCommand)
        {
            iCommand.command += " & " + oCommand.command;
            return iCommand;
        }


    }

}
