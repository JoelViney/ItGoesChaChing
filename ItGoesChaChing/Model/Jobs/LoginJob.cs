using ItGoesChaChing.Ebay.ClientAlerts.Call;
using ItGoesChaChing.Model.Ebay;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Jobs
{
	public class LoginJob
	{
		private ILogger Logger;

		#region Constructors...

		public LoginJob()
			: this(DependencyFactory.Resolve<ILogger>())
		{

		}

		public LoginJob(ILogger logger)
		{
			this.Logger = logger;
		}
		
		#endregion

		public void Login(Account account)
		{
			EbayContext context = new EbayContext(account.EbayToken);

			// Get the Token
			// TODO: Initial failure point is on login here....
			string userAlertsToken = null;
			{
				GetUserAlertsToken command = new GetUserAlertsToken(context);
				command.Execute();
				userAlertsToken = command.ClientAlertsAuthToken;
			}

			// Login ...
			ItGoesChaChing.Ebay.ILogger clientAlertsLogger = this.Logger as ItGoesChaChing.Ebay.ILogger;
			LoginCall apiCall = new LoginCall(clientAlertsLogger);

			apiCall.ApiRequest.ClientAlertsAuthToken = userAlertsToken;

			apiCall.Execute();

			account.SessionID = apiCall.ApiResponse.SessionID;
			account.SessionData = apiCall.ApiResponse.SessionData;
		}

	}
}
