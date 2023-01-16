using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.IOs
{
    ///<summery>
    ///This interface is used to write output.
    ///</summery>
    internal interface IWriter
    {
        /// <summary>
        /// This method is used to write a line.
        /// </summary>
        /// <param name="output">String to Write</param>
        void WriteLine(string output);
    }
}
