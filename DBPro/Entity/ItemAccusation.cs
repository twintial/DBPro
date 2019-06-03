using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBPro.Database;
namespace DBPro.Entity
{
    [DBTable("DBItemAccusation")]
    public class ItemAccusation
    {
        public ItemAccusation(string u_id,string i_id,DateTime time,string reason,int state)
        {
            userID = u_id;
            itemID = i_id;
            accusationTime = time;
            accusationReason = reason;
            accusationState = state;
        }
        public ItemAccusation() { }
        [DBPrimaryKey("userID")]
        [DBMember("userID")]
        string userID { get; set; }
        [DBPrimaryKey("itemID")]
        [DBMember("itemID")]
        string itemID { get; set; }
        [DBMember("accusationTime")]
        DateTime accusationTime { get; set; }
        [DBMember("accusationReason")]
        string accusationReason { get; set; }
        [DBMember("accusationState")]
        int accusationState { get; set; }
    }
}
