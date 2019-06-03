using DBPro.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DBPro.Entity
{
    [DBTable("DBImage")]
    public class Image
    {
        public const int IMG_MAX_SIZE = 102400;
        public Image(string imageid, byte[]img_)
        {
            imageID = imageid;
            img = img_;
        }

        public Image(string imageid,string path)
        {
            imageID = imageid;
            FileStream fs;
            fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryReader br = new BinaryReader(fs);
            img = br.ReadBytes(IMG_MAX_SIZE);
            fs.Close();
        }

        public Image() { }
        [DBPrimaryKey("imageID")]
        [DBMember("imageID")]
        public string imageID { get; set; }

        [DBMember("img")]
        public byte[]img{ get; set; }

    }

}
