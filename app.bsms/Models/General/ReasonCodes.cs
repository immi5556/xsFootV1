 
// Type: app.bsms.Models.General.ReasonCodes
 
 
 

using app.bsms.Helpers;
using Newtonsoft.Json;

namespace app.bsms.Models.General
{
  public class ReasonCodes
  {
    [JsonConverter(typeof (Trimmer))]
    public string reasonCode { get; set; }

    [JsonConverter(typeof (Trimmer))]
    public string reasonDesc { get; set; }
  }
}
