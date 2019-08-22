using app.bsms.Helpers;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace app.bsms
{
	public class RouteConfig
	{
		public RouteConfig()
		{
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapRoute("Default", "{controller}/{action}/{id}/{id2}", new { culture = CultureHelper.GetDefaultCulture(), controller = "Home", action = "Login", id = UrlParameter.Optional, id2 = UrlParameter.Optional });
		}
	}
}