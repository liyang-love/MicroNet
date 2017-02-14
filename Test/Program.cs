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
using MicroNet.Common;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //LogTest();

            // SQLTest();

          //string sql=  ExecuteScalar("select sysdate from dual", "Data Source=192.168.20.91/orcl;Persist Security Info=True;User ID=C##FGCPOE;Password=3602001;");
          //  Console.WriteLine(sql);

          //  return;

            Snowflakea s = new Snowflakea(30,12);
           var QU= s.NextId();
            Console.WriteLine(QU);

            TestSnowflake();

            //var dt = GetTime("1288834974657L");

            //var re = ConvertDateTimeInt(DateTime.Parse("2015-01-01"));
            //dt = StampToDateTime("1288834974657L");
            //Console.WriteLine(dt);
            //Console.WriteLine(re);
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime;// = long.Parse(timeStamp + "0000000");
            lTime = 1420041600L;
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }


        // 时间戳转为C#格式时间
        static DateTime StampToDateTime(string timeStamp)
        {
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime= long.Parse("14200416000000000");
           lTime = 1420041600L;
            TimeSpan toNow = new TimeSpan(lTime);

            return dateTimeStart.Add(toNow);
        }

        // DateTime时间格式转换为Unix时间戳格式
        static int DateTimeToStamp(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }


        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// unix时间戳转换成日期
        /// </summary>
        /// <param name="unixTimeStamp">时间戳（秒）</param>
        /// <returns></returns>
        public static DateTime UnixTimestampToDateTime(DateTime target, long timestamp)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
            return start.AddSeconds(timestamp);
        }

        /// <summary>
        /// 日期转换成unix时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long DateTimeToUnixTimestamp(DateTime dateTime)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, dateTime.Kind);
            return Convert.ToInt64((dateTime - start).TotalSeconds);
        }


        static void TestSnowflake()
        {
            Console.WriteLine(SnowflakeHelper.Instance().NextId());

            //HashSet<long> set = new HashSet<long>();
            //SnowflakeHelper idWorker1 = new SnowflakeHelper(0, 0);
            //SnowflakeHelper idWorker2 = new SnowflakeHelper(1, 0);
            //762884413578018816
            //762884520121729024
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //for (int i = 0; i < 1; i++)
            //{
            //long id = idWorker1.nextId();
            //    set.Add(id);
            //    if (!set.Add(id))
            //    {
            //Console.WriteLine("duplicate:" + id);
            //}
            //    }
            //sw.Stop();
            //foreach (var item in set)
            //{
            //    Console.WriteLine("结果:" + item);
            //}
            //Console.WriteLine("时间:" + sw.ElapsedTicks);
        }



        static void SQLTest()
        {
            SQLHelper db = new SQLHelper("Constr");
            var data = db.Instance.GetData("select * from sys.tables");

            var model = db.Instance.GetData<Advice>("select name,object_id from sys.tables");

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


  public  class Snowflakea
    {

        //工作机器id的bit段拆分为前5个bit标记workerid，后5个bit标记datacenterid
        const int WorkerIdBits = 5;
        const int DatacenterIdBits = 5;
        //序列号bit数
        const int SequenceBits = 12;
        //最大编号限制
        const long MaxWorkerId = -1L ^ (-1L << WorkerIdBits);
        const long MaxDatacenterId = -1L ^ (-1L << DatacenterIdBits);
        private const long SequenceMask = -1L ^ (-1L << SequenceBits);
        //位左运算移动量
        public const int WorkerIdShift = SequenceBits;
        public const int DatacenterIdShift = SequenceBits + WorkerIdBits;
        public const int TimestampLeftShift = SequenceBits + WorkerIdBits + DatacenterIdBits;

        //序列号记录
        private long _sequence = 0L;
        //时间戳记录
        private long _lastTimestamp = -1L;


        public long WorkerId { get; protected set; }
        public long DatacenterId { get; protected set; }

        public Snowflakea(long workerId, long datacenterId, long sequence = 0L)
        {
            WorkerId = workerId;
            DatacenterId = datacenterId;
            _sequence = sequence;

            // sanity check for workerId
            if (workerId > MaxWorkerId || workerId < 0)
            {
                throw new ArgumentException(String.Format("worker Id can't be greater than {0} or less than 0", MaxWorkerId));
            }

            if (datacenterId > MaxDatacenterId || datacenterId < 0)
            {
                throw new ArgumentException(String.Format("datacenter Id can't be greater than {0} or less than 0", MaxDatacenterId));
            }

        }
        /// <summary>
        /// 格林时间戳
        /// </summary>
        /// <returns></returns>
        public long TimeGen()
        {
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);//
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        readonly object _lock = new Object();

        public virtual long NextId()
        {
            lock (_lock)
            {
                var timestamp = TimeGen();

                if (timestamp < _lastTimestamp)
                {
                    throw new Exception(String.Format(
                        "发现最新时间戳少{0}毫秒的异常", _lastTimestamp - timestamp));
                }

                if (_lastTimestamp == timestamp)
                {
                    _sequence = (_sequence + 1) & SequenceMask;
                    if (_sequence == 0)
                    {
                        //序列号超过限制，重新取时间戳
                        timestamp = TilNextMillis(_lastTimestamp);
                    }
                }
                else
                {
                    _sequence = 0;
                }

                _lastTimestamp = timestamp;
                //snowflake算法
                var id = (timestamp << TimestampLeftShift) |
                         (DatacenterId << DatacenterIdShift) |
                         (WorkerId << WorkerIdShift) | _sequence;

                return id;
            }
        }
        /// <summary>
        /// 重新取时间戳
        /// </summary>
        protected virtual long TilNextMillis(long lastTimestamp)
        {
            var timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                //新的时间戳要大于旧的时间戳，才算作有效时间戳
                timestamp = TimeGen();
            }
            return timestamp;
        }
    }

    public class Advice
    {
        public string 医嘱ID { get; set; }

        public string 主医嘱ID { get; set; }
    }
}
