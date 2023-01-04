using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.NewFolder
{
    public class Node
    {
        public Node left, right, up, down;
        public ColumnNode Header;

        public Node(ColumnNode header)
        {
            left = right = up = down = this;
            Header = header;
        }

        public Node(Node left, Node right, Node up, Node down, ColumnNode header)
        {
            this.left = left;
            this.right = right;
            this.up = up;
            this.down = down;
            Header = header;
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
        




    }
}
