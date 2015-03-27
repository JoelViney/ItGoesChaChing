using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Alerts
{
	public class MessageAlert : AlertBase, INotifyPropertyChanged
	{
		public User Sender { get; set; }
		public Item Item { get; set; }
		
		public string Subject { get; set; }
		public string Body { get; set; }

		public IList<Media> MediaList { get; set; }

		#region Constructors...

		public MessageAlert() : base()
		{
			// Nonthing to do here.
			this.MediaList = new List<Media>();
		}

		#endregion

	}
}
