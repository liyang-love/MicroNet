using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MicroNet.Common;

namespace MicroNet.SQLHelpers
{
    public class MySQLHelper : ISQLHelper
    {
        public string ConnectionString { get; set; }

        public MySQLHelper(string _ConnectionString)
        {
            this.ConnectionString = _ConnectionString;
        }

        public int ExecuteNoQuery(List<string> sql)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        int result = 0;
                        foreach (var item in sql)
                        {
                            MySqlCommand cmd = new MySqlCommand(item, conn);
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
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                conn.Close();
                return result;
            }
        }

        public int ExecuteNoQuery(string sql, IDbDataParameter[] para)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                var pa = (MySqlParameter[])para;
                cmd.Parameters.AddRange(pa);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                conn.Close();
                return result;
            }
        }

        public object ExecuteScalar(string sql)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                object result = cmd.ExecuteScalar();
                conn.Close();
                return result;
            }
        }

        public object ExecuteScalar(string sql, IDbDataParameter[] para)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                var pa = (MySqlParameter[])para;
                cmd.Parameters.AddRange(pa);
                conn.Open();
                object result = cmd.ExecuteScalar();
                conn.Close();
                return result;
            }
        }

        public DataTable ExecuteStor(string storeName, IDbDataParameter[] para)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(storeName, conn);

                var pa = (MySqlParameter[])para;
                cmd.Parameters.AddRange(pa);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                conn.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
        }

        public List<T> ExecuteStor<T>(string storeName, IDbDataParameter[] para)
        {
            //using (MySqlConnection conn = new MySqlConnection(ConnectionString))
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
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(storeName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void ExecuteStore(string storeName, IDbDataParameter[] para)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(storeName, conn);

                var pa = (MySqlParameter[])para;
                cmd.Parameters.AddRange(pa);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataTable GetData(string sql)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                MySqlDataAdapter sda = new MySqlDataAdapter(sql, conn);
                conn.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
        }

        public DataTable GetData(string sql, IDbDataParameter[] para)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                var pa = (MySqlParameter[])para;
                cmd.Parameters.AddRange(pa);
                cmd.CommandType = CommandType.Text;
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                conn.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
        }

        public List<T> GetData<T>(string sql)
        {
            //using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            //{
            //    var query = conn.Query<T>(sql, null, null,
            //                            true,
            //                            null, CommandType.Text).ToList();
            //    return query;
            //}
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                MySqlDataAdapter sda = new MySqlDataAdapter(sql, conn);
                conn.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return DataTableToModelHelper.DataTableToList<T>(ds.Tables[0]);
            }
        }

        public List<T> GetData<T>(string sql, IDbDataParameter[] para)
        {
            //using (MySqlConnection conn = new MySqlConnection(ConnectionString))
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

            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                var pa = (MySqlParameter[])para;
                cmd.Parameters.AddRange(pa);
                cmd.CommandType = CommandType.Text;
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                conn.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return DataTableToModelHelper.DataTableToList<T>(ds.Tables[0]);
            }
        }
    }
}
