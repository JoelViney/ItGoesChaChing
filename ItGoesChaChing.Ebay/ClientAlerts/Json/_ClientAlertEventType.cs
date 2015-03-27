using System;
using System.Runtime.Serialization;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	[DataContract]
	public abstract class ClientAlertEventType
	{
		[DataMember(Name = "EventType")]
		private string EventTypeString
		{
			set { this.EventType = (ClientAlertsEventTypeCodeType)Enum.Parse(typeof(ClientAlertsEventTypeCodeType), value); }
			get { throw new NotImplementedException(); }
		}

		/// <summary>Identifies the action that is the subject of the alert.</summary>
		public ClientAlertsEventTypeCodeType EventType { get; set; }

		/// <summary>Time of most recent retrieval of data.</summary>
		[DataMember] public DateTime Timestamp { get; set; }

		// TODO Temp
		public string DebugJsonData { get; set; }
	}
}
