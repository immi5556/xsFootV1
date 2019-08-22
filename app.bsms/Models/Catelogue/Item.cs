 
// Type: app.bsms.Models.Catelogue.Item
 
 
 

using System;

namespace app.bsms.Models.Catelogue
{
  public class Item
  {
    public string itemCode { get; set; }

    public string itemName { get; set; }

    public string itemDescription { get; set; }

    public string subItemCode { get; set; }

    public string subItemDescription { get; set; }

    public string itemType { get; set; }

    public Decimal itemPrice { get; set; }

    public string rangeCode { get; set; }

    public string rangeName { get; set; }

    public string brandCode { get; set; }

    public string brandName { get; set; }

    public bool taxable { get; set; }

    public int workPoints { get; set; }

    public int salesPoints { get; set; }

    public bool openPrepaid { get; set; }

    public int treatmentLimitCount { get; set; }

    public int treatmentExpiryInMonths { get; set; }

    public bool isFlexi { get; set; }
  }
}
