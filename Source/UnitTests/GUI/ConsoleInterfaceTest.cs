namespace UnitTests.GUI
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.GUI;
    using Minesweeper.GUI.ConsoleSkins;
    using System.Reflection;
    using Minesweeper.Interfaces;
    using System.IO;
    
    [TestClass]
    public class ConsoleInterfaceTest
    {
        [TestMethod]
        public void TestConsoleInterfaceConstructorWithoutParameterInitializesAllWhiteSkin()
        {
            var cInterface = new ConsoleInterface();
            var skin = typeof(ConsoleInterface).GetField("skin", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(cInterface);
            Assert.IsInstanceOfType(skin, typeof(AllWhiteSkin));
        }

        [TestMethod]
        public void TestConsoleInterfaceConstructorWithAllWhiteSkinParameter()
        {
            var cInterface = new ConsoleInterface(new AllWhiteSkin());
            var skin = typeof(ConsoleInterface).GetField("skin", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(cInterface);
            Assert.IsInstanceOfType(skin, typeof(AllWhiteSkin));
        }

        [TestMethod]
        public void TestConsoleInterfaceConstructorWithBrightSkinParameter()
        {
            var cInterface = new ConsoleInterface(new BrightSkin());
            var skin = typeof(ConsoleInterface).GetField("skin", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(cInterface);
            Assert.IsInstanceOfType(skin, typeof(BrightSkin));
        }

        [TestMethod]
        public void TestConsoleInterfaceShowMessage()
        {
            var cInterface = new ConsoleInterface();
            string message = "This is the message";
            var output = new StringWriter();
            Console.SetOut(output);
            cInterface.ShowMessage(message);
            Assert.AreEqual(message + "\r\n", output.ToString());
        }

        [TestMethod]
        public void TestConsoleInterfaceShowMessageWithNullParameter()
        {
            var cInterface = new ConsoleInterface();
            string message = "This is the message";
            var output = new StringWriter();
            Console.SetOut(output);
            cInterface.ShowMessage(null);
            Assert.AreEqual("\r\n", output.ToString());
        }

        [TestMethod]
        public void TestConsoleInterfaceShowWelcomeMessage()
        {
            var cInterface = new ConsoleInterface();
            string expected = "Welcome to the game “Minesweeper”. Try to reveal all cells without mines.\r\n" +
                "Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit the game.\r\n\r\n";
            var output = new StringWriter();
            Console.SetOut(output);
            cInterface.ShowWelcomeScreen();
            Assert.AreEqual(expected, output.ToString());
        }
    }
}