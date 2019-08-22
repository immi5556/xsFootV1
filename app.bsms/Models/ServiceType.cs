 
// Type: app.bsms.Models.ServiceType
 
 
 

using Newtonsoft.Json;

namespace app.bsms.Models
{
  public class ServiceType
  {
    [JsonProperty("serviceTypeCode")]
    public int serviceTypeCode { get; set; }

    [JsonProperty("serviceTypeName")]
    public string serviceTypeName { get; set; }

    [JsonProperty("serviceTypePic")]
    public string serviceTypePic { get; set; }
  }
}
