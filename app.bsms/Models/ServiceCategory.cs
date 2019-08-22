 
// Type: app.bsms.Models.ServiceCategory
 
 
 

using Newtonsoft.Json;

namespace app.bsms.Models
{
  public class ServiceCategory
  {
    [JsonProperty("departmentCode")]
    public int departmentCode { get; set; }

    [JsonProperty("departmentName")]
    public string departmentName { get; set; }

    [JsonProperty("Price")]
    public double Price { get; set; }

    [JsonProperty("departmentPic")]
    public string departmentPicURL { get; set; }
  }
}
