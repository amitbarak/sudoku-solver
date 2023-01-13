using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2
{

    ///<summary>
    /// This interface is used to define the methods
    /// that are used to read and write the sudoku.
    /// </summary>
    public interface IInputOutput : IReader, IWriter
    {

    }
}
