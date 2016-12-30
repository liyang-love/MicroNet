using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroNet.Common
{
    /// <summary>
    /// 字符串帮助类
    /// </summary>
    public static class StringHelper
    {

        /// <summary>
        /// Null转成""  非空则移除里面的空白
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string NullToString(object obj)
        {
            if (obj == null)
                return string.Empty;
            else
            {
                return obj.ToString().Trim();
            }
        }

    }
}
