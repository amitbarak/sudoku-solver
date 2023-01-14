using SudokuSolver2.DLXSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.DLXSolver
{
    public class ColumnNode : Node
    {
        public int size;
        public int index;

        public ColumnNode(int index) : base(header: null)
        {   
            size = 0;
            index = 0;
        }


        //remove the column from the list
        public void cover()
        {
            //remove the columnNode 
            right.left = left;
            left.right = right;
            //remove all of the rows that this culomn contains a node in

            //iterate through all the matrix rows that contains this column
            for (Node i = down; i != this; i = i.down) 
            {
                //iterate through all of the nodes in the row
                for (Node j = i.right; j != i; j = j.right)
                {
                    //removes the rows that are in this Column
                    j.down.up = j.up;
                    j.up.down = j.down;
                    j.Header.size--;
                }
            }
        }
        

        public void UnCover()
        {
            //add the column back to the list
            for (Node i = down; i != this; i = i.down)
            {
                for (Node j = i.right; j != i; j = j.right)
                {
                    j.down.up = j;
                    j.up.down = j;
                    j.Header.size++;
                }
            }
            right.left = this;
            left.right = this;
        }




    }


}
