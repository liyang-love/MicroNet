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
    public class AesHelperTests
    {
        [TestMethod()]
        public void AESEncryptTest()
        {
           var aes= AesHelper.AESEncrypt("qwesseeed", "12345678123456781234567812345678", "1234567812345678");
            Assert.IsTrue(aes.Length>0);
        }

        [TestMethod()]
        public void AESDecryptTest()
        {
            var Aes =Convert.ToBase64String( AesHelper.AESEncrypt("qwesseeed", "12345678123456781234567812345678", "1234567812345678"));
            var DeAes = AesHelper.AESDecrypt(Aes, "12345678123456781234567812345678", "1234567812345678");
            Assert.IsTrue(DeAes.Length>0);
        }
    }
}