using eBay.Service.Core.Soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBay.Service.Core.Sdk;
using eBay.Service.Call;

namespace ItGoesChaChing.Model.Ebay
{
	/*
	Feedback Left


	SELLER
	======
	Messages > AskSellerQuestion

	Best Offer > BestOffer
	Bids > BidReceived

	Feedback > FeedbackReceived, FeedbackLeft, FeedbackStarChanged

	EndOfAuction

	Sales > ItemSold, FixedPriceTransaction, FixedPriceEndOfTransaction

	Listing Ends: ItemEnded, ItemSold

	ItemListed

	PriceChange

	SecondChanceOffer

	BUYER
	=====
	Best Offer > BestOfferPlaced, BestOfferDeclined, CounterOfferReceived
	Bids > BidPlaced, OutBid

	ItemWon

	PaymentReminder 

	EndOfAuction


	Feedback > FeedbackReceived, FeedbackLeft, FeedbackStarChanged

	ItemAddedToWatchList, ItemRemovedFromWatchList

	ItemLost

	ItemMarkedPaid
	ItemMarkedShipped

	PriceChange

	WatchedItemEndingSoon
	*/
	public class SetNotificationPreferences : EbayCommandBase
	{
		public NotificationPreference[] NotificationPreferences { get; set; }

		#region Constructors...

		public SetNotificationPreferences(EbayContext context)
			: base(context)
		{

		}

		#endregion
		
		/*
		// Messages...
		list.Add(});
		list.Add(new NotificationEnableType { EventType = NotificationEventTypeCodeType.MyMessageseBayMessage, EventEnable = EnableCodeType.Enable });
		list.Add(new NotificationEnableType { EventType = NotificationEventTypeCodeType.MyMessagesM2MMessage, EventEnable = EnableCodeType.Enable });
				
		// Feedback
		list.Add(new NotificationEnableType { EventType = NotificationEventTypeCodeType.FeedbackReceived, EventEnable = EnableCodeType.Enable });
		// FeedbackStarChanged

		// Sales

		list.Add(new NotificationEnableType { EventType = NotificationEventTypeCodeType.AuctionCheckoutComplete, EventEnable = EnableCodeType.Enable });
		list.Add(new NotificationEnableType { EventType = NotificationEventTypeCodeType.BestOffer, EventEnable = EnableCodeType.Enable });
		list.Add(new NotificationEnableType { EventType = NotificationEventTypeCodeType.BidReceived, EventEnable = EnableCodeType.Enable });
		list.Add(new NotificationEnableType { EventType = NotificationEventTypeCodeType.FixedPriceTransaction, EventEnable = EnableCodeType.Enable });
		// ItemSold == Only shown when a purchase happens and it ends the listing. The FixedPriceTransaction also occurs.
		list.Add(new NotificationEnableType { EventType = NotificationEventTypeCodeType.ItemMarkedPaid, EventEnable = EnableCodeType.Enable });
				
				
		// Listings Ended
		list.Add(new NotificationEnableType { EventType = NotificationEventTypeCodeType.ItemSuspended, EventEnable = EnableCodeType.Enable });
		list.Add(new NotificationEnableType { EventType = NotificationEventTypeCodeType.ItemClosed, EventEnable = EnableCodeType.Enable });
		list.Add(new NotificationEnableType { EventType = NotificationEventTypeCodeType.ItemUnsold, EventEnable = EnableCodeType.Enable });
				

		// Request Totals
		list.Add(new NotificationEnableType { EventType = NotificationEventTypeCodeType.CheckoutBuyerRequestsTotal, EventEnable = EnableCodeType.Enable });

		// Disputes
		list.Add(new NotificationEnableType { EventType = NotificationEventTypeCodeType.BuyerResponseDispute, EventEnable = EnableCodeType.Enable });
		// INRBuyerClosedDispute
		// INRBuyerOpenedDispute
		// INRBuyerRespondedToDispute
		// 

		// WTF
		list.Add(new NotificationEnableType { EventType = NotificationEventTypeCodeType.TokenRevocation, EventEnable = EnableCodeType.Enable });
		list.Add(new NotificationEnableType { EventType = NotificationEventTypeCodeType.UserIDChanged, EventEnable = EnableCodeType.Enable });
		*/

		protected override void ExecuteInternal()
		{
			ApiContext apiContext = this.ApiContext;
			SetNotificationPreferencesCall apiCall = new SetNotificationPreferencesCall(apiContext);

			// Application Delivery Preferences
			{
				ApplicationDeliveryPreferencesType applicationDeliveryPreferences = new ApplicationDeliveryPreferencesType();
				applicationDeliveryPreferences.ApplicationEnable = EnableCodeType.Enable;

				applicationDeliveryPreferences.DeviceType = DeviceTypeCodeType.ClientAlerts;

				applicationDeliveryPreferences.AlertEnable = EnableCodeType.Disable;

				apiCall.ApplicationDeliveryPreferences = applicationDeliveryPreferences;
			}

			// UserDeliveryPreferenceList
			{
				apiCall.UserDeliveryPreferenceList = new NotificationEnableTypeCollection();

				foreach (NotificationPreference item in this.NotificationPreferences)
				{
					if (item.Dirty)
					{ 
						NotificationEnableType enableType = new NotificationEnableType()
						{
							EventType = item.EventType,
							EventEnable = item.Enabled ? EnableCodeType.Enable : EnableCodeType.Disable
						};

						apiCall.UserDeliveryPreferenceList.Add(enableType);
					}
				}
			}

			apiCall.Execute();
			
			foreach (NotificationPreference item in this.NotificationPreferences)
			{
				item.Dirty = false;
			}
		}

	}
}
