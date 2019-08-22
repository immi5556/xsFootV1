 
// Type: app.bsms.Models.Reports.DayEnd.StaffSales
 
 
 

using System;

namespace app.bsms.Models.Reports.DayEnd
{
  public class StaffSales
  {
    public string staffCode { get; set; }

    public string staffName { get; set; }

    public DateTime timeIn { get; set; }

    public DateTime timeOut { get; set; }

    public Decimal ServiceSales { get; set; }

    public Decimal TDSales { get; set; }
  }
}
