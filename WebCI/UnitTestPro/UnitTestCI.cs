using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestPro
{
    [TestClass]
    public class UnitTestCI
    {
        [TestMethod]
        public void CItest()
        {
            var num = 5;

            Assert.AreEqual(5, num);
        }
    }
}
