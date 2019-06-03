using DBPro.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBPro.Entity
{
    [DBTable("DBUserInfo")]
    public class UserInformation
    {
        public UserInformation(string rid,string id, string name = null, string pro = null, string c = null, string dis = null,
            string blo = null, string detail = null, string rec=null)
        {
            ReceiveID = rid;
            userID = id;
            receiptName = name;
            province = pro;
            city = c;
            district = dis;
            street = blo;
            detailAdress = detail;
            receiptPhone = rec;
        }
        public UserInformation() { }

        [DBPrimaryKey("ReceiveID")]
        [DBMember("ReceiveID")]
        string ReceiveID { get; set; }
        [DBMember("userID")]
        string userID { get; set; }
        [DBMember("receiptName")]
        string receiptName { get; set; }
        [DBMember("province")]
        string province { get; set; }
        [DBMember("city")]
        string city { get; set; }
        [DBMember("district")]
        string district { get; set; }
        [DBMember("street")]
        string street { get; set; }
        [DBMember("detailAdress")]
        string detailAdress { get; set; }
        [DBMember("receiptPhone")]
        string receiptPhone { get; set; }
    }
}
