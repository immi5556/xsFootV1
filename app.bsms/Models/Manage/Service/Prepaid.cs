 
// Type: app.bsms.Models.Manage.Service.Prepaid
 
 
 

using System;

namespace app.bsms.Models.Manage.Service
{
  public class Prepaid
  {
    public string siteCode { get; set; }

    public string customerName { get; set; }

    public string customerCode { get; set; }

    public Decimal prepaidAmount { get; set; }

    public string prepaidCategory { get; set; }

    public string prepaidCode { get; set; }

    public Decimal availablePrepaidAmount { get; set; }

    public Decimal usedAmount { get; set; }

    public string prepaidType { get; set; }

    public string prepaidItemType { get; set; }

    public string conditionType1 { get; set; }

    public string conditionType2 { get; set; }
  }
}
