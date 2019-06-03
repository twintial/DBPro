using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBPro.Database;
namespace DBPro.Entity
{
    [DBTable("DBUserInteraction")]
    public class UserInteraction
    {
        public UserInteraction(string s_ID,string r_ID,string content,DateTime time)
        {
            senderID = s_ID;
            receiverID = r_ID;
            massageContent = content;
            massageTime = time;
        }
        public UserInteraction() { }
        [DBPrimaryKey("senderID")]
        [DBMember("senderID")]
        string senderID { get; set; }
        [DBPrimaryKey("receiverID")]
        [DBMember("receiverID")]
        string receiverID { get; set; }
        [DBMember("massageContent")]
        string massageContent { get; set; }
        [DBPrimaryKey("massageTime")]
        [DBMember("massageTime")]
        DateTime massageTime { get; set; }
    }
}
