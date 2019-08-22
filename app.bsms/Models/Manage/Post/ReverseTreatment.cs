 
// Type: app.bsms.Models.Manage.Post.ReverseTreatment
 
 
 

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace app.bsms.Models.Manage.Post
{
  public class ReverseTreatment
  {
    public string siteCode { get; set; }

    public string staffCode { get; set; }

    public string customerCode { get; set; }

    public string staffName { get; set; }

    public string customerName { get; set; }

    public string originalTransactionNumber { get; set; }

    public Decimal originalBalanceAmount { get; set; }

    [Required(ErrorMessageResourceName = "AmountRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Range(1.0, 1E+16, ErrorMessageResourceName = "AmountRange", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "Amount", ResourceType = typeof (Resources.Resources))]
    public Decimal adjustedAmount { get; set; }

    public Decimal creditNoteAmount { get; set; }

    [Required(ErrorMessageResourceName = "ReasonRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "Reason", ResourceType = typeof (Resources.Resources))]
    public string reason { get; set; }

    [Display(Name = "Remarks", ResourceType = typeof (Resources.Resources))]
    public string remarks { get; set; }

    public string treatmentParentCode { get; set; }

    public List<ReverseItems> reversalItems { get; set; }
  }
}
