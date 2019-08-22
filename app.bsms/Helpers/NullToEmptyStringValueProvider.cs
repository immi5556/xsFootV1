using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;

namespace app.bsms.Helpers
{
	public class NullToEmptyStringValueProvider : IValueProvider
	{
		private PropertyInfo _MemberInfo;

		public NullToEmptyStringValueProvider(PropertyInfo memberInfo)
		{
			this._MemberInfo = memberInfo;
		}

		public object GetValue(object target)
		{
			object value = this._MemberInfo.GetValue(target);
			if (this._MemberInfo.PropertyType == typeof(string) && value == null)
			{
				value = "";
			}
			return value;
		}

		public void SetValue(object target, object value)
		{
			this._MemberInfo.SetValue(target, value);
		}
	}
}