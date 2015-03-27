using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	// Same as BidCount without the BidCount
	[DataContract]
	public class ItemListedEventType : ClientAlertEventType
	{
		/// <summary>Price when the item was listed, if known.</summary>
		[DataMember] public AmountType CurrentPrice { get; set; }

		/// <summary>Time stamp of when the listing is scheduled to end, or time stamp of the actual end time (if the item ended).</summary>
		[DataMember] public DateTime EndTime { get; set; }
	
		/// <summary>URL for a picture used as the Gallery thumbnail, if any. The image uses one of the following graphics formats: JPEG, BMP, TIF, or GIF. Only returned if the seller chose to show a gallery image.</summary>
		[DataMember] public string GalleryURL { get; set; }
		
		/// <summary>Unique ID of the item that is the subject of the alert.</summary>
		[DataMember] public string ItemID { get; set; }
		
		/// <summary>Number of items listed.</summary>
		[DataMember] public int Quantity { get; set; }
		
		/// <summary>The eBay ID of the seller who listed the item.</summary>
		[DataMember] public string SellerUserID { get; set; }
		
		/// <summary>Name of the item as it appears in the listing or search results.</summary>
		[DataMember] public string Title { get; set; }
	}
}
