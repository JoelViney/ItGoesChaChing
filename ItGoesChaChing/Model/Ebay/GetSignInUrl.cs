using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Ebay
{
	public class GetSignInUrl : EbayCommandBase
	{
		public string SessionId { get; private set; }
		public string SignInUrl { get; private set; }

		#region Constructors...

		public GetSignInUrl(EbayContext context)
			: base(context)
		{

		}

		#endregion

		// Unlike other Trading API calls, GetSessionID requires your application keys (AppID, DevID, and CERT)
		// as well as an RuName value.
		protected override void ExecuteInternal()
		{
			eBay.Service.Call.GetSessionIDCall apiCall = new eBay.Service.Call.GetSessionIDCall(this.ApiContext);

			apiCall.RuName = this.Context.RuName;
			apiCall.Execute();

			this.SessionId = apiCall.ApiResponse.SessionID;

			string url = GenerateSignInUrl(this.Context.Production, this.Context.RuName, this.SessionId);

			this.SignInUrl = url;
		}

		private static string GenerateSignInUrl(bool production, string ruName, string sessionId)
		{ 
			if (production)
				return String.Format("https://signin.ebay.com/ws/eBayISAPI.dll?SignIn&RUName={0}&SessID={1}", ruName, sessionId);
			else
				return String.Format("https://signin.sandbox.ebay.com/ws/eBayISAPI.dll?SignIn&RUName={0}&SessID={1}", ruName, sessionId);
		}
	}
}
