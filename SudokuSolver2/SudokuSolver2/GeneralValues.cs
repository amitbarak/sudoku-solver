using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2
{

    /// <summary>
    /// a class to represent general values of the program
    /// </summary>
    public static class GeneralValues
    {
        // error message for the valdition
        public static String error_message = "";
        //the value of an empty char
        public static char emptyChar = '0';
        //the accepted row size of the board which will be set by in Validition class
        public static int acceptedSize = -1;
        //the possible lengths of input
        public static int[] validLengths = { 1, 16, 81, 256, 625 };
    }
}
