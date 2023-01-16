using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SudokuSolver2.BoardObjects;
using SudokuSolver2.IOs;

namespace SudokuSolver2.InputHandling
{
    /// <summary>
    /// This class is used to handle the user input and output.
    /// </summary>
    public class UserHandlingHelper
    {

        static readonly Stopwatch timer = new();

        /// <summary>
        /// sets the List of writers the program should output to
        /// </summary>
        /// <returns></returns>
        public static List<IWriter> SetOutputList()
        {
            ConsoleHandler consoleHandler = new();
            List<IWriter> resultWriters = new()
            {
                consoleHandler
            };
            consoleHandler.WriteLine("do you want to save the board in a file?");
            consoleHandler.WriteLine("type f for file or enter otherwise:");
            string answer = "";
            try
            {
                answer = consoleHandler.ReadLine();
            }

            //the user has not typed f
            catch (Exception e) when (e is IOException) { }

            FileHandler outputFile;
            //file was selected
            if (answer == "f")
            {
                consoleHandler.WriteLine("type the address of the file this will go into:");
                string address;
                try
                {
                    address = consoleHandler.ReadLine();
                }

                //the user has not typed f
                catch (Exception e) when (e is IOException)
                {
                    return resultWriters;
                }
                outputFile = new FileHandler(address);
                resultWriters.Add(outputFile);
            }
            consoleHandler.WriteLine("");
            return resultWriters;
        }



        /// <summary>
        /// this function handles a string that represents a board
        /// and prints it to the result writers
        /// </summary>
        /// <param name="input">a string that represents a board</param>
        /// <param name="resultWriters">writers to print the result</param>
        public static void HandleInput(string input, List<IWriter> resultWriters)
        {
            if (!InputValidation.IsValid(input))
            {
                WriteToAll(resultWriters, GeneralValues.ErrorMessage);
                return;
            }
            Board board = new(input);

            //Console.WriteLine(board); //this line is very usefull for debugging

            SolveAndPrint(board, resultWriters);

        }


        /// <summary>
        /// solves the board and prints the result to the result writers
        /// </summary>
        /// <param name="board">Board object</param>
        /// <param name="outputList">a list of objects to output to</param>
        public static void SolveAndPrint(Board board, List<IWriter> outputList)
        {
            //checking the board is valid
            if (!board.IsValid())
            {
                WriteToAll(outputList, "board is not valid");
            }
            timer.Restart();
            Board? solution = DLXSolver.DancingLinksSolver.Solve(board);
            Console.WriteLine("miliseconds:" + timer.ElapsedMilliseconds.ToString());
            timer.Stop();
            string outPut = "";
            if (solution != null)
            {
                outPut = solution.ToString();
            }
            else
            {
                outPut = "board is unsolvable";
            }
            WriteToAll(outputList, outPut);




        }

        /// <summary>
        /// prints the outPut to all outputList
        /// </summary>
        /// <param name="outputList">a list of outputs</param>
        /// <param name="outPut">String to output</param>
        public static void WriteToAll(List<IWriter> outputList, string outPut)
        {
            foreach (IWriter writer in outputList)
            {
                try
                {
                    writer.WriteLine(outPut);
                }
                catch (IOException)
                {
                    ConsoleHandler c = new ConsoleHandler();
                    Console.Write("could not write to your chosen output method");
                }
            }
        }


        /// <summary>
        /// gets the string from the user
        /// </summary>
        /// <param name="IOhandler">an input output handler with a valid writing method</param>
        /// <returns></returns>
        public static string GetInput(IInputOutput IOhandler)
        {
            string answer;
            string input = "";
            while (input.Equals(""))
            {
                IOhandler.WriteLine("where should the board be taken from?");
                IOhandler.WriteLine("c for console, f for file");
                try
                {
                    answer = IOhandler.ReadLine();
                }
                catch (Exception e) when (e is IOException)
                {
                    Console.WriteLine("your answer was not accepted");
                    continue;
                }

                //if the user wanted to get input from the console
                if (answer == "c")
                {
                    IOhandler.WriteLine("type the board");
                    try
                    {
                        input = IOhandler.ReadLine();
                    }
                    catch (Exception e) when (e is IOException)
                    {
                        Console.WriteLine("invalid input");
                        continue;
                    }
                }

                //if the user wanted to get input from a file
                else if (answer == "f")
                {
                    IOhandler.WriteLine("please type the address:");
                    try
                    {
                        string address = IOhandler.ReadLine();
                        FileHandler f = new FileHandler(address);
                        input = f.ReadLine();
                    }
                    catch (Exception e) when (
                    e is IOException
                    )
                    {
                        IOhandler.WriteLine("encounterd problems when reading the file");
                        input = "";
                    }
                }
                //invalid input
                else
                {
                    IOhandler.WriteLine("wrong input, please type c or f");
                }
            }


            return input;
        }



        /// <summary>
        /// waites for the user to press enter and writes hum that
        /// </summary>
        /// <param name="consoleHandler"></param>
        public static void Wait(ConsoleHandler consoleHandler)
        {

            consoleHandler.WriteLine("press enter to continue");
            consoleHandler.ReadEmpty();
        }
    }


}
