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
            if (!InputValidation.isValid(input))
            {
                Console.WriteLine(GeneralValues.error_message);
                Console.ReadLine();
            }
            Board board = new Board(input);
            Console.WriteLine(board);
            timer.Start();
            SolveAndPrint(board);
            Console.WriteLine(timer.Elapsed.ToString());
            timer.Stop();
            Console.ReadLine();
        }

        public static void SolveAndPrint(Board board)
        {
            Board solution = Solver.solve(board);
            if (solution == null)
            {
                Console.WriteLine("board is unsolvable");
            }
            else
            {
                Console.WriteLine(solution);
            }
        }
    }
}
