 
// Type: app.bsms.Models.ProductBrandtypeCategory
 
 
 

using Newtonsoft.Json;

namespace app.bsms.Models
{
  public class ProductBrandtypeCategory
  {
    [JsonProperty("rangeCode")]
    public int rangeCode { get; set; }

    [JsonProperty("rangeName")]
    public string rangeName { get; set; }

    [JsonProperty("rangeNamePicURL")]
    public string rangeNamePicURL { get; set; }
  }
}
