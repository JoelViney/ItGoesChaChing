using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	// Same as ListingEventTypesBase without the GalleryURL
	[DataContract]
	public class ItemEndedEventType : ClientAlertEventType
	{
		/// <summary>The total number of bids the user placed on the item.</summary>
		[DataMember]
		public string BidCount { get; set; }

		/// <summary>Price when the listing ended.</summary>
		[DataMember]
		public AmountType CurrentPrice { get; set; }

		/// <summary>Date and time when the listing ended.</summary>
		[DataMember]
		public DateTime EndTime { get; set; }

		/// <summary>Unique ID of the item that is the subject of the alert.</summary>
		[DataMember]
		public string ItemID { get; set; }

		/// <summary>Number of items in the transaction.</summary>
		[DataMember]
		public int Quantity { get; set; }

		/// <summary>The eBay ID of the seller who listed the item.</summary>
		[DataMember]
		public string SellerUserID { get; set; }

		/// <summary>Name of the item as it appears in the listing or search results.</summary>
		[DataMember]
		public string Title { get; set; }
	}
}
