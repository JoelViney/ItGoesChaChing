using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	public enum ClientAlertsEventTypeCodeType
	{
		/// <summary>Sent to a seller when a question is posted about one of the seller's active listings.</summary>
		AskSellerQuestion,
		/// <summary>Sent to a seller when a bidder makes a best offer on an item opted into the Best Offer feature by the seller.</summary>
		BestOffer,
		/// <summary>Sent to a buyer when a seller rejects the buyer's best offer on an item opted into the Best Offer feature by the seller.</summary>
		BestOfferDeclined,
		/// <summary>Sent to a buyer who makes a best offer on an item opted into the Best Offer feature by a seller.</summary>
		BestOfferPlaced,
		/// <summary>An informational alert for the buyer when a user places a bid for an item.</summary>
		BidPlaced,
		/// <summary>An informational alert for the seller when a user places a bid for an item.</summary>
		BidReceived,
		/// <summary>Sent to a buyer when a sellerer makes a counter offer to the buyer's best offer on an item opted into the Best Offer feature by the seller.</summary>
		CounterOfferReceived,
		/// <summary>Reserved for future use.</summary>
		CustomCode,
		/// <summary>Sent when an auction ends. An auction ends either when its duration expires, the buyer purchases an item with Buy It Now, or the auction is canceled. Applies to both Chinese and Dutch auctions.</summary>
		EndOfAuction,
		/// <summary>Sent to who left the feedback when a buyer leaves feedback for the seller or the seller leaves feedback for a buyer.</summary>
		FeedbackLeft,
		/// <summary>Sent to a partner when a buyer leaves feedback for the seller or the seller leaves feedback for a buyer.</summary>
		FeedbackReceived,
		/// <summary>Sent to whose feedback star level was changed.</summary>
		FeedbackStarChanged,
		/// <summary>Sent to a seller when a fixed-price item is sold and the buyer completes the checkout process. Not sent when a fixed-price item's duration expires without purchase.</summary>
		FixedPriceEndOfTransaction,
		/// <summary>Sent to a user when a listing for a fixed price item ends. A fixed price listing ends when a buyer starts to move through checkout or purchases the item.</summary>
		FixedPriceTransaction,
		/// <summary>An informational alert when a user adds an item to her/his watch list.	</summary>
		ItemAddedToWatchList,
		/// <summary>Indicates the end of item listing.	</summary>
		ItemEnded,
		/// <summary>Alert created or an eBay partner on behalf of a seller when a seller has listed an item. Sent for each item the seller lists.</summary>
		ItemListed,
		/// <summary>An informational alert for the buyer when a user doesn't win an item.</summary>
		ItemLost,
		/// <summary>Alert created for a subscribing buyer (or buyer's application) when item marked as paid by the seller.</summary>
		ItemMarkedPaid,
		/// <summary>Alert created for a subscribing buyer (or buyer's application) when item marked as shipped by the seller.</summary>
		ItemMarkedShipped,
		/// <summary>An informational alert when a user removes an item from her/his watch list.</summary>
		ItemRemovedFromWatchList,
		/// <summary>An informational alert for the seller when a user sold an item.</summary>
		ItemSold,
		/// <summary>An informational alert for the seller when an item exipired without sold.</summary>
		ItemUnsold,
		/// <summary>An informational alert for the buyer when a user bought an item.</summary>
		ItemWon,
		/// <summary>Sent to a user when another buyer has placed a higher maximum bid and the user is no longer the current high bidder.</summary>
		OutBid,
		/// <summary>Indicates that the price of an item has changed.</summary>
		PriceChange,
		/// <summary>Alert sent when a seller has used the Trading API AddSecondChanceItem to extend extend to a non-winning bidder a chance to purchase an unsold item or a simliar item.</summary>
		SecondChanceOffer,
		/// <summary>A notification type where the listing of the watched item is about to end. This event has a property with which caller can specify the TimeLeft before the listing ends.</summary>
		WatchedItemEndingSoon,
	}
	
	/// <summary>
	/// This class is structured how ClientAlerts are returned from eBay. We convert this into an array of 
	/// ClientAlerts in the ClientAlertTypes class
	/// </summary>
	[DataContract]
	internal class OriginalClientAlertEventType
	{
		[DataMember(Name = "EventType")]
		private string EventTypeString
		{
			set { this.EventType = (ClientAlertsEventTypeCodeType)Enum.Parse(typeof(ClientAlertsEventTypeCodeType), value); }
			get { throw new NotImplementedException(); }
		}
		public ClientAlertsEventTypeCodeType EventType { get; set; }

		public static ClientAlertEventType ResolveClientAlertEventType(OriginalClientAlertEventType item)
		{
			switch (item.EventType)
			{
				case ClientAlertsEventTypeCodeType.AskSellerQuestion: return item.AskSellerQuestion;
				case ClientAlertsEventTypeCodeType.BestOffer: return item.BestOffer;
				case ClientAlertsEventTypeCodeType.BestOfferDeclined: return item.BestOfferDeclined;
				case ClientAlertsEventTypeCodeType.BestOfferPlaced: return item.BestOfferPlaced;
				case ClientAlertsEventTypeCodeType.BidPlaced: return item.BidPlaced;
				case ClientAlertsEventTypeCodeType.BidReceived: return item.BidReceived;
				case ClientAlertsEventTypeCodeType.CounterOfferReceived: return item.CounterOfferReceived;
				case ClientAlertsEventTypeCodeType.EndOfAuction: return item.EndOfAuction;
				case ClientAlertsEventTypeCodeType.FeedbackLeft: return item.FeedbackLeft;
				case ClientAlertsEventTypeCodeType.FeedbackReceived: return item.FeedbackReceived;
				case ClientAlertsEventTypeCodeType.FeedbackStarChanged: return item.FeedbackStarChanged;
				case ClientAlertsEventTypeCodeType.FixedPriceEndOfTransaction: return item.FixedPriceEndOfTransaction;
				case ClientAlertsEventTypeCodeType.FixedPriceTransaction: return item.FixedPriceTransaction;
				case ClientAlertsEventTypeCodeType.ItemAddedToWatchList: return item.ItemAddedToWatchList;
				case ClientAlertsEventTypeCodeType.ItemEnded: return item.ItemEnded;
				case ClientAlertsEventTypeCodeType.ItemListed: return item.ItemListed;
				case ClientAlertsEventTypeCodeType.ItemLost: return item.ItemLost;
				case ClientAlertsEventTypeCodeType.ItemMarkedPaid: return item.ItemMarkedPaid;
				case ClientAlertsEventTypeCodeType.ItemMarkedShipped: return item.ItemMarkedShipped;
				case ClientAlertsEventTypeCodeType.ItemRemovedFromWatchList: return item.ItemRemovedFromWatchList;
				case ClientAlertsEventTypeCodeType.ItemSold: return item.ItemSold;
				case ClientAlertsEventTypeCodeType.ItemUnsold: return item.ItemUnsold;
				case ClientAlertsEventTypeCodeType.ItemWon: return item.ItemWon;
				case ClientAlertsEventTypeCodeType.OutBid: return item.OutBid;
				case ClientAlertsEventTypeCodeType.PriceChange: throw new NotImplementedException("PriceChange EventType was called.");
				case ClientAlertsEventTypeCodeType.SecondChanceOffer: return item.SecondChanceOffer;
				case ClientAlertsEventTypeCodeType.WatchedItemEndingSoon: return item.WatchedItemEndingSoon;
				default: throw new ApplicationException("Default case reached in ResolveClientAlertEventType.");
			}
		}

		[DataMember] public AskSellerQuestionEventType AskSellerQuestion { get; set; }

		[DataMember] public ClientAlertsBestOfferType BestOffer { get; set; }

		[DataMember] public BestOfferDeclinedEventType BestOfferDeclined { get; set; }

		[DataMember] public BestOfferPlacedEventType BestOfferPlaced { get; set; }

		[DataMember] public BidPlacedEventType BidPlaced { get; set; }

		[DataMember] public BidReceivedEventType BidReceived { get; set; }

		[DataMember] public CounterOfferReceivedEventType CounterOfferReceived { get; set; }
		
		[DataMember] public EndOfAuctionEventType EndOfAuction { get; set; }
		
		[DataMember] public FeedbackLeftEventType FeedbackLeft { get; set; }

		[DataMember] public FeedbackReceivedEventType FeedbackReceived { get; set; }
		
		[DataMember] public FeedbackStarChangedEventType FeedbackStarChanged { get; set; }

		[DataMember] public EndOfTransactionEventType FixedPriceEndOfTransaction { get; set; }
		
		[DataMember] public EndOfTransactionEventType FixedPriceTransaction { get; set; }
	
		[DataMember] public ItemAddedToWatchListEventType ItemAddedToWatchList { get; set; }

		[DataMember] public ItemEndedEventType ItemEnded { get; set; }

		[DataMember] public ItemListedEventType ItemListed { get; set; }

		[DataMember] public ItemLostEventType ItemLost { get; set; }

		[DataMember] public ItemMarkedPaidEventType ItemMarkedPaid { get; set; }

		[DataMember] public ItemMarkedShippedEventType ItemMarkedShipped { get; set; }

		[DataMember] public ItemRemovedFromWatchListEventType ItemRemovedFromWatchList { get; set; }

		[DataMember] public ItemSoldEventType ItemSold { get; set; }

		[DataMember] public ItemUnsoldEventType ItemUnsold { get; set; }

		[DataMember] public ItemWonEventType ItemWon { get; set; }
		
		[DataMember] public OutbidEventType OutBid { get; set; }

		[DataMember] public SecondChanceOfferEventType SecondChanceOffer { get; set; }

		[DataMember] public WatchedItemEndingSoonEventType WatchedItemEndingSoon { get; set; }
	}

	[DataContract]
	public class ClientAlertsType
	{
		[DataMember(Name = "ClientAlertEvent")]
		private OriginalClientAlertEventType[] OriginalClientAlertEvent { get; set; }

		public ClientAlertEventType[] ClientAlertEvent
		{
			get
			{
				List<ClientAlertEventType> list = new List<ClientAlertEventType>();

				if (this.OriginalClientAlertEvent != null)
				{
					foreach (OriginalClientAlertEventType item in this.OriginalClientAlertEvent)
					{
						ClientAlertEventType eventType = OriginalClientAlertEventType.ResolveClientAlertEventType(item);
						list.Add(eventType);
					}
				}

				return list.ToArray();
			}
		}
	}

}
