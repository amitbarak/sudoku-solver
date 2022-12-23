using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SudokuSolver
{

    class Board
    {
        public int Size {get;}
        public int[,] grid { get; set;}
        public Board()
        {
            this.Size = GeneralValues.Size;
            this.grid = new int[Size, Size];
        }

        public Board(String contents)
        {
            this.Size = GeneralValues.Size;
            this.grid = new int[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    this.grid[i, j] = (int)(contents[j + i * Size] - '0');
                }
            }
        }

        public override string ToString()
        {
            StringBuilder representation = new StringBuilder();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
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
