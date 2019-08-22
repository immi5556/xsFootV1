 
// Type: app.bsms.Models.BSMSCategory
 
 
 

using System.Collections.Generic;

namespace app.bsms.Models
{
  public class BSMSCategory
  {
    public List<ServiceCategory> lstServiceCategory { get; set; }

    public List<DepartmentDetailss> lstDepartmentDetailss { get; set; }

    public List<ServiceTypeItem> lstServiceTypeItem { get; set; }

    public List<PreCartTypeItem> lstPreCartTypeItem { get; set; }

    public List<ProductBrandCategory> lstProductBrandCategory { get; set; }

    public List<ProductBrandtypeCategory> lstProductBrandtypeCategory { get; set; }

    public List<ProductBrandItemtypeCategory> lstProductBrandItemtypeCategory { get; set; }

    public List<ServiceType> lstBSMS { get; set; }
  }
}
