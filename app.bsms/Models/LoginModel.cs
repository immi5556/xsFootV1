 
// Type: app.bsms.Models.LoginModel
 
 
 

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace app.bsms.Models
{
  public class LoginModel
  {
    [Required]
    [Display(Name = "Username")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "SiteCode")]
    public string SiteCode { get; set; }

    [Display(Name = "ClientCode")]
    public string ClientCode { get; set; }

    [Display(Name = "ClientName")]
    public string ClientName { get; set; }

    [Display(Name = "LoginStatus")]
    public string LoginStatus { get; set; }

    [Display(Name = "Remember me?")]
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
