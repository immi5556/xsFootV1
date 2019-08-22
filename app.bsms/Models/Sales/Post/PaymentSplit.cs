
// Type: app.bsms.Models.Sales.Post.PaymentSplit

using System;

namespace app.bsms.Models.Sales.Post
{
    public class PaymentSplit
    {
        public string itemNo { get; set; }
        public int posDaudLineNo { get; set; }
        public decimal usedAmount { get; set; }
        public string conditionType1 { get; set; }
        public string conditionType2 { get; set; }
    }
}
