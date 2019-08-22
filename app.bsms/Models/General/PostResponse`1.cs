 
// Type: app.bsms.Models.General.PostResponse`1
 
 
 

namespace app.bsms.Models.General
{
  public class PostResponse<T>
  {
    public string success { get; set; }

    public T result { get; set; }

    public string error { get; set; }
  }
}
