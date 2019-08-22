 
// Type: app.bsms.Models.ModelPaymentType
 
 
 

using Newtonsoft.Json;

namespace app.bsms.Models
{
  public class ModelPaymentType
  {
    [JsonProperty("SNO")]
    public string SNO { get; set; }

    [JsonProperty("TotalAmount")]
    public string TotalAmount { get; set; }

    [JsonProperty("PaymentType")]
    public string PaymentType { get; set; }

    [JsonProperty("Payment")]
    public string Payment { get; set; }

    [JsonProperty("Balance")]
    public string Balance { get; set; }
  }
}
