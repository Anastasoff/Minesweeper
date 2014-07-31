namespace UnitTests.GUI
{
    using System;
    using System.IO;
    using System.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.GUI;
    using Minesweeper.GUI.ConsoleSkins;
    
    [TestClass]
    public class ConsoleInterfaceTest
    {
        [TestMethod]
        public void TestConsoleInterfaceConstructorWithoutParameterInitializesAllWhiteSkin()
        {
            var consoleInterface = new ConsoleInterface();
            var skin = typeof(ConsoleInterface).GetField("skin", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(consoleInterface);
            Assert.IsInstanceOfType(skin, typeof(AllWhiteSkin));
        }

        [TestMethod]
        public void TestConsoleInterfaceConstructorWithAllWhiteSkinParameter()
        {
            var consoleInterface = new ConsoleInterface(new AllWhiteSkin());
            var skin = typeof(ConsoleInterface).GetField("skin", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(consoleInterface);
            Assert.IsInstanceOfType(skin, typeof(AllWhiteSkin));
        }

        [TestMethod]
        public void TestConsoleInterfaceConstructorWithBrightSkinParameter()
        {
            var consoleInterface = new ConsoleInterface(new BrightSkin());
            var skin = typeof(ConsoleInterface).GetField("skin", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(consoleInterface);
            Assert.IsInstanceOfType(skin, typeof(BrightSkin));
        }

        [TestMethod]
        public void TestConsoleInterfaceShowMessage()
        {
            var consoleInterface = new ConsoleInterface();
            string message = "This is the message";
            var output = new StringWriter();
            Console.SetOut(output);
            consoleInterface.ShowMessage(message);
            Assert.AreEqual(message + "\r\n", output.ToString());
        }

        [TestMethod]
        public void TestConsoleInterfaceShowMessageWithNullParameter()
        {
            var consoleInterface = new ConsoleInterface();
            var output = new StringWriter();
            Console.SetOut(output);
            consoleInterface.ShowMessage(null);
            Assert.AreEqual("\r\n", output.ToString());
        }

        [TestMethod]
        public void TestConsoleInterfaceShowWelcomeMessage()
        {
            var consoleInterface = new ConsoleInterface();
            string expected = "Welcome to the game “Minesweeper”. Try to reveal all cells without mines.\r\n" +
                              "Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit the game.\r\n\r\n";
            var output = new StringWriter();
            Console.SetOut(output);
            consoleInterface.ShowWelcomeScreen();
            Assert.AreEqual(expected, output.ToString());
        }
    }
}