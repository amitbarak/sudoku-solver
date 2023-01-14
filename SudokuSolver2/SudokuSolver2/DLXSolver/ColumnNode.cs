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
        public int Size;

        public ColumnNode(int index) : base(header: null)
        {
            Size = 0;
        }

        /// <summary>
        /// removes the column from the matrix, and removes all the rows that this culomn is in
        /// </summary>
        public void Cover()
        {
            //remove the columnNode 
            Right.Left = Left;
            Left.Right = Right;
            //remove all of the rows that this culomn contains a node in

            //iterate through all the matrix rows that contains this column
            for (Node i = Down; i != this; i = i.Down) 
            {
                //iterate through all of the nodes in the row
                for (Node j = i.Right; j != i; j = j.Right)
                {
                    //removes the rows that are in this Column
                    j.RemoveFromColumn();
                }
            }
            //NOTE: there is no need to handle the connections in the same row as 
            //it's entire row is removed. In the uncover method this will become usefull
        }


        public void UnCover()
        {
            //iterate through all the matrix rows that contains this column
            for (Node i = Down; i != this; i = i.Down)
            {
                //iterate through all the matrix rows that contains this column
                for (Node j = i.Right; j != i; j = j.Right)
                {
                    j.RestoreToColumn();

                }
            }
            //adding to culomn node back to the list
            Right.Left = this;
            Left.Right = this;
        }




    }


}
