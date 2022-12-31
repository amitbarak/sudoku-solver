using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public static class InputValidation
    {
        public static HashSet<char> validChars = new HashSet<char>();
        public static HashSet<int> validLengths = new HashSet<int>();
        public static int maxLength = 5;


        public static Boolean IsValid(String input)
        {
            for (int i = 1; i <= maxLength; i++)
            {
                validLengths.Add((i * i) * (i * i));
            }
            Boolean valid = CheckLegth(input);
            if (!valid)
            {
                return false;
            }
            setValidChars();
            valid &= CheckChars(input);
            return valid;
        }

        public static Boolean CheckChars(String input)
        {
            foreach (char c in input)
            {
                if (!validChars.Contains(c))
                {
                    GeneralValues.error_message = "invalid character: " + c;
                    return false;
                }
            }
            return true;
        }
        public static Boolean CheckLegth(String input)
        {
            GeneralValues.acceptedSize = (int)Math.Sqrt(input.Length);
            if (!validLengths.Contains(input.Length))
            {
                GeneralValues.error_message = "wrong length: " + input.Length;
                return false;
            }
            return true;
                
        }

        public static void setValidChars()
        {
            for (int i = 0; i <= GeneralValues.acceptedSize; i++)
            {
                validChars.Add((char)(i + '0'));
            }
        }

    }
}
