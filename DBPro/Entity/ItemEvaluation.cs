using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBPro.Database;
namespace DBPro.Entity
{
    [DBTable("DBItemevaluation")]
    public class ItemEvaluation
    {
        public ItemEvaluation(string u_id,string i_id,string content,DateTime time)
        {
            userID = u_id;
            itemID = i_id;
            evaluationContent = content;
            evaluationTime = time;
        }
        public ItemEvaluation() { }

        [DBPrimaryKey("userID")]
        [DBMember("userID")]
        string userID { get; set; }
        [DBPrimaryKey("itemID")]
        [DBMember("itemID")]
        string itemID { get; set; }
        [DBMember("evaluationContent")]
        string evaluationContent { get; set; }
        [DBMember("evaluationTime")]
        DateTime evaluationTime { get; set; }
    }
}
