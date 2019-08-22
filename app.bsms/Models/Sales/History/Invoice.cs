 
// Type: app.bsms.Models.Sales.History.Invoice
 
 
 

using System;

namespace app.bsms.Models.Sales.History
{
  public class Invoice
  {
    public string siteCode { get; set; }

    public string customerCode { get; set; }

    public DateTime transactionDate { get; set; }

    public DateTime transactionTime { get; set; }

    public string transactionNumber { get; set; }

    public string invoiceNumber { get; set; }

    public Decimal transactionAmount { get; set; }

    public Decimal transactionPaid { get; set; }

    public string transctionStaff { get; set; }

    public string transctionStatus { get; set; }

    public string transctionType { get; set; }

        public string customerName { get; set; }
        public bool isVoid { get; set; }

    }
}
