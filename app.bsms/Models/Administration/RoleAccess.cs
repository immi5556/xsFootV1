 
// Type: app.bsms.Models.Administration.RoleAccess
 
 
 

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace app.bsms.Models.Administration
{
  public class RoleAccess
  {
    public string siteCode { get; set; }

    [Required(ErrorMessageResourceName = "RoleIDRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "RoleID", ResourceType = typeof (Resources.Resources))]
    public int roleId { get; set; }

    [Required(ErrorMessageResourceName = "RoleNameRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "RoleName", ResourceType = typeof (Resources.Resources))]
    public string roleName { get; set; }

    [Required(ErrorMessageResourceName = "RoleTypeRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "RoleType", ResourceType = typeof (Resources.Resources))]
    public string roleType { get; set; }

    [Required(ErrorMessageResourceName = "DepartmentIDRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "DepartmentID", ResourceType = typeof (Resources.Resources))]
    public int departmentId { get; set; }

    [Required(ErrorMessageResourceName = "RoleGroupIDRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "RoleGroupID", ResourceType = typeof (Resources.Resources))]
    public int roleGroupId { get; set; }

    public List<RoleGroup> lstRoleGroup { get; set; }

    public List<Department> lstDepartment { get; set; }

    public RoleAccess()
    {
      this.lstRoleGroup = new List<RoleGroup>();
      this.lstDepartment = new List<Department>();
    }
  }
}
