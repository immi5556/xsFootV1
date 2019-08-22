 
// Type: app.bsms.Models.Sales.Post.Discounts
 
 
 

using System;

namespace app.bsms.Models.Sales.Post
{
  public class Discounts
  {
    public int discountLineNumber { get; set; }

    public string discountReason { get; set; }

    public Decimal discountAmount { get; set; }

    public Decimal discountPercentage { get; set; }

    public bool useDiscountWindow { get; set; }

    public bool? autoDiscount { get; set; }
  }
}
