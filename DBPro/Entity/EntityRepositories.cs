using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DBPro.Database;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DBPro.Entity
{

    public class EntityRepositories
    {

        public static bool addObject(Type type,params object[] param)
        {
            object obj = Activator.CreateInstance(type, param);
            return DataBaseAccess.insertObj(obj);
        }
        public static bool delObject(Type type, params object[] param)
        {
            object obj = Activator.CreateInstance(type, param);
            return DataBaseAccess.deleteObj(obj);
        }
        public static bool updateObject(Type type, params object[] param)
        {
            object obj = Activator.CreateInstance(type, param);
            return DataBaseAccess.updateObj(obj);
        }

        public static bool addObject(object obj)
        {
            return DataBaseAccess.insertObj(obj);
        }
        public static bool delObject(object obj)
        {
            return DataBaseAccess.deleteObj(obj);
        }
        public static bool updateObject(object obj)
        {
            return DataBaseAccess.updateObj(obj);
        }
        public static List<T> getObject<T>(string sql)
        {
            List<T> res=DataBaseAccess.testQuery<T>(sql).ToList();
            return res;
        }
        public static List<T> getAllObject<T>()
        {
            List<string> key = new List<string>();
            DBAttribute.getDBAllKey(typeof(T), key);
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("select {0} from {1}", string.Join(',', key.ToArray()), DBAttribute.getDBTable(typeof(T))));
            List<T> res = DataBaseAccess.testQuery<T>(sb.ToString()).ToList();
            return res;
        }
    }
}
