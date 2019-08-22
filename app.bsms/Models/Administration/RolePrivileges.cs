 
// Type: app.bsms.Models.Administration.RolePrivileges
 
 
 

namespace app.bsms.Models.Administration
{
  public class RolePrivileges
  {
    public string siteCode { get; set; }

    public int roleGroupId { get; set; }

    public int departmentId { get; set; }

    public int rNo { get; set; }

    public int rolePriviledgeNo { get; set; }

    public int roleId { get; set; }

    public int menuId { get; set; }

    public bool visible { get; set; }

    public bool rAdd { get; set; }

    public bool rModify { get; set; }
  }
}
