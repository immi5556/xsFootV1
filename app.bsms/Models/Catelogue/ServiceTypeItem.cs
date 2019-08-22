 
// Type: app.bsms.Models.Catelogue.ServiceTypeItem
 
 
 

namespace app.bsms.Models.Catelogue
{
  public class ServiceTypeItem
  {
    public string stockCode { get; set; }

    public string stockName { get; set; }

    public int workCommPoints { get; set; }

    public int salesCommPoints { get; set; }

    public double itemPrice { get; set; }

    public string itemType { get; set; }

    public int treatmentLimitCount { get; set; }

    public int treatmentExpiryInMonths { get; set; }

    public bool isFlexi { get; set; }

    public int stockCount { get; set; }

    public double itemSinglePrice { get; set; }

    public string itemSingleCode { get; set; }

    public double itemCoursePrice { get; set; }

    public string itemCourseCode { get; set; }

    public bool itemBoth { get; set; }
  }
}
