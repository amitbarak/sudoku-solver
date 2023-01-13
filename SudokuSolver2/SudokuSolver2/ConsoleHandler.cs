using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2
{

    //<summery>
    //This class is used to write output to the console.
    //</summery>

    public class ConsoleHandler : IInputOutput
    {
        public static int bufferSize = 4096;


        //<summery>
        //setter is used increase the number of chars the console can accept.
        //</summery>
        //<param></param>
        //<returns></returns>
        public ConsoleHandler()
        {
            Console.SetIn(new StreamReader(Console.OpenStandardInput(bufferSize)));
        }

        ///<summery>
        ///This method is used to write output to the console.
        /// </summery>
        /// <param name="output"> output to the console </param>
        public void Write(String output)
        {
            Console.WriteLine(output);
        }


        ///<summery>
        ///this method is used to read lines from the console.
        /// </summery>
        /// <param ></param>
        /// <returns> the line read from the console </returns>
        public String Read()
        {
            String input = Console.ReadLine();
            return input;
        }
    }
}
