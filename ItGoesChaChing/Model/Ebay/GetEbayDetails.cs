using eBay.Service.Core.Soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Ebay
{
	/// <summary>
	/// This is used to retrieve the use URLDetails for the site.
	/// </summary>
	public class GetEbayDetails : EbayCommandBase
	{
		public string SiteCode { private get; set; }

		public URLDetailsTypeCollection UrlDetails { get; private set; }

		#region Constructors...

		public GetEbayDetails(EbayContext context) : base(context)
		{

		}

		#endregion

		protected override void ExecuteInternal()
		{
			eBay.Service.Core.Sdk.ApiContext apiContext = this.ApiContext;
			eBay.Service.Call.GeteBayDetailsCall apiCall = new eBay.Service.Call.GeteBayDetailsCall(apiContext);

			SiteCodeType siteCodeType = (SiteCodeType)Enum.Parse(typeof(SiteCodeType), this.SiteCode);
			apiCall.Site = siteCodeType;

			apiCall.DetailLevelList.Add(DetailLevelCodeType.ReturnAll);

			apiCall.DetailNameList = new DetailNameCodeTypeCollection();
			apiCall.DetailNameList.Add(DetailNameCodeType.URLDetails);

			apiCall.Execute();

			this.UrlDetails = apiCall.URLDetailList;
		}

	}
}
