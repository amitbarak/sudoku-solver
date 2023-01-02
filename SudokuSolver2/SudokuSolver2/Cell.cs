using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class Cell
    {
        private HashSet<int> possibleValues;

        public int element { get; set; }


        public Cell(int element)
        {
            if (element == 0)
            {
                this.possibleValues = new HashSet<int>(
                    Enumerable.Range(1, GeneralValues.acceptedSize));
            }
            else
            {
                this.possibleValues = new HashSet<int>(element);
                this.possibleValues.Add(element);
            }
            this.element = element;
        }

        public Cell(char element)
        {
            if (element == GeneralValues.emptyChar)
            {
                this.possibleValues = new HashSet<int>(
                    Enumerable.Range(1, GeneralValues.acceptedSize));
            }
            else
            {
                this.possibleValues = new HashSet<int>();
                this.possibleValues.Add(element - GeneralValues.emptyChar);
            }
            this.element = (element - GeneralValues.emptyChar);
        }

        public Cell(HashSet<int> possibleValues)
        {
            this.possibleValues = possibleValues;
        }

        public HashSet<int> GetPossibleValues()
        {
            return this.possibleValues;
        }

        public void AddPossibleValue(int element)
        {
            this.possibleValues.Add(element);
            if (possibleValues.Count() == 1)
            {
                _ = this.element == possibleValues.First();
            }
        }
        public void removePossibleValue(int element)
        {
            this.possibleValues.Remove(element);
            if (possibleValues.Count() == 1)
            {
                _ = this.element == possibleValues.First();
            }
        }

        public bool IsEmpty()
        {
            return possibleValues.Count() == 0 ||
                element == 0;
        }

        public override string ToString()
        {
            StringBuilder reperesention = new StringBuilder();
            int maxSpaces = 3;
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
