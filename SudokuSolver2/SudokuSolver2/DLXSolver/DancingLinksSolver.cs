using SudokuSolver2.BoardObjects;
using SudokuSolver2.DLXSolver.DLXObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.DLXSolver
{
    /// <summary>
    /// This class is used to solve a sudoku puzzle using the DLX algorithm.
    /// </summary>
    /// <remarks>
    /// This class is used to solve a sudoku puzzle using the DLX algorithm.
    /// it contains a method to solve a sudoku puzzle.
    /// </remarks>
    public class DancingLinksSolver
    {
        /// <summary>
        /// This method is used to solve a sudoku puzzle using the DLX algorithm.
        /// </summary>
        /// <param name="board">
        /// 
        /// </param>
        /// <returns>
        /// a solved board if the board is solveble,
        /// null if the board is unsolveble.
        /// </returns>

        public static Board? Solve(Board board)
        {
            //creates a Column node before all nodes in the matrix
            ColumnNode starterNode = new();
            //convertor is used to convert the board to a DLX matrix
            DLXConvertor convertor = new(board, starterNode);
            convertor.createLinkedList();

            //creates a new AlgorithmX object
            AlgorithmX alg = new(starterNode);

            //solves the sudoku puzzle
            if (alg.Search())
            {
                //returns the soluition if it was found
                return alg.getSolution();
            }
            else
            {
                //returns null if the solution was not found
                return null;
            }
            
        }
    }
}
