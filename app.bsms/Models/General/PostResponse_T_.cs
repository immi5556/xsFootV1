using System;
using System.Runtime.CompilerServices;

namespace app.bsms.Models.General
{
	public class PostResponse<T>
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

		public PostResponse()
		{
		}
	}
}