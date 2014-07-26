namespace UnitTests.GUI.ConsoleSkins
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.GUI.ConsoleSkins;
    using Minesweeper.Interfaces;
    
    [TestClass]
    public class AllWhiteSkinTest
    {
        /// <summary>
        /// All white skin has no predefined colors, so we expect the collection holding them
        /// to have zero elements.
        /// </summary>
        [TestMethod]
        public void TestAllWhiteSkinColorsCount()
        {
            IConsoleSkin skin = new AllWhiteSkin();
            Assert.AreEqual(0, skin.ColorScheme.Count);
        }
    }
}