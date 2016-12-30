using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MicroNet.SQLHelpers
{
    public class SQLServerHelper : ISQLHelper
    {
        public string ConnectionString { get; set; }

        public SQLServerHelper(string _connectionString)
        {
            this.ConnectionString = _connectionString;
        }


        public int ExecuteNoQuery(List<string> sql)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNoQuery(string sql)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNoQuery(string sql, IDbDataParameter[] para)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalar(string sql)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalar(string sql, IDbDataParameter[] para)
        {
            throw new NotImplementedException();
        }

        public DataTable ExecuteStor(string storeName, IDbDataParameter[] para)
        {
            throw new NotImplementedException();
        }

        public List<T> ExecuteStor<T>(string storeName, IDbDataParameter[] para)
        {
            throw new NotImplementedException();
        }

        public void ExecuteStore(string storeName)
        {
            throw new NotImplementedException();
        }

        public void ExecuteStore(string storeName, IDbDataParameter[] para)
        {
            throw new NotImplementedException();
        }

        public DataTable GetData(string sql)
        {
            throw new NotImplementedException();
        }

        public DataTable GetData(string sql, IDbDataParameter[] para)
        {
            throw new NotImplementedException();
        }

        public List<T> GetData<T>(string sql)
        {
            throw new NotImplementedException();
        }

        public List<T> GetData<T>(string sql, IDbDataParameter[] para)
        {
            throw new NotImplementedException();
        }
    }
}
