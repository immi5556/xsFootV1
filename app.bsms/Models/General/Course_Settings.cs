 
// Type: app.bsms.Models.General.Course_Settings
 
 
 

namespace app.bsms.Models.General
{
  public class Course_Settings
  {
    public string courseType { get; set; }

    public string courseTypeCode { get; set; }

    public bool showSpecialTreatments { get; set; }

    public string maxSpecialTreatments { get; set; }

    public bool showDiscount { get; set; }

    public bool showDiscountReason { get; set; }

    public bool showChangePrice { get; set; }

    public bool showAutoProportion { get; set; }

    public bool showExpiry { get; set; }

    public bool showTreatmentLimit { get; set; }
  }
}
