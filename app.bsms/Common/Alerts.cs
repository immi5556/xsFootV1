using System;

namespace app.bsms.Common
{
	public static class Alerts
	{
		public static string body;

		private static string _message;

		private static string _sucmessage;

		private static string _errmessage;

		private static string _infmessage;

		public static string ErrorMessage
		{
			get
			{
				return Alerts._errmessage;
			}
			set
			{
				Alerts._errmessage = string.Concat(new string[] { "<div class=\"alert alert-danger alert-dismissible fade show\" role=\"alert\"><h4 class=\"alert-heading\">Alert!</h4><p>", Alerts.body, "</p><hr><p class=\"mb-0\">", value, "</p><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button></div>" });
			}
		}

		public static string Information
		{
			get
			{
				return Alerts._infmessage;
			}
			set
			{
				Alerts._infmessage = string.Concat(new string[] { "<div class=\"alert alert-info alert-dismissible fade show\" role=\"alert\"><h4 class=\"alert-heading\">Information!</h4><p>", Alerts.body, "</p><hr><p class=\"mb-0\">", value, "</p><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button></div>" });
			}
		}

		public static string Message
		{
			get
			{
				return Alerts._message;
			}
			set
			{
				Alerts._message = string.Concat(new string[] { "<div class=\"alert alert-warning alert-dismissible fade show\" role=\"alert\"><h4 class=\"alert-heading\">Alert!</h4><p>", Alerts.body, "</p><hr><p class=\"mb-0\">", value, "</p><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button></div>" });
			}
		}

		public static string Success
		{
			get
			{
				return Alerts._sucmessage;
			}
			set
			{
				Alerts._sucmessage = string.Concat(new string[] { "<div class=\"alert alert-success alert-dismissible fade show\" role=\"alert\"><h4 class=\"alert-heading\">Success!</h4><p>", Alerts.body, "</p><hr><p class=\"mb-0\">", value, "</p><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button></div>" });
			}
		}

		static Alerts()
		{
			Alerts.body = string.Empty;
			Alerts._message = string.Empty;
			Alerts._sucmessage = string.Empty;
			Alerts._errmessage = string.Empty;
			Alerts._infmessage = string.Empty;
		}
	}
}