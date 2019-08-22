 
// Type: app.bsms.Models.Reports.DayEnd.Index
 
 
 

using System.Collections.Generic;

namespace app.bsms.Models.Reports.DayEnd
{
  public class Index
  {
    public List<app.bsms.Models.Reports.DayEnd.Sales> Sales { get; set; }

    public List<app.bsms.Models.Reports.DayEnd.Sales> NonSales { get; set; }

    public List<app.bsms.Models.Reports.DayEnd.DepartmentSales> DepartmentSales { get; set; }

    public List<app.bsms.Models.Reports.DayEnd.EmployeePerformance> EmployeePerformance { get; set; }

    public List<app.bsms.Models.Reports.DayEnd.Attendance> Attendance { get; set; }

    public List<app.bsms.Models.Reports.DayEnd.StaffSales> StaffSales { get; set; }
  }
}
