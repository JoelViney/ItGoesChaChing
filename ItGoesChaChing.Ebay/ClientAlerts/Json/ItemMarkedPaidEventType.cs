using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	/// <summary>
	/// Alert created for a subscribing buyer (or buyer's application) when item marked as paid by the seller.
	/// </summary>
	[DataContract]
	public class ItemMarkedPaidEventType : ClientAlertEventType
	{
		/// <summary>Unique ID of the item that is the subject of the alert.</summary>
		[DataMember] public string ItemID { get; set; }
		/// <summary>Unique identifier that eBay generates for the order. This is not always populated.</summary>
		[DataMember] public string OrderID { get; set; }
		/// <summary>eBay user ID of the seller.</summary>
		[DataMember] public string SellerUserID { get; set; }
		/// <summary>The title of the listing marked as 'Paid'.  This is not always populated.</summary>
		[DataMember] public string Title { get; set; }
		/// <summary>Identifies one transaction for a listing.  This is not always populated.</summary>
		[DataMember] public string TransactionID { get; set; }
	}
}
