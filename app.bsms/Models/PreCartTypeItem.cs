 
// Type: app.bsms.Models.PreCartTypeItem
 
 
 

using Newtonsoft.Json;

namespace app.bsms.Models
{
  public class PreCartTypeItem
  {
    [JsonProperty("Sno")]
    public int Sno { get; set; }

    [JsonProperty("ItemName")]
    public string ItemName { get; set; }

    [JsonProperty("itemPrice")]
    public double itemPrice { get; set; }

    [JsonProperty("Quantity")]
    public double Quantity { get; set; }
  }
}
