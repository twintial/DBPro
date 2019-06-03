using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBPro.Database;
namespace DBPro.Entity
{
    [DBTable("DBOrder")]
    public class Order
    {
        public Order(string o_id,string u_id,int state,DateTime time)
        {
            orderID = o_id;
            userID = u_id;
            orderState = state;
            createTime = time;
        }
        public Order() { }
        [DBPrimaryKey("orderID")]
        [DBMember("orderID")]
        string orderID { get; set; }
        [DBMember("userID")]
        string userID { get; set; }
        [DBMember("orderState")]
        int orderState { get; set; }
        [DBMember("createTime")]
        DateTime createTime { get; set; }
    }
}
