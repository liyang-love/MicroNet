using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using MicroNet.Common;

namespace MicroNet.SQLHelpers
{
    public class OracleHelper : ISQLHelper
    {
        public string ConnectionString { get; set; }

        public OracleHelper(string _ConnectionString)
        {
            this.ConnectionString = _ConnectionString;
        }

        public int ExecuteNoQuery(List<string> sql)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        int result = 0;
                        foreach (var item in sql)
                        {
                            OracleCommand cmd = new OracleCommand(item, conn);
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
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                OracleCommand cmd = new OracleCommand(sql, conn);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                conn.Close();
                return result;
            }
        }

        public int ExecuteNoQuery(string sql, IDbDataParameter[] para)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                OracleCommand cmd = new OracleCommand(sql, conn);

                var pa = (OracleParameter[])para;
                cmd.Parameters.AddRange(pa);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                conn.Close();
                return result;
            }
        }

        public object ExecuteScalar(string sql)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                OracleCommand cmd = new OracleCommand(sql, conn);
                conn.Open();
                object result = cmd.ExecuteScalar();
                conn.Close();
                return result;
            }
        }

        public object ExecuteScalar(string sql, IDbDataParameter[] para)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                OracleCommand cmd = new OracleCommand(sql, conn);

                var pa = (OracleParameter[])para;
                cmd.Parameters.AddRange(pa);
                conn.Open();
                object result = cmd.ExecuteScalar();
                conn.Close();
                return result;
            }
        }

        public DataTable ExecuteStor(string storeName, IDbDataParameter[] para)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                OracleCommand cmd = new OracleCommand(storeName, conn);

                var pa = (OracleParameter[])para;
                cmd.Parameters.AddRange(pa);
                cmd.CommandType = CommandType.StoredProcedure;
                OracleDataAdapter sda = new OracleDataAdapter(cmd);
                conn.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
        }

        public List<T> ExecuteStor<T>(string storeName, IDbDataParameter[] para)
        {
            //using (OracleConnection conn = new OracleConnection(ConnectionString))
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
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                OracleCommand cmd = new OracleCommand(storeName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void ExecuteStore(string storeName, IDbDataParameter[] para)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                OracleCommand cmd = new OracleCommand(storeName, conn);

                var pa = (OracleParameter[])para;
                cmd.Parameters.AddRange(pa);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataTable GetData(string sql)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                OracleDataAdapter sda = new OracleDataAdapter(sql, conn);
                conn.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
        }

        public DataTable GetData(string sql, IDbDataParameter[] para)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                OracleCommand cmd = new OracleCommand(sql, conn);

                var pa = (OracleParameter[])para;
                cmd.Parameters.AddRange(pa);
                cmd.CommandType = CommandType.Text;
                OracleDataAdapter sda = new OracleDataAdapter(cmd);
                conn.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
        }

        public List<T> GetData<T>(string sql)
        {
            //using (OracleConnection conn = new OracleConnection(ConnectionString))
            //{
            //    var query = conn.Query<T>(sql, null, null,
            //                            true,
            //                            null, CommandType.Text).ToList();
            //    return query;
            //}
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                OracleDataAdapter sda = new OracleDataAdapter(sql, conn);
                conn.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return DataTableToModelHelper.DataTableToList<T>(ds.Tables[0]);
            }
        }

        public List<T> GetData<T>(string sql, IDbDataParameter[] para)
        {
            //using (OracleConnection conn = new OracleConnection(ConnectionString))
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

            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                OracleCommand cmd = new OracleCommand(sql, conn);

                var pa = (OracleParameter[])para;
                cmd.Parameters.AddRange(pa);
                cmd.CommandType = CommandType.Text;
                OracleDataAdapter sda = new OracleDataAdapter(cmd);
                conn.Open();
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return DataTableToModelHelper.DataTableToList<T>(ds.Tables[0]);
            }
        }

        public object ExecuteStorToOut(string storeName, string outParametersName, IDbDataParameter[] para)
        {
            using (OracleConnection conn = new OracleConnection(ConnectionString))
            {
                OracleCommand cmd = new OracleCommand(storeName, conn);
                var pa = (OracleParameter[])para;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(pa);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return cmd.Parameters[outParametersName].Value;
            }
        }
    }
}
