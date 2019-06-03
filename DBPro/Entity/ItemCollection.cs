using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBPro.Database;
namespace DBPro.Entity
{
    [DBTable("DBItemCollection")]
    public class ItemCollection
    {
        public ItemCollection(string u_id,string i_id)
        {
            userID = u_id;
            itemID = i_id;
        }
        public ItemCollection() { }
        [DBPrimaryKey("userID")]
        [DBMember("userID")]
        string userID { get; set; }
        [DBPrimaryKey("itemID")]
        [DBMember("itemID")]
        string itemID { get; set; }
    }
}
