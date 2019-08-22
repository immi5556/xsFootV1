 
// Type: app.bsms.Models.Reports.DayEnd.Attendance
 
 
 

using System;

namespace app.bsms.Models.Reports.DayEnd
{
  public class Attendance
  {
    public DateTime reportDate { get; set; }

    public string siteCode { get; set; }

    public string staffCode { get; set; }

    public int attendanceType { get; set; }

    public DateTime attendanceTime { get; set; }
  }
}
