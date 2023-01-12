using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.NewFolder
{
    public class Node
    {
        public Node left, right, up, down;
        public ColumnNode Header;
        public int ID;
        public Node(ColumnNode header)
        {
            left = right = up = down = this;
            Header = header;
        }

        public Node(ColumnNode header, int id)
        {
            left = right = up = down = this;
            Header = header;
            this.ID = id;
        }


        public Node(Node left, Node right, Node up, Node down, ColumnNode header, int id)
        {
            this.left = left;
            this.right = right;
            this.up = up;
            this.down = down;
            Header = header;
            this.ID = id;
        }


        public void LinkLeft(Node node)
        {
            node.left = left;
            node.right = this;
            left.right = node;
            left = node;
        }

        public void LinkRight(Node node)
        {
            node.right = this.right;
            node.left = this;
            right.left = node;
            right = node;
        }


        public void LinkUp(Node node)
        {
            node.up = up;
            node.down = this;
            up.down = node;
            up = node;
        }



        public void LinkDown(Node node)
        {
            node.down = this.down;
            node.up = this;
            down.up = node;
            down = node;
        }


        public void LinkToColumn()
        {
            //links a node into its column
            //without the column node
            ColumnNode header = this.Header;
            header.size += 1;
            down = header;
            up = header.up;
            header.up.down = this;
            header.up = this;
        }



        public void RemoveLeftRight()
        {
            left.right = right;
            right.left = left;
        }



        public void RemoveFromDown()
        {
            up.down = down;
            down.up = up;
        }


        public void removeFromUp()
        {
            up.down = down;
            down.up = up;

        }


        public void addToRightByRow(Node node)
        {

            this.right = node;
            this.left = node.left;
            node.left.right = this;
            node.left = this;
        }

        public static void LinkNodes(Node Cell, Node Row, Node Column, Node Square)
        {
            //the order of the links is:
            //row1 -> square1 -> cell1 -> column1 ->row1 ->..

            //this three lines of code connect all four node to each other
            //(circularly)
            Row.LinkRight(Square);
            Square.LinkRight(Cell);
            Cell.LinkRight(Column);


        }
    }
}
