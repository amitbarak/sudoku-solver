using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.IOs
{

    //<summery>
    //This class is used to write output to the console.
    //</summery>

    public class ConsoleHandler : IInputOutput
    {
        public static int bufferSize = 4096;


        ///<summery>
        ///setter is used increase the number of chars the console can accept.
        ///</summery>
        public ConsoleHandler()
        {
            Console.SetIn(new StreamReader(Console.OpenStandardInput(bufferSize)));
        }

        ///<summery>
        ///This method is used to write output to the console.
        /// </summery>
        /// <param name="output"> output to the console </param>
        public void WriteLine(string output)
        {
            Console.WriteLine(output);
        }


        ///<summery>
        ///this method is used to read lines from the console.
        /// </summery>
        /// <param ></param>
        /// <returns> the line read from the console </returns>
        public string ReadLine()
        {
            string? input = Console.ReadLine();
            if (input == null || input.Equals(""))
            {
                throw new IOException();
            }
            return input;
        }


        ///<summery>
        ///This method is used to read lines from the console.
        ///Without raising error on empty input.
        /// </summery>
        /// <param ></param>
        /// <returns> the line read from the console </returns>
        public string ReadEmpty()
        {
            string? input = Console.ReadLine();
            if (input == null)
            {
                throw new IOException();
            }
            return input;
        }


        /// <summary>
        /// clears the console
        /// </summary>
        public void Clear()
        {
            Console.Clear();
        }
    }
}
