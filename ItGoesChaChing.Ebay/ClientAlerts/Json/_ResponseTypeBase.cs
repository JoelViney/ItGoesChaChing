using ItGoesChaChing.Ebay.ClientAlerts.Json;
using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	public enum AckCodeType
	{
		/// <summary>Reserved for internal or future use.</summary>
		CustomCode,
		/// <summary>Request processing failed</summary>
		Failure,
		/// <summary>Request processing completed with some failures. See the Errors data to determine which portions of the request failed.</summary>
		PartialFailure,
		/// <summary>Request processing succeeded</summary>
		Success,
		/// <summary>Request processing completed with warning information being included in the response message</summary>
		Warning
	}
	
	/// <summary>
	/// Provides a base class for all of the Api calls .ApiResponse
	/// </summary>
	[DataContract]
	public abstract class ResponseTypeBase
	{
		[DataMember] public DateTime Timestamp   { get; set; }

		[DataMember(Name = "Ack")]
		private string AckString
		{
			set { this.Ack = (AckCodeType)Enum.Parse(typeof(AckCodeType), value); }
			get { throw new NotImplementedException(); }
		}
		public AckCodeType Ack { get; set; }

		[DataMember] public ErrorType[] Errors { get; set; }
		[DataMember] public string Build { get; set; }
		[DataMember] public string CorrelationID { get; set; }
		[DataMember] public string Version { get; set; }
	}
}
