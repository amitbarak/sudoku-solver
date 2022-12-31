using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{

    public class ConsoleHandler : IInputOutput
    {
        public static int bufferSize = 4096;
        
        public ConsoleHandler()
        {
            Console.SetIn(new StreamReader(Console.OpenStandardInput(bufferSize)));
        }
        public void Write(String value)
        {
            Console.WriteLine(value);
        }

        public String Read()
        {
            String input = Console.ReadLine();
            return input;
        }
    }
}
