using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    static class InputValidation
    {
        public static Boolean isValid(String input)
        {
            Boolean valid = checkLegth(input);
            valid &= checkChars(input);
            return valid;
        }

        public static Boolean checkChars(String input)
        {
            char max_value = GeneralValues.max_char;
            char min_value = GeneralValues.min_char;
            Console.WriteLine("hello");
            foreach (char c in input)
            {
                if (c > max_value || c < min_value)
                {
                    Console.WriteLine("hi");
                    GeneralValues.error_message = "wrong char: " + c;
                    return false;
                }
            }
            return true;
        }
        public static Boolean checkLegth(String input)
        {
            if (input.Length == GeneralValues.Size * GeneralValues.Size)
            {
                return true;
            }
            else
            {
                GeneralValues.error_message = "wrong size";
                return false;
            }
        }

    }
}
