using ItGoesChaChing.Ebay.ClientAlerts.Json;
using ItGoesChaChing.Model;
using ItGoesChaChing.Model.Alerts;
using ItGoesChaChing.Model.Ebay;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace ItGoesChaChing.ViewModel
{
	public class PopUpViewModel : ViewModelBase, INotifyPropertyChanged
	{
		private Engine Engine{ get; set; }

		private ObservableCollection<AlertBase> _alerts;
		private bool _playSound;
		private DispatcherTimer Timer { get; set; }

		#region Constructors...

		public PopUpViewModel() : this(DependencyFactory.Resolve<Engine>())
		{

		}

		public PopUpViewModel(Engine engine) : base()
		{
			this.Engine = engine;
			this._alerts = new ObservableCollection<AlertBase>();

			this.Engine.AlertRaised += Engine_AlertRaised;
			this.Timer = new DispatcherTimer();
			this.Timer.Tick += new EventHandler(DispatcherTimer_Tick);
		}

		#endregion

		private void Engine_AlertRaised(object sender, AlertEventArgs args)
		{
			if (args.DisplayPopup)
			{
				AlertBase alert = args.Alert;
				this._playSound = args.PlaySound;
				this.Alerts.Insert(0, alert);
				this.StartTimer();
			}
		}

		#region Properties...

		public bool PlaySound
		{
			get { return _playSound; }
		}

		public ObservableCollection<AlertBase> Alerts
		{
			get { return _alerts; }
			set { this._alerts = value; NotifyOfPropertyChange(() => Alerts); }
		}

		#endregion

		#region Dispatch Timer

		// Run the timer for 10 seconds and remove the first alert, double the time if the alert is a Login.
		// If there are more, remove the next one after 5 seconds.
		public void StartTimer()
		{
			if (this.Alerts.Count == 0) return;

			if (!this.Timer.IsEnabled)
			{
				AlertBase alert = this.Alerts[0];
				if (alert is LoginAlert)
					this.Timer.Interval = new TimeSpan(0, 0, 20);
				else
					this.Timer.Interval = new TimeSpan(0, 0, 10);

				this.Timer.Start();
			}
			else
			{
				this.Timer.Interval = new TimeSpan(0, 0, 5);
			}
		}

		private void StopTimer()
		{
			if (!this.Timer.IsEnabled) return;
			
			this.Timer.Stop();
		}

		private void DispatcherTimer_Tick(object sender, EventArgs e)
		{
			int index = this.Alerts.Count - 1;

			this.Alerts.RemoveAt(index);

			if (this.Alerts.Count == 0)
				this.Timer.Stop();
			else
				this.Timer.Start(); // Re-Start it.
		}

		#endregion

		public ICommand RemoveCommand
		{
			get
			{
				return new RelayCommand(
					param =>
					{
						// Remove the selected Alert
						AlertBase item = param as AlertBase;
						if (item != null)
						{
							if (this.Alerts.Count == 1)
								this.Timer.Stop();

							this.Alerts.Remove(item);
						}
					}
					);
			}
		}

		public ICommand HyperlinkClickedCommand
		{
			get
			{
				return new RelayCommand(
					param =>
					{
						string url = param as string;

						System.Diagnostics.Process.Start(url);
					}
					);
			}
		}
	}
}
