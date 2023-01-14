using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.DLXSolver
{
    public class OptionPoint
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public int CellValue { get; set; }
        public OptionPoint(int column, int row, int cellValue)
        {
            this.Column = column;
            this.Row = row;
            this.CellValue = cellValue;
        }
    }
}
