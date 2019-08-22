// Type: app.bsms.Models.Sales.Post.Payment_Details

using ExpressiveAnnotations.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace app.bsms.Models.Sales.Post
{
  public class Payment_Details
  {
    public int lineNumber { get; set; }

    public string paymentType { get; set; }

    [Display(Name = "PaymentGroup", ResourceType = typeof (Resources.Resources))]
    [Required(ErrorMessageResourceName = "PaymentGroupRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    public string paymentGroup { get; set; }

    public string paymentDescription { get; set; }

    [Display(Name = "PaymentAmount", ResourceType = typeof (Resources.Resources))]
    [Range(1.0, 1E+16, ErrorMessageResourceName = "PaymentAmountRange", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Required(ErrorMessageResourceName = "PaymentAmountRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    public Decimal paymentAmount { get; set; }

    public Decimal payActualAmount { get; set; }

    public Decimal payChange { get; set; }

    public Decimal transactionItemGST { get; set; }

    public Decimal transactionPayGST { get; set; }

    public string payCurrency { get; set; }

    [Display(Name = "Remarks", ResourceType = typeof (Resources.Resources))]
        //Yoonusblock [RequiredIf("paymentDescription == 'PREPAID' || ContainsIgnoreCase(paymentDescription,'VOUCHER') == true || paymentDescription == 'Credit Note'|| paymentGroup == 'CARD'", ErrorMessageResourceName = "Reason", ErrorMessageResourceType = typeof(Resources.Resources))]
        [RequiredIf("paymentDescription == 'PREPAID' || ContainsIgnoreCase(paymentDescription,'VOUCHER') == true || paymentDescription == 'Credit Note'", ErrorMessageResourceName = "Reason", ErrorMessageResourceType = typeof (Resources.Resources))]
    public string remark1 { get; set; }

    [Display(Name = "Remarks", ResourceType = typeof (Resources.Resources))]
    //Yoonusblock [RequiredIf("paymentGroup == 'CARD'", ErrorMessageResourceName = "Reason", ErrorMessageResourceType = typeof (Resources.Resources))]
    public string remark2 { get; set; }

    [Display(Name = "Remarks", ResourceType = typeof (Resources.Resources))]
        //Yoonusblock [RequiredIf("paymentGroup == 'CARD'", ErrorMessageResourceName = "Reason", ErrorMessageResourceType = typeof (Resources.Resources))]
        public string remark3 { get; set; }

    [Display(Name = "Remarks", ResourceType = typeof (Resources.Resources))]
        //Yoonusblock [RequiredIf("paymentGroup == 'CARD'", ErrorMessageResourceName = "Reason", ErrorMessageResourceType = typeof (Resources.Resources))]
        public string remark4 { get; set; }

    public bool isGST { get; set; }

    public bool isCreditCard { get; set; }

    public bool isOnlinePayment { get; set; }

    public List<PaymentSplit> paySplit { get; set; }

    }
}
