using System;
using System.Runtime.Serialization;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	public enum ErrorClassificationCodeType
	{
		/// <summary>Reserved for internal or future use.</summary>
		CustomCode,
		/// <summary>An error has occurred either as a result of a problem in the sending application or because the application's end-user has attempted to submit invalid data (or missing data). In these cases, do not retry the request. The problem must be corrected before the request can be made again. If the problem is due to something in the application (such as a missing required field), the application must be changed. If the problem is a result of end-user data, the application must alert the end-user to the problem and provide the means for the end-user to correct the data. Once the problem in the application or data is resolved, resend the request to eBay with the corrected data.</summary>
		RequestError,
		/// <summary>Indicates that an error has occurred on the eBay system side, such as a database or server down. An application can retry the request as-is a reasonable number of times (eBay recommends twice). If the error persists, contact Developer Technical Support. Once the problem has been resolved, the request may be resent in its original form.</summary>
		SystemError,
	}

	public enum ErrorClassification
	{
		/// <summary>Reserved for internal or future use</summary>
		CustomCode,
		/// <summary>The request that triggered the error was not processed successfully. When a serious application-level error occurs, the error is returned instead of the business data.</summary>
		Error,
		/// <summary>The request was processed successfully, but something occurred that may affect your application or the user. For example, eBay may have changed a value the user sent in. In this case, eBay returns a normal, successful response and also returns the warning.</summary>
		Warning,
	}
	
	[DataContract]
	public class ErrorType
	{
		[DataMember] public string ShortMessage { get; set; }
		[DataMember] public string LongMessage { get; set; }
		[DataMember] public string ErrorCode { get; set; }
		[DataMember] public string SeverityCode { get; set; }
		[DataMember] public ErrorParameterType[] ErrorParameters { get; set; }
		[DataMember(Name = "ErrorClassification")]
		private string ErrorClassificationString
		{
			set { this.ErrorClassification = (ErrorClassificationCodeType)Enum.Parse(typeof(ErrorClassificationCodeType), value); }
			get { throw new NotImplementedException(); }
		}
		public ErrorClassificationCodeType ErrorClassification { get; set; }		
	}

	[DataContract]
	public class ErrorParameterType
	{
		[DataMember] public string ParamID { get; set; }
	}
}
