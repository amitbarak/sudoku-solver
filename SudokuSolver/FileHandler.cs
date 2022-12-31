using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SudokuSolver
{
    public class FileHandler : IInputOutput
    {
        public String address { get; set; }

        public FileHandler(String address)
        {
            this.address = address;
        }
        public void Write(String content)
        {
            FileStream fs = new FileStream(address, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();
            fs.Close();
        }


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
