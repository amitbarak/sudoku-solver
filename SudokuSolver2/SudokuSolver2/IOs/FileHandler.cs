using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SudokuSolver2.IOs
{

    ///<summery>
    ///This class is used to read and write output to files.
    ///</summery>
    internal class FileHandler : IInputOutput
    {

        //the adress of the file
        public string Address { get; set; }


        /// <summary>
        /// creates a new FileHandler object
        /// </summary>
        /// <param name="address">a string with the adress of the file</param>
        public FileHandler(string address)
        {
            this.Address = address;
        }

        ///<summery>
        ///This method is used to write output to a file.
        ///</summery>
        ///<param name="output">The output to be written.</param>
        public void WriteLine(string output)
        {
            if (!File.Exists(Address))
                throw new FileNotFoundException();
            FileStream fs = new(Address, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new(fs);
            sw.WriteLine(output);
            sw.Flush();
            sw.Close();
            fs.Close();
        }


        ///<summery>
        ///reads the first line of a file
        ///</summery>
        ///<param></param>
        ///<returns>the line read from the console</returns>
        public string ReadLine()
        {
            try
            {
                FileStream fs = new(Address, FileMode.Open, FileAccess.Read);
                StreamReader sr = new(fs);
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
#pragma warning disable CS8600
                string input = sr.ReadLine(); //this warning is being handled at the catch
#pragma warning restore CS8600 
                sr.Close();
                fs.Close();
                if (input == null || input.Equals(""))
                {
                    throw new IOException();
                }

                return input;
            }
            catch (Exception e) when (
                    e is FileNotFoundException ||
                    e is FileLoadException || e is IOException
                    || e is OutOfMemoryException
                    )
            {
                throw new IOException();
            }

        }

    }
}
