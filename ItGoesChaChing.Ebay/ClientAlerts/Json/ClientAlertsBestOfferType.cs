using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	/// <summary>The status of the best offer.</summary>
	public enum BestOfferTypeCodeType
	{
		/// <summary>The buyer has placed best offer on the item.</summary>
		BuyerBestOffer,
		/// <summary>The buyer has made a counter offer.</summary>
		BuyerCounterOffer,
		/// <summary>CustomCode	Reserved for internal or future use.</summary>
		CustomCode,
		/// <summary>The seller has made a counter offer.</summary>
		SellerCounterOffer,
	}

	/// <summary>The status of the best offer.</summary>
	public enum BestOfferStatusCodeType
	{
		/// <summary>The best offer was accepted by the seller.</summary>
		Accepted,
		/// <summary>Retrieve active best offers only.</summary>
		Active,
		/// <summary>The best offer was ended by an administrator.</summary>
		AdminEnded,
		/// <summary>Retrieve all best offers (including declined offers, etc.).</summary>
		All,
		/// <summary>Retrieve all counter best offers.</summary>
		Countered,
		/// <summary>Reserved for internal or future use.</summary>
		CustomCode,
		/// <summary>The best offer was rejected by the seller.</summary>
		Declined,
		/// <summary>The best offer expired after 48 hours due to no action by the seller.</summary>
		Expired,
		/// <summary>The best offer is awaiting seller response or will naturally expire after 48 hours.</summary>
		Pending,
		/// <summary>The best offer was retracted by the buyer.</summary>
		Retracted,
	}

	/// <summary>Identifies and describes the type of best offer event being alerted.</summary>
	[DataContract]
	public class ClientAlertsBestOfferType : ClientAlertEventType
	{
		[DataMember(Name = "BestOfferCodeType")]
		private string EventTypeString
		{
			set { this.BestOfferCodeType = (BestOfferTypeCodeType)Enum.Parse(typeof(BestOfferTypeCodeType), value); }
			get { throw new NotImplementedException(); }
		}
		/// <summary>The best offer type.</summary>
		public BestOfferTypeCodeType BestOfferCodeType { get; set; }

		/// <summary>An ID to distinguish this best offer from other best offers made on the item.</summary>
		[DataMember]
		public string BestOfferID { get; set; }

		/// <summary>Text message that was provided by a buyer when placing a best offer. Max length: 500 (in bytes).</summary>
		[DataMember]
		public string BuyerMessage { get; set; }

		/// <summary>Unique eBay user ID for the user.</summary>
		[DataMember]
		public string BuyerUserID { get; set; }

		/// <summary>Date and time (in GMT) the offer naturally expires (if the seller has not accepted or declined the offer).</summary>
		[DataMember]
		public DateTime ExpirationTime { get; set; }

		/// <summary>The amount of the best offer.</summary>
		[DataMember]
		public AmountType Price { get; set; }

		/// <summary>The number of items for which the buyer is making an offer.</summary>
		[DataMember]
		public int Quantity { get; set; }

		/// <summary>Text response to buyer from seller. Max length: 500 (in bytes).</summary>
		[DataMember]
		public string SellerMessage { get; set; }

		[DataMember(Name = "Status")]
		private string StatusString
		{
			set { this.Status = (BestOfferStatusCodeType)Enum.Parse(typeof(BestOfferStatusCodeType), value); }
			get { throw new NotImplementedException(); }
		}
		/// <summary>The state of the offer. In the case of a PlaceOffer response that specified a best offer, Status may be "Accepted" if the best offer was at or above an auto-accept price.</summary>
		public BestOfferStatusCodeType Status { get; set; }
	}
}
