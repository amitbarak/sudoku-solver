using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SudokuSolver
{
    
    class Board
    {
        public readonly int size = 9;
        public int[,] grid;
        public Board()
        {
            this.grid = new int[size, size];
        }

        public Board(String contents)
        {
            this.grid = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    this.grid[i, j] = (int)(contents[j + i * size] - '0');
                }
            }
        }

        public override string ToString()
        {
            StringBuilder representation = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    representation.Append(grid[i,j]);
                    representation.Append(" ");
                }
                representation.Append("\n");
            }
            return representation.ToString();
        }
    }
}
