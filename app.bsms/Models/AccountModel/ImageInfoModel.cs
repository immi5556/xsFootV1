 
// Type: app.bsms.Models.AccountModel.ImageInfoModel
 
 
 

using System.Web;

namespace app.bsms.Models.AccountModel
{
  public class ImageInfoModel
  {
    public HttpPostedFileBase UserImg { get; set; }

    public string stringImg { get; set; }

    public bool isImgAvailable { get; set; }
  }
}
