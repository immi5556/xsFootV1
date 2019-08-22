 
// Type: app.bsms.Models.CustomerDetails
 
 
 

using Newtonsoft.Json;
using System.Collections.Generic;

namespace app.bsms.Models
{
  public class CustomerDetails
  {
    [JsonProperty("customerCode")]
    public string customerCode { get; set; }

    [JsonProperty("customerName")]
    public string customerName { get; set; }

    public List<AddCartTypeItem> lstCartTypeItem { get; set; }

    public List<ModelPaymentType> lstModelPaymentType { get; set; }
  }
}
