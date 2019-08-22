 
// Type: app.bsms.Models.Manage.Post.Supplimentary_Member
 
 
 

using System;
using System.ComponentModel.DataAnnotations;

namespace app.bsms.Models.Manage.Post
{
  public class Supplimentary_Member
  {
    public string siteCode { get; set; }

    public string customerCode { get; set; }

    public string customerName { get; set; }

    [Required(ErrorMessageResourceName = "SupMemberJoinDateRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "SupMemberJoinDate", ResourceType = typeof (Resources.Resources))]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime? supplementaryMemberJoinedDate { get; set; }

    [Required(ErrorMessageResourceName = "SupMemberCodeRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "SupMemberCode", ResourceType = typeof (Resources.Resources))]
    public string supplementaryMemberCode { get; set; }

    [Required(ErrorMessageResourceName = "SupMemberNameRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "SupMemberName", ResourceType = typeof (Resources.Resources))]
    public string supplementaryMemberName { get; set; }

    [Display(Name = "Prepaid", ResourceType = typeof (Resources.Resources))]
    public bool prepaid { get; set; }

    [Display(Name = "UsageAll", ResourceType = typeof (Resources.Resources))]
    public bool usageAll { get; set; }
  }
}
