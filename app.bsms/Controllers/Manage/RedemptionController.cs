using app.bsms.api;
using app.bsms.Common;
using app.bsms.Controllers;
using app.bsms.Helpers;
using app.bsms.Models.Account;
using app.bsms.Models.General;
using app.bsms.Models.Manage.Customer;
using app.bsms.Models.Manage.Post;
using app.bsms.Models.Manage.Service;
using app.bsms.Models.Sales;
using app.bsms.Models.Sales.Post;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace app.bsms.Controllers.Manage
{
	public class RedemptionController : BaseController
	{
		public RedemptionController()
		{
		}

		public ActionResult Details(string id, string id2)
		{
			app.bsms.Models.Manage.Service.ReverseTreatment reverseTreatment = new app.bsms.Models.Manage.Service.ReverseTreatment();
			try
			{
				reverseTreatment.reverse.siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode;
				reverseTreatment.reverse.customerCode = id;
				reverseTreatment.reverse.treatmentParentCode = id2;
				app.bsms.api.Service.Parameters.Clear();
				app.bsms.api.Service.Parameters.Add("siteCode", reverseTreatment.reverse.siteCode);
				app.bsms.api.Service.Parameters.Add("customerCode", id);
				app.bsms.api.Service.Parameters.Add("treatmentParentCode", id2);
				reverseTreatment.treatments = app.bsms.api.Service.GetList<Treatment>("serviceRedemptionReverseTreatmentList");
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(reverseTreatment);
		}

		public ActionResult Index(string id)
		{
			List<Redemption> redemptions = new List<Redemption>();
			try
			{
				((dynamic)base.ViewBag).customerCode = id;
				app.bsms.api.Service.Parameters.Clear();
				app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				app.bsms.api.Service.Parameters.Add("customerCode", id);
				redemptions = app.bsms.api.Service.GetList<Redemption>("serviceRedemptionList");
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(redemptions);
		}

                      
        public ActionResult NextAppointment(string id, string id2)
		{
			return base.View();
		}

		[HttpPost]
		public JsonResult RedeemItem(Cart_Details model)
		{
			JsonResult jsonResult;
			app.bsms.Models.Sales.Cart cart = new app.bsms.Models.Sales.Cart();
			try
			{
				decimal num = new decimal();
				app.bsms.api.Service.Parameters.Clear();
				app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				app.bsms.api.Service.Parameters.Add("customerCode", model.staffcode);
				app.bsms.api.Service.Parameters.Add("treatmentParentCode", model.referenceTreatmentCode);
				List<Accounts> list = app.bsms.api.Service.GetList<Accounts>("serviceAccountSummaryInfo");
				Treatment treatment = app.bsms.api.Service.GetList<Treatment>("serviceRedemptionReverseTreatmentList").FirstOrDefault<Treatment>();
				if (list != null)
				{
					num = list.Sum<Accounts>((Accounts f) => f.balance);
					list.Sum<Accounts>((Accounts f) => f.outstanding);
				}
				app.bsms.api.Service.Parameters.Clear();
				app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				app.bsms.api.Service.Parameters.Add("userId", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).userID);
				cart.cart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
				if (cart.cart.userID == null)
				{
					cart.cart = new Temp_Cart()
					{
						userID = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).userID,
						siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode,
						staffCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).staffCode,
						macAddress = base.Request.ServerVariables["REMOTE_ADDR"],
						customerCode = model.staffcode,
						transactionDetails = new List<Cart_Details>()
					};
				}
				else if ((
					from f in cart.cart.transactionDetails
					where f.lineType == "TD"
					select f).Count<Cart_Details>() <= 0 || !(cart.cart.customerCode != model.staffcode))
				{
					cart.cart.customerCode = model.staffcode;
					num = num - (
						from f in cart.cart.transactionDetails
						where f.lineType == "TD"
						select f).Sum<Cart_Details>((Cart_Details f) => f.unitPrice);
					if (cart.cart.transactionDetails.Where<Cart_Details>((Cart_Details f) => {
						if (f.treatmentCode != model.treatmentCode)
						{
							return false;
						}
						return f.referenceTreatmentCode == model.referenceTreatmentCode;
					}).Count<Cart_Details>() > 0)
					{
                        jsonResult = base.Json("{ \"status\": \"0\", \"message\":\"Item already exists\" }", JsonRequestBehavior.AllowGet);
                        //jsonResult = base.Json(string.Concat("{ \"status\": \"0\", \"message\":\"Item already exists\" }"), JsonRequestBehavior.AllowGet);
                        return jsonResult;
					}
				}
				else
				{
                    //jsonResult = base.Json(string.Concat("{ \"status\": \"0\", \"message\":\"Item already exists\" }"), JsonRequestBehavior.AllowGet);
                    jsonResult = base.Json(string.Concat("{ \"status\": \"0\", \"message\":\"Re-deemed service(s) is not belongs to ", model.staffcode, "\" }"), JsonRequestBehavior.AllowGet);
                    //return jsonResult;
                }
				model.staffcode = null;
				if (cart.cart.customerCode != null && cart.cart.customerCode != string.Empty)
				{
					Register customer = Utility.GetCustomer(cart.cart.customerCode, ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
					cart.cart.customerName = customer.customerName;
				}
				num -= model.unitPrice;
				if (num >= decimal.Zero)
				{
					model.balanceAmount = num;
					if (treatment != null)
					{
						model.salesStaffDetails = treatment.salesStaffDetails;
					}
					model.treatmentCode = model.treatmentCode;
					model.referenceTreatmentCode = model.referenceTreatmentCode;
                    //Yoonustopup
                    model.referenceTransactionNumber = model.referenceTransactionNumber;
                    model.topupBalance = model.topupBalance;
                    model.topupOutstanding = model.topupOutstanding;
                    //Yoonus
                    model.courseDetails = new List<Course_Details>();
					Course_Details courseDetail = new Course_Details()
					{
						treatmentNumber = 1,
						itemCode = model.itemCode,
						itemDescription = model.itemName,
						treatmentUnitPrice = model.itemAmount,
						courseType = "N",
						treatmentStatus = "Done", //Yoonus Changing to Done from "Open"
						treatmentDate = DateTime.Now,
						nextAppt = null,
						isFOC = new bool?(false)
					};
					model.courseDetails.Add(courseDetail);
					model.holdItemQty = (model.holdItemQty.IsNullOrZero() ? new int?(0) : model.holdItemQty);
                    model.depositAmount = (model.depositAmount.IsNullOrZero() ? new decimal?(0) : model.depositAmount);
                    cart.cart.transactionDetails.Add(model);
					cart.cart.totalQuantity = cart.cart.transactionDetails.Sum<Cart_Details>((Cart_Details f) => f.itemQty);
					cart.cart.totalAmount = cart.cart.transactionDetails.Sum<Cart_Details>((Cart_Details f) => f.itemAmount);
					cart.cart.depositAmount = cart.cart.transactionDetails.Sum<Cart_Details>((Cart_Details f) => ((!f.depositAmount.HasValue ? new decimal?(new decimal()) : f.depositAmount)).Value);
                    cart.cart.payDetails = new List<app.bsms.Models.Sales.Post.Payment_Details>();
					app.bsms.api.Service.Parameters.Clear();
					JsonSerializerSettings jsonSerializerSetting = new JsonSerializerSettings()
					{
						ContractResolver = new NullToEmptyStringResolver()
					};
					app.bsms.api.Service.Post<Temp_Cart>("itemCart", JsonConvert.SerializeObject(cart.cart, jsonSerializerSetting));
					jsonResult = base.Json("{ \"status\": \"1\", \"message\":\"\" }", JsonRequestBehavior.AllowGet);
				}
				else
				{
					jsonResult = base.Json("{ \"status\": \"0\", \"message\":\"Insufficient Balance\" }", JsonRequestBehavior.AllowGet);
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return jsonResult;
		}

        //Yoonus Creating
        [HttpPost]
        public JsonResult TopupItem(Cart_Details model)
        {
            JsonResult jsonResult;
            app.bsms.Models.Sales.Cart cart = new app.bsms.Models.Sales.Cart();
            try
            {
                decimal num = new decimal();
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
                app.bsms.api.Service.Parameters.Add("customerCode", model.staffcode);
                app.bsms.api.Service.Parameters.Add("treatmentParentCode", model.referenceTreatmentCode);
                List<Accounts> list = app.bsms.api.Service.GetList<Accounts>("serviceAccountSummaryInfo");
                Treatment treatment = app.bsms.api.Service.GetList<Treatment>("serviceRedemptionReverseTreatmentList").FirstOrDefault<Treatment>();
                if (list != null)
                {
                    num = list.Sum<Accounts>((Accounts f) => f.balance);
                    list.Sum<Accounts>((Accounts f) => f.outstanding);
                }
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
                app.bsms.api.Service.Parameters.Add("userId", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).userID);
                cart.cart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
                if (cart.cart.userID == null)
                {
                    cart.cart = new Temp_Cart()
                    {
                        userID = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).userID,
                        siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode,
                        staffCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).staffCode,
                        macAddress = base.Request.ServerVariables["REMOTE_ADDR"],
                        customerCode = model.staffcode,
                        transactionDetails = new List<Cart_Details>()
                    };
                }
                else if ((
                    from f in cart.cart.transactionDetails
                    where f.lineType == "TP SERVICE"
                    select f).Count<Cart_Details>() <= 0 || !(cart.cart.customerCode != model.staffcode))
                {
                    cart.cart.customerCode = model.staffcode;
                    num = num - (
                        from f in cart.cart.transactionDetails
                        where f.lineType == "TP SERVICE"
                        select f).Sum<Cart_Details>((Cart_Details f) => f.unitPrice);
                    if (cart.cart.transactionDetails.Where<Cart_Details>((Cart_Details f) => {
                        if (f.treatmentCode != model.treatmentCode)
                        {
                            return false;
                        }
                        return f.referenceTreatmentCode == model.referenceTreatmentCode;
                    }).Count<Cart_Details>() > 0)
                    {
                        jsonResult = base.Json("{ \"status\": \"0\", \"message\":\"Item already exists\" }", JsonRequestBehavior.AllowGet);
                        return jsonResult;
                    }
                }
                else
                {
                    jsonResult = base.Json(string.Concat("{ \"status\": \"0\", \"message\":\"Topup is not belong to ", model.staffcode, "\" }"), JsonRequestBehavior.AllowGet);
                    return jsonResult;
                }
                model.staffcode = null;
                if (cart.cart.customerCode != null && cart.cart.customerCode != string.Empty)
                {
                    Register customer = Utility.GetCustomer(cart.cart.customerCode, ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
                    cart.cart.customerName = customer.customerName;
                }
                num -= model.unitPrice;
                    model.balanceAmount = num;
                    if (treatment != null)
                    {
                        model.salesStaffDetails = treatment.salesStaffDetails;
                    }
                    model.treatmentCode = model.treatmentCode;
                    model.referenceTreatmentCode = model.referenceTreatmentCode;
                    //Yoonus
                    model.referenceTransactionNumber = model.referenceTransactionNumber;
                    model.topupBalance = model.topupBalance;
                    model.topupOutstanding = model.topupOutstanding-model.unitPrice;
                    //Yoonus
                    model.holdItemQty = (model.holdItemQty.IsNullOrZero() ? new int?(0) : model.holdItemQty);
                    model.depositAmount = (model.depositAmount.IsNullOrZero() ? new decimal?(0) : model.depositAmount);
                    cart.cart.transactionDetails.Add(model);
                    cart.cartDetails.referenceTransactionNumber = model.referenceTransactionNumber;
                    cart.cartDetails.topupBalance = model.topupBalance;
                    cart.cartDetails.topupOutstanding = model.topupOutstanding;
                cart.cart.totalQuantity = cart.cart.transactionDetails.Sum<Cart_Details>((Cart_Details f) => f.itemQty);
                    cart.cart.totalAmount = cart.cart.transactionDetails.Sum<Cart_Details>((Cart_Details f) => f.itemAmount);
                    cart.cart.depositAmount = cart.cart.transactionDetails.Sum<Cart_Details>((Cart_Details f) => ((!f.depositAmount.HasValue ? new decimal?(new decimal()) : f.depositAmount)).Value);
                    cart.cart.payDetails = new List<app.bsms.Models.Sales.Post.Payment_Details>();
                    app.bsms.api.Service.Parameters.Clear();
                    JsonSerializerSettings jsonSerializerSetting = new JsonSerializerSettings()
                    {
                        ContractResolver = new NullToEmptyStringResolver()
                    };
                    app.bsms.api.Service.Post<Temp_Cart>("itemCart", JsonConvert.SerializeObject(cart.cart, jsonSerializerSetting));
                    jsonResult = base.Json("{ \"status\": \"1\", \"message\":\"\" }", JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return jsonResult;
        }
        //Yoonus Creating

        //Yoonus Adding
        [HttpPost]
        public JsonResult RecallTransaction(Cart_Details model)
        {
            JsonResult jsonResult;
            app.bsms.Models.Sales.Cart cart = new app.bsms.Models.Sales.Cart();
            try
            {
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
                //app.bsms.api.Service.Parameters.Add("customerCode", model.staffcode);
                app.bsms.api.Service.Parameters.Add("transactionNumber", model.transactionNumber);
                //app.bsms.api.Service.Parameters.Add("userId", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).userID);
                cart.cart = app.bsms.api.Service.Get<Temp_Cart>("postSales");
                if (cart.cart.transactionNumber == null)
                {
                    jsonResult = base.Json(string.Concat("{ \"status\": \"0\", \"message\":\"Are you sure to void this transaction?", "\" }"), JsonRequestBehavior.AllowGet);
                    //return null;
                } else
                {
                    jsonResult = base.Json("{ \"status\": \"1\", \"message\":\"" + cart.cart.transactionNumber + "\\" + cart.cart.invoiceNumber + "\" }", JsonRequestBehavior.AllowGet);
                    //jsonResult = base.Json("{ \"status\": \"1\", \"message\":\"\", \"data\":" + (object)JsonConvert.SerializeObject((object)app.bsms.api.Service.Get<app.bsms.Models.Sales.Post.Cart>("itemCart").transactionDetails + " }", JsonRequestBehavior.AllowGet);
                    //return this.Json((object)JsonConvert.SerializeObject((object)cart.cart.transactionDetails), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return jsonResult;
        }
        //

        public ActionResult Reminder(string id, string id2)
		{
			app.bsms.Models.Manage.Service.ReverseTreatment reverseTreatment = new app.bsms.Models.Manage.Service.ReverseTreatment();
			try
			{
				reverseTreatment.reverse.siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode;
				reverseTreatment.reverse.customerCode = id;
				reverseTreatment.reverse.treatmentParentCode = id2;
				app.bsms.api.Service.Parameters.Clear();
				app.bsms.api.Service.Parameters.Add("siteCode", reverseTreatment.reverse.siteCode);
				app.bsms.api.Service.Parameters.Add("customerCode", id);
				app.bsms.api.Service.Parameters.Add("treatmentParentCode", id2);
				reverseTreatment.treatments = app.bsms.api.Service.GetList<Treatment>("serviceRedemptionReverseTreatmentList");
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(reverseTreatment);
		}

		public ActionResult ReverseTreatment(string id, string id2)
		{
			app.bsms.Models.Manage.Service.ReverseTreatment reverseTreatment = new app.bsms.Models.Manage.Service.ReverseTreatment();
			try
			{
				reverseTreatment.reverse.siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode;
				reverseTreatment.reverse.customerCode = id;
				reverseTreatment.reverse.treatmentParentCode = id2;
				reverseTreatment.types = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = "+",
						itemDesc = "Add"
					},
					new ListItems()
					{
						itemCode = "-",
						itemDesc = "Subtract"
					}
				};
				app.bsms.api.Service.Parameters.Clear();
				app.bsms.api.Service.Parameters.Add("siteCode", reverseTreatment.reverse.siteCode);
				reverseTreatment.reverseReasons = new List<ReverseReason>()
				{
					new ReverseReason()
					{
						siteCode = reverseTreatment.reverse.siteCode,
						reverseNumber = string.Empty,
						reverseDescription = string.Empty
					}
				};
				reverseTreatment.reverseReasons.AddRange(app.bsms.api.Service.GetList<ReverseReason>("reverseReason"));
				app.bsms.api.Service.Parameters.Add("customerCode", id);
				app.bsms.api.Service.Parameters.Add("treatmentParentCode", id2);
				reverseTreatment.treatments = app.bsms.api.Service.GetList<Treatment>("serviceRedemptionReverseTreatmentList");
				reverseTreatment.treatment_trans = app.bsms.api.Service.GetList<TreatmentTransaction>("serviceRedemptionReverseTransactionRecord");
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(reverseTreatment);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ReverseTreatment(app.bsms.Models.Manage.Service.ReverseTreatment model)
		{
			ActionResult action;
			app.bsms.Models.Manage.Service.ReverseTreatment reverseTreatment = new app.bsms.Models.Manage.Service.ReverseTreatment();
			try
			{
				reverseTreatment.reverse.siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode;
				reverseTreatment.reverse.customerCode = model.reverse.customerCode;
				reverseTreatment.reverse.treatmentParentCode = model.reverse.treatmentParentCode;
				reverseTreatment.types = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = "+",
						itemDesc = "Add"
					},
					new ListItems()
					{
						itemCode = "-",
						itemDesc = "Subtract"
					}
				};
				app.bsms.api.Service.Parameters.Clear();
				app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				reverseTreatment.reverseReasons = new List<ReverseReason>()
				{
					new ReverseReason()
					{
						siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode,
						reverseNumber = string.Empty,
						reverseDescription = string.Empty
					}
				};
				reverseTreatment.reverseReasons.AddRange(app.bsms.api.Service.GetList<ReverseReason>("reverseReason"));
				app.bsms.api.Service.Parameters.Add("customerCode", model.reverse.customerCode);
				app.bsms.api.Service.Parameters.Add("treatmentParentCode", model.reverse.treatmentParentCode);
				reverseTreatment.treatments = app.bsms.api.Service.GetList<Treatment>("serviceRedemptionReverseTreatmentList");
				reverseTreatment.treatment_trans = app.bsms.api.Service.GetList<TreatmentTransaction>("serviceRedemptionReverseTransactionRecord");
				if (!base.ModelState.IsValid)
				{
					return base.View(reverseTreatment);
				}
				else
				{
					List<ReverseItems> reverseItems = new List<ReverseItems>();
					if (model.strReversalItems != null)
					{
						reverseItems = JsonConvert.DeserializeObject<List<ReverseItems>>(model.strReversalItems);
					}
					model.reverse.siteCode = reverseTreatment.reverse.siteCode;
					model.reverse.staffCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).staffCode;
					model.reverse.staffName = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).fullName;
					if (model.reverse.customerCode != null && model.reverse.customerCode != string.Empty)
					{
						Register customer = Utility.GetCustomer(reverseTreatment.reverse.customerCode, ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
						model.reverse.customerName = customer.customerName;
						app.bsms.api.Service.Parameters.Clear();
						app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
						app.bsms.api.Service.Parameters.Add("customerCode", model.reverse.customerCode);
						List<Redemption> list = app.bsms.api.Service.GetList<Redemption>("serviceRedemptionList");
						if (list != null)
						{
							model.reverse.originalTransactionNumber = (
								from f in list
								where f.treatmentParentCode == model.reverse.treatmentParentCode
								select f).FirstOrDefault<Redemption>().transactionNumber;
						}
					}
					model.reverse.adjustedAmount = (model.adjustmentType == "-" ? model.reverse.adjustedAmount * decimal.MinusOne : model.reverse.adjustedAmount);
					model.reverse.originalBalanceAmount = reverseTreatment.treatment_trans.Sum<TreatmentTransaction>((TreatmentTransaction f) => f.creditBalance);
					model.reverse.creditNoteAmount = reverseTreatment.treatment_trans.Sum<TreatmentTransaction>((TreatmentTransaction f) => f.creditBalance) + model.reverse.adjustedAmount;
					model.reverse.reversalItems = reverseItems;
					app.bsms.api.Service.Parameters.Clear();
					JsonSerializerSettings jsonSerializerSetting = new JsonSerializerSettings()
					{
						ContractResolver = new NullToEmptyStringResolver()
					};
					app.bsms.Models.Manage.Service.ReverseTreatment reverseTreatment1 = app.bsms.api.Service.Post<app.bsms.Models.Manage.Service.ReverseTreatment>("reverseTreatment", JsonConvert.SerializeObject(model.reverse, jsonSerializerSetting));
					Alerts.body = "Success!";
					Alerts.Success = string.Concat("Reversal Number: ", reverseTreatment1.reversalNumber, ", CreditNote Number: ", reverseTreatment1.creditNoteNumber);
					base.TempData["Message"] = Alerts.Success;
					action = base.RedirectToAction("Index", "ReverseTreatment", new { id = reverseTreatment.reverse.customerCode, id2 = reverseTreatment.reverse.treatmentParentCode });
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return action;
		}
	}
}