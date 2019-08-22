using app.bsms.Helpers;
using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace app.bsms.Controllers
{
	public class BaseController : Controller
	{
		public BaseController()
		{
		}

		protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
		{
			string userLanguages;
			base.TempData["Message"] = null;
			string value = null;
			HttpCookie item = base.Request.Cookies["_culture"];
			if (item == null)
			{
				if (base.Request.UserLanguages == null || base.Request.UserLanguages.Length == 0)
				{
					userLanguages = null;
				}
				else
				{
					userLanguages = base.Request.UserLanguages[0];
				}
				value = userLanguages;
			}
			else
			{
				value = item.Value;
			}
			value = CultureHelper.GetImplementedCulture(value);
			Thread.CurrentThread.CurrentCulture = new CultureInfo(value);
			Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
			return base.BeginExecuteCore(callback, state);
		}
	}
}