using SudokuSolver2.DLXSolver;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.DLXSolver
{
    public class Node
    {
        public Node Left, Right, Up, Down;
        public ColumnNode Header;
        public OptionPoint Possibility;
        public Node(ColumnNode header)
        {
            Left = Right = Up = Down = this;
            Header = header;
            Possibility = new OptionPoint(-1, -1, -1);
        }

        public Node(ColumnNode header, OptionPoint id)
        {
            Left = Right = Up = Down = this;
            Header = header;
            this.Possibility = id;
        }


        public Node(Node left, Node right, Node up, Node down, ColumnNode header, OptionPoint id)
        {
            this.Left = left;
            this.Right = right;
            this.Up = up;
            this.Down = down;
            Header = header;
            this.Possibility = id;
        }


        public void LinkLeft(Node node)
        {
            node.Left = Left;
            node.Right = this;
            Left.Right = node;
            Left = node;
        }

        public void LinkRight(Node node)
        {
            node.Right = this.Right;
            node.Left = this;
            Right.Left = node;
            Right = node;
        }


        public void LinkUp(Node node)
        {
            node.Up = Up;
            node.Down = this;
            Up.Down = node;
            Up = node;
        }



        public void LinkDown(Node node)
        {
            node.Down = this.Down;
            node.Up = this;
            Down.Up = node;
            Down = node;
        }


        public void LinkToColumn()
        {
            //links a node into its column
            //without the column node
            ColumnNode header = this.Header;
            header.Size += 1;
            Down = header;
            Up = header.Up;
            header.Up.Down = this;
            header.Up = this;
        }



        public void RemoveLeftRight()
        {
            Left.Right = Right;
            Right.Left = Left;
        }



        public void RemoveFromColumn()
        {
            Up.Down = Down;
            Down.Up = Up;
            Header.Size -= 1;
        }



        public void RestoreToColumn()
        {
            Down.Up = this;
            Up.Down = this;
            Header.Size++;
        }


        public void addToRightByRow(Node node)
        {

            this.Right = node;
            this.Left = node.Left;
            node.Left.Right = this;
            node.Left = this;
        }

        public static void LinkNodes(Node cell, Node row, Node column, Node square)
        {
            //the order of the links is:
            //row1 -> square1 -> cell1 -> column1 ->row1 ->..

            //this three lines of code connect all four node to each other
            //(circularly)
            row.LinkRight(square);
            square.LinkRight(cell);
            cell.LinkRight(column);

            cell.LinkToColumn();
            row.LinkToColumn();
            column.LinkToColumn();
            square.LinkToColumn();

        }
    }
}
