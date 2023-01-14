using SudokuSolver2.DLXSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.DLXSolver
{
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
            int rowSize = (int) Math.Sqrt(result.Count());

            //getting a list of all of the ID's
            List<int> cellsIDsList = new List<int>();
            foreach (Node cellID in result)
            {
                cellsIDsList.Add(cellID.ID);
            }
            cellsIDsList.Sort();

            //creates a list of the final values for each cell
            int[] elementValues = new int[cellsIDsList.Count()];
            int index = 0;
            foreach (var cellID in cellsIDsList)
            {
                elementValues[index] = cellID % rowSize + 1;
                index++;
            }

            //insert the elementValues into a board object
            int[,] finalGrid = new int[rowSize, rowSize];
            for (int col = 0; col < rowSize; col++)
            {
                for (int row = 0; row < rowSize; row++)
                {
                    finalGrid[col, row] = elementValues[col * rowSize + row];
                }
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
            ColumnNode j = (ColumnNode)header.right;
            ColumnNode minCol = j;
            while (j != header)
            {
                if (j.size < minCol.size)
                {
                    minCol = j;
                }
                j = (ColumnNode) j.right;
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
            if (header.right == header)
                return true;

            //get the column with the least number of nodes
            ColumnNode minColumn = getMinCol();
            minColumn.cover();

            for (Node rowsIterator = minColumn.down; rowsIterator != minColumn; rowsIterator = rowsIterator.down)
            {
                //includes teh rowsIterator in the solution
                result.Push(rowsIterator);
                for (Node j = rowsIterator.right; j != rowsIterator; j = j.right)
                {
                    j.Header.cover();
                }

                if (Search() == true)
                {
                    return true;
                }

                rowsIterator = result.Pop();
                minColumn = rowsIterator.Header;

                for (Node j = rowsIterator.left; j != rowsIterator; j = j.left)
                {
                    j.Header.UnCover();
                }
            }
            minColumn.UnCover();
        
            return false;

        }
    }
}
