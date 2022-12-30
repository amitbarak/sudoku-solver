using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Console.WriteLine("type the board");
            String input = Console.ReadLine();
            if (!InputValidation.IsValid(input))
            {
                Console.WriteLine(GeneralValues.error_message);
                Console.ReadLine();
            }
            Board board = new Board(input);
            Console.WriteLine(board);
            timer.Start();
            SolveAndPrint(board);
            Console.WriteLine("miliseconds: " + timer.ElapsedMilliseconds.ToString());
            timer.Stop();
            Console.ReadLine();
        }

        public static void SolveAndPrint(Board board)
        {
            bool isSolution = Solver.Solve(board);
            if (isSolution)
            {
                Console.WriteLine(board);
            }
            else
            {
                Console.WriteLine("board is unsolvable");
            }
        }
    }
}
