using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MicroNet.Log
{
    /// <summary>
    /// 定义错误日志接口
    /// </summary>
    public sealed class Logger
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        private static bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        /// <summary>
        /// 写入Critical级别日志
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogCritical(string category, params object[] args)
        {
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            if (IsEnabled(LogLevel.Critical))
            {
                Trace.WriteLine(string.Join(",", args) + "\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--" + category);
            }
            //});
        }

        /// <summary>
        /// 写入Critical级别日志
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="eventId"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogCritical(string category, EventId eventId, params object[] args)
        {
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            if (IsEnabled(LogLevel.Critical))
            {
                string info = string.Format("{0} \r\n其他信息:{1}\r\n", eventId.ToString(), string.Join(",", args));
                Trace.WriteLine(info, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--" + category);
            }
            //});
        }

        /// <summary>
        /// 写入Critical级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogCritical(string category, EventId eventId, Exception exception, params object[] args)
        {
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            if (IsEnabled(LogLevel.Critical))
            {
                string info = string.Format("{0} \r\n 异常信息:{1} \r\n其他信息:{2}\r\n", eventId.ToString(), exception.ToString(), string.Join(",", args));
                Trace.WriteLine(info, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--" + category);
            }
            //});
        }

        /// <summary>
        /// 写入Debug级别日志
        /// </summary>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogDebug(string category, params object[] args)
        {
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            if (IsEnabled(LogLevel.Debug))
            {
                Trace.WriteLine(string.Join(",", args) + "\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--" + category);
            }
            //});
        }

        /// <summary>
        /// 写入Debug级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogDebug(string category, EventId eventId, params object[] args)
        {
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            if (IsEnabled(LogLevel.Debug))
            {
                string info = string.Format("{0} \r\n其他信息:{1}\r\n", eventId.ToString(), string.Join(",", args));
                Trace.WriteLine(info, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--" + category);
            }
            //});
        }

        /// <summary>
        /// 写入Debug级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogDebug(string category, EventId eventId, Exception exception, params object[] args)
        {
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            if (IsEnabled(LogLevel.Debug))
            {
                string info = string.Format("{0} \r\n 异常信息:{1} \r\n其他信息:{2}\r\n", eventId.ToString(), exception.ToString(), string.Join(",", args));
                Trace.WriteLine(info, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--" + category);
            }
            //});
        }

        /// <summary>
        /// 写入Error级别日志
        /// </summary>
        /// <param name="category">分类标识</param>
        /// <param name="args">记录信息</param>
        public static void LogError(string category, params object[] args)
        {
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            if (IsEnabled(LogLevel.Error))
            {
                Trace.WriteLine(string.Join(",", args) + "\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--" + category);
            }
            //});
        }

        /// <summary>
        /// /// <summary>
        /// 写入Error级别日志
        /// </summary>
        /// <param name="category"></param>
        /// <param name="args"></param>
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogError(string category, EventId eventId, params object[] args)
        {
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            if (IsEnabled(LogLevel.Error))
            {
                string info = string.Format("{0} \r\n其他信息:{1}\r\n", eventId.ToString(), string.Join(",", args));
                Trace.WriteLine(info, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--" + category);
            }
            //});
        }

        /// <summary>
        /// /// <summary>
        /// 写入Error级别日志
        /// </summary>
        /// <param name="category"></param>
        /// <param name="args"></param>
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogError(string category, EventId eventId, Exception exception, params object[] args)
        {
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            if (IsEnabled(LogLevel.Error))
            {
                string info = string.Format("{0} \r\n 异常信息:{1} \r\n其他信息:{2}\r\n", eventId.ToString(), exception.ToString(), string.Join(",", args));
                Trace.WriteLine(info, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--" + category);
            }
            //});
        }

        /// <summary>
        /// /// <summary>
        /// 写入Information级别日志
        /// </summary>
        /// <param name="category">分类标识</param>
        /// <param name="args">记录信息</param>
        public static void LogInformation(string category, params object[] args)
        {
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            if (IsEnabled(LogLevel.Information))
            {
                Trace.WriteLine(string.Join(",", args), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--" + category);
            }
            //});
        }

        /// <summary>
        /// 写入Information级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogInformation(string category, EventId eventId, params object[] args)
        {
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            if (IsEnabled(LogLevel.Information))
            {
                string info = string.Format("{0} \r\n其他信息:{1}\r\n", eventId.ToString(), string.Join(",", args));
                Trace.WriteLine(info, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--" + category);
            }
            //});
        }

        /// <summary>
        /// 写入Information级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogInformation(string category, EventId eventId, Exception exception, params object[] args)
        {
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            if (IsEnabled(LogLevel.Information))
            {
                string info = string.Format("{0} \r\n 异常信息:{1} \r\n其他信息:{2}\r\n", eventId.ToString(), exception.ToString(), string.Join(",", args));
                Trace.WriteLine(info, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--" + category);
            }
            //});
        }

        /// <summary>
        /// 写入Trace级别日志
        /// </summary>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogTrace(string category, params object[] args)
        {
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            if (IsEnabled(LogLevel.Trace))
            {
                Trace.WriteLine(string.Join(",", args) + "\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--" + category);
            }
            //});
        }

        /// <summary>
        /// 写入Trace级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogTrace(string category, EventId eventId, params object[] args)
        {
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            if (IsEnabled(LogLevel.Trace))
            {
                string info = string.Format("{0} \r\n其他信息:{1}\r\n", eventId.ToString(), string.Join(",", args));
                Trace.WriteLine(info, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--" + category);
            }
            //});
        }

        /// <summary>
        /// 写入Trace级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogTrace(string category, EventId eventId, Exception exception, params object[] args)
        {
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            if (IsEnabled(LogLevel.Trace))
            {
                string info = string.Format("{0} \r\n 异常信息:{1} \r\n其他信息:{2}\r\n", eventId.ToString(), exception.ToString(), string.Join(",", args));
                Trace.WriteLine(info, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--" + category);
            }
            //});
        }

        /// <summary>
        /// 写入Warning级别日志
        /// </summary>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogWarning(string category, params object[] args)
        {
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            if (IsEnabled(LogLevel.Warning))
            {
                Trace.WriteLine(string.Join(",", args) + "\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--" + category);
            }
            //});
        }

        /// <summary>
        /// 写入Warning级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogWarning(string category, EventId eventId, params object[] args)
        {
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            if (IsEnabled(LogLevel.Warning))
            {
                string info = string.Format("{0} \r\n其他信息:{1}\r\n", eventId.ToString(), string.Join(",", args));
                Trace.WriteLine(info, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--" + category);
            }
            //});
        }

        /// <summary>
        /// 写入Warning级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogWarning(string category, EventId eventId, Exception exception, params object[] args)
        {
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            if (IsEnabled(LogLevel.Warning))
            {
                string info = string.Format("{0} \r\n 异常信息:{1} \r\n其他信息:{2}\r\n", eventId.ToString(), exception.ToString(), string.Join(",", args));
                Trace.WriteLine(info, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--" + category);
            }
            // });
        }

        public static void LogSql(string methodName, string sql)
        {
            //ThreadPool.QueueUserWorkItem((e) =>
            //{
            if (IsEnabled(LogLevel.Information))
            {
                Trace.WriteLine(sql + "\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "--SQL语句 " + methodName);
            }
            // });
        }
    }
}
