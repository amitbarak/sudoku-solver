using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.NewFolder
{
    public class ColumnNode : Node
    {
        public int size;
        public int index;

        public ColumnNode(int index) : base(header: null)
        {   
            size = 0;
        }


        

    }


}
