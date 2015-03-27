using ItGoesChaChing.Ebay.ClientAlerts.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	public class GetUserAlertsRequestType
	{
		/// <summary>
		/// Returned from the server in the Login and GetUserAlerts responses, and passed in the GetUserAlerts and 
		/// Logout requests. Because SessionData contains an encrypted timestamp, when the application uses the 
		/// SessionData returned from the previous GetUserAlerts call in a new GetUserAlerts call, GetUserAlerts 
		/// returns all new alerts since the previous call along with a brand new SessionData for use use next time.
		/// </summary>
		public string SessionData { get; set; }
		/// <summary>
		/// Identifier of the session, which must be passed in every time GetUserAlerts is called. SessionId does not 
		/// change over the lifetime of the session.
		/// </summary>
		public string SessionID { get; set; }
		/// <summary>
		/// Set this value to inform the API that your application is not an application predating the release of the 
		/// Client Alerts API.
		/// </summary>
		public string Version { get; set; }
	}

	/// <summary>
	/// Retrieves alerts privately subscribed to by the user.
	/// </summary>
	[DataContract]
	public class GetUserAlertsResponseType : ResponseTypeBase
	{
		[DataMember]
		public ClientAlertsType ClientAlerts { get; set; }

		[DataMember]
		public string SessionData { get; set; }
	}
}

namespace ItGoesChaChing.Ebay.ClientAlerts.Call
{
	public class GetUserAlertsCall : ClientAlertsCallBase
	{
		public GetUserAlertsRequestType ApiRequest { get; set; }
		public GetUserAlertsResponseType ApiResponse { get; set; }

		#region Constructors...

		/// <summary>Default constructor.</summary>
		public GetUserAlertsCall(ILogger logger)
			: base(logger)
		{
			this.ApiRequest = new GetUserAlertsRequestType();
		}

		/// <summary>This constructor is used in unit tests.</summary>
		public GetUserAlertsCall(ILogger logger, IJsonWebRequest jsonWebService)
			: base(logger, jsonWebService)
		{
			this.ApiRequest = new GetUserAlertsRequestType();
		}

		#endregion

		public void Execute()
		{
			if (String.IsNullOrEmpty(this.ApiRequest.SessionID)) throw new ApplicationException("Invalid call to GetUserAlerts. The SessionID is null or empty.");
			// TODO: SessionData gets set to null, I am guessing after 24 hours and we are disconnected.
			if (String.IsNullOrEmpty(this.ApiRequest.SessionData)) throw new ApplicationException("Invalid call to GetUserAlerts. The SessionData is null or empty.");

			if (this.ApiRequest.Version != null)
				Logger.Log(LogLevel.Info, "Calling GetUserAlerts... Version: {0}", this.ApiRequest.Version);
			else
				Logger.Log(LogLevel.Info, "Calling GetUserAlerts...");

			string url = String.Format(this.UrlBase + @"?callname=GetUserAlerts&SessionID={0}&SessionData={1}"
				, this.EscapeParam(this.ApiRequest.SessionID)
				, this.EscapeParam(this.ApiRequest.SessionData));

			if (this.ApiRequest.Version != null)
				url += String.Format(@"&Version={0}", this.EscapeParam(this.ApiRequest.Version));

			string responseJson = null;
			try
			{
				responseJson = this.JsonWebService.Call(url);

			}
			catch
			{
				// Log that we were calling the web service. Leave the caller to log the exception.
				this.Logger.Log(LogLevel.Error, "An exception occurred while calling the GetUserAlert's web service.");
				throw;
			}

			Logger.Log(LogLevel.Debug, "GetUserAlertsCall JSON: {0}", responseJson);

			try
			{
				JsonDeserializer deserializer = new JsonDeserializer();
				this.ApiResponse = deserializer.DeSerialiseObject<GetUserAlertsResponseType>(responseJson);
			}
			catch
			{
				Logger.Log(LogLevel.Error, "Failed to deserialise the following: {0}", responseJson);
				throw;
			}

			if (this.ApiResponse.ClientAlerts != null && this.ApiResponse.ClientAlerts.ClientAlertEvent != null)
			{
				Logger.Log(LogLevel.Debug, "GetUserAlerts Ack {0} : {1} alert(s) returned.", this.ApiResponse.Ack, this.ApiResponse.ClientAlerts.ClientAlertEvent.Length);

				foreach (ClientAlertEventType item in this.ApiResponse.ClientAlerts.ClientAlertEvent)
				{
					item.DebugJsonData = responseJson;
				}
			}
			else
			{ 
				Logger.Log(LogLevel.Debug, "GetUserAlerts Ack {0}", this.ApiResponse.Ack);
			}
		}
	}
}
