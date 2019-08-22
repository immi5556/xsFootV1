 
// Type: app.bsms.Models.Sales.Post.TreatmentStaff
 
 
 

using System;

namespace app.bsms.Models.Sales.Post
{
  public class TreatmentStaff
  {
    public string staffCode { get; set; }

    public string staffName { get; set; }

    public Decimal shareAmount { get; set; }

    public int workPoint1 { get; set; }

    public int workPoint2 { get; set; }

    public int workPoint3 { get; set; }
  }
}
