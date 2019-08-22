 
// Type: app.bsms.Models.Administration.Department
 
 
 

using System.ComponentModel.DataAnnotations;

namespace app.bsms.Models.Administration
{
  public class Department
  {
    public string siteCode { get; set; }

    [Required(ErrorMessageResourceName = "DepartmentIDRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "DepartmentID", ResourceType = typeof (Resources.Resources))]
    public int? departmentId { get; set; }

    [Required(ErrorMessageResourceName = "DepartmentNameRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "DepartmentName", ResourceType = typeof (Resources.Resources))]
    public string departmentName { get; set; }

    [Display(Name = "Description", ResourceType = typeof (Resources.Resources))]
    public string departmentDescription { get; set; }
  }
}
