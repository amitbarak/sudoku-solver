using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.NewFolder
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


        public int GetRowConstraintIndex(int colIndex, int CellValue)
        {
            return board.cellsNumber + colIndex * board.rowSize + CellValue;
        }



        public int GetColConstraintIndex(int rowIndex, int CellValue)
        {
            return board.cellsNumber * 2 + rowIndex * board.rowSize + CellValue;
        }

        public int GetSqrConstraintIndex(int colIndex, int y, int CellValue)
        {
            return (board.cellsNumber) * 3 + (board.nonetSize * (colIndex / board.nonetSize)
                + y / board.nonetSize) * board.rowSize + CellValue;
        }

        public int GetRowIndex(int x, int y, int CellValue)
        {
            return x * board.cellsNumber + y * board.rowSize + CellValue;
        }

        public ColumnNode createLinkedList(Board board)
        {
            List<ColumnNode> columnsList = new List<ColumnNode>();

            //in this for loop we create the maximun number of columns
            // that could be used
            //there are 4 constraints so its the input length * 4
            for (int col_ind = 0; col_ind < board.cellsNumber * 4; col_ind++)
            {
                ColumnNode col = new ColumnNode(col_ind);
                //insert a node before the former node
                col.right = h;
                col.left = h.left;
                h.left.right = col;
                h.left = col;

                //add to the columns list
                columnsList.Add(col);
            }
            return h;
        }
    }
}
