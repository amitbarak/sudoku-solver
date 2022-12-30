using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class Cell
    {
        public int element;
        public Cell(int element)
        {
            this.element = element;
        }

        public Cell(char element)
        {
            this.element = element - '0';
        }

        public bool IsEmpty()
        {
            return element == 0;
        }

        public override string ToString()
        {
            StringBuilder reperesention = new StringBuilder();
            int maxSpaces = 3;
            int spaceCount = maxSpaces - this.element.ToString().Length;
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
