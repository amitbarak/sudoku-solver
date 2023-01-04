using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.NewFolder
{
    internal class DancingLinksSolver
    {

        public static bool Solve(Board board, List<IWriter> resultWriters)
        {
            if (!board.isValid())
            {
                foreach (IWriter writer in resultWriters)
                {
                    writer.Write("board is not valid");
                }
                return false;
            }
            //to do:
            //create the Dancing nodes linked list
            // this is the h node from the theory paper
            ColumnNode starterNode = new ColumnNode(-1); 

            
            

            //
            return true;
        }
    }
}
