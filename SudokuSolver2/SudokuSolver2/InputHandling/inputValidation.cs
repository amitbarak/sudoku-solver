using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.InputHandling
{
    /// <summary>
    /// this class is used to validate the input.
    /// </summary>
    public static class InputValidation
    {
        //hash set that represents valid chars.
        public static HashSet<char> validChars = new HashSet<char>();


        /// <summary>
        /// static method that is used to initialize the validChars hashSet.
        /// it checks the length and the chars the input contains.
        /// </summary>
        /// <param name="input">String input recived to the program.</param>
        /// <returns>returns true if the input is valid, false otherwise.</returns>
        public static bool IsValid(string input)
        {
            bool valid = CheckLegth(input);
            if (!valid)
            {
                return false;
            }
            setValidChars();
            valid &= CheckChars(input);
            return valid;
        }

        /// <summary>
        /// static method that is used to check the chars of the input.
        /// </summary>
        /// <param name="input">String input recived to the program</param>
        /// <returns>true if the chars are ok, false otherwise</returns>
        public static bool CheckChars(string input)
        {
            foreach (char c in input)
            {
                if (!validChars.Contains(c))
                {
                    GeneralValues.ErrorMessage = "invalid character: " + c;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// static method that is used to check the length of the input.
        /// </summary>
        /// <param name="input">String input recived to the program</param>
        /// <returns>true if the length is valid, false otherwise</returns>
        public static bool CheckLegth(string input)
        {
            GeneralValues.acceptedSize = (int)Math.Sqrt(input.Length);
            if (!GeneralValues.validLengths.Contains(input.Length))
            {
                GeneralValues.ErrorMessage = "wrong length: " + input.Length;
                return false;
            }
            return true;

        }


        /// <summary>
        /// sets the valid chars hashset for the validation class
        /// according to the length of the input accepted
        /// </summary>
        /// <param></param>
        /// <return></return>
        public static void setValidChars()
        {
            for (int i = 0; i <= GeneralValues.acceptedSize; i++)
            {
                validChars.Add((char)(i + '0'));
            }
        }



    }
}
