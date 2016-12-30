using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;

namespace MicroNet.SQLHelpers
{
    /// <summary>
    /// SQLHelper帮助类
    /// </summary>
    public sealed class SQLHelper
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        private EnumDBType EnumDBType { get; set; }

        /// <summary>
        /// 链接字符串
        /// </summary>
        private ConnectionStringSettings ConnectionString { get; set; }

        /// <summary>
        /// 初始化SqlHelper
        /// </summary>
        /// <param name="connectionStringName">链接字符串名称</param>
        [Description("connectionStringName：链接字符串名称")]
        public SQLHelper(string connectionStringName)
        {
            this.EnumDBType = GetDBType(connectionStringName);
        }

        /// <summary>
        /// 获取sqlHelper所执行的实例
        /// </summary>
        [Description("获取sqlHelper所执行的实例")]
        public ISQLHelper Instance
        {
            get
            {
                switch (EnumDBType)
                {
                    case EnumDBType.None:
                        return null;

                    case EnumDBType.SQLServer:
                        return new SQLServerHelper(ConnectionString.ConnectionString);

                    case EnumDBType.Oracle:
                        return new OracleHelper(ConnectionString.ConnectionString);

                    case EnumDBType.MySQL:
                        return new MySQLHelper(ConnectionString.ConnectionString);

                    case EnumDBType.MongoDB:
                        return null;

                    case EnumDBType.Redis:
                        return null;

                    case EnumDBType.DB2:
                        return null;

                    case EnumDBType.SQLite:
                        return null;

                    case EnumDBType.SqlCe:
                        return null;

                    default:
                        throw new SQLHelperException("找不到所传入数据库类型的所对应的解释器");
                }
            }
        }

        private EnumDBType GetDBType(string connectionStringName)
        {
            ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (ConnectionString != null)
            {
                switch (ConnectionString.ProviderName)
                {
                    case "System.Data.SqlClient":
                        return EnumDBType.SQLServer;

                    case "System.Data.OracleClient":
                        return EnumDBType.Oracle;

                    case "Oracle.ManagedDataAccess.Client":
                        return EnumDBType.Oracle;

                    case "MySql.Data.MySqlClient":
                        return EnumDBType.MySQL;
                    case "Npgsql":
                        return EnumDBType.PostgreSQL;
                    case "ASEOLEDB":
                        return EnumDBType.Sybase;

                    default:
                        throw new SQLHelperException("找不到连接名:" + connectionStringName + " 的ProviderName属性");
                }
            }
            throw new SQLHelperException("找不到连接名:" + connectionStringName + " 的配置信息");
        }
    }
}
