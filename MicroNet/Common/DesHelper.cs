using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MicroNet.Common
{
    public sealed class DesHelper
    {
        /// <summary>
        /// des加密类
        /// </summary>
        /// <param name="encryptString">待加密字符</param>
        /// <param name="key">加密所需  Key</param>
        /// <param name="iv">加密所需  IV</param>
        /// <returns>加密后的结果</returns>
        public static string DesEncrypt(string encryptString, string key, string iv)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(key);
            byte[] rgbIV = Encoding.UTF8.GetBytes(iv);
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            using (MemoryStream mStream = new MemoryStream())
            {
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
        }

        /// <summary>
        /// des解密类
        /// </summary>
        /// <param name="decryptString">待加密字符</param>
        /// <param name="key">加密所需  Key</param>
        /// <param name="iv">加密所需  IV</param>
        /// <returns>解密后的结果</returns>
        public static string DesDecrypt(string decryptString, string key, string iv)
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(key);
            byte[] rgbIV = Encoding.UTF8.GetBytes(iv);
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            using (MemoryStream mStream = new MemoryStream())
            {
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
        }
    }
}
