 
// Type: app.bsms.Models.Reports.DayEnd.EmployeePerformance
 
 
 

using System;

namespace app.bsms.Models.Reports.DayEnd
{
  public class EmployeePerformance
  {
    public DateTime reportDate { get; set; }

    public string siteCode { get; set; }

    public DateTime transactionDate { get; set; }

    public string staffCode { get; set; }

    public string recordDetailType { get; set; }

    public Decimal staffSales { get; set; }
  }
}
