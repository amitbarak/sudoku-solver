using SudokuSolver2.DLXSolver;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.DLXSolver
{
    public class DLXConvertor
    {

        //root node is h
        public ColumnNode h;
        public Board board;
        public DLXConvertor(Board board, ColumnNode h)
        {
            this.h = h;
            this.board = board;
        }

        public int GetCellConstraintIndex(int colIndex, int rowIndex)
        {
            return colIndex * board.rowSize + rowIndex;
        }

        
        public int GetRowConstraintIndex(int rowIndex, int CellValue)
        {
            return board.cellsNumber + rowIndex * board.rowSize + CellValue;
        }



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
