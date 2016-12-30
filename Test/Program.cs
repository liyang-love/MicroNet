using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MicroNet.Logging;
using MicroNet.SQLHelpers;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //LogTest();

           // SQLTest();
        }



        static void SQLTest()
        {
            SQLHelper db = new SQLHelper("Constr");
            var data = db.Instance.GetData("select * from 医嘱_医生医嘱表");

            var model = db.Instance.GetData<Advice>("select * from 医嘱_医生医嘱表");

            OracleParameter[] OracleParameters = {
                        new OracleParameter("p_BQID", OracleDbType.Varchar2,200,"8001",ParameterDirection.Input),
                        new OracleParameter("P_UID", OracleDbType.Varchar2,200,"6666",ParameterDirection.Input),
                        new OracleParameter("P_KIND", OracleDbType.Char,200,"0",ParameterDirection.Input),
                        new OracleParameter("P_CUR", OracleDbType.RefCursor, ParameterDirection.Output)
                    };

            var drug = db.Instance.ExecuteStor("ZY_YS_GETYPINFOR", OracleParameters);
        }


        static void LogTest()
        {
            EventId even = new EventId();
            even.Id = 0;
            even.Name = "Main";

            Exception ex = new Exception("测试异常信息");

            Logger.SetLogLevel = LogLevel.All;
            //Logger.SetLogLevel = LogLevel.SQL;

            Logger.LogDebug("Trace", new[] { "错误信息" });
            Logger.LogDebug("Trace", even, new[] { "错误信息" });
            Logger.LogDebug("Trace", even, ex, new[] { "错误信息" });

            Logger.LogInformation("Trace", new[] { "错误信息" });
            Logger.LogInformation("Trace", even, new[] { "错误信息" });
            Logger.LogInformation("Trace", even, ex, new[] { "错误信息" });

            Logger.LogSQL("Trace", new[] { "错误信息" });
            Logger.LogSQL("Trace", even, new[] { "错误信息" });
            Logger.LogSQL("Trace", even, ex, new[] { "错误信息" });

            Logger.LogTrace("Trace", new[] { "错误信息" });
            Logger.LogTrace("Trace", even, new[] { "错误信息" });
            Logger.LogTrace("Trace", even, ex, new[] { "错误信息" });

            Logger.LogWarning("Trace", new[] { "错误信息" });
            Logger.LogWarning("Trace", even, new[] { "错误信息" });
            Logger.LogWarning("Trace", even, ex, new[] { "错误信息" });

            Logger.LogError("Trace", new[] { "错误信息" });
            Logger.LogError("Trace", even, new[] { "错误信息" });
            Logger.LogError("Trace", even, ex, new[] { "错误信息" });

            Logger.LogCritical("Trace", new[] { "错误信息" });
            Logger.LogCritical("Trace", even, new[] { "错误信息" });
            Logger.LogCritical("Trace", even, ex, new[] { "错误信息" });

            //Logger.LogSql("错误", "debug");
            Console.ReadKey();
            //Trace.Listeners.Add(new TextWriterTraceListener(DateTime.Now.ToString("yyyy-MM-dd") + ".log"));
            //Trace.AutoFlush = true;
            //DoProcessing();
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


    public class Advice
    {
        public string 医嘱ID { get; set; }

        public string 主医嘱ID { get; set; }
    }
}
