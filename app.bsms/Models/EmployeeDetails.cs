 
// Type: app.bsms.Models.EmployeeDetails
 
 
 

using Newtonsoft.Json;

namespace app.bsms.Models
{
  public class EmployeeDetails
  {
    [JsonProperty("customerCode")]
    public string customerCode { get; set; }

    [JsonProperty("customerName")]
    public string customerName { get; set; }
  }
}
