using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MicroNet.Common;

namespace MicroNet.SQLHelpers
{
    public class SQLServerHelper : ISQLHelper
    {
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        public SQLServerHelper(string _connectionString)
        {
            this.ConnectionString = _connectionString;
        }

        /// <summary>
        /// 事务内执行多条SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteNoQuery(List<string> sql)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        int result = 0;
                        foreach (var item in sql)
                        {
                            SqlCommand cmd = new SqlCommand(item, conn);
                            result = cmd.ExecuteNonQuery();
                        }
                        tran.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public int ExecuteNoQuery(string sql)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                conn.Close();
                return result;
            }
        }

        public int ExecuteNoQuery(string sql, IDbDataParameter[] para)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                var pa = (SqlParameter[])para;
                cmd.Parameters.AddRange(pa);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                conn.Close();
                return result;
            }
        }

        public object ExecuteScalar(string sql)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                object result = cmd.ExecuteScalar();
                conn.Close();
                return result;
            }
        }

        public object ExecuteScalar(string sql, IDbDataParameter[] para)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                var pa = (SqlParameter[])para;
                cmd.Parameters.AddRange(pa);
                conn.Open();
                object result = cmd.ExecuteScalar();
                conn.Close();
                return result;
            }
        }

        public DataTable ExecuteStor(string storeName, IDbDataParameter[] para)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(storeName, conn);

                var pa = (SqlParameter[])para;
                cmd.Parameters.AddRange(pa);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                conn.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
        }

        public List<T> ExecuteStor<T>(string storeName, IDbDataParameter[] para)
        {
            //using (SqlConnection conn = new SqlConnection(ConnectionString))
            //{
            //    DynamicParameters p = new DynamicParameters();
            //    foreach (var item in para)
            //    {
            //        p.Add(item.ParameterName, item.Value);
            //    }
            //    var query = conn.Query<T>(storeName, p, null,
            //                            true,
            //                            null, CommandType.StoredProcedure).ToList();
            //    return query;
            //}

            return new List<T>();
        }

        public void ExecuteStore(string storeName)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(storeName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void ExecuteStore(string storeName, IDbDataParameter[] para)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(storeName, conn);

                var pa = (SqlParameter[])para;
                cmd.Parameters.AddRange(pa);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataTable GetData(string sql)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                conn.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
        }

        public DataTable GetData(string sql, IDbDataParameter[] para)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                var pa = (SqlParameter[])para;
                cmd.Parameters.AddRange(pa);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                conn.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
        }

        public List<T> GetData<T>(string sql)
        {
            //using (SqlConnection conn = new SqlConnection(ConnectionString))
            //{
            //    var query = conn.Query<T>(sql, null, null,
            //                            true,
            //                            null, CommandType.Text).ToList();
            //    return query;
            //}
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                conn.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return DataTableToModelHelper.DataTableToList<T>(ds.Tables[0]);
            }
        }

        public List<T> GetData<T>(string sql, IDbDataParameter[] para)
        {
            //using (SqlConnection conn = new SqlConnection(ConnectionString))
            //{
            //    DynamicParameters p = new DynamicParameters();
            //    foreach (var item in para)
            //    {
            //        p.Add(item.ParameterName, item.Value);
            //    }
            //    var query = conn.Query<T>(sql, para, null,
            //                            true,
            //                            null, CommandType.Text).ToList();
            //    return query;
            //}

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                var pa = (SqlParameter[])para;
                cmd.Parameters.AddRange(pa);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                conn.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return DataTableToModelHelper.DataTableToList<T>(ds.Tables[0]);
            }
        }
    }
}
