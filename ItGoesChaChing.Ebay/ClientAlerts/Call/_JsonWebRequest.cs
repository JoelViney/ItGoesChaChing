using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace ItGoesChaChing.Ebay.ClientAlerts.Call
{
	public interface IJsonWebRequest
	{
		string Call(string url);
		string EscapeParam(string str);
	}

	public class JsonWebRequest : IJsonWebRequest
	{
		protected readonly string UrlBase = @"http://clientalerts.ebay.com/ws/ecasvc/ClientAlerts";

		// Returns a JSON string
		// From: http://stackoverflow.com/questions/8270464/best-way-to-call-a-json-webservice-from-a-net-console
		public string Call(string url)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			try
			{
				WebResponse response = request.GetResponse();
				using (Stream responseStream = response.GetResponseStream())
				{
					StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
					return reader.ReadToEnd();
				}
			}
			catch (WebException ex)
			{
				WebResponse errorResponse = ex.Response;
				if (errorResponse != null)
				{
					using (Stream responseStream = errorResponse.GetResponseStream())
					{
						StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
						String errorText = reader.ReadToEnd();
					}
				}
				throw;
			}
		}

		public string EscapeParam(string str)
		{
			if (str == null) return null;

			return System.Uri.EscapeDataString(str);
		}
	}
}
