using SudokuSolver2.DLXSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.DLXSolver
{
    internal class DancingLinksSolver
    {

        public static Board? Solve(Board board)
        {
            //to do:
            //create the Dancing nodes linked list
            // this is the h node from the theory paper
            ColumnNode starterNode = new ColumnNode(-1);
            DLXConvertor convertor = new DLXConvertor(board, starterNode);
            convertor.createLinkedList();
            AlgorithmX alg = new AlgorithmX(starterNode);
            if (alg.Search())
            {
                return alg.getSolution();
            }
            else
            {
                return null;
            }
            
        }
    }

    
    
}
