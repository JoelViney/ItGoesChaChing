using eBay.Service.Core.Soap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Alerts
{
	public class User : ModelBase, INotifyPropertyChanged
	{
		public string UserId { get; set; }
		public int? FeedbackScore { get; set; }

		public string ViewUserUrl { get; set; }

		#region Constructors...

		public User() : base()
		{
			// Nothing to do here.
		}

		public User(Site site, string userId) : this(site, userId, null)
		{
			// Nothing to do here.
		}

		public User(Site site, string userId, int? feedbackScore)
			: base()
		{
			this.UserId = userId;
			this.FeedbackScore = feedbackScore;
			this.ViewUserUrl = site.UrlLinks.GetUrl(URLTypeCodeType.ViewUserURL, userId);
		}

		#endregion

	}
	
}
