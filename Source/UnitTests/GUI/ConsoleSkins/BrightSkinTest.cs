namespace UnitTests.GUI.ConsoleSkins
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.GUI.ConsoleSkins;
    using Minesweeper.Interfaces;

    [TestClass]
    public class BrightSkinTest
    {
        [TestMethod]
        public void TestBrightSkinIfColorsCollectionHasElements()
        {
            IConsoleSkin skin = new BrightSkin();
            Assert.AreNotEqual(0, skin.ColorScheme.Count);
        }

        /// <summary>
        /// By design the bright skin has four colors.
        /// </summary>
        [TestMethod]
        public void TestBrightSkinIfColorsCollectionHasFourElements()
        {
            IConsoleSkin skin = new BrightSkin();
            Assert.AreEqual(4, skin.ColorScheme.Count);
        }
    }
}