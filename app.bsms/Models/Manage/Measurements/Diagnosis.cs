 
// Type: app.bsms.Models.Manage.Measurements.Diagnosis
 
 
 


using System;
using System.Collections.Generic;

namespace app.bsms.Models.Manage.Measurements
{

  public class Diagnosis
  {
    public DateTime dateCaptured { get; set; }

    public string treatmentCode { get; set; }

    public List<app.bsms.Models.Manage.Service.Treatment> treatments { get; set; }
  }

}
