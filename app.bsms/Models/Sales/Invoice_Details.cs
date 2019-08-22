 
// Type: app.bsms.Models.Sales.Invoice_Details
 
 
 

using System;

namespace app.bsms.Models.Sales
{
  public class Invoice_Details
  {
    public string siteCode { get; set; }

    public string transactionNumber { get; set; }

    public int lineNumber { get; set; }

    public string itemCode { get; set; }

    public string itemDescription { get; set; }

    public string itemUOM { get; set; }

    public int qty { get; set; }

    public Decimal netPrice { get; set; }

    public Decimal paid { get; set; }

    public Decimal balance { get; set; }

    public string totalBalancePts { get; set; }

    public string transactionRemarks { get; set; }
  }
}
