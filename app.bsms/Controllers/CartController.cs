
// Type: app.bsms.Controllers.CartController

using app.bsms.Common;
using app.bsms.Helpers;
using app.bsms.Models.Account;
using app.bsms.Models.Catelogue;
using app.bsms.Models.General;
using app.bsms.Models.Manage.Customer;
using app.bsms.Models.Sales;
using app.bsms.Models.Sales.Post;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

namespace app.bsms.Controllers
{
    [NoCache]
    public class CartController : BaseController
    {
        public ActionResult Index(string id)
        {
            app.bsms.Models.Sales.Cart cart = new app.bsms.Models.Sales.Cart();
            try
            {
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                app.bsms.api.Service.Parameters.Add("userId", ((User)this.Session["Login_Details"]).userID);
                if (id != null && id != string.Empty)
                    app.bsms.api.Service.Parameters.Add("cartToken", id);
                cart.cart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
                if (cart.cart.transactionDetails == null)
                    return (ActionResult)this.RedirectToAction("Empty", "Cart");
                if (cart.cart.transactionDetails.Count<Cart_Details>() == 0)
                    return (ActionResult)this.RedirectToAction("Empty", "Cart");
                //cart.lstCustomer = new List<Register>();
                ////List<Register> registerList = app.bsms.api.Service.PostRead<Register>("SearchCustomer", JsonConvert.SerializeObject((object)new Search()
                //List<Register> registerList = app.bsms.api.Service.PostRead<Register>("listCustomer", JsonConvert.SerializeObject((object)new Search()
                //{
                //    siteCode = ((User)this.Session["Login_Details"]).siteCode
                //}).Replace(",\"lstCustomer\":null,\"lstCustomerClass\":null,\"lstTherapist\":null,\"lstConsultant\":null,\"lstCustomerType\":null", string.Empty));
                //cart.lstCustomer.AddRange((IEnumerable<Register>)registerList);
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                cart.lstStaff = new List<Staffs>();
                cart.lstStaff.Add(new Staffs()
                {
                    staffCode = string.Empty,
                    staffName = string.Empty
                });
                cart.lstStaff.AddRange((IEnumerable<Staffs>)app.bsms.api.Service.GetList<Staffs>("getStaffList"));
                cart.cart.salesStatus = "SA";
                if (cart.cart.transactionDetails.Where<Cart_Details>((Func<Cart_Details, bool>)(f => f.lineType == "TD")).Count<Cart_Details>() > 0)
                {
                    int count = cart.cart.transactionDetails.Count;
                    int num = cart.cart.transactionDetails.Where<Cart_Details>((Func<Cart_Details, bool>)(f => f.lineType == "TD")).Count<Cart_Details>();
                    cart.cart.salesType = count != num ? "Receipt" : "Receipt";
                }
                else
                {
                    if (cart.cart.depositAmount > 0)
                        cart.cart.salesType = "Receipt";
                    else cart.cart.salesType = "Non Sales";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (ActionResult)this.View((object)cart);
        }

        public ActionResult GetItem(string code, string type)
        {
            app.bsms.Models.Sales.Cart cart = new app.bsms.Models.Sales.Cart();
            try
            {
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                cart.lstHoldReasons = new List<ListItems>();
                cart.lstHoldReasons.Add(new ListItems()
                {
                    itemCode = string.Empty,
                    itemDesc = string.Empty
                });
                cart.lstHoldReasons.AddRange((IEnumerable<ListItems>)app.bsms.api.Service.GetList<ListItems>("holdItemReason"));
                app.bsms.api.Service.Parameters.Add("userId", ((User)this.Session["Login_Details"]).userID);
                if (this.Request.UrlReferrer.Query != null && this.Request.UrlReferrer.Query != string.Empty)
                    app.bsms.api.Service.Parameters.Add("cartToken", this.Request.UrlReferrer.Query);
                cart.cart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
                cart.cartDetails = cart.cart.transactionDetails.Where<Cart_Details>((Func<Cart_Details, bool>)(f => f.itemCode == code)).FirstOrDefault<Cart_Details>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (ActionResult)this.PartialView("_PartialHold", (object)cart);
        }

        public ActionResult GetCustomerModal(string customer)
        {
            var lstCustomer = new List<Register>();
            List<Register> registerList = app.bsms.api.Service.PostRead<Register>("SearchCustomer", JsonConvert.SerializeObject((object)new Search()
            //List<Register> registerList = app.bsms.api.Service.PostRead<Register>("listCustomer", JsonConvert.SerializeObject((object)new Search()
            {
                siteCode = ((User)this.Session["Login_Details"]).siteCode,
                customerName = customer
            }).Replace(",\"lstCustomer\":null,\"lstCustomerClass\":null,\"lstTherapist\":null,\"lstConsultant\":null,\"lstCustomerType\":null", string.Empty));
            lstCustomer.AddRange((IEnumerable<Register>)registerList);

            return this.PartialView("_SelectCustomerModal", lstCustomer);

            //Catelogue catelogue = new Catelogue();
            //app.bsms.api.Service.Parameters.Clear();
            //app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
            //app.bsms.api.Service.Parameters.Add("departmentCode", serviceID);
            //catelogue.lstServiceTypes = app.bsms.api.Service.GetList<ServiceType>("serviceType");
            //return this.PartialView("_PartialServiceType", catelogue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Staff(app.bsms.Models.Sales.Cart model)
        {
            app.bsms.Models.Sales.Cart cart = new app.bsms.Models.Sales.Cart();
            try
            {
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", model.cart.siteCode);
                app.bsms.api.Service.Parameters.Add("userId", ((User)this.Session["Login_Details"]).userID);
                if (model.cart.cartToken != null && model.cart.cartToken != string.Empty)
                    app.bsms.api.Service.Parameters.Add("cartToken", model.cart.cartToken);
                cart.cart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
                if (cart.cart.userID == null)
                {
                    string cartToken = model.cart.cartToken;
                    app.bsms.api.Service.Parameters.Clear();
                    app.bsms.api.Service.Parameters.Add("siteCode", model.cart.siteCode);
                    app.bsms.api.Service.Parameters.Add("userId", ((User)this.Session["Login_Details"]).userID);
                    cart.cart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
                    cart.cart.cartToken = cartToken;
                }
                if (cart.cart.transactionDetails.Where<Cart_Details>((Func<Cart_Details, bool>)(f => f.lineType == "TD")).Count<Cart_Details>() > 0)
                {
                    if (cart.cart.customerCode != model.cart.customerCode)
                    {
                        Alerts.body = Resources.Resources.Unsuccessful; // "Unsuccessful";
                        Alerts.ErrorMessage = Resources.Resources.ServiceNotBelong + " " + model.cart.customerCode; //  "Redeemed service does not belong to " + model.cart.customerCode;
                        this.TempData["Message"] = (object)Alerts.ErrorMessage;
                        return (ActionResult)this.Json((object)("/Cart/Index" + (model.cart.cartToken == null || !(model.cart.cartToken != string.Empty) ? (string)null : model.cart.cartToken)), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                    cart.cart.customerCode = model.cart.customerCode;
                if (model.cart.customerCode != null && model.cart.customerCode != string.Empty)
                {
                    Register customer = Utility.GetCustomer(cart.cart.customerCode, ((User)this.Session["Login_Details"]).siteCode);
                    cart.cart.customerName = customer.customerName;
                }
                List<SalesStaff> staffs = new List<SalesStaff>();
                cart.cart.staffCode = (string)null;
                cart.cart.staffName = (string)null;
                if (model.cartDetails.strStaffs != null)
                    staffs = JsonConvert.DeserializeObject<List<SalesStaff>>(model.cartDetails.strStaffs);
                if (staffs.Count<SalesStaff>() != 0)
                    cart.cart.transactionDetails.Where<Cart_Details>((Func<Cart_Details, bool>)(f =>
                   {
                       if (!(f.staffcode == string.Empty))
                           return f.staffcode == null;
                       return true;
                   })).Select<Cart_Details, Cart_Details>((Func<Cart_Details, Cart_Details>)(s => s)).Update<Cart_Details>((Extensions.Func<Cart_Details>)(f =>
        {
                       f.staffcode = (string)null;
                       f.staffName = (string)null;
                       f.salesStaffDetails = staffs;
                       f.holdItemQty = f.holdItemQty.IsNullOrZero() ? new int?(0) : f.holdItemQty;
                   }));
                app.bsms.api.Service.Parameters.Clear();
                JsonSerializerSettings settings = new JsonSerializerSettings()
                {
                    ContractResolver = (IContractResolver)new NullToEmptyStringResolver()
                };
                if (model.cart.cartToken != null && model.cart.cartToken != string.Empty)
                {
                    app.bsms.api.Service.Parameters.Add("cartToken", model.cart.cartToken);
                    app.bsms.api.Service.Put<Suspend_Cart>("itemCart", JsonConvert.SerializeObject((object)cart.cart, settings));
                }
                else
                    app.bsms.api.Service.Post<Temp_Cart>("itemCart", JsonConvert.SerializeObject((object)cart.cart, settings));
                return (ActionResult)this.RedirectToAction("Index", "Cart", (object)new
                {
                    id = (model.cart.cartToken == null || !(model.cart.cartToken != string.Empty) ? (string)null : model.cart.cartToken)
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SubmitButtonSelector(Name = "Pay")]
        public ActionResult Pay(app.bsms.Models.Sales.Cart model)
        {
            app.bsms.Models.Sales.Cart cart = new app.bsms.Models.Sales.Cart();
            try
            {
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", model.cart.siteCode);
                app.bsms.api.Service.Parameters.Add("userId", ((User)this.Session["Login_Details"]).userID);
                if (model.cart.cartToken != null && model.cart.cartToken != string.Empty)
                    app.bsms.api.Service.Parameters.Add("cartToken", model.cart.cartToken);
                cart.cart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");

                //Yoonus Opening this block
                /*
                //IEnumerable < new< List<SalesStaff>,int>> source = cart.cart.transactionDetails.Select(f => new
                IEnumerable < new<List<SalesStaff>,int>>  source = cart.cart.transactionDetails.Select(f => new
        {
          staffs = f.salesStaffDetails,
          staffCount = f.salesStaffDetails == null ? 0 : f.salesStaffDetails.Count
        });
        cart.cart.transactionDetails.Select(f => new
        {
          staffs = f.courseDetails,
          staffCount = f.courseDetails == null ? 0 : f.courseDetails.Count
        });
        if (source.Sum(f => f.staffCount) == 0)
        {
          Alerts.body = "Check out Unsuccessfull";
          Alerts.ErrorMessage = Resources.Resources.StaffDetailsRequired;
          this.TempData["Message"] = (object) Alerts.ErrorMessage;
          return (ActionResult) this.RedirectToAction("Index", "Cart", (object) new
          {
            id = (model.cart.cartToken == null || !(model.cart.cartToken != string.Empty) ? (string) null : model.cart.cartToken)
          });
        }
                //Yoonus Opening this block
                */

                cart.cart.salesStatus = "SA";
                if (cart.cart.transactionDetails.Where<Cart_Details>((Func<Cart_Details, bool>)(f => f.lineType == "TD")).Count<Cart_Details>() > 0)
                {
                    int count = cart.cart.transactionDetails.Count;
                    int num = cart.cart.transactionDetails.Where<Cart_Details>((Func<Cart_Details, bool>)(f => f.lineType == "TD")).Count<Cart_Details>();
                    cart.cart.salesType = count != num ? "Receipt" : "Redeem Service";
                }
                else
                {
                    if (cart.cart.depositAmount > 0)
                        cart.cart.salesType = "Receipt";
                    else cart.cart.salesType = "Non Sales";
                }

                cart.cart.macAddress = this.Request.ServerVariables["REMOTE_ADDR"];
                cart.cart.userID = ((User)this.Session["Login_Details"]).userID;
                cart.cart.siteCode = ((User)this.Session["Login_Details"]).siteCode;
                cart.cart.staffCode = (string)null;
                cart.cart.staffName = (string)null;
                cart.cart.customerCode = model.cart.customerCode;
                if (model.cart.customerCode != null && model.cart.customerCode != string.Empty)
                {
                    Register customer = Utility.GetCustomer(cart.cart.customerCode, ((User)this.Session["Login_Details"]).siteCode);
                    cart.cart.customerName = customer.customerName;
                }
                int num1 = 1;
                foreach (Cart_Details transactionDetail in cart.cart.transactionDetails)
                {
                    Cart_Details item = transactionDetail;
                    //Yoonus Adding
                    if (item.salesStaffDetails == null)
                    {
                        Alerts.body = "No staff"; //Resources.Resources.NoStaff; // "Unsuccessful";
                        Alerts.ErrorMessage = "Staff is not selected"; //Resources.Resources.ServiceNotBelong + " " + model.cart.customerCode; //  "Redeemed service does not belong to " + model.cart.customerCode;
                        this.TempData["Message"] = (object)Alerts.ErrorMessage;
                        return (ActionResult)this.RedirectToAction("Index", "Cart", (object)new
                        {
                            id = (model.cart.cartToken == null || !(model.cart.cartToken != string.Empty) ? (string)null : model.cart.cartToken)
                        });
                    }
                    //Yoonus Adding
                    item.lineNumber = num1;
                    item.lineStatus = "SA";
                    item.macAddress = this.Request.ServerVariables["REMOTE_ADDR"];
                    item.transactionAmount = item.salesAmount;
                    if (item.courseDetails != null)
                        //YoonusAddinghere
                        item.courseDetails.Update<Course_Details>((Extensions.Func<Course_Details>)(f =>
                       {
                           Course_Details courseDetails1 = f;
                           courseDetails1.itemCode = courseDetails1.itemCode == null || f.itemCode == string.Empty ? item.itemCode : f.itemCode;
                           Course_Details courseDetails2 = f;
                           courseDetails2.itemDescription = courseDetails2.itemDescription == null || f.itemDescription == string.Empty ? item.itemName : f.itemDescription;
                       }));
                    item.depositAmount = item.depositAmount.IsNullOrZero() ? new Decimal?(item.promoPrice) : item.depositAmount;
                    item.holdItemQty = item.holdItemQty.IsNullOrZero() ? new int?(0) : item.holdItemQty;
                    ++num1;
                }
                cart.cart.totalQuantity = cart.cart.transactionDetails.Sum<Cart_Details>((Func<Cart_Details, int>)(f => f.itemQty));
                cart.cart.totalAmount = cart.cart.transactionDetails.Sum<Cart_Details>((Func<Cart_Details, Decimal>)(f => f.itemAmount));
                cart.cart.totalDiscount = cart.cart.transactionDetails.Sum<Cart_Details>((Func<Cart_Details, Decimal>)(f => f.unitDiscount));
                cart.cart.totalGST = cart.cart.transactionDetails.Sum<Cart_Details>((Func<Cart_Details, Decimal>)(f => f.gstAmountCollected));
                cart.cart.depositAmount = cart.cart.transactionDetails.Sum<Cart_Details>((Func<Cart_Details, Decimal>)(f => (!f.depositAmount.HasValue ? new Decimal?(new Decimal()) : f.depositAmount).Value));
                cart.cart.salesDate = DateTime.Now.Date;
                cart.cart.salesTime = DateTime.Now;
                cart.cart.payDetails = new List<app.bsms.Models.Sales.Post.Payment_Details>();
                app.bsms.api.Service.Parameters.Clear();
                JsonSerializerSettings settings = new JsonSerializerSettings()
                {
                    ContractResolver = (IContractResolver)new NullToEmptyStringResolver()
                };
                if (model.cart.cartToken != null && model.cart.cartToken != string.Empty)
                {
                    app.bsms.api.Service.Parameters.Add("cartToken", model.cart.cartToken);
                    app.bsms.api.Service.Put<Suspend_Cart>("itemCart", JsonConvert.SerializeObject((object)cart.cart, settings));
                }
                else
                    app.bsms.api.Service.Post<Temp_Cart>("itemCart", JsonConvert.SerializeObject((object)cart.cart, settings));
                if (cart.cart.totalAmount == Decimal.Zero)
                {
                    cart.sales = JsonConvert.DeserializeObject<app.bsms.Models.Sales.Post.Cart>(JsonConvert.SerializeObject((object)cart.cart, settings));
                    cart.sales = app.bsms.api.Service.Post<app.bsms.Models.Sales.Post.Cart>("postSales", JsonConvert.SerializeObject((object)cart.sales, settings));
                    if (cart.sales == null)
                    {
                        Alerts.body = Resources.Resources.Unsuccessful; //"Unsuccessful";
                        this.TempData["Message"] = (object)Alerts.ErrorMessage;
                    }
                    else
                    {
                        app.bsms.api.Service.Parameters.Clear();
                        app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                        app.bsms.api.Service.Parameters.Add("userId", ((User)this.Session["Login_Details"]).userID);
                        if (model.cart.cartToken != null && model.cart.cartToken != string.Empty)
                            app.bsms.api.Service.Parameters.Add("cartToken", model.cart.cartToken);
                        app.bsms.api.Service.Delete("itemCart");
                        return (ActionResult)this.RedirectToAction("Invoice", "Payment", (object)new
                        {
                            id = cart.sales.transactionNumber,
                            id2 = cart.sales.invoiceNumber
                        });
                    }
                }
                return (ActionResult)this.RedirectToAction("Index", "Payment", (object)new
                {
                    id = (model.cart.cartToken == null || !(model.cart.cartToken != string.Empty) ? (string)null : model.cart.cartToken)
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Suspend(app.bsms.Models.Sales.Cart model)
        {
            app.bsms.Models.Sales.Cart cart = new app.bsms.Models.Sales.Cart();
            try
            {
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", model.cart.siteCode);
                app.bsms.api.Service.Parameters.Add("userId", ((User)this.Session["Login_Details"]).userID);
                if (model.cart.cartToken != null && model.cart.cartToken != string.Empty)
                    app.bsms.api.Service.Parameters.Add("cartToken", model.cart.cartToken);
                cart.cart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
                cart.cart.cartToken = model.cart.cartToken == null || model.cart.cartToken == string.Empty ? Utility.GenerateToken(20) : model.cart.cartToken;
                if (cart.cart.userID == null)
                {
                    string cartToken = model.cart.cartToken;
                    app.bsms.api.Service.Parameters.Clear();
                    app.bsms.api.Service.Parameters.Add("siteCode", model.cart.siteCode);
                    app.bsms.api.Service.Parameters.Add("userId", ((User)this.Session["Login_Details"]).userID);
                    cart.cart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
                    cart.cart.cartToken = cartToken;
                }
                else
                    cart.cart.cartToken = model.cart.cartToken == null || model.cart.cartToken == string.Empty ? Utility.GenerateToken(20) : model.cart.cartToken;
                cart.cart.salesStatus = "SA";
                if (cart.cart.transactionDetails.Where<Cart_Details>((Func<Cart_Details, bool>)(f => f.lineType == "TD")).Count<Cart_Details>() > 0)
                {
                    int count = cart.cart.transactionDetails.Count;
                    int num = cart.cart.transactionDetails.Where<Cart_Details>((Func<Cart_Details, bool>)(f => f.lineType == "TD")).Count<Cart_Details>();
                    cart.cart.salesType = count != num ? "Receipt" : "Redeem Service";
                }
                else
                {
                    if (cart.cart.depositAmount > 0)
                        cart.cart.salesType = "Receipt";
                    else cart.cart.salesType = "Non Sales";
                }
                cart.cart.macAddress = this.Request.ServerVariables["REMOTE_ADDR"];
                cart.cart.userID = ((User)this.Session["Login_Details"]).userID;
                cart.cart.siteCode = ((User)this.Session["Login_Details"]).siteCode;
                cart.cart.staffCode = (string)null;
                cart.cart.staffName = (string)null;
                cart.cart.customerCode = model.cart.customerCode;
                if (model.cart.customerCode != null && model.cart.customerCode != string.Empty)
                {
                    Register customer = Utility.GetCustomer(cart.cart.customerCode, ((User)this.Session["Login_Details"]).siteCode);
                    cart.cart.customerName = customer.customerName;
                }
                int num1 = 1;
                foreach (Cart_Details transactionDetail in cart.cart.transactionDetails)
                {
                    transactionDetail.lineNumber = num1;
                    transactionDetail.lineStatus = "SA";
                    transactionDetail.macAddress = this.Request.ServerVariables["REMOTE_ADDR"];
                    Cart_Details cartDetails = transactionDetail;
                    cartDetails.transactionAmount = cartDetails.salesAmount;
                    transactionDetail.holdItemQty = transactionDetail.holdItemQty.IsNullOrZero() ? new int?(0) : transactionDetail.holdItemQty;
                    ++num1;
                }
                cart.cart.totalQuantity = cart.cart.transactionDetails.Sum<Cart_Details>((Func<Cart_Details, int>)(f => f.itemQty));
                cart.cart.totalAmount = cart.cart.transactionDetails.Sum<Cart_Details>((Func<Cart_Details, Decimal>)(f => f.itemAmount));
                cart.cart.totalDiscount = cart.cart.transactionDetails.Sum<Cart_Details>((Func<Cart_Details, Decimal>)(f => f.unitDiscount));
                cart.cart.totalGST = cart.cart.transactionDetails.Sum<Cart_Details>((Func<Cart_Details, Decimal>)(f => f.gstAmountCollected));
                cart.cart.depositAmount = cart.cart.transactionDetails.Sum<Cart_Details>((Func<Cart_Details, Decimal>)(f => (!f.depositAmount.HasValue ? new Decimal?(new Decimal()) : f.depositAmount).Value));
                cart.cart.salesDate = DateTime.Now.Date;
                cart.cart.salesTime = DateTime.Now;
                cart.cart.payDetails = new List<app.bsms.Models.Sales.Post.Payment_Details>();
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("cartToken", model.cart.cartToken == null ? cart.cart.cartToken : model.cart.cartToken);
                JsonSerializerSettings settings = new JsonSerializerSettings()
                {
                    ContractResolver = (IContractResolver)new NullToEmptyStringResolver()
                };
                app.bsms.api.Service.Put<Suspend_Cart>("itemCart", JsonConvert.SerializeObject((object)cart.cart, settings));
                if (model.cart.cartToken == null)
                {
                    app.bsms.api.Service.Parameters.Clear();
                    app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                    app.bsms.api.Service.Parameters.Add("userId", ((User)this.Session["Login_Details"]).userID);
                    app.bsms.api.Service.Delete("itemCart");
                }
                return (ActionResult)this.RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Hold(app.bsms.Models.Sales.Cart model)
        {
            app.bsms.Models.Sales.Cart cart = new app.bsms.Models.Sales.Cart();
            try
            {
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", model.cart.siteCode);
                app.bsms.api.Service.Parameters.Add("userId", ((User)this.Session["Login_Details"]).userID);
                if (model.cart.cartToken != null && model.cart.cartToken != string.Empty)
                    app.bsms.api.Service.Parameters.Add("cartToken", model.cart.cartToken);
                cart.cart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
                cart.cart.transactionDetails.Where<Cart_Details>((Func<Cart_Details, bool>)(f =>
               {
                   if (f.itemCode == model.cartDetails.itemCode)
                       return f.itemName == model.cartDetails.itemName;
                   return false;
               })).Select<Cart_Details, Cart_Details>((Func<Cart_Details, Cart_Details>)(s => s)).Update<Cart_Details>((Extensions.Func<Cart_Details>)(f =>
      {
                   f.isHoldItem = model.cartDetails.isHoldItem;
                   if (model.cartDetails.isHoldItem)
                   {
                       f.holdItemQty = model.cartDetails.holdItemQty;
                       f.holdItemType = model.cartDetails.holdItemType;
                   }
                   else
                   {
                       f.holdItemQty = new int?(0);
                       f.holdItemType = (string)null;
                   }
               }));
                app.bsms.api.Service.Parameters.Clear();
                JsonSerializerSettings settings = new JsonSerializerSettings()
                {
                    ContractResolver = (IContractResolver)new NullToEmptyStringResolver()
                };
                if (model.cart.cartToken != null && model.cart.cartToken != string.Empty)
                {
                    app.bsms.api.Service.Parameters.Add("cartToken", model.cart.cartToken);
                    app.bsms.api.Service.Put<Suspend_Cart>("itemCart", JsonConvert.SerializeObject((object)cart.cart, settings));
                }
                else
                    app.bsms.api.Service.Post<Temp_Cart>("itemCart", JsonConvert.SerializeObject((object)cart.cart, settings));
                return (ActionResult)this.RedirectToAction("Index", "Cart", (object)new
                {
                    id = (model.cart.cartToken == null || !(model.cart.cartToken != string.Empty) ? (string)null : model.cart.cartToken)
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SubmitButtonSelector(Name = "Delete")]
        public ActionResult Delete(app.bsms.Models.Sales.Cart model)
        {
            app.bsms.Models.Sales.Cart cart = new app.bsms.Models.Sales.Cart();
            try
            {
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", model.cart.siteCode);
                app.bsms.api.Service.Parameters.Add("userId", ((User)this.Session["Login_Details"]).userID);
                if (model.cart.cartToken != null && model.cart.cartToken != string.Empty)
                    app.bsms.api.Service.Parameters.Add("cartToken", model.cart.cartToken);
                app.bsms.api.Service.Delete("itemCart");
                return (ActionResult)this.RedirectToAction("Index", "Cart");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult DeleteItem(string code, string type)
        {
            try
            {
                if (!string.IsNullOrEmpty(code))
                {
                    Temp_Cart tempCart1 = new Temp_Cart();
                    app.bsms.api.Service.Parameters.Clear();
                    app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                    app.bsms.api.Service.Parameters.Add("userId", ((User)this.Session["Login_Details"]).userID);
                    if (type != null && type != string.Empty)
                        app.bsms.api.Service.Parameters.Add("cartToken", type);
                    Temp_Cart tempCart2 = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
                    tempCart2.userID = ((User)this.Session["Login_Details"]).userID;
                    tempCart2.siteCode = ((User)this.Session["Login_Details"]).siteCode;
                    tempCart2.staffCode = ((User)this.Session["Login_Details"]).staffCode;
                    tempCart2.macAddress = this.Request.ServerVariables["REMOTE_ADDR"];
                    tempCart2.transactionDetails = tempCart2.transactionDetails.Where<Cart_Details>((Func<Cart_Details, bool>)(f => f.itemCode != code)).ToList<Cart_Details>();
                    tempCart2.totalQuantity = tempCart2.transactionDetails.Sum<Cart_Details>((Func<Cart_Details, int>)(f => f.itemQty));
                    tempCart2.totalAmount = tempCart2.transactionDetails.Sum<Cart_Details>((Func<Cart_Details, Decimal>)(f => f.itemAmount));
                    tempCart2.totalDiscount = tempCart2.transactionDetails.Sum<Cart_Details>((Func<Cart_Details, Decimal>)(f => f.unitDiscount));
                    tempCart2.depositAmount = tempCart2.transactionDetails.Sum<Cart_Details>((Func<Cart_Details, Decimal>)(f => (!f.depositAmount.HasValue ? new Decimal?(new Decimal()) : f.depositAmount).Value));
                    tempCart2.payDetails = new List<app.bsms.Models.Sales.Post.Payment_Details>();
                    app.bsms.api.Service.Parameters.Clear();
                    JsonSerializerSettings settings = new JsonSerializerSettings()
                    {
                        ContractResolver = (IContractResolver)new NullToEmptyStringResolver()
                    };
                    if (type != null && type != string.Empty)
                    {
                        app.bsms.api.Service.Parameters.Add("cartToken", type);
                        app.bsms.api.Service.Put<Suspend_Cart>("itemCart", JsonConvert.SerializeObject((object)tempCart2, settings));
                    }
                    else
                        app.bsms.api.Service.Post<app.bsms.Models.Sales.Post.Cart>("itemCart", JsonConvert.SerializeObject((object)tempCart2, settings));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return (ActionResult) this.Json((object) this.Url.Action("Index", "Cart"));
            //Yoonus Changed to this
            return (ActionResult)this.Json((object)this.Url.Action("Index", "Cart"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult RefreshCartDetails()
        {
            string empty = string.Empty;
            app.bsms.api.Service.Parameters.Clear();
            app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
            app.bsms.api.Service.Parameters.Add("userId", ((User)this.Session["Login_Details"]).userID);
            return this.Json((object)JsonConvert.SerializeObject((object)app.bsms.api.Service.Get<app.bsms.Models.Sales.Post.Cart>("itemCart").transactionDetails), JsonRequestBehavior.AllowGet);
        }

        public JsonResult RecallCartDetails(string transactionNumber)
        {
            string empty = string.Empty;
            app.bsms.api.Service.Parameters.Clear();
            app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
            app.bsms.api.Service.Parameters.Add("transactionNumber", transactionNumber);
            return this.Json((object)JsonConvert.SerializeObject((object)app.bsms.api.Service.Get<app.bsms.Models.Sales.Post.Cart>("postSales").transactionDetails), JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public ActionResult SuspendedCartDetails()
        {
            string empty = string.Empty;
            app.bsms.api.Service.Parameters.Clear();
            app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
            app.bsms.api.Service.Parameters.Add("userId", ((User)this.Session["Login_Details"]).userID);
            return (ActionResult)this.PartialView("~/Views/Shared/_MenuPartial.cshtml", (object)new app.bsms.Models.Sales.Cart()
            {
                lstSuspend_Cart = app.bsms.api.Service.GetList<Suspend_Cart>("getSuspendedCartList")
            }.lstSuspend_Cart);
        }

        [HttpGet]
        public ActionResult Item(string id, string id2)
        {
            app.bsms.Models.Sales.Cart cart1 = new app.bsms.Models.Sales.Cart();
            try
            {

                /* Yoonus
                        // ISSUE: reference to a compiler-generated field
                        if (CartController.\u003C\u003Eo__10.\u003C\u003Ep__0 == null)
                        {
                          // ISSUE: reference to a compiler-generated field
                          CartController.\u003C\u003Eo__10.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Title", typeof (CartController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                          {
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                          }));
                        }
                        // ISSUE: reference to a compiler-generated field
                        // ISSUE: reference to a compiler-generated field
                               object obj = CartController.<> o__10.<> p__0.Target((CallSite)CartController.\u003C\u003Eo__10.\u003C\u003Ep__0, this.ViewBag, id);
                */

                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                cart1.lstFOCs = new List<ListItems>();
                cart1.lstFOCs.Add(new ListItems()
                {
                    itemCode = string.Empty,
                    itemDesc = string.Empty
                });
                cart1.lstFOCs.AddRange((IEnumerable<ListItems>)app.bsms.api.Service.GetList<ListItems>("focReason"));
                app.bsms.api.Service.Parameters.Add("userId", ((User)this.Session["Login_Details"]).userID);
                if (id2 != null && id2 != string.Empty)
                    app.bsms.api.Service.Parameters.Add("cartToken", id2);
                cart1.cart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
                cart1.cartDetails = cart1.cart.transactionDetails.Where<Cart_Details>((Func<Cart_Details, bool>)(f => f.itemCode == id)).SingleOrDefault<Cart_Details>();
                if (cart1.cartDetails.discountDetails != null)
                {
                    cart1.cartDetails.strDiscounts = JsonConvert.SerializeObject((object)cart1.cartDetails.discountDetails);
                    cart1.cartDetails.strDiscounts = cart1.cartDetails.strDiscounts == "[]" ? (string)null : cart1.cartDetails.strDiscounts;
                }
                else
                    cart1.cartDetails.strDiscounts = (string)null;
                if (cart1.cartDetails.salesStaffDetails != null)
                {
                    cart1.cartDetails.strStaffs = JsonConvert.SerializeObject((object)cart1.cartDetails.salesStaffDetails);
                    cart1.cartDetails.strStaffs = cart1.cartDetails.strStaffs == "[]" ? (string)null : cart1.cartDetails.strStaffs;
                }
                else
                    cart1.cartDetails.strStaffs = (string)null;
                if (cart1.cartDetails.lineType != "TD")
                    cart1.cartDetails.itemAmount = cart1.cartDetails.unitPrice * (Decimal)cart1.cartDetails.itemQty;
                cart1.cartDetails.depositAmount = new Decimal?();
                if (cart1.cartDetails.courseDetails != null)
                {
                    if (cart1.cartDetails.courseDetails.Count == 0)
                    {
                        app.bsms.Models.Sales.Cart cart2 = cart1;
                        cart2.treatmentDate = !cart2.treatmentDate.HasValue ? new DateTime?(DateTime.Now.Date) : cart1.treatmentDate;
                        app.bsms.Models.Sales.Cart cart3 = cart1;
                        cart3.interval = cart3.interval == 0 ? 7 : cart1.interval;
                        cart1.totalTreatments = new int?(1);
                        cart1.cartDetails.FOCQuantity = new int?(0);
                        cart1.cartDetails.autoProportionate = false;
                        cart1.expiryDate = new DateTime?(DateTime.Now);
                        cart1.treatmentLimit = new int?(0);
                    }
                    else
                    {
                        Course_Details courseDetails1 = cart1.cartDetails.courseDetails.FirstOrDefault<Course_Details>();
                        if (courseDetails1.courseType == "N")
                            cart1.totalTreatments = new int?(cart1.cartDetails.courseDetails.Where<Course_Details>((Func<Course_Details, bool>)(f =>
                           {
                               bool? isFoc = f.isFOC;
                               bool flag = false;
                               if (isFoc.GetValueOrDefault() != flag)
                                   return false;
                               return isFoc.HasValue;
                           })).Count<Course_Details>());
                        cart1.treatmentDate = new DateTime?(courseDetails1.treatmentDate);
                        app.bsms.Models.Sales.Cart cart2 = cart1;
                        DateTime? nullable = courseDetails1.nextAppt;
                        int num;
                        if (nullable.HasValue)
                        {
                            nullable = courseDetails1.nextAppt;
                            num = (int)(nullable.Value - courseDetails1.treatmentDate).TotalDays;
                        }
                        else
                            num = 7;
                        cart2.interval = num;
                        cart1.courseType = courseDetails1.courseType;
                        app.bsms.Models.Sales.Cart cart3 = cart1;
                        nullable = cart3.cartDetails.expiryDate;
                        cart3.expiryDate = !nullable.HasValue ? new DateTime?(DateTime.Now) : cart1.cartDetails.expiryDate;
                        app.bsms.Models.Sales.Cart cart4 = cart1;
                        cart4.treatmentLimit = cart4.cartDetails.treatmentLimit;
                        if (courseDetails1.treatmentStaffDetails != null)
                        {
                            cart1.cartDetails.strTreatmentStaffs = JsonConvert.SerializeObject((object)courseDetails1.treatmentStaffDetails);
                            cart1.cartDetails.strTreatmentStaffs = cart1.cartDetails.strTreatmentStaffs == "[]" ? (string)null : cart1.cartDetails.strTreatmentStaffs;
                        }
                        else
                            cart1.cartDetails.strTreatmentStaffs = (string)null;
                        cart1.cartDetails.FOCQuantity = new int?(cart1.cartDetails.courseDetails.Where<Course_Details>((Func<Course_Details, bool>)(f =>
                       {
                           bool? isFoc = f.isFOC;
                           bool flag = true;
                           if (isFoc.GetValueOrDefault() != flag)
                               return false;
                           return isFoc.HasValue;
                       })).Count<Course_Details>());
                        Course_Details courseDetails2 = cart1.cartDetails.courseDetails.Where<Course_Details>((Func<Course_Details, bool>)(f =>
                       {
                           bool? isFoc = f.isFOC;
                           bool flag = true;
                           if (isFoc.GetValueOrDefault() != flag)
                               return false;
                           return isFoc.HasValue;
                       })).FirstOrDefault<Course_Details>();
                        if (courseDetails2 != null)
                            cart1.cartDetails.autoProportionate = courseDetails2.treatmentUnitPrice > Decimal.Zero;
                    }
                }
                else
                {
                    app.bsms.Models.Sales.Cart cart2 = cart1;
                    cart2.treatmentDate = !cart2.treatmentDate.HasValue ? new DateTime?(DateTime.Now.Date) : cart1.treatmentDate;
                    app.bsms.Models.Sales.Cart cart3 = cart1;
                    cart3.interval = cart3.interval == 0 ? 7 : cart1.interval;
                    cart1.cartDetails.FOCQuantity = new int?(0);
                    cart1.cartDetails.autoProportionate = false;
                    cart1.expiryDate = new DateTime?(DateTime.Now);
                    cart1.treatmentLimit = new int?(0);
                }
                app.bsms.api.Service.Parameters.Clear();
                cart1.lstDiscountReason = new List<ReasonCodes>();
                cart1.lstDiscountReason.Add(new ReasonCodes()
                {
                    reasonCode = string.Empty,
                    reasonDesc = string.Empty
                });
                cart1.lstDiscountReason.AddRange((IEnumerable<ReasonCodes>)app.bsms.api.Service.GetList<ReasonCodes>("getDiscountReasonList"));
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                cart1.lstDiscountUser = new List<Staffs>();
                cart1.lstDiscountUser.Add(new Staffs()
                {
                    staffCode = string.Empty,
                    staffName = string.Empty
                });
                cart1.lstDiscountUser.AddRange((IEnumerable<Staffs>)app.bsms.api.Service.GetList<Staffs>("getStaffList"));
                cart1.lstCourse_Settings = new List<Course_Settings>();
                cart1.lstCourse_Settings.AddRange((IEnumerable<Course_Settings>)app.bsms.api.Service.GetList<Course_Settings>("courseSettings"));
                app.bsms.api.Service.Parameters.Add("itemCode", cart1.cartDetails.itemCode);
                cart1.lstPackages = new List<PackageDetails>();
                cart1.lstPackages.AddRange((IEnumerable<PackageDetails>)app.bsms.api.Service.GetList<PackageDetails>("packageContent"));
                cart1.lstFlexiDetails = new List<Item>();
                cart1.lstFlexiDetails.AddRange((IEnumerable<Item>)app.bsms.api.Service.GetList<Item>("getFlexiDetails"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (ActionResult)this.View((object)cart1);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Item(app.bsms.Models.Sales.Cart model)
        {
            app.bsms.Models.Sales.Cart cart1 = new app.bsms.Models.Sales.Cart();
            try
            {
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                app.bsms.api.Service.Parameters.Add("itemCode", model.cartDetails.itemCode);
                //Item item = app.bsms.api.Service.Get<Item>("getItemDetails");
                //Yoonus Changing to getItemDetail
                Item item = app.bsms.api.Service.Get<Item>("getItemDetail");
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                cart1.lstStaff = app.bsms.api.Service.GetList<Staffs>("getStaffList");
                app.bsms.api.Service.Parameters.Add("userId", ((User)this.Session["Login_Details"]).userID);
                if (model.cart.cartToken != null && model.cart.cartToken != string.Empty)
                    app.bsms.api.Service.Parameters.Add("cartToken", model.cart.cartToken);
                cart1.cart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
                List<Discounts> discs = new List<Discounts>();
                List<SalesStaff> staffs = new List<SalesStaff>();
                List<TreatmentStaff> treatmentStaffs = new List<TreatmentStaff>();
                if (model.cartDetails.strDiscounts != null)
                    discs = JsonConvert.DeserializeObject<List<Discounts>>(model.cartDetails.strDiscounts);
                if (model.cartDetails.strStaffs != null)
                    staffs = JsonConvert.DeserializeObject<List<SalesStaff>>(model.cartDetails.strStaffs);
                if (model.cartDetails.strTreatmentStaffs != null)
                    treatmentStaffs = JsonConvert.DeserializeObject<List<TreatmentStaff>>(model.cartDetails.strTreatmentStaffs);
                cart1.cart.transactionDetails.Where<Cart_Details>((Func<Cart_Details, bool>)(f =>
               {
                   if (f.itemCode == model.cartDetails.itemCode)
                       return f.itemName == model.cartDetails.itemName;
                   return false;
               })).Select<Cart_Details, Cart_Details>((Func<Cart_Details, Cart_Details>)(s => s)).Update<Cart_Details>((Extensions.Func<Cart_Details>)(f =>
      {
                   f.staffcode = (string)null;
                   f.staffName = (string)null;
                   f.itemCode = model.cartDetails.itemCode;
                   f.itemName = model.cartDetails.itemName;
                   f.itemUOM = model.cartDetails.itemUOM;
                   f.itemQty = model.cartDetails.itemQty;
                   f.unitPrice = model.isAmountChanged ? model.cartDetails.itemAmount / (Decimal)model.cartDetails.itemQty : model.cartDetails.unitPrice;
                   f.itemAmount = model.cartDetails.itemAmount;
                   f.unitDiscount = discs.Sum<Discounts>((Func<Discounts, Decimal>)(d => d.discountAmount));
                   if (f.lineType == "COURSE")
                   {
                       if (model.courseType == "N")
                       {
                           f.itemQty = model.totalTreatments.Value;
                           f.totalTreatments = model.totalTreatments;
                           f.unitPrice = model.isAmountChanged ? f.itemAmount / (Decimal)f.itemQty : f.unitPrice;
                           f.promoPrice = model.isAmountChanged ? f.itemAmount - f.unitDiscount : (Decimal)f.totalTreatments.Value * f.unitPrice - f.unitDiscount;
                       }
                       else
                           f.promoPrice = model.isAmountChanged ? f.itemAmount - f.unitDiscount : model.cartDetails.unitPrice * (Decimal)model.cartDetails.itemQty - f.unitDiscount;
                       Cart_Details cartDetails1 = f;
                       cartDetails1.itemAmount = cartDetails1.promoPrice;
                       Cart_Details cartDetails2 = f;
                       cartDetails2.salesAmount = cartDetails2.promoPrice;
                       f.depositAmount = model.cartDetails.depositAmount.IsNullOrZero() ? new Decimal?(f.promoPrice) : model.cartDetails.depositAmount;
                   }
                   else if (f.lineType == "VOUCHER" && item.brandName == "NON SALES VOUCHER")
                   {
                       f.promoPrice = Decimal.Zero;
                       f.itemAmount = Decimal.Zero;
                       f.salesAmount = Decimal.Zero;
                       f.depositAmount = new Decimal?(new Decimal());
                   }
                   else if (f.lineType == "TD")
                   {
                       f.promoPrice = model.cartDetails.itemAmount - f.unitDiscount;
                       Cart_Details cartDetails1 = f;
                       cartDetails1.itemAmount = cartDetails1.promoPrice;
                       Cart_Details cartDetails2 = f;
                       cartDetails2.salesAmount = cartDetails2.promoPrice;
                       f.depositAmount = model.cartDetails.depositAmount.IsNullOrZero() ? new Decimal?(f.promoPrice) : model.cartDetails.depositAmount;
                   }
                   else
                   {
                       f.promoPrice = model.cartDetails.isFOC ? Decimal.Zero : model.cartDetails.unitPrice * (Decimal)model.cartDetails.itemQty - f.unitDiscount;
                       f.itemAmount = model.cartDetails.isFOC ? Decimal.Zero : f.promoPrice;
                       if (f.lineType != "TD")
                       {
                           f.salesAmount = model.cartDetails.isFOC ? Decimal.Zero : f.promoPrice;
                           f.depositAmount = model.cartDetails.isFOC ? new Decimal?(new Decimal()) : (model.cartDetails.depositAmount.IsNullOrZero() ? new Decimal?(f.promoPrice) : model.cartDetails.depositAmount);
                       }
                   }
                   if (f.lineType != "TD")
                   {
                       f.discountUser = model.cartDetails.discountUser;
                       f.discountPercent = discs.Sum<Discounts>((Func<Discounts, Decimal>)(p => p.discountPercentage));
                       f.discountUser = model.cartDetails.discountUser;
                       f.discountDetails = discs;
                   }
                   f.salesStaffDetails = staffs;
                   f.isFOC = model.cartDetails.isFOC;
                   f.FOCReason = model.cartDetails.FOCReason;
                   f.itemRemarks = model.cartDetails.itemRemarks;
                   f.referenceTreatmentCode = model.cartDetails.referenceTreatmentCode;
            //Yoonus
            f.referenceTransactionNumber = model.cartDetails.referenceTransactionNumber;
                   f.topupBalance = model.cartDetails.topupBalance;
                   f.topupOutstanding = model.cartDetails.topupOutstanding;
            //Yoonus
            if (f.lineType == "COURSE")
                   {
                       f.isFirstTreatmentDone = model.cartDetails.isFirstTreatmentDone;
                       f.courseDetails = new List<Course_Details>();
                       Decimal num1 = new Decimal();
                       int? nullable1;
                       DateTime dateTime;
                       DateTime? treatmentDate;
                       if (model.courseType == "N")
                       {
                           f.itemQty = model.totalTreatments.Value;
                           f.totalTreatments = model.totalTreatments;
                           if (model.cartDetails.autoProportionate)
                           {
                               num1 = f.itemAmount / (Decimal)(model.totalTreatments.Value + model.cartDetails.FOCQuantity.Value);
                           }
                           else
                           {
                               Decimal itemAmount = f.itemAmount;
                               nullable1 = model.totalTreatments;
                               Decimal num2 = (Decimal)nullable1.Value;
                               num1 = itemAmount / num2;
                           }
                           int num3 = 0;
                           while (true)
                           {
                               int num2 = num3;
                               nullable1 = model.totalTreatments;
                               int valueOrDefault = nullable1.GetValueOrDefault();
                               if ((num2 < valueOrDefault ? (nullable1.HasValue ? 1 : 0) : 0) != 0)
                               {
                                   Course_Details courseDetails1 = new Course_Details();
                                   courseDetails1.treatmentNumber = num3 + 1;
                                   courseDetails1.itemCode = f.itemCode;
                                   courseDetails1.itemDescription = f.itemName;
                                   courseDetails1.treatmentUnitPrice = num1;
                                   courseDetails1.courseType = model.courseType;
                                   courseDetails1.treatmentStatus = num3 + 1 == 1 ? (model.cartDetails.isFirstTreatmentDone ? "Done" : "Open") : "Open";
                                   courseDetails1.treatmentDate = model.treatmentDate.Value;
                                   dateTime = model.treatmentDate.Value;
                                   courseDetails1.nextAppt = new DateTime?(dateTime.AddDays((double)model.interval));
                                   courseDetails1.isFOC = new bool?(false);
                                   Course_Details courseDetails2 = courseDetails1;
                                   f.courseDetails.Add(courseDetails2);
                                   app.bsms.Models.Sales.Cart cart2 = model;
                                   dateTime = model.treatmentDate.Value;
                                   DateTime? nullable2 = new DateTime?(dateTime.AddDays((double)model.interval));
                                   cart2.treatmentDate = nullable2;
                                   ++num3;
                               }
                               else
                                   break;
                           }
                           if (f.isFirstTreatmentDone)
                           {
                               Course_Details courseDetails = f.courseDetails.OrderBy<Course_Details, int>((Func<Course_Details, int>)(o => o.treatmentNumber)).FirstOrDefault<Course_Details>();
                               if (courseDetails != null)
                               {
                                   courseDetails.treatmentStaffDetails = new List<TreatmentStaff>();
                                   courseDetails.treatmentStaffDetails = treatmentStaffs;
                                   if (item != null)
                                       courseDetails.treatmentStaffDetails.Update<TreatmentStaff>((Extensions.Func<TreatmentStaff>)(s =>
                               {
                               s.shareAmount = Decimal.Zero;
                               s.workPoint1 = item.workPoints;
                           }));
                               }
                           }
                       }
                       else if (model.courseType == "FFi")
                       {
                           f.expiryDate = model.expiryDate;
                           f.treatmentLimit = model.treatmentLimit;
                           app.bsms.api.Service.Parameters.Clear();
                           app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                           app.bsms.api.Service.Parameters.Add("itemCode", model.cartDetails.subItemCode);
                    //Item obj = app.bsms.api.Service.Get<Item>("getItemDetails");
                    //Yoonus changing getItemDetail
                    Item obj = app.bsms.api.Service.Get<Item>("getItemDetail");
                           if (model.cartDetails.isFirstTreatmentDone)
                           {
                               Course_Details courseDetails = new Course_Details()
                               {
                                   treatmentNumber = 1,
                                   itemCode = obj.itemCode,
                                   itemDescription = obj.itemName,
                                   treatmentUnitPrice = obj.itemPrice,
                                   courseType = model.courseType,
                                   treatmentStatus = "Done",
                                   treatmentDate = model.treatmentDate.Value,
                                   nextAppt = new DateTime?(model.treatmentDate.Value.AddDays((double)model.interval)),
                                   isFOC = new bool?(model.cartDetails.isFOC)
                               };
                               courseDetails.treatmentStaffDetails = treatmentStaffs;
                               if (item != null)
                                   courseDetails.treatmentStaffDetails.Update<TreatmentStaff>((Extensions.Func<TreatmentStaff>)(s =>
                           {
                             s.shareAmount = Decimal.Zero;
                             s.workPoint1 = item.workPoints;
                         }));
                               f.courseDetails.Add(courseDetails);
                               app.bsms.Models.Sales.Cart cart2 = model;
                               treatmentDate = model.treatmentDate;
                               DateTime? nullable2 = new DateTime?(treatmentDate.Value.AddDays((double)model.interval));
                               cart2.treatmentDate = nullable2;
                           }
                           Course_Details courseDetails1 = new Course_Details();
                           courseDetails1.treatmentNumber = f.courseDetails.Count + 1;
                           courseDetails1.itemCode = f.itemCode;
                           courseDetails1.itemDescription = f.itemName;
                           courseDetails1.treatmentUnitPrice = model.cartDetails.unitPrice;
                           courseDetails1.courseType = model.courseType;
                           courseDetails1.treatmentStatus = "Open";
                           treatmentDate = model.treatmentDate;
                           courseDetails1.treatmentDate = treatmentDate.Value;
                           treatmentDate = model.treatmentDate;
                           courseDetails1.nextAppt = new DateTime?(treatmentDate.Value.AddDays((double)model.interval));
                           courseDetails1.isFOC = new bool?(model.cartDetails.isFOC);
                           Course_Details courseDetails2 = courseDetails1;
                           f.courseDetails.Add(courseDetails2);
                       }
                       else
                       {
                           f.expiryDate = model.expiryDate;
                           f.treatmentLimit = model.treatmentLimit;
                           if (model.cartDetails.isFirstTreatmentDone)
                           {
                               Course_Details courseDetails1 = new Course_Details();
                               courseDetails1.treatmentNumber = 1;
                               courseDetails1.itemCode = f.itemCode;
                               courseDetails1.itemDescription = f.itemName;
                               courseDetails1.treatmentUnitPrice = model.cartDetails.unitPrice;
                               courseDetails1.courseType = model.courseType;
                               courseDetails1.treatmentStatus = "Done";
                               treatmentDate = model.treatmentDate;
                               courseDetails1.treatmentDate = treatmentDate.Value;
                               treatmentDate = model.treatmentDate;
                               courseDetails1.nextAppt = new DateTime?(treatmentDate.Value.AddDays((double)model.interval));
                               courseDetails1.isFOC = new bool?(model.cartDetails.isFOC);
                               Course_Details courseDetails2 = courseDetails1;
                               courseDetails2.treatmentStaffDetails = treatmentStaffs;
                               if (item != null)
                                   courseDetails2.treatmentStaffDetails.Update<TreatmentStaff>((Extensions.Func<TreatmentStaff>)(s =>
                           {
                             s.shareAmount = Decimal.Zero;
                             s.workPoint1 = item.workPoints;
                         }));
                               f.courseDetails.Add(courseDetails2);
                               app.bsms.Models.Sales.Cart cart2 = model;
                               treatmentDate = model.treatmentDate;
                               DateTime? nullable2 = new DateTime?(treatmentDate.Value.AddDays((double)model.interval));
                               cart2.treatmentDate = nullable2;
                           }
                           Course_Details courseDetails3 = new Course_Details();
                           courseDetails3.treatmentNumber = f.courseDetails.Count + 1;
                           courseDetails3.itemCode = f.itemCode;
                           courseDetails3.itemDescription = f.itemName;
                           courseDetails3.treatmentUnitPrice = model.cartDetails.unitPrice;
                           courseDetails3.courseType = model.courseType;
                           courseDetails3.treatmentStatus = "Open";
                           treatmentDate = model.treatmentDate;
                           courseDetails3.treatmentDate = treatmentDate.Value;
                           treatmentDate = model.treatmentDate;
                           courseDetails3.nextAppt = new DateTime?(treatmentDate.Value.AddDays((double)model.interval));
                           courseDetails3.isFOC = new bool?(model.cartDetails.isFOC);
                           Course_Details courseDetails4 = courseDetails3;
                           f.courseDetails.Add(courseDetails4);
                       }
                       if (model.cartDetails.isFOC)
                       {
                           nullable1 = model.cartDetails.FOCQuantity;
                           int num2 = 0;
                           if ((nullable1.GetValueOrDefault() > num2 ? (nullable1.HasValue ? 1 : 0) : 0) != 0)
                           {
                               int num3 = 0;
                               while (true)
                               {
                                   int num4 = num3;
                                   nullable1 = model.cartDetails.FOCQuantity;
                                   int valueOrDefault = nullable1.GetValueOrDefault();
                                   if ((num4 < valueOrDefault ? (nullable1.HasValue ? 1 : 0) : 0) != 0)
                                   {
                                       Course_Details courseDetails1 = new Course_Details();
                                       courseDetails1.treatmentNumber = 1 + f.courseDetails.Count;
                                       courseDetails1.itemCode = f.itemCode;
                                       courseDetails1.itemDescription = f.itemName;
                                       courseDetails1.treatmentUnitPrice = model.cartDetails.autoProportionate ? num1 : Decimal.Zero;
                                       courseDetails1.courseType = model.courseType;
                                       courseDetails1.treatmentStatus = "Open";
                                       courseDetails1.isFOC = new bool?(true);
                                       treatmentDate = model.treatmentDate;
                                       courseDetails1.treatmentDate = treatmentDate.Value;
                                       treatmentDate = model.treatmentDate;
                                       dateTime = treatmentDate.Value;
                                       courseDetails1.nextAppt = new DateTime?(dateTime.AddDays((double)model.interval));
                                       Course_Details courseDetails2 = courseDetails1;
                                       f.courseDetails.Add(courseDetails2);
                                       app.bsms.Models.Sales.Cart cart2 = model;
                                       treatmentDate = model.treatmentDate;
                                       dateTime = treatmentDate.Value;
                                       DateTime? nullable2 = new DateTime?(dateTime.AddDays((double)model.interval));
                                       cart2.treatmentDate = nullable2;
                                       ++num3;
                                   }
                                   else
                                       break;
                               }
                           }
                       }
                   }
                   else
                   {
                       f.courseDetails = new List<Course_Details>();
                       if (f.lineType == "PACKAGE")
                       {
                           app.bsms.api.Service.Parameters.Clear();
                           app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                           app.bsms.api.Service.Parameters.Add("itemCode", f.itemCode);
                           List<PackageDetails> packageDetailsList = new List<PackageDetails>();
                           packageDetailsList.AddRange((IEnumerable<PackageDetails>)app.bsms.api.Service.GetList<PackageDetails>("packageContent"));
                           foreach (PackageDetails packageDetails in packageDetailsList)
                           {
                               for (int index = 0; index < packageDetails.quantity; ++index)
                               {
                                   Course_Details courseDetails = new Course_Details()
                                   {
                                       treatmentNumber = packageDetails.lineNumber,
                                       itemCode = packageDetails.contentItemCode,
                                       itemDescription = packageDetails.contentItemName,
                                       treatmentUnitPrice = packageDetails.price,
                                       courseType = "N",
                                       treatmentStatus = model.cartDetails.isFirstTreatmentDone ? "Done" : "Open",
                                       treatmentDate = DateTime.Now,
                                       nextAppt = new DateTime?(),
                                       isFOC = new bool?(model.cartDetails.isFOC)
                                   };
                                   courseDetails.treatmentStaffDetails = treatmentStaffs;
                                   if (item != null)
                                       courseDetails.treatmentStaffDetails.Update<TreatmentStaff>((Extensions.Func<TreatmentStaff>)(s =>
                               {
                               s.shareAmount = Decimal.Zero;
                               s.workPoint1 = item.workPoints;
                           }));
                                   f.courseDetails.Add(courseDetails);
                               }
                           }
                       }
                       else
                       {
                           Course_Details courseDetails = new Course_Details()
                           {
                               treatmentNumber = 1,
                               itemCode = f.itemCode,
                               itemDescription = f.itemName,
                               treatmentUnitPrice = model.cartDetails.itemAmount,
                               courseType = "N",
                               treatmentStatus = model.cartDetails.isFirstTreatmentDone ? "Done" : "Open",
                               treatmentDate = DateTime.Now,
                               nextAppt = new DateTime?(),
                               isFOC = new bool?(model.cartDetails.isFOC)
                           };
                           courseDetails.treatmentStaffDetails = treatmentStaffs;
                           if (item != null)
                               courseDetails.treatmentStaffDetails.Update<TreatmentStaff>((Extensions.Func<TreatmentStaff>)(s =>
                       {
                           s.shareAmount = Decimal.Zero;
                           s.workPoint1 = item.workPoints;
                       }));
                           f.courseDetails.Add(courseDetails);
                       }
                   }
                   List<Settings> settings = ((User)this.Session["Login_Details"]).settings;
                   bool inclusive = settings.Where<Settings>((Func<Settings, bool>)(s => s.settingName == "GST SETTING")).FirstOrDefault<Settings>().settingValue == "INCLUSIVE";
                   Decimal num = Convert.ToDecimal(settings.Where<Settings>((Func<Settings, bool>)(s => s.settingName == "GST PERCENTAGE")).FirstOrDefault<Settings>().settingValue);
                   Decimal taxPercentage = Decimal.One + num / new Decimal(100);
                   string settingValue = settings.Where<Settings>((Func<Settings, bool>)(s => s.settingName == "GST APPLY BY")).FirstOrDefault<Settings>().settingValue;
                   if (f.lineType != "TD")
                   {
                       if (settingValue == "ALL")
                           f.gstAmountCollected = Utility.GetGST(inclusive, f.itemAmount, taxPercentage);
                       else if (settingValue == "ITEMWISE" && item.taxable)
                           f.gstAmountCollected = Utility.GetGST(inclusive, f.itemAmount, taxPercentage);
                   }
                   f.holdItemQty = f.holdItemQty.IsNullOrZero() ? new int?(0) : f.holdItemQty;
               }));
                app.bsms.api.Service.Parameters.Clear();
                JsonSerializerSettings settings1 = new JsonSerializerSettings()
                {
                    ContractResolver = (IContractResolver)new NullToEmptyStringResolver()
                };
                if (model.cart.cartToken != null && model.cart.cartToken != string.Empty)
                {
                    app.bsms.api.Service.Parameters.Add("cartToken", model.cart.cartToken);
                    app.bsms.api.Service.Put<Suspend_Cart>("itemCart", JsonConvert.SerializeObject((object)cart1.cart, settings1));
                }
                else
                    app.bsms.api.Service.Post<Temp_Cart>("itemCart", JsonConvert.SerializeObject((object)cart1.cart, settings1));
                Alerts.body = Resources.Resources.Success;  //"Success!";
                Alerts.Success = Resources.Resources.Item + " - " + model.cartDetails.itemCode + Resources.Resources.UpdatedSuccessfully; //" Updated Successfully";
                this.TempData["Message"] = (object)Alerts.Success;
                return (ActionResult)this.RedirectToAction("Index", "Cart", (object)new
                {
                    id = (model.cart.cartToken == null || !(model.cart.cartToken != string.Empty) ? (string)null : model.cart.cartToken)
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Empty()
        {
            app.bsms.Models.Sales.Cart cart = new app.bsms.Models.Sales.Cart();
            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (ActionResult)this.View((object)cart);
        }

        [HttpGet]
        public ActionResult Presetprepaid(string id)
        {
            app.bsms.Models.Sales.Cart cart = new app.bsms.Models.Sales.Cart();
            try
            {
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                app.bsms.api.Service.Parameters.Add("userId", ((User)this.Session["Login_Details"]).userID);
                cart.cart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
                cart.cartDetails = cart.cart.transactionDetails.Where<Cart_Details>((Func<Cart_Details, bool>)(f => f.itemCode == id)).SingleOrDefault<Cart_Details>();
                string[] lineType = new string[2]
                {
          "SERVICE",
          "PRODUCT"
                };
                app.bsms.api.Service.Parameters.Clear();
                cart.lstLineTypes = new List<ListItems>();
                cart.lstLineTypes.Add(new ListItems()
                {
                    itemCode = "",
                    itemDesc = "",
                    lineType = ""
                });
                cart.lstLineTypes.Add(new ListItems()
                {
                    itemCode = "All",
                    itemDesc = "All",
                    lineType = "All"
                });
                cart.lstLineTypes.AddRange(app.bsms.api.Service.GetList<ListItems>("lineType").Where<ListItems>((Func<ListItems, bool>)(f => ((IEnumerable<string>)lineType).Contains<string>(f.lineType))));
                cart.lstCondition2 = new List<ListItems>();
                cart.lstPrepaidTypes = Utility.GetPrepaidTypes();
                cart.prepaid.prepaidExpiryDate = new DateTime?(DateTime.Now);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (ActionResult)this.View((object)cart);
        }

        [HttpGet]
        public JsonResult GetBrandServices(string condition)
        {
            List<ListItems> listItemsList = new List<ListItems>();
            try
            {
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                listItemsList.Add(new ListItems()
                {
                    itemCode = "",
                    itemDesc = "",
                    lineType = ""
                });
                listItemsList.Add(new ListItems()
                {
                    itemCode = "All",
                    itemDesc = "All",
                    lineType = "All"
                });
                if (condition == "SERVICE")
                    listItemsList.AddRange(app.bsms.api.Service.GetList<app.bsms.Models.Catelogue.Service>("department").Select<app.bsms.Models.Catelogue.Service, ListItems>((Func<app.bsms.Models.Catelogue.Service, ListItems>)(f => new ListItems()
                    {
                        itemCode = f.departmentCode,
                        itemDesc = f.departmentName,
                        lineType = condition
                    })));
                else if (condition == "PRODUCT")
                    listItemsList.AddRange(app.bsms.api.Service.GetList<Brand>("Brand").Select<Brand, ListItems>((Func<Brand, ListItems>)(f => new ListItems()
                    {
                        itemCode = f.brandCode,
                        itemDesc = f.brandName,
                        lineType = condition
                    })));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return this.Json((object)listItemsList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Presetprepaid(app.bsms.Models.Sales.Cart model)
        {
            app.bsms.Models.Sales.Cart cart = new app.bsms.Models.Sales.Cart();
            try
            {
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                app.bsms.api.Service.Parameters.Add("userId", ((User)this.Session["Login_Details"]).userID);
                cart.cart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
                cart.cartDetails = cart.cart.transactionDetails.Where<Cart_Details>((Func<Cart_Details, bool>)(f => f.itemCode == model.cartDetails.itemCode)).SingleOrDefault<Cart_Details>();
                string[] lineType = new string[2]
                {
          "SERVICE",
          "PRODUCT"
                };
                app.bsms.api.Service.Parameters.Clear();
                cart.lstLineTypes = new List<ListItems>();
                cart.lstLineTypes.Add(new ListItems()
                {
                    itemCode = "",
                    itemDesc = "",
                    lineType = ""
                });
                cart.lstLineTypes.Add(new ListItems()
                {
                    itemCode = "All",
                    itemDesc = "All",
                    lineType = "All"
                });
                cart.lstLineTypes.AddRange(app.bsms.api.Service.GetList<ListItems>("lineType").Where<ListItems>((Func<ListItems, bool>)(f => ((IEnumerable<string>)lineType).Contains<string>(f.lineType))));
                cart.lstPrepaidTypes = Utility.GetPrepaidTypes();
                cart.prepaid.prepaidExpiryDate = new DateTime?(DateTime.Now);
                cart.cartDetails.prepaidAccountDetails = new List<Prepaid>();
                cart.cartDetails.prepaidAccountDetails.Add(new Prepaid()
                {
                    prepaidLineNumber = cart.cartDetails.prepaidAccountDetails.Count + 1,
                    prepaidItemType = model.prepaid.prepaidItemType,
                    prepaidCondition1 = model.prepaid.prepaidCondition1,
                    prepaidCondition2 = model.prepaid.prepaidCondition2,
                    prepaidExpiryDate = model.prepaid.prepaidExpiryDate,
                    prepaidAmount = model.prepaid.prepaidAmount,
                    prepaidValue = model.prepaid.prepaidValue,
                    prepaidType = cart.cartDetails.itemCode
                });
                app.bsms.api.Service.Parameters.Clear();
                JsonSerializerSettings settings = new JsonSerializerSettings()
                {
                    ContractResolver = (IContractResolver)new NullToEmptyStringResolver()
                };
                if (cart.cart.cartToken != null && cart.cart.cartToken != string.Empty)
                {
                    app.bsms.api.Service.Parameters.Add("cartToken", cart.cart.cartToken);
                    app.bsms.api.Service.Put<Suspend_Cart>("itemCart", JsonConvert.SerializeObject((object)cart.cart, settings));
                }
                else
                    app.bsms.api.Service.Post<Temp_Cart>("itemCart", JsonConvert.SerializeObject((object)cart.cart, settings));
                return (ActionResult)this.RedirectToAction("Index", "Cart");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Presetcourse(string id)
        {
            app.bsms.Models.Sales.Cart cart1 = new app.bsms.Models.Sales.Cart();
            try
            {
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                cart1.lstStaff = new List<Staffs>();
                cart1.lstStaff.Add(new Staffs()
                {
                    staffCode = string.Empty,
                    staffName = string.Empty
                });
                cart1.lstStaff.AddRange((IEnumerable<Staffs>)app.bsms.api.Service.GetList<Staffs>("getStaffList"));
                cart1.lstFOCs = new List<ListItems>();
                cart1.lstFOCs.Add(new ListItems()
                {
                    itemCode = string.Empty,
                    itemDesc = string.Empty
                });
                cart1.lstFOCs.AddRange((IEnumerable<ListItems>)app.bsms.api.Service.GetList<ListItems>("focReason"));
                app.bsms.api.Service.Parameters.Add("userId", ((User)this.Session["Login_Details"]).userID);
                cart1.cart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                app.bsms.api.Service.Parameters.Add("itemCode", id);
                //Item obj = app.bsms.api.Service.Get<Item>("getItemDetails");
                //Yoonus changing to getItemDetail
                Item obj = app.bsms.api.Service.Get<Item>("getItemDetail");
                cart1.cartDetails = cart1.cart.transactionDetails.Where<Cart_Details>((Func<Cart_Details, bool>)(f => f.itemCode == id)).SingleOrDefault<Cart_Details>();
                if (cart1.cartDetails.lineType == "COURSE")
                {
                    if (cart1.cartDetails.courseDetails == null)
                    {
                        app.bsms.Models.Sales.Cart cart2 = cart1;
                        cart2.treatmentDate = !cart2.treatmentDate.HasValue ? new DateTime?(DateTime.Now.Date) : cart1.treatmentDate;
                        app.bsms.Models.Sales.Cart cart3 = cart1;
                        cart3.interval = cart3.interval == 0 ? 7 : cart1.interval;
                        if (!obj.isFlexi)
                        {
                            cart1.totalTreatments = new int?(1);
                        }
                        else
                        {
                            cart1.expiryDate = new DateTime?(DateTime.Now.AddMonths(obj.treatmentExpiryInMonths));
                            cart1.treatmentLimit = new int?(obj.treatmentLimitCount);
                        }
                        cart1.cartDetails.FOCQuantity = new int?(0);
                        cart1.cartDetails.autoProportionate = false;
                        cart1.courseType = obj.isFlexi ? "FFi" : "N";
                    }
                    else if (cart1.cartDetails.courseDetails.Count == 0)
                    {
                        app.bsms.Models.Sales.Cart cart2 = cart1;
                        cart2.treatmentDate = !cart2.treatmentDate.HasValue ? new DateTime?(DateTime.Now.Date) : cart1.treatmentDate;
                        app.bsms.Models.Sales.Cart cart3 = cart1;
                        cart3.interval = cart3.interval == 0 ? 7 : cart1.interval;
                        if (!obj.isFlexi)
                        {
                            cart1.totalTreatments = new int?(1);
                        }
                        else
                        {
                            cart1.expiryDate = new DateTime?(DateTime.Now.AddMonths(obj.treatmentExpiryInMonths));
                            cart1.treatmentLimit = new int?(obj.treatmentLimitCount);
                        }
                        cart1.cartDetails.FOCQuantity = new int?(0);
                        cart1.cartDetails.autoProportionate = false;
                        cart1.courseType = obj.isFlexi ? "FFi" : "N";
                    }
                    else
                    {
                        Course_Details courseDetails1 = cart1.cartDetails.courseDetails.FirstOrDefault<Course_Details>();
                        app.bsms.Models.Sales.Cart cart2 = cart1;
                        cart2.totalTreatments = new int?(cart2.cartDetails.courseDetails.Count);
                        cart1.treatmentDate = new DateTime?(courseDetails1.treatmentDate);
                        cart1.interval = (int)(courseDetails1.nextAppt.Value - courseDetails1.treatmentDate).TotalDays;
                        cart1.courseType = courseDetails1.courseType;
                        cart1.cartDetails.FOCQuantity = new int?(cart1.cartDetails.courseDetails.Where<Course_Details>((Func<Course_Details, bool>)(f =>
                       {
                           bool? isFoc = f.isFOC;
                           bool flag = true;
                           if (isFoc.GetValueOrDefault() != flag)
                               return false;
                           return isFoc.HasValue;
                       })).Count<Course_Details>());
                        Course_Details courseDetails2 = cart1.cartDetails.courseDetails.Where<Course_Details>((Func<Course_Details, bool>)(f =>
                       {
                           bool? isFoc = f.isFOC;
                           bool flag = true;
                           if (isFoc.GetValueOrDefault() != flag)
                               return false;
                           return isFoc.HasValue;
                       })).FirstOrDefault<Course_Details>();
                        if (courseDetails2 != null)
                            cart1.cartDetails.autoProportionate = courseDetails2.treatmentUnitPrice > Decimal.Zero;
                    }
                }
                cart1.lstCourse_Settings = new List<Course_Settings>();
                cart1.lstCourse_Settings.AddRange((IEnumerable<Course_Settings>)app.bsms.api.Service.GetList<Course_Settings>("courseSettings"));
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                app.bsms.api.Service.Parameters.Add("itemCode", id);
                cart1.lstFlexiDetails = new List<Item>();
                cart1.lstFlexiDetails.AddRange((IEnumerable<Item>)app.bsms.api.Service.GetList<Item>("getFlexiDetails"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (ActionResult)this.View((object)cart1);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Presetcourse(app.bsms.Models.Sales.Cart model)
        {
            app.bsms.Models.Sales.Cart cart1 = new app.bsms.Models.Sales.Cart();
            try
            {
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                app.bsms.api.Service.Parameters.Add("itemCode", model.cartDetails.itemCode);
                //Item item = app.bsms.api.Service.Get<Item>("getItemDetails");
                //Yoonus chaning to GetItemDetail
                Item item = app.bsms.api.Service.Get<Item>("getItemDetail");
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                cart1.lstStaff = new List<Staffs>();
                cart1.lstStaff.Add(new Staffs()
                {
                    staffCode = string.Empty,
                    staffName = string.Empty
                });
                cart1.lstStaff.AddRange((IEnumerable<Staffs>)app.bsms.api.Service.GetList<Staffs>("getStaffList"));
                app.bsms.api.Service.Parameters.Add("userId", ((User)this.Session["Login_Details"]).userID);
                cart1.cart = app.bsms.api.Service.Get<Temp_Cart>("itemCart");
                List<TreatmentStaff> treatmentStaffs = new List<TreatmentStaff>();
                if (model.cartDetails.strTreatmentStaffs != null)
                    treatmentStaffs = JsonConvert.DeserializeObject<List<TreatmentStaff>>(model.cartDetails.strTreatmentStaffs);
                cart1.cart.transactionDetails.Where<Cart_Details>((Func<Cart_Details, bool>)(f =>
               {
                   if (f.itemCode == model.cartDetails.itemCode)
                       return f.itemName == model.cartDetails.itemName;
                   return false;
               })).Select<Cart_Details, Cart_Details>((Func<Cart_Details, Cart_Details>)(s => s)).Update<Cart_Details>((Extensions.Func<Cart_Details>)(f =>
      {
                   f.staffcode = (string)null;
                   f.staffName = (string)null;
                   f.itemCode = model.cartDetails.itemCode;
                   f.itemName = model.cartDetails.itemName;
                   f.itemUOM = model.cartDetails.itemUOM;
                   f.unitPrice = model.cartDetails.unitPrice;
                   f.itemQty = model.cartDetails.itemQty;
                   if (f.lineType == "COURSE")
                   {
                       int? nullable1;
                       if (model.courseType == "N")
                       {
                           f.itemQty = model.totalTreatments.Value;
                           f.totalTreatments = model.totalTreatments;
                           Cart_Details cartDetails = f;
                           nullable1 = f.totalTreatments;
                           Decimal num = (Decimal)nullable1.Value * f.unitPrice - f.unitDiscount;
                           cartDetails.promoPrice = num;
                       }
                       else
                           f.promoPrice = model.cartDetails.unitPrice * (Decimal)model.cartDetails.itemQty - f.unitDiscount;
                       Cart_Details cartDetails1 = f;
                       cartDetails1.itemAmount = cartDetails1.promoPrice;
                       Cart_Details cartDetails2 = f;
                       cartDetails2.salesAmount = cartDetails2.promoPrice;
                       f.depositAmount = model.cartDetails.depositAmount.IsNullOrZero() ? new Decimal?(f.promoPrice) : model.cartDetails.depositAmount;
                       f.isFirstTreatmentDone = model.cartDetails.isFirstTreatmentDone;
                       f.isFOC = model.cartDetails.isFOC;
                       f.FOCReason = model.cartDetails.FOCReason;
                       f.courseDetails = new List<Course_Details>();
                       Decimal num1 = new Decimal();
                       DateTime? treatmentDate;
                       DateTime dateTime;
                       if (model.courseType == "N")
                       {
                           if (model.cartDetails.autoProportionate)
                           {
                               Decimal itemAmount = f.itemAmount;
                               nullable1 = model.totalTreatments;
                               int num2 = nullable1.Value;
                               nullable1 = model.cartDetails.FOCQuantity;
                               nullable1 = !nullable1.HasValue ? new int?(0) : model.cartDetails.FOCQuantity;
                               int num3 = nullable1.Value;
                               Decimal num4 = (Decimal)(num2 + num3);
                               num1 = itemAmount / num4;
                           }
                           else
                           {
                               Decimal itemAmount = f.itemAmount;
                               nullable1 = model.totalTreatments;
                               Decimal num2 = (Decimal)nullable1.Value;
                               num1 = itemAmount / num2;
                           }
                           int num5 = 0;
                           while (true)
                           {
                               int num2 = num5;
                               nullable1 = model.totalTreatments;
                               int valueOrDefault = nullable1.GetValueOrDefault();
                               if ((num2 < valueOrDefault ? (nullable1.HasValue ? 1 : 0) : 0) != 0)
                               {
                                   Course_Details courseDetails1 = new Course_Details();
                                   courseDetails1.treatmentNumber = num5 + 1;
                                   courseDetails1.itemCode = f.itemCode;
                                   courseDetails1.itemDescription = f.itemName;
                                   courseDetails1.treatmentUnitPrice = num1;
                                   courseDetails1.courseType = model.courseType;
                                   courseDetails1.treatmentStatus = num5 + 1 == 1 ? (f.isFirstTreatmentDone ? "Done" : "Open") : "Open";
                                   treatmentDate = model.treatmentDate;
                                   courseDetails1.treatmentDate = treatmentDate.Value;
                                   treatmentDate = model.treatmentDate;
                                   dateTime = treatmentDate.Value;
                                   courseDetails1.nextAppt = new DateTime?(dateTime.AddDays((double)model.interval));
                                   courseDetails1.isFOC = new bool?(false);
                                   Course_Details courseDetails2 = courseDetails1;
                                   f.courseDetails.Add(courseDetails2);
                                   app.bsms.Models.Sales.Cart cart2 = model;
                                   treatmentDate = model.treatmentDate;
                                   dateTime = treatmentDate.Value;
                                   DateTime? nullable2 = new DateTime?(dateTime.AddDays((double)model.interval));
                                   cart2.treatmentDate = nullable2;
                                   ++num5;
                               }
                               else
                                   break;
                           }
                           if (f.isFirstTreatmentDone)
                           {
                               Course_Details courseDetails = f.courseDetails.OrderBy<Course_Details, int>((Func<Course_Details, int>)(o => o.treatmentNumber)).FirstOrDefault<Course_Details>();
                               if (courseDetails != null)
                               {
                                   courseDetails.treatmentStaffDetails = new List<TreatmentStaff>();
                                   courseDetails.treatmentStaffDetails = treatmentStaffs;
                                   if (item != null)
                                       courseDetails.treatmentStaffDetails.Update<TreatmentStaff>((Extensions.Func<TreatmentStaff>)(s =>
                               {
                               s.shareAmount = Decimal.Zero;
                               s.workPoint1 = item.workPoints;
                           }));
                               }
                           }
                       }
                       else if (model.courseType == "FFi")
                       {
                           f.expiryDate = model.expiryDate;
                           f.treatmentLimit = model.treatmentLimit;
                           if (model.cartDetails.isFirstTreatmentDone)
                           {
                               app.bsms.api.Service.Parameters.Clear();
                               app.bsms.api.Service.Parameters.Add("siteCode", ((User)this.Session["Login_Details"]).siteCode);
                               app.bsms.api.Service.Parameters.Add("itemCode", model.cartDetails.subItemCode);
                        //Item obj = app.bsms.api.Service.Get<Item>("getItemDetails");
                        //Yoonus Changing to GetItemDetail
                        Item obj = app.bsms.api.Service.Get<Item>("getItemDetail");
                               Course_Details courseDetails1 = new Course_Details();
                               courseDetails1.treatmentNumber = 1;
                               courseDetails1.itemCode = obj.itemCode;
                               courseDetails1.itemDescription = obj.itemName;
                               courseDetails1.treatmentUnitPrice = obj.itemPrice;
                               courseDetails1.courseType = model.courseType;
                               courseDetails1.treatmentStatus = "Done";
                               courseDetails1.treatmentDate = model.treatmentDate.Value;
                               dateTime = model.treatmentDate.Value;
                               courseDetails1.nextAppt = new DateTime?(dateTime.AddDays((double)model.interval));
                               courseDetails1.isFOC = new bool?(model.cartDetails.isFOC);
                               Course_Details courseDetails2 = courseDetails1;
                               courseDetails2.treatmentStaffDetails = treatmentStaffs;
                               if (item != null)
                                   courseDetails2.treatmentStaffDetails.Update<TreatmentStaff>((Extensions.Func<TreatmentStaff>)(s =>
                           {
                             s.shareAmount = Decimal.Zero;
                             s.workPoint1 = item.workPoints;
                         }));
                               f.courseDetails.Add(courseDetails2);
                               app.bsms.Models.Sales.Cart cart2 = model;
                               treatmentDate = model.treatmentDate;
                               dateTime = treatmentDate.Value;
                               DateTime? nullable2 = new DateTime?(dateTime.AddDays((double)model.interval));
                               cart2.treatmentDate = nullable2;
                           }
                           Course_Details courseDetails3 = new Course_Details();
                           courseDetails3.treatmentNumber = f.courseDetails.Count + 1;
                           courseDetails3.itemCode = f.itemCode;
                           courseDetails3.itemDescription = f.itemName;
                           courseDetails3.treatmentUnitPrice = model.cartDetails.unitPrice;
                           courseDetails3.courseType = model.courseType;
                           courseDetails3.treatmentStatus = "Open";
                           treatmentDate = model.treatmentDate;
                           courseDetails3.treatmentDate = treatmentDate.Value;
                           treatmentDate = model.treatmentDate;
                           dateTime = treatmentDate.Value;
                           courseDetails3.nextAppt = new DateTime?(dateTime.AddDays((double)model.interval));
                           courseDetails3.isFOC = new bool?(model.cartDetails.isFOC);
                           Course_Details courseDetails4 = courseDetails3;
                           f.courseDetails.Add(courseDetails4);
                       }
                       else
                       {
                           f.expiryDate = model.expiryDate;
                           f.treatmentLimit = model.treatmentLimit;
                           if (model.cartDetails.isFirstTreatmentDone)
                           {
                               Course_Details courseDetails = new Course_Details()
                               {
                                   treatmentNumber = 1,
                                   itemCode = f.itemCode,
                                   itemDescription = f.itemName,
                                   treatmentUnitPrice = model.cartDetails.unitPrice,
                                   courseType = model.courseType,
                                   treatmentStatus = "Done",
                                   treatmentDate = model.treatmentDate.Value,
                                   nextAppt = new DateTime?(model.treatmentDate.Value.AddDays((double)model.interval)),
                                   isFOC = new bool?(model.cartDetails.isFOC)
                               };
                               courseDetails.treatmentStaffDetails = treatmentStaffs;
                               if (item != null)
                                   courseDetails.treatmentStaffDetails.Update<TreatmentStaff>((Extensions.Func<TreatmentStaff>)(s =>
                           {
                             s.shareAmount = Decimal.Zero;
                             s.workPoint1 = item.workPoints;
                         }));
                               f.courseDetails.Add(courseDetails);
                               app.bsms.Models.Sales.Cart cart2 = model;
                               treatmentDate = model.treatmentDate;
                               DateTime? nullable2 = new DateTime?(treatmentDate.Value.AddDays((double)model.interval));
                               cart2.treatmentDate = nullable2;
                           }
                           Course_Details courseDetails1 = new Course_Details();
                           courseDetails1.treatmentNumber = f.courseDetails.Count + 1;
                           courseDetails1.itemCode = f.itemCode;
                           courseDetails1.itemDescription = f.itemName;
                           courseDetails1.treatmentUnitPrice = model.cartDetails.unitPrice;
                           courseDetails1.courseType = model.courseType;
                           courseDetails1.treatmentStatus = "Open";
                           treatmentDate = model.treatmentDate;
                           courseDetails1.treatmentDate = treatmentDate.Value;
                           treatmentDate = model.treatmentDate;
                           courseDetails1.nextAppt = new DateTime?(treatmentDate.Value.AddDays((double)model.interval));
                           courseDetails1.isFOC = new bool?(model.cartDetails.isFOC);
                           Course_Details courseDetails2 = courseDetails1;
                           f.courseDetails.Add(courseDetails2);
                       }
                       if (model.cartDetails.isFOC)
                       {
                           nullable1 = model.cartDetails.FOCQuantity;
                           int num2 = 0;
                           if ((nullable1.GetValueOrDefault() > num2 ? (nullable1.HasValue ? 1 : 0) : 0) != 0)
                           {
                               int num3 = 0;
                               while (true)
                               {
                                   int num4 = num3;
                                   nullable1 = model.cartDetails.FOCQuantity;
                                   int valueOrDefault = nullable1.GetValueOrDefault();
                                   if ((num4 < valueOrDefault ? (nullable1.HasValue ? 1 : 0) : 0) != 0)
                                   {
                                       Course_Details courseDetails1 = new Course_Details();
                                       courseDetails1.treatmentNumber = 1 + f.courseDetails.Count;
                                       courseDetails1.itemCode = f.itemCode;
                                       courseDetails1.itemDescription = f.itemName;
                                       courseDetails1.treatmentUnitPrice = model.cartDetails.autoProportionate ? num1 : Decimal.Zero;
                                       courseDetails1.courseType = model.courseType;
                                       courseDetails1.treatmentStatus = "Open";
                                       courseDetails1.isFOC = new bool?(true);
                                       treatmentDate = model.treatmentDate;
                                       courseDetails1.treatmentDate = treatmentDate.Value;
                                       treatmentDate = model.treatmentDate;
                                       dateTime = treatmentDate.Value;
                                       courseDetails1.nextAppt = new DateTime?(dateTime.AddDays((double)model.interval));
                                       Course_Details courseDetails2 = courseDetails1;
                                       f.courseDetails.Add(courseDetails2);
                                       app.bsms.Models.Sales.Cart cart2 = model;
                                       treatmentDate = model.treatmentDate;
                                       dateTime = treatmentDate.Value;
                                       DateTime? nullable2 = new DateTime?(dateTime.AddDays((double)model.interval));
                                       cart2.treatmentDate = nullable2;
                                       ++num3;
                                   }
                                   else
                                       break;
                               }
                           }
                       }
                   }
                   List<Settings> settings = ((User)this.Session["Login_Details"]).settings;
                   bool inclusive = settings.Where<Settings>((Func<Settings, bool>)(s => s.settingName == "GST SETTING")).FirstOrDefault<Settings>().settingValue == "INCLUSIVE";
                   Decimal num6 = Convert.ToDecimal(settings.Where<Settings>((Func<Settings, bool>)(s => s.settingName == "GST PERCENTAGE")).FirstOrDefault<Settings>().settingValue);
                   Decimal taxPercentage = Decimal.One + num6 / new Decimal(100);
                   string settingValue = settings.Where<Settings>((Func<Settings, bool>)(s => s.settingName == "GST APPLY BY")).FirstOrDefault<Settings>().settingValue;
                   if (f.lineType != "TD")
                   {
                       if (settingValue == "ALL")
                           f.gstAmountCollected = Utility.GetGST(inclusive, f.itemAmount, taxPercentage);
                       else if (item.taxable)
                           f.gstAmountCollected = Utility.GetGST(inclusive, f.itemAmount, taxPercentage);
                   }
                   f.holdItemQty = f.holdItemQty.IsNullOrZero() ? new int?(0) : f.holdItemQty;
               }));
                app.bsms.api.Service.Parameters.Clear();
                JsonSerializerSettings settings1 = new JsonSerializerSettings()
                {
                    ContractResolver = (IContractResolver)new NullToEmptyStringResolver()
                };
                if (model.cart.cartToken != null && model.cart.cartToken != string.Empty)
                {
                    app.bsms.api.Service.Parameters.Add("cartToken", model.cart.cartToken);
                    app.bsms.api.Service.Put<Suspend_Cart>("itemCart", JsonConvert.SerializeObject((object)cart1.cart, settings1));
                }
                else
                    app.bsms.api.Service.Post<Temp_Cart>("itemCart", JsonConvert.SerializeObject((object)cart1.cart, settings1));
                return (ActionResult)this.RedirectToAction("Index", "Cart");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
