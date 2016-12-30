using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MicroNet.SQLHelpers
{
    /// <summary>
    /// sqlhelper公共帮助接口
    /// </summary>
    public interface ISQLHelper
    {

        string ConnectionString { get; set; }

        /// <summary>
        /// 执行增删改
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int ExecuteNoQuery(string sql);

        /// <summary>
        /// 查询数据返回dateTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataTable GetData(string sql);

        /// <summary>
        /// 查询数据返回dateTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataTable GetData(string sql, IDbDataParameter[] para);

        /// <summary>
        /// 查询数据返回对象
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        List<T> GetData<T>(string sql);

        /// <summary>
        /// 查询数据返回对象
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        List<T> GetData<T>(string sql, IDbDataParameter[] para);

        /// <summary>
        /// 执行sql返回第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        object ExecuteScalar(string sql);

        /// <summary>
        /// 执行sql返回第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        object ExecuteScalar(string sql, IDbDataParameter[] para);

        /// <summary>
        /// 执行多条增删改,并且实现事物 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int ExecuteNoQuery(List<string> sql);

        /// <summary>
        /// 执行增删改 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int ExecuteNoQuery(string sql, IDbDataParameter[] para);

        /// <summary>
        /// 执行存储过程 
        /// </summary>
        /// <param name="storeName"></param>
        /// <returns></returns>
        void ExecuteStore(string storeName, IDbDataParameter[] para);

        /// <summary>
        /// 执行存储过程 
        /// </summary>
        /// <param name="storeName"></param>
        /// <returns></returns>
        void ExecuteStore(string storeName);

        /// <summary>
        /// 执行存储过程 
        /// </summary>
        /// <param name="storeName"></param>
        /// <returns></returns>
        DataTable ExecuteStor(string storeName, IDbDataParameter[] para);

        /// <summary>
        /// 执行存储过程 
        /// </summary>
        /// <param name="storeName"></param>
        /// <returns></returns>
        List<T> ExecuteStor<T>(string storeName, IDbDataParameter[] para);
    }
}
