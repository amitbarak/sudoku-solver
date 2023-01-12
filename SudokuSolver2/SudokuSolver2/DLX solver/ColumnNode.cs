using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.NewFolder
{
    public class ColumnNode : Node
    {
        public int size;
        public int index;

        public ColumnNode(int index) : base(header: null)
        {   
            size = 0;
        }


        public void cover()
        {
            //remove the column from the list
            right.left = left;
            left.right = right;
            //remove the rows that contain this column
            for (Node i = down; i != this; i = i.down)
            {
                for (Node j = i.right; j != i; j = j.right)
                {
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
