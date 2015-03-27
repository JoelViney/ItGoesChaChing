using eBay.Service.Core.Soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Ebay
{
	public class GetUserCommand : EbayCommandBase
	{
		public string UserId { get; private set; }
		public string SiteCode { get; private set; }

		#region Constructors...

		public GetUserCommand(EbayContext context)
			: base(context)
		{

		}

		#endregion
		
		protected override void ExecuteInternal()
		{
			eBay.Service.Core.Sdk.ApiContext apiContext = this.ApiContext;
			eBay.Service.Call.GetUserCall apiCall = new eBay.Service.Call.GetUserCall(apiContext);

			apiCall.Execute();

			this.SiteCode = Enum.GetName(typeof(SiteCodeType), apiCall.Site);

			this.UserId = apiCall.ApiResponse.User.UserID;
		}

	}
}
