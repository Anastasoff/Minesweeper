using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using Minesweeper;
using Minesweeper.Interfaces;

namespace UnitTests
{
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
    }
}