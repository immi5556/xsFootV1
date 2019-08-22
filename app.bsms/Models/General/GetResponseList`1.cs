 
// Type: app.bsms.Models.General.GetResponseList`1
 
 
 

using System.Collections.Generic;

namespace app.bsms.Models.General
{
  public class GetResponseList<T>
  {
    public string success { get; set; }

    public List<T> result { get; set; }

    public string error { get; set; }
  }
}
