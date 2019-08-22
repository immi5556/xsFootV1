using app.bsms.api;
using app.bsms.Models.General;
using app.bsms.Models.Manage.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace app.bsms.Common
{
	public static class Utility
	{
		private static Random random;

		static Utility()
		{
			Utility.random = new Random();
		}

		public static string GenerateToken(int length)
		{
			return new string((
				from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length)
				select s[Utility.random.Next(s.Length)]).ToArray<char>());
		}

		public static List<ListItems> GetBillOpsOptions()
		{
			List<ListItems> listItems;
			try
			{
				listItems = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = "Transaction",
						itemDesc = "Transactions"
					},
					new ListItems()
					{
						itemCode = "Suspend",
						itemDesc = "Suspended Transactions"
					},
					new ListItems()
					{
						itemCode = "Pending",
						itemDesc = "Pending Transactions"
					}
				};
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return listItems;
		}

		public static Register GetCustomer(string customerCode, string siteCode)
		{
			Register register;
			try
			{
				Service.Parameters.Clear();
				Service.Parameters.Add("customerCode", customerCode);
				Service.Parameters.Add("siteCode", siteCode);
				Service.Parameters.Add("page", "General");
				register = Service.Get<Register>("customer");
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return register;
		}

		public static decimal GetGST(bool inclusive, decimal amount, decimal taxPercentage)
		{
			decimal num;
			try
			{
				num = (!inclusive ? Math.Round((amount * taxPercentage) / new decimal(100), 2) : Math.Round(amount - (amount / taxPercentage), 2));
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return num;
		}

		public static List<ListItems> GetPrepaidTypes()
		{
			List<ListItems> listItems;
			try
			{
				listItems = new List<ListItems>()
				{
					new ListItems()
					{
						itemCode = "",
						itemDesc = ""
					},
					new ListItems()
					{
						itemCode = "Inclusive",
						itemDesc = "Inclusive"
					},
					new ListItems()
					{
						itemCode = "Exclusive",
						itemDesc = "Exclusive"
					}
				};
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return listItems;
		}

		public static string ToDelimitedString<S, T>(this IEnumerable<S> lst, Func<S, T> selector, string separator = ", ")
		{
			return string.Join<T>(separator, lst.Select<S, T>(selector));
		}

		public static string ToDelimitedString<T>(this IEnumerable<T> lst, string separator = ", ")
		{
			return lst.ToDelimitedString<T, T>((T p) => p, separator);
		}
	}
}