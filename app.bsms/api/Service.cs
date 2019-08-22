using app.bsms.Common;
using app.bsms.Models.Account;
using app.bsms.Models.General;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace app.bsms.api
{
	public static class Service
	{
		private static string tokenurl;

		private static string url;

		public static JavaScriptSerializer Serializer;

		public static Dictionary<string, string> Parameters;

        static Service()
		{
			Service.tokenurl = string.Concat(ConfigurationManager.AppSettings["apiURL"].ToString(), "/token");
			Service.url = string.Concat(ConfigurationManager.AppSettings["apiURL"].ToString(), "/api/");
			Service.Serializer = new JavaScriptSerializer();
			Service.Parameters = new Dictionary<string, string>();
    }

		public static string Delete(string methodName)
		{
			string str;
			try
			{
                CookieContainer cookieContainer = new CookieContainer();
                string empty = string.Empty;
				if (!string.IsNullOrEmpty(methodName))
				{
                    //Yoonus
                    if (HttpContext.Current.Session["token"] == null)
                    {
                        HttpContext.Current.Session["token"] = Service.GetToken();
                    }
                    else if (((TokenInfo)HttpContext.Current.Session["token"]).token_expires_datetime <= DateTime.Now)
                    {
                        HttpContext.Current.Session["token"] = Service.GetToken();
                    }
                    //Yoonus

                    StringBuilder stringBuilder = new StringBuilder();
					if (Service.Parameters.Count > 0)
					{
						foreach (KeyValuePair<string, string> parameter in Service.Parameters)
						{
							stringBuilder.Append(string.Concat((stringBuilder.ToString() == string.Empty ? "?" : "&"), parameter.Key, "=", parameter.Value));
						}
						methodName = string.Concat(methodName, stringBuilder.ToString());
					}
					HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Concat(Service.url, methodName));
					httpWebRequest.Method = "DELETE";
					httpWebRequest.ContentType = "application/json";

                    TokenInfo item = (TokenInfo)HttpContext.Current.Session["token"];
                    httpWebRequest.Headers.Add("Authorization", string.Concat(item.token_type, " ", item.access_token));
					using (WebResponse response = httpWebRequest.GetResponse())
					{
						using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
						{
							empty = streamReader.ReadToEnd();
						}

                        response.Close();
                        response.Dispose();
                    }
                }
				str = empty;
			}
			catch (Exception exception)
			{
				throw exception;
			}
            return str;
		}

		public static string Get(string methodName)
		{
			string str;
			try
			{
				string empty = string.Empty;
				if (!string.IsNullOrEmpty(methodName))
				{
                    //Yoonus
                    if (HttpContext.Current.Session["token"] == null)
                    {
                        HttpContext.Current.Session["token"] = Service.GetToken();
                    }
                    else if (((TokenInfo)HttpContext.Current.Session["token"]).token_expires_datetime <= DateTime.Now)
                    {
                        HttpContext.Current.Session["token"] = Service.GetToken();
                    }
                    //Yoonus

                    StringBuilder stringBuilder = new StringBuilder();
					if (Service.Parameters.Count > 0)
					{
						foreach (KeyValuePair<string, string> parameter in Service.Parameters)
						{
							stringBuilder.Append(string.Concat((stringBuilder.ToString() == string.Empty ? "?" : "&"), parameter.Key, "=", parameter.Value));
						}
						methodName = string.Concat(methodName, stringBuilder.ToString());
					}
					HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Concat(Service.url, methodName));
					httpWebRequest.Method = "GET";
					httpWebRequest.ContentType = "application/json";

                    TokenInfo item = (TokenInfo)HttpContext.Current.Session["token"];
					httpWebRequest.Headers.Add("Authorization", string.Concat(item.token_type, " ", item.access_token));
					using (WebResponse response = httpWebRequest.GetResponse())
					{
						using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
						{
							empty = streamReader.ReadToEnd();
						}
                        response.Close();
                        response.Dispose();
                    }
                }
				str = empty;
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return str;
		}

		public static T Get<T>(string methodName)
		{
			GetResponse<T> getResponse = new GetResponse<T>();
			try
			{
				if (!string.IsNullOrEmpty(methodName))
				{
                    //Yoonus
                    if (HttpContext.Current.Session["token"] == null)
                    {
                        HttpContext.Current.Session["token"] = Service.GetToken();
                    }
                    else if (((TokenInfo)HttpContext.Current.Session["token"]).token_expires_datetime <= DateTime.Now)
                    {
                        HttpContext.Current.Session["token"] = Service.GetToken();
                    }
                    //Yoonus

                    StringBuilder stringBuilder = new StringBuilder();
					if (Service.Parameters.Count > 0)
					{
						foreach (KeyValuePair<string, string> parameter in Service.Parameters)
						{
							stringBuilder.Append(string.Concat((stringBuilder.ToString() == string.Empty ? "?" : "&"), parameter.Key, "=", parameter.Value));
						}
						methodName = string.Concat(methodName, stringBuilder.ToString());
					}
					HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Concat(Service.url, methodName));
					httpWebRequest.Method = "GET";
					httpWebRequest.ContentType = "application/json";

                    TokenInfo item = (TokenInfo)HttpContext.Current.Session["token"];
					httpWebRequest.Headers.Add("Authorization", string.Concat(item.token_type, " ", item.access_token));
					using (WebResponse response = httpWebRequest.GetResponse())
					{
						using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
						{
							getResponse = JsonConvert.DeserializeObject<GetResponse<T>>(streamReader.ReadToEnd());
						}
						if (getResponse == null)
						{
							getResponse = new GetResponse<T>();
						}
						if (getResponse.success != "1")
						{
							getResponse = new GetResponse<T>()
							{
								result = (T)Activator.CreateInstance(Type.GetType(string.Concat(typeof(T).ToString(), ", app.bsms")))
							};
						}
					}
				}
			}
			catch (WebException webException)
			{
				using (Stream responseStream = webException.Response.GetResponseStream())
				{
					(new StreamReader(responseStream)).ReadToEnd();
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return getResponse.result;
		}

		public static List<T> GetList<T>(string methodName)
		{
			GetResponseList<T> getResponseList = new GetResponseList<T>();
			try
			{
				if (!string.IsNullOrEmpty(methodName))
				{
                    //Yoonus
                    if (HttpContext.Current.Session["token"] == null)
                    {
                        HttpContext.Current.Session["token"] = Service.GetToken();
                    }
                    else if (((TokenInfo)HttpContext.Current.Session["token"]).token_expires_datetime <= DateTime.Now)
                    {
                        HttpContext.Current.Session["token"] = Service.GetToken();
                    }
                    //Yoonus

                    StringBuilder stringBuilder = new StringBuilder();
					if (Service.Parameters.Count > 0)
					{
						foreach (KeyValuePair<string, string> parameter in Service.Parameters)
						{
							stringBuilder.Append(string.Concat((stringBuilder.ToString() == string.Empty ? "?" : "&"), parameter.Key, "=", parameter.Value));
						}
						methodName = string.Concat(methodName, stringBuilder.ToString());
					}
					HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Concat(Service.url, methodName));
					httpWebRequest.Method = "GET";
					httpWebRequest.ContentType = "application/json";

                    TokenInfo item = (TokenInfo)HttpContext.Current.Session["token"];
					httpWebRequest.Headers.Add("Authorization", string.Concat(item.token_type, " ", item.access_token));
					using (WebResponse response = httpWebRequest.GetResponse())
					{
						using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
						{
							getResponseList = JsonConvert.DeserializeObject<GetResponseList<T>>(streamReader.ReadToEnd());
						}
						if (getResponseList == null)
						{
							getResponseList = new GetResponseList<T>()
							{
								result = new List<T>()
							};
						}
						if (getResponseList.success != "1")
						{
							getResponseList.result = new List<T>();
						}
					}
				}
			}
			catch (WebException webException)
			{
				using (Stream responseStream = webException.Response.GetResponseStream())
				{
					(new StreamReader(responseStream)).ReadToEnd();
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return getResponseList.result;
		}

		public static string GetMedia(string methodName)
		{
			string str;
			try
			{
				string empty = string.Empty;
				if (!string.IsNullOrEmpty(methodName))
				{
                    //Yoonus
                    if (HttpContext.Current.Session["token"] == null)
                    {
                        HttpContext.Current.Session["token"] = Service.GetToken();
                    }
                    else if (((TokenInfo)HttpContext.Current.Session["token"]).token_expires_datetime <= DateTime.Now)
                    {
                        HttpContext.Current.Session["token"] = Service.GetToken();
                    }
                    //Yoonus

                    StringBuilder stringBuilder = new StringBuilder();
					if (Service.Parameters.Count > 0)
					{
						foreach (KeyValuePair<string, string> parameter in Service.Parameters)
						{
							stringBuilder.Append(string.Concat((stringBuilder.ToString() == string.Empty ? "?" : "&"), parameter.Key, "=", parameter.Value));
						}
						methodName = string.Concat(methodName, stringBuilder.ToString());
					}
					HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Concat(Service.url, methodName));
					httpWebRequest.Method = "GET";
					httpWebRequest.ContentType = "image/png";

                    TokenInfo item = (TokenInfo)HttpContext.Current.Session["token"];
					httpWebRequest.Headers.Add("Authorization", string.Concat(item.token_type, " ", item.access_token));
					using (WebResponse response = httpWebRequest.GetResponse())
					{
						using (BinaryReader binaryReader = new BinaryReader(response.GetResponseStream()))
						{
							byte[] numArray = binaryReader.ReadBytes(10485760);
							empty = string.Concat("data:image/png;base64,", Convert.ToBase64String(numArray));
						}
					}
				}
				str = empty;
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return str;
		}

		public static TokenInfo GetToken()
		{
			TokenInfo tokenInfo;
			try
			{
				TokenInfo tokenInfo1 = new TokenInfo();
				byte[] bytes = Encoding.ASCII.GetBytes("grant_type=password&userName=vsteam&password=lkas0n1v");
				HttpWebRequest length = (HttpWebRequest)WebRequest.Create(Service.tokenurl);
				length.Method = "POST";
				length.ContentType = "application/x-www-form-urlencoded";
				length.ContentLength = (long)((int)bytes.Length);
				using (Stream requestStream = length.GetRequestStream())
				{
					requestStream.Write(bytes, 0, (int)bytes.Length);
				}
				using (WebResponse response = length.GetResponse())
				{
					using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
					{
						tokenInfo1 = JsonConvert.DeserializeObject<TokenInfo>(streamReader.ReadToEnd());
					}
				}
				if (tokenInfo1.error != null)
				{
					tokenInfo = tokenInfo1;
				}
				else
				{
					tokenInfo1.token_expires_datetime = DateTime.Now.AddSeconds(tokenInfo1.expires_in);
					tokenInfo = tokenInfo1;
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return tokenInfo;
		}

		public static bool Post(string methodName, string data)
		{
			bool flag;
			try
			{
				PostResponse postResponse = new PostResponse();
				if (!string.IsNullOrEmpty(methodName))
				{
                    //Yoonus
                    if (HttpContext.Current.Session["token"] == null)
                    {
                        HttpContext.Current.Session["token"] = Service.GetToken();
                    }
                    else if (((TokenInfo)HttpContext.Current.Session["token"]).token_expires_datetime <= DateTime.Now)
                    {
                        HttpContext.Current.Session["token"] = Service.GetToken();
                    }
                    //Yoonus

                    StringBuilder stringBuilder = new StringBuilder();
					if (Service.Parameters.Count > 0)
					{
						foreach (KeyValuePair<string, string> parameter in Service.Parameters)
						{
							stringBuilder.Append(string.Concat((stringBuilder.ToString() == string.Empty ? "?" : "&"), parameter.Key, "=", parameter.Value));
						}
						methodName = string.Concat(methodName, stringBuilder.ToString());
					}
					byte[] bytes = Encoding.ASCII.GetBytes(data);
					HttpWebRequest length = (HttpWebRequest)WebRequest.Create(string.Concat(Service.url, methodName));
					length.Method = "POST";
					length.ContentType = "application/json";

                    TokenInfo item = (TokenInfo)HttpContext.Current.Session["token"];
					length.Headers.Add("Authorization", string.Concat(item.token_type, " ", item.access_token));
					length.ContentLength = (long)((int)bytes.Length);
					using (Stream requestStream = length.GetRequestStream())
					{
						requestStream.Write(bytes, 0, (int)bytes.Length);
					}
					using (WebResponse response = length.GetResponse())
					{
						using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
						{
							postResponse = JsonConvert.DeserializeObject<PostResponse>(streamReader.ReadToEnd());
						}
					}
				}
				flag = (postResponse.success == "1" ? true : false);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return flag;
		}

		public static T Post<T>(string methodName, string data)
		{
			T t;
			try
			{
				PostResponse<T> postResponse = new PostResponse<T>();
				if (!string.IsNullOrEmpty(methodName))
				{
                    //Yoonus
                    if (HttpContext.Current.Session["token"] == null)
                    {
                        HttpContext.Current.Session["token"] = Service.GetToken();
                    }
                    else if (((TokenInfo)HttpContext.Current.Session["token"]).token_expires_datetime <= DateTime.Now)
                    {
                        HttpContext.Current.Session["token"] = Service.GetToken();
                    }
                    //Yoonus

                    StringBuilder stringBuilder = new StringBuilder();
					if (Service.Parameters.Count > 0)
					{
						foreach (KeyValuePair<string, string> parameter in Service.Parameters)
						{
							stringBuilder.Append(string.Concat((stringBuilder.ToString() == string.Empty ? "?" : "&"), parameter.Key, "=", parameter.Value));
						}
						methodName = string.Concat(methodName, stringBuilder.ToString());
					}
					byte[] bytes = Encoding.ASCII.GetBytes(data);
					HttpWebRequest length = (HttpWebRequest)WebRequest.Create(string.Concat(Service.url, methodName));
					length.Method = "POST";
					length.ContentType = "application/json";

                    TokenInfo item = (TokenInfo)HttpContext.Current.Session["token"];
					length.Headers.Add("Authorization", string.Concat(item.token_type, " ", item.access_token));
					length.ContentLength = (long)((int)bytes.Length);
					using (Stream requestStream = length.GetRequestStream())
					{
						requestStream.Write(bytes, 0, (int)bytes.Length);
					}
					using (WebResponse response = length.GetResponse())
					{
						using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
						{
							postResponse = JsonConvert.DeserializeObject<PostResponse<T>>(streamReader.ReadToEnd());
						}
					}
				}
				if (postResponse.success != "1")
				{
					T postResponse1 = (new PostResponse<T>()).result;
					T t1 = postResponse1;
					postResponse.result = postResponse1;
					t = t1;
				}
				else
				{
					t = postResponse.result;
                }
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return t;
		}

		public static List<T> PostRead<T>(string methodName, string data)
		{
			GetResponseList<T> getResponseList = new GetResponseList<T>();
			try
			{
				if (!string.IsNullOrEmpty(methodName))
				{
                    //Yoonus
                    if (HttpContext.Current.Session["token"] == null)
                    {
                        HttpContext.Current.Session["token"] = Service.GetToken();
                    }
                    else if (((TokenInfo)HttpContext.Current.Session["token"]).token_expires_datetime <= DateTime.Now)
                    {
                        HttpContext.Current.Session["token"] = Service.GetToken();
                    }
                    //Yoonus

                    StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("{");
					if (Service.Parameters.Count > 0)
					{
						foreach (KeyValuePair<string, string> parameter in Service.Parameters)
						{
							stringBuilder.Append(string.Concat(new string[] { "\"", parameter.Key, "\":\"", parameter.Value, "\"," }));
						}
					}
					stringBuilder.Append("}");
					byte[] bytes = Encoding.ASCII.GetBytes(data);
					HttpWebRequest length = (HttpWebRequest)WebRequest.Create(string.Concat(Service.url, methodName));
					length.Method = "POST";
					length.ContentType = "application/json";

                    TokenInfo item = (TokenInfo)HttpContext.Current.Session["token"];
					length.Headers.Add("Authorization", string.Concat(item.token_type, " ", item.access_token));
					length.ContentLength = (long)((int)bytes.Length);
					using (Stream requestStream = length.GetRequestStream())
					{
						requestStream.Write(bytes, 0, (int)bytes.Length);
					}
					using (WebResponse response = length.GetResponse())
					{
						using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
						{
							getResponseList = JsonConvert.DeserializeObject<GetResponseList<T>>(streamReader.ReadToEnd());
						}
						if (getResponseList == null)
						{
							getResponseList = new GetResponseList<T>()
							{
								result = new List<T>()
							};
						}
						if (getResponseList.success != "1")
						{
							getResponseList.result = new List<T>();
						}
					}
				}
			}
			catch (WebException webException)
			{
				using (Stream responseStream = webException.Response.GetResponseStream())
				{
					(new StreamReader(responseStream)).ReadToEnd();
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return getResponseList.result;
		}

		public static bool Put(string methodName, string data)
		{
			bool flag;
			try
			{
				PostResponse postResponse = new PostResponse();
				if (!string.IsNullOrEmpty(methodName))
				{
                    //Yoonus
                    if (HttpContext.Current.Session["token"] == null)
                    {
                        HttpContext.Current.Session["token"] = Service.GetToken();
                    }
                    else if (((TokenInfo)HttpContext.Current.Session["token"]).token_expires_datetime <= DateTime.Now)
                    {
                        HttpContext.Current.Session["token"] = Service.GetToken();
                    }
                    //Yoonus

                    StringBuilder stringBuilder = new StringBuilder();
					if (Service.Parameters.Count > 0)
					{
						foreach (KeyValuePair<string, string> parameter in Service.Parameters)
						{
							stringBuilder.Append(string.Concat((stringBuilder.ToString() == string.Empty ? "?" : "&"), parameter.Key, "=", parameter.Value));
						}
						methodName = string.Concat(methodName, stringBuilder.ToString());
					}
					byte[] bytes = Encoding.ASCII.GetBytes(data);
					HttpWebRequest length = (HttpWebRequest)WebRequest.Create(string.Concat(Service.url, methodName));
					length.Method = "PUT";
					length.ContentType = "application/json";

                    TokenInfo item = (TokenInfo)HttpContext.Current.Session["token"];
					length.Headers.Add("Authorization", string.Concat(item.token_type, " ", item.access_token));
					length.ContentLength = (long)((int)bytes.Length);
					using (Stream requestStream = length.GetRequestStream())
					{
						requestStream.Write(bytes, 0, (int)bytes.Length);
					}
					using (WebResponse response = length.GetResponse())
					{
						using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
						{
							postResponse = JsonConvert.DeserializeObject<PostResponse>(streamReader.ReadToEnd());
						}
					}
				}
				flag = (postResponse.success == "1" ? true : false);
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return flag;
		}

		public static T Put<T>(string methodName, string data)
		{
			T t;
			try
			{
				PostResponse<T> postResponse = new PostResponse<T>();
				if (!string.IsNullOrEmpty(methodName))
				{
                    //Yoonus
                    if (HttpContext.Current.Session["token"] == null)
                    {
                        HttpContext.Current.Session["token"] = Service.GetToken();
                    }
                    else if (((TokenInfo)HttpContext.Current.Session["token"]).token_expires_datetime <= DateTime.Now)
                    {
                        HttpContext.Current.Session["token"] = Service.GetToken();
                    }
                    //Yoonus

                    StringBuilder stringBuilder = new StringBuilder();
					if (Service.Parameters.Count > 0)
					{
						foreach (KeyValuePair<string, string> parameter in Service.Parameters)
						{
							stringBuilder.Append(string.Concat((stringBuilder.ToString() == string.Empty ? "?" : "&"), parameter.Key, "=", parameter.Value));
						}
						methodName = string.Concat(methodName, stringBuilder.ToString());
					}
					byte[] bytes = Encoding.ASCII.GetBytes(data);
					HttpWebRequest length = (HttpWebRequest)WebRequest.Create(string.Concat(Service.url, methodName));
					length.Method = "PUT";
					length.ContentType = "application/json";

                    TokenInfo item = (TokenInfo)HttpContext.Current.Session["token"];
					length.Headers.Add("Authorization", string.Concat(item.token_type, " ", item.access_token));
					length.ContentLength = (long)((int)bytes.Length);
					using (Stream requestStream = length.GetRequestStream())
					{
						requestStream.Write(bytes, 0, (int)bytes.Length);
					}
					using (WebResponse response = length.GetResponse())
					{
						using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
						{
							postResponse = JsonConvert.DeserializeObject<PostResponse<T>>(streamReader.ReadToEnd());
						}
					}
				}
				if (postResponse.success != "1")
				{
					Alerts.ErrorMessage = postResponse.error;
					T postResponse1 = (new PostResponse<T>()).result;
					T t1 = postResponse1;
					postResponse.result = postResponse1;
					t = t1;
				}
				else
				{
					t = postResponse.result;
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return t;
		}

		public static string ToJSON()
		{
			string str;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("{");
				if (Service.Parameters.Count > 0)
				{
					foreach (KeyValuePair<string, string> parameter in Service.Parameters)
					{
						stringBuilder.Append(string.Concat(new string[] { "\"", parameter.Key, "\":\"", parameter.Value, "\"," }));
					}
				}
				stringBuilder.Append("}");
				str = stringBuilder.ToString();
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return str;
		}

		public static object ToNonAnonymousList<T>(this List<T> list, Type t)
		{
			object obj = Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[] { t }));
			MethodInfo method = obj.GetType().GetMethod("Add");
			foreach (T t1 in list)
			{
				method.Invoke(obj, new object[] { t1.ToType<Type>(t) });
			}
			return obj;
		}

		public static object ToType<T>(this object obj, T type)
		{
			object obj1 = Activator.CreateInstance(Type.GetType(string.Concat(type.ToString(), ", app.bsms")));
			foreach (KeyValuePair<string, object> keyValuePair in (Dictionary<string, object>)obj)
			{
				try
				{
					obj1.GetType().GetProperty(keyValuePair.Key).SetValue(obj1, keyValuePair.Value, null);
				}
				catch (Exception exception)
				{
					throw exception;
				}
			}
			return obj1;
		}
	}
}