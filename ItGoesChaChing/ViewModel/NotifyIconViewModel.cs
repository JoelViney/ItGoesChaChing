using System;
using System.Windows;
using System.Windows.Input;
using Hardcodet.Wpf.TaskbarNotification;
using ItGoesChaChing.View;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using ItGoesChaChing.Model;
using System.Linq;
using ItGoesChaChing.Helpers;

namespace ItGoesChaChing.ViewModel
{
	/// <summary>
	/// Provides bindable properties and commands for the NotifyIcon. In this sample, the
	/// view model is assigned to the NotifyIcon in XAML. Alternatively, the startup routing
	/// in App.xaml.cs could have created this view model, and assigned it to the NotifyIcon.
	/// </summary>
	public class NotifyIconViewModel : ViewModelBase, INotifyPropertyChanged
	{
		public Context Context { get; set; }
		public Engine Engine { get; set; }
		public ObservableCollection<Account> Accounts { get { return this.Context.Accounts; } }

		#region Constructors...

		public NotifyIconViewModel()
			: this(DependencyFactory.Resolve<Context>(), DependencyFactory.Resolve<Engine>())
		{

		}

		public NotifyIconViewModel(Context context, Engine engine)
		{
			this.Context = context;
			this.Engine = engine;
		}

		#endregion

		#region Properties

		public bool Debug
		{
			get 
			{ 
				return this.Context.Debug; 
			}
		}

		#endregion

		public ICommand DoubleClickCommand
		{ 
			get 
			{
				return new RelayCommand(
					param =>
					{
						// Toggle the History window loaded/unloaded state
						HistoryView view = WindowHelper.GetWindow<HistoryView>();

						if (view != null)
						{
							view.Activate();
						}
						else
						{
							Application.Current.MainWindow = new HistoryView();
							Application.Current.MainWindow.Show();
							Application.Current.MainWindow.Activate();
						}
					});
			}
		}
		public ICommand LeftClickCommand
		{
			get
			{
				return new RelayCommand(
					param =>
					{
						// Toggle the History window loaded/unloaded state
						HistoryView view = WindowHelper.GetWindow<HistoryView>();

						if (view != null)
						{
							view.Close();
						}
						else
						{
							Application.Current.MainWindow = new HistoryView();
							Application.Current.MainWindow.Show();
							Application.Current.MainWindow.Activate();
						}
					});
			}
		}

		public ICommand ShowHistoryCommand
		{
			get
			{
				return new RelayCommand(
					param =>
					{
						HistoryView view = WindowHelper.GetWindow<HistoryView>();

						if (view != null)
						{
							view.Activate();
						}
						else
						{
							Application.Current.MainWindow = new HistoryView();
							Application.Current.MainWindow.Show();
							Application.Current.MainWindow.Activate();
						}
					});
			}
		}

		/// <summary>Shows a window, if none is already open.</summary>
		public ICommand ShowOptionsCommand
		{
			get
			{
				return new RelayCommand(
					param =>
					{
						OptionsView view = WindowHelper.GetWindow<OptionsView>();

						if (view != null)
						{
							view.Activate();
						}
						else
						{
							Application.Current.MainWindow = new OptionsView();
							Application.Current.MainWindow.Show();
							Application.Current.MainWindow.Activate();
						}
					}
					);
			}
		}

		/// <summary>
		/// Hides the main window. This command is only enabled if a window is open.
		/// </summary>
		public ICommand ShowAboutCommand
		{
			get
			{
				return new RelayCommand(
					param =>
					{
						AboutView view = WindowHelper.GetWindow<AboutView>();

						if (view != null)
						{
							view.Activate();
						}
						else
						{
							Application.Current.MainWindow = new AboutView();
							Application.Current.MainWindow.Show();
							Application.Current.MainWindow.Activate();
						}
					});
			}
		}

		/// <summary>
		/// Shuts down the application.
		/// </summary>
		public ICommand ShutdownApplicationCommand
		{
			get
			{
				return new RelayCommand(param => Application.Current.Shutdown(), null);
			}
		}


		/// <summary>
		/// Hides the main window. This command is only enabled if a window is open.
		/// </summary>
		public ICommand TestCommand
		{
			get
			{
				return new RelayCommand(
					param =>
					{
						this.Engine.DebugAlerts();
					});
			}
		}

	}
}
