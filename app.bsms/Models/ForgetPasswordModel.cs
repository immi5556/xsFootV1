 
// Type: app.bsms.Models.ForgetPasswordModel
 
 
 

using System.ComponentModel.DataAnnotations;

namespace app.bsms.Models
{
  public class ForgetPasswordModel
  {
    [Display(Name = "Username")]
    public string UserName { get; set; }
  }
}
