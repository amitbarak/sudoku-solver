using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2
{
    ///<summery>
    ///This interface is used to write output.
    ///</summery>
    public interface IWriter
    {
        void Write(String output);
    }
}
