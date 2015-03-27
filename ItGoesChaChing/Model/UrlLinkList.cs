using eBay.Service.Core.Soap;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model
{

	public class UrlLinkList : ObservableCollection<UrlLink>
	{
		#region Constructors...

		public UrlLinkList() : base()
		{

		}

		#endregion

		public ObservableCollection<UrlLink> UrlLinks
		{
			get { return this; }
			set
			{
				this.Items.Clear();
				foreach (UrlLink item in value)
				{
					this.Items.Add(item);
				};
			}
		}

		public string GetUrl(URLTypeCodeType urlTypeCodeType)
		{
			UrlLink link = this.FirstOrDefault(u => u.UrlType == urlTypeCodeType);

			if (link == null)
				return null;

			return link.Url;
		}

		public string GetUrl(URLTypeCodeType urlTypeCodeType, string param1)
		{
			UrlLink link = this.FirstOrDefault(u => u.UrlType == urlTypeCodeType);

			if (link == null)
				return null;

			return String.Format("{0}{1}", link.Url, param1);
		}
	}
}
