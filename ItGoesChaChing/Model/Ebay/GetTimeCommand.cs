using eBay.Service.Core.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Ebay
{

	/// <summary>
	/// Use GeteBayOfficialTime to retrieve the official eBay system time in GMT.
	/// </summary>
	/// <see cref="http://developer.ebay.com/DevZone/XML/docs/Reference/eBay/GeteBayOfficialTime.html"/>
	public class GetTimeCommand : EbayCommandBase
	{
		public DateTime EbayDateTime { get; private set; }

		#region Constructors...

		public GetTimeCommand(EbayContext context)
			: base(context)
		{

		}

		#endregion

		protected override void ExecuteInternal()
		{
			eBay.Service.Core.Sdk.ApiContext apiContext = this.ApiContext;
			eBay.Service.Call.GeteBayOfficialTimeCall apiCall = new eBay.Service.Call.GeteBayOfficialTimeCall(apiContext);

			apiCall.Execute();

			this.EbayDateTime = apiCall.EBayTime;
		}

	}
}
