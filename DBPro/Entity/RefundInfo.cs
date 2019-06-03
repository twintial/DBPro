using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBPro.Database;
namespace DBPro.Entity
{
    [DBTable("DBRefundInfo")]
    public class RefundInfo
    {
        public RefundInfo(string o_id,string i_id,DateTime time,string reason)
        {
            orderID = o_id;
            refundItemID = i_id;
            refundTime = time;
            refundReason = reason;
        }
        public RefundInfo() { }
        [DBPrimaryKey("orderID")]
        [DBMember("orderID")]
        string orderID { get; set; }
        [DBPrimaryKey("refundItemID")]
        [DBMember("refundItemID")]
        string refundItemID { get; set; }
        [DBMember("refundTime")]
        DateTime refundTime { get; set; }
        [DBMember("refundReason")]
        string refundReason { get; set; }
    }
}
