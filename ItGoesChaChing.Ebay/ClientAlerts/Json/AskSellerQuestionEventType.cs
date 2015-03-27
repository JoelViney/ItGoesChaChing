using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Runtime.Serialization;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	public enum MessageTypeCodeType
	{
		/// <summary>All message types.</summary>
		All,
		/// <summary>Member to Member message initiated by bidder/potential bidder to a seller of a particular item.</summary>
		AskSellerQuestion,
		/// <summary>Member to Member message initiated by any eBay member to another eBay member.</summary>
		ContactEbayMember,
		/// <summary>Member to Member message initiated by any eBay member to another eBay member who has posted on a community forum within the past 7 days.</summary>
		ContacteBayMemberViaCommunityLink,
		/// <summary>Member to Member message initiated by sellers to their bidders during an active listing.</summary>
		ContactMyBidder,
		/// <summary>Member message between transaction partners within 90 days after the transaction.</summary>
		ContactTransactionPartner,
		/// <summary>Reserved for future or internal use.</summary>
		CustomCode,
		/// <summary>Member to Member message initiated as a response to an Ask A Question message.</summary>
		ResponseToASQQuestion,
		/// <summary>Member to Member message initiated as a response to a Contact eBay Member message.</summary>
		ResponseToContacteBayMember,
	
	}

	/// <summary>Sent to a seller when a question is posted about one of the seller's active listings.</summary>
	[DataContract]
	public class AskSellerQuestionEventType : ClientAlertEventType
	{
		/// <summary>Item about which seller is being asked questions.</summary>
		[DataMember] public string ItemID { get; set; }

		/// <summary>Unique ID assigned to the AskSellerQuestion message.</summary>
		[DataMember] public string MessageID { get; set; }
		
		[DataMember(Name = "MessageType")]
		private string MessageTypeString
		{
			set { this.MessageType = (MessageTypeCodeType)Enum.Parse(typeof(MessageTypeCodeType), value); }
			get { throw new NotImplementedException(); }
		}
		/// <summary>Specifies the type of message. For example, AskSellerQuestion.</summary>
		public MessageTypeCodeType MessageType { get; set; }

		/// <summary>The title of the active listing on which the seller has received a question about from a potential bidder/buyer.</summary>
		[DataMember] public string Title { get; set; }
	}
}
