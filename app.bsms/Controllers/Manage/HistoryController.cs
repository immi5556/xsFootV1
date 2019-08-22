using app.bsms;
using app.bsms.api;
using app.bsms.Controllers;
using app.bsms.Models.Account;
using app.bsms.Models.Sales;
using app.bsms.Models.Sales.History;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace app.bsms.Controllers.Manage
{
	[NoCache]
	public class HistoryController : BaseController
	{
		public HistoryController()
		{
		}

		public ActionResult Invoice(string id)
		{
			List<app.bsms.Models.Sales.History.Invoice> invoices = new List<app.bsms.Models.Sales.History.Invoice>();
			try
			{
				((dynamic)base.ViewBag).customerCode = id;
				app.bsms.api.Service.Parameters.Clear();
				app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				app.bsms.api.Service.Parameters.Add("customerCode", id);
				invoices = app.bsms.api.Service.GetList<app.bsms.Models.Sales.History.Invoice>("treatmentHistoryInvoiceList");
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(invoices);
		}

		public ActionResult Treatment(string id)
		{
			List<app.bsms.Models.Sales.Transaction> transactions = new List<app.bsms.Models.Sales.Transaction>();
			try
			{
				((dynamic)base.ViewBag).customerCode = id;
				app.bsms.api.Service.Parameters.Clear();
				app.bsms.api.Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				app.bsms.api.Service.Parameters.Add("customerCode", id);
				app.bsms.api.Service.Parameters.Add("treatmentParentCode", string.Empty);
				transactions = app.bsms.api.Service.GetList<app.bsms.Models.Sales.Transaction>("transactionHistoryList");
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(transactions);
		}
	}
}