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
	public class WatchedItemEndingSoonEventType : ListingEventTypesBase
	{
		[DataMember] public string HighBidderUserID { get; set; }

		[DataMember] public string MinimumToBid { get; set; }

		[DataMember] public string ViewItemURL { get; set; }
	}
}
