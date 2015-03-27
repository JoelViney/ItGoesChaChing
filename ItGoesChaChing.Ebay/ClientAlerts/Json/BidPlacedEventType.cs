using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	[DataContract]
	public class BidPlacedEventType : ListingEventTypesBase
	{
		[DataMember] public bool BuyItNowAvailable { get; set;}
		[DataMember] string HighBidderUserID { get; set; }
		[DataMember] bool ReserveMet { get; set; }
	}
}
