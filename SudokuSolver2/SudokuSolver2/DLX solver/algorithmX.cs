using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.NewFolder
{
    public class algorithmX
    {
        public bool Solve(Board board)
        {
            //to do:            
            //create the Dancing nodes linked list
            //create 
            
            // this is the h node from the theory paper
            ColumnNode starterNode = new ColumnNode(-1);
            DLXConvertor convertor = new DLXConvertor(board, starterNode);
            //create the columns
            convertor.createLinkedList();
            return true;

        }
    }
}
