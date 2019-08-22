using app.bsms;
using System;
using System.Web.Mvc;

namespace app.bsms.Controllers
{
	[NoCache]
	public class ProductController : BaseController
	{
		public ProductController()
		{
		}

		public ActionResult Index()
		{
			return base.View();
		}

		public ActionResult ProductDetails()
		{
			return base.View();
		}
	}
}