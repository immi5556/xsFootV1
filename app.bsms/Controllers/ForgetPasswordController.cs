using app.bsms;
using System;
using System.Web;
using System.Web.Mvc;

namespace app.bsms.Controllers
{
	[NoCache]
	public class ForgetPasswordController : BaseController
	{
		public ForgetPasswordController()
		{
		}

		public ActionResult Index()
		{
			base.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
			base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			base.Response.Cache.SetNoStore();
			return base.View();
		}
	}
}