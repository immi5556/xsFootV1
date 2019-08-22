 
// Type: app.bsms.Models.Sales.Suspend_Cart
 
 
 

using System;

namespace app.bsms.Models.Sales
{
  public class Suspend_Cart
  {
    public string siteCode { get; set; }

    public string userId { get; set; }

    public int cartId { get; set; }

    public string cartToken { get; set; }

    public DateTime cartItemDate { get; set; }

    public DateTime cartItemTime { get; set; }
  }
}
