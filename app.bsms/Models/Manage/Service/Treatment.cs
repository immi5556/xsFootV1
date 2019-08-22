 
// Type: app.bsms.Models.Manage.Service.Treatment
 
 
 

using app.bsms.Models.Sales.Post;
using System;
using System.Collections.Generic;

namespace app.bsms.Models.Manage.Service
{
  public class Treatment
  {
    public string siteCode { get; set; }

    public string customerCode { get; set; }

    public string treatmentParentCode { get; set; }

    public string treatmentDescription { get; set; }

    public Decimal treatmentAmount { get; set; }

    public string treatmentStatus { get; set; }

    public int treatmentTimes { get; set; }

    public string treatmentCode { get; set; }

    public string itemCode { get; set; }

    public List<SalesStaff> salesStaffDetails { get; set; }
  }
}
