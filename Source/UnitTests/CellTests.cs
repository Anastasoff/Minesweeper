namespace UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.GameObjects;

    [TestClass]
    public class CellTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCellConstructorWithNegativeRow()
        {
            Cell cell = new MineCell(-1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCellConstructorWithNegativeCol()
        {
            Cell cell = new MineCell(1, -1);
        }
    }
}