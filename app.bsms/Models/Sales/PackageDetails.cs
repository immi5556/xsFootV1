 
// Type: app.bsms.Models.Sales.PackageDetails
 
 
 

using System;

namespace app.bsms.Models.Sales
{
  public class PackageDetails
  {
    public string packageCode { get; set; }

    public string contentItemCode { get; set; }

    public int lineNumber { get; set; }

    public string contentItemName { get; set; }

    public Decimal price { get; set; }

    public int quantity { get; set; }

    public bool limitServiceToFlexiOnly { get; set; }
  }
}
