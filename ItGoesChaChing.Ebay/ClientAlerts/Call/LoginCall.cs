using ItGoesChaChing.Ebay.ClientAlerts.Json;
using ItGoesChaChing.Ebay.ClientAlerts.Call;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	public class LoginRequestType
	{
		/// <summary>
		/// Authenticates the client to the Client Alerts server. Obtained using the Trading API call GetUserAlertsToken. 
		/// A Client Alerts token is valid for seven days. After the Client Alerts token expires, the client will need 
		/// to use the eBay user token to fetch a new Client Alerts token.
		/// </summary>
		public string ClientAlertsAuthToken { get; set; }
	}

	[DataContract]
	public class LoginResponseType : ResponseTypeBase
	{
		[DataMember]
		public string SessionID { get; set; }

		[DataMember]
		public string SessionData { get; set; }
	}
}

namespace ItGoesChaChing.Ebay.ClientAlerts.Call
{
	/// <summary>
	/// Logs the client application in to the Client Alerts server. Required for the GetUserAlerts call, and not 
	/// required for the GetPublicAlerts call. The server returns SessionID and SessionData, which are required in 
	/// subsequent GetUserAlerts calls.
	/// </summary>
	/// <see cref="http://developer.ebay.com/DevZone/client-alerts/docs/CallRef/Login.html"/>
	public class LoginCall : ClientAlertsCallBase
	{
		public LoginRequestType ApiRequest { get; set; }
		public LoginResponseType ApiResponse { get; set; }

		#region Constructors...

		/// <summary>Default constructor.</summary>
		public LoginCall(ILogger logger)
			: base(logger)
		{
			this.ApiRequest = new LoginRequestType();
		}

		/// <summary>This constructor is used in unit tests.</summary>
		public LoginCall(ILogger logger, IJsonWebRequest jsonWebService)
			: base(logger, jsonWebService)
		{
			this.ApiRequest = new LoginRequestType();
		}

		#endregion

		public void Execute()
		{
			if (String.IsNullOrEmpty(this.ApiRequest.ClientAlertsAuthToken)) throw new ApplicationException("Invalid call to Login. The ClientAlerts authentication token is null or empty.");

			Logger.Log(LogLevel.Info, "Logging into ClientAlerts...");

			string url = String.Format(this.UrlBase + @"?callname=Login&ClientAlertsAuthToken={0}", EscapeParam(this.ApiRequest.ClientAlertsAuthToken));

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
				this.ApiResponse = deserializer.DeSerialiseObject<LoginResponseType>(responseJson);
			}
			catch
			{
				Logger.Log(LogLevel.Error, "Failed to deserialise the following: {0}", responseJson);
				throw;
			}

			Logger.Log(LogLevel.Debug, "Login Ack {0}", this.ApiResponse.Ack);
			
		}
	}
}
