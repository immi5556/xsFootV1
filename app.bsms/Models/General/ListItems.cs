 
// Type: app.bsms.Models.General.ListItems
 
 
 

using app.bsms.Helpers;
using Newtonsoft.Json;

namespace app.bsms.Models.General
{
  public class ListItems
  {
    [JsonConverter(typeof (Trimmer))]
    public string itemCode { get; set; }

    [JsonConverter(typeof (Trimmer))]
    public string itemDesc { get; set; }

    [JsonConverter(typeof (Trimmer))]
    public string lineType { get; set; }
  }
}
