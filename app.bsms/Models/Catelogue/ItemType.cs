 
// Type: app.bsms.Models.Catelogue.ItemType
 
 
 

using app.bsms.Models.Sales.Post;
using System.Collections.Generic;

namespace app.bsms.Models.Catelogue
{
  public class ItemType
  {
    public string stockCode { get; set; }

    public string stockName { get; set; }

    public double itemPrice { get; set; }

    public bool openPrepaid { get; set; }

    public int validityDays { get; set; }

    public List<Prepaid> prepaidCondition { get; set; }
  }
}
