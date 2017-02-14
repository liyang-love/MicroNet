using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicroNet.SQLHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNet.SQLHelpers.Tests
{
    [TestClass()]
    public class SQLHelperTests
    {
        SQLHelper db = new SQLHelper("Constr");

        [TestMethod()]
        public void ExecuteNoQueryTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ExecuteNoQueryTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ExecuteNoQueryTest2()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ExecuteScalarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ExecuteScalarTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ExecuteStorTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ExecuteStorTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ExecuteStoreTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ExecuteStoreTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetDataTest()
        {
            var data = db.Instance.GetData("select * from 医嘱_医生医嘱表");
            Assert.IsTrue(data.Rows.Count>=0);
        }

        [TestMethod()]
        public void GetDataTest1()
        {
            var model = db.Instance.GetData<Advice>("select * from 医嘱_医生医嘱表");
            Assert.IsTrue((model==null||model.Count >= 0));
        }

        [TestMethod()]
        public void GetDataTest2()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetDataTest3()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ExecuteStorToOutTest()
        {
            Assert.Fail();
        }
    }

    public class Advice
    {
        public string name { get; set; }

        public string object_id { get; set; }
    }
}