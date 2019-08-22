 
// Type: app.bsms.Models.Account.LoginModel
 
 
 

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace app.bsms.Models.Account
{
  public class LoginModel
  {
    [Display(Name = "Username", ResourceType = typeof (Resources.Resources))]
    [Required(ErrorMessageResourceName = "UsernameRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    public string UserName { get; set; }

    [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [DataType(DataType.Password)]
    [Display(Name = "Password", ResourceType = typeof (Resources.Resources))]
    public string Password { get; set; }

    [Required(ErrorMessageResourceName = "LocaleRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [DataType(DataType.Password)]
    [Display(Name = "Locale", ResourceType = typeof (Resources.Resources))]
    public string ClientCode { get; set; }

    [Display(Name = "RememberMe", ResourceType = typeof (Resources.Resources))]
    public bool RememberMe { get; set; }

    public string SecurityImg { get; set; }

    public string SecurityPhrase { get; set; }

    public string ErrorMessage { get; set; }

    public string Status { get; set; }

    [HiddenInput(DisplayValue = false)]
    public string ReturnUrl { get; set; }

    public string OpenModal { get; set; }
  }
}
