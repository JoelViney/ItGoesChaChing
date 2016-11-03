using ItGoesChaChing.Model.Ebay;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Jobs
{
	public class UpdatePreferencesJob
	{
		private ILogger Logger;

		#region Constructors...
        
		public UpdatePreferencesJob()
		{
            this.Logger = LogManager.GetLogger();
		}

		#endregion

		public void Execute(ObservableCollection<Account> accounts, ObservableCollection<AlertPreference> alertPreferences)
		{
			this.Logger.Log(LogLevel.Info, "Saving AlertPreferences for {0} accounts...", accounts.Count);

			foreach (Account account in accounts)
			{
				this.Execute(account, alertPreferences);
			}
			this.Logger.Log(LogLevel.Info, "Saving AlertPreferences Done.");
		}

		public void Execute(Account account, ObservableCollection<AlertPreference> alertPreferences)
		{
			this.Logger.Log(LogLevel.Info, "Saving Preferences for Account {0}.", account.UserId);

			EbayContext eBayContext = new EbayContext(account.EbayToken, account.SiteCode);
			SetNotificationPreferences command = new SetNotificationPreferences(eBayContext);

			// Build up the list
			List<NotificationPreference> list = new List<NotificationPreference>();
			foreach (AlertPreference alertPreference in alertPreferences)
			{
				List<NotificationPreference> newItems = alertPreference.GetNotificationPreferences();

				foreach (NotificationPreference item in newItems)
				{
					list.Add(item);
				}
			}

			command.NotificationPreferences = list.ToArray();
			command.Execute();

			this.Logger.Log(LogLevel.Info, "Saving Preferences for Account {0} Done.", account.UserId);
		}
	}
}
