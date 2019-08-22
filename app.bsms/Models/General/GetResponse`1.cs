 
// Type: app.bsms.Models.General.GetResponse`1
 
 
 

namespace app.bsms.Models.General
{
  public class GetResponse<T>
  {
    public string success { get; set; }

    public T result { get; set; }

    public string error { get; set; }
  }
}
