 
// Type: app.bsms.Models.Manage.Service.ReverseTreatment
 
 
 

using app.bsms.Models.General;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace app.bsms.Models.Manage.Service
{
  public class ReverseTreatment
  {
    public List<Treatment> treatments { get; set; }

    public List<app.bsms.Models.General.ListItems> types { get; set; }

    public List<TreatmentTransaction> treatment_trans { get; set; }

    public List<ReverseReason> reverseReasons { get; set; }

    public app.bsms.Models.Manage.Post.ReverseTreatment reverse { get; set; }

    [Required(ErrorMessageResourceName = "TypeRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "Type", ResourceType = typeof (Resources.Resources))]
    public string adjustmentType { get; set; }

    [Required(ErrorMessageResourceName = "ReverseItemsRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    public string strReversalItems { get; set; }

    public string reversalNumber { get; set; }

    public string creditNoteNumber { get; set; }

    public ReverseTreatment()
    {
      this.reverse = new app.bsms.Models.Manage.Post.ReverseTreatment();
    }
  }
}
