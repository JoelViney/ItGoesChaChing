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
	public class ItemSoldAlert : AlertBase, INotifyPropertyChanged
	{
		public string TransactionId { get; set; }
		public User Buyer { get; set; }
		public Item Item { get; set; }
		public AmountType AmountPaid { get; set; }
		public int QuantitySold { get; set; }

		#region Constructors...

		public ItemSoldAlert() : base()
		{
			// Nothing to do here.
		}

		#endregion

	}
}
