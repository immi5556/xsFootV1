 
// Type: app.bsms.Models.ProductBrandItemtypeCategory
 
 
 

using Newtonsoft.Json;

namespace app.bsms.Models
{
  public class ProductBrandItemtypeCategory
  {
    [JsonProperty("stockCode")]
    public int stockCode { get; set; }

    [JsonProperty("stockName")]
    public string stockName { get; set; }

    [JsonProperty("itemPrice")]
    public string itemPrice { get; set; }
  }
}
