using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	[DataContract]
	public abstract class ListingEventTypesBase : ClientAlertEventType
	{
		/// <summary>The total number of bids the user placed on the item.</summary>
		[DataMember] public int BidCount { get; set; }
		/// <summary>Price when the listing ended.</summary>
		[DataMember] public AmountType CurrentPrice { get; set; }
		/// <summary>Date and time when the listing ended.</summary>
		[DataMember] public DateTime EndTime { get; set; }
		/// <summary>URL for a picture to be used as the Gallery thumbnail. Ignored if GalleryType is None or unspecified.</summary>
		[DataMember] public string GalleryURL { get; set; }
		/// <summary>Unique ID of the item that is the subject of the alert.</summary>
		[DataMember] public string ItemID { get; set; }
		/// <summary>Not used by any call.</summary>
		[DataMember] public int Quantity { get; set; }
		/// <summary>The eBay ID of the seller who listed the item.</summary>
		[DataMember] public string SellerUserID { get; set; }
		/// <summary>Name of the item as it appears in the listing or search results.</summary>
		[DataMember] public string Title { get; set; }
	}
}
