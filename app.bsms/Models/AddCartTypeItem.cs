 
// Type: app.bsms.Models.AddCartTypeItem
 
 
 

using Newtonsoft.Json;

namespace app.bsms.Models
{
  public class AddCartTypeItem
  {
    [JsonProperty("SNO")]
    public string SNO { get; set; }

    [JsonProperty("ITEMNAME")]
    public string ITEMNAME { get; set; }

    [JsonProperty("QUANTITY")]
    public string QUANTITY { get; set; }

    [JsonProperty("DEPOSIT")]
    public string DEPOSIT { get; set; }

    [JsonProperty("DISCOUNT")]
    public string DISCOUNT { get; set; }

    [JsonProperty("UNITPRICE")]
    public string UNITPRICE { get; set; }

    [JsonProperty("TOTALAMOUNT")]
    public string TOTALAMOUNT { get; set; }
  }
}
