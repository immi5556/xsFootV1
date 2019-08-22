using app.bsms.Controllers;
using ExpressiveAnnotations.Attributes;
using ExpressiveAnnotations.MvcUnobtrusive.Validators;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace app.bsms
{
	public class MvcApplication : HttpApplication
	{
		public MvcApplication()
		{
		}

		protected void Application_Error(object sender, EventArgs e)
		{
			Exception lastError = base.Server.GetLastError();
			base.Server.ClearError();
			RouteData routeDatum = new RouteData();
			routeDatum.Values.Add("controller", "Error");
			routeDatum.Values.Add("action", "Index");
			if (lastError.GetType() != typeof(HttpException))
			{
				routeDatum.Values.Add("exception", lastError);
			}
			else
			{
				routeDatum.Values.Add("exception", (HttpException)lastError);
			}
			((IController)(new ErrorController())).Execute(new RequestContext(new HttpContextWrapper(base.Context), routeDatum));
			base.Response.End();
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			GlobalFilters.Filters.Add(new app.bsms.FilterAttribute());
			DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfAttribute), typeof(RequiredIfValidator));
			DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(AssertThatAttribute), typeof(AssertThatValidator));
		}
	}
}