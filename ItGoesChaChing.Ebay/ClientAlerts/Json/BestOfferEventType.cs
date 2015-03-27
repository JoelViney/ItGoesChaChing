using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	public class BestOfferEventType : ClientAlertEventType
	{
		/// <summary>A container of details about a best offer.</summary>
		[DataMember] public ClientAlertsBestOfferType BestOffer { get; set; }

		/// <summary>Unique ID of the item that is the subject of the alert.</summary>
		[DataMember] public string ItemID { get; set; }
	}

}
