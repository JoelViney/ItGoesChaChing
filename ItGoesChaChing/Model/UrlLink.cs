using eBay.Service.Core.Soap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model
{
	public class UrlLink : ModelBase, INotifyPropertyChanged
	{
		private string _url;
		private URLTypeCodeType _urlType;

		#region Constructors...

		#endregion

		public string Url
		{
			get { return this._url; }
			set { this._url = value; NotifyOfPropertyChange(() => Url); }
		}

		public URLTypeCodeType UrlType
		{
			get { return this._urlType; }
			set { this._urlType = value; NotifyOfPropertyChange(() => UrlType); }
		}

	}
}
