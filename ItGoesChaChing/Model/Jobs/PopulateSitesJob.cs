using eBay.Service.Core.Soap;
using ItGoesChaChing.Model.Ebay;
using ItGoesChaChing.Model.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Jobs
{
	public class PopulateSitesJob
	{
		private ILogger Logger;

		#region Constructors...

		public PopulateSitesJob()
		{
			this.Logger = LogManager.GetLogger();
		}
		
		#endregion

		public Site PopulateSite(ObservableCollection<Site> sites, Account account)
		{
			Site site = sites.FirstOrDefault(s => s.SiteCode == account.SiteCode);

			if (site == null)
			{
				site = new Site() { SiteCode = account.SiteCode };
				sites.Add(site);
			}

			EbayContext context = new EbayContext(account.EbayToken);
			GetEbayDetails command = new GetEbayDetails(context);

			command.SiteCode = account.SiteCode;
			command.Execute();

			foreach (URLDetailsType urlDetails in command.UrlDetails)
			{
				UrlLink link = site.UrlLinks.FirstOrDefault(u => u.UrlType == urlDetails.URLType);
				if (link == null)
				{
					link = new UrlLink() { UrlType = urlDetails.URLType };
					site.UrlLinks.Add(link);
				}

				link.Url = urlDetails.URL;
			}

			SitesFactory factory = new SitesFactory();
			factory.Save(sites);

			return site;
		}

	}
}
