using DBPro.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBPro.Entity
{
    [DBTable("DBItem")]
    public class Item
    {
        public Item(string i_id, string s_id, string name, double price,
            string intro=null, int fol=0, double sco=0)
        {
            itemID = i_id;
            shopID = s_id;
            itemName = name;
            itemPrice = price;
            itemIntroduction = intro;
            itemFollow = fol;
            itemScore = sco;
        }
        public Item() { }
        [DBPrimaryKey("itemID")]
        [DBMember("itemID")]
        string itemID { get; set; }
        [DBMember("shopID")]
        string shopID { get; set; }
        [DBMember("itemName")]
        string itemName { get; set; }
        [DBMember("itemPrice")]
        double itemPrice { get; set; }
        [DBMember("itemIntroduction")]
        string itemIntroduction { get; set; }
        [DBMember("itemFollow")]
        int itemFollow { get; set; }
        [DBMember("itemScore")]
        double itemScore { get; set; }
    }
}
