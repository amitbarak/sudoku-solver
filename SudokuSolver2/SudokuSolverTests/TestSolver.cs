using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
using SudokuSolver2.DLXSolver;
using SudokuSolver2.BoardObjects;

namespace SudokuSolverTests
{
    [TestClass]
    public class TestSolver
    {

        /// <summary>
        /// tests solving works for: a one by one board
        /// </summary>
        [TestMethod]
        public void Solve_SolvebleBoard1X1_ReturnsTrue()
        {
            //Arange    
            var board = new Board("1");


            //act
            Board? resultedBoard = DancingLinksSolver.Solve(board);
            var result = resultedBoard != null && resultedBoard.IsValid();

            //assert
            Assert.IsTrue(result);
        }



        /// <summary>
        /// tests solving works for: a four by four board
        /// </summary>
        [TestMethod]
        public void Solve_SolvebleBoard4x4_ReturnsTrue()
        {
            //Arange    
            var board = new Board("3000000100000000");


            //act
            Board? resultedBoard = DancingLinksSolver.Solve(board);
            var result = resultedBoard != null && resultedBoard.IsValid();

            //assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// tests solving works for: a nine by nine board
        /// </summary>
        [TestMethod]
        public void Solve_SolvebleBoard9x9_ReturnsTrue()
        {
            //Arange    
            var board = new Board("800000070006010053040600000000080400003000700020005038000000800004050061900002000");


            //act
            Board? resultedBoard = DancingLinksSolver.Solve(board);
            var result = resultedBoard != null && resultedBoard.IsValid();

            //assert
            Assert.IsTrue(result);
        }


        /// <summary>
        /// test that the solver returns null for unsolveble 9X9 board
        /// </summary>
        [TestMethod]
        public void Solve_UnSolvebleBoard9x9_ReturnsFalse()
        {
            //Arange
            var board = new Board("850000124720000890004000701600090004100820000000060100000502000000705600620000407");


            //act
            Board? resultedBoard = DancingLinksSolver.Solve(board);
            var result = resultedBoard != null && resultedBoard.IsValid();
            
            //assert
            Assert.IsFalse(result);
        }





        /// <summary>
        /// test solving returns a valid board for a solveble 16X16 board
        /// </summary>
        [TestMethod]
        public void Solve_SolvebleBoard16X16_ReturnsTrue()
        {
            //Arange
            var board = new Board("10023400<06000700080007003009:6;0<00:0010=0;00>0300?200>000900<0=000800:0<201?000;76000@000?005=000:05?0040800;0@0059<00100000800200000=00<580030=00?0300>80@000580010002000=9?000<406@0=00700050300<0006004;00@0700@050>0010020;1?900=002000>000>000;0200=3500<");


            //act
            Board? resultedBoard = DancingLinksSolver.Solve(board);
            var result = resultedBoard != null && resultedBoard.IsValid();

            //assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// tests solving works for: 25 by 25 board
        /// </summary>
        [TestMethod]
        public void Solve_SolvebleBoard25X25_ReturnsTrue()
        {
            //Arange
            var board = new Board("<:;090BA000I0E203C0FHD0>G070000I0E03C@0=06>GH900;02504E30@F0600H0<00?0100BA0000F0>G0D00?9<870A0E05I406>00:;?007000020I00F=0C03C@00>G000;01<:00A0825I00000900?0<00AE070I0F2=00@00;010BA000I40050C00=D0>090BA0004020C@H036>G9000001500F200000>00D6:00008700E0@003G0<00?18:00AE07000F=0G900?10:;AE200000=000@HD;?18:00200400500@0036>G0<B0E204F05I@0000>G0<6:;018I000000D0009<6>000800BA024000I0000090:>G0180;0AE05@H060000>G087000E25B00F=000<000800?025B04003IC0H06?100;E25B00=304@HD00000<00E250F030000600G9<0>;01870=0C006>@H<:009180B0A005IH060@0:;0007B?0E05I04F00C9<:0080B002500E0=3C40H06>00700200AE=3000HD00@0000000000=0040D0>00900;G?1000");


            //act
            Board? resultedBoard = DancingLinksSolver.Solve(board);
            var result = resultedBoard != null && resultedBoard.IsValid();

            //assert
            Assert.IsTrue(result);
        }
            



    }
}