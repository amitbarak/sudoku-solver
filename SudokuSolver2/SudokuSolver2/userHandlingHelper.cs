using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2
{
    internal class userHandlingHelper
    {

        static readonly Stopwatch timer = new Stopwatch();
        
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
            String answer = "";
            try
            {
                answer = consoleHandler.Read();
            }

            //the user has not typed f
            catch (Exception e) when (e is IOException) { }

            FileHandler outputFile;
            //file was selected
            if (answer == "f")
            {
                consoleHandler.Write("type the address of the file this will go into:");
                String address;
                try
                {
                    address = consoleHandler.Read();
                }

                //the user has not typed f
                catch (Exception e) when (e is IOException)
                {
                    return resultWriters;
                }
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
                writeToAll(resultWriters, GeneralValues.error_message);
                return;
            }
            Board board = new Board(input);

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
            if (!board.isValid())
            {
                writeToAll(outputList, "board is not valid");
            }
            timer.Restart();
            Board? solution = DLXSolver.DancingLinksSolver.Solve(board);
            Console.WriteLine("miliseconds:" + timer.ElapsedMilliseconds.ToString());
            timer.Stop();
            String outPut = "";
            if (solution != null)
            {
                outPut = solution.ToString();
            }
            else
            {
                outPut = "board is unsolvable";
            }
            writeToAll(outputList, outPut);




        }

        /// <summary>
        /// prints the outPut to all outputList
        /// </summary>
        /// <param name="outputList">a list of outputs</param>
        /// <param name="outPut">String to output</param>
        public static void writeToAll(List<IWriter> outputList, String outPut)
        {
            foreach (IWriter writer in outputList)
            {
                try
                {
                    writer.Write(outPut);
                }
                catch (System.IO.IOException)
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
        public static String getInput(IInputOutput IOhandler)
        {
            String answer;
            String input = "";
            while (input.Equals(""))
            {
                IOhandler.Write("where should the board be taken from?");
                IOhandler.Write("c for console, f for file");
                try
                {
                    answer = IOhandler.Read();
                }
                catch (Exception e) when (e is IOException)
                {
                    Console.WriteLine("your answer was not accepted");
                    continue;
                }

                //if the user wanted to get input from the console
                if (answer == "c")
                {
                    IOhandler.Write("type the board");
                    try
                    {
                        input = IOhandler.Read();
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
                    IOhandler.Write("please type the address:");
                    try
                    {
                        String address = IOhandler.Read();
                        FileHandler f = new FileHandler(address);
                        input = f.Read();
                    }
                    catch (Exception e) when (
                    e is IOException
                    )
                    {
                        IOhandler.Write("encounterd problems when reading the file");
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
