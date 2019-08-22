using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace app.bsms.Models.General
{
	public class GetResponseList<T>
	{
		public string error
		{
			get;
			set;
		}

		public List<T> result
		{
			get;
			set;
		}

		public string success
		{
			get;
			set;
		}

		public GetResponseList()
		{
		}
	}
}