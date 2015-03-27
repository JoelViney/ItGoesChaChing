using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	public enum CommentTypeCodeType
	{
		/// <summary>Reserved for internal or future use.</summary>
		CustomCode,
		/// <summary>Applies to the eBay Motors site only. Feedback is withdrawn based on the decision of a third party.</summary>
		IndependentlyWithdrawn,
		/// <summary>Negative feedback. Decreases total feedback score.</summary>
		Negative,
		/// <summary>Neutral feedback. No effect on total feedback score.</summary>
		Neutral,
		/// <summary>Positive feedback. Increases total feedback score.</summary>
		Positive,
		/// <summary>Withdrawn feedback. Removes the effect of the original feedback on total feedback score. Comments from withdrawn feedback are still visible.</summary>
		Withdrawn,
	}

	public enum TradingRoleCodeType
	{
		/// <summary>Acting as buyer.</summary>
		Buyer,
		/// <summary>Reserved for future use</summary>
		CustomCode,
		/// <summary>Acting as seller.</summary>
		Seller,
	}
	
	[DataContract]
	public class FeedbackLeftEventType : ClientAlertEventType
	{
		/// <summary>Container for details about feedback left, including User, CommentType, and Item.</summary>
		[DataMember]
		public FeedbackDetailType FeedbackDetail { get; set; }
	}

	[DataContract]
	public class FeedbackDetailType
	{
		/// <summary>eBay user ID for the user who left the feedback.</summary>
		[DataMember]
		public string CommentingUser { get; set; }

		/// <summary>Text message left by the user in CommentingUser. Used to provide a more in-depth description of the user's opinion of the transaction. Returned as text in the language that the comment was originally left in.</summary>
		[DataMember]
		public string CommentText { get; set; }

		[DataMember(Name = "CommentType")]
		private string CommentTypeString
		{
			set { this.CommentType = (CommentTypeCodeType)Enum.Parse(typeof(CommentTypeCodeType), value); }
			get { throw new NotImplementedException(); }
		}
		/// <summary>Type of feedback. Can be Positive, Neutral, Negative, or Withdrawn (see the CommentTypeCodeType code list). Positive feedbacks add to the user's total feedback score, negative feedbacks lower the score, and neutral feedbacks do not affect the score (but do affect the overall picture of the user's online reputation).</summary>
		public CommentTypeCodeType CommentType { get; set; }

		/// <summary>Unique identifier for the feedback entry.</summary>
		[DataMember]
		public string FeedbackID { get; set; }

		/// <summary>Indicates the total feedback score for the user who made this feedback entry.</summary>
		[DataMember]
		public int FeedbackScore { get; set; }

		/// <summary>The ID that uniquely identifies the item listing.</summary>
		[DataMember]
		public string ItemID { get; set; }

		/// <summary>The final price for the item, associated with the currency identified by the currencyId attribute of the AmountType.</summary>
		[DataMember]
		public AmountType ItemPrice { get; set; }

		/// <summary>Name of the listing for which feedback was provided. Returned as CDATA.</summary>
		[DataMember]
		public string ItemTitle { get; set; }

		[DataMember(Name = "Role")]
		private string RoleString
		{
			set { this.Role = (TradingRoleCodeType)Enum.Parse(typeof(TradingRoleCodeType), value); }
			get { throw new NotImplementedException(); }
		}

		/// <summary>Indicates whether the user who was the feedback recipient was a Buyer or the Seller for that transaction.</summary>
		public TradingRoleCodeType Role { get; set; }

		/// <summary>Unique identifier for the transaction about which this feedback entry was left.</summary>
		[DataMember]
		public string TransactionID { get; set; }
	}
}
