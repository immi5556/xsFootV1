 
// Type: app.bsms.Models.Manage.Service.Redemption
 
 
 

using System;

namespace app.bsms.Models.Manage.Service
{
  public class Redemption
  {
    public string siteCode { get; set; }

    public string customerCode { get; set; }

    public string customerName { get; set; }

    public string phoneNumber { get; set; }

    public string referenceCode { get; set; }

    public DateTime transactionDate { get; set; }

    public string transactionNumber { get; set; }

    public string treatmentDescription { get; set; }

    public string treatmentType { get; set; }

    public DateTime treatmentNextAppt { get; set; }

    public Decimal treatmentAmount { get; set; }

    public string treatmentStatus { get; set; }

    public int treatmentTimes { get; set; }

    public int treatmentLimit { get; set; }

    public string treatmentParentCode { get; set; }

    public string invoiceNumber { get; set; }

    public int sessionLeft { get; set; }
  }
}
