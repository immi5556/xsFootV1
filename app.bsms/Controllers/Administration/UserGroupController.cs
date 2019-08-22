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
	public class UserGroupController : Controller
	{
		public UserGroupController()
		{
		}

		[HttpGet]
		public ActionResult Create()
		{
			UserGroup userGroup = new UserGroup();
			try
			{
				userGroup.siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode;
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(userGroup);
		}

		[HttpPost]
		public ActionResult Create(UserGroup model)
		{
			ActionResult action;
			UserGroup userGroup = new UserGroup();
			try
			{
				if (base.ModelState.IsValid)
				{
					Service.Parameters.Clear();
					model.CDT = new DateTime?(DateTime.Now.Date);
					model.CID = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).userID;
					if (!Service.Post("userGroup", JsonConvert.SerializeObject(model)))
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
						action = base.RedirectToAction("List", "UserGroup");
						return action;
					}
				}
				return base.View(userGroup);
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
			UserGroup userGroup = new UserGroup();
			try
			{
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				Service.Parameters.Add("roleGroupId", id);
				userGroup = Service.GetList<UserGroup>("userGroup").FirstOrDefault<UserGroup>();
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(userGroup);
		}

		[HttpPost]
		public ActionResult Edit(UserGroup model)
		{
			ActionResult action;
			UserGroup userGroup = new UserGroup();
			try
			{
				if (base.ModelState.IsValid)
				{
					Service.Parameters.Clear();
					model.CDT = new DateTime?(DateTime.Now.Date);
					model.CID = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).userID;
					if (!Service.Put("userGroup", JsonConvert.SerializeObject(model)))
					{
						Alerts.body = "Sorry!, Unsuccessfull";
						Alerts.ErrorMessage = "Unable to save new department record";
						base.TempData["Message"] = Alerts.ErrorMessage;
					}
					else
					{
						Alerts.body = "Success!";
						Alerts.Success = "User Group Saved Successfully";
						base.TempData["Message"] = Alerts.Success;
						action = base.RedirectToAction("List", "UserGroup");
						return action;
					}
				}
				return base.View(userGroup);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			//return action;
		}

		public ActionResult List()
		{
			List<UserGroup> userGroups = new List<UserGroup>();
			try
			{
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				userGroups = Service.GetList<UserGroup>("userGroup");
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(userGroups);
		}
	}
}