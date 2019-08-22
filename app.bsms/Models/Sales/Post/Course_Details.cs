 
// Type: app.bsms.Models.Sales.Post.Course_Details
 
 
 

using System;
using System.Collections.Generic;

namespace app.bsms.Models.Sales.Post
{
  public class Course_Details
  {
    public int treatmentNumber { get; set; }

    public Decimal treatmentUnitPrice { get; set; }

    public string courseType { get; set; }

    public string treatmentStatus { get; set; }

    public DateTime treatmentDate { get; set; }

    public DateTime? nextAppt { get; set; }

    public bool? isFOC { get; set; }

    public string itemCode { get; set; }

    public string itemDescription { get; set; }

    public List<TreatmentStaff> treatmentStaffDetails { get; set; }
  }
}
