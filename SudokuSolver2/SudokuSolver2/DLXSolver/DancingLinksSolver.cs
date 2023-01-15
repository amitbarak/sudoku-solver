using SudokuSolver2.DLXSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.DLXSolver
{
    public class DancingLinksSolver
    {

        public static Board? Solve(Board board)
        {
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
