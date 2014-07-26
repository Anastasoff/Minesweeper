namespace UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.GameObjects;
    using Minesweeper.Interfaces;
    using Moq;

    [TestClass]
    public class VisitorsTest
    {
        [TestMethod]
        public void TestFlagVisitorIsChangingState()
        {
            Cell cell = new MineCell(1, 1);
            IVisitor visitor = new FlagVisitor();
            visitor.Visit(cell);
            Assert.AreEqual(cell.Type, CellTypes.Flag);
        }

        [TestMethod]
        public void TestFlagVisitorIsKeepingStateWhenCellIsRevealled()
        {
            Cell cell = new MineCell(1, 1);
            IVisitor visitor = new FlagVisitor();
            cell.IsCellRevealed = true;
            cell.Accept(visitor);
            Assert.AreNotEqual(cell.Type, CellTypes.Regular);
        }

        [TestMethod]
        public void TestCellRevealingVisitorIsChangingState()
        {
            Cell cell = new MineCell(1, 1);
            IVisitor visitor = new CellRevealingVisitor();
            cell.Accept(visitor);
            Assert.AreEqual(true, cell.IsCellRevealed);
        }

        [TestMethod]
        public void TestNeighbouringMinesVisitorSettingCorrectNumberOfMines()
        {
            var cell = new RegularCell(1, 1);
            IVisitor visitor = new NeighbouringMinesVisitor(5);
            cell.Accept(visitor);
            Assert.AreEqual(5, cell.NumberOfNeighbouringMines);
        }
    }
}