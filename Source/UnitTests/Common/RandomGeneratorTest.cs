namespace UnitTests.Common
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.Common;
    
    [TestClass]
    public class RandomGeneratorTest
    {
        [TestMethod]
        public void TestRandomGeneratorInstanceTypeIsRandom()
        {
            var random = RandomGenerator.GetInstance;
            Assert.AreEqual(typeof(Random), random.GetType());
        }
    }
}