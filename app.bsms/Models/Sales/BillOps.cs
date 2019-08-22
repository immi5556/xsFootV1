 
// Type: app.bsms.Models.Sales.BillOps
 
 
 

using System;
using System.Collections.Generic;

namespace app.bsms.Models.Sales
{
  public class BillOps
  {
    public Temp_Cart cart { get; set; }

    public string option { get; set; }

    public string referenceNumber { get; set; }

    public string customerNo { get; set; }

    public string customerName { get; set; }

    public DateTime? fromDate { get; set; }

    public DateTime? toDate { get; set; }

    public List<app.bsms.Models.General.ListItems> lstOptions { get; set; }

    public List<Temp_Cart> lstPendingCarts { get; set; }

    public List<Suspend_Cart> lstSuspendedCarts { get; set; }

    public List<app.bsms.Models.Sales.History.Invoice> lstInvoices { get; set; }
  }
}
