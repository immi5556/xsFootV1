 
// Type: app.bsms.Models.Manage.Service.Voucher
 
 
 

using System;

namespace app.bsms.Models.Manage.Service
{
  public class Voucher
  {
    public string transactionNumber { get; set; }

    public int? lineNumber { get; set; }

    public string voucherName { get; set; }

    public string voucherNo { get; set; }

    public string customerCode { get; set; }

    public string customerName { get; set; }

    public DateTime salesDate { get; set; }

    public string siteCode { get; set; }

    public string staffCode { get; set; }

    public Decimal value { get; set; }

    public int? percent { get; set; }

    public DateTime issuedExpiryDate { get; set; }

    public int? onHold { get; set; }

    public int? used { get; set; }
  }
}
