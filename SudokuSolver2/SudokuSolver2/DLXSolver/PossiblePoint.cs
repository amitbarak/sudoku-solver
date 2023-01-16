using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.DLXSolver
{
    /// <summary>
    /// This class exists to represent the possibility of a value to be in certain cell.
    /// </summary>
    /// <remarks>
    /// It's main use is to determine the possibility a row in the matrix represents.
    /// </remarks>
    public class PossiblePoint
    {
        //the Column of the cell
        public int Column { get; set; }

        //the Row of the cell
        public int Row { get; set; }

        //the possible Value of the cell
        public int CellValue { get; set; }
        /// <summary>
        /// This is the constructor of the class
        /// </summary>
        /// <param name="column">a column of a cell in the Board</param>
        /// <param name="row">a row of a cell in the Board</param>
        /// <param name="cellValue">the value of a cell</param>
        public PossiblePoint(int column, int row, int cellValue)
        {
            this.Column = column;
            this.Row = row;
            this.CellValue = cellValue;
        }
    }
}
