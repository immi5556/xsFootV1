 
// Type: app.bsms.Models.Manage.Customer.Search
 
 
 

using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace app.bsms.Models.Manage.Customer
{
  public class Search
  {
    [Display(Name = "JoinDate", ResourceType = typeof (Resources.Resources))]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime? joinedDate { get; set; }

    [Display(Name = "RefCode", ResourceType = typeof (Resources.Resources))]
    public string referenceCode { get; set; }

    [Display(Name = "CustClass", ResourceType = typeof (Resources.Resources))]
    public string customerClass { get; set; }

    [Display(Name = "CustCode", ResourceType = typeof (Resources.Resources))]
    public string customerCode { get; set; }

    [Display(Name = "CustName", ResourceType = typeof (Resources.Resources))]
    public string customerName { get; set; }

    [Display(Name = "NRIC", ResourceType = typeof (Resources.Resources))]
    public string nric { get; set; }

    [Display(Name = "Mobile", ResourceType = typeof (Resources.Resources))]
    public string mobile { get; set; }

    [Display(Name = "Mobile1", ResourceType = typeof (Resources.Resources))]
    public string mobile1 { get; set; }

    [Display(Name = "DOB", ResourceType = typeof (Resources.Resources))]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime? dob { get; set; }

    [Display(Name = "PhoneNumber", ResourceType = typeof (Resources.Resources))]
    public string phoneNumber { get; set; }

    [Display(Name = "ConsultantCode", ResourceType = typeof (Resources.Resources))]
    public string consultantCode { get; set; }

    [Display(Name = "Consultant", ResourceType = typeof (Resources.Resources))]
    public string consultant { get; set; }

    [Display(Name = "TherapistCode", ResourceType = typeof (Resources.Resources))]
    public string therapistCode { get; set; }

    [Display(Name = "Therapist", ResourceType = typeof (Resources.Resources))]
    public string therapist { get; set; }

    [Display(Name = "CustType", ResourceType = typeof (Resources.Resources))]
    public string customerType { get; set; }

    [Display(Name = "SiteCode", ResourceType = typeof (Resources.Resources))]
    public string siteCode { get; set; }

    public IPagedList<Register> lstCustomer { get; set; }

    public List<app.bsms.Models.General.ListItems> lstCustomerClass { get; set; }

    public List<app.bsms.Models.General.ListItems> lstTherapist { get; set; }

    public List<app.bsms.Models.General.ListItems> lstConsultant { get; set; }

    public List<app.bsms.Models.General.ListItems> lstCustomerType { get; set; }
  }
}
