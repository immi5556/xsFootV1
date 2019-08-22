 
// Type: app.bsms.Models.Sales.Payment_Details
 
 
 

using System;

namespace app.bsms.Models.Sales
{
  public class Payment_Details
  {
    public string siteCode { get; set; }

    public string transactionNumber { get; set; }

    public string payDescription { get; set; }

    public string payRemarks { get; set; }

    public Decimal payActualAmount { get; set; }
  }
}
