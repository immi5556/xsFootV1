using app.bsms;
using app.bsms.api;
using app.bsms.Common;
using app.bsms.Helpers;
using app.bsms.Models.Account;
using app.bsms.Models.Sales.Post;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace app.bsms.Controllers
{
	[NoCache]
    public class HomeController : BaseController
	{
		public HomeController()
		{
		}

		public ActionResult About()
		{
			((dynamic)base.ViewBag).Message = "Your application description page.";
			return base.View();
		}

		public ActionResult Contact()
		{
			((dynamic)base.ViewBag).Message = "Your contact page.";
			return base.View();
		}

		public ActionResult Index()
		{
			return base.View();
		}

		public ActionResult Login()
		{
			base.Session["Login_Details"] = new app.bsms.Models.Account.User();
			base.Session["Cart_Details"] = new List<Cart_Details>();
			return base.View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginModel model)
		{
			try
			{
				if (base.ModelState.IsValid)
				{
					Service.Parameters.Clear();
					Service.Parameters.Add("userName", model.UserName);
					Service.Parameters.Add("password", model.Password);
					Service.Parameters.Add("clientCode", model.ClientCode);
					Service.Parameters.Add("forgotPassword", string.Empty);
					app.bsms.Models.Account.User list = Service.Get<app.bsms.Models.Account.User>("login");
					if (list != null)
					{
						if (list.siteCode == string.Empty)
						{
							Alerts.body = "Login Unsuccessfull";
							Alerts.ErrorMessage = "Invalid Username or Password or Client Code";
							base.TempData["Message"] = Alerts.ErrorMessage;
						}
						else
						{
							Service.Parameters.Clear();
							Service.Parameters.Add("siteCode", list.siteCode);
							list.settings = Service.GetList<Settings>("settings");
							base.Session["Login_Details"] = list;
							return base.RedirectToAction("Index", "Dashboard");
						}
					}
				}
			}
			catch (Exception exception)
			{
				Alerts.body = "Login Unsuccessfull";
				Alerts.ErrorMessage = exception.Message;
				base.TempData["Message"] = Alerts.ErrorMessage;
			}
			return base.View();
		}

      

        public ActionResult LogOut()
		{
			base.Session["Login_Details"] = new app.bsms.Models.Account.User();
			base.Session["Cart_Details"] = new List<Cart_Details>();
			return base.RedirectToAction("Login", "Home");
		}

		public ActionResult SetCulture(string culture)
		{
			culture = CultureHelper.GetImplementedCulture(culture);
			HttpCookie item = base.Request.Cookies["_culture"];
			if (item == null)
			{
				item = new HttpCookie("_culture")
				{
					Value = culture,
					Expires = DateTime.Now.AddYears(1)
				};
			}
			else
			{
				item.Value = culture;
			}
			base.Response.Cookies.Add(item);

            return base.RedirectToAction("Login", "Home");
		}
	}
}