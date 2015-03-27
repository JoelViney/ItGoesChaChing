using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	[DataContract]
	public class FeedbackReceivedEventType : ClientAlertEventType
	{
		/// <summary>Container for details about feedback left, including User, CommentType, and Item.</summary>
		[DataMember] public FeedbackDetailType FeedbackDetail { get; set; }
	}

}
