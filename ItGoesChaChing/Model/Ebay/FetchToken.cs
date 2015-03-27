using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Ebay
{
	/// <summary>
	/// FetchToken returns the authentication token for the specified user into the eBayAuthToken field. 
	/// It also returns the expiration date and time for the token in HardExpirationTime.
	/// </summary>
	public class FetchToken : EbayCommandBase
	{
		public string SessionId { private get; set; }

		public string EbayToken { get; private set; }
		public DateTime HardExpirationTime { get; private set; }

		#region Constructors...

		public FetchToken(EbayContext context) : 
			base(context)
		{
		
		}

		#endregion

		protected override void ExecuteInternal()
		{
			eBay.Service.Call.FetchTokenCall apiCall = new eBay.Service.Call.FetchTokenCall(this.ApiContext);

			apiCall.SessionID = this.SessionId;

			apiCall.Execute();

			this.EbayToken = apiCall.ApiResponse.eBayAuthToken;
			this.HardExpirationTime = apiCall.HardExpirationTime;
		}
	}
}
