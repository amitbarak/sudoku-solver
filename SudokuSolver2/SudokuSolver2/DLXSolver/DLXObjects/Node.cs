using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.DLXSolver.DLXObjects
{

    /// <summary>
    /// this class is used to represent a node in the DLX matrix.
    /// </summary>
    /// <remarks>
    /// According to the theory behind DLX, A node is a cell in the matrix with the value
    /// of one. A node has a reference to the column it is in,
    /// and a reference to the Posibility the row of the node represents is in.
    /// </remarks>
    public class Node
    {
        //the nodes of all four sides of this node
        public Node Left, Right, Up, Down;
        //the columnNode of the column this node is in
        public ColumnNode Header;
        //the possibility for placement in the board this node represents
        public PossiblePoint Possibility;

        /// <summary>
        /// This method is used to create a node without Location.
        /// </summary>
        /// <remarks>
        /// This is especially usefull for the column nodes.
        /// </remarks>
        /// <param name="header">a ColumnNode of the constructed node</param>
        public Node(ColumnNode header)
        {
            Left = Right = Up = Down = this;
            Header = header;
            Possibility = new PossiblePoint(-1, -1, -1);
        }

        /// <summary>
        /// This method is used to create a node.
        /// </summary>
        /// <param name="header">a ColumnNode of the constructed node</param>
        /// <param name="possibility">the possibily that row of nodes represents</param>
        public Node(ColumnNode header, PossiblePoint possibility)
        {
            Left = Right = Up = Down = this;
            Header = header;
            Possibility = possibility;
        }

        /// <summary>
        /// This method is used to create a node.
        /// </summary>
        /// <param name="left">the node on left of this node</param>
        /// <param name="right">the node on right of this node</param>
        /// <param name="up">the node above this node</param>
        /// <param name="down">the node under this node</param>
        /// <param name="header">the columnNode of this node</param>
        /// <param name="possibility">the possibility this node represents</param>
        public Node(Node left, Node right, Node up, Node down, ColumnNode header, PossiblePoint possibility)
        {
            Left = left;
            Right = right;
            Up = up;
            Down = down;
            Header = header;
            Possibility = possibility;
        }


        /// <summary>
        /// Links the given node to the Right of this Node
        /// </summary>
        /// <param name="node">A Node that will be connected to this Node</param>
        public void LinkRight(Node node)
        {
            node.Right = Right;
            node.Left = this;
            Right.Left = node;
            Right = node;
        }


        /// <summary>
        /// links a node into its column
        /// </summary>
        /// <remarks>
        /// changes the columnNode's size and sets the connections between the two nodes
        /// accordingly.
        /// </remarks>
        public void LinkToColumn()
        {
            //without the column node
            ColumnNode header = Header;
            header.Size += 1;
            Down = header;
            Up = header.Up;
            header.Up.Down = this;
            header.Up = this;
        }


        /// <summary>
        /// removes this node from it's column without regarding it's row
        /// </summary>
        /// <remarks>
        /// changes the columnNode's size and sets the connections between the 
        /// nodes in the column
        /// </remarks>
        public void RemoveFromColumn()
        {
            Up.Down = Down;
            Down.Up = Up;
            Header.Size -= 1;
        }


        /// <summary>
        /// returns this Node to it's column without regarding it's row
        /// </summary>
        /// <remarks>
        /// changes the columnNode's size and sets the connections between the 
        /// nodes in the column
        /// </remarks>
        public void RestoreToColumn()
        {
            Down.Up = this;
            Up.Down = this;
            Header.Size++;
        }

        /// <summary>
        /// Adds this node to the end of a row that starts in the given Node
        /// </summary>
        /// <param name="StartRow">the node in the start of the row</param>
        public void AddToEnd(Node StartRow)
        {

            Right = StartRow;
            Left = StartRow.Left;
            StartRow.Left.Right = this;
            StartRow.Left = this;
        }


        /// <summary>
        /// Links four nodes that represent a row in the matrix, to the entire matrix
        /// </summary>
        /// <remarks>
        ///the order of the links is:
        ///row1 -> square1 -> cell1 -> column1 ->row1 ->..
        /// </remarks>
        /// <param name="cell">a node for the Cell constraint</param>
        /// <param name="row">a node for the row constraint</param>
        /// <param name="column">a node for the column constraint</param>
        /// <param name="square">a node for the square constraint</param>
        public static void LinkNodes(Node cell, Node row, Node column, Node square)
        {

            //this three lines of code connect all four node to each other
            //(circularly)
            row.LinkRight(square);
            square.LinkRight(cell);
            cell.LinkRight(column);

            //links the four nodes into their columns
            cell.LinkToColumn();
            row.LinkToColumn();
            column.LinkToColumn();
            square.LinkToColumn();

        }
    }
}
