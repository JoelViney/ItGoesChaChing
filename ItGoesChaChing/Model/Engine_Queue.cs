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
	public partial class Engine : ModelBase, INotifyPropertyChanged
	{
		private Queue<AlertBase> AlertQueue { get; set; }

		private DispatcherTimer AlertQueueTimer { get; set; }

		/// <summary>
		/// Adds the Alerts to the Queue so they are drip-fed to the UI.
		/// </summary>
		/// <param name="alerts"></param>
		private void QueueAlerts(List<AlertBase> alerts)
		{
			if (alerts == null || alerts.Count == 0) return;

			this.Logger.Log(LogLevel.Debug, "Adding {0} items to the Queue.", alerts.Count);
			List<AlertBase> sorted = alerts.OrderBy(a => a.Timestamp).ToList();

			lock (this.AlertQueue)
			{
				foreach (AlertBase item in sorted)
				{
					this.AlertQueue.Enqueue(item);
				}

				this.StartQueueAlertTimer();
			}
		}

		#region QueueAlertTimer...

		private void StartQueueAlertTimer()
		{
			// Make the next Tick be the next time the Alert.Timestamp.Second matches 
			// the DateTime.Now.Second. 
			// e.g. If it is 9:00:00 and first Alert occurred at XX:XX:34 the next time we fire the Timer 
			// will be at 9:00:34...
			int seconds = (this.AlertQueue.Peek().Timestamp.Second - System.DateTime.Now.Second);
			if (seconds <= 0)
			{
				seconds = 1;
			}

			this.Logger.Log(LogLevel.Debug, "Starting Queue Timer. Activation in {0} seconds", seconds);

			if (this.AlertQueueTimer == null)
			{
				this.AlertQueueTimer = new DispatcherTimer();
				this.AlertQueueTimer.Tick += new EventHandler(AlertQueueTimer_Tick);
			}
			
			this.AlertQueueTimer.Interval = new TimeSpan(0, 0, 0, seconds);
			if (!this.AlertQueueTimer.IsEnabled)
			{ 
				this.AlertQueueTimer.Start();
			}
		}

		private void StopQueueAlertTimer()
		{
			this.Logger.Log(LogLevel.Debug, "Stopping Queue Timer.");

			if (this.AlertQueueTimer == null) return;

			this.AlertQueueTimer.Stop();
		}

		private void AlertQueueTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				lock (this.AlertQueue)
				{
					this.Logger.Log(LogLevel.Debug, "Queue Timer Tick... Total Alerts {0}.", this.AlertQueue.Count);
					if (this.AlertQueue.Count > 0)
					{
						// Always de-queue only one item.
						AlertBase alert = this.AlertQueue.Dequeue();
						int second = System.DateTime.Now.Second;

						OnAlertRaised(alert);
					}

					if (this.AlertQueue.Count > 0)
						this.StartQueueAlertTimer();
					else
						this.StopQueueAlertTimer();
				}
			}
			catch (Exception ex)
			{
				this.Logger.Log(LogLevel.Error, "", ex);
			}
		}

		#endregion

	}
}
