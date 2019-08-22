 
// Type: app.bsms.Models.General.Staffs
 
 
 

using app.bsms.Helpers;
using Newtonsoft.Json;

namespace app.bsms.Models.General
{
  public class Staffs
  {
    [JsonConverter(typeof (Trimmer))]
    public string staffCode { get; set; }

    [JsonConverter(typeof (Trimmer))]
    public string staffName { get; set; }

    [JsonConverter(typeof (Trimmer))]
    public string siteCode { get; set; }

    [JsonConverter(typeof (Trimmer))]
    public string storeName { get; set; }

    [JsonConverter(typeof (Trimmer))]
    public string staffDesignation { get; set; }

    [JsonConverter(typeof (Trimmer))]
    public string staffRole { get; set; }

    [JsonConverter(typeof (Trimmer))]
    public string staffGroup { get; set; }

    [JsonConverter(typeof(Trimmer))]
    public string profilePicture { get; set; }
        
    }
}
