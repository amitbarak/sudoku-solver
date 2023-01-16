using SudokuSolver2.IOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.InputHandling
{
    /// <summary>
    /// This is the main class of the application.
    /// </summary>
    /// <remarks>
    /// In here the application is started and the input is handled.
    /// first we define handlers for the input.
    /// </remarks>
    public class Program
    {
        /// <summary>
        /// This is the main method of the application.
        /// </summary>
        /// <remarks>
        /// this method is used to run the application.
        /// </remarks>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //sets the objects necessary to handle the input.
            List<IWriter> resultWriters;
            string input;
            ConsoleHandler consoleHandler = new();
            while (true)
            {
                //clears the console
                consoleHandler.Clear();
                Console.WriteLine("C:\\Users\\USER-HP1\\Downloads\\sudoku_example (2).txt");
                //gets the input from the user
                input = UserHandlingHelper.GetInput(consoleHandler);
                //sets a list of writers to write the result to (for example console and file)
                resultWriters = UserHandlingHelper.SetOutputList();



                consoleHandler.WriteLine("result: \n");

                //validates and handles the input
                UserHandlingHelper.HandleInput(input, resultWriters);


                UserHandlingHelper.Wait(consoleHandler);
            }
        }



    }
}
