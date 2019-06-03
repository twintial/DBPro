using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBPro.Database;
namespace DBPro.Entity
{
    [DBTable("DBUser")]
    public class User
    {
        public const int privilege = 0;
        public const int normal = 1;

        public User(string id,string name = null, string gender = null, int level =normal, string icon = null)
        {
            userId = id;
            userName = name;
            userGender = gender;
            userLevel = level;
            userIconID = icon;
        }
        public User() { }
        [DBPrimaryKey("userID")]
        [DBMember("userID")]
        string userId { get; set; }
        [DBMember("userName")]
        string userName { get; set; }
        [DBMember("userGender")]
        string userGender { get; set; }
        [DBMember("userLevel")]
        int userLevel { get; set; }
        [DBMember("userIconID")]
        string userIconID { get; set; }
    }

}
