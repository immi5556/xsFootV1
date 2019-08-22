using app.bsms;
using app.bsms.Models.Reminder;
using app.bsms.Models.Treatment;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace app.bsms.Controllers
{
	[NoCache]
	public class ManageCustomerController : BaseController
	{
		public ManageCustomerController()
		{
		}

		public ActionResult AccountDetails()
		{
			return base.View();
		}

		public ActionResult AddIemUsage()
		{
			return base.View();
		}

		public ActionResult AppointmentHistory()
		{
			return base.View();
		}

		public ActionResult BodyMeasurement()
		{
			return base.View();
		}

		public ActionResult BodyTreatment(BodyTreatment body)
		{
			return base.View();
		}

		[HttpPost]
		public ActionResult BodyTreatment()
		{
			return base.View(new BodyTreatment());
		}

		public ActionResult BustMeasurement()
		{
			return base.View();
		}

		public ActionResult CreditNote()
		{
			return base.View();
		}

		public ActionResult DiagnosisMain()
		{
			return base.View();
		}

		public ActionResult Index()
		{
			return base.View();
		}

		public ActionResult InvoiceHistory()
		{
			return base.View();
		}

		public ActionResult NextAppointment()
		{
			return base.View();
		}

		public ActionResult PrepaidAccount()
		{
			return base.View();
		}

		public ActionResult ProductAccount()
		{
			return base.View();
		}

		public ActionResult RedeemtionScreenThree()
		{
			return base.View();
		}

		public ActionResult RedeemtionScreenTwo()
		{
			return base.View();
		}

		public ActionResult Reedemption()
		{
			return base.View();
		}

		public ActionResult Reminder()
		{
			Reminder reminder = new Reminder()
			{
				appointmentDateTime = DateTime.Now
			};
			Details detail = new Details()
			{
				DetailsId = 1,
				Description = "Test"
			};
			DateTime dateTime = DateTime.Now.AddDays(1);
			detail.NextApptDate = dateTime.ToString("yyyy/MM/dd");
			detail.Total = 10;
			detail.TrnNo = "6754edf";
			reminder.details = new List<Details>()
			{
				detail
			};
			reminder.remarks = "";
			return base.View(reminder);
		}

		public ActionResult ServiceAccount()
		{
			return base.View();
		}

		public ActionResult ServiceAccountDetail()
		{
			return base.View();
		}

		public ActionResult ServiceRedemption()
		{
			return base.View();
		}

		[HttpPost]
		public ActionResult SkinTreatment(SkinTreatment body)
		{
			return base.RedirectToAction("Index");
		}

		public ActionResult SkinTreatment()
		{
			return base.View(new SkinTreatment());
		}

		public ActionResult TreatmentFeedback()
		{
			return base.View();
		}

		public ActionResult TreatmentHistory()
		{
			return base.View();
		}
	}
}