using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicroNet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNet.Common.Tests
{
    [TestClass()]
    public class SnowflakeHelperTests
    {
        [TestMethod()]
        public void NextIdTest()
        {
            Assert.IsTrue(SnowflakeHelper.Instance().NextId()>0);
        }
    }
}