using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicroNet.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNet.Logging.Tests
{
    [TestClass()]
    public class LoggerTests
    {
        EventId even = new EventId(0, "Main");
        Exception ex = new Exception("测试异常信息");

        [TestMethod()]
        public void LogDebugTest()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogDebug("Trace", new[] { "错误信息" });
        }

        [TestMethod()]
        public void LogDebugTest1()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogDebug("Trace", even, new[] { "错误信息" });
        }

        [TestMethod()]
        public void LogDebugTest2()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogDebug("Trace", even, ex, new[] { "错误信息" });
        }

        [TestMethod()]
        public void LogInformationTest()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogInformation("Trace", new[] { "错误信息" });
        }

        [TestMethod()]
        public void LogInformationTest1()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogInformation("Trace", even, new[] { "错误信息" });
        }

        [TestMethod()]
        public void LogInformationTest2()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogInformation("Trace", even, ex, new[] { "错误信息" });
        }

        [TestMethod()]
        public void LogSQLTest()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogSQL("Trace", new[] { "错误信息" });
        }

        [TestMethod()]
        public void LogSQLTest1()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogSQL("Trace", even, new[] { "错误信息" });
        }

        [TestMethod()]
        public void LogSQLTest2()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogSQL("Trace", even, ex, new[] { "错误信息" });
        }

        [TestMethod()]
        public void LogTraceTest()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogTrace("Trace", new[] { "错误信息" });
        }

        [TestMethod()]
        public void LogTraceTest1()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogTrace("Trace", even, new[] { "错误信息" });
        }

        [TestMethod()]
        public void LogTraceTest2()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogTrace("Trace", even, ex, new[] { "错误信息" });
        }

        [TestMethod()]
        public void LogWarningTest()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogWarning("Trace", new[] { "错误信息" });
        }

        [TestMethod()]
        public void LogWarningTest1()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogWarning("Trace", even, new[] { "错误信息" });
        }

        [TestMethod()]
        public void LogWarningTest2()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogWarning("Trace", even, ex, new[] { "错误信息" });
        }

        [TestMethod()]
        public void LogErrorTest()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogError("Trace", new[] { "错误信息" });
        }

        [TestMethod()]
        public void LogErrorTest1()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogError("Trace", even, new[] { "错误信息" });
        }

        [TestMethod()]
        public void LogErrorTest2()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogError("Trace", even, ex, new[] { "错误信息" });
        }

        [TestMethod()]
        public void LogCriticalTest()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogCritical("Trace", new[] { "错误信息" });
        }

        [TestMethod()]
        public void LogCriticalTest1()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogCritical("Trace", even, new[] { "错误信息" });
        }

        [TestMethod()]
        public void LogCriticalTest2()
        {
            Logger.SetLogLevel = LogLevel.All;
            Logger.LogCritical("Trace", even, ex, new[] { "错误信息" });
        }
    }
}