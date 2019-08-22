using app.bsms;
using app.bsms.api;
using app.bsms.Models.Account;
using app.bsms.Models.General;
using app.bsms.Models.Reports.DayEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace app.bsms.Controllers
{
	[NoCache]
	public class ReportController : BaseController
	{
		public ReportController()
		{
		}

		public ActionResult DailyReport()
		{
			Index index = new Index();
			try
			{
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				Service.GetList<app.bsms.Models.Sales.PaymentType>("paymentType");
				List<Staffs> staffs = new List<Staffs>();
				List<Staffs> list = Service.GetList<Staffs>("getStaffList");
				Service.Parameters.Add("reportDate", "2017/04/04");
				index.Sales = Service.GetList<app.bsms.Models.Reports.DayEnd.Sales>("dayEndReportSales");
				index.NonSales = Service.GetList<app.bsms.Models.Reports.DayEnd.Sales>("dayEndReportNonSales");
				index.Attendance = Service.GetList<Attendance>("dayEndReportAttendanceList");
				index.DepartmentSales = Service.GetList<DepartmentSales>("dayEndReportDeptSales");
				index.EmployeePerformance = Service.GetList<EmployeePerformance>("dayEndReportEmployeePerformance");
				(
					from f in index.EmployeePerformance
					select f.recordDetailType).Distinct<string>();
				List<Attendance> attendances = new List<Attendance>();
				List<Attendance> attendances1 = new List<Attendance>();
				attendances = (
					from f in index.Attendance
					where f.attendanceType == 1
					select f).ToList<Attendance>();
				attendances1 = (
					from f in index.Attendance
					where f.attendanceType == 0
					select f).ToList<Attendance>();
				var variable = 
					from s in list
					join i in attendances on new { siteCode = s.siteCode, staffCode = s.staffCode } equals new { siteCode = i.siteCode, staffCode = i.staffCode }
					join o in attendances1 on new { siteCode = s.siteCode, staffCode = s.staffCode } equals new { siteCode = o.siteCode, staffCode = o.staffCode }
					join p in index.EmployeePerformance on new { siteCode = s.siteCode, staffCode = s.staffCode } equals new { siteCode = p.siteCode, staffCode = p.staffCode }
					select new { staffName = s.staffName, timeIn = i.attendanceTime, Time_In = o.attendanceTime, recordDetailType = p.recordDetailType };
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(index);
		}

		public ActionResult DayEndReport()
		{
			Index index = new Index();
			try
			{
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				Service.GetList<app.bsms.Models.Sales.PaymentType>("paymentType");
				Service.Parameters.Add("reportDate", "2017/04/04");
				index.Sales = Service.GetList<app.bsms.Models.Reports.DayEnd.Sales>("dayEndReportSales");
				index.NonSales = Service.GetList<app.bsms.Models.Reports.DayEnd.Sales>("dayEndReportNonSales");
				index.Attendance = Service.GetList<Attendance>("dayEndReportAttendanceList");
				index.DepartmentSales = Service.GetList<DepartmentSales>("dayEndReportDeptSales");
				index.EmployeePerformance = Service.GetList<EmployeePerformance>("dayEndReportEmployeePerformance");
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(index);
		}
	}
}