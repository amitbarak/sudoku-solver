using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SudokuSolver2.BoardObjects
{
    /// <summary>
    /// A Class that represents a single board
    /// </summary>
    public class Board
    {
        //the number of cells in the board
        public int CellsNumber { get; private set; }
        //the size of the board
        public int RowSize { get; private set; }
        //A cells array with the cells of the board
        public Cell[,] Grid { get; set; }
        //the size of a block/square/nonet in the board
        public int NonetSize { get; private set; }

        /// <summary>
        /// constructor that accepts a string
        /// </summary>
        /// <param name="grid">two dimentional array that represents a board</param>
        public Board(int[,] grid)
        {
            //set the correct sizes of the board
            RowSize = grid.GetLength(0);
            GeneralValues.acceptedSize = RowSize;
            NonetSize = (int)Math.Sqrt(RowSize);
            Grid = new Cell[RowSize, RowSize];

            //insert the values to the board
            for (int col = 0; col < grid.GetLength(0); col++)
            {
                for (int row = 0; row < grid.GetLength(1); row++)
                {
                    Grid[col, row] = new Cell(grid[col, row]);
                }
            }

            CellsNumber = RowSize * RowSize;
        }

        /// <summary>
        /// Constructor for the board
        /// </summary>
        /// <param name="grid">two dimentional array of Cells</param>
        public Board(Cell[,] grid)
        {
            Grid = grid;
            RowSize = grid.GetLength(0);
            NonetSize = (int)Math.Sqrt(RowSize);
            CellsNumber = RowSize * RowSize;
        }


        /// <summary>
        /// the constructor for the board class
        /// recives the input and creates a corrosponding matrix board
        /// </summary>
        /// <param name="contents">the string contents the board will be created from</param>
        public Board(string contents)
        {
            //sets the correct sizes of the board
            RowSize = (int)Math.Sqrt(contents.Length);
            GeneralValues.acceptedSize = RowSize;
            NonetSize = (int)Math.Sqrt(RowSize);
            Grid = new Cell[RowSize, RowSize];

            //sets the values of the board
            for (int col = 0; col < RowSize; col++)
            {
                for (int row = 0; row < RowSize; row++)
                {
                    Grid[col, row] = new Cell(contents[col + row * RowSize]);
                }
            }

            CellsNumber = RowSize * RowSize;
        }


        /// <summary>
        /// returns an element from the board at index: col, row
        /// </summary>
        /// <param name="col">column index</param>
        /// <param name="row">row index</param>
        /// <returns></returns>
        public Cell GetElement(int col, int row)
        {
            return Grid[col, row];
        }

        /// <summary>
        /// sets an element of the board
        /// </summary>
        /// <param name="col">int column index</param>
        /// <param name="row">int row index</param>
        /// <param name="element">Cell</param>
        public void SetPos(int col, int row, Cell element)
        {
            Grid[col, row] = element;
        }

        /// <summary>
        /// sets an element of the board
        /// </summary>
        /// <param name="col">culomn index</param>
        /// <param name="row">row index</param>
        /// <param name="element">int element</param>
        public void SetPos(int col, int row, int element)
        {
            Grid[col, row] = new Cell(element);
        }



        /// <summary>
        /// checks the validity of the board (for example that there aren't two of the same number in the same row)
        /// </summary>
        /// <returns>true if board is valid, false otherwise</returns>
        public bool IsValid()
        {
            int tempValue = 0;
            //iterate the board cells
            for (int col = 0; col < RowSize; col++)
            {
                for (int row = 0; row < RowSize; row++)
                {
                    //check the validity of the cell
                    tempValue = Grid[col, row].Element;
                    Grid[col, row].Element = 0;
                    if (tempValue != 0 && !CheckPlacement(tempValue, col, row))
                    {
                        return false;
                    }
                    Grid[col, row].Element = tempValue;
                }
            }
            return true;
        }



        /// <summary>
        /// toString of the board. prints if nicely
        /// </summary>
        /// <returns>string of the Board</returns>
        public override string ToString()
        {
            StringBuilder representation = new StringBuilder();
            int lineLength = 0;
            for (int i = 0; i < RowSize; i++)
            {
                for (int j = 0; j < RowSize; j++)
                {
                    representation.Append(Grid[j, i]);
                    lineLength += Grid[j, i].ToString().Length;
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
            for (int i = 0; i < RowSize; i++)
            {
                if (Grid[col, i].Element == element
                    || Grid[col, i].Element == element)

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
                    if (Grid[col, i].Element == element)
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
            return col / NonetSize * NonetSize;
        }


        /// <summary>
        /// returns the starting index of the Nonet
        /// </summary>
        /// <param name="row">row index</param>
        /// <returns></returns>
        private int GetNonetStartRow(int row)
        {
            return row / NonetSize * NonetSize;
        }


        /// <summary>
        /// returns the ending column index of the Nonet
        /// </summary>
        /// <param name="col">column index</param>
        /// <returns>the ending column index of the Nonet</returns>
        private int GetNonetEndCol(int col)
        {
            return col / NonetSize * NonetSize + NonetSize;
        }

        /// <summary>
        /// returns the index of the end row in the Nonet
        /// </summary>
        /// <param name="row">row index</param>
        /// <returns>the ending row index of the Nonet</returns>
        private int GetNonetEndRow(int row)
        {
            return row / NonetSize * NonetSize + NonetSize;
        }
    }



}
