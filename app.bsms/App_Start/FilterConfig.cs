using System;
using System.Web.Mvc;

namespace app.bsms
{
	public class FilterConfig
	{
		public FilterConfig()
		{
		}

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}