namespace UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper;
    using Minesweeper.Interfaces;

    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void TestPlayerNameWithNull()
        {
            IPlayer testPlayer = new Player(null, 0);

            Assert.AreEqual("Unnamed player", testPlayer.Name);
        }

        [TestMethod]
        public void TestPlayerNameWIthEmptyString()
        {
            IPlayer testPlayer = new Player(string.Empty, 0);

            Assert.AreEqual("Unnamed player", testPlayer.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestPlayerNameWithLongString()
        {
            IPlayer testPlayer = new Player("This is a long string with 36 chars.", 0);
        }

        [TestMethod]
        public void TestPlayerScoreGetter()
        {
            IPlayer testPlayer = new Player("Pesho", 5);
            Assert.AreEqual(5, testPlayer.Score);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestPlayerScoreSetterWithNegativeScore()
        {
            IPlayer testPlayer = new Player("Pesho", -5);
        }
    }
}