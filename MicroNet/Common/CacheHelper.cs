using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroNet.Common
{
    /// <summary>
    /// 缓存帮助类
    /// </summary>
    public sealed class CacheHelper
    {
        private static ConcurrentDictionary<object, object> dic = new ConcurrentDictionary<object, object>();

        /// <summary>
        /// 添加缓存
        /// </summary>
        public static bool Add(object key, object value)
        {
            if (string.IsNullOrWhiteSpace(key.ToString()))
            {
                throw new ArgumentNullException("键值不能为空或null值");
            }
            return dic.TryAdd(key, value);

        }

        /// <summary>
        /// 存在则直接获取不存在则添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object GetOrAdd(object key, object value)
        {
            if (string.IsNullOrWhiteSpace(key.ToString()))
            {
                throw new ArgumentNullException("键值不能为空或null值");
            }
            return dic.GetOrAdd(key, value);
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Remove(object key)
        {
            if (string.IsNullOrWhiteSpace(key.ToString()))
            {
                throw new ArgumentNullException("键值不能为空或null值");
            }

            object _obj;
            return dic.TryRemove(key, out _obj);
        }

        /// <summary>
        /// 更新键值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">要更新的值</param>
        /// <returns></returns>
        public static bool Update(object key, object value)
        {
            if (string.IsNullOrWhiteSpace(key.ToString()))
            {
                throw new ArgumentNullException("键值不能为空或null值");
            }
            object _obj;
            if (dic.TryGetValue(key, out _obj))
            {
                return dic.TryUpdate(key, _obj, value);
            }
            return false;
        }

        /// <summary>
        /// 根据主键获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(object key)
        {
            object _obj;
            if (dic.TryGetValue(key, out _obj))
            {
                return _obj;
            }
            return null;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    dic.Clear();
                    // TODO: 释放托管状态(托管对象)。
                }
                dic = null;

                disposedValue = true;
            }
        }


        /// <summary>
        /// 释放资料
        /// </summary>
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 清除所有缓存数据
        /// </summary>
        public void Clear()
        {
            dic.Clear();
        }

        #endregion

    }
}
