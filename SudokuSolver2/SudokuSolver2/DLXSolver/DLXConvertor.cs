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
    /// This class is used to convert a sudoku board to a corrospinding DLX structure
    /// </summary>
    /// <remarks>
    /// 
    /// This class is used to convert a sudoku board into:
    /// a matrix of 1's and 0's.
    /// However! the matrix is represented by:
    /// A Quadripley lincked list of nodes. And not by a 2D array.
    /// the Quadripley lincked list will later be used to solve the sudoku board
    /// </remarks>
    public class DLXConvertor
    {
        //The node before all nodes
        public ColumnNode StaterNode;
        //A board object to convert into a DLX structure
        public Board Board;

        /// <summary>
        /// This is the constructor of the class
        /// </summary>
        /// <param name="board">A board to create a DLX structure from</param>
        /// <param name="starterNode">A starter node </param>
        public DLXConvertor(Board board, ColumnNode starterNode)
        {
            this.StaterNode = starterNode;
            this.Board = board;
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
            return colIndex * Board.rowSize + rowIndex;
        }

        /// <summary>
        /// returns index to the columnNode in the columnNodes array
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="CellValue"></param>
        /// <returns></returns>
        public int GetRowConstraintIndex(int rowIndex, int CellValue)
        {
            return Board.cellsNumber + rowIndex * Board.rowSize + CellValue - 1;
        }


        /// <summary>
        /// returns index to the columnNode in the columnNodes array
        /// </summary>
        /// <param name="colIndex">the index of the culomn</param>
        /// <param name="CellValue">the posible value of the cell</param>
        /// <returns></returns>
        public int GetColConstraintIndex(int colIndex, int CellValue)
        {
            return Board.cellsNumber * 2 + colIndex * Board.rowSize + CellValue - 1;
        }


        /// <summary>
        /// returns index to the columnNode in the columnNodes array
        /// </summary>
        /// <param name="colIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="CellValue"></param>
        /// <returns></returns>
        public int GetSqrConstraintIndex(int colIndex, int rowIndex, int CellValue)
        {
            return (Board.cellsNumber) * 3 + (Board.nonetSize * (colIndex / Board.nonetSize)
                + rowIndex / Board.nonetSize) * Board.rowSize + CellValue - 1;
        }

        /// <summary>
        /// creates a row of nodes for a given cell and it's possible value and
        /// adds it to the matrix
        /// </summary>
        /// <param name="HeadersArr">array of column headers</param>
        /// <param name="colIndex">index of the column</param>
        /// <param name="rowIndex">index of the row</param>
        /// <param name="cellValue">a possible value for a cell in that position</param>

        public void CreateRow(ColumnNode[] HeadersArr,
            OptionPoint option)
        {
            //index to the column node in the columnNodes array
            int indexInColList;

            //form links for all rows and columns



            //creates a node for the Position/Cell constraint:
            //Only one number can occupy a cell
            indexInColList = GetCellConstraintIndex(option.Column, option.Row);
            Node cell = new Node(HeadersArr[indexInColList], option);

            //creates a node for the Row constraint:
            //Each row must contain all numbers once
            indexInColList = GetRowConstraintIndex(option.Row, option.CellValue);
            Node row = new Node(HeadersArr[indexInColList], option);

            //creates a node for the column constraint:
            //Each column must contain all numbers once
            indexInColList = GetColConstraintIndex(option.Column, option.CellValue);
            Node col = new Node(HeadersArr[indexInColList], option);

            //creates a node for the square constraint:
            //Each square must contain all numbers once (square = Nonet = 3x3 in reular sudoku)
            indexInColList = GetSqrConstraintIndex(option.Column, option.Row, option.CellValue);
            Node sqr = new Node(HeadersArr[indexInColList], option);

            //links the nodes to each other as they are in the same row, and to their columns
            Node.LinkNodes(cell, row, col, sqr);
            


        }

        /// <summary>
        /// Creates rows in the matrix for each value that could be in that cell
        /// </summary>
        /// <param name="HeadersArr"></param>
        /// <param name="colIndex"></param>
        /// <param name="rowIndex"></param>
        public void CreateRows(ColumnNode[] HeadersArr,
            int colIndex, int rowIndex)
        {

            for (int cellValue = 1; cellValue <= Board.rowSize; cellValue++)
            {
                CreateRow(HeadersArr, new OptionPoint(colIndex, rowIndex, cellValue));
            }
        }


        public ColumnNode createLinkedList()
        {
            ColumnNode[] HeadersArr = new ColumnNode[Board.cellsNumber * 4];

            //in this for loop we create the maximun number of columns
            // that could be used
            //there are 4 constraints so its the input length * 4
            for (int col_ind = 0; col_ind < HeadersArr.Length; col_ind++)
            {
                ColumnNode col = new ColumnNode(col_ind);
                //insert a node before the former node
                col.addToRightByRow(StaterNode);

                //add to the columns list
                HeadersArr[col_ind] = col;
            }
            Cell currentCell;
            OptionPoint currentOption;
            //in this for loop we create the nodes for the rows
            for (int colIndex = 0; colIndex < Board.rowSize; colIndex++)
            {
                for (int rowIndex = 0; rowIndex < Board.rowSize; rowIndex++)
                {

                    currentCell = Board.getElement(colIndex, rowIndex);
                    if (currentCell.IsEmpty())
                    {
                        CreateRows(HeadersArr, colIndex, rowIndex);
                    }
                    else
                    {
                        currentOption = new OptionPoint(colIndex, rowIndex, currentCell.element);
                        CreateRow(HeadersArr, currentOption);
                    }
                }         
            }
            
            return StaterNode;
        }

    }
}
