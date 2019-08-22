using app.bsms;
using System;
using System.Web.Mvc;

namespace app.bsms.Controllers
{
	[NoCache]
	public class DashboardController : BaseController
	{
		public DashboardController()
		{
		}

		public ActionResult Index()
		{
			return base.View();
		}
	}
}