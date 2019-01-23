using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliToolSpace
{
    /// <summary>
    /// ErrorHandler class deals with parameter checking through CliToolSpace
    /// </summary>
    class ErrorHandler
    {
        /// <summary>
        /// PathCheck method checks if path is null or empty
        /// </summary>
        /// <param name="path">String containing path</param>
        public static void PathCheck(string path)
        {
            if (path == null || path == "")
            {
                throw new ArgumentNullException("Path null or empty");
            }
        }
    }

}
