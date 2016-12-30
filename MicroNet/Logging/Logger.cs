using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MicroNet.Logging
{
    /// <summary>
    /// 定义错误日志接口
    /// </summary>
    public sealed class Logger
    {
        //static Logger()
        //{
        //    Trace.Listeners.Add(new TextWriterTraceListener(DateTime.Now.ToString("yyyy-MM-dd") + ".log"));
        //    Trace.AutoFlush = true;
        //}

        /// <summary>
        /// 设置日志级别
        /// </summary>
        public static LogLevel SetLogLevel { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        private static bool IsEnabled(LogLevel logLevel)
        {
            if (SetLogLevel == LogLevel.None)
            {
                return false;
            }
            if (SetLogLevel == LogLevel.All)
            {
                return true;
            }
            if (SetLogLevel <= logLevel)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 写入Debug级别日志
        /// </summary>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogDebug(
                string category,
                object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.Debug))
            {
                WriteFile(LogLevel.Debug, category, string.Join(",", args), memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 写入Debug级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogDebug(
                string category,
                EventId eventId,
                 object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.Debug))
            {
                string info = string.Format("{0},\r\n {1}", eventId.ToString(), string.Join(",", args));
                WriteFile(LogLevel.Debug, category, info, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 写入Debug级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogDebug(
                string category,
                EventId eventId,
                Exception exception,
                 object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.Debug))
            {
                string info = string.Format("{0},\r\n 异常:{1},\r\n 其他:{2}", eventId.ToString(), exception, string.Join(",", args));
                WriteFile(LogLevel.Debug, category, info, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 写入Information级别日志
        /// </summary>
        /// <param name="category">分类标识</param>
        /// <param name="args">记录信息</param>
        public static void LogInformation(
                string category,
                 object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.Information))
            {
                WriteFile(logLevel: LogLevel.Information, category: category, content: string.Join(",", args), memberName: memberName, sourceFilePath: sourceFilePath, sourceLineNumber: sourceLineNumber);
            }
        }

        /// <summary>
        /// 写入Information级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogInformation(
                string category,
                EventId eventId,
                 object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.Information))
            {
                string info = string.Format("{0},\r\n 其他:{1}", eventId.ToString(), string.Join(",", args));
                WriteFile(LogLevel.Information, category, info, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 写入Information级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogInformation(
                string category,
                EventId eventId,
                Exception exception,
                 object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.Information))
            {
                string info = string.Format("{0},\r\n 异常:{1},\r\n 其他:{2}", eventId.ToString(), exception, string.Join(",", args));
                WriteFile(LogLevel.Information, category, info, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 写入Sql级别日志
        /// </summary>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogSQL(
                string category,
                 object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.SQL))
            {
                WriteFile(LogLevel.SQL, category, string.Join(",", args), memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 写入Sql级别日志
        /// </summary>
        /// <param name="category"></param>
        /// <param name="args"></param>
        /// <param name="eventId"></param>
        public static void LogSQL(
                string category,
                EventId eventId,
                 object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.SQL))
            {
                string info = string.Format("{0},\r\n 其他:{1}", eventId.ToString(), string.Join(",", args));
                WriteFile(LogLevel.SQL, category, info, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 写入Sql级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogSQL(
                string category,
                EventId eventId,
                Exception exception,
                 object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.SQL))
            {
                string info = string.Format("{0} ,\r\n 异常:{1},\r\n 其他:{2}", eventId.ToString(), exception, string.Join(",", args));
                WriteFile(LogLevel.SQL, category, info, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 写入Trace级别日志
        /// </summary>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogTrace(
                string category,
                 object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.Trace))
            {
                WriteFile(LogLevel.Trace, category, string.Join(",", args), memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 写入Trace级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogTrace(
                string category,
                EventId eventId,
                 object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.Trace))
            {
                string info = string.Format("{0},\r\n 其他:{1}", eventId.ToString(), string.Join(",", args));
                WriteFile(LogLevel.Trace, category, info, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 写入Trace级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogTrace(
                string category,
                EventId eventId,
                Exception exception,
                 object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.Trace))
            {
                string info = string.Format("{0} ,\r\n 异常:{1},\r\n 其他:{2}", eventId.ToString(), exception, string.Join(",", args));
                WriteFile(LogLevel.Trace, category, info, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 写入Warning级别日志
        /// </summary>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogWarning(
                string category,
                 object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.Warning))
            {
                WriteFile(LogLevel.Warning, category, string.Join(",", args), memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 写入Warning级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogWarning(
                string category,
                EventId eventId,
                 object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.Warning))
            {
                string info = string.Format("{0},\r\n 其他:{1}", eventId.ToString(), string.Join(",", args));
                WriteFile(LogLevel.Warning, category, info, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 写入Warning级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogWarning(
                string category,
                EventId eventId,
                Exception exception,
                 object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.Warning))
            {
                string info = string.Format("{0} ,\r\n 异常:{1},\r\n 其他:{2}", eventId.ToString(), exception, string.Join(",", args));
                WriteFile(LogLevel.Warning, category, info, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 写入Error级别日志
        /// </summary>
        /// <param name="category">分类标识</param>
        /// <param name="args">记录信息</param>
        public static void LogError(
                string category,
                 object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.Error))
            {
                WriteFile(LogLevel.Error, category, string.Join(",", args), memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 写入Error级别日志
        /// </summary>
        /// <param name="category"></param>
        /// <param name="args"></param>
        /// <param name="eventId"></param>
        public static void LogError(
                string category,
                EventId eventId,
                 object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.Error))
            {
                string info = string.Format("{0},\r\n 其他:{1}", eventId.ToString(), string.Join(",", args));
                WriteFile(LogLevel.Error, category, info, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 写入Error级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogError(
                string category,
                EventId eventId,
                Exception exception,
                 object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.Error))
            {
                string info = string.Format("{0},\r\n 异常:{1},\r\n 其他:{2}", eventId.ToString(), exception, string.Join(",", args));
                WriteFile(LogLevel.Error, category, info, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 写入Critical级别日志
        /// </summary>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogCritical(
                string category,
                 object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.Critical))
            {
                WriteFile(LogLevel.Critical, category, string.Join(",", args), memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 写入Critical级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogCritical(
                string category,
                EventId eventId,
                 object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.Critical))
            {
                string info = string.Format("{0},\r\n 其他:{1}", eventId.ToString(), string.Join(",", args));
                WriteFile(LogLevel.Critical, category, info, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 写入Critical级别日志
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="category"></param>
        /// <param name="args"></param>
        public static void LogCritical(
                string category,
                EventId eventId,
                Exception exception,
                 object[] args,
                [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
                [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
                [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (IsEnabled(LogLevel.Critical))
            {
                string info = string.Format("{0},\r\n 异常:{1},\r\n 其他:{2}", eventId.ToString(), exception, string.Join(",", args));
                WriteFile(LogLevel.Critical, category, info, memberName, sourceFilePath, sourceLineNumber);
            }
        }


        private static void WriteFile(LogLevel logLevel, string category, string content, string memberName,
                                      string sourceFilePath, int sourceLineNumber)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Log");

                }
                using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    string info = string.Format("[{0}]-【{1}】-[{2}]:\r\n Function:{3} \r\n SourceFile:{4} ,CodeLine:{5}　\r\n 日志:{6}　　\r\n\r\n",
                                               DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                               logLevel.ToString(),
                                               category,
                                               memberName,
                                               sourceFilePath,
                                               sourceLineNumber,
                                               content);
                    byte[] bty = Encoding.UTF8.GetBytes(info);
                    fs.Write(bty, 0, bty.Length);

                    fs.Flush();
                    fs.Close();
                    fs.Dispose();
                }
            }
            catch
            {
            }
        }
    }
}

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class CallerMemberNameAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class CallerFilePathAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class CallerLineNumberAttribute : Attribute { }
}
