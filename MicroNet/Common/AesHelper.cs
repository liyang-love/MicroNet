using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MicroNet.Common
{
    /// <summary>
    /// AES加密帮助类
    /// </summary>
    public sealed class AesHelper
    {
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="inputdata">输入的数据</param>
        /// <param name="iv">向量128位(Iv的字符串长度为16)</param>
        /// <param name="key">加密密钥(key的长度为32)</param>
        /// <returns></returns>
        public static byte[] AESEncrypt(string encryptString, string key, string iv)
        {
            //分组加密算法   
            SymmetricAlgorithm Aes = Rijndael.Create();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);//得到需要加密的字节数组       
            //设置密钥及密钥向量
            Aes.Key = Encoding.UTF8.GetBytes(key);
            Aes.IV = Encoding.UTF8.GetBytes(iv); ;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, Aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    byte[] cipherBytes = ms.ToArray();//得到加密后的字节数组   
                    cs.Close();
                    ms.Close();
                    return cipherBytes;
                    //return Convert.ToBase64String(cipherBytes); //返回字符串
                }
            }
        }


        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="inputdata">输入的数据</param>
        /// <param name="iv">向量128位(Iv的字符串长度为16)</param>
        /// <param name="key">加密密钥(key的长度为32)</param>
        /// <returns></returns>
        public static byte[] AESDecrypt(string decryptString, string key, string iv)
        {
            SymmetricAlgorithm Aes = Rijndael.Create();
            Aes.Key = Encoding.UTF8.GetBytes(key);
            Aes.IV = Encoding.UTF8.GetBytes(iv);
            byte[] inputByteArray = Convert.FromBase64String(decryptString);//得到需要加密的字节数组
            byte[] decryptBytes = new byte[inputByteArray.Length];
            using (MemoryStream ms = new MemoryStream(inputByteArray))
            {
                using (CryptoStream cs = new CryptoStream(ms, Aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cs.Read(decryptBytes, 0, decryptBytes.Length);
                    cs.Close();
                    ms.Close();
                }
            }
            return decryptBytes;
            //return Encoding.UTF8.GetString(decryptBytes); //返回字符串
        }
    }
}
