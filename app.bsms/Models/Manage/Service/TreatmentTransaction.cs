 
// Type: app.bsms.Models.Manage.Service.TreatmentTransaction
 
 
 

using System;

namespace app.bsms.Models.Manage.Service
{
  public class TreatmentTransaction
  {
    public string siteCode { get; set; }

    public string treatmentParentCode { get; set; }

    public Decimal creditBalance { get; set; }

    public Decimal totalPaid { get; set; }

    public Decimal treatmentAmount { get; set; }

    public Decimal outstanding { get; set; }
  }
}
