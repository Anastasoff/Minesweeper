namespace UnitTests.Common
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.Common;
    using Minesweeper.Interfaces;
    using Minesweeper.GameObjects;

    [TestClass]
    public class CellCreatorTest
    {
        [TestMethod]
        public void TestCellCreatorCreateMineCell()
        {
            CellFactory factory = new CellCreator();
            IGameObject cell = factory.CreateMineCell(new Position(1, 1));
            Assert.IsInstanceOfType(cell, typeof(MineCell));
        }

        [TestMethod]
        public void TestCellCreatorCreateSafeCell()
        {
            CellFactory factory = new CellCreator();
            IGameObject cell = factory.CreateSafeCell(new Position(1, 1));
            Assert.IsInstanceOfType(cell, typeof(SafeCell));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCellCreatorCreateMineCellWithNegativePosition()
        {
            CellFactory factory = new CellCreator();
            IGameObject cell = factory.CreateMineCell(new Position(-1, 5));
        }
    }
}
