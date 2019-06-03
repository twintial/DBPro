using DBPro.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
namespace DBPro.Entity
{
    [DBTable("DBAccount")]
    public class Account
    {
        public Account(string acc, string ID, string pass, DateTime register, DateTime login, string ip = null)
        {
            this.account = acc;
            this.userID = ID;
            this.password = pass;
            this.registerTime = register;
            this.lastLogin = login;
            this.lastLoginIP = ip;
        }
         public Account() { }

        [DBPrimaryKey("account")]
        [DBMember("account")]
        public string account { get; set; }
        [DBMember("userID")]
        public string userID { get; set; }
        [DBMember("password")]
        public string password { get; set; }
        [DBMember("registerTime")]
        public DateTime registerTime { get; set; }
        [DBMember("lastLogin")]
        public DateTime lastLogin { get; set; }
        [DBMember("lastLoginIP")]
        public string lastLoginIP { get; set; }
    }
}
