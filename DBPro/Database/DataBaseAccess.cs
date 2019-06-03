using Dapper;
using DBPro.Entity;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBPro.Database
{
    public static class DataBaseAccess
    {
        static DBConnectionPool pool;
        static DataBaseAccess()
        {
            pool = DBConnectionPool.getInstance();
        }
        public static bool insertObj(object obj)
        {
            OracleConnection conn = pool.fetchConnection();
            bool res = OracleHelper.InsertObject(conn, obj);
            pool.releaseConnection(conn);
            return res;
        }
        public static bool updateObj(object obj)
        {
            OracleConnection conn = pool.fetchConnection();
            bool res = OracleHelper.Update(conn,obj);
            pool.releaseConnection(conn);
            return res;
        }
        public static bool deleteObj(object obj)
        {
            OracleConnection conn = pool.fetchConnection();
            bool res = OracleHelper.Delete(conn, obj);
            pool.releaseConnection(conn);
            return res;
        }
        public static bool existObj(object obj)
        {
            List<string> prikey = new List<string>(), prival = new List<string>();
            DBAttribute.getDBPrimaryElement(obj.GetType(), obj, prikey, prival);
            return exist(obj.GetType(), prikey, prival);
        }
        public static bool exist(Type type,List<string> key, List<string> val)
        {
            OracleConnection conn = pool.fetchConnection();
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("select * from {0} where ", DBAttribute.getDBTable(type)));
            int len = key.Count();
            for (int i = 0; i < len; ++i)
            {
                if (i != 0)
                    sb.Append(" AND ");
                sb.Append($"{key[i]}='{val[i]}'");

            }
            bool res = OracleHelper.Exists(conn, sb.ToString());
            pool.releaseConnection(conn);
            return res;
        }
        public static OracleDataReader qerySql(string sqlStr)
        {
            return null;
        }
        public static List<T> testQuery<T>( string sqlStr)
        {
            OracleConnection conn = pool.fetchConnection();
            List<T> lt = SqlMapper.Query<T>(conn, sqlStr).ToList();
            pool.releaseConnection(conn);
            return lt;
        }
        public static int ExecuteSql(string SQLString)
        {
            OracleConnection conn = pool.fetchConnection();
            int res = OracleHelper.ExecuteSql(conn, SQLString);
            pool.releaseConnection(conn);
            return res;
        }

    }
}
