using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBPro.Database;
namespace DBPro.Entity
{
    [DBTable("DBShopItem")]
    public class ShopItem
    {
        public ShopItem(string id, string item)
        {
            shopID = id;
            shopItem = item;
        }
        public ShopItem() { }

        [DBPrimaryKey("shopID")]
        [DBMember("shopID")]
        string shopID { get; set; }
        [DBPrimaryKey("shopItem")]
        [DBMember("shopItem")]
        string shopItem { get; set; }
    }
}
