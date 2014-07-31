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
        private static Cell[,] cellMatrix = new Cell[,] { { new MineCell(new Position(0, 0)), new SafeCell(new Position(0, 1)) }, { new MineCell(new Position(1, 0)), new SafeCell(new Position(1, 1)) } };

        [ClassInitialize()]
        public static void GameBoardInit(TestContext context)
        {
            board = GameBoard.GetInstance;
        }

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

       [TestMethod]
       public void TestIfCheckForMineReturnsTrueWhenThereIsMine()
        {
            var moq = new Mock<IGameBoard>();
            moq.SetupGet(b => b.Board).Returns(cellMatrix);
            Position pos = new Position(0, 0);
            moq.Setup(x => x.CheckIfHasMine(pos)).Returns(cellMatrix[pos.Row, pos.Col] is MineCell);

            Assert.IsTrue(moq.Object.CheckIfHasMine(pos));
        }

        [TestMethod]
        public void TestIfCheckForMineReturnsFalseWhenThereIsSafeCell()
        {
            var moq = new Mock<IGameBoard>();
            moq.SetupGet(b => b.Board).Returns(cellMatrix);
            Position pos = new Position(0, 1);
            moq.Setup(x => x.CheckIfHasMine(pos)).Returns(cellMatrix[pos.Row, pos.Col] is MineCell);

            Assert.IsFalse(moq.Object.CheckIfHasMine(pos));
        }
        
    }
}
