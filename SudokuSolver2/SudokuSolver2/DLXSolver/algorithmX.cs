using SudokuSolver2.DLXSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.DLXSolver
{
    /// <summary>
    /// This class is used to solve a Sudoku puzzle using the Dancing Links Algorithm
    /// it contains a search method that creates the result stack of the sudoku puzzle
    /// additionaly it has a getSulition method that returns the solution as a board object
    /// </summary>
    public class AlgorithmX
    {

        public ColumnNode header;
        public Stack<Node> result = new Stack<Node>();

        /// <summary>
        /// creates a list of all the columns in the matrix.
        /// </summary>
        /// <param name="starterNode">a culomn node at index -1. it is a node 
        /// before all ColumnNodes</param>
        public AlgorithmX(ColumnNode starterNode)
        {
            header = starterNode;
        }


        /// <summary>
        /// returns a board with the sudoku solution after the search method 
        /// was called
        /// </summary>
        /// <returns>weather or not the sulition was found</returns>
        public Board getSolution()
        {
            

            int rowSize = (int)Math.Sqrt(result.Count());

            //getting a list of all of the ID's
            int[,] finalGrid = new int[rowSize, rowSize];
            foreach (Node cellID in result)
            {
                finalGrid[cellID.Possibility.Column, cellID.Possibility.Row] = cellID.Possibility.CellValue;
            }

            //creates and returns board made from the grid
            Board board = new Board(finalGrid);
            return board;
        }

        /// <summary>
        /// gets the culomn with the least number of nodes
        /// </summary>
        /// <returns>returns the culomn with the least number of nodes</returns>
        public ColumnNode getMinCol()
        {
            ColumnNode j = (ColumnNode)header.Right;
            ColumnNode minCol = j;
            while (j != header)
            {
                if (j.Size < minCol.Size)
                {
                    minCol = j;
                }
                j = (ColumnNode) j.Right;
            }
            return minCol;
        }




        /// <summary>
        /// searches for the sulition of the board 
        /// </summary>
        /// <returns>whether or not the sulition was found</returns>
        public bool Search()
        {
            //if the matrix has no columns, then the solution is found
            if (header.Right == header)
                return true;

            //get the column with the least number of nodes and cover it
            ColumnNode minColumn = getMinCol();
            minColumn.Cover();

            
            for (Node rowsIterator = minColumn.Down; rowsIterator != minColumn; rowsIterator = rowsIterator.Down)
            {
                //includes teh rowsIterator in the solution
                result.Push(rowsIterator);
                
                
                for (Node j = rowsIterator.Right; j != rowsIterator; j = j.Right)
                {
                    j.Header.Cover();
                }
                //recursivly continues on the search for a solution
                if (Search() == true)
                {
                    return true;
                }
                //if the sulition was not found on this path
                //then the rowsIterator is removed from the result
                // and all of the nodes that were covered, will be uncovered
                
                rowsIterator = result.Pop();
                minColumn = rowsIterator.Header;

                for (Node j = rowsIterator.Left; j != rowsIterator; j = j.Left)
                {
                    j.Header.UnCover();
                }
            }
            minColumn.UnCover();
        
            return false;

        }
    }
}
