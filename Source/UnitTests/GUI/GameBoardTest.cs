namespace UnitTests.GUI
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.GameObjects;
    using Minesweeper.GUI;
    using Minesweeper.Interfaces;
    using Moq;

    [TestClass]
    public class GameBoardTest
    {
        private static GameBoard board;

        [ClassInitialize()]
        public static void GameBoardInit(TestContext context)
        {
            board = GameBoard.GetInstance;
        }

     //  [TestMethod]
     //  public void TestIfGetCellMethodReturnsCellOfTheCorrectType()
     //  {
     //      var moq = new Mock<GameBoard>();
     //      moq.SetupGet(b => b.Board).Returns(new Cell[,] { {new MineCell(new Position(0, 0)), new //SafeCell(new Position(0, 1))}, {new MineCell(new Position(1, 0)), new SafeCell(new /Position(1,/ 1))}});
     //      Position pos = new Position(0, 0);
     //      var cell = board.GetCell(pos);
     //
     //      Assert.IsInstanceOfType(cell, typeof(MineCell));
     //  }

        [TestMethod]

        public void TestIfInsideBoardReturnsTrueWhenCellIsInsideTheBoard()
        {
            Position pos = new Position(3, 5);
            Assert.IsTrue(board.IsInsideBoard(pos));
        }

        [TestMethod]
        public void TestIfInsideBoardReturnFalseWhenCellRowIsNegativeNumber()
        {
            Position pos = new Position(-1, 5);
            Assert.IsFalse(board.IsInsideBoard(pos));
        }

        [TestMethod]
        public void TestIfInsideBoardReturnFalseWhenCellColIsNegativeNumber()
        {
            Position pos = new Position(5, -1);
            Assert.IsFalse(board.IsInsideBoard(pos));
        }

        [TestMethod]
        public void TestIfInsideBoardReturnFalseWhenCellRowIsBiggerThanMaxCols()
        {
            Position pos = new Position(50, 5);
            Assert.IsFalse(board.IsInsideBoard(pos));
        }

        [TestMethod]
        public void TestIfInsideBoardReturnFalseWhenCellColIsBiggerThanMaxCols()
        {
            Position pos = new Position(5, 50);
            Assert.IsFalse(board.IsInsideBoard(pos));
        }


    }
}
