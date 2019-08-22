 
// Type: app.bsms.Models.Administration.Menu
 
 
 

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace app.bsms.Models.Administration
{
  public class Menu
  {
    public string siteCode { get; set; }

    [Required(ErrorMessageResourceName = "MenuIDRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "MenuID", ResourceType = typeof (Resources.Resources))]
    public int menuId { get; set; }

    [Required(ErrorMessageResourceName = "MenuNameRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "MenuName", ResourceType = typeof (Resources.Resources))]
    public string menuName { get; set; }

    public int? menuParentId { get; set; }

    [Required(ErrorMessageResourceName = "MenuSeqNoRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "MenuSeqNo", ResourceType = typeof (Resources.Resources))]
    public int menuSeqNo { get; set; }

    [Required(ErrorMessageResourceName = "MenuURLRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "MenuURL", ResourceType = typeof (Resources.Resources))]
    public string menuUrl { get; set; }

    public bool active { get; set; }

    public int? moduleSeqNo { get; set; }

    public int? submenuParentId { get; set; }

    public string menuIcon { get; set; }

    public List<Menu> lstParentMenus { get; set; }

    public List<Menu> lstSubmenus { get; set; }

    public Menu()
    {
      this.lstParentMenus = new List<Menu>();
      this.lstSubmenus = new List<Menu>();
    }
  }
}
