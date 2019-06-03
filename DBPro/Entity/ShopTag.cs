using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBPro.Database;
namespace DBPro.Entity
{
    [DBTable("DBShopTag")]
    public class ShopTag
    {
        public ShopTag(string id,string tag)
        {
            shopID = id;
            shopTag = tag;
        }
        public ShopTag() { }
        [DBPrimaryKey("shopID")]
        [DBMember("shopID")]
        string shopID { get; set; }
        [DBPrimaryKey("shopTag")]
        [DBMember("shopTag")]
        string shopTag { get; set; }
    }
}
