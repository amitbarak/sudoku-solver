using SudokuSolver2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2
{
    class Program
    {

        static void Main(string[] args)
        {
            List<IWriter> resultWriters;
            String input;
            ConsoleHandler consoleHandler = new ConsoleHandler();
            while (true)
            {
                //clears the console
                Console.Clear();
                Console.WriteLine("C:\\Users\\USER-HP1\\Downloads\\sudoku_example (2).txt");
                //gets the input from the user
                input = userHandlingHelper.getInput(consoleHandler);
                //sets a list of writers to write the result to (for example console and file)
                resultWriters = userHandlingHelper.SetOutputList();

                //validates and handles the input
                userHandlingHelper.handleInput(input, resultWriters);
                Console.WriteLine("press enter to continue");
                Console.ReadLine();
            }
        }



    }
}
