using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBPro.Database
{
    public class DBAttribute
    {
        // public static Dictionary<Type, OracleDbType> typeMap;
        enum DBAttributesType { Table, Primary, Member }



        public static string getDBTable(Type type)
        {
            foreach (Attribute attr in Attribute.GetCustomAttributes(type))
            {
                if (attr.GetType() == typeof(DBTableAttribute))
                {
                    return ((DBTableAttribute)attr).table;
                }
            }
            throw new ArgumentException("Invaild Argument!\n\n from public static string getTableAttribute(Type type)\n");
        }
        public static void getDBElement(Type type, object obj, List<string> key, List<string> value, List<string> primaryKey, List<string> primaryValue)
        {
            PropertyInfo[] propertyInfoList = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo propertyInfo in propertyInfoList)
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(propertyInfo))
                {
                    if (attr.GetType() == typeof(DBPrimaryKeyAttribute))
                    {
                        object val = propertyInfo.GetValue(obj);
                        if (val != null)
                        {
                            primaryValue.Add("'" + val.ToString() + "'");
                            primaryKey.Add(((DBPrimaryKeyAttribute)attr).key);
                        }
                        break;
                    }
                    if (attr.GetType() == typeof(DBMemberAttribute))
                    {

                        object val = propertyInfo.GetValue(obj);
                        if (val != null)
                        {
                            value.Add("'" + val.ToString() + "'");
                            key.Add(((DBMemberAttribute)attr).key);
                        }

                    }
                }
            }
        }

        public static void getDBElement(Type type, Type target, object obj, List<string> key, List<string> value)
        {
            PropertyInfo[] propertyInfoList = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo propertyInfo in propertyInfoList)
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(propertyInfo))
                {
                    if (attr.GetType() == target)
                    {
                        key.Add(propertyInfo.Name);
                        object val = propertyInfo.GetValue(obj);
                        if (val != null)
                            value.Add("'" + val.ToString() + "'");
                        else
                            value.Add(null);
                        break;
                    }
                }
            }
        }
        public static void getDBElement(Type type, object obj, List<string> key, List<string> value)
        {
            getDBElement(type, typeof(DBMemberAttribute), obj, key, value);
        }
        public static void getDBPrimaryElement(Type type, object obj, List<string> key, List<string> value)
        {
            getDBElement(type, typeof(DBPrimaryKeyAttribute), obj, key, value);

        }
        public static void getDBAllKey(Type type, List<string> key)
        {
            PropertyInfo[] propertyInfoList = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo propertyInfo in propertyInfoList)
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(propertyInfo))
                {

                    if (attr.GetType() == typeof(DBMemberAttribute))
                    {
                        key.Add(((DBMemberAttribute)attr).key);
                        break;
                    }
                }
            }
        }

    }
    public class DBMemberAttribute : Attribute
    {
        public string key;
        public DBMemberAttribute(string t)
        {
            key = t;
        }
    }
    public class DBTableAttribute:Attribute
    {
        public string table;
        public DBTableAttribute(string t)
        {
            table = t;
        }
    }
    public class DBPrimaryKeyAttribute:Attribute
    {
        public string key;
        public DBPrimaryKeyAttribute(string t)
        {
            key = t;
        }
    }

    
}
