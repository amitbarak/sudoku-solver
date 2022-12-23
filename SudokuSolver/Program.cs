using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("type the board");
            String input = Console.ReadLine();
            if (!InputValidation.isValid(input))
            {
                Console.WriteLine(GeneralValues.error_message);
                return;
            }
            Board board = new Board(input);
            Console.WriteLine(board);
            Console.ReadLine();
        }
    }
}
