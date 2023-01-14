using SudokuSolver2.DLXSolver;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.DLXSolver
{

    /// <summary>
    /// This class is used to convert a sudoku board into:
    /// a matrix of 1's and 0's.
    /// However! the matrix is represented by
    /// A Quadripley lincked list of nodes. And not by a 2D array.
    /// the Quadripley lincked list will later be used to solve the sudoku board
    /// </summary>
    public class DLXConvertor
    {
        
        public ColumnNode h;
        public Board board;
        
        //create the DLX convertor
        public DLXConvertor(Board board, ColumnNode h)
        {
            this.h = h;
            this.board = board;
        }

        /// <summary>
        /// returns index to the columnNode in the columnNodes array
        /// of the co
        /// </summary>
        /// <param name="colIndex">the index of the column of the cell</param>
        /// <param name="rowIndex">the index of the row of the cell</param>
        /// <returns></returns>
        public int GetCellConstraintIndex(int colIndex, int rowIndex)
        {
            return colIndex * board.rowSize + rowIndex;
        }

        /// <summary>
        /// returns index to the columnNode in the columnNodes array
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="CellValue"></param>
        /// <returns></returns>
        public int GetRowConstraintIndex(int rowIndex, int CellValue)
        {
            return board.cellsNumber + rowIndex * board.rowSize + CellValue;
        }


        /// <summary>
        /// returns index to the columnNode in the columnNodes array
        /// </summary>
        /// <param name="colIndex">the index of the culomn</param>
        /// <param name="CellValue">the posible value of the cell</param>
        /// <returns></returns>
        public int GetColConstraintIndex(int colIndex, int CellValue)
        {
            return board.cellsNumber * 2 + colIndex * board.rowSize + CellValue;
        }

        public int GetSqrConstraintIndex(int colIndex, int rowIndex, int CellValue)
        {
            if (this.board.nonetSize <= 1)
            {
                //the square constraint is quite useLess if the nonetSize is 1
                //however keeping it won't hurt the algorythm
                return (board.cellsNumber) * 3 + colIndex + rowIndex + CellValue; // TODO: check this line later
            }
            return (board.cellsNumber) * 3 + (board.nonetSize * (colIndex / board.nonetSize)
                + rowIndex / board.nonetSize) * board.rowSize + CellValue;
        }

        public int getNodeID(int colIndex, int rowIndex, int CellValue)
        {
            return colIndex * board.cellsNumber + rowIndex * board.rowSize + CellValue;
        }

        public void CreateCellConstraints(ColumnNode[] HeadersArr, 
            int colIndex, int rowIndex, int cellValue)
        {

            //form links for all rows and columns


            int NodeID = getNodeID(colIndex, rowIndex, cellValue);

            int indexInColList = GetCellConstraintIndex(colIndex, rowIndex);
            Node Cell = new Node(HeadersArr[indexInColList], NodeID);

            indexInColList = GetRowConstraintIndex(rowIndex, cellValue);
            Node row = new Node(HeadersArr[indexInColList], NodeID);

            indexInColList = GetColConstraintIndex(colIndex, cellValue);
            Node col = new Node(HeadersArr[indexInColList], NodeID);

            indexInColList = GetSqrConstraintIndex(colIndex, rowIndex, cellValue);
            Node sqr = new Node(HeadersArr[indexInColList], NodeID);


            Node.LinkNodes(Cell, row, col, sqr);
            Cell.LinkToColumn();
            row.LinkToColumn();
            col.LinkToColumn();
            sqr.LinkToColumn();


        }

        public void CreateCellConstraints(ColumnNode[] HeadersArr,
            int colIndex, int rowIndex)
        {

            for (int i = 0; i < board.rowSize; i++)
            {
                CreateCellConstraints(HeadersArr, colIndex, rowIndex, i);
            }
        }


        public ColumnNode createLinkedList()
        {
            ColumnNode[] HeadersArr = new ColumnNode[board.cellsNumber * 4];

            //in this for loop we create the maximun number of columns
            // that could be used
            //there are 4 constraints so its the input length * 4
            for (int col_ind = 0; col_ind < HeadersArr.Length; col_ind++)
            {
                ColumnNode col = new ColumnNode(col_ind);
                //insert a node before the former node
                col.addToRightByRow(h);

                //add to the columns list
                HeadersArr[col_ind] = col;
            }
            Cell currentCell;
            //in this for loop we create the nodes for the rows
            for (int colIndex = 0; colIndex < board.rowSize; colIndex++)
            {
                for (int rowIndex = 0; rowIndex < board.rowSize; rowIndex++)
                {

                    currentCell = board.getElement(colIndex, rowIndex);
                    if (currentCell.IsEmpty())
                    {
                        CreateCellConstraints(HeadersArr, colIndex, rowIndex);
                    }
                    else
                    {
                        CreateCellConstraints(HeadersArr, colIndex, rowIndex, currentCell.element - 1);
                    }
                }         
            }
            
            return h;
        }

    }
}
