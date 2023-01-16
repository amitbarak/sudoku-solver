using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.DLXSolver.DLXObjects
{
    /// <summary>
    /// this class is used to represent at the head of a column in the DLX matrix.
    /// </summary>
    public class ColumnNode : Node
    {
        public int Size { get; set; }

        /// <summary>
        /// This method is used to create a column node.
        /// </summary>
        /// <param name="size">size of the board</param>
        public ColumnNode() : base(header: null)
        {
            Size = 0;
        }

        /// <summary>
        /// Covers the Column node
        /// </summary>
        /// <remarks>
        /// removes the column from the matrix, and removes all the rows that this column is in
        /// </remarks>
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

        /// <summary>
        /// Uncover the Column node
        /// </summary>
        /// <remarks>
        /// returns this column to the matrix, and returns all the rows that are in this column
        /// into the matrix
        /// </remarks>
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
