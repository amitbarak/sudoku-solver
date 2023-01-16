using SudokuSolver2.BoardObjects;
using SudokuSolver2.DLXSolver.DLXObjects;
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
        
            
        //The number of constraints
        public const int ConstraintNumber = 4;

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
            return colIndex * Board.RowSize + rowIndex;
        }

        /// <summary>
        /// returns index to the columnNode in the columnNodes array
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="CellValue"></param>
        /// <returns></returns>
        public int GetRowConstraintIndex(int rowIndex, int CellValue)
        {
            return Board.CellsNumber + rowIndex * Board.RowSize + CellValue - 1;
        }


        /// <summary>
        /// returns index to the columnNode in the columnNodes array
        /// </summary>
        /// <param name="colIndex">the index of the culomn</param>
        /// <param name="CellValue">the posible value of the cell</param>
        /// <returns></returns>
        public int GetColConstraintIndex(int colIndex, int CellValue)
        {
            return Board.CellsNumber * 2 + colIndex * Board.RowSize + CellValue - 1;
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
            return (Board.CellsNumber) * 3 + (Board.NonetSize * (colIndex / Board.NonetSize)
                + rowIndex / Board.NonetSize) * Board.RowSize + CellValue - 1;
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
            PossiblePoint option)
        {
            //index to the column node in the columnNodes array
            int indexInColList;

            //form links for all rows and columns



            //creates a node for the Position/Cell constraint:
            //Only one number can occupy a cell
            indexInColList = GetCellConstraintIndex(option.Column, option.Row);
            Node cell = new(HeadersArr[indexInColList], option);

            //creates a node for the Row constraint:
            //Each row must contain all numbers once
            indexInColList = GetRowConstraintIndex(option.Row, option.CellValue);
            Node row = new(HeadersArr[indexInColList], option);

            //creates a node for the column constraint:
            //Each column must contain all numbers once
            indexInColList = GetColConstraintIndex(option.Column, option.CellValue);
            Node col = new(HeadersArr[indexInColList], option);

            //creates a node for the square constraint:
            //Each square must contain all numbers once (square = Nonet = 3x3 in reular sudoku)
            indexInColList = GetSqrConstraintIndex(option.Column, option.Row, option.CellValue);
            Node sqr = new(HeadersArr[indexInColList], option);

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
            //iterate through all possible values and create a row (of nodes) for each
            for (int cellValue = 1; cellValue <= Board.RowSize; cellValue++)
            {
                CreateRow(HeadersArr, new PossiblePoint(colIndex, rowIndex, cellValue));
            }
        }


        public ColumnNode createLinkedList()
        {
            //creates an array of columnNodes in the size of the number of constraints
            ColumnNode[] HeadersArr = new ColumnNode[Board.CellsNumber * ConstraintNumber];

            //in this for loop we create the maximun number of columns
            // that could be used
            //there are 4 constraints so its the input length * 4
            for (int col_ind = 0; col_ind < HeadersArr.Length; col_ind++)
            {
                ColumnNode col = new ColumnNode();
                //insert a node after the former node
                col.AddToEnd(StaterNode);

                //add to the columns list
                HeadersArr[col_ind] = col;
            }
            Cell currentCell;
            PossiblePoint currentOption;
            //in this for loop we create the nodes for the rows
            for (int colIndex = 0; colIndex < Board.RowSize; colIndex++)
            {
                for (int rowIndex = 0; rowIndex < Board.RowSize; rowIndex++)
                {

                    currentCell = Board.GetElement(colIndex, rowIndex);
                    if (currentCell.IsEmpty())
                    {
                        //if the cell is empty we create a row for each possible value
                        CreateRows(HeadersArr, colIndex, rowIndex);
                    }
                    else
                    {
                        //we create a row for the value that's in the cell
                        currentOption = new PossiblePoint(colIndex, rowIndex, currentCell.Element);
                        CreateRow(HeadersArr, currentOption);
                    }
                }         
            }
            
            return StaterNode;
        }

    }
}
