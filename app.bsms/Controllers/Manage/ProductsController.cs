using app.bsms;
using app.bsms.api;
using app.bsms.Controllers;
using app.bsms.Models.Account;
using app.bsms.Models.Manage.Service;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace app.bsms.Controllers.Manage
{
	[NoCache]
	public class ProductsController : BaseController
	{
		public ProductsController()
		{
		}

		public ActionResult Details(string id, string id2)
		{
			Products product = new Products();
			try
			{
				((dynamic)base.ViewBag).customerCode = id;
				product.customerCode = id;
				product.treatmentCode = id2;
				product.siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode;
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", product.siteCode);
				Service.Parameters.Add("customerCode", product.customerCode);
				Service.Parameters.Add("treatCode", product.treatmentCode);
				product.details = Service.GetList<Product_Details>("productAccountDetail");
				product.info = Service.GetList<Products>("ProductAccountSummaryInfo");
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(product);
		}

		public ActionResult Index(string id)
		{
			List<Products> products = new List<Products>();
			try
			{
				((dynamic)base.ViewBag).customerCode = id;
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				Service.Parameters.Add("customerCode", id);
				products = Service.GetList<Products>("ProductAccountSummary");
				foreach (Products product in products)
				{
					Service.Parameters.Clear();
					Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
					Service.Parameters.Add("transactionNumber", product.transactionNumber);
					Service.Parameters.Add("lineNumber", product.lineNumber.ToString());
					product.holdItems = Service.GetList<Products>("ProductAccountHoldInfo");
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(products);
		}
	}
}