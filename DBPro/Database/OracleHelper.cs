using DBPro.Entity;
using Microsoft.IdentityModel.Protocols;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBPro.Database
{
    
    public class OracleHelper
    {
        //const int IMG_MAX_SIZE = 102400;

        public static void createOraParam(PropertyInfo propertyInfo, Dictionary<string, OracleParameter>lo, object val, List<string> key=null, 
             List<string> primaryKey = null, List<string> allMember = null)
        {
            OracleParameter op = new OracleParameter();
            bool primary = false;
            foreach (Attribute attr in Attribute.GetCustomAttributes(propertyInfo))
            {
                if (attr.GetType() == typeof(DBPrimaryKeyAttribute))
                {
                    primary = true;
                    continue;
                }
                if (attr.GetType() == typeof(DBMemberAttribute))
                {
                    op.DbType = TypeConvert.TypeToDBType(propertyInfo.PropertyType);
                    op.OracleDbType = TypeConvert.DbTypeToOracleDbType(op.DbType);
                    op.ParameterName = ((DBMemberAttribute)attr).key;
                    op.Value = val;
                    lo.Add(op.ParameterName, op);
                    if(primaryKey!=null&&primary)
                    {
                        primaryKey.Add(op.ParameterName);
                    }
                    else if(key!=null)
                    {
                        key.Add(op.ParameterName);
                    }
                    if(allMember!=null)
                    {
                        allMember.Add(op.ParameterName);
                    }
                }
            }
        }
        public static Dictionary<string,OracleParameter> getDBMember(object obj, List<string> key = null,
             List<string> primaryKey = null, List<string> allMember = null)
        {
            Type type = obj.GetType();
            
            PropertyInfo[] propertyInfoList = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance| BindingFlags.Public);
            Dictionary<string, OracleParameter> lo = new Dictionary<string, OracleParameter>();
            foreach (PropertyInfo propertyInfo in propertyInfoList)
            {
                object val = propertyInfo.GetValue(obj);
                OracleHelper.createOraParam(propertyInfo,lo, val, key, primaryKey, allMember);

            }
            return lo;
        }


        #region InsertObject(OracleConnection connection,object obj)
        public static bool InsertObject(OracleConnection connection, object obj)
        {
            StringBuilder strSql = new StringBuilder();

            List<string> all = new List<string>();
            Dictionary<string, OracleParameter> opd = getDBMember(obj, null, null, all);
            List<OracleParameter> op = new List<OracleParameter>();
            try
            {
                strSql.Append(string.Format("insert into {0}", DBAttribute.getDBTable(obj.GetType())));
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("Invaild Argument!\n\n from Insert(OracleConnection connection,object obj) \n");
            }

            strSql.Append(string.Format("( {0} )", string.Join(",", all.ToArray())));
            strSql.Append(string.Format(" values ({0})", ":"+string.Join(",:", all.ToArray())));
            foreach(string str in all)
            {
                op.Add(opd[str]);
            }
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, connection, null, strSql.ToString(), op.ToArray());
            int tem = cmd.ExecuteNonQuery();
            return tem != -1 && tem != 0 ;
        }
        #endregion

        







        #region PrepareCommand
        public static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, string cmdText, OracleParameter[] cmdParms,CommandType type=CommandType.Text)
        {
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = type;//cmdType;
            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        #endregion

        #region Exists(OracleConnection connection, string SQLString)
        public static bool Exists(OracleConnection connection, string SQLString)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand(SQLString, connection))
                {
                    object obj = cmd.ExecuteScalar();
                    return !((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)));
                }
            }
            catch (OracleException e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region int ExecuteSql(OracleConnection connection, string SQLString)

        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(OracleConnection connection, string SQLString, OracleParameter[] op = null, CommandType type=CommandType.Text)
        {
            
            try
            {
                OracleCommand cmd = new OracleCommand();
                PrepareCommand(cmd, connection, null, SQLString, op, type);
                
                int rows = cmd.ExecuteNonQuery();
                return rows;
                
            }
            catch (OracleException E)
            {
                throw new Exception(E.Message);
            }
        }
        #endregion


        #region OracleDataReader ExecuteReader(OracleConnection , string)
        /// <summary>
        /// Query,Return OracleDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">Query</param>
        /// <returns>OracleDataReader</returns>
        public static OracleDataReader ExecuteReader(OracleConnection connection, string strSQL)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand(strSQL, connection))
                {
                    OracleDataReader myReader = cmd.ExecuteReader();
                    return myReader;
                }
            }
            catch (OracleException e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region DataSet Query(OracleConnection , string)
        /// <summary>
        /// Query
        /// </summary>
        /// <param name="SQLString">Query</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(OracleConnection connection, string SQLString)
        {
            DataSet ds = new DataSet();
            try
            {
                using (OracleDataAdapter command = new OracleDataAdapter(SQLString, connection))
                {
                    command.Fill(ds, "ds");
                    return ds;
                }
            }
            catch (OracleException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

 
        #region OracleDataReader ExecuteReader(OracleConnection , string , params OracleParameter[] )
        /// <summary>
        /// 执行查询语句，返回OracleDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>OracleDataReader</returns>
        public static OracleDataReader ExecuteReader(OracleConnection connection, string SQLString, params OracleParameter[] cmdParms)
        {

            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    OracleDataReader myReader = cmd.ExecuteReader();
                    //cmd.Parameters.Clear();
                    return myReader;
                }
            }
            catch (OracleException e)
            {
                throw new Exception(e.Message);
            }

        }
        #endregion

        #region DataSet Query(OracleConnection , string , params OracleParameter[] )
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(OracleConnection connection, string SQLString, params OracleParameter[] cmdParms)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds, "ds");
                        //cmd.Parameters.Clear();
                        return ds;
                    }
                }
            }
            catch (OracleException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region OracleDataReader RunProcedureReader
        /// <summary>
        /// exec stored procedure  ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">stored procedure name</param>
        /// <param name="parameters">stored procedure parameters</param>
        /// <returns>OracleDataReader</returns>
        public static OracleDataReader RunProcedureReader(OracleConnection connection, string storedProcName,params OracleParameter[] parameters)
        {
            OracleDataReader returnReader;
            OracleCommand command = StoredProcReturnCommand(connection, storedProcName, OracleDbType.RefCursor, parameters);
            returnReader = command.ExecuteReader();
            return returnReader;
        }
        #endregion

        #region int RunProcedureInt
        /// <summary>
        /// exec stored procedure
        /// </summary>
        /// <param name="storedProcName">stored procedure name</param>
        /// <param name="parameters">stored procedure parameters</param>
        /// <returns>affected rows num</returns>
        public static int RunProcedureInt(OracleConnection connection, string storedProcName, OracleParameter[] parameters)
        {
            
            Oracle.ManagedDataAccess.Types.OracleDecimal result;
            OracleCommand command = StoredProcReturnCommand(connection, storedProcName,OracleDbType.Int32, parameters);
            command.ExecuteNonQuery();
            result = (Oracle.ManagedDataAccess.Types.OracleDecimal)command.Parameters["ReturnValue"].Value;
            return result.IsNull ? 0:result.ToInt32() ;
        }
        #endregion

        #region void RunProcedure
        /// <summary>
        /// exec stored procedure 
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        public static void RunProcedure(OracleConnection connection, string storedProcName, OracleParameter[] parameters)
        {
            try
            {
                OracleCommand cmd = BuildStoredProcCommand(connection, storedProcName, parameters);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                throw new Exception(ex.Message);
            }

        }

        #endregion
        
        #region DataSet RunProcedureGetDataSet
        /// <summary>
        /// 执行存储过程,返回数据集
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedureGetDataSet(OracleConnection connection, string storedProcName, OracleParameter[] parameters)
        {
            DataSet dataSet = new DataSet();
            OracleCommand Command=StoredProcReturnCommand(connection, storedProcName, OracleDbType.RefCursor, parameters);
            OracleDataAdapter sqlDA = new OracleDataAdapter(Command);
            sqlDA.Fill(dataSet, "dt");
            return dataSet;
        }
        #endregion
            
        #region OracleCommand BuildStoredProcCommand
        /// <summary>
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>OracleCommand</returns>
        public static OracleCommand BuildStoredProcCommand(OracleConnection connection, string storedProcName,params OracleParameter[] parameters)
        {
            OracleCommand command = new OracleCommand();
            PrepareCommand(command, connection, null, storedProcName, parameters,CommandType.StoredProcedure);
            return command;
        }
        #endregion

        #region OracleCommand StoredProcReturnCommand  Used By RunProcedureInt
        /// <summary>
        /// 创建 OracleCommand 对象实例(用来返回一个整数值) 
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>OracleCommand 对象实例</returns>
        private static OracleCommand StoredProcReturnCommand(OracleConnection connection, string storedProcName, OracleDbType type,params OracleParameter[] parameters)
        {
            OracleCommand command = BuildStoredProcCommand(connection, storedProcName, parameters);
            var tem = new OracleParameter("returnvalue", type);
            tem.Direction = ParameterDirection.Output;
            command.Parameters.Add(tem);
            return command;
        }
        #endregion



        #region Update(OracleConnection connection,object obj)
        /*不负责检查原对象是否存在于表中*/
        /// <summary>
        /// 修改
        /// </summary>
        public static bool Update(OracleConnection connection, object obj)
        {
            StringBuilder strSql = new StringBuilder();
            List<string> key=new List<string>(), pri=new List<string>();
            Dictionary<string, OracleParameter> opd =getDBMember(obj, key, pri);
            List<OracleParameter> op = new List<OracleParameter>();

            strSql.Append(string.Format("update {0} set ", DBAttribute.getDBTable(obj.GetType())));
            for (int i = 0; i < key.Count(); ++i)
            {
                strSql.Append(string.Format(" {0} = :{0} ,", key[i]));
                op.Add(opd[key[i]]);
            }
            --strSql.Length;
            strSql.Append(" where ");
            for (int i = 0; i < pri.Count(); ++i)
            {
                strSql.Append(string.Format(" {0} = :{0} AND", pri[i]));
                op.Add(opd[pri[i]]);
            }
            strSql.Length -= 3;

            return ExecuteSql(connection, strSql.ToString(), op.ToArray()) > 0 ;


        }
        #endregion

        #region Delete(OracleConnection connection, object obj)
        /// <summary>
        /// 删除obj对象
        /// </summary>
        public static bool Delete(OracleConnection connection, object obj)
        {
            Type type = obj.GetType();
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append(string.Format("delete from {0}", DBAttribute.getDBTable(type)));
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("Invaild Argument!\n\n from Delete(OracleConnection connection, object obj) \n");
            }
            List<string> propertyPrimaryKeyList = new List<string>();
            List<string> propertyPrimaryValueList = new List<string>();
            DBAttribute.getDBPrimaryElement(type, obj, propertyPrimaryKeyList, propertyPrimaryValueList);
            if (!(propertyPrimaryKeyList.Any() && propertyPrimaryValueList.Any()))
            {
                throw new ArgumentException("Invaild Argument!\n\n from Delete(OracleConnection connection, object obj) \n");
            }
            strSql.Append(string.Format(" where "));
            for(int i=0;i<propertyPrimaryKeyList.Count;++i)
            {
                strSql.Append(string.Format(" {0}={1} AND", propertyPrimaryKeyList[i], propertyPrimaryValueList[i]));
            }
            strSql.Length -= 3;
            return ExecuteSql(connection,strSql.ToString())>0;
        }
        #endregion

        #region Delete(OracleConnection connection, string table,List<string> key,List<string>value)
        /// <summary>
        /// 根据键值对删除(为空则删除全部)
        /// </summary>
        public static void Delete(OracleConnection connection, string table,List<string> key,List<string>value)
        {
            StringBuilder strSql = new StringBuilder() ;
            strSql.Append(string.Format("delete from {0}", table));
            if (value!=null&&value.Any())
            {
                if (key.Count != value.Count)
                    throw new ArgumentException("Invaild Argument! \n\n from Delete(OracleConnection connection, string table,List<string> key,List<string>value) \n");
                strSql.Append(string.Format(" where "));
                for (int i = 0; i < key.Count; ++i)
                {
                    strSql.Append(string.Format(" {0}={1} AND", key[i], value[i]));
                }
            }
            ExecuteSql(connection, strSql.ToString());
        }
        #endregion
    }

}
