using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBPro.Database;
namespace DBPro.Entity
{
    [DBTable("DBShopFollow")]
    public class ShopFollow
    {
        public ShopFollow(string u_id,string s_id)
        {
            userID = u_id;
            shopID = s_id;
        }
        public ShopFollow() { }
        [DBPrimaryKey("userID")]
        [DBMember("userID")]
        string userID { get; set; }
        [DBPrimaryKey("shopID")]
        [DBMember("shopID")]
        string shopID { get; set; }
    }
}
