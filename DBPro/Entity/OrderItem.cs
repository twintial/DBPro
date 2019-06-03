using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBPro.Database;
namespace DBPro.Entity
{
    [DBTable("DBOrderItem")]
    public class OrderItem
    {
        public OrderItem(string o_id,string i_id,int amount)
        {
            orderID = o_id;
            itemID = i_id;
            itemAmount = amount;
        }
        public OrderItem() { }
        [DBPrimaryKey("orderID")]
        [DBMember("orderID")]
        string orderID { get; set; }
        [DBPrimaryKey("itemID")]
        [DBMember("itemID")]
        string itemID { get; set; }
        [DBMember("itemAmount")]
        int itemAmount { get; set; }
    }
}
