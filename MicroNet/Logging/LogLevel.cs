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
        //
        // 摘要:
        // 不记录任何信息
        None = 0,
        //
        // 摘要:
        // 记录所有信息
        All = 1,

        //
        // 摘要:
        // 调试跟踪。
        Debug = 2,

        //
        // 摘要:
        //     信息性消息。
        Information = 4,

        //
        // 摘要:
        //     记录SQL语句。
        SQL = 8,

        //
        // 摘要:
        //关于可能影响设备功能的情况 [例如高可用性 (HA) 状态更改 ]的消息
        Trace = 16,

        //
        // 摘要:
        // 非关键性问题。
        Warning = 32,

        //
        // 摘要:
        // 可恢复的错误。
        Error = 64,

        //
        // 摘要:
        //     错误或应用程序崩溃
        Critical = 128
    }
}
