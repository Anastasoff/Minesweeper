using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper.GameObjects;
using Moq;
using Minesweeper.Interfaces;

namespace UnitTests
{
    [TestClass]
    public class CellTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCellConstructorWithNegativeRowValue()
        {
            Cell cell = new MineCell(-1, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCellConstructorWithNegativeColValue()
        {
            Cell cell = new MineCell(1, -5);
        }

        [TestMethod]
        public void TestCellRevealMethod()
        {
            Cell cell = new MineCell(1, 1);
            cell.RevealCell();
            Assert.AreEqual(true, cell.IsCellRevealed);
        }

        [TestMethod]
        public void TestCellPositionCoordinates()
        {
            Cell cell = new MineCell(5, 5);
            Assert.AreEqual(new Position(5, 5), cell.Coordinates);
        }

        [TestMethod]
        public void TestMineCellType()
        {
            Cell cell = new MineCell(1, 1);
            Assert.AreEqual(CellTypes.Mine, cell.Type);
        }

        [TestMethod]
        public void TestRegularCellType()
        {
            Cell cell = new RegularCell(1, 1);
            Assert.AreEqual(CellTypes.Regular, cell.Type);
        }

        [TestMethod]
        public void TestRegularCellDefaultNeighboringMines()
        {
            RegularCell cell = new RegularCell(1, 1);
            Assert.AreEqual(0, cell.NumberOfNeighbouringMines);
        }

        [TestMethod]
        public void TestMineCellVisitor()
        {
            var visitorMock = new Mock<IVisitor>();
            visitorMock.Setup(v => v.Visit(It.IsAny<Cell>())).Verifiable();
            MineCell cell = new MineCell(1, 1);
            cell.Accept(visitorMock.Object);
            visitorMock.Verify();
        }

        [TestMethod]
        public void TestRegularCellVisitor()
        {
            var visitorMock = new Mock<IVisitor>();
            visitorMock.Setup(v => v.Visit(It.IsAny<Cell>())).Verifiable();
            RegularCell cell = new RegularCell(1, 1);
            cell.Accept(visitorMock.Object);
            visitorMock.Verify();
        }
    }
}