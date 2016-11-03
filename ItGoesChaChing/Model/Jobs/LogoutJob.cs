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
	public class LogoutJob
	{
		private ILogger Logger;

		#region Constructors...

		public LogoutJob()
		{
            this.Logger = LogManager.GetLogger();
		}

		#endregion
		
		public void Logout(Account account)
		{
			if (account.SessionData == null)
				return; // There is no need to logout as the Account isn't logged in.

			ItGoesChaChing.Ebay.ILogger clientAlertsLogger = this.Logger as ItGoesChaChing.Ebay.ILogger;
			LogoutCall apiCall = new LogoutCall(clientAlertsLogger);

			apiCall.ApiRequest.SessionID = account.SessionID;
			apiCall.ApiRequest.SessionData = account.SessionData;
			
			apiCall.Execute(); 	
		}

	}
}
