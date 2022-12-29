using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Solver
    {
        private Board board;
        private int size;
        private Solver(Board board)
        {
            this.board = board;
            this.size = board.Size;

        }
        public static Board solve(Board board)
        {
            Solver s = new Solver(board);
            if (s.placeAllElements())
            {
                return board;
            }
            else
            {
                return null;
            }
        }

        

        private bool placeAllElements()
        {
            int position = getFirstEmpty();
            if (position == -1)
            {
                return true;
            }
            int col = position / size;
            int row = position % size;
            for (int element = 1; element <= 9; element++)
            {
                if (checkPlacement(element, col, row))
                {
                    board.setPos(col, row, element);
                    if (placeAllElements()) {
                        return true;
                    }
                }
            }
            board.setPos(col, row, 0);
            return false;

        }

        private int getFirstEmpty()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (board.getElement(col, row).isEmpty())
                    {
                        return row + col * size;
                    }
                }
            }
            return -1;
        }

        private bool checkPlacement(int element, int col, int row)
        {
            for (int i = 0; i < size; i++)
            {
                if (board.getElement(col, i).element == element 
                    || board.getElement(i, row).element == element)
                    return false;
            }
            return checkNonetes(element, col, row);
        }

        private bool checkNonetes(int element, int col, int row)
        {
            int startCol = getNonetStartCol(col);
            int startRow = getNonetStartRow(row);
            int EndCol = getNonetEndCol(col);
            int EndRow = getNonetEndRow(row);

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

        private int getNonetStartCol(int col)
        {
            return (col / 3) * 3;
        }


        private int getNonetStartRow(int row)
        {
            return (row / 3) * 3;
        }

        private int getNonetEndCol(int col)
        {
            return (col / 3) * 3 + 3;
        }
        private int getNonetEndRow(int row)
        {
            return (row / 3) * 3 + 3;
        }

    }
}
