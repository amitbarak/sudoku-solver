using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.IOs
{

    ///<summery>
    ///This interface is used to read input.
    ///</summery>
    public interface IReader
    {
        /// <summary>
        /// This method is used to read a line.
        /// </summary>
        /// <returns>String that was read</returns>
        string ReadLine();
    }
}
