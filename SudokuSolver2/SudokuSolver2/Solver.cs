using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2
{
    public class Solver
    {
        private Board board;

        private HashSet<int> emptyCells;
        private Solver(Board board)
        {
            this.board = board;
            /*
            this.emptyCells = new HashSet<int>();
            for (int col = 0; col < board.size; col++)
            {
                for (int row = 0; row < board.size; row++)
                {
                    if (board.getElement(col, row).IsEmpty())
                    {
                        emptyCells.Add(col + row * board.size);
                    }
                }
            }
            */

        }
        public static bool Solve(Board board)
        {
            /* while (BoardOptimizer.Optimize(board)) {
             }
             Console.WriteLine(board);
             Solver s = new Solver(board);
             return s.PlaceAllElements();
            */
            while (BoardOptimizer.Optimize(board))
            {
            }
            Solver s = new Solver(board);
            int[,] intGrid = new int[board.rowSize, board.rowSize];
            for (int col = 0; col < board.rowSize; col++)
            {
                for (int row = 0; row < board.rowSize; row++)
                {
                    intGrid[col, row] = board.getElement(col, row).element;
                }
            }
            bool isSolved = s.solveByBits(intGrid);
            for (int col = 0; col < board.rowSize; col++)
            {
                for (int row = 0; row < board.rowSize; row++)
                {
                    board.getElement(col, row).element = intGrid[col, row];
                }
            }
            return isSolved;
        }


        /*
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
                    board.setPos(col, row, 0);
                }
            }
            emptyCells.Add(position);
            return false;

        }

        private int GetFirstEmpty()
        {
            if (emptyCells.Count == 0)
            {
                return -1;
            }
            int position = emptyCells.First();
            emptyCells.Remove(position);
            return position;
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
        }*/


        bool solveByBits(int[,] intBoard)
        {
            int[,] nonetsDigits = new int[board.nonetSize, board.nonetSize];
            int[] columnDigits = new int[board.rowSize];
            int[] rowDigits = new int[board.rowSize];

            Array.Clear(nonetsDigits, 0, board.rowSize);
            Array.Clear(rowDigits, 0, board.rowSize);
            Array.Clear(columnDigits, 0, board.rowSize);

            // get 3x3 submatrix, row and column digits
            for (int col = 0; col < board.rowSize; col++)
            {
                for (int row = 0; row < board.rowSize; row++)
                {
                    if (intBoard[col, row] != 0)
                    {
                        int value = 1 << (intBoard[col, row] - 1);
                        //adds this value to the nonet cell in the matrix
                        nonetsDigits[col / board.nonetSize, row / board.nonetSize] |= value;
                        //adds this value to the
                        //rows array in the cell of teh cell's row
                        rowDigits[row] |= value;
                        columnDigits[col] |= value;
                    }
                }
            }
            // Backtrack
            if (backtrackByBits(0, 0, intBoard, nonetsDigits, rowDigits, columnDigits))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        bool backtrackByBits(int col, int row, int[,] bitBoard, int[,] nonetsDigits,
           int[] rowDigits, int[] columnDigits)
        {
            if (col == board.rowSize)
            {
                return true;
            }
            if (row == board.rowSize)
            {
                return backtrackByBits(col + 1, 0, bitBoard, nonetsDigits,
                             rowDigits, columnDigits);
            }

            if (bitBoard[col, row] == 0)
            {
                for (int i = 1; i <= board.rowSize; i++)
                {
                    int digit = 1 << (i - 1);

                    if ((
                        (nonetsDigits[col / board.nonetSize, row / board.nonetSize] & digit) == 0)
                          && ((rowDigits[row] & digit) == 0)
                          && ((columnDigits[col] & digit) == 0))
                    {
                        // set digit
                        nonetsDigits[col / board.nonetSize, row / board.nonetSize] |= digit;
                        rowDigits[row] |= digit;
                        columnDigits[col] |= digit;
                        bitBoard[col, row] = i;

                        if (backtrackByBits(col, row + 1, bitBoard, nonetsDigits,
                                  rowDigits, columnDigits))
                        {
                            return true;
                        }
                        else
                        {
                            //removes the digit from the rowDigits, columnDigits, bitBoard and nonetsDigits
                            nonetsDigits[col / board.nonetSize, row / board.nonetSize]
                                &= ~digit;
                            rowDigits[row] &= ~digit;
                            columnDigits[col] &= ~digit;
                            bitBoard[col, row] = 0;
                        }
                    }
                }
                return false;
            }
            return backtrackByBits(col, row + 1, bitBoard, nonetsDigits,
                         rowDigits, columnDigits);
        }
        


    }
}
