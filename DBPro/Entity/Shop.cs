using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBPro.Database;
namespace DBPro.Entity
{
    [DBTable("DBShop")]
    public class Shop
    {
        public Shop(string s_ID,string u_ID,string name,string icon,string intro,
            DateTime apply,DateTime approval,double f_rate,double value,int s_vol,
            int f_vol)
        {
            shopID = s_ID;
            userID = u_ID;
            shopName = name;
            shopIcon = icon;
            shopIntroduction = intro;
            applicationTime = apply;
            approvalTime = approval;
            favorableRate = f_rate;
            salesValue = value;
            salesVolume = s_vol;
            followVolume = f_vol;
        }
        public Shop() { }
        [DBPrimaryKey("shopID")]
        [DBMember("shopId")]
        string shopID { get; set; }
        [DBMember("userID")]
        string userID { get; set; }
        [DBMember("shopName")]
        string shopName { get; set; }
        [DBMember("shopIcon")]
        string shopIcon { get; set; }
        [DBMember("shopIntroduction")]
        string shopIntroduction { get; set; }
        [DBMember("applicationTime")]
        DateTime applicationTime { get; set; }
        [DBMember("approvalTime")]
        DateTime approvalTime { get; set; }
        [DBMember("favorableRate")]
        double favorableRate { get; set; }
        [DBMember("salesValue")]
        double salesValue { get; set; }
        [DBMember("salesVolume")]
        int salesVolume { get; set; }
        [DBMember("followVolume")]
        int followVolume { get; set; }
    }
}
