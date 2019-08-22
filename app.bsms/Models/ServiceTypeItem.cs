 
// Type: app.bsms.Models.ServiceTypeItem
 
 
 

using Newtonsoft.Json;

namespace app.bsms.Models
{
  public class ServiceTypeItem
  {
    [JsonProperty("stockCode")]
    public int stockCode { get; set; }

    [JsonProperty("stockName")]
    public string stockName { get; set; }

    [JsonProperty("itemPrice")]
    public double itemPrice { get; set; }
  }
}
