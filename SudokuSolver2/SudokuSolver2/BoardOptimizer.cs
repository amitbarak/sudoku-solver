using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SudokuSolver
{
    public class BoardOptimizer
    {
        private Board board;
        private BoardOptimizer(Board board)
        {
            this.board = board;
        }



        public static bool Optimize(Board board)
        {
            BoardOptimizer bo = new BoardOptimizer(board);
            return bo.setBoardPossibleValues();
        }


        private bool setBoardPossibleValues()
        {
            bool b = false;
            for (int col = 0; col < board.rowSize; col++) //O(n)
            {
                for (int row = 0; row < board.rowSize; row++)//O(n)
                {
                    Cell currentCell = board.getElement(col, row);
                    if (currentCell.IsEmpty())
                    {
                        if (removeImpossibleValues(currentCell, col, row))
                        {
                            b = true;
                        }
                    }
                }
            }
            return b;
        }

        private bool removeImpossibleValues(Cell cell, int col, int row)
        {
            bool b = removeByNonets(cell, col, row);
            b = b || removeByLines(cell, col, row);
            return b;
        }


        private bool removeByLines(Cell cell, int col, int row)
        {
            bool b = false;
            int elementInLine;
            int elementInCol;
            for (int i = 0; i < board.rowSize; i++)
            {
                elementInLine = board.getElement(i, row).element;
                elementInCol = board.getElement(col, i).element;
                if (elementInLine != 0 && cell.GetPossibleValues().Contains(elementInLine))
                {
                    cell.removePossibleValue(elementInLine);
                    b = true;
                }

                if (elementInCol != 0 && cell.GetPossibleValues().Contains(elementInCol))
                {
                    cell.removePossibleValue(elementInCol);
                    b = true;
                }
            }
            return b;
        }



        private bool removeByNonets(Cell cell, int col, int row)
        {
            bool b = false;
            int startCol = GetNonetStartCol(col);
            int startRow = GetNonetStartRow(row);
            int EndCol = GetNonetEndCol(col);
            int EndRow = GetNonetEndRow(row);
            int currentElement;
            for (int i = startCol; i < EndCol; i++)
            {
                for (int j = startRow; j < EndRow; j++)
                {
                    currentElement = board.getElement(i, j).element;
                    if (currentElement != 0 && cell.GetPossibleValues().Contains(currentElement))
                    {
                        cell.removePossibleValue(currentElement);
                        b = true;
                    }
                }
            }
            return b;
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
