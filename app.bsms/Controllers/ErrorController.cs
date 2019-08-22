using System;
using System.Web.Mvc;

namespace app.bsms.Controllers
{
	public class ErrorController : BaseController
	{
		public ErrorController()
		{
		}

		public ActionResult Index(Exception exception)
		{
			return base.View(exception);
		}

		public ActionResult NoPrivilege()
		{
			return base.View();
		}

		public ActionResult NotFound()
		{
			return base.View();
		}
	}
}