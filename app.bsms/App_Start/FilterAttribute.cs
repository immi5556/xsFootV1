using app.bsms.api;
using app.bsms.Models.Account;
using System;
using System.Web;
using System.Web.Mvc;

namespace app.bsms
{
	public class FilterAttribute : ActionFilterAttribute
	{
		public FilterAttribute()
		{
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (filterContext.HttpContext.Session["token"] == null)
			{
				filterContext.HttpContext.Session["token"] = Service.GetToken();
			}
			else if (((TokenInfo)filterContext.HttpContext.Session["token"]).token_expires_datetime <= DateTime.Now)
			{
				filterContext.HttpContext.Session["token"] = Service.GetToken();
			}
			if (filterContext.HttpContext.Session["Login_Details"] == null)
			{
				filterContext.HttpContext.Response.Redirect("~/Home/Login", false);
				return;
			}
			string actionName = filterContext.ActionDescriptor.ActionName;
			string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
			if (actionName != "Login")
			{
				User item = (User)filterContext.HttpContext.Session["Login_Details"];
				if (item.siteCode == null || item.siteCode == string.Empty)
				{
					filterContext.HttpContext.Response.Redirect("~/Home/Login", false);
				}
			}
		}
	}
}