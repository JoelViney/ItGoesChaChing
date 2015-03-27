
using ItGoesChaChing.Ebay.ClientAlerts.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Alerts
{
	public class Item : ModelBase, INotifyPropertyChanged
	{
		// Not always populated.
		public Media Media{ get; set; }

		public string ItemId { get; set; }
		public string Title { get; set; }
		// Not always populated.
		public AmountType Price { get; set; }

		public string ViewItemUrl { get; set; }

		#region Constructors...

		public Item()
			: base()
		{
			// Nothing to do here.
		}

		public Item(Site site, string itemId, string title)
			: base()
		{

			this.ItemId = itemId;
			this.Title = title;
			this.Price = null;
			this.ViewItemUrl = site.UrlLinks.GetUrl(eBay.Service.Core.Soap.URLTypeCodeType.ViewItemURL, itemId);
		}

		public Item(Site site, string itemId, string title, AmountType price)
			: base()
		{

			this.ItemId = itemId;
			this.Title = title;
			this.Price = price;
			this.ViewItemUrl = site.UrlLinks.GetUrl(eBay.Service.Core.Soap.URLTypeCodeType.ViewItemURL, itemId);
		}

		#endregion

	}
}
