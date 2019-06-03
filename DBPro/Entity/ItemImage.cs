using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBPro.Database;
namespace DBPro.Entity
{
    [DBTable("DBItemImage")]
    public class ItemImage
    {
        public ItemImage(string image,string i_ID)
        {
            imageID = image;
            itemID = i_ID;
        }
        public ItemImage() { }
        [DBPrimaryKey("imageID")]
        [DBMember("imageID")]
        string imageID { get; set; }
        [DBMember("itemID")]
        string itemID { get; set; }

    }
}
