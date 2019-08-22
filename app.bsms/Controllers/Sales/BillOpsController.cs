using app.bsms;
using app.bsms.api;
using app.bsms.Common;
using app.bsms.Controllers;
using app.bsms.Models.Account;
using app.bsms.Models.Sales;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace app.bsms.Controllers.Sales
{
    [NoCache]
    public class BillOpsController : BaseController
    {
        public BillOpsController()
        {
        }

        public ActionResult Index(string id)
        {
            BillOps billOp = new BillOps();
            try
            {
                if (id == null || id == string.Empty)
                {
                    billOp.option = "Pending";
                }
                billOp.lstOptions = Utility.GetBillOpsOptions();
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
                if (billOp.option == "Pending")
                {
                    billOp.lstPendingCarts = app.bsms.api.Service.GetList<Temp_Cart>("getItemCartList");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return base.View(billOp);
        }

        public ActionResult Void(BillOps model)
        {
            BillOps billOp = new BillOps();
            try
            {
                app.bsms.api.Service.Parameters.Clear();
                app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return base.View(billOp);
        }

        [HttpPost]
        public ActionResult Index(BillOps model)
        {
            DateTime date;
            int year;
            BillOps billOp = new BillOps();
            try
            {
                if (base.ModelState.IsValid)
                {
                    billOp.lstOptions = Utility.GetBillOpsOptions();
                    billOp.option = model.option;
                    app.bsms.api.Service.Parameters.Clear();
                    app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
                    if (model.option == "Pending")
                    {
                        billOp.lstPendingCarts = app.bsms.api.Service.GetList<Temp_Cart>("getItemCartList");
                    }
                    else if (model.option == "Suspend")
                    {
                        billOp.lstSuspendedCarts = app.bsms.api.Service.GetList<Suspend_Cart>("getSuspendedCartList");
                    }
                    else if (model.option == "Transaction")
                    {
                        if (!model.fromDate.HasValue)
                        {
                            Dictionary<string, string> parameters = app.bsms.api.Service.Parameters;
                            date = DateTime.Now.Date;
                            year = date.Year;
                            parameters.Add("startDate", string.Concat(year.ToString(), "/01/01"));
                        }
                        else
                        {
                            Dictionary<string, string> strs = app.bsms.api.Service.Parameters;
                            date = model.fromDate.Value;
                            strs.Add("startDate", date.ToString("yyyy/MM/dd"));
                        }
                        if (!model.toDate.HasValue)
                        {
                            Dictionary<string, string> parameters1 = app.bsms.api.Service.Parameters;
                            date = DateTime.Now.Date;
                            year = date.Year;
                            parameters1.Add("endDate", string.Concat(year.ToString(), "/12/31"));
                        }
                        else
                        {
                            Dictionary<string, string> strs1 = app.bsms.api.Service.Parameters;
                            date = model.toDate.Value;
                            strs1.Add("endDate", date.ToString("yyyy/MM/dd"));
                        }
                        billOp.lstInvoices = app.bsms.api.Service.GetList<app.bsms.Models.Sales.History.Invoice>("treatmentHistoryInvoiceList");
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return base.View(billOp);
        }

        public ActionResult VoidInvoice(string id, string id2)
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
                invoice.paymentDetails = app.bsms.api.Service.GetList<Payment_Details>("getInvoiceFooterTax");
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

        public ActionResult CheckSecurity(string un, string ps, string logdet)
        {
            try
            {
                return Json(false);
            }
            catch { return Json(false); }

            //    try
            //    {


            //        app.bsms.api.Service.Parameters.Clear();
            //        app.bsms.api.Service.Parameters.Add("userName", un);
            //        app.bsms.api.Service.Parameters.Add("password", ps);
            //        //Service.Parameters.Add("clientCode", model.ClientCode);
            //        app.bsms.api.Service.Parameters.Add("sitecode", ((User)this.Session["Login_Details"]).siteCode);
            //        app.bsms.api.Service.Parameters.Add("forgotPassword", logdet);
            //        app.bsms.Models.Account.User list = app.bsms.api.Service.Get<app.bsms.Models.Account.User>("login");
            //        if (list != null)
            //        {
            //            if (list.siteCode == string.Empty)
            //            {
            //                //Alerts.body = "Login Unsuccessfull";
            //                //Alerts.ErrorMessage = "Invalid Username or Password or Client Code";
            //                //base.TempData["Message"] = Alerts.ErrorMessage;
            //                return Json(false);
            //            }
            //            else
            //            {
            //                //Service.Parameters.Clear();
            //                //Service.Parameters.Add("siteCode", list.siteCode);
            //                //list.settings = Service.GetList<Settings>("settings");
            //                //base.Session["Login_Details"] = list;
            //                //return base.RedirectToAction("Index", "Dashboard");
            //                return Json(true);
            //            }
            //        }

            //    }
            //    catch (Exception exception)
            //    {
            //        Alerts.body = "Login Unsuccessfull";
            //        Alerts.ErrorMessage = exception.Message;
            //        base.TempData["Message"] = Alerts.ErrorMessage;
            //    }
            //    return Json(true);
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
                invoice.paymentDetails = app.bsms.api.Service.GetList<Payment_Details>("getInvoiceFooterTax");
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
    }
}