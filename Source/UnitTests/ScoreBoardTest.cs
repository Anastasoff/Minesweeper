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
        [TestMethod]
        //TODO: FIND A WAY TO FIX THIS!
        public void TestScoreBoardInitialPlayersCount()
        {
            Scoreboard board = Scoreboard.GetInstance;
            Assert.AreEqual(0, board.Count());
        }
        
        [TestMethod]
        public void TestScoreBoardAddPlayer()
        {
            var consoleInterface = new Mock<IOInterface>();
            consoleInterface.Setup(x => x.GetUserInput(It.IsAny<string>())).Returns("Test");

            Scoreboard instance = Scoreboard.GetInstance;
            instance.SetIOInterface(consoleInterface.Object);
            instance.AddPlayer(5);
            instance.AddPlayer(1);

            Assert.AreEqual(2, instance.Count());
        }

        [TestMethod]
        public void TestScoreBoardInstance()
        {
            Scoreboard board = Scoreboard.GetInstance;
            Assert.IsInstanceOfType(board, typeof(Scoreboard));
        }

        [TestMethod]
        public void TestScoreBoardSortPlayersDescendingByScore()
        {
        }
    }
}