using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.NewFolder
{
    public class AlgorithmX
    {

        public ColumnNode header;
        public Stack<Node> result = new Stack<Node>();


        public AlgorithmX(ColumnNode starterNode)
        {
            header = starterNode;
        }

        public ColumnNode nextCol()
        {
            ColumnNode j = (ColumnNode)header.right;
            ColumnNode minCol = j;
            while (j != header)
            {
                if (j.size < minCol.size)
                {
                    minCol = j;
                }
                j = (ColumnNode) j.right;
            }
            return minCol;
        }





        public bool Search()
        {
            if (header.right == header)
                return true;

            var c = nextCol();
            c.cover();

            for (var r = c.down; r != c; r = r.down)
            {
                result.Push(r);
                for (var j = r.right; j != r; j = j.right)
                {
                    j.Header.cover();
                }

                if (Search() == true)
                {
                    return true;
                }

                r = result.Pop();
                c = r.Header;

                for (var j = r.left; j != r; j = j.left)
                {
                    j.Header.UnCover();
                }
            }
            c.UnCover();
        
            return false;

        }
    }
}
