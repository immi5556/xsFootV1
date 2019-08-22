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
	public class AccountsController : BaseController
	{
		public AccountsController()
		{
		}

		public ActionResult Details(string id, string id2)
		{
			Accounts account = new Accounts();
			try
			{
				((dynamic)base.ViewBag).customerCode = id;
				account.customerCode = id;
				account.treatmentCode = id2;
				account.siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode;
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", account.siteCode);
				Service.Parameters.Add("customerCode", id);
				Service.Parameters.Add("treatmentCode", id2);
				account.details = Service.GetList<Accounts>("ServiceAccountDetail");
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", account.siteCode);
				Service.Parameters.Add("customerCode", id);
				Service.Parameters.Add("treatmentParentCode", id2);
				account.info = Service.GetList<Accounts>("serviceAccountSummaryInfo");
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(account);
		}

        public ActionResult Topup(string id, string id2)
        {
            Accounts account = new Accounts();
            try
            {
                ((dynamic)base.ViewBag).customerCode = id;
                account.customerCode = id;
                account.treatmentCode = id2;
                account.siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode;
                Service.Parameters.Clear();
                Service.Parameters.Add("siteCode", account.siteCode);
                Service.Parameters.Add("customerCode", id);
                Service.Parameters.Add("treatmentCode", id2);
                account.details = Service.GetList<Accounts>("ServiceAccountDetail");
                Service.Parameters.Clear();
                Service.Parameters.Add("siteCode", account.siteCode);
                Service.Parameters.Add("customerCode", id);
                Service.Parameters.Add("treatmentParentCode", id2);
                account.info = Service.GetList<Accounts>("serviceAccountSummaryInfo");
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return base.View(account);
        }

        public ActionResult Index(string id)
		{
			List<Accounts> accounts = new List<Accounts>();
			try
			{
				((dynamic)base.ViewBag).customerCode = id;
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				Service.Parameters.Add("customerCode", id);
				accounts = Service.GetList<Accounts>("serviceAccountSummary");
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(accounts);
		}
	}
}