using app.bsms.api;
using app.bsms.Common;
using app.bsms.Models.Account;
using app.bsms.Models.Administration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace app.bsms.Controllers.Administration
{
	public class RoleAccessController : Controller
	{
		public RoleAccessController()
		{
		}

		[HttpGet]
		public ActionResult Create()
		{
			RoleAccess roleAccess = new RoleAccess();
			try
			{
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				roleAccess.lstDepartment.AddRange(Service.GetList<Department>("feDepartment"));
				roleAccess.lstRoleGroup.AddRange(Service.GetList<RoleGroup>("roleGroupWithFilter"));
				roleAccess.siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode;
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(roleAccess);
		}

		[HttpPost]
		public ActionResult Create(RoleAccess model)
		{
			ActionResult action;
			RoleAccess roleAccess = new RoleAccess();
			try
			{
				if (base.ModelState.IsValid)
				{
					Service.Parameters.Clear();
					if (!Service.Post("RoleAccessWithFilter", JsonConvert.SerializeObject(model)))
					{
						Alerts.body = "Sorry!, Unsuccessfull";
						Alerts.ErrorMessage = "Unable to save new role group record";
						base.TempData["Message"] = Alerts.ErrorMessage;
					}
					else
					{
						Alerts.body = "Success!";
						Alerts.Success = "Role Group Saved Successfully";
						base.TempData["Message"] = Alerts.Success;
						action = base.RedirectToAction("List", "RoleAccess");
						return action;
					}
				}
				return base.View(roleAccess);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			//return action;
		}

		[HttpGet]
		public ActionResult Edit(string id)
		{
			RoleAccess roleAccess = new RoleAccess();
			try
			{
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				Service.Parameters.Add("roleId", id);
				roleAccess = Service.GetList<RoleAccess>("RoleAccessWithFilter").FirstOrDefault<RoleAccess>();
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				roleAccess.lstDepartment.AddRange(Service.GetList<Department>("feDepartment"));
				roleAccess.lstRoleGroup.AddRange(Service.GetList<RoleGroup>("roleGroupWithFilter"));
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(roleAccess);
		}

		[HttpPost]
		public ActionResult Edit(RoleAccess model)
		{
			ActionResult action;
			RoleAccess roleAccess = new RoleAccess();
			try
			{
				if (base.ModelState.IsValid)
				{
					Service.Parameters.Clear();
					if (!Service.Put("RoleAccessWithFilter", JsonConvert.SerializeObject(model)))
					{
						Alerts.body = "Sorry!, Unsuccessfull";
						Alerts.ErrorMessage = "Unable to save new department record";
						base.TempData["Message"] = Alerts.ErrorMessage;
					}
					else
					{
						Alerts.body = "Success!";
						Alerts.Success = "userGroup Saved Successfully";
						base.TempData["Message"] = Alerts.Success;
						action = base.RedirectToAction("List", "RoleAccess");
						return action;
					}
				}
				return base.View(roleAccess);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			//return action;
		}

		public ActionResult List()
		{
			List<RoleAccess> roleAccesses = new List<RoleAccess>();
			try
			{
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				roleAccesses = Service.GetList<RoleAccess>("RoleAccessWithFilter");
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(roleAccesses);
		}
	}
}