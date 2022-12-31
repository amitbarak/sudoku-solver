using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class Solver
    {
        private Board board;
        private Solver(Board board)
        {
            this.board = board;

        }
        public static bool Solve(Board board)
        {
            Solver s = new Solver(board);
            return s.PlaceAllElements();
        }

        

        private bool PlaceAllElements()
        {
            int position = GetFirstEmpty();
            if (position == -1)
            {
                return true;
            }
            int col = position % board.size;
            int row = position / board.size;
            Cell currentCell = this.board.getElement(col, row);
            foreach (int element in currentCell.GetPossibleValues())
            {
                if (CheckPlacement(element, col, row))
                {
                    board.setPos(col, row, element);
                    if (PlaceAllElements()) {
                        return true;
                    }
                }
            }
            board.setPos(col, row, 0);
            return false;

        }

        private int GetFirstEmpty()
        {
            for (int row = 0; row < board.size; row++)
            {
                for (int col = 0; col < board.size; col++)
                {
                    if (board.getElement(col, row).IsEmpty())
                    {
                        return row * board.size + col;
                    }
                }
            }
            return -1;
        }

        private bool CheckPlacement(int element, int col, int row)
        {
            for (int i = 0; i < board.size; i++)
            {
                if (board.getElement(col, i).element == element 
                    || board.getElement(i, row).element == element)

                    return false;
            }
            return CheckNonetes(element, col, row);
        }

        private bool CheckNonetes(int element, int col, int row)
        {
            int startCol = GetNonetStartCol(col);
            int startRow = GetNonetStartRow(row);
            int EndCol = GetNonetEndCol(col);
            int EndRow = GetNonetEndRow(row);

            for (int i = startCol; i < EndCol; i++)
            {
                for (int j = startRow; j < EndRow; j++)
                {
                    if (board.getElement(i, j).element == element)
                        return false;
                }
            }
            return true;
        }

        private int GetNonetStartCol(int col)
        {
            return (col / board.nonetSize) * board.nonetSize;
        }


        private int GetNonetStartRow(int row)
        {
            return (row / board.nonetSize) * board.nonetSize;
        }

        private int GetNonetEndCol(int col)
        {
            return (col / board.nonetSize) * board.nonetSize + board.nonetSize;
        }
        private int GetNonetEndRow(int row)
        {
            return (row / board.nonetSize) * board.nonetSize + board.nonetSize;
        }



    }
}
