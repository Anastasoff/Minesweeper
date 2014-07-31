namespace UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.GUI;
    using Minesweeper.Interfaces;
    using Moq;

    [TestClass]
    public class ScoreBoardTest
    {
        private Scoreboard board = Scoreboard.GetInstance;

        [TestMethod]
        public void TestScoreBoardInitialPlayersCount()
        {
            Assert.AreEqual(0, this.board.Count());
        }

        [TestMethod]
        public void TestScoreBoardAddPlayer()
        {
            var consoleInterface = new Mock<IOInterface>();
            consoleInterface.Setup(x => x.GetUserInput(It.IsAny<string>())).Returns("Test");

            this.board.SetIOInterface(consoleInterface.Object);
            this.board.AddPlayer(5);
            this.board.AddPlayer(1);

            Assert.AreEqual(2, this.board.Count());
        }

        [TestMethod]
        public void TestScoreBoardInstance()
        {
            Assert.IsInstanceOfType(this.board, typeof(Scoreboard));
        }

        [TestMethod]
        public void TestScoreBoardSortPlayersDescendingByScore()
        {
        }
    }
}