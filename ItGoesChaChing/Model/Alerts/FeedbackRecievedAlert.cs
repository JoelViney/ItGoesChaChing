using ItGoesChaChing.Ebay.ClientAlerts.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Alerts
{
	public class FeedbackReceivedAlert : AlertBase, INotifyPropertyChanged
	{
		public User CommentingUser { get; set; }
		public Item Item { get; set; }

		public string CommentText { get; set; }
		public CommentTypeCodeType CommentType { get; set; }

		#region Constructors...

		public FeedbackReceivedAlert() : base()
		{
			// Nothing to do here.
		}

		#endregion
	}
}
