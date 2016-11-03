using eBay.Service.Core.Soap;
using ItGoesChaChing.Ebay.ClientAlerts.Call;
using ItGoesChaChing.Ebay.ClientAlerts.Json;
using ItGoesChaChing.Model.Alerts;
using ItGoesChaChing.Model.Ebay;
using ItGoesChaChing.Model.Factories;
using ItGoesChaChing.Model.Jobs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ItGoesChaChing.Model
{
	#region Events...
	
	public class AlertEventArgs : EventArgs
	{
		public AlertBase Alert { get; private set; }
		public bool DisplayPopup { get; private set; }
		public bool PlaySound { get; private set; }

		public AlertEventArgs(AlertBase alert, bool displayPopup, bool playSound)
		{
			this.Alert = alert;
			this.DisplayPopup = displayPopup;
			this.PlaySound = playSound;
		}
	}

	#endregion

	/// <summary>
	/// This is the heart of the application. 
	/// It handles all events that occur and raises them to the user interface. 
	/// </summary>
	public partial class Engine : ModelBase, INotifyPropertyChanged
	{
		/// <summary>The maximum number of notifications to store in the history.</summary>
		private const int MaxStoredNotifications = 30;
		/// <summary>How long before the application checks to see if there are any new Notifications.</summary>
		private const int NotificationTickSeconds = 60;

		public event EventHandler<AlertEventArgs> AlertRaised;

		private ILogger Logger { get; set; }
		private Context Context { get; set; }
		private DispatcherTimer PollTimer { get; set; }

		#region Constructors...

		public Engine(Context context)
			: base()
		{
			this.Context = context;
			this.Logger = LogManager.GetLogger();
			this.AlertQueue = new Queue<AlertBase>();
		}

		#endregion

		public void DebugAlerts()
		{
			if (this.Context.Accounts.Count == 0) return;

			Account account = this.Context.Accounts[0];
			string sessionData = account.SessionData;

			IJsonWebRequest jsonService = new JsonServiceStub(
				Test.StartJson
				+ Test.FeedbackReceivedJson + ","
				+ Test.AskSellerQuestion + ","
				+ Test.AskSellerQuestion2 + ","
				+ Test.FixedPriceTransactionJson
				+ Test.EndJson);
			ItGoesChaChing.Ebay.ILogger logger = this.Logger as ItGoesChaChing.Ebay.ILogger;

			GetUserAlertsCall alertsCall = new GetUserAlertsCall(logger, jsonService);

			GetUserAlertsJob job = new GetUserAlertsJob(this.Logger, alertsCall);

			List<AlertBase> alerts = job.GetUserAlerts(account);

			alerts[0].Timestamp = System.DateTime.Now.AddSeconds(1);
			alerts[1].Timestamp = System.DateTime.Now.AddSeconds(4);
			alerts[2].Timestamp = System.DateTime.Now.AddSeconds(8);
			alerts[3].Timestamp = System.DateTime.Now.AddSeconds(12);

			QueueAlerts(alerts);

			account.SessionData = sessionData;
		}
		
		#region Login...

		public async void Login()
		{
			this.Logger.Log(LogLevel.Debug, "Logging in accounts...");

			// Parallel.ForEach(this.Accounts, account => { Task.Factory.StartNew(() => LoginAccountAsync(account)); });
			var tasks = this.Context.Accounts.Select(account => LoginAccountAsync(account));
			await Task.WhenAll(tasks);

			StartPollTimer();
		}

		private Task<LoginAlert> LoginAccountAsync(Account account)
		{
			LoginAlert alert = null;
			App.Current.Dispatcher.Invoke((Action)delegate
			{
				alert = new LoginAlert(account.UserId, "", account.AccountUrl);
				OnAlertRaised(alert);
			});
			return Task<LoginAlert>.Factory.StartNew(() => LoginAccount(account, alert));
		}

		private LoginAlert LoginAccount(Account account, LoginAlert alert)
		{
			string result = "Success";
			try
			{
				LoginJob job = new LoginJob();

				job.Login(account);
			}
			catch(Exception ex)
			{
				result = "Failure.";
				App.Current.Dispatcher.Invoke((Action)delegate
				{
					ExceptionAlert exceptionAlert = new ExceptionAlert(ex);
					OnAlertRaised(exceptionAlert);
				});
			}

			// Blocking call to the dispatcher.
			App.Current.Dispatcher.Invoke((Action)delegate
			{
				alert.Detail += result;
			});

			return alert;
		}

		#endregion

		#region PollTimer...

		private void StartPollTimer()
		{
			this.PollTimer = new DispatcherTimer();
			this.PollTimer.Tick += new EventHandler(PollTimer_Tick);
			this.PollTimer.Interval = new TimeSpan(0, 0, NotificationTickSeconds);
			this.PollTimer.Start();
		}

		private void StopPollTimer()
		{
			this.PollTimer.Stop();
		}

		private void PollTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				GetAllUserAlerts();
			}
			catch (Exception ex)
			{
				this.Logger.Log(LogLevel.Error, "", ex);
			}
		}

		#endregion

		/// <summary>This raises an alert to the PopupViewModel so it can display the Alert.</summary>
		public void OnAlertRaised(AlertBase item)
		{
			SchedulerSetting setting = this.Context.Schedule.GetApplicableSetting(DateTime.Now);

			// TODO: Threading wrap around this...
			bool displayPopup = (setting == SchedulerSetting.Enabled || setting == SchedulerSetting.PopupOnly);
			bool playSound = (setting == SchedulerSetting.Enabled);

			App.Current.Dispatcher.Invoke((Action)delegate
			{
				if (AlertRaised != null)
				{
					if (!(item is TickAlert))
					{
						this.Context.Alerts.Insert(0, item);
					}

					AlertRaised(this, new AlertEventArgs(item, displayPopup, playSound));
				}
			});
		}

		private void GetAllUserAlerts()
		{
			// DebugTick
			if (this.Context.Debug)
			{
				TickAlert alert = new TickAlert();
				App.Current.Dispatcher.Invoke((Action)delegate
				{
					OnAlertRaised(alert);
				});
			}

			// Parallel.ForEach(this.Context.Accounts, account => { Task.Factory.StartNew(() => GetUserAlertsAsync(account)); });
			foreach(Account account in this.Context.Accounts)
			{
				this.GetUserAlerts(account);
			}
		}
		private void GetUserAlertsAsync(Account account)
		{
			Task.Factory.StartNew(() => GetUserAlerts(account));
		}

		private void GetUserAlerts(Account account)
		{
			try
			{
				if (!account.IsConnected())
				{
					LoginAlert alert = null;
					App.Current.Dispatcher.Invoke((Action)delegate
					{
						alert = new LoginAlert(account.UserId, "", account.AccountUrl);
						OnAlertRaised(alert);
					});
					Task<LoginAlert>.Factory.StartNew(() => LoginAccount(account, alert));
					return;
				}

				// GetUserAlerts
				GetUserAlertsJob job = new GetUserAlertsJob();
				List<AlertBase> alerts = job.GetUserAlerts(account);

				QueueAlerts(alerts);
			}
			catch(Exception ex)
			{
				// Log the exception... We could optionally display the exception the the user's
				this.Logger.Log(LogLevel.Error, "Exception:", ex);
			}
		}



		#region Logout

		public void Logout()
		{
			// TODO: Thread Logout()
			foreach (Account account in this.Context.Accounts)
			{
				LogoutAccount(account);
			}
			this.StopPollTimer();
		}
		private void LogoutAccount(Account account)
		{
			LogoutJob job = new LogoutJob();

			job.Logout(account);
		}

		#endregion

	}
}
