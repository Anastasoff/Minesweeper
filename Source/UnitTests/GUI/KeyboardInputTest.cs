namespace UnitTests.GUI
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.Interfaces;
    using Minesweeper.GUI;
    using Moq;
    using System.IO;

    [TestClass]
    public class KeyboardInputTest
    {
        [TestMethod]
        public void TestKeyboardInputReturnsRightString()
        {
            var input = new StringReader("some text");  
            Console.SetIn(input);
            IInputDevice keyboard = new KeyboardInput();
            var parsedInput = keyboard.GetInput();
            Assert.AreEqual("some text", parsedInput);
        }

        [TestMethod]
        public void TestKeyboardInputTrimsTheInput()
        {
            var input = new StringReader("   some text   ");
            Console.SetIn(input);
            IInputDevice keyboard = new KeyboardInput();
            var parsedInput = keyboard.GetInput();
            Assert.AreEqual("some text", parsedInput);
        }
    }
}