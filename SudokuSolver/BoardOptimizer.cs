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
        private BoardOptimizer(Board board) { 
            this.board = board;
        }
        


        public static void Optimize(Board board)
        {
            BoardOptimizer bo = new BoardOptimizer(board);
            bo.setBoardPossibleValues();
        }


        private void setBoardPossibleValues()
        {
            for (int col = 0; col < board.size; col++) //O(n)
            {
                for (int row = 0; row < board.size; row++)//O(n)
                {
                    Cell currentCell = board.getElement(col, row);
                    if (currentCell.IsEmpty())
                    {
                        removeImpossibleValues(currentCell, col, row);//O(n)
                    }
                }
            }
            Console.WriteLine(board);
        }
        
        private void removeImpossibleValues(Cell cell, int col, int row)
        {
            removeByNonets(cell, col, row);
            removeByLines(cell, col, row);
        }


        private void removeByLines(Cell cell, int col, int row)
        {
            int elementInLine;
            int elementInCol;
            for (int i = 0; i < board.size; i++)
            {
                elementInLine = board.getElement(i, row).element;
                elementInCol = board.getElement(col, i).element;
                if (elementInLine != 0)
                    cell.removePossibleValue(elementInCol);
                if (elementInCol != 0)
                    cell.removePossibleValue(elementInLine);
            }
        }



        private void removeByNonets(Cell cell, int col, int row)
        {
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
                    if (currentElement != 0)
                        cell.removePossibleValue(currentElement);
                }
            }
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
