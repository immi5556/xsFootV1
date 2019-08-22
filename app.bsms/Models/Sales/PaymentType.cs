 
// Type: app.bsms.Models.Sales.PaymentType
 
 
 

using System;

namespace app.bsms.Models.Sales
{
  public class PaymentType
  {
    public string siteCode { get; set; }

    public string payCode { get; set; }

    public string payGroup { get; set; }

    public string payDescription { get; set; }

    public string paySequence { get; set; }

    public string GTGroup { get; set; }

    public bool isGST { get; set; }

    public bool isCreditCard { get; set; }

    public bool isOnlinePayment { get; set; }

    public Decimal creditCardCharges { get; set; }

    public Decimal onlinePaymentCharges { get; set; }
  }
}
