namespace UnitTests.GUI
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.GUI;
    using Minesweeper.Interfaces;

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