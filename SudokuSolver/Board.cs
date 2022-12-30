using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SudokuSolver
{

    public class Board
    {
        public int size { get; private set; }
        public Cell[,] grid { get; set; }
        public int nonetSize { get; private set; }


        public Board(String contents)
        {
            this.size = (int) Math.Sqrt(contents.Length);
            this.nonetSize = (int) Math.Sqrt(size);
            this.grid = new Cell[size, size];
            for (int col = 0; col < size; col++)
            {
                for (int row = 0; row < size; row++)
                {
                    this.grid[col, row] = new Cell(contents[col + row * size]);
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
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
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
