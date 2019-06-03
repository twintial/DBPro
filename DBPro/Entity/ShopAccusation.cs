using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBPro.Database;
namespace DBPro.Entity
{
    [DBTable("DBShopAccusation")]
    public class ShopAccusation
    {
        public ShopAccusation(string u_id, string s_id, DateTime time, string reason, int state)
        {
            userID = u_id;
            shopID = s_id;
            accusationTime = time;
            accusationReason = reason;
            accusationState = state;
        }
        public ShopAccusation() { }

        [DBPrimaryKey("userID")]
        [DBMember("userID")]
        string userID { get; set; }
        [DBPrimaryKey("shopID")]
        [DBMember("shopID")]
        string shopID { get; set; }
        [DBMember("accusationTime")]
        DateTime accusationTime { get; set; }
        [DBMember("accusationReason")]
        string accusationReason { get; set; }
        [DBMember("accusationState")]
        int accusationState { get; set; }
    }
}
