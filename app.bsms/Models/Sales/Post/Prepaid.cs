 
// Type: app.bsms.Models.Sales.Post.Prepaid
 
 
 

using ExpressiveAnnotations.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace app.bsms.Models.Sales.Post
{
  public class Prepaid
  {
    public int prepaidLineNumber { get; set; }

    public string prepaidType { get; set; }

    [Display(Name = "PrepaidAmount", ResourceType = typeof (Resources.Resources))]
    [Required(ErrorMessageResourceName = "PrepaidAmountRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    public Decimal? prepaidAmount { get; set; }

    public Decimal prepaidBonus { get; set; }

    public Decimal prepaidTotal { get; set; }

    public Decimal prepaidUsed { get; set; }

    public Decimal prepaidRemain { get; set; }

    [Display(Name = "PrepaidCondition", ResourceType = typeof (Resources.Resources))]
    [Required(ErrorMessageResourceName = "PrepaidConditionRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    public string prepaidCondition1 { get; set; }

    [Display(Name = "PrepaidBrand", ResourceType = typeof (Resources.Resources))]
    [Required(ErrorMessageResourceName = "PrepaidBrandRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    public string prepaidCondition2 { get; set; }

    [Display(Name = "PrepaidItemType", ResourceType = typeof (Resources.Resources))]
    [Required(ErrorMessageResourceName = "PrepaidItemTypeRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    public string prepaidItemType { get; set; }

    [Display(Name = "PrepaidValue", ResourceType = typeof (Resources.Resources))]
    [Required(ErrorMessageResourceName = "PrepaidValueRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    public Decimal? prepaidValue { get; set; }

    [Display(Name = "ValidPeriod", ResourceType = typeof (Resources.Resources))]
    [Required(ErrorMessageResourceName = "ValidPeriodRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [AssertThat("prepaidExpiryDate > Today()", ErrorMessageResourceName = "ValidPeriodRange", ErrorMessageResourceType = typeof (Resources.Resources))]
    public DateTime? prepaidExpiryDate { get; set; }
  }
}
