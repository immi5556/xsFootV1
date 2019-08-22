using System;
using System.Web;
using System.Web.Mvc;

namespace app.bsms
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public sealed class NoCacheAttribute : app.bsms.FilterAttribute, IResultFilter
	{
		public NoCacheAttribute()
		{
		}

		public new void OnResultExecuted(ResultExecutedContext filterContext)
		{
			HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
			cache.SetCacheability(HttpCacheability.NoCache);
			cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
			cache.SetValidUntilExpires(false);
			cache.SetExpires(DateTime.UtcNow.AddDays(-1));
			cache.SetNoStore();
			cache.AppendCacheExtension("private");
			cache.AppendCacheExtension("no-cache=Set-Cookie");
			cache.SetProxyMaxAge(TimeSpan.Zero);
		}
	}
}