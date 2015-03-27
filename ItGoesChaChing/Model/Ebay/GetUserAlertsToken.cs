using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Ebay
{
	public class GetUserAlertsToken : EbayCommandBase
	{
		public string ClientAlertsAuthToken { get; private set; }

		#region Constructors...

		public GetUserAlertsToken(EbayContext context)
			: base(context)
		{ 
		
		}
		
		#endregion

		protected override void ExecuteInternal()
		{ 
			eBay.Service.Core.Sdk.ApiContext apiContext = this.ApiContext;
			eBay.Service.Call.GetClientAlertsAuthTokenCall apiCall = new eBay.Service.Call.GetClientAlertsAuthTokenCall(apiContext);

			apiCall.Execute();
			this.ClientAlertsAuthToken = apiCall.ApiResponse.ClientAlertsAuthToken;
		}
	}
}
