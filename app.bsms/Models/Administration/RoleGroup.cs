 
// Type: app.bsms.Models.Administration.RoleGroup
 
 
 

using System.ComponentModel.DataAnnotations;

namespace app.bsms.Models.Administration
{
  public class RoleGroup
  {
    public string siteCode { get; set; }

    [Required(ErrorMessageResourceName = "RoleGroupIDRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "RoleGroupID", ResourceType = typeof (Resources.Resources))]
    public int? roleGroupId { get; set; }

    [Required(ErrorMessageResourceName = "RoleGroupNameRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "RoleGroupName", ResourceType = typeof (Resources.Resources))]
    public string roleGroupName { get; set; }
  }
}
