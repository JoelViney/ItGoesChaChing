using Hardcodet.Wpf.TaskbarNotification;
using ItGoesChaChing.Model;
using ItGoesChaChing.View;
using ItGoesChaChing.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Linq;
using ItGoesChaChing.Model.Ebay;
using ItGoesChaChing.Model.Factories;
using ItGoesChaChing.Model.Jobs;

namespace ItGoesChaChing
{
	public partial class App : Application
	{
		/// <summary>This is the main entrypoint of the application.</summary>
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			
			// Set the Current Culture.
			FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

			// Setup the Logger
			ILogger logger = LogManager.GetLogger();
			logger.Log(LogLevel.Info, "Loading the Application.");
			
			// Load up the Context & Engine 
			Engine engine = null;
			Context context = null;
			{
				// Load the Accounts
				ObservableCollection<Account> accounts;
				try
				{ 
					AccountsFactory accountFactory = new AccountsFactory();
					accounts = accountFactory.Load();
				}
				catch (Exception ex)
				{
					logger.Log(LogLevel.Error, "Failed to load the Accounts.", ex);
					accounts = new ObservableCollection<Account>();
				}

				// Load the Sites
				ObservableCollection<Site> sites = null;
				try
				{ 
					SitesFactory siteFactory = new SitesFactory();
					sites = siteFactory.Load();
				}
				catch(Exception ex)
				{
					logger.Log(LogLevel.Error, "Failed to load the Sites.", ex);
					sites = new ObservableCollection<Site>();
				}

				// Load the AlertPreferences
				ObservableCollection<AlertPreference> alertPreferences = null;
				AlertPreferencesFactory alertPreferencesFactory = new AlertPreferencesFactory();
				try
				{
					alertPreferences = alertPreferencesFactory.Load();
				}
				catch (Exception ex)
				{
					logger.Log(LogLevel.Error, "Failed to load the AlertPreferences.", ex);
					alertPreferences = alertPreferencesFactory.CreateNew();
				}

				// Load the Scheduler
				Schedule scheduler = null;
				try
				{
					ScheduleFactory schedulerFactory = new ScheduleFactory();
					scheduler = schedulerFactory.Load();
				}
				catch (Exception ex)
				{
					logger.Log(LogLevel.Error, "Failed to load the Scheduler.", ex);
					scheduler = new Schedule();
				}

				// Check to see that we have a site for each account loaded
				foreach (Account account in accounts)
				{
					Site site = sites.FirstOrDefault(s => s.SiteCode == account.SiteCode);
					if (site == null)
					{
						PopulateSitesJob job = new PopulateSitesJob();
						site = job.PopulateSite(sites, account);
					}

					account.Site = site;
				}
				
				// Load the Context
				context = new Context(accounts, sites, alertPreferences, scheduler);

				// Load the Engine
				engine = new Engine(context);

				DependencyFactory.RegisterInstance(context);
				DependencyFactory.RegisterInstance(engine);
			}

			// Create Initial Account (if no acount exists)
			if (context.Accounts.Count == 0)
			{
				logger.Log(LogLevel.Info, "No accounts exist. Creating initial account.");
				StartupWizardView view = new StartupWizardView();
				StartupWizardViewModel viewModel = (StartupWizardViewModel)view.DataContext;

				Application.Current.MainWindow = view;
				Application.Current.MainWindow.ShowDialog();
			}

			// If the user hit cancel and no accounts are saved, end the application
			if (context.Accounts.Count == 0)
				Shutdown();

			// Load up the Alert balloon
			try
			{
				logger.Log(LogLevel.Info, "Loading up the Alert balloons...");

				PopupView view = new PopupView();
				TaskbarIcon notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");

				notifyIcon.ShowCustomBalloon(view, PopupAnimation.None, null);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Failed loading the alert balloons.", ex);
			}

			// Login.
			logger.Log(LogLevel.Info, "Performing Login...");
			engine.Login();
		}

		protected override void OnExit(ExitEventArgs e)
		{
            ILogger logger = LogManager.GetLogger();

			// Logout
			Engine engine = DependencyFactory.Resolve<Engine>();
			engine.Logout();
			
			logger.Log(LogLevel.Info, "Shutting down the application");

			base.OnExit(e);
		}
	}
}
