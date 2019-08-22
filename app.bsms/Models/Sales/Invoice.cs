 
// Type: app.bsms.Models.Sales.Invoice
 
 
 

using app.bsms.Models.Manage.Customer;
using System;
using System.Collections.Generic;

namespace app.bsms.Models.Sales
{
  public class Invoice
  {
    public string siteCode { get; set; }

    public string siteHeader { get; set; }

    public string siteAddress { get; set; }

    public string companyRegNo { get; set; }

    public string clientLogo { get; set; }

    public string transactionNumber { get; set; }

    public string invoiceNumber { get; set; }

    public string customerCode { get; set; }

    public string customerName { get; set; }

    public string customerEmail { get; set; }

    public string customerSignature { get; set; }

    public string transactionNumberTitle { get; set; }

    public DateTime salesDate { get; set; }

    public DateTime salesTime { get; set; }

    public string staffName { get; set; }

    public string phoneNumber { get; set; }

    public string previousPts { get; set; }

    public string earnPts { get; set; }

    public string usedPts { get; set; }

    public string totalBalance { get; set; }

    public Decimal totalDiscount { get; set; }

    public string transactionRemarks { get; set; }

    public List<GST> GSTdetails { get; set; }

    public bool isGst { get; set; }

    public Decimal beforeTaxAmount { get; set; }

    public Decimal taxAmount { get; set; }

    public string transactionFooter1 { get; set; }

    public string transactionFooter2 { get; set; }

    public string transactionFooter3 { get; set; }

    public string transactionFooter4 { get; set; }

    public List<Invoice_Details> invoiceDetails { get; set; }

    public IEnumerable<Payment_Details> paymentDetails { get; set; }

    public Register customer { get; set; }
  }
}
