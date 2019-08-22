using app.bsms.Models.Manage.Measurements;
using app.bsms.Models.Manage.Service;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

namespace app.bsms.Controllers.Manage
{
	public class MeasurementsController : Controller
	{
		public MeasurementsController()
		{
		}

		public ActionResult Body(string id)
		{
			Diagnosis diagnosi = new Diagnosis();
			try
			{
				((dynamic)base.ViewBag).customerCode = id;
				diagnosi.treatments = new List<Treatment>();
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(diagnosi);
		}

		public ActionResult Bust(string id)
		{
			Diagnosis diagnosi = new Diagnosis();
			try
			{
				((dynamic)base.ViewBag).customerCode = id;
				diagnosi.treatments = new List<Treatment>();
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(diagnosi);
		}

		public ActionResult Diagnosis(string id)
		{
			Diagnosis diagnosi = new Diagnosis();
			try
			{
				((dynamic)base.ViewBag).customerCode = id;
				diagnosi.treatments = new List<Treatment>();
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(diagnosi);
		}
	}
}