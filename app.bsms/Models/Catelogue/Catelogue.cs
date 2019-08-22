 
// Type: app.bsms.Models.Catelogue.Catelogue
 
 
 

using System.Collections.Generic;

namespace app.bsms.Models.Catelogue
{
  public class Catelogue
  {
    public int departmentCode { get; set; }

    public string departmentName { get; set; }

    public string productName { get; set; }

    public string type { get; set; }

    public string cart_details { get; set; }

    public List<Service> lstServices { get; set; }

        

    public List<Brand> lstBrands { get; set; }

    public List<Brand> lstVouchers { get; set; }

    public List<Brand> lstPrepaids { get; set; }

    public List<ServiceType> lstServiceTypes { get; set; }

    
    public List<Range> lstRanges { get; set; }

    public List<ServiceTypeItem> lstServiceTypeItems { get; set; }

    public List<ItemType> lstItemTypes { get; set; }
  }
}
