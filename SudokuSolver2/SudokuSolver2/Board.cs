using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SudokuSolver2
{

    public class Board
    {
        public int cellsNumber;
        public int rowSize { get; private set; }
        public Cell[,] grid { get; set; }
        public int nonetSize { get; private set; }

        public Board(int[,] grid)
        {

            this.rowSize = (int)Math.Sqrt(grid.GetLength(0) * grid.GetLength(1));
            GeneralValues.acceptedSize = rowSize;
            this.nonetSize = (int)Math.Sqrt(rowSize);
            this.grid = new Cell[rowSize, rowSize];
            for (int col = 0; col < grid.GetLength(0); col++)
            {
                for (int row = 0; row < grid.GetLength(1); row++)
                {
                    this.grid[col, row] = new Cell(grid[col, row]);
                }
            }

            this.cellsNumber = rowSize * rowSize;
        }

        public Board(Cell[,] grid)
        {
            this.grid = grid;
            this.rowSize = grid.GetLength(0);
            this.nonetSize = (int)Math.Sqrt(rowSize);
            this.cellsNumber = rowSize * rowSize;
        }


        /// <summary>
        /// the constructor for the board class
        /// recives the input and creates a corrosponding matrix board
        /// </summary>
        /// <param name="contents">the string contents the board will be created from</param>
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

            this.cellsNumber = rowSize * rowSize;
        }


        /// <summary>
        /// returns an element from the board
        /// </summary>
        /// <param name="col">column index</param>
        /// <param name="row">row index</param>
        /// <returns></returns>
        public Cell getElement(int col, int row)
        {
            return this.grid[col, row];
        }

        /// <summary>
        /// sets an element of the board
        /// </summary>
        /// <param name="col">int column index</param>
        /// <param name="row">int row index</param>
        /// <param name="element">Cell</param>
        public void setPos(int col, int row, Cell element)
        {
            this.grid[col, row] = element;
        }

        /// <summary>
        /// sets an element of the board
        /// </summary>
        /// <param name="col">culomn index</param>
        /// <param name="row">row index</param>
        /// <param name="element">int element</param>
        public void setPos(int col, int row, int element)
        {
            this.grid[col, row] = new Cell(element);
        }



        /// <summary>
        /// checks the validity of the board (for example that there aren't two of the same number in the same row)
        /// </summary>
        /// <returns>true if board is valid, false otherwise</returns>
        public bool isValid()
        {
            int tempValue = 0;
            bool b = true;
            //iterate the board cells
            for (int col = 0; col < rowSize; col++)
            {
                for (int row = 0; row < rowSize; row++)
                {
                    //check the validity of the cell
                    tempValue = grid[col, row].element;
                    grid[col, row].element = 0;
                    if (tempValue != 0 && !CheckPlacement(tempValue, col, row))
                    {
                        return false;
                    }
                    grid[col, row].element = tempValue;
                }
            }
            return true;
        }

        

        /// <summary>
        /// toString of the board. prints if nicely
        /// </summary>
        /// <returns>string of the object</returns>
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



        /// <summary>
        /// checks if a placement of a an element in the board is ok
        /// </summary>
        /// <param name="element"> element to be checked</param>
        /// <param name="col"> column index</param>
        /// <param name="row"> row index</param>
        /// <returns></returns>
        private bool CheckPlacement(int element, int col, int row)
        {
            //checks for the same values in the same row and column
            for (int i = 0; i < this.rowSize; i++)
            {
                if (grid[col, i].element == element
                    || grid[col, i].element == element)

                    return false;
            }
            return CheckNonetes(element, col, row);
        }

        /// <summary>
        /// checks if the elemnt can be placed here
        /// </summary>
        /// <param name="element">number element</param>
        /// <param name="col">column index</param>
        /// <param name="row">row index</param>
        /// <returns></returns>

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
                    if (grid[col, i].element == element)
                        return false;
                }
            }
            return true;
        }


        /// <summary>
        /// returns the starting index column of the Nonet
        /// </summary>
        /// <param name="col">index of column</param>
        /// <returns></returns>
        private int GetNonetStartCol(int col)
        {
            return (col / this.nonetSize) * this.nonetSize;
        }


        /// <summary>
        /// returns the starting index of the Nonet
        /// </summary>
        /// <param name="row">row index</param>
        /// <returns></returns>
        private int GetNonetStartRow(int row)
        {
            return (row / this.nonetSize) * this.nonetSize;
        }


        /// <summary>
        /// returns the ending column index of the Nonet
        /// </summary>
        /// <param name="col">column index</param>
        /// <returns></returns>
        private int GetNonetEndCol(int col)
        {
            return (col / this.nonetSize) * this.nonetSize + this.nonetSize;
        }

        /// <summary>
        /// returns the index of the end row
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private int GetNonetEndRow(int row)
        {
            return (row / this.nonetSize) * this.nonetSize + this.nonetSize;
        }
    }



}
