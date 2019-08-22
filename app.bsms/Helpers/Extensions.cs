using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace app.bsms.Helpers
{
	public static class Extensions
	{
		public static bool IsNullOrValue(this double? value, double valueToCheck)
		{
			double? nullable = value;
			return (nullable.HasValue ? nullable.GetValueOrDefault() : valueToCheck) == valueToCheck;
		}

		public static bool IsNullOrZero(this decimal? value)
		{
			if (value.HasValue)
			{
				decimal? nullable = value;
				decimal num = new decimal();
				if ((nullable.GetValueOrDefault() == num ? !nullable.HasValue : true))
				{
					return false;
				}
			}
			return true;
		}

		public static bool IsNullOrZero(this int? value)
		{
			if (value.HasValue)
			{
				int? nullable = value;
				if ((nullable.GetValueOrDefault() == 0 ? !nullable.HasValue : true))
				{
					return false;
				}
			}
			return true;
		}

		public static int Update<TSource>(this IEnumerable<TSource> source, Extensions.Func<TSource> update)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (update == null)
			{
				throw new ArgumentNullException("update");
			}
			if (typeof(TSource).IsValueType)
			{
				throw new NotSupportedException("value type elements are not supported by update.");
			}
			int num = 0;
			foreach (TSource tSource in source)
			{
				update(tSource);
				num++;
			}
			return num;
		}

		public static int Update<TSource>(this List<TSource> source, Extensions.Func<TSource> update)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (update == null)
			{
				throw new ArgumentNullException("update");
			}
			if (typeof(TSource).IsValueType)
			{
				throw new NotSupportedException("value type elements are not supported by update.");
			}
			int num = 0;
			foreach (TSource tSource in source)
			{
				update(tSource);
				num++;
			}
			return num;
		}

		public delegate void Func<TArg0>(TArg0 element);
	}
}