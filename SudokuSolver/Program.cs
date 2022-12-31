using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Program
    {
        static readonly Stopwatch timer = new Stopwatch();
        static void Main(string[] args)
        {
            while (true)
            {
                ConsoleHandler consoleHandler = new ConsoleHandler();
                String input = getInput(consoleHandler);
                List<IWriter>  resultWriters = setOutputList();


                handleInput(input, resultWriters);
            }
        }


        public static List<IWriter> setOutputList()
        {
            ConsoleHandler consoleHandler = new ConsoleHandler();
            List<IWriter> resultWriters = new List<IWriter>();
            resultWriters.Add(consoleHandler);
            consoleHandler.Write("do you want to save the board in a file?");
            consoleHandler.Write("type f for file or enter otherwise:");
            String answer = consoleHandler.Read();
            FileHandler outputFile = null;
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
        public static void handleInput(String input, List<IWriter> resultWriters)
        {
            if (!InputValidation.IsValid(input))
            {
                foreach(IWriter write in resultWriters)
                {
                    Console.WriteLine(GeneralValues.error_message);
                }
                return;
            }
            Board board = new Board(input);

            Console.WriteLine(board);

            SolveAndPrint(board, resultWriters);
        }

        public static void SolveAndPrint(Board board, List<IWriter> outputList)
        {

            timer.Restart();
            BoardOptimizer.Optimize(board);
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
                writer.Write(outPut);
            }

        }


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
                    IOhandler.Write("type the address:");
                    String address = input = IOhandler.Read();
                    FileHandler f = new FileHandler(address);
                    input = f.Read();
                }
                else
                {
                    IOhandler.Write("wrong input, please type c or f");
                }
            }
            

            return input;
        }


    }
}
