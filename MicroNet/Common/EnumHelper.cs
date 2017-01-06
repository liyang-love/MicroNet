using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroNet.Common
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 根据值取枚举的名称
        /// </summary>
        /// <param name="enm"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetName(Type enm, int value)
        {
            var name = Enum.GetName(enm, value);
            return name;
        }

    }
}
