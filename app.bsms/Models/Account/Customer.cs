 
// Type: app.bsms.Models.Account.Customer
 
 
 

using System;
using System.ComponentModel.DataAnnotations;

namespace app.bsms.Models.Account
{
  public class Customer
  {
    [Display(Name = "JoinStatus", ResourceType = typeof (Resources.Resources))]
    public bool joinStatus { get; set; }

    [Required(ErrorMessageResourceName = "JoinDateRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "JoinDate", ResourceType = typeof (Resources.Resources))]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime? joinedDate { get; set; }

        //Yoonus
        //[Required(ErrorMessageResourceName = "RefCodeRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "RefCode", ResourceType = typeof (Resources.Resources))]
    public string referenceCode { get; set; }

    [Required(ErrorMessageResourceName = "CustClassRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "CustClass", ResourceType = typeof (Resources.Resources))]
    public string customerClass { get; set; }

    [Display(Name = "classcode")]
    public string classCode { get; set; }

    [Required(ErrorMessageResourceName = "CustCodeRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "CustCode", ResourceType = typeof (Resources.Resources))]
    public string customerCode { get; set; }

    [Required(ErrorMessageResourceName = "CustNameRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "CustName", ResourceType = typeof (Resources.Resources))]
    public string customerName { get; set; }

    [Required(ErrorMessageResourceName = "NRICRequired", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "NRIC", ResourceType = typeof (Resources.Resources))]
    public string nric { get; set; }

    [Display(Name = "Mobile", ResourceType = typeof (Resources.Resources))]
    public string mobile { get; set; }

    [Required(ErrorMessageResourceName = "Mobile1Required", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Range(0, 2147483647, ErrorMessageResourceName = "Mobile1Range", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "Mobile1", ResourceType = typeof (Resources.Resources))]
    public string mobile1 { get; set; }

    [Range(0, 2147483647, ErrorMessageResourceName = "Mobile2Range", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "Mobile2", ResourceType = typeof (Resources.Resources))]
    public string mobile2 { get; set; }

    [Display(Name = "DOB", ResourceType = typeof (Resources.Resources))]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime? dateOfBirth { get; set; }

    [Display(Name = "DOB", ResourceType = typeof (Resources.Resources))]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime? dob { get; set; }

    [Display(Name = "AgeGrp", ResourceType = typeof (Resources.Resources))]
    public string ageGroup { get; set; }

    [EmailAddress(ErrorMessageResourceName = "EmailInvalid", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "Email", ResourceType = typeof (Resources.Resources))]
    public string email { get; set; }

    [Display(Name = "PhoneNumber", ResourceType = typeof (Resources.Resources))]
    public string phoneNumber { get; set; }

    [Display(Name = "PhoneOffice", ResourceType = typeof (Resources.Resources))]
    public string phoneOffice { get; set; }

    [Display(Name = "Address1", ResourceType = typeof (Resources.Resources))]
    public string address1 { get; set; }

    [Display(Name = "Address2", ResourceType = typeof (Resources.Resources))]
    public string address2 { get; set; }

    [Display(Name = "Address3", ResourceType = typeof (Resources.Resources))]
    public string address3 { get; set; }

    [Display(Name = "Address4", ResourceType = typeof (Resources.Resources))]
    public string address4 { get; set; }

    [Display(Name = "City", ResourceType = typeof (Resources.Resources))]
    public string city { get; set; }

    [RegularExpression("([0-9][0-9]*)", ErrorMessageResourceName = "ZipRegular", ErrorMessageResourceType = typeof (Resources.Resources))]
    [Display(Name = "Zip", ResourceType = typeof (Resources.Resources))]
    public string zip { get; set; }

    [Display(Name = "Country", ResourceType = typeof (Resources.Resources))]
    public string country { get; set; }

    [Display(Name = "State", ResourceType = typeof (Resources.Resources))]
    public string state { get; set; }

    [Display(Name = "Occupation", ResourceType = typeof (Resources.Resources))]
    public string occupation { get; set; }

    [Display(Name = "Gender", ResourceType = typeof (Resources.Resources))]
    public string gender { get; set; }
    [Display(Name = "GenderCode")]
    public string genderCode { get; set; }

    [Display(Name = "Race", ResourceType = typeof (Resources.Resources))]
    public string race { get; set; }

    [Display(Name = "ConsultantCode", ResourceType = typeof (Resources.Resources))]
    public string consultantCode { get; set; }

    [Display(Name = "Consultant", ResourceType = typeof (Resources.Resources))]
    public string consultant { get; set; }

    [Display(Name = "TherapistCode", ResourceType = typeof (Resources.Resources))]
    public string therapistCode { get; set; }

    [Display(Name = "Therapist", ResourceType = typeof (Resources.Resources))]
    public string therapist { get; set; }

    [Display(Name = "Nationality", ResourceType = typeof (Resources.Resources))]
    public string nationality { get; set; }
    [Display(Name = "NationalityCode")]
    public string nationalityCode { get; set; }

    [Display(Name = "Religion", ResourceType = typeof (Resources.Resources))]
    public string religion { get; set; }

    [Display(Name = "Source", ResourceType = typeof (Resources.Resources))]
    public string source { get; set; }

    [Display(Name = "SecurityImg", ResourceType = typeof (Resources.Resources))]
    public string SecurityImg { get; set; }

    [Display(Name = "SecurityPhrase", ResourceType = typeof (Resources.Resources))]
    public string SecurityPhrase { get; set; }

    [Display(Name = "CustType", ResourceType = typeof (Resources.Resources))]
    public string customerType { get; set; }

    [Display(Name = "ProdGrp", ResourceType = typeof (Resources.Resources))]
    public string productGroup { get; set; }

    [Display(Name = "Grp1", ResourceType = typeof (Resources.Resources))]
    public string group1 { get; set; }

    [Display(Name = "Grp2", ResourceType = typeof (Resources.Resources))]
    public string group2 { get; set; }

    [Display(Name = "Grp3", ResourceType = typeof (Resources.Resources))]
    public string group3 { get; set; }

    [Display(Name = "Language", ResourceType = typeof (Resources.Resources))]
    public string language { get; set; }

    [Display(Name = "Location", ResourceType = typeof (Resources.Resources))]
    public string location { get; set; }

    [Display(Name = "MariStatus", ResourceType = typeof (Resources.Resources))]
    public string maritalStatus { get; set; }

    [Display(Name = "Anniversary", ResourceType = typeof (Resources.Resources))]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime? anniversary { get; set; }

    [Display(Name = "AccCode", ResourceType = typeof (Resources.Resources))]
    public string accountCode { get; set; }

    [Display(Name = "SkinType", ResourceType = typeof (Resources.Resources))]
    public string skinType { get; set; }

    [Display(Name = "SalRange", ResourceType = typeof (Resources.Resources))]
    public string salaryRange { get; set; }

    [Display(Name = "Weight", ResourceType = typeof (Resources.Resources))]
    public string weight { get; set; }

    [Display(Name = "Height", ResourceType = typeof (Resources.Resources))]
    public string height { get; set; }

    [Display(Name = "MemberType", ResourceType = typeof (Resources.Resources))]
    public string memberType { get; set; }

    [Display(Name = "SiteCode", ResourceType = typeof (Resources.Resources))]
    public string siteCode { get; set; }

    [Display(Name = "Remarks", ResourceType = typeof (Resources.Resources))]
    public string remarks { get; set; }

    [Display(Name = "Salutation", ResourceType = typeof (Resources.Resources))]
    public string salutation { get; set; }

    public string createDate { get; set; }

    public string referBy { get; set; }
  }
}
