 
// Type: app.bsms.Models.Sales.Cart
 
 
 

using app.bsms.Models.Catelogue;
using app.bsms.Models.General;
using app.bsms.Models.Manage.Customer;
using app.bsms.Models.Manage.Service;
using app.bsms.Models.Sales.Post;
using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace app.bsms.Models.Sales
{
  public class Cart
  {
    public string strTreatmentstaffs { get; set; }

    public string strSalesStaffs { get; set; }

    public string strPayDetails { get; set; }

    public Temp_Cart cart { get; set; }

    public app.bsms.Models.Sales.Post.Cart sales { get; set; }

    public Cart_Details cartDetails { get; set; }

    public app.bsms.Models.Sales.Post.Payment_Details payDetails { get; set; }

    public app.bsms.Models.Sales.Post.Prepaid prepaid { get; set; }

    public List<PackageDetails> lstPackages { get; set; }

    public List<Register> lstCustomer { get; set; }

    public List<Staffs> lstStaff { get; set; }

    public List<PaymentType> paymentTypes { get; set; }

    public List<ReasonCodes> lstDiscountReason { get; set; }

    public List<Staffs> lstDiscountUser { get; set; }

    public List<Discount_Details> lstDiscounts { get; set; }

    public List<app.bsms.Models.General.ListItems> lstFOCs { get; set; }

    public List<Course_Settings> lstCourse_Settings { get; set; }

    public List<Suspend_Cart> lstSuspend_Cart { get; set; }

    public List<app.bsms.Models.General.ListItems> lstLineTypes { get; set; }

    public List<app.bsms.Models.General.ListItems> lstPrepaidTypes { get; set; }

    public List<app.bsms.Models.General.ListItems> lstCondition2 { get; set; }

    public List<Item> lstFlexiDetails { get; set; }

    public List<app.bsms.Models.Manage.Service.Prepaid> lstPrepaid { get; set; }

    public List<Voucher> lstVoucher { get; set; }

    public List<CreditNote> lstCreditnote { get; set; }

    public List<app.bsms.Models.General.ListItems> lstHoldReasons { get; set; }

    public Cart()
    {
      this.cart = new Temp_Cart();
      this.sales = new app.bsms.Models.Sales.Post.Cart();
      this.cartDetails = new Cart_Details();
      this.cartDetails.parent = this;
      this.prepaid = new app.bsms.Models.Sales.Post.Prepaid();
      this.payDetails = new app.bsms.Models.Sales.Post.Payment_Details();
    }

    [Required(ErrorMessageResourceName = "CourseTypeRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "CourseType", ResourceType = typeof (Resources.Resources))]
    public string courseType { get; set; }

    [Required(ErrorMessageResourceName = "TreatmentDateRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "TreatmentDate", ResourceType = typeof (Resources.Resources))]
    public DateTime? treatmentDate { get; set; }

    [RequiredIf("courseType == 'N'", ErrorMessageResourceName = "NoOfTreatmentsRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Range(0, 999, ErrorMessageResourceName = "NoOfTreatmentsRange", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "NoOfTreatments", ResourceType = typeof (Resources.Resources))]
    public int? totalTreatments { get; set; }

    [Display(Name = "AppointmentInterval", ResourceType = typeof (Resources.Resources))]
    [Required(ErrorMessageResourceName = "AppointmentIntervalRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Range(0, 365, ErrorMessageResourceName = "AppointmentIntervalRange", ErrorMessageResourceType = typeof (Resources.Resources))]
    public int interval { get; set; }

    [Display(Name = "ExpiryDate", ResourceType = typeof (Resources.Resources))]
    [RequiredIf("courseType != 'N'", ErrorMessageResourceName = "ExpiryDateRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [AssertThat("expiryDate > treatmentDate", ErrorMessageResourceName = "ExpiryDateRange", ErrorMessageResourceType = typeof (Resources.Resources))]
    public DateTime? expiryDate { get; set; }

    [Display(Name = "TreatmentLimit", ResourceType = typeof (Resources.Resources))]
    [RequiredIf("courseType != 'N'", ErrorMessageResourceName = "TreatmentLimitRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [AssertThat("treatmentLimit <= 999", ErrorMessageResourceName = "TreatmentLimitRange", ErrorMessageResourceType = typeof (Resources.Resources))]
    public int? treatmentLimit { get; set; }

    [Display(Name = "DiscountType", ResourceType = typeof (Resources.Resources))]
    public string discType { get; set; }

    [Display(Name = "DiscountPrice", ResourceType = typeof (Resources.Resources))]
    public Decimal discountPrice { get; set; }

    [Display(Name = "ChangePrice", ResourceType = typeof (Resources.Resources))]
    public bool isAmountChanged { get; set; }
  }
}
