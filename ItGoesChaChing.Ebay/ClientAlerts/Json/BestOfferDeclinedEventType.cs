using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	[DataContract]
	public class BestOfferDeclinedEventType : ClientAlertEventType
	{
		[DataMember] public ClientAlertsBestOfferType BestOffer { get; set; }

		[DataMember] public string ItemID { get; set; }
	}
}
