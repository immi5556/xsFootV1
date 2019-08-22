using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

namespace app.bsms
{
	public class SubmitButtonSelector : ActionNameSelectorAttribute
	{
		public string Name
		{
			get;
			set;
		}

		public SubmitButtonSelector()
		{
		}

		public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
		{
			if (controllerContext.Controller.ValueProvider.GetValue(this.Name) != null)
			{
				return true;
			}
			return false;
		}
	}
}