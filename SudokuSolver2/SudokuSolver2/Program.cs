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
        static readonly Stopwatch timer = new Stopwatch();

        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("C:\\Users\\USER-HP1\\Downloads\\sudoku_example (2).txt");
                ConsoleHandler consoleHandler = new ConsoleHandler();
                String input = getInput(consoleHandler);
                List<IWriter> resultWriters = SetOutputList();

                
                handleInput(input, resultWriters);
            }
        }

        /// <summary>
        /// sets the List of writers the program should output to
        /// </summary>
        /// <returns></returns>
        public static List<IWriter> SetOutputList()
        {
            ConsoleHandler consoleHandler = new ConsoleHandler();
            List<IWriter> resultWriters = new List<IWriter>();
            resultWriters.Add(consoleHandler);
            consoleHandler.Write("do you want to save the board in a file?");
            consoleHandler.Write("type f for file or enter otherwise:");
            String answer = consoleHandler.Read();
            FileHandler outputFile = null;
            //file was selected
            if (answer == "f")
            {
                consoleHandler.Write("type the address of the file this will go into:");
                String address = consoleHandler.Read();
                outputFile = new FileHandler(address);
                resultWriters.Add(outputFile);
            }
            consoleHandler.Write("");
            return resultWriters;
        }



        /// <summary>
        /// this function handles a string that represents a board
        /// and prints it to the result writers
        /// </summary>
        /// <param name="input">a string that represents a board</param>
        /// <param name="resultWriters">writers to print the result</param>
        public static void handleInput(String input, List<IWriter> resultWriters)
        {
            if (!InputValidation.IsValid(input))
            {
                foreach (IWriter writer in resultWriters)
                {
                    writer.Write(GeneralValues.error_message);
                }
                return;
            }
            Board board = new Board(input);

            Console.WriteLine(board);
            timer.Start();
            DLXSolver.DancingLinksSolver.Solve(board, resultWriters);
            Console.WriteLine("ElapsedMilliseconds: " + timer.ElapsedMilliseconds);
            //SolveAndPrint(board, resultWriters);
        }

        public static void SolveAndPrint(Board board, List<IWriter> outputList)
        {

            timer.Restart();
            bool isSolution = Solver.Solve(board);
            Console.WriteLine("miliseconds:" + timer.ElapsedMilliseconds.ToString());
            timer.Stop();
            String outPut = "";
            if (isSolution)
            {
                outPut = board.ToString();
            }
            else
            {
                outPut = "board is unsolvable";
            }


            foreach (IWriter writer in outputList)
            {
                try
                {
                    writer.Write(outPut);
                }
                catch (System.IO.IOException e)
                {
                    ConsoleHandler c = new ConsoleHandler();
                    c.Write("could not write to your chosen output method");
                }
            }

        }


        /// <summary>
        /// gets the string from the user
        /// </summary>
        /// <param name="IOhandler">an input output handler</param>
        /// <returns></returns>
        public static String getInput(IInputOutput IOhandler)
        {
            String input = "";
            while (input.Equals(""))
            {
                IOhandler.Write("where should the board be taken from?");
                IOhandler.Write("c for console, f for file");
                String answer = IOhandler.Read();
                if (answer == "c")
                {
                    IOhandler.Write("type the board");
                    input = IOhandler.Read();
                }
                else if (answer == "f")
                {
                    IOhandler.Write("please type the address:");
                    String address = input = IOhandler.Read();
                    FileHandler f = new FileHandler(address);
                    try
                    {
                        input = f.Read();
                    }
                    catch (Exception e) when (
                    e is FileNotFoundException ||
                    e is FileLoadException
                    )
                    {
                        IOhandler.Write("file is unable to load or does not exist");
                        input = "";
                    }
                }
                //invalid input
                else
                {
                    IOhandler.Write("wrong input, please type c or f");
                }
            }


            return input;
        }


    }
}
