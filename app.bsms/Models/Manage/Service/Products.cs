 
// Type: app.bsms.Models.Manage.Service.Products
 
 
 

using System;
using System.Collections.Generic;

namespace app.bsms.Models.Manage.Service
{
  public class Products
  {
    public string siteCode { get; set; }

    public string customerCode { get; set; }

    public string treatmentCode { get; set; }

    public DateTime transactionDate { get; set; }

    public string invoiceNumber { get; set; }

    public bool isPackage { get; set; }

    public string transactionNumber { get; set; }

    public int lineNumber { get; set; }

    public string itemBarcode { get; set; }

    public string itemCode { get; set; }

    public string itemName { get; set; }

    public string itemDescription { get; set; }

    public Decimal itemPrice { get; set; }

    public int holdItemQty { get; set; }

    public string staffCode { get; set; }

    public Decimal amount { get; set; }

    public Decimal balance { get; set; }

    public Decimal outstanding { get; set; }

    public List<Products> holdItems { get; set; }

    public List<Product_Details> details { get; set; }

    public List<Products> info { get; set; }
  }
}
