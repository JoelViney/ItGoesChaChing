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
	public class Site : ModelBase, INotifyPropertyChanged
	{
		private string _siteCode;
		private UrlLinkList _urlLinks;

		#region Constructors...

		public Site()
		{
			this._urlLinks = new UrlLinkList();
		}

		#endregion

		#region Properties...

		public string SiteCode
		{
			get { return this._siteCode; }
			set { this._siteCode = value; NotifyOfPropertyChange(() => SiteCode); }
		}

		public UrlLinkList UrlLinks
		{
			get { return this._urlLinks; }
			set { this._urlLinks = value; NotifyOfPropertyChange(() => UrlLinks); }
		}

		#endregion
	}
}
