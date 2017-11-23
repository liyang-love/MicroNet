using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroNet.Logging
{
    /// <summary>
    /// 定义日志级别
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 摘要: 不记录任何信息
        /// </summary>
        None = 0,

        /// <summary>
        /// 摘要:记录所有信息
        /// </summary>
        All = 1,

        /// <summary>
        /// 摘要:调试跟踪。 
        /// </summary>
        Debug = 2,

        /// <summary>
        /// 摘要:信息性消息。
        /// </summary>
        Information = 4,

        /// <summary>
        ///  摘要:记录SQL语句。
        /// </summary>
        SQL = 8,

        /// <summary>
        /// 摘要:关于可能影响设备功能的情况 [例如高可用性 (HA) 状态更改 ]的消息
        /// </summary>
        Trace = 16,

        /// <summary>
        /// 摘要:非关键性问题。
        /// </summary>
        Warning = 32,

        /// <summary>
        /// 摘要:可恢复的错误。
        /// </summary>
        Error = 64,

        /// <summary>
        /// 摘要:错误或应用程序崩溃
        /// </summary>
        Critical = 128
    }
}
