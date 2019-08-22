 
// Type: app.bsms.Models.Sales.Post.Cart_Details
 
 
 

using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace app.bsms.Models.Sales.Post
{
  public class Cart_Details
  {
    public string siteCode { get; set; }

    public string userID { get; set; }

    public string macAddress { get; set; }

    public string transactionNumber { get; set; }

    public int lineNumber { get; set; }

    [Display(Name = "LineStatus", ResourceType = typeof (Resources.Resources))]
    public string lineStatus { get; set; }

    [Display(Name = "LineType", ResourceType = typeof (Resources.Resources))]
    public string lineType { get; set; }

    [Required(ErrorMessageResourceName = "ItemCodeRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "ItemCode", ResourceType = typeof (Resources.Resources))]
    public string itemCode { get; set; }

    [Required(ErrorMessageResourceName = "ItemNameRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "ItemName", ResourceType = typeof (Resources.Resources))]
    public string itemName { get; set; }

    [Required(ErrorMessageResourceName = "QuantityRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "Quantity", ResourceType = typeof (Resources.Resources))]
    public int itemQty { get; set; }

    public int? totalTreatments { get; set; }

    public List<Course_Details> courseDetails { get; set; }

    [Required(ErrorMessageResourceName = "UnitPriceRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "UnitPrice", ResourceType = typeof (Resources.Resources))]
    public Decimal unitPrice { get; set; }

    [Display(Name = "DiscountPrice", ResourceType = typeof (Resources.Resources))]
    [Range(0.0, 1E+16, ErrorMessageResourceName = "DiscountPriceRange", ErrorMessageResourceType = typeof (Resources.Resources))]
    public Decimal unitDiscount { get; set; }

    [Display(Name = "PromoPrice", ResourceType = typeof (Resources.Resources))]
    [Range(0.0, 1E+16, ErrorMessageResourceName = "PromoPriceRange", ErrorMessageResourceType = typeof (Resources.Resources))]
    public Decimal promoPrice { get; set; }

    [Display(Name = "TotalPrice", ResourceType = typeof (Resources.Resources))]
    [Range(0.0, 1E+16, ErrorMessageResourceName = "TotalPriceRange", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Required(ErrorMessageResourceName = "TotalPriceRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    public Decimal itemAmount { get; set; }

    public Decimal salesAmount { get; set; }

    [Display(Name = "Deposit", ResourceType = typeof (Resources.Resources))]
    [Range(0.0, 1E+16, ErrorMessageResourceName = "DepositRange", ErrorMessageResourceType = typeof (Resources.Resources))]
    public Decimal? depositAmount { get; set; }

    public Decimal gstAmountCollected { get; set; }

    [Display(Name = "StaffCode", ResourceType = typeof (Resources.Resources))]
    [RequiredIf("strStaffs == null", ErrorMessageResourceName = "StaffDetailsRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    public string staffcode { get; set; }

    public string staffName { get; set; }

    [Display(Name = "StaffRatio", ResourceType = typeof (Resources.Resources))]
    [RequiredIf("staffcode != null", ErrorMessageResourceName = "StaffRatioRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    public Decimal? ratio { get; set; }

    public string strStaffs { get; set; }

    public List<SalesStaff> salesStaffDetails { get; set; }

    [Display(Name = "DiscountPercent", ResourceType = typeof (Resources.Resources))]
    [Range(0.0, 99.99, ErrorMessageResourceName = "DiscountPercentRange", ErrorMessageResourceType = typeof (Resources.Resources))]
    public Decimal discountPercent { get; set; }

    [Display(Name = "DiscountDescription", ResourceType = typeof (Resources.Resources))]
    public string discountDescription { get; set; }

    [RequiredIf("strDiscounts != null", ErrorMessageResourceName = "DiscountUserRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "DiscountUser", ResourceType = typeof (Resources.Resources))]
    public string discountUser { get; set; }

    public string strDiscounts { get; set; }

    public List<Discounts> discountDetails { get; set; }

    [Display(Name = "TreatmentCode", ResourceType = typeof (Resources.Resources))]
    public string treatmentCode { get; set; }

    [Display(Name = "UOM", ResourceType = typeof (Resources.Resources))]
    public string itemUOM { get; set; }

    [Display(Name = "FOC", ResourceType = typeof (Resources.Resources))]
    public bool isFOC { get; set; }

    [Display(Name = "FOCReason", ResourceType = typeof (Resources.Resources))]
    [RequiredIf("isFOC == true", ErrorMessageResourceName = "FOCReasonRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    public string FOCReason { get; set; }

    [Display(Name = "FOCQuantity", ResourceType = typeof (Resources.Resources))]
    [RequiredIf("isFOC == true", ErrorMessageResourceName = "FOCQuantityRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [AssertThat("FOCQuantity > 0", ErrorMessageResourceName = "FOCQuantityRange", ErrorMessageResourceType = typeof (Resources.Resources))]
    public int? FOCQuantity { get; set; }

    [Display(Name = "AutoProportionate", ResourceType = typeof (Resources.Resources))]
    public bool autoProportionate { get; set; }

    [Display(Name = "Remarks", ResourceType = typeof (Resources.Resources))]
    public string itemRemarks { get; set; }

    [Display(Name = "FirstTreatmentDone", ResourceType = typeof (Resources.Resources))]
    public bool isFirstTreatmentDone { get; set; }

    [Display(Name = "TreatmentStaff", ResourceType = typeof (Resources.Resources))]
    [RequiredIf("isFirstTreatmentDone == true && strTreatmentStaffs == null", ErrorMessageResourceName = "TreatmentStaffRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    public string firstTreatmentStaffCode { get; set; }

    public app.bsms.Models.Sales.Cart parent { get; set; }

    [Display(Name = "SubItemCode", ResourceType = typeof (Resources.Resources))]
    [RequiredIf("parent.courseType == 'FFi' && isFirstTreatmentDone == true", ErrorMessageResourceName = "SubItemCodeRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    public string subItemCode { get; set; }

    [Display(Name = "TreatmentStaff", ResourceType = typeof (Resources.Resources))]
    [RequiredIf("isFirstTreatmentDone == true", ErrorMessageResourceName = "TreatmentStaffRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    public string strTreatmentStaffs { get; set; }

    public string firstTreatmentStaffName { get; set; }

    public DateTime? expiryDate { get; set; }

    public int? treatmentLimit { get; set; }

    public string treatmentStaffCode { get; set; }

    public string treatmentStaffName { get; set; }

    public string treatmentDoneId { get; set; }

    public string treatmentDonetype { get; set; }

    [Display(Name = "HoldItem", ResourceType = typeof (Resources.Resources))]
    public bool isHoldItem { get; set; }

    [Display(Name = "HoldItemQty", ResourceType = typeof (Resources.Resources))]
    [RequiredIf("isHoldItem == true", ErrorMessageResourceName = "HoldItemQtyRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [AssertThat("holdItemQty > 0 && holdItemQty <= itemQty", ErrorMessageResourceName = "HoldItemQtyRange", ErrorMessageResourceType = typeof (Resources.Resources))]
    public int? holdItemQty { get; set; }

    [Display(Name = "HoldItemType", ResourceType = typeof (Resources.Resources))]
    [RequiredIf("isHoldItem == true", ErrorMessageResourceName = "HoldItemTypeRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    public string holdItemType { get; set; }

    public string comboCode { get; set; }

    [Display(Name = "Remarks", ResourceType = typeof (Resources.Resources))]
    public string remarks { get; set; }

    public string referenceTreatmentCode { get; set; }

    public string referenceTransactionNumber { get; set; }

    public Decimal balanceAmount { get; set; }

    public string topupServiceTreatmentCode { get; set; }

    public string topupProductTreatCode { get; set; }

    public string topupPrepaidTransactionCode { get; set; }

    public string topupPrepaidTypeCode { get; set; }

    public List<Prepaid> prepaidAccountDetails { get; set; }

    [Display(Name = "Prepaid", ResourceType = typeof (Resources.Resources))]
    [Required(ErrorMessageResourceName = "PrepaidRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    public string strPrepaid { get; set; }

    public string cashierLogNumber { get; set; }

    public bool isHoldItemOut { get; set; }

    public Decimal transactionAmount { get; set; }

        public Decimal topupBalance { get; set; }
        public Decimal topupOutstanding { get; set; }

    }
}
