 
// Type: app.bsms.Models.Reports.DayEnd.Sales
 
 
 

using System;

namespace app.bsms.Models.Reports.DayEnd
{
  public class Sales
  {
    public DateTime reportDate { get; set; }

    public string siteCode { get; set; }

    public string itemDescription { get; set; }

    public Decimal amount { get; set; }

    public int quantity { get; set; }
  }
}
