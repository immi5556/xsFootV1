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
	public class DepartmentController : Controller
	{
		public DepartmentController()
		{
		}

		[HttpGet]
		public ActionResult Create()
		{
			Department department = new Department();
			try
			{
				department.siteCode = ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode;
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(department);
		}

        [HttpPost]
        public ActionResult Create(Department model)
        {
            ActionResult action;
            Department department = new Department();
            try
            {
                if (base.ModelState.IsValid)
                {
                    Service.Parameters.Clear();
                    if (!Service.Post("feDepartment", JsonConvert.SerializeObject(model)))
                    {
                        Alerts.body = "Sorry!, Unsuccessfull";
                        Alerts.ErrorMessage = "Unable to save new department record";
                        base.TempData["Message"] = Alerts.ErrorMessage;
                    }
                    else
                    {
                        Alerts.body = "Success!";
                        Alerts.Success = "Department Saved Successfully";
                        base.TempData["Message"] = Alerts.Success;
                        action = base.RedirectToAction("List", "Department");
                        return action;
                    }
                }
                return base.View(department);
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
			Department department = new Department();
			try
			{
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				Service.Parameters.Add("departmentId", id);
				department = Service.GetList<Department>("feDepartment").FirstOrDefault<Department>();
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(department);
		}

		[HttpPost]
		public ActionResult Edit(Department model)
		{
			ActionResult action;
			Department department = new Department();
			try
			{
				if (base.ModelState.IsValid)
				{
					Service.Parameters.Clear();
					if (!Service.Put("feDepartment", JsonConvert.SerializeObject(model)))
					{
						Alerts.body = "Sorry!, Unsuccessfull";
						Alerts.ErrorMessage = "Unable to save new department record";
						base.TempData["Message"] = Alerts.ErrorMessage;
					}
					else
					{
						Alerts.body = "Success!";
						Alerts.Success = "Department Saved Successfully";
						base.TempData["Message"] = Alerts.Success;
						action = base.RedirectToAction("List", "Department");
						return action;
					}
				}
				return base.View(department);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			//return action;
		}

		public ActionResult List()
		{
			List<Department> departments = new List<Department>();
			try
			{
				Service.Parameters.Clear();
				Service.Parameters.Add("siteCode", ((app.bsms.Models.Account.User)base.Session["Login_Details"]).siteCode);
				departments = Service.GetList<Department>("feDepartment");
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return base.View(departments);
		}
	}
}