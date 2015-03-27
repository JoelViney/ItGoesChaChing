using eBay.Service.Core.Soap;
using ItGoesChaChing.Ebay.ClientAlerts.Call;
using ItGoesChaChing.Ebay.ClientAlerts.Json;
using ItGoesChaChing.Model.Ebay;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ItGoesChaChing.Model
{
	public class Account : ModelBase, INotifyPropertyChanged
	{
		private Site _site;
		private ObservableCollection<NotificationPreference> _notificationPreferences;

		private string _userId;
		private string _siteCode;

		private string _eBayToken;
		private string _sessionID;
		private string _sessionData;

		#region Constructors...

		public Account() : base()
		{

		}

		#endregion

		[XmlIgnore]
		public Site Site
		{
			get { return this._site; }
			set { this._site = value; NotifyOfPropertyChange(() => Site); }
		}

		[XmlIgnore]
		public ObservableCollection<NotificationPreference> NotificationPreferences
		{
			get { return this._notificationPreferences; }
			set { this._notificationPreferences = value; NotifyOfPropertyChange(() => NotificationPreferences); }
		}

		public string UserId
		{
			get { return this._userId; }
			set { this._userId = value; NotifyOfPropertyChange(() => UserId); }
		}

		public string SiteCode
		{
			get { return this._siteCode; }
			set { this._siteCode = value; NotifyOfPropertyChange(() => SiteCode); }
		}

		public string EbayToken
		{
			get { return this._eBayToken; }
			set { this._eBayToken = value; NotifyOfPropertyChange(() => EbayToken); }
		}

		[XmlIgnore]
		public string SessionID
		{
			get { return this._sessionID; }
			set { this._sessionID = value; NotifyOfPropertyChange(() => SessionID); }
		}

		[XmlIgnore]
		public string SessionData
		{
			get { return this._sessionData; }
			set { this._sessionData = value; NotifyOfPropertyChange(() => SessionData); }
		}

		// TODO: SessionData gets set to null, I am guessing after 24 hours and we are disconnected.
		public bool IsConnected()
		{
			return (this.SessionData != null);
		}

		[XmlIgnore]
		public string AccountUrl
		{
			get
			{
				string url = this.Site.UrlLinks.GetUrl(URLTypeCodeType.ViewUserURL, this.UserId);

				return url;
			}
		}
	}
}
