using SudokuSolver2.DLXSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2.DLXSolver
{
    public class AlgorithmX
    {

        public ColumnNode header;
        public Stack<Node> result = new Stack<Node>();


        public AlgorithmX(ColumnNode starterNode)
        {
            header = starterNode;
        }


        public Board getSolution()
        {
            int rowSize = (int) Math.Sqrt(result.Count());

            //getting a list of all of the ID's
            List<int> cellsIDsList = new List<int>();
            foreach (Node cellID in result)
            {
                cellsIDsList.Add(cellID.ID);
            }
            cellsIDsList.Sort();
            
            //
            int[] elementValues = new int[cellsIDsList.Count()];
            int index = 0;
            foreach (var cellID in cellsIDsList)
            {
                elementValues[index] = cellID % rowSize + 1;
                index++;
            }

            //insert the elementValues into a board object
            int[,] finalGrid = new int[rowSize, rowSize];
            for (int col = 0; col < rowSize; col++)
            {
                for (int row = 0; row < rowSize; row++)
                {
                    finalGrid[col, row] = elementValues[col * rowSize + row];
                }
            }

            Board board = new Board(finalGrid);
            return board;



        }
        public ColumnNode getMinCol()
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

            ColumnNode c = getMinCol();
            c.cover();

            for (Node r = c.down; r != c; r = r.down)
            {
                result.Push(r);
                for (Node j = r.right; j != r; j = j.right)
                {
                    j.Header.cover();
                }

                if (Search() == true)
                {
                    return true;
                }

                r = result.Pop();
                c = r.Header;

                for (Node j = r.left; j != r; j = j.left)
                {
                    j.Header.UnCover();
                }
            }
            c.UnCover();
        
            return false;

        }
    }
}
