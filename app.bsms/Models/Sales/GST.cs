 
// Type: app.bsms.Models.Sales.GST
 
 
 

using System;

namespace app.bsms.Models.Sales
{
  public class GST
  {
    public string siteCode { get; set; }

    public string transactionNumber { get; set; }

    public bool isGst { get; set; }

    public Decimal beforeTaxAmount { get; set; }

    public Decimal taxAmount { get; set; }
  }
}
