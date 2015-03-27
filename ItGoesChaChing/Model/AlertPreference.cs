using eBay.Service.Core.Soap;
using ItGoesChaChing.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model
{
	public enum AlertType
	{ 
		NotSpecififed,
		MessageReceived,
		FeedbackReceived,
		ItemSold,
	}

	/// <summary>
	/// Defines the alerts that users want to see.
	/// Providing a simplified view of the NotificationPreferences.
	/// </summary>
	public class AlertPreference : ModelBase, IDirty
	{
		private AlertType _alertType { get; set; }
		private bool _enabled { get; set; }

		public bool Dirty { get; set; }
		
		#region Constructors...

		public AlertPreference() : this(AlertType.NotSpecififed, true, false) 
		{

		}

		public AlertPreference(AlertType alertType, bool enabled, bool dirty)
			: base()
		{
			this._alertType = alertType;
			this._enabled = enabled;
			this.Dirty = false;
		}

		#endregion

		#region Properties...

		public AlertType AlertType
		{
			get { return this._alertType; }
			set 
			{
				if (this._alertType != value)
				{
					this._alertType = value;
					NotifyOfPropertyChange(() => AlertType);
					this.Dirty = true;
				}
			}
		}

		public string AlertTypeString
		{
			get
			{
				return StringHelper.SpaceTitleCase(this.AlertType.ToString());
			}
		}

		public bool Enabled
		{
			get { return this._enabled; }
			set 
			{
				if (this._enabled != value)
				{ 
					this._enabled = value;
					NotifyOfPropertyChange(() => Enabled);
					this.Dirty = true;
				}
			}
		}

		#endregion

		/// <summary>
		/// Translates the AlertPreference to the related NotificationPreference(s)
		/// </summary>
		/// <returns></returns>
		public List<NotificationPreference> GetNotificationPreferences()
		{ 
			switch(this.AlertType)
			{
				case AlertType.NotSpecififed: /* Lets ignore this one. */ return new List<NotificationPreference>();
				case AlertType.MessageReceived: return BuildNotifications(NotificationEventTypeCodeType.AskSellerQuestion);
				case AlertType.FeedbackReceived: return BuildNotifications(NotificationEventTypeCodeType.FeedbackReceived);
				case AlertType.ItemSold: return BuildNotifications(NotificationEventTypeCodeType.FixedPriceTransaction);
			}
			throw new ApplicationException("A NotSpecififed was found when returning the NotificationPreferences.");
		}

		private List<NotificationPreference> BuildNotifications(params NotificationEventTypeCodeType[] args)
		{
			List<NotificationPreference> list = new List<NotificationPreference>();
			foreach (NotificationEventTypeCodeType codeType in args)
			{ 
				NotificationPreference preference = new NotificationPreference(codeType, this.Enabled, true);
				list.Add(preference);
			}

			return list;
		}

	}
}
