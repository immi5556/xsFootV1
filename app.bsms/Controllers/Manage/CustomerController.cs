using app.bsms;
using app.bsms.api;
using app.bsms.Common;
using app.bsms.Controllers;
using app.bsms.Models.Account;
using app.bsms.Models.General;
using app.bsms.Models.Manage.Customer;
using app.bsms.Models.Manage.Post;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace app.bsms.Controllers.Manage
{
	[NoCache]
	public class CustomerController : BaseController
	{
		public CustomerController()
		{
		}

		public ActionResult Create()
		{
			Register register = new Register();
			try
			{
				register.lstSalutation = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstSalutation.AddRange(Service.GetList<ListItems>("salutation"));
				register.lstCustomerClass = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
                register.lstCustomerClass.AddRange(Service.GetList<ListItems>("CustomerClass"));
				register.lstAgeGroup = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstAgeGroup.AddRange(Service.GetList<ListItems>("AgeGroup"));
				register.lstCountry = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstCountry.AddRange(Service.GetList<ListItems>("Country"));
				register.lstState = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstState.AddRange(Service.GetList<ListItems>("State"));
				register.lstTherapist = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstTherapist.AddRange(Service.GetList<ListItems>("Therapist"));
				register.lstRace = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstRace.AddRange(Service.GetList<ListItems>("Race"));
				register.lstSource = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstSource.AddRange(Service.GetList<ListItems>("Source"));
				register.lstOccupation = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstOccupation.AddRange(Service.GetList<ListItems>("Occupation"));
				register.lstConsultant = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstConsultant.AddRange(Service.GetList<ListItems>("Consultant"));
				register.lstNationality = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstNationality.AddRange(Service.GetList<ListItems>("Nationality"));
				register.lstReligion = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstReligion.AddRange(Service.GetList<ListItems>("Religion"));
				register.lstGender = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstGender.AddRange(Service.GetList<ListItems>("Gender"));
				register.lstCustomerType = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstCustomerType.AddRange(Service.GetList<ListItems>("CustomerType"));
				register.lstcustomerGroup1 = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstcustomerGroup1.AddRange(Service.GetList<ListItems>("customerGroup1"));
				register.lstcustomerGroup2 = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstcustomerGroup2.AddRange(Service.GetList<ListItems>("customerGroup2"));
				register.lstcustomerGroup3 = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstcustomerGroup3.AddRange(Service.GetList<ListItems>("customerGroup3"));
				register.lstLanguage = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstLanguage.AddRange(Service.GetList<ListItems>("Language"));
				register.lstLocation = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstLocation.AddRange(Service.GetList<ListItems>("Location"));
				register.lstMaritalStatus = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstMaritalStatus.AddRange(Service.GetList<ListItems>("MaritalStatus"));
				register.lstSkinType = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstSkinType.AddRange(Service.GetList<ListItems>("SkinType"));
				register.lstSalaryRange = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				register.lstSalaryRange.AddRange(Service.GetList<ListItems>("SalaryRange"));
				register.siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode;
				register.joinedDate = new DateTime?(DateTime.Now);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(register);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(app.bsms.Models.Manage.Post.Customer model)
		{
			ActionResult action;
			try
			{
				Register register = new Register();
				if (base.ModelState.IsValid)
				{
					Service.Parameters.Clear();
					if (!Service.Post("customer", JsonConvert.SerializeObject(model)))
					{
						Alerts.body = "Sorry!, Unsuccessfull";
						Alerts.ErrorMessage = "Unable to save customer record";
						base.TempData["Message"] = Alerts.ErrorMessage;
						register = JsonConvert.DeserializeObject<Register>(JsonConvert.SerializeObject(model));
						register.lstSalutation = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstSalutation.AddRange(Service.GetList<ListItems>("salutation"));
						register.lstCustomerClass = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstCustomerClass.AddRange(Service.GetList<ListItems>("CustomerClass"));
						register.lstAgeGroup = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstAgeGroup.AddRange(Service.GetList<ListItems>("AgeGroup"));
						register.lstCountry = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstCountry.AddRange(Service.GetList<ListItems>("Country"));
						register.lstState = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstState.AddRange(Service.GetList<ListItems>("State"));
						register.lstTherapist = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstTherapist.AddRange(Service.GetList<ListItems>("Therapist"));
						register.lstRace = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstRace.AddRange(Service.GetList<ListItems>("Race"));
						register.lstSource = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstSource.AddRange(Service.GetList<ListItems>("Source"));
						register.lstOccupation = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstOccupation.AddRange(Service.GetList<ListItems>("Occupation"));
						register.lstConsultant = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstConsultant.AddRange(Service.GetList<ListItems>("Consultant"));
						register.lstNationality = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstNationality.AddRange(Service.GetList<ListItems>("Nationality"));
						register.lstReligion = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstReligion.AddRange(Service.GetList<ListItems>("Religion"));
						register.lstGender = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstGender.AddRange(Service.GetList<ListItems>("Gender"));
						register.lstCustomerType = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstCustomerType.AddRange(Service.GetList<ListItems>("CustomerType"));
						register.lstcustomerGroup1 = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstcustomerGroup1.AddRange(Service.GetList<ListItems>("customerGroup1"));
						register.lstcustomerGroup2 = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstcustomerGroup2.AddRange(Service.GetList<ListItems>("customerGroup2"));
						register.lstcustomerGroup3 = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstcustomerGroup3.AddRange(Service.GetList<ListItems>("customerGroup3"));
						register.lstLanguage = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstLanguage.AddRange(Service.GetList<ListItems>("Language"));
						register.lstLocation = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstLocation.AddRange(Service.GetList<ListItems>("Location"));
						register.lstMaritalStatus = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstMaritalStatus.AddRange(Service.GetList<ListItems>("MaritalStatus"));
						register.lstSkinType = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstSkinType.AddRange(Service.GetList<ListItems>("SkinType"));
						register.lstSalaryRange = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstSalaryRange.AddRange(Service.GetList<ListItems>("SalaryRange"));
					}
					else
					{
						if (!string.IsNullOrEmpty(model.supplementaryMemberCode) && !string.IsNullOrEmpty(model.supplementaryMemberName) && model.supplementaryMemberJoinedDate.HasValue)
						{
							Service.Parameters.Clear();
							Supplimentary_Member supplimentaryMember = new Supplimentary_Member()
							{
								siteCode = model.siteCode,
								customerCode = model.customerCode,
								customerName = model.customerName,
								supplementaryMemberCode = model.supplementaryMemberCode,
								supplementaryMemberName = model.supplementaryMemberName,
								supplementaryMemberJoinedDate = model.supplementaryMemberJoinedDate,
								prepaid = model.prepaid,
								usageAll = model.usageAll
							};
							Service.Post("CustomerSupplementaryMember", JsonConvert.SerializeObject(supplimentaryMember));
						}
						Alerts.body = "Success!";
						Alerts.Success = "Customer Saved Successfully";
						base.TempData["Message"] = Alerts.Success;
						action = base.RedirectToAction("Edit", "Customer", new { referenceCode = model.referenceCode, customerCode = model.customerCode, customerName = model.customerName, nric = model.nric, siteCode = model.siteCode });
						return action;
					}
				}
				action = base.View(register);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return action;
		}

		public ActionResult Edit(string referenceCode, string customerCode, string customerName, string nric, string siteCode)
		{
			ActionResult action;
			Register register = new Register();

            if (customerCode.Length < 8 && customerCode == referenceCode)
            {
                customerCode = "";
            }

            try
			{
				Service.Parameters.Clear();
				Service.Parameters.Add("referenceCode", ((string.IsNullOrEmpty(referenceCode) ? string.Empty : referenceCode)).Trim());
				Service.Parameters.Add("customerCode", ((string.IsNullOrEmpty(customerCode) ? string.Empty : customerCode)).Trim());
				Service.Parameters.Add("customerName", ((string.IsNullOrEmpty(customerName) ? string.Empty : customerName)).Trim());
				Service.Parameters.Add("nric", ((string.IsNullOrEmpty(nric) ? string.Empty : nric)).Trim());
				Service.Parameters.Add("siteCode", ((string.IsNullOrEmpty(siteCode) ? string.Empty : siteCode)).Trim());
				Service.Parameters.Add("page", "General");
				register = Service.Get<Register>("customer");
				if (register == null)
				{
					action = base.RedirectToAction("NotFound", "Error");
				}
				else
				{
					if (referenceCode == null && customerCode != null)
					{
						register.referenceCode = customerCode;
						register.customerCode = customerCode;
					}
					Service.Parameters.Clear();
					Service.Parameters.Add("siteCode", ((string.IsNullOrEmpty(siteCode) ? string.Empty : siteCode)).Trim());
					Service.Parameters.Add("customerCode", ((string.IsNullOrEmpty(customerCode) ? string.Empty : customerCode)).Trim());
					register.Supplimentary_Members = Service.GetList<Supplimentary_Member>("CustomerSupplementaryMember");
					register.lstSalutation = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstSalutation.AddRange(Service.GetList<ListItems>("salutation"));
					register.lstCustomerClass = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstCustomerClass.AddRange(Service.GetList<ListItems>("CustomerClass"));
					register.lstAgeGroup = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstAgeGroup.AddRange(Service.GetList<ListItems>("AgeGroup"));
					register.lstCountry = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstCountry.AddRange(Service.GetList<ListItems>("Country"));
					register.lstState = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstState.AddRange(Service.GetList<ListItems>("State"));
					register.lstTherapist = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstTherapist.AddRange(Service.GetList<ListItems>("Therapist"));
					register.lstRace = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstRace.AddRange(Service.GetList<ListItems>("Race"));
					register.lstSource = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstSource.AddRange(Service.GetList<ListItems>("Source"));
					register.lstOccupation = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstOccupation.AddRange(Service.GetList<ListItems>("Occupation"));
					register.lstConsultant = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstConsultant.AddRange(Service.GetList<ListItems>("Consultant"));
					register.lstNationality = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstNationality.AddRange(Service.GetList<ListItems>("Nationality"));
					register.lstReligion = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstReligion.AddRange(Service.GetList<ListItems>("Religion"));
					register.lstGender = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstGender.AddRange(Service.GetList<ListItems>("Gender"));
					register.lstCustomerType = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstCustomerType.AddRange(Service.GetList<ListItems>("CustomerType"));
					register.lstcustomerGroup1 = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstcustomerGroup1.AddRange(Service.GetList<ListItems>("customerGroup1"));
					register.lstcustomerGroup2 = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstcustomerGroup2.AddRange(Service.GetList<ListItems>("customerGroup2"));
					register.lstcustomerGroup3 = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstcustomerGroup3.AddRange(Service.GetList<ListItems>("customerGroup3"));
					register.lstLanguage = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstLanguage.AddRange(Service.GetList<ListItems>("Language"));
					register.lstLocation = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstLocation.AddRange(Service.GetList<ListItems>("Location"));
					register.lstMaritalStatus = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstMaritalStatus.AddRange(Service.GetList<ListItems>("MaritalStatus"));
					register.lstSkinType = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstSkinType.AddRange(Service.GetList<ListItems>("SkinType"));
					register.lstSalaryRange = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					register.lstSalaryRange.AddRange(Service.GetList<ListItems>("SalaryRange"));
					return base.View(register);
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return action;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(app.bsms.Models.Manage.Post.Customer model)
		{
			ActionResult action;
			try
			{
				Register register = new Register();
				if (base.ModelState.IsValid)
				{
					Service.Parameters.Clear();
					if (!Service.Put("customer", JsonConvert.SerializeObject(model)))
					{
						Alerts.body = "Sorry!, Unsuccessfull";
						Alerts.ErrorMessage = "Unable to save customer record";
						base.TempData["Message"] = Alerts.ErrorMessage;
						register = JsonConvert.DeserializeObject<Register>(JsonConvert.SerializeObject(model));
						register.lstSalutation = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstSalutation.AddRange(Service.GetList<ListItems>("salutation"));
						register.lstCustomerClass = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstCustomerClass.AddRange(Service.GetList<ListItems>("CustomerClass"));
						register.lstAgeGroup = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstAgeGroup.AddRange(Service.GetList<ListItems>("AgeGroup"));
						register.lstCountry = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstCountry.AddRange(Service.GetList<ListItems>("Country"));
						register.lstState = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstState.AddRange(Service.GetList<ListItems>("State"));
						register.lstTherapist = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstTherapist.AddRange(Service.GetList<ListItems>("Therapist"));
						register.lstRace = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstRace.AddRange(Service.GetList<ListItems>("Race"));
						register.lstSource = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstSource.AddRange(Service.GetList<ListItems>("Source"));
						register.lstOccupation = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstOccupation.AddRange(Service.GetList<ListItems>("Occupation"));
						register.lstConsultant = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstConsultant.AddRange(Service.GetList<ListItems>("Consultant"));
						register.lstNationality = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstNationality.AddRange(Service.GetList<ListItems>("Nationality"));
						register.lstReligion = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstReligion.AddRange(Service.GetList<ListItems>("Religion"));
						register.lstGender = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstGender.AddRange(Service.GetList<ListItems>("Gender"));
						register.lstCustomerType = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstCustomerType.AddRange(Service.GetList<ListItems>("CustomerType"));
						register.lstcustomerGroup1 = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstcustomerGroup1.AddRange(Service.GetList<ListItems>("customerGroup1"));
						register.lstcustomerGroup2 = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstcustomerGroup2.AddRange(Service.GetList<ListItems>("customerGroup2"));
						register.lstcustomerGroup3 = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstcustomerGroup3.AddRange(Service.GetList<ListItems>("customerGroup3"));
						register.lstLanguage = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstLanguage.AddRange(Service.GetList<ListItems>("Language"));
						register.lstLocation = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstLocation.AddRange(Service.GetList<ListItems>("Location"));
						register.lstMaritalStatus = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstMaritalStatus.AddRange(Service.GetList<ListItems>("MaritalStatus"));
						register.lstSkinType = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstSkinType.AddRange(Service.GetList<ListItems>("SkinType"));
						register.lstSalaryRange = new List<ListItems>()
						{
							new ListItems()
							{
								itemCode = string.Empty,
								itemDesc = string.Empty
							}
						};
						register.lstSalaryRange.AddRange(Service.GetList<ListItems>("SalaryRange"));
					}
					else
					{
						Service.Parameters.Clear();
						Service.Parameters.Add("siteCode", model.siteCode);
						Service.Parameters.Add("customerCode", model.customerCode);
						Supplimentary_Member supplimentaryMember = Service.Get<Supplimentary_Member>("CustomerSupplementaryMember");
						if (supplimentaryMember != null)
						{
							supplimentaryMember.siteCode = model.siteCode;
							supplimentaryMember.customerCode = model.customerCode;
							supplimentaryMember.customerName = model.customerName;
							supplimentaryMember.supplementaryMemberCode = (string.IsNullOrEmpty(model.supplementaryMemberCode) ? supplimentaryMember.supplementaryMemberCode : model.supplementaryMemberCode);
							supplimentaryMember.supplementaryMemberName = (string.IsNullOrEmpty(model.supplementaryMemberCode) ? supplimentaryMember.supplementaryMemberName : model.supplementaryMemberName);
							supplimentaryMember.supplementaryMemberJoinedDate = (model.supplementaryMemberJoinedDate.HasValue ? model.supplementaryMemberJoinedDate : supplimentaryMember.supplementaryMemberJoinedDate);
							supplimentaryMember.prepaid = model.prepaid;
							supplimentaryMember.usageAll = model.usageAll;
							Service.Put("CustomerSupplementaryMember", JsonConvert.SerializeObject(supplimentaryMember));
						}
						else
						{
							supplimentaryMember = new Supplimentary_Member();
							if (!string.IsNullOrEmpty(model.supplementaryMemberCode) && !string.IsNullOrEmpty(model.supplementaryMemberName) && model.supplementaryMemberJoinedDate.HasValue)
							{
								supplimentaryMember.siteCode = model.siteCode;
								supplimentaryMember.customerCode = model.customerCode;
								supplimentaryMember.customerName = model.customerName;
								supplimentaryMember.supplementaryMemberCode = model.supplementaryMemberCode;
								supplimentaryMember.supplementaryMemberName = model.supplementaryMemberName;
								supplimentaryMember.supplementaryMemberJoinedDate = model.supplementaryMemberJoinedDate;
								supplimentaryMember.prepaid = model.prepaid;
								supplimentaryMember.usageAll = model.usageAll;
								Service.Post("CustomerSupplementaryMember", JsonConvert.SerializeObject(supplimentaryMember));
							}
						}
						Alerts.body = "Success!";
						Alerts.Success = "Customer Saved Successfully";
						base.TempData["Message"] = Alerts.Success;
						action = base.RedirectToAction("Edit", "Customer", new { referenceCode = model.referenceCode, customerCode = model.customerCode, customerName = model.customerName, nric = model.nric, siteCode = model.siteCode });
						return action;
					}
				}
				action = base.View(register);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return action;
		}

		public ActionResult GetCustomer(string code, string type)
		{
			Register register = new Register();
			try
			{
				Service.Parameters.Clear();
				if (type != "reference")
				{
					Service.Parameters.Add("referenceCode", string.Empty);
				}
				else
				{
					Service.Parameters.Add("referenceCode", code);
				}
				if (type != "customer")
				{
					Service.Parameters.Add("customerCode", string.Empty);
				}
				else
				{
					Service.Parameters.Add("customerCode", code);
				}
				Service.Parameters.Add("customerName", string.Empty);
				Service.Parameters.Add("nric", string.Empty);
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				Service.Parameters.Add("page", "General");
				register = Service.Get<Register>("customer");
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return this.PartialView("_ViewCustomer", register);
		}

		public ActionResult MGM(string id)
		{
			List<Hierarchy> hierarchies = new List<Hierarchy>();
			try
			{
				((dynamic)base.ViewBag).customerCode = id;
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				Service.Parameters.Add("customerCode", id);
				List<Hierarchy> list = Service.GetList<Hierarchy>("getMGMHierarchy");
				Register customer = Utility.GetCustomer((
					from f in list
					orderby f.level
					select f).FirstOrDefault<Hierarchy>().referredBy, ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				Hierarchy hierarchy = new Hierarchy()
				{
					level = "level0",
					referredBy = string.Empty,
					customerName = customer.customerName,
					customerCode = customer.customerCode,
					customerCardNumber = string.Empty,
					customerJoinedDate = customer.joinedDate.Value,
					customerPoints = 0,
					children = (
						from f in list
						where f.referredBy == customer.customerCode
						select f).ToList<Hierarchy>()
				};
				foreach (Hierarchy child in hierarchy.children)
				{
					child.children = (
						from f in list
						where f.referredBy == child.customerCode
						select f).ToList<Hierarchy>();
					foreach (Hierarchy child1 in child.children)
					{
						child1.children = (
							from f in list
							where f.referredBy == child1.customerCode
							select f).ToList<Hierarchy>();
						foreach (Hierarchy list1 in child1.children)
						{
							list1.children = (
								from f in list
								where f.referredBy == list1.customerCode
								select f).ToList<Hierarchy>();
						}
					}
				}
				hierarchies.Add(hierarchy);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(hierarchies);
		}

        public ActionResult Profile(string id)
        {
            Register register = new Register();
            try
            {
                ((dynamic)base.ViewBag).customerCode = id;
                Service.Parameters.Clear();
                Service.Parameters.Add("customerCode", id);
                Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
                Service.Parameters.Add("page", "General");
                register = Service.Get<Register>("customer");
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return base.View(register);
        }

        public ActionResult Search(int? page, int? size)
		{
			Search search = new Search();
			try
			{
				Service.Parameters.Clear();
				search.lstCustomerClass = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				search.lstCustomerClass.AddRange(Service.GetList<ListItems>("CustomerClass"));
				search.lstCustomerType = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				search.lstCustomerType.AddRange(Service.GetList<ListItems>("CustomerType"));
				search.lstConsultant = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				search.lstConsultant.AddRange(Service.GetList<ListItems>("Consultant"));
				search.lstTherapist = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = string.Empty,
						itemDesc = string.Empty
					}
				};
				search.lstTherapist.AddRange(Service.GetList<ListItems>("Therapist"));
				search.siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode;
				List<Register> registers = Service.PostRead<Register>("SearchCustomer", JsonConvert.SerializeObject(new Search()
				{
					siteCode = search.siteCode
				}).Replace(",\"lstCustomer\":null,\"lstCustomerClass\":null,\"lstTherapist\":null,\"lstConsultant\":null,\"lstCustomerType\":null", string.Empty));
				Search pagedList = search;
				List<Register> registers1 = registers;
				int? nullable = page;
				pagedList.lstCustomer = registers1.ToPagedList<Register>((nullable.HasValue ? nullable.GetValueOrDefault() : 1), 50);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(search);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Search(Search model)
		{
			ActionResult actionResult;
			Search search = new Search();
			try
			{
				if (base.ModelState.IsValid)
				{
					Service.Parameters.Clear();
					search.lstCustomerClass = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					search.lstCustomerClass.AddRange(Service.GetList<ListItems>("CustomerClass"));
					search.lstCustomerType = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					search.lstCustomerType.AddRange(Service.GetList<ListItems>("CustomerType"));
					search.lstConsultant = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					search.lstConsultant.AddRange(Service.GetList<ListItems>("Consultant"));
					search.lstTherapist = new List<ListItems>()
					{
						new ListItems()
						{
							itemCode = string.Empty,
							itemDesc = string.Empty
						}
					};
					search.lstTherapist.AddRange(Service.GetList<ListItems>("Therapist"));
					List<Register> registers = Service.PostRead<Register>("SearchCustomer", JsonConvert.SerializeObject(model).Replace(",\"lstCustomer\":null,\"lstCustomerClass\":null,\"lstTherapist\":null,\"lstConsultant\":null,\"lstCustomerType\":null", string.Empty));
					search.lstCustomer = registers.ToPagedList<Register>(1, 50);
					search.siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode;
				}
				actionResult = base.View(search);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return actionResult;
		}
	}
}