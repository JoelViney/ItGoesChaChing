using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Ebay
{
	/// <summary>
	/// Wraps the eBay ApiContext object.
	/// </summary>
	public class EbayContext
	{
		private const string CLIENT_ALERTS_API_VERSION = "847";

		private const string API_VERSION = "897";

		private const string ACCOUNT_DEVELOPER = "e2e09ef3-e91c-45e0-a742-556a81c2428b";
		private const string ACCOUNT_APPLICATION = "ItGoesCh-d816-4ebd-bf60-758cfee0132e";
		private const string ACCOUNT_CERTIFICATE = "eae73c15-54c7-441a-984c-4a23478147c3";
		private const string RU_NAME = @"ItGoesChaChing-ItGoesCh-d816-4-phqjo";

		public string EbayToken { get; set; }
		public string SiteCode { get; set; }
		public bool Production { get; set; }
		
		ApiContext _apiContext;

		#region Constructors...

		public EbayContext(string eBayToken) : this(eBayToken, null)
		{

		}

		public EbayContext(string eBayToken, string siteCode)
		{
			this.EbayToken = eBayToken;
			this.SiteCode = siteCode;
			this.Production = true;
		}

		#endregion
		
		public static string ClientAlertsApiVersion
		{
			get { return CLIENT_ALERTS_API_VERSION; }
		}

		public string RuName
		{
			get { return RU_NAME; }
		}
		
		internal ApiContext ApiContext
		{
			get 
			{
				if (this._apiContext == null)
					GenerateApiContext();

				return this._apiContext; 
			}
		}

		private void GenerateApiContext()
		{
			this._apiContext = new ApiContext();

			this._apiContext.Version = API_VERSION;
			this._apiContext.Timeout = 6000;

			ApiAccount apiAccount = new ApiAccount();
			apiAccount.Developer = ACCOUNT_DEVELOPER;
			apiAccount.Application = ACCOUNT_APPLICATION;
			apiAccount.Certificate = ACCOUNT_CERTIFICATE;
			
			ApiCredential apiCredential = new ApiCredential();
			apiCredential.ApiAccount = apiAccount;

			apiCredential.eBayToken = this.EbayToken;

			this._apiContext.ApiCredential = apiCredential;

			this._apiContext.EnableMetrics = false;

			SiteCodeType siteCodeType;
			if (!Enum.TryParse<SiteCodeType>(this.SiteCode, out siteCodeType))
				siteCodeType = SiteCodeType.Australia;

			this._apiContext.Site = siteCodeType;

			this._apiContext.RuleName = "";
		}

	}
}
