 
// Type: app.bsms.Models.AccountModel.UserInfoModel
 
 
 

using System;
using System.Web;

namespace app.bsms.Models.AccountModel
{
  public class UserInfoModel
  {
    public string ICNo { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Mobile_No { get; set; }

    public string Address { get; set; }

    public int? Country { get; set; }

    public int? State { get; set; }

    public string Postcode { get; set; }

    public string City { get; set; }

    public DateTime? DOB { get; set; }

    public string Gender { get; set; }

    public HttpPostedFileBase UserImg { get; set; }

    public string stringImg { get; set; }

    public bool isImgAvailable { get; set; }
  }
}
