using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SudokuSolver
{

    public class Board
    {
        public int cellsNumber;
        public int rowSize { get; private set; }
        public Cell[,] grid { get; set; }
        public int nonetSize { get; private set; }

        public Board(int[,] grid)
        {
            for (int col = 0; col < grid.GetLength(0); col++)
            {
                for (int row = 0; row < grid.GetLength(1); row++)
                {
                    this.grid[col, row] = new Cell((char)grid[col, row]);
                }
            }
        }

        public Board(Cell[,] grid)
        {
            this.grid = grid;
            this.rowSize = grid.GetLength(0);
            this.nonetSize = (int)Math.Sqrt(rowSize);
        }

        public Board(String contents)
        {
            this.rowSize = (int)Math.Sqrt(contents.Length);
            GeneralValues.acceptedSize = rowSize;
            this.nonetSize = (int)Math.Sqrt(rowSize);
            this.grid = new Cell[rowSize, rowSize];
            for (int col = 0; col < rowSize; col++)
            {
                for (int row = 0; row < rowSize; row++)
                {
                    this.grid[col, row] = new Cell(contents[col + row * rowSize]);
                }
            }
        }

        public Cell getElement(int col, int row)
        {
            return this.grid[col, row];
        }
        public void setPos(int col, int row, Cell element)
        {
            this.grid[col, row] = element;
        }

        public void setPos(int col, int row, int element)
        {
            this.grid[col, row] = new Cell(element);
        }




        public bool isValid()
        {
            bool b = true;
            for (int col = 0; col < rowSize; col++)
            {
                for (int row = 0; row < rowSize; row++)
                {
                    if(!CheckPlacement(col, row, grid[col, row].element))
                    {
                        return false;
                    }
                }
            }
            return b;
        }

        
        
        public override string ToString()
        {
            StringBuilder representation = new StringBuilder();
            int lineLength = 0;
            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < rowSize; j++)
                {
                    representation.Append(grid[j, i]);
                    lineLength += grid[j, i].ToString().Length;
                }
                representation.Append("\n");
                for (int j = 0; j < lineLength; j++)
                {
                    representation.Append("-");
                }
                representation.Append("\n");
                lineLength = 0;
            }
            return representation.ToString();
        }


        private bool CheckPlacement(int element, int col, int row)
        {
            for (int i = 0; i < this.rowSize; i++)
            {
                if (this.getElement(col, i).element == element
                    || this.getElement(i, row).element == element)

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
                    if (this.getElement(i, j).element == element)
                        return false;
                }
            }
            return true;
        }

        private int GetNonetStartCol(int col)
        {
            return (col / this.nonetSize) * this.nonetSize;
        }


        private int GetNonetStartRow(int row)
        {
            return (row / this.nonetSize) * this.nonetSize;
        }

        private int GetNonetEndCol(int col)
        {
            return (col / this.nonetSize) * this.nonetSize + this.nonetSize;
        }
        private int GetNonetEndRow(int row)
        {
            return (row / this.nonetSize) * this.nonetSize + this.nonetSize;
        }
    }



}
