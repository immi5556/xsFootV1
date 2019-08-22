 
// Type: app.bsms.Models.General.Discount_Details
 
 
 

using System;

namespace app.bsms.Models.General
{
  public class Discount_Details
  {
    public Decimal discountPercent { get; set; }

    public Decimal unitDiscount { get; set; }

    public string discountCode { get; set; }

    public string discountDescription { get; set; }
  }
}
