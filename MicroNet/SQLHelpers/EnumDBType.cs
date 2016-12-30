using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroNet.SQLHelpers
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum EnumDBType
    {
        /// <summary>
        /// 不配置数据库
        /// </summary>
        None = 0,

        /// <summary>
        /// SQLServer数据库为1
        /// </summary>
        SQLServer = 1,

        /// <summary>
        /// Oracle数据库 为2
        /// </summary>
        Oracle = 2,

        /// <summary>
        /// MySQL为3
        /// </summary>
        MySQL = 3,

        /// <summary>
        /// Sysbase为4
        /// </summary>
        Sybase = 4,

        /// <summary>
        /// PostgreSQL 5
        /// </summary>
        PostgreSQL = 5,

        /// <summary>
        /// SQLite  6
        /// </summary>
        SQLite = 6,

        /// <summary>
        /// DB2 7
        /// </summary>
        DB2 = 7,

        /// <summary>
        /// Npqsql 8
        /// </summary>
        Npqsql = 8,

        /// <summary>
        /// MongoDB
        /// </summary>
        MongoDB = 9,

        /// <summary>
        /// Redis 10
        /// </summary>
        Redis = 10,

        /// <summary>
        /// SqlCe  11
        /// </summary>
        SqlCe = 11
    }
}
