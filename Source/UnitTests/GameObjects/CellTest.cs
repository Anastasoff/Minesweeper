namespace UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.GameObjects;
    using Minesweeper.Interfaces;
    using Moq;

    [TestClass]
    public class CellTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCellConstructorWithNegativeRowValue()
        {
            Cell cell = new MineCell(new Position(-1, 5));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCellConstructorWithNegativeColValue()
        {
            Cell cell = new MineCell(new Position(1, -5));
        }

        [TestMethod]
        public void TestCellRevealMethod()
        {
            Cell cell = new MineCell(new Position(1, 1));
            cell.RevealCell();
            Assert.AreEqual(true, cell.IsCellRevealed);
        }

        [TestMethod]
        public void TestCellPositionCoordinates()
        {
            Cell cell = new MineCell(new Position(5, 5));
            Assert.AreEqual(new Position(5, 5), cell.Coordinates);
        }

        [TestMethod]
        public void TestMineCellType()
        {
            Cell cell = new MineCell(new Position(1, 1));
            Assert.AreEqual(CellTypes.Mine, cell.Type);
        }

        [TestMethod]
        public void TestRegularCellType()
        {
            Cell cell = new SafeCell(new Position(1, 1));
            Assert.AreEqual(CellTypes.Safe, cell.Type);
        }

        [TestMethod]
        public void TestRegularCellDefaultNeighboringMines()
        {
            SafeCell cell = new SafeCell(new Position(1, 1));
            Assert.AreEqual(0, cell.NumberOfNeighbouringMines);
        }

        [TestMethod]
        public void TestMineCellVisitor()
        {
            var visitorMock = new Mock<IVisitor>();
            visitorMock.Setup(v => v.Visit(It.IsAny<Cell>())).Verifiable();
            MineCell cell = new MineCell(new Position(1, 1));
            cell.Accept(visitorMock.Object);
            visitorMock.Verify();
        }

        [TestMethod]
        public void TestRegularCellVisitor()
        {
            var visitorMock = new Mock<IVisitor>();
            visitorMock.Setup(v => v.Visit(It.IsAny<Cell>())).Verifiable();
            SafeCell cell = new SafeCell(new Position(1, 1));
            cell.Accept(visitorMock.Object);
            visitorMock.Verify();
        }
    }
}