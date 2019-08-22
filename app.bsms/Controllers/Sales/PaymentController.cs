using app.bsms;
using app.bsms.api;
using app.bsms.Common;
using app.bsms.Controllers;
using app.bsms.Helpers;
using app.bsms.Models.Account;
using app.bsms.Models.General;
using app.bsms.Models.Manage.Service;
using app.bsms.Models.Sales;
using app.bsms.Models.Sales.Post;
using Newtonsoft.Json;
using Resources;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace app.bsms.Controllers.Sales
{
	[NoCache]
	public class PaymentController : BaseController
	{
		public PaymentController()
		{
		}

		[HttpPost]
		[SubmitButtonSelector(Name="Cancel")]
		[ValidateAntiForgeryToken]
		public ActionResult Cancel(app.bsms.Models.Sales.Cart model)
		{
			string str;
			try
			{
				Temp_Cart tempCart = new Temp_Cart();
				app.bsms.api.Service.Parameters.Clear();
				app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				app.bsms.api.Service.Parameters.Add("userId", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).userID);
				if (model.cart.cartToken != null && model.cart.cartToken != string.Empty)
				{
					app.bsms.api.Service.Parameters.Add("cartToken", model.cart.cartToken);
				}
				tempCart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
				tempCart.userID = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).userID;
				tempCart.siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode;
				tempCart.staffCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).staffCode;
				tempCart.macAddress = base.Request.ServerVariables["REMOTE_ADDR"];
				tempCart.payDetails = new List<app.bsms.Models.Sales.Post.Payment_Details>();
				app.bsms.api.Service.Parameters.Clear();
				JsonSerializerSettings jsonSerializerSetting = new JsonSerializerSettings()
				{
					ContractResolver = new NullToEmptyStringResolver()
				};
				if (model.cart.cartToken == null || !(model.cart.cartToken != string.Empty))
				{
					app.bsms.api.Service.Post<Temp_Cart>("itemCart", JsonConvert.SerializeObject(tempCart, jsonSerializerSetting));
				}
				else
				{
					app.bsms.api.Service.Parameters.Add("cartToken", model.cart.cartToken);
					app.bsms.api.Service.Put<Suspend_Cart>("itemCart", JsonConvert.SerializeObject(tempCart, jsonSerializerSetting));
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
			if (model.cart.cartToken == null || !(model.cart.cartToken != string.Empty))
			{
				str = null;
			}
			else
			{
				str = model.cart.cartToken;
			}
			return base.RedirectToAction("Index", "Cart", new { id = str });
		}

		public ActionResult Delete(string code, string type)
		{
			string str;
			try
			{
				if (!string.IsNullOrEmpty(code))
				{
					Temp_Cart tempCart = new Temp_Cart();
					app.bsms.api.Service.Parameters.Clear();
					app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
					app.bsms.api.Service.Parameters.Add("userId", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).userID);
					if (type != null && type != string.Empty)
					{
						app.bsms.api.Service.Parameters.Add("cartToken", type);
					}
					tempCart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
					tempCart.userID = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).userID;
					tempCart.siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode;
					tempCart.staffCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).staffCode;
					tempCart.macAddress = base.Request.ServerVariables["REMOTE_ADDR"];
					tempCart.payDetails = (
						from f in tempCart.payDetails
						where f.paymentGroup != code
						select f).ToList<app.bsms.Models.Sales.Post.Payment_Details>();
					app.bsms.api.Service.Parameters.Clear();
					JsonSerializerSettings jsonSerializerSetting = new JsonSerializerSettings()
					{
						ContractResolver = new NullToEmptyStringResolver()
					};
					if (type == null || !(type != string.Empty))
					{
						app.bsms.api.Service.Post<app.bsms.Models.Sales.Post.Cart>("itemCart", JsonConvert.SerializeObject(tempCart, jsonSerializerSetting));
					}
					else
					{
						app.bsms.api.Service.Parameters.Add("cartToken", type);
						app.bsms.api.Service.Put<Suspend_Cart>("itemCart", JsonConvert.SerializeObject(tempCart, jsonSerializerSetting));
					}
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
			UrlHelper url = base.Url;
			if (type == null || !(type != string.Empty))
			{
				str = null;
			}
			else
			{
				str = type;
			}
			//return base.Json(url.Action("Index", "Payment", new { id = str }));
            //Yoonus Changed to next line
            return base.Json(url.Action("Index", "Payment", new { id = str }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItem(string code, string type)
		{
			app.bsms.Models.Sales.Cart cart = new app.bsms.Models.Sales.Cart();
			try
			{
				app.bsms.api.Service.Parameters.Clear();
				app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				List<PaymentType> list = app.bsms.api.Service.GetList<PaymentType>("paymentType");
				app.bsms.api.Service.Parameters.Clear();
				app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				app.bsms.api.Service.Parameters.Add("userId", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).userID);
				if (base.Request.UrlReferrer.Query != null && base.Request.UrlReferrer.Query != string.Empty)
				{
					app.bsms.api.Service.Parameters.Add("cartToken", base.Request.UrlReferrer.Query);
				}
				cart.cart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
				if (cart.cart.payDetails != null)
				{
					cart.cart.transactionAmount = (
						from f in cart.cart.payDetails
						select f.paymentAmount).DefaultIfEmpty<decimal>(decimal.Zero).Sum();
				}
				cart.cart.totalDiscount = cart.cart.transactionDetails.Sum<Cart_Details>((Cart_Details f) => f.unitDiscount);
                //Yoonus Block this
				//if (cart.cart.totalAmount == cart.cart.depositAmount)
				//{
				//	cart.payDetails.paymentAmount = cart.cart.totalAmount - (cart.cart.transactionAmount + cart.cart.totalDiscount);
				//}
				//else
				//{
				//	cart.payDetails.paymentAmount = cart.cart.depositAmount - (cart.cart.transactionAmount + cart.cart.totalDiscount);
				//}
                //Yoonus Block this
                //and Add this
                //cart.payDetails.paymentAmount = cart.cart.depositAmount;
                cart.payDetails.paymentAmount = cart.cart.depositAmount- cart.cart.transactionAmount;
                //and Add this
                cart.payDetails.paymentType = code;
				cart.payDetails.paymentGroup = type;
				cart.payDetails.paymentDescription = list.Where<PaymentType>((PaymentType f) => {
					if (f.payCode != code)
					{
						return false;
					}
					return f.payGroup == type;
				}).SingleOrDefault<PaymentType>().payDescription;
				cart.payDetails.isGST = list.Where<PaymentType>((PaymentType f) => {
					if (f.payCode != code)
					{
						return false;
					}
					return f.payGroup == type;
				}).SingleOrDefault<PaymentType>().isGST;
				cart.payDetails.isCreditCard = list.Where<PaymentType>((PaymentType f) => {
					if (f.payCode != code)
					{
						return false;
					}
					return f.payGroup == type;
				}).SingleOrDefault<PaymentType>().isCreditCard;
				cart.payDetails.isOnlinePayment = list.Where<PaymentType>((PaymentType f) => {
					if (f.payCode != code)
					{
						return false;
					}
					return f.payGroup == type;
				}).SingleOrDefault<PaymentType>().isOnlinePayment;
				app.bsms.api.Service.Parameters.Clear();
				if (cart.payDetails.paymentDescription == "PREPAID")
				{
					app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
					app.bsms.api.Service.Parameters.Add("customerCode", cart.cart.customerCode);
					cart.lstPrepaid = new List<app.bsms.Models.Manage.Service.Prepaid>();
					cart.lstPrepaid.AddRange(app.bsms.api.Service.GetList<app.bsms.Models.Manage.Service.Prepaid>("getPrepaid"));
				}
				else if (cart.payDetails.paymentDescription.Contains("VOUCHER"))
				{
					app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
					app.bsms.api.Service.Parameters.Add("customerCode", cart.cart.customerCode);
					cart.lstVoucher = new List<Voucher>();
					cart.lstVoucher.AddRange(app.bsms.api.Service.GetList<Voucher>("getVoucherRecord"));
				}
				else if (cart.payDetails.paymentDescription != "Credit Note")
				{
					cart.lstDiscountReason = new List<ReasonCodes>()
					{
						new ReasonCodes()
						{
							reasonCode = string.Empty,
							reasonDesc = string.Empty
						}
					};
					cart.lstDiscountReason.AddRange(app.bsms.api.Service.GetList<ReasonCodes>("getDiscountReasonList"));
				}
				else
				{
					app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
					app.bsms.api.Service.Parameters.Add("customerCode", cart.cart.customerCode);
					cart.lstCreditnote = new List<CreditNote>();
					cart.lstCreditnote.AddRange(app.bsms.api.Service.GetList<CreditNote>("creditNoteAccountSummary"));
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return this.PartialView("_PartialPay", cart);
		}

		public ActionResult Index(string id)
		{
			app.bsms.Models.Sales.Cart cart = new app.bsms.Models.Sales.Cart();
			try
			{
				app.bsms.api.Service.Parameters.Clear();
				app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				app.bsms.api.Service.Parameters.Add("userId", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).userID);
				if (id != null && id != string.Empty)
				{
					app.bsms.api.Service.Parameters.Add("cartToken", id);
				}
				cart.cart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
				app.bsms.api.Service.Parameters.Clear();
				app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				cart.paymentTypes = app.bsms.api.Service.GetList<PaymentType>("paymentType");
				if (cart.cart.payDetails != null)
				{
					cart.cart.transactionAmount = (
						from f in cart.cart.payDetails
						select f.paymentAmount).DefaultIfEmpty<decimal>(decimal.Zero).Sum();
				}
				cart.cart.totalDiscount = cart.cart.transactionDetails.Sum<Cart_Details>((Cart_Details f) => f.unitDiscount);
				cart.cart.depositAmount = cart.cart.transactionDetails.Sum<Cart_Details>((Cart_Details f) => ((!f.depositAmount.HasValue ? new decimal?(new decimal()) : f.depositAmount)).Value);
				cart.cart.totalGST = cart.cart.transactionDetails.Sum<Cart_Details>((Cart_Details f) => f.gstAmountCollected);
				if (cart.cart.totalAmount == cart.cart.depositAmount)
				{
					cart.cart.outstandingAmount = cart.cart.totalAmount - cart.cart.transactionAmount;
				}
				else
				{
					cart.cart.outstandingAmount = cart.cart.depositAmount - cart.cart.transactionAmount;
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(cart);
		}

		public ActionResult Invoice(string id, string id2)
		{
			Invoice invoice = new Invoice();
			try
			{
				app.bsms.api.Service.Parameters.Clear();
				app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				invoice.clientLogo = app.bsms.api.Service.GetMedia("getClientLogo");
				Invoice invoice1 = app.bsms.api.Service.Get<Invoice>("getInvoiceTitle");
				invoice.siteHeader = invoice1.siteHeader;
				invoice.siteAddress = invoice1.siteAddress;
				invoice.companyRegNo = invoice1.companyRegNo;
				app.bsms.api.Service.Parameters.Add("invoiceNumber", id2);
				Invoice invoice2 = app.bsms.api.Service.Get<Invoice>("getInvoiceHeader");
				invoice.siteCode = invoice2.siteCode;
				invoice.invoiceNumber = invoice2.invoiceNumber;
				invoice.transactionNumber = invoice2.transactionNumber;
				invoice.transactionNumberTitle = invoice2.transactionNumberTitle;
				invoice.salesDate = invoice2.salesDate;
				invoice.salesTime = invoice2.salesTime;
				invoice.staffName = invoice2.staffName;
				invoice.customerCode = invoice2.customerCode;
				invoice.customerName = invoice2.customerName;
				invoice.customerEmail = invoice2.customerEmail;
				invoice.phoneNumber = invoice2.phoneNumber;
				invoice.previousPts = invoice2.previousPts;
				invoice.earnPts = invoice2.earnPts;
				invoice.usedPts = invoice2.usedPts;
				invoice.totalBalance = invoice2.totalBalance;
				invoice.totalDiscount = invoice2.totalDiscount;
				invoice.transactionRemarks = invoice2.transactionRemarks;
				Invoice invoice3 = app.bsms.api.Service.Get<Invoice>("getInvoiceFooterRemark");
				invoice.transactionFooter1 = invoice3.transactionFooter1;
				invoice.transactionFooter2 = invoice3.transactionFooter2;
				invoice.transactionFooter3 = invoice3.transactionFooter3;
				app.bsms.api.Service.Parameters.Clear();
				app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				app.bsms.api.Service.Parameters.Add("transactionNumber", id);
				invoice.invoiceDetails = app.bsms.api.Service.GetList<Invoice_Details>("getInvoiceDetail");
				invoice.customerSignature = app.bsms.api.Service.GetMedia("getCustomerSignature");
				invoice.GSTdetails = app.bsms.api.Service.GetList<GST>("getInvoiceGSTSummary");
				invoice.paymentDetails = app.bsms.api.Service.GetList<app.bsms.Models.Sales.Payment_Details>("getInvoiceFooterTax");
				app.bsms.api.Service.Parameters.Clear();
				app.bsms.api.Service.Parameters.Add("referenceCode", string.Empty);
				app.bsms.api.Service.Parameters.Add("customerCode", invoice.customerCode);
				app.bsms.api.Service.Parameters.Add("customerName", string.Empty);
				app.bsms.api.Service.Parameters.Add("nric", string.Empty);
				app.bsms.api.Service.Parameters.Add("siteCode", invoice.siteCode);
				app.bsms.api.Service.Parameters.Add("page", "General");
				invoice.customer = app.bsms.api.Service.Get<app.bsms.Models.Manage.Customer.Register>("customer");
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(invoice);
		}

		[HttpPost]
		[SubmitButtonSelector(Name="Proceed")]
		[ValidateAntiForgeryToken]
		public ActionResult Proceed(app.bsms.Models.Sales.Cart model)
		{
			ActionResult action;
			string str;
			string str1;
			string str2;
			app.bsms.Models.Sales.Cart cart = new app.bsms.Models.Sales.Cart();
			try
			{
				app.bsms.api.Service.Parameters.Clear();
				app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				app.bsms.api.Service.Parameters.Add("userId", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).userID);
				if (model.cart.cartToken != null && model.cart.cartToken != string.Empty)
				{
					app.bsms.api.Service.Parameters.Add("cartToken", model.cart.cartToken);
				}
				cart.cart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
				app.bsms.api.Service.Parameters.Clear();
				app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				cart.paymentTypes = app.bsms.api.Service.GetList<PaymentType>("paymentType");
				if (cart.cart.payDetails != null)
				{
					cart.cart.transactionAmount = (
						from f in cart.cart.payDetails
						select f.paymentAmount).DefaultIfEmpty<decimal>(decimal.Zero).Sum();
				}
                //Yoonus Adding
                /*
                if (cart.cart.transactionAmount < cart.cart.depositAmount)
                {
                    Alerts.body = "Payment incomplete";
                    Alerts.ErrorMessage = "Deposit amount is not paid" ;
                    base.TempData["Message"] = Alerts.ErrorMessage;
                    if (model.cart.cartToken == null || !(model.cart.cartToken != string.Empty))
                    {
                        str = null;
                    }
                    else
                    {
                        str = model.cart.cartToken;
                    }
                    action = base.RedirectToAction("Index", "Payment", new { id = str });
                }
                */
                //Yoonus Adding
                cart.cart.totalDiscount = cart.cart.transactionDetails.Sum<Cart_Details>((Cart_Details f) => f.unitDiscount);
                //Yoonus Blocking
                //if (cart.cart.totalAmount == cart.cart.depositAmount)
				//{
				//	cart.cart.outstandingAmount = cart.cart.totalAmount - (cart.cart.transactionAmount + cart.cart.totalDiscount);
				//}
				//else
				//{
				//	cart.cart.outstandingAmount = cart.cart.depositAmount - (cart.cart.transactionAmount + cart.cart.totalDiscount);
				//}
                //Yoonus Blocking
                //and Add this
                //cart.payDetails.paymentAmount = cart.cart.depositAmount;
                //cart.payDetails.paymentAmount = cart.cart.depositAmount - cart.cart.transactionAmount;
                cart.cart.outstandingAmount = cart.cart.depositAmount - cart.cart.transactionAmount ;
                //and Add this

                if (cart.cart.payDetails == null)
				{
                    Alerts.body = Resources.Resources.PaymentUnsuccessful; // "Payment Unsuccessful";
					Alerts.ErrorMessage = Resources.Resources.NoPayment; // "No Payment found";
                    base.TempData["Message"] = Alerts.ErrorMessage;
					if (model.cart.cartToken == null || !(model.cart.cartToken != string.Empty))
					{
						str = null;
					}
					else
					{
						str = model.cart.cartToken;
					}
					action = base.RedirectToAction("Index", "Payment", new { id = str });
				}
				else if (cart.cart.payDetails.Count == 0)
				{
					Alerts.body = Resources.Resources.PaymentUnsuccessful; // "Payment Unsuccessful";
                    Alerts.ErrorMessage = Resources.Resources.NoPayment; // "No Payment found";
                    base.TempData["Message"] = Alerts.ErrorMessage;
					if (model.cart.cartToken == null || !(model.cart.cartToken != string.Empty))
					{
						str2 = null;
					}
					else
					{
						str2 = model.cart.cartToken;
					}
					action = base.RedirectToAction("Index", "Payment", new { id = str2 });
				}
				else if (cart.cart.outstandingAmount <= decimal.Zero)
				{
					app.bsms.api.Service.Parameters.Clear();
					JsonSerializerSettings jsonSerializerSetting = new JsonSerializerSettings()
					{
						ContractResolver = new NullToEmptyStringResolver()
					};
					cart.sales = JsonConvert.DeserializeObject<app.bsms.Models.Sales.Post.Cart>(JsonConvert.SerializeObject(cart.cart, jsonSerializerSetting));
					cart.sales = app.bsms.api.Service.Post<app.bsms.Models.Sales.Post.Cart>("postSales", JsonConvert.SerializeObject(cart.sales, jsonSerializerSetting));
					if (cart.sales != null)
					{
						app.bsms.api.Service.Parameters.Clear();
						app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
						app.bsms.api.Service.Parameters.Add("userId", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).userID);
						if (model.cart.cartToken != null && model.cart.cartToken != string.Empty)
						{
							app.bsms.api.Service.Parameters.Add("cartToken", model.cart.cartToken);
						}
						app.bsms.api.Service.Delete("itemCart");
						action = base.RedirectToAction("Invoice", "Payment", new { id = cart.sales.transactionNumber, id2 = cart.sales.invoiceNumber });
					}
					else
					{
						Alerts.body = Resources.Resources.PaymentUnsuccessful; // "Payment Unsuccessful";
                        base.TempData["Message"] = Alerts.ErrorMessage;
						return base.View(cart);
					}
				}
				else
				{
					Alerts.body = Resources.Resources.PaymentUnsuccessful; // "Payment Unsuccessful";
                    Alerts.ErrorMessage = Resources.Resources.PayFull; // "Pay full deposit or change deposit";
                    base.TempData["Message"] = Alerts.ErrorMessage;
					if (model.cart.cartToken == null || !(model.cart.cartToken != string.Empty))
					{
						str1 = null;
					}
					else
					{
						str1 = model.cart.cartToken;
					}
					action = base.RedirectToAction("Index", "Payment", new { id = str1 });
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return action;
		}

		[HttpPost]
		public ActionResult SendEMail(string email, string sub, string msg)
		{
			try
			{
				MailMessage mailMessage = new MailMessage()
				{
					From = new MailAddress("sendinvoice@sequoia-asia.com")
				};
				mailMessage.To.Add(email);
				mailMessage.Subject = sub;
				mailMessage.Body = msg;
				mailMessage.IsBodyHtml = true;
				(new SmtpClient()
				{
					Host = "cpanel1.s.fozzy.com",
					Port = 465,
					Credentials = new NetworkCredential("sendinvoice@sequoia-asia.com", "qwertyuiop33!")
				}).Send(mailMessage);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			//return base.Json(string.Concat("e-Mail Sent to ", email));
            //Yoonus Changed to this
            return base.Json(string.Concat("e-Mail Sent to ", email), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Update(app.bsms.Models.Sales.Cart model)
		{
			ActionResult actionResult;
			decimal zero;
			decimal num;
			string str;
			decimal zero1;
			decimal num1;
			string str1;
			string str2;
			string str3;
			app.bsms.Models.Sales.Cart cart = new app.bsms.Models.Sales.Cart();
			try
			{
				app.bsms.api.Service.Parameters.Clear();
				app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				app.bsms.api.Service.Parameters.Add("userId", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).userID);
				if (model.cart.cartToken != null && model.cart.cartToken != string.Empty)
				{
					app.bsms.api.Service.Parameters.Add("cartToken", model.cart.cartToken);
				}
				cart.cart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
				if (cart.cart.payDetails == null)
				{
					cart.cart.payDetails = new List<app.bsms.Models.Sales.Post.Payment_Details>();
				}
                if (cart.payDetails.paySplit == null)
                {
                    cart.payDetails.paySplit = new List<app.bsms.Models.Sales.Post.PaymentSplit>();
                }

                if (model.payDetails.paymentDescription == "PREPAID")
				{
					app.bsms.api.Service.Parameters.Clear();
					app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
					app.bsms.api.Service.Parameters.Add("customerCode", cart.cart.customerCode);
					cart.lstPrepaid = new List<app.bsms.Models.Manage.Service.Prepaid>();
					cart.lstPrepaid = app.bsms.api.Service.GetList<app.bsms.Models.Manage.Service.Prepaid>("getPrepaid");
					app.bsms.Models.Manage.Service.Prepaid prepaid = (
						from f in cart.lstPrepaid
						where f.prepaidCode == model.payDetails.remark1
						select f).FirstOrDefault<app.bsms.Models.Manage.Service.Prepaid>();
					if (model.payDetails.paymentAmount > prepaid.availablePrepaidAmount )
					{
						Alerts.body = Resources.Resources.PaymentUnsuccessful; // "Payment Unsuccessful";
                        Alerts.ErrorMessage = Resources.Resources.InsufficientFunds;
						base.TempData["Message"] = Alerts.ErrorMessage;
						UrlHelper url = base.Url;
						if (model.cart.cartToken == null || !(model.cart.cartToken != string.Empty))
						{
							str3 = null;
						}
						else
						{
							str3 = model.cart.cartToken;
						}
                        //actionResult = base.Json(url.Action("Index", "Payment", new { id = str3 }));
                        //Yoonus Changingthis
                        actionResult = base.Json(url.Action("Index", "Payment", new { id = str3 }),JsonRequestBehavior.AllowGet);
						return actionResult;
					}

                    //Yoonus
                    if (prepaid.conditionType1 == "All" || prepaid.conditionType2 == "All")
                    {
                    }
                    else
                    {
                        if (cart.cart.transactionDetails.Where<Cart_Details>((Func<Cart_Details, bool>)(f => f.lineType == prepaid.conditionType1 || f.lineType == prepaid.conditionType2)).Count<Cart_Details>() > 0)
                        {
                        }
                        else
                        {
                            Alerts.body = Resources.Resources.PaymentUnsuccessful; // "Payment Unsuccessful";
                            Alerts.ErrorMessage = Resources.Resources.InsufficientFunds;
                            base.TempData["Message"] = Alerts.ErrorMessage;
                            UrlHelper url = base.Url;
                            if (model.cart.cartToken == null || !(model.cart.cartToken != string.Empty))
                            {
                                str3 = null;
                            }
                            else
                            {
                                str3 = model.cart.cartToken;
                            }
                            //actionResult = base.Json(url.Action("Index", "Payment", new { id = str3 }));
                            //Yoonus Changingthis
                            actionResult = base.Json(url.Action("Index", "Payment", new { id = str3 }), JsonRequestBehavior.AllowGet);
                            return actionResult;
                        }
                    }

                    //cart.payDetails.paySplit = new List<app.bsms.Models.Sales.Post.PaymentSplit>();
                    app.bsms.Models.Sales.Post.PaymentSplit paySplit = new app.bsms.Models.Sales.Post.PaymentSplit();
                    paySplit.usedAmount = 0;
                    decimal? availablePrepaidAmount = prepaid.availablePrepaidAmount;
                    string conditionType1 = prepaid.conditionType1;
                    string conditionType2 = prepaid.conditionType2;
                    if (prepaid.conditionType1 == "All")
                    {
                        conditionType1 = "SERVICE";
                        conditionType2 = "PRODUCT";
                    }
                    foreach (Cart_Details itemCart in cart.cart.transactionDetails)
                    {
                        if (itemCart.lineType == conditionType1 || itemCart.lineType == conditionType2)
                            //if (itemCart.lineType == prepaid.conditionType1 || itemCart.lineType == prepaid.conditionType2)
                            {
                                if (availablePrepaidAmount > itemCart.depositAmount)
                            {
                                paySplit.usedAmount = Convert.ToDecimal(itemCart.depositAmount);
                                availablePrepaidAmount -= itemCart.depositAmount;
                            }
                            else
                            {
                                paySplit.usedAmount = Convert.ToDecimal(itemCart.depositAmount - availablePrepaidAmount);
                                availablePrepaidAmount = 0;
                            }
                        }
                        paySplit.conditionType1 = prepaid.conditionType1;
                        paySplit.conditionType2 = prepaid.conditionType2;
                        paySplit.itemNo = itemCart.itemCode;
                        paySplit.posDaudLineNo = itemCart.lineNumber;
                        cart.payDetails.paySplit.Add(paySplit);
                        if (availablePrepaidAmount == 0)
                        {
                            break;
                        }
                    }
                    //decimal? usedAmount = cart.cart.transactionDetails.Sum<Cart_Details>((Cart_Details f) => f.depositAmount);
                    //int count = cart.cart.transactionDetails.Count;
                    //int num3 = cart.cart.transactionDetails.Where<Cart_Details>((Func<Cart_Details, bool>)(f => f.lineType == "TD")).Count<Cart_Details>();
                    //cart.cart.salesType = count != num3 ? "Receipt" : "Receipt";

                    //Yoonus

                    /*
                    cart.payDetails.paySplit = new List<app.bsms.Models.Sales.Post.PaymentSplit>();
                    //app.bsms.Models.Sales.Post.PaymentSplit paySplit = new app.bsms.Models.Sales.Post.PaymentSplit();
                    app.bsms.Models.Sales.Post.PaymentSplit paySplit = (
                        from f in cart.cartDetails
                        where f.lineType == prepaid.conditionType1 || f.lineType == prepaid.conditionType2
                        select f).FirstOrDefault<app.bsms.Models.Manage.Service.Prepaid>();
                    paySplit.itemNo = cart.cartDetails.
                    cart.payDetails.paySplit.Add;
                    */
                }
                else if (model.payDetails.paymentDescription.Contains("VOUCHER"))
				{
					app.bsms.api.Service.Parameters.Clear();
					app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
					app.bsms.api.Service.Parameters.Add("customerCode", cart.cart.customerCode);
					cart.lstVoucher = new List<Voucher>();
					cart.lstVoucher = app.bsms.api.Service.GetList<Voucher>("getVoucherRecord");
					Voucher voucher = (
						from f in cart.lstVoucher
						where f.voucherNo == model.payDetails.remark1
						select f).FirstOrDefault<Voucher>();
					if (model.payDetails.paymentAmount > voucher.@value && voucher.issuedExpiryDate < DateTime.Now.Date)
					{
						Alerts.body = Resources.Resources.PaymentUnsuccessful; // "Payment Unsuccessful";
                        Alerts.ErrorMessage = Resources.Resources.InsufficientFunds;
						base.TempData["Message"] = Alerts.ErrorMessage;
						UrlHelper urlHelper = base.Url;
						if (model.cart.cartToken == null || !(model.cart.cartToken != string.Empty))
						{
							str2 = null;
						}
						else
						{
							str2 = model.cart.cartToken;
						}
						//actionResult = base.Json(urlHelper.Action("Index", "Payment", new { id = str2 }));
                        //Yoonus changing this
                        actionResult = base.Json(urlHelper.Action("Index", "Payment", new { id = str2 }),JsonRequestBehavior.AllowGet);
                        return actionResult;
					}
				}
				else if (model.payDetails.paymentDescription == "Credit Note")
				{
					app.bsms.api.Service.Parameters.Clear();
					app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
					app.bsms.api.Service.Parameters.Add("customerCode", cart.cart.customerCode);
					cart.lstCreditnote = new List<CreditNote>();
					cart.lstCreditnote = app.bsms.api.Service.GetList<CreditNote>("creditNoteAccountSummary");
                    CreditNote creditNote = (
                        from f in cart.lstCreditnote
                        where f.creditNote == model.payDetails.remark1
                        select f).FirstOrDefault<CreditNote>();
					if (model.payDetails.paymentAmount > creditNote.balance)
					{
						Alerts.body = Resources.Resources.PaymentUnsuccessful; // "Payment Unsuccessful";
                        Alerts.ErrorMessage = Resources.Resources.InsufficientFunds;
						base.TempData["Message"] = Alerts.ErrorMessage;
						UrlHelper url1 = base.Url;
						if (model.cart.cartToken == null || !(model.cart.cartToken != string.Empty))
						{
							str1 = null;
						}
						else
						{
							str1 = model.cart.cartToken;
						}
						//actionResult = base.Json(url1.Action("Index", "Payment", new { id = str1 }));
                        //Yoonus changing this
                        actionResult = base.Json(url1.Action("Index", "Payment", new { id = str1 }),JsonRequestBehavior.AllowGet);
                        return actionResult;
					}
                    //cart.payDetails.remark2 = creditNote.transactionNumber;
                    model.payDetails.remark2 = creditNote.transactionNumber;
                }
                //model.payDetails.remark2 = creditNote.transactionNumber;

                List<Settings> item = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).settings;
				bool flag = ((
					from s in item
					where s.settingName == "GST SETTING"
					select s).FirstOrDefault<Settings>().settingValue == "INCLUSIVE" ? true : false);
				decimal num2 = Convert.ToDecimal((
					from s in item
					where s.settingName == "GST PERCENTAGE"
					select s).FirstOrDefault<Settings>().settingValue);
				string str4 = (
					from s in item
					where s.settingName == "GST APPLY BY"
					select s).FirstOrDefault<Settings>().settingValue;
				if (cart.cart.payDetails.Where<app.bsms.Models.Sales.Post.Payment_Details>((app.bsms.Models.Sales.Post.Payment_Details f) => {
					if (f.paymentGroup != model.payDetails.paymentGroup)
					{
						return false;
					}
					return f.paymentDescription == model.payDetails.paymentDescription;
				}).Count<app.bsms.Models.Sales.Post.Payment_Details>() <= 0)
				{
					int num3 = (
						from f in cart.cart.payDetails
						select f.lineNumber).DefaultIfEmpty<int>(0).Max();
					List<app.bsms.Models.Sales.Post.Payment_Details> paymentDetails = cart.cart.payDetails;
					app.bsms.Models.Sales.Post.Payment_Details paymentDetail = new app.bsms.Models.Sales.Post.Payment_Details()
					{
						lineNumber = num3 + 1,
						paymentGroup = model.payDetails.paymentGroup,
						paymentType = model.payDetails.paymentType,
						paymentDescription = model.payDetails.paymentDescription,
						remark1 = model.payDetails.remark1,
						remark2 = model.payDetails.remark2,
						paymentAmount = model.payDetails.paymentAmount,
						payActualAmount = model.payDetails.paymentAmount,
						payCurrency = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).currency,
                        paySplit = cart.payDetails.paySplit
                    };
					if (model.payDetails.isGST)
					{
						zero = (flag ? Math.Round(model.payDetails.paymentAmount / (decimal.One + num2), 2) : Math.Round((model.payDetails.paymentAmount * num2) / new decimal(100), 2));
					}
					else
					{
						zero = decimal.Zero;
					}
					paymentDetail.transactionItemGST = zero;
					if (model.payDetails.isGST)
					{
						num = (flag ? Math.Round(model.payDetails.paymentAmount / (decimal.One + num2), 2) : Math.Round((model.payDetails.paymentAmount * num2) / new decimal(100), 2));
					}
					else
					{
						num = decimal.Zero;
					}
					paymentDetail.transactionPayGST = num;
					paymentDetails.Add(paymentDetail);
				}
				else
				{
					app.bsms.Models.Sales.Post.Payment_Details paymentDetail1 = cart.cart.payDetails.SingleOrDefault<app.bsms.Models.Sales.Post.Payment_Details>((app.bsms.Models.Sales.Post.Payment_Details f) => {
						if (f.paymentGroup != model.payDetails.paymentGroup)
						{
							return false;
						}
						return f.paymentDescription == model.payDetails.paymentDescription;
					});
					paymentDetail1.remark1 = model.payDetails.remark1;
					paymentDetail1.remark2 = model.payDetails.remark2;
					paymentDetail1.remark3 = model.payDetails.remark3;
					paymentDetail1.remark4 = model.payDetails.remark4;
					app.bsms.Models.Sales.Post.Payment_Details paymentDetail2 = paymentDetail1;
					paymentDetail2.paymentAmount = paymentDetail2.paymentAmount + model.payDetails.paymentAmount;
					paymentDetail2 = paymentDetail1;
					paymentDetail2.payActualAmount = paymentDetail2.payActualAmount + model.payDetails.paymentAmount;
					paymentDetail2 = paymentDetail1;
					app.bsms.Models.Sales.Post.Payment_Details paymentDetail3 = paymentDetail2;
					decimal num4 = paymentDetail2.transactionItemGST;
					if (model.payDetails.isGST)
					{
						zero1 = (flag ? Math.Round(model.payDetails.paymentAmount / (decimal.One + num2), 2) : Math.Round((model.payDetails.paymentAmount * num2) / new decimal(100), 2));
					}
					else
					{
						zero1 = decimal.Zero;
					}
					paymentDetail3.transactionItemGST = num4 + zero1;
					paymentDetail2 = paymentDetail1;
					app.bsms.Models.Sales.Post.Payment_Details paymentDetail4 = paymentDetail2;
					decimal num5 = paymentDetail2.transactionPayGST;
					if (model.payDetails.isGST)
					{
						num1 = (flag ? Math.Round(model.payDetails.paymentAmount / (decimal.One + num2), 2) : Math.Round((model.payDetails.paymentAmount * num2) / new decimal(100), 2));
					}
					else
					{
						num1 = decimal.Zero;
					}
					paymentDetail4.transactionPayGST = num5 + num1;
				}
				cart.cart.transactionAmount = (
					from f in cart.cart.payDetails
					select f.paymentAmount).DefaultIfEmpty<decimal>(decimal.Zero).Sum();
				app.bsms.api.Service.Parameters.Clear();
				JsonSerializerSettings jsonSerializerSetting = new JsonSerializerSettings()
				{
					ContractResolver = new NullToEmptyStringResolver()
				};
				if (model.cart.cartToken == null || !(model.cart.cartToken != string.Empty))
				{
					app.bsms.api.Service.Post<Temp_Cart>("itemCart", JsonConvert.SerializeObject(cart.cart, jsonSerializerSetting));
				}
				else
				{
					app.bsms.api.Service.Parameters.Add("cartToken", model.cart.cartToken);
					app.bsms.api.Service.Put<Suspend_Cart>("itemCart", JsonConvert.SerializeObject(cart.cart, jsonSerializerSetting));
				}
				UrlHelper urlHelper1 = base.Url;
				if (model.cart.cartToken == null || !(model.cart.cartToken != string.Empty))
				{
					str = null;
				}
				else
				{
					str = model.cart.cartToken;
				}
				//return base.Json(urlHelper1.Action("Index", "Payment", new { id = str }));
                //Yoonus Changed to this
                return base.Json(urlHelper1.Action("Index", "Payment", new { id = str }),JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
			{
				throw exception;
			}
			return actionResult;
		}
	}
}