 
// Type: app.bsms.Models.Account.LoginData
 
 
 

using System.ComponentModel.DataAnnotations;

namespace app.bsms.Models.Account
{
  public class LoginData
  {
    [Display(Name = "Username")]
    public string username { get; set; }

    [Display(Name = "Site Code")]
    public string siteCode { get; set; }

    [Display(Name = "Locale")]
    public string clientCode { get; set; }

    [Display(Name = "Client Name")]
    public string clientName { get; set; }

    [Display(Name = "Login Status")]
    public string loginStatus { get; set; }
  }
}
