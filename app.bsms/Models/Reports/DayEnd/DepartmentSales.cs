 
// Type: app.bsms.Models.Reports.DayEnd.DepartmentSales
 
 
 

using System;

namespace app.bsms.Models.Reports.DayEnd
{
  public class DepartmentSales
  {
    public DateTime reportDate { get; set; }

    public string siteCode { get; set; }

    public string reportType { get; set; }

    public string itemDescription { get; set; }

    public Decimal amount { get; set; }

    public int quantity { get; set; }

    public Decimal targetAmount { get; set; }
  }
}
