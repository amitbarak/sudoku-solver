using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SudokuSolver
{

    class Board
    {
        public int Size { get; }
        public Cell[,] grid { get; set; }
        public Board()
        {
            this.Size = GeneralValues.Size;
            this.grid = new Cell[Size, Size];
        }

        public Board(String contents)
        {
            this.Size = GeneralValues.Size;
            this.grid = new Cell[Size, Size];
            for (int col = 0; col < Size; col++)
            {
                for (int row = 0; row < Size; row++)
                {
                    this.grid[col, row] = new Cell(contents[col + row * Size]);
                }
            }
        }

        public Cell getElement(int col,int row)
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
        public override string ToString()
        {
            StringBuilder representation = new StringBuilder();
            int lineLength = 0;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
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
    }
}
