using System;
using System.Runtime.CompilerServices;

namespace app.bsms.Models.General
{
	public class GetResponse<T>
	{
		public string error
		{
			get;
			set;
		}

		public T result
		{
			get;
			set;
		}

		public string success
		{
			get;
			set;
		}

		public GetResponse()
		{
		}
	}
}