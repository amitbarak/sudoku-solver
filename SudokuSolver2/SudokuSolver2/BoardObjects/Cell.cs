using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.BoardObjects
{
    /// <summary>
    /// A class to represent a single Cell
    /// </summary>
    public class Cell
    {
        public int Element { get; set; }

        /// <summary>
        /// constructor for Cell object
        /// </summary>
        /// <param name="element">int that represents an element</param>
        public Cell(int element)
        {
            Element = element;
        }

        /// <summary>
        /// constructor for Cell object
        /// </summary>
        /// <param name="element">char that represents an element</param>
        public Cell(char element)
        {
            Element = element - GeneralValues.emptyChar;
        }


        /// <summary>
        /// checks if a cell has a value or not
        /// </summary>
        /// <returns>true if the Cell is empty, otherwise false</returns>
        public bool IsEmpty()
        {
            return Element == 0;
        }

        /// <summary>
        /// converts the cell into a proper string
        /// </summary>
        /// <returns>String that represents the cell</returns>
        public override string ToString()
        {
            StringBuilder reperesention = new StringBuilder();
            int maxSpaces = 2;
            int spaceCount = maxSpaces - Element.ToString().Length;
            reperesention.Append("|");
            for (int i = 0; i < spaceCount; i++)
            {
                reperesention.Append(" ");
            }

            reperesention.Append(Element.ToString());
            for (int i = 0; i < maxSpaces; i++)
            {
                reperesention.Append(" ");
            }

            return reperesention.ToString();
        }
    }
}
