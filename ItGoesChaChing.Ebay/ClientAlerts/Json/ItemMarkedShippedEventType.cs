using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	/// <summary>Contains types of item alerts.</summary>
	[DataContract]
	public class ClientAlertsShipmentType : ClientAlertEventType
	{ 
		/// <summary></summary>
		[DataMember] public string ShipmentTrackingNumber  { get; set; }
		/// <summary>Not used by any call.</summary>
		[DataMember] public DateTime ShippedTime { get; set; }
		/// <summary>The shipping carrier used.</summary>
		[DataMember] public string ShippingCarrierUsed { get; set; }
	}
	
	[DataContract]
	public class ItemMarkedShippedEventType : ClientAlertEventType 
	{
		/// <summary>Unique ID of the item that is the subject of the alert.</summary>
		[DataMember] public string ItemID { get; set; }
		/// <summary>Unique identifier that eBay generates for the order.</summary>
		[DataMember] public string OrderID { get; set; }
		/// <summary>eBay user ID of the seller.</summary>
		[DataMember] public string SellerUserID { get; set; }
		/// <summary>Details about the shipment. Setting the tracking number and carrier automatically marks the item as shipped (sets Shipped to true).</summary>
		[DataMember] public ClientAlertsShipmentType Shipment { get; set; }
		/// <summary>The title of the listing marked as 'Shipped'.</summary>
		[DataMember] public string Title { get; set; }
		/// <summary>Identifies one transaction for a listing.</summary>
		[DataMember] public string TransactionID { get; set; }
	}
}
