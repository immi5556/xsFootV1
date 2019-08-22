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
	public class VoucherController : BaseController
	{
		public VoucherController()
		{
		}

		public ActionResult Index(string id)
		{
			List<Voucher> vouchers = new List<Voucher>();
			try
			{
				((dynamic)base.ViewBag).customerCode = id;
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				Service.Parameters.Add("customerCode", id);
				vouchers = Service.GetList<Voucher>("getVoucherRecord");
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(vouchers);
		}
	}
}