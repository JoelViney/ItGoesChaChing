using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	// Same as ListingEventTypesBase but missing Quantity
	[DataContract]
	public class OutbidEventType : ClientAlertEventType
	{		
		[DataMember] public int BidCount { get; set; }
		[DataMember] public AmountType CurrentPrice { get; set; }
		[DataMember] public DateTime EndTime { get; set; }
		[DataMember] public string GalleryURL { get; set; }
		
		[DataMember] public string HighBidderEIASToken { get; set; }
		[DataMember] public string HighBidderUserID { get; set; }

		[DataMember] public string ItemID { get; set; }

		/// <summary>Not used by any call.</summary>
		[DataMember] public string MinimmToBid { get; set; }

		[DataMember] public string SellerUserID { get; set; }
		[DataMember] public string Title { get; set; }
		
		[DataMember] public string ViewItemURL { get; set; }
	}
}
