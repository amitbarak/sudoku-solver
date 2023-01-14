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

        private Solver(Board board)
        {
            this.board = board;
        }
        public static bool Solve(Board board)
        {
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
