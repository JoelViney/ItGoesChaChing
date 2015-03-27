using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	[DataContract]
	public class ClientAlertsUserType
	{ 
		// TODO: Should be an enum?
		[DataMember] public string FeedbackRatingStar { get; set;  }
	}
	
	[DataContract]
	public class FeedbackStarChangedEventType : ClientAlertEventType
	{
		/// <summary>
		/// Visual indicator of user's feedback score. See FeedbackRatingStarCodeType in the Trading API for specific values.
		/// </summary>
		[DataMember] ClientAlertsUserType User { get; set; }
		/// <summary>
		/// Percent of total feedback that is positive. For example, if the member has 50 feedbacks, where 49 are positive and 1 is neutral or negative, the positive feedback percent could be 98.0. The value uses a max precision of 4 and a scale of 1. If the user has feedback, this value can be returned regardless of whether the member has chosen to make their feedback private. Not returned if the user has no feedback.
		/// </summary>
		[DataMember] float PositiveFeedbackPercent { get; set; }

		/// <summary>
		/// Unique eBay user ID for the user.
		/// Since a bidder's user info is anonymous, this tag will contain the real ID value only for that bidder, and the seller of an item that the user is bidding on. For all other users, the real ID value will be replaced with the anonymous value, according to these rules:
		/// 
		/// When bidding on items listed on the US site: UserID is replaced with the value "a****b" where a and b are random characters from the UserID. For example, if the UserID = IBidALot, it might be displayed as, "I****A".
		/// 
		/// Note that in this format, the anonymous bidder ID stays the same for every auction.
		/// </summary>
		[DataMember] string UserID { get; set; }
	}
}
