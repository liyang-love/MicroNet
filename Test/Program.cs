﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MicroNet.Logging;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            EventId even = new EventId();
            even.Id = 0;
            even.Name = "Main";

            Exception ex = new Exception("测试异常信息");

            Logger.SetLogLevel = LogLevel.All;
            //Logger.SetLogLevel = LogLevel.SQL;

            Logger.LogDebug("Trace", "错误信息");
            Logger.LogDebug("Trace", even, "错误信息");
            Logger.LogDebug("Trace", even, ex, "错误信息");

            Logger.LogInformation("Trace", "错误信息");
            Logger.LogInformation("Trace", even, "错误信息");
            Logger.LogInformation("Trace", even, ex, "错误信息");

            Logger.LogSQL("Trace", "错误信息");
            Logger.LogSQL("Trace", even, "错误信息");
            Logger.LogSQL("Trace", even, ex, "错误信息");

            Logger.LogTrace("Trace", "错误信息");
            Logger.LogTrace("Trace", even, "错误信息");
            Logger.LogTrace("Trace", even, ex, "错误信息");

            Logger.LogWarning("Trace", "错误信息");
            Logger.LogWarning("Trace", even, "错误信息");
            Logger.LogWarning("Trace", even, ex, "错误信息");

            Logger.LogError("Trace", "错误信息");
            Logger.LogError("Trace", even, "错误信息");
            Logger.LogError("Trace", even, ex, "错误信息");

            Logger.LogCritical("Trace", "错误信息");
            Logger.LogCritical("Trace", even, "错误信息");
            Logger.LogCritical("Trace", even, ex, "错误信息");

            //Logger.LogSql("错误", "debug");
            Console.ReadKey();
            //Trace.Listeners.Add(new TextWriterTraceListener(DateTime.Now.ToString("yyyy-MM-dd") + ".log"));
            //Trace.AutoFlush = true;
            //DoProcessing();
        }

        //[CallerMemberName]
        static void Get()
        {
            throw new Exception("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        }

        //static void DoProcessing()
        //{
        //    TraceMessage("Something happened.");
        //}

        //static void TraceMessage(string message,
        //        [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        //        [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        //        [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        //{
        //    System.Diagnostics.Trace.WriteLine("message: " + message);
        //    System.Diagnostics.Trace.WriteLine("member name: " + memberName);
        //    System.Diagnostics.Trace.WriteLine("source file path: " + sourceFilePath);
        //    System.Diagnostics.Trace.WriteLine("source line number: " + sourceLineNumber);
        //}
    }


    //namespace System.Runtime.CompilerServices
    //{
    //    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    //    public class CallerMemberNameAttribute : Attribute { }
    //    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    //    public class CallerFilePathAttribute : Attribute { }
    //    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    //    public class CallerLineNumberAttribute : Attribute { }
    //}
}