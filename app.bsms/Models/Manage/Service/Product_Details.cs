 
// Type: app.bsms.Models.Manage.Service.Product_Details
 
 
 

using System;

namespace app.bsms.Models.Manage.Service
{
  public class Product_Details
  {
    public string siteCode { get; set; }

    public string customerCode { get; set; }

    public string treatCode { get; set; }

    public DateTime transactionDate { get; set; }

    public string description { get; set; }

    public string type { get; set; }

    public Decimal amount { get; set; }

    public Decimal balance { get; set; }

    public Decimal outstanding { get; set; }
  }
}
