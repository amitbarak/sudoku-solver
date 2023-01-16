using SudokuSolver2.BoardObjects;
using SudokuSolver2.DLXSolver.DLXObjects;
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

        private readonly ColumnNode StarterNode;
        private readonly Stack<Node> Result = new();

        /// <summary>
        /// creates a list of all the columns in the matrix.
        /// </summary>
        /// <param name="starterNode">a culomn node at index -1. it is a node 
        /// before all ColumnNodes</param>
        public AlgorithmX(ColumnNode starterNode)
        {
            StarterNode = starterNode;
        }


        /// <summary>
        /// returns a board with the sudoku solution after the search method 
        /// was called
        /// </summary>
        /// <returns>weather or not the sulition was found</returns>
        public Board getSolution()
        {
            

            int rowSize = (int)Math.Sqrt(Result.Count());

            //getting a list of all of the ID's
            int[,] finalGrid = new int[rowSize, rowSize];
            foreach (Node cellID in Result)
            {
                finalGrid[cellID.Possibility.Column, cellID.Possibility.Row] = cellID.Possibility.CellValue;
            }

            //creates and returns board made from the grid
            Board board = new(finalGrid);
            return board;
        }

        /// <summary>
        /// gets the culomn with the least number of nodes
        /// </summary>
        /// <returns>returns the culomn with the least number of nodes</returns>
        public ColumnNode getMinCol()
        {
            ColumnNode j = (ColumnNode)StarterNode.Right;
            ColumnNode minCol = j;
            //iterate through all columns to find the minimum column
            while (j != StarterNode)
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
        /// searches for the solution of the board 
        /// </summary>
        /// <remarks>
        /// this is the direct implemtation of algorythm X on a DLX structure
        /// </remarks>
        /// <returns>whether or not the solution was found</returns>
        public bool Search()
        {
            //if the matrix has no columns, then the solution is found
            if (StarterNode.Right == StarterNode)
                return true;

            //get the column with the least number of nodes and cover it
            ColumnNode minColumn = getMinCol();
            minColumn.Cover();

            
            for (Node rowsIterator = minColumn.Down; rowsIterator != minColumn; rowsIterator = rowsIterator.Down)
            {
                //includes the rowsIterator in the solution
                Result.Push(rowsIterator);
                
                
                for (Node rowIterator = rowsIterator.Right; rowIterator != rowsIterator; rowIterator = rowIterator.Right)
                {
                    //removes the columns that are in this row
                    rowIterator.Header.Cover();
                }
                //recursivly continues on the search for a solution
                if (Search() == true)
                {
                    return true;
                }
                //if the sulition was not found on this path
                //then the rowsIterator is removed from the result
                // and all of the nodes that were covered, will be uncovered
                
                rowsIterator = Result.Pop();
                minColumn = rowsIterator.Header;

                for (Node rowIterator = rowsIterator.Left; rowIterator != rowsIterator; rowIterator = rowIterator.Left)
                {
                    //retores the columns
                    rowIterator.Header.UnCover();
                }
            }
            //retores the column
            minColumn.UnCover();

            //if no sulition was found on this path
            return false;

        }
    }
}
