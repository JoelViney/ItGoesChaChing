using ItGoesChaChing.Ebay.ClientAlerts.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	public class LogoutRequestType
	{
		public string SessionData { get; set; }
		public string SessionID { get; set; }
		public string MessageID { get; set; }
	}

	[DataContract]
	public class LogoutResponseType : ResponseTypeBase
	{

	}
}

namespace ItGoesChaChing.Ebay.ClientAlerts.Call
{
	/// <summary>
	/// Logs the client application out of the client alerts server.
	/// </summary>
	/// <see cref="http://developer.ebay.com/DevZone/client-alerts/docs/CallRef/Logout.html"/>
	public class LogoutCall : ClientAlertsCallBase
	{
		public LogoutRequestType ApiRequest { get; set; }
		public LogoutResponseType ApiResponse { get; set; }

		#region Constructors...

		/// <summary>Default constructor.</summary>
		public LogoutCall(ILogger logger) : base(logger)
		{
			this.ApiRequest = new LogoutRequestType();
		}
		
		/// <summary>This constructor is used in unit tests.</summary>
		public LogoutCall(ILogger logger, IJsonWebRequest jsonWebService)
			: base(logger, jsonWebService)
		{
			this.ApiRequest = new LogoutRequestType();
		}

		#endregion

		public void Execute()
		{
			if (String.IsNullOrEmpty(this.ApiRequest.SessionID)) throw new ApplicationException("Invalid call to Logout. The SessionID is null or empty.");
			if (String.IsNullOrEmpty(this.ApiRequest.SessionData)) throw new ApplicationException("Invalid call to Logout. The SessionData is null or empty.");

			Logger.Log(LogLevel.Info, "Logging out from ClientAlerts...");

			string url = String.Format(this.UrlBase + @"?callname=Logout&SessionID={0}&SessionData={1}"
				, this.EscapeParam(this.ApiRequest.SessionID)
				, this.EscapeParam(this.ApiRequest.SessionData));

			if (this.ApiRequest.MessageID != null)
				url += String.Format(@"&MessageID={0}", this.EscapeParam(this.ApiRequest.MessageID));
			
			string responseJson = null;
			try
			{
				responseJson = this.JsonWebService.Call(url);
			}
			catch
			{
				this.Logger.Log(LogLevel.Error, "An exception occurred while calling the Login web service.");
				throw;
			}

			try
			{
				JsonDeserializer deserializer = new JsonDeserializer();
				this.ApiResponse = deserializer.DeSerialiseObject<LogoutResponseType>(responseJson);
			}
			catch
			{
				Logger.Log(LogLevel.Error, "Failed to deserialise the following: {0}", responseJson);
				throw;
			}

			Logger.Log(LogLevel.Debug, "Logout Ack {0}", this.ApiResponse.Ack);
		}
	}
}
