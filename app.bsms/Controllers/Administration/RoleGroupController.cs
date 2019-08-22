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
	public class RoleGroupController : Controller
	{
		public RoleGroupController()
		{
		}

		[HttpGet]
		public ActionResult Create()
		{
			RoleGroup roleGroup = new RoleGroup();
			try
			{
				roleGroup.siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode;
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(roleGroup);
		}

		[HttpPost]
		public ActionResult Create(RoleGroup model)
		{
			ActionResult action;
			RoleGroup roleGroup = new RoleGroup();
			try
			{
				if (base.ModelState.IsValid)
				{
					Service.Parameters.Clear();
					if (!Service.Post("roleGroupWithFilter", JsonConvert.SerializeObject(model)))
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
						action = base.RedirectToAction("List", "RoleGroup");
						return action;
					}
				}
				return base.View(roleGroup);
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
			RoleGroup roleGroup = new RoleGroup();
			try
			{
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				Service.Parameters.Add("roleGroupId", id);
				roleGroup = Service.GetList<RoleGroup>("roleGroupWithFilter").FirstOrDefault<RoleGroup>();
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(roleGroup);
		}

		[HttpPost]
		public ActionResult Edit(RoleGroup model)
		{
			ActionResult action;
			RoleGroup roleGroup = new RoleGroup();
			try
			{
				if (base.ModelState.IsValid)
				{
					Service.Parameters.Clear();
					if (!Service.Put("roleGroupWithFilter", JsonConvert.SerializeObject(model)))
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
						action = base.RedirectToAction("List", "RoleGroup");
						return action;
					}
				}
				return base.View(roleGroup);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			//return action;
		}

		public ActionResult List()
		{
			List<RoleGroup> roleGroups = new List<RoleGroup>();
			try
			{
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				roleGroups = Service.GetList<RoleGroup>("roleGroupWithFilter");
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(roleGroups);
		}
	}
}