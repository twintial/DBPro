using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBPro.Database;
namespace DBPro.Entity
{
    [DBTable("DBItemTag")]
    public class ItemTag
    {
        public ItemTag(string i_id,string i_tag)
        {
            itemID=i_id;
            itemTag=i_tag;
        }
        public ItemTag() { }
        [DBPrimaryKey("itemID")]
        [DBMember("itemID")]
        string itemID { get; set; }
        [DBPrimaryKey("itemTag")]
        [DBMember("itemTag")]
        string itemTag { get; set; }
    }
}
