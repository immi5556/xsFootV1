using app.bsms.api;
using app.bsms.Common;
using app.bsms.Models.Account;
using app.bsms.Models.Administration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace app.bsms.Controllers.Administration
{
	public class MenuController : Controller
	{
		public MenuController()
		{
		}

		[HttpGet]
		public ActionResult Create()
		{
			Menu menu = new Menu();
			try
			{
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				menu.lstParentMenus.AddRange(
					from f in Service.GetList<Menu>("menuWithFilter")
					where !f.menuParentId.HasValue
					select f);
				menu.lstSubmenus.AddRange(
					from f in Service.GetList<Menu>("menuWithFilter")
					where f.menuParentId.HasValue
					select f);
				menu.siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode;
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(menu);
		}

		[HttpPost]
		public ActionResult Create(Menu model)
		{
			ActionResult action;
			Menu menu = new Menu();
			try
			{
				if (base.ModelState.IsValid)
				{
					Service.Parameters.Clear();
					if (!Service.Post("MenuWithFilter", JsonConvert.SerializeObject(model)))
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
						action = base.RedirectToAction("List", "Menu");
						return action;
					}
				}
				return base.View(menu);
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
			Menu menu = new Menu();
			try
			{
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				Service.Parameters.Add("menuId", id);
				menu = Service.GetList<Menu>("MenuWithFilter").FirstOrDefault<Menu>();
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				menu.lstParentMenus.AddRange(
					from f in Service.GetList<Menu>("menuWithFilter")
					where !f.menuParentId.HasValue
					select f);
				menu.lstSubmenus.AddRange(
					from f in Service.GetList<Menu>("menuWithFilter")
					where f.menuParentId.HasValue
					select f);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(menu);
		}

        [HttpPost]
        public ActionResult Edit(Menu model)
        {
            ActionResult action;
            Menu menu = new Menu();
            try
            {
                if (base.ModelState.IsValid)
                {
                    Service.Parameters.Clear();
                    if (!Service.Put("MenuWithFilter", JsonConvert.SerializeObject(model)))
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
                        action = base.RedirectToAction("List", "Menu");
                        return action;
                    }
                }
                return base.View(menu);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            //return action;
        }

        public ActionResult List()
		{
			List<Menu> menus = new List<Menu>();
			try
			{
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				menus = Service.GetList<Menu>("MenuWithFilter");
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(menus);
		}
	}
}