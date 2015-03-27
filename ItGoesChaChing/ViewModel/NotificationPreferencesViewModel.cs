using ItGoesChaChing.Model;
using ItGoesChaChing.Model.Ebay;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ItGoesChaChing.ViewModel
{
	public class NotificationPreferencesViewModel : ViewModelBase
	{
		public Context Context { get; set; }
		public ObservableCollection<Account> Accounts { get { return this.Context.Accounts; } }
		public ObservableCollection<NotificationPreference> NotificationPreferences { get; set; }

		private Account _selectedAccount;

		public Account SelectedAccount
		{
			get { return this._selectedAccount;  }
			set { this._selectedAccount = value; this.NotifyOfPropertyChange(() => SelectedAccount); }
		}

		#region Constructors...

		public NotificationPreferencesViewModel() : this(DependencyFactory.Resolve<Context>())
		{

		}

		public NotificationPreferencesViewModel(Context context)
		{
			this.Context = context;
			this.NotificationPreferences = new ObservableCollection<NotificationPreference>();
			if (this.Context.Accounts.Count > 0)
				this.SelectedAccount = this.Context.Accounts[0];
		}

		#endregion

		public ICommand LoadCommand
		{
			get
			{
				return new RelayCommand(
					param =>
					{
						this.NotificationPreferences.Clear();

						// Use the first account to setup the environment.
						Account account = this.SelectedAccount;

						if (account == null)
							return;

						EbayContext context = new EbayContext(account.EbayToken, account.SiteCode);
						GetNotificationPreferences command = new GetNotificationPreferences(context);

						command.Execute();

						foreach (NotificationPreference item in command.NotificationPreferences)
						{
							this.NotificationPreferences.Add(item);
						}
					}
					);
			}
		}


		public ICommand SaveCommand
		{
			get
			{
				return new RelayCommand(
					param =>
					{
						Account account = this.SelectedAccount;

						if (account == null)
							return;

						EbayContext context = new EbayContext(account.EbayToken, account.SiteCode);

						SetNotificationPreferences command = new SetNotificationPreferences(context);

						command.NotificationPreferences = this.NotificationPreferences.ToArray();
						command.Execute();
					}
					);
			}
		}
	}
}
