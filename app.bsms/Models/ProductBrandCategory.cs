 
// Type: app.bsms.Models.ProductBrandCategory
 
 
 

using Newtonsoft.Json;

namespace app.bsms.Models
{
  public class ProductBrandCategory
  {
    [JsonProperty("brandCode")]
    public int brandCode { get; set; }

    [JsonProperty("brandName")]
    public string brandName { get; set; }

    [JsonProperty("brandNamePicURL")]
    public string brandNamePicURL { get; set; }
  }
}
