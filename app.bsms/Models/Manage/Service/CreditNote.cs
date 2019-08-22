 
// Type: app.bsms.Models.Manage.Service.CreditNote
 
 
 

using System;

namespace app.bsms.Models.Manage.Service
{
  public class CreditNote
  {
    public string siteCode { get; set; }

    public string customerCode { get; set; }

    public string creditNote { get; set; }

    public DateTime transactionDate { get; set; }

    public string invoiceNumber { get; set; }

    public string transactionNumber { get; set; }

    public string treatCode { get; set; }

    public Decimal amount { get; set; }

    public Decimal balance { get; set; }

    public string status { get; set; }
  }
}
