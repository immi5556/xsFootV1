 
// Type: app.bsms.Models.Sales.Post.Cart
 
 
 

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace app.bsms.Models.Sales.Post
{
  public class Cart
  {
    [Required(ErrorMessageResourceName = "SiteCodeRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "SiteCode", ResourceType = typeof (Resources.Resources))]
    public string siteCode { get; set; }

    [Display(Name = "Username", ResourceType = typeof (Resources.Resources))]
    [Required(ErrorMessageResourceName = "UsernameRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    public string userID { get; set; }

    public string macAddress { get; set; }

    public string transactionNumber { get; set; }

    public string invoiceNumber { get; set; }

    [Required(ErrorMessageResourceName = "SalesStatusRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "SalesStatus", ResourceType = typeof (Resources.Resources))]
    [StringLength(2)]
    public string salesStatus { get; set; }

    [Required(ErrorMessageResourceName = "SalesTypeRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "SalesType", ResourceType = typeof (Resources.Resources))]
    public string salesType { get; set; }

    [Display(Name = "TotalQuantity", ResourceType = typeof (Resources.Resources))]
    public int totalQuantity { get; set; }

    [Display(Name = "StaffCode", ResourceType = typeof (Resources.Resources))]
    public string staffCode { get; set; }

    [Display(Name = "StaffName", ResourceType = typeof (Resources.Resources))]
    public string staffName { get; set; }

    [Required(ErrorMessageResourceName = "CustCodeRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "CustCode", ResourceType = typeof (Resources.Resources))]
    public string customerCode { get; set; }

    [Required(ErrorMessageResourceName = "CustNameRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "CustName", ResourceType = typeof (Resources.Resources))]
    public string customerName { get; set; }

    [Display(Name = "TotalAmount", ResourceType = typeof (Resources.Resources))]
    public Decimal totalAmount { get; set; }

    [Display(Name = "TotalDiscount", ResourceType = typeof (Resources.Resources))]
    public Decimal totalDiscount { get; set; }

    [Display(Name = "TotalGST", ResourceType = typeof (Resources.Resources))]
    public Decimal totalGST { get; set; }

    [Display(Name = "Deposit", ResourceType = typeof (Resources.Resources))]
    [Range(0.0, 1E+16, ErrorMessageResourceName = "DepositRange", ErrorMessageResourceType = typeof (Resources.Resources))]
    public Decimal depositAmount { get; set; }

    public Decimal transactionAmount { get; set; }

    public Decimal outstandingAmount { get; set; }

    public string transactionRemark { get; set; }

    public string paymentRemark { get; set; }

    public string salesRemark { get; set; }

    public string courseSummary { get; set; }

    public Decimal totalCreditNoteAmount { get; set; }

    public Decimal totalPrepaidAmount { get; set; }

    public int holdItemQty { get; set; }

    public int totalHoldItemQty { get; set; }

    public int numberOfVouchersAvailable { get; set; }

    public bool isHoldItem { get; set; }

    public string cashierLogNumber { get; set; }

    public bool isVoid { get; set; }

    public Decimal amountReturned { get; set; }

    public string doNumber { get; set; }

    public DateTime salesDate { get; set; }

    public DateTime salesTime { get; set; }

    public string voidReferenceNumber { get; set; }

    public List<Cart_Details> transactionDetails { get; set; }

    public List<Payment_Details> payDetails { get; set; }
  }
}
