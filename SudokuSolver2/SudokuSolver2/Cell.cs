using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2
{
    internal class Cell
    {
        public int element { get; set; }


        public Cell(int element)
        {
            this.element = element;
        }

        public Cell(char element)
        {
            this.element = (element - GeneralValues.emptyChar);
        }
        
        

        public bool IsEmpty()
        {
            return element == 0;
        }

        public override string ToString()
        {
            StringBuilder reperesention = new StringBuilder();
            int maxSpaces = 1;
            int spaceCount = maxSpaces - element.ToString().Length;
            reperesention.Append("|");
            for (int i = 0; i < spaceCount; i++)
            {
                reperesention.Append(" ");
            }

            reperesention.Append(element.ToString());
            for (int i = 0; i < maxSpaces; i++)
            {
                reperesention.Append(" ");
            }

            return reperesention.ToString();
        }
    }
}
