using eBay.Service.Core.Soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model
{
	public class NotificationPreference : ModelBase
	{
		public bool Dirty { get; set; }

		private NotificationEventTypeCodeType _eventType { get; set; }
		private bool _enabled { get; set; }
		
		#region Constructors...

		public NotificationPreference()
			: this(NotificationEventTypeCodeType.None, false, false) 
		{

		}

		public NotificationPreference(NotificationEventTypeCodeType eventType)
			: this(eventType, false, false)
		{

		}

		public NotificationPreference(NotificationEventTypeCodeType eventType, bool enabled, bool dirty)
			: base()
		{
			this._eventType = eventType;
			this._enabled = enabled;
			this.Dirty = dirty;
		}

		#endregion

		public NotificationEventTypeCodeType EventType
		{
			get { return this._eventType; }
			set 
			{
				if (this._eventType != value)
				{ 
					this._eventType = value;
					NotifyOfPropertyChange(() => EventType);
					this.Dirty = true;
				}
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
	}
}
