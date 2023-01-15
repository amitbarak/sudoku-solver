using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
using SudokuSolver2;
using SudokuSolver2.DLXSolver;

namespace SudokuSolverTests
{
    [TestClass]
    public class TestSolver
    {
        [TestMethod]
        public void Solve_SolvebleBoard4x4_ReaturnsTrue()
        {
            //Arange    
            var board = new Board("3000000100000000");


            //act
            var result = DancingLinksSolver.Solve(board);

            //assert
            Assert.IsTrue(result.isValid());
        }

        [TestMethod]
        public void Solve_SolvebleBoard9x9_ReaturnsTrue()
        {
            //Arange    
            var board = new Board("800000070006010053040600000000080400003000700020005038000000800004050061900002000");


            //act
            var result = DancingLinksSolver.Solve(board);

            //assert
            Assert.IsTrue(result.isValid());
        }


        [TestMethod]
        public void Solve_UnSolvebleBoard9x9_ReaturnsFalse()
        {
            //Arange
            var board = new Board("850000124720000890004000701600090004100820000000060100000502000000705600620000407");


            //act
            var result = DancingLinksSolver.Solve(board);

            //assert
            Assert.IsFalse(result.isValid());
        }





    }
}