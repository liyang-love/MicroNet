using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroNet.Common
{
    /// <summary>
    /// 获取Guid帮助类
    /// </summary>
    public sealed class GuidHelper
    {
        /// <summary>
        /// 获取新的Guid 返回Guid类型
        /// </summary>
        public static Guid GetGuid()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// 获取Guid 返回string类型
        /// 返回结果为全部大写字符
        /// </summary>
        /// <returns></returns>
        public static string GetGuidToUpper()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }

        /// <summary>
        /// 获取Guid  返回不带有 “-”的Guid  
        /// 返回结果为全部大写字符
        /// </summary>
        /// <returns></returns>
        public static string GetNGuidToUpper()
        {
            return Guid.NewGuid().ToString("N").ToUpper();
        }

        /// <summary>
        /// 获取Guid 返回string类型
        /// 返回结果为全部小写字符
        /// </summary>
        /// <returns></returns>
        public static string GetGuidToLower()
        {
            return Guid.NewGuid().ToString().ToLower();
        }

        /// <summary>
        /// 获取Guid  返回不带有 “-”的Guid 
        /// 返回结果为全部小写字符
        /// </summary>
        /// <returns></returns>
        public static string GetNGuidToLower()
        {
            return Guid.NewGuid().ToString("N").ToUpper();
        }
    }
}
