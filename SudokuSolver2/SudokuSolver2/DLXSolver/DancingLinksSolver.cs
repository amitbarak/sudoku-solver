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
            DLXConvertor convertor = new DLXConvertor(board, starterNode);
            convertor.createLinkedList();
            AlgorithmX alg = new AlgorithmX(starterNode);
            alg.Search();
            Board resultBoard = alg.getSolution();
            Console.WriteLine(convertor.ToString());

            
            foreach (IWriter writer in resultWriters)
            {
                writer.Write(resultBoard.ToString());
            }

            //
            return true;
        }
    }

    
    
}
