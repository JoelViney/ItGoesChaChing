using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	[DataContract]
	public class BidReceivedEventType : ListingEventTypesBase
	{
		[DataMember] public string HighBidderUserID { get; set; }
	}
}
