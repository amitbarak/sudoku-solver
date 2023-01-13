using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SudokuSolver2
{

    ///<summery>
    ///This class is used to read and write output to files.
    ///</summery>
    public class FileHandler : IInputOutput
    {

        //the adress of the file
        public String address { get; set; }


        /// <summary>
        /// creates a new FileHandler object
        /// </summary>
        /// <param name="address">a string with the adress of the file</param>
        public FileHandler(String address)
        {
            this.address = address;
        }

        ///<summery>
        ///This method is used to write output to a file.
        ///</summery>
        ///<param name="output">The output to be written.</param>
        public void Write(String output)
        {
            if (!File.Exists(address))
                throw new System.IO.FileNotFoundException();
            FileStream fs = new FileStream(address, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
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
        public String Read()
        {
            FileStream fs = new FileStream(address, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            string str = sr.ReadLine();
            sr.Close();
            fs.Close();
            return str;
        }

    }
}
