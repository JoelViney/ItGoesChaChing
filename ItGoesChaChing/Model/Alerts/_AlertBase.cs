using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Alerts
{
	/// <summary>
	/// Provides a base class for the Alert type.
	/// </summary>
	public abstract class AlertBase : NotifyPropertyChangedBase, INotifyPropertyChanged
	{
		public Account Account { get; set; }

		/// <summary>Time of most recent retrieval of data.</summary>
		public DateTime Timestamp { get; set; }

		/// <summary>Debug details of the Alert's JSON call.</summary>
		public string DebugJsonData { get; set; }

		public AlertBase() : base()
		{
			// Default the Timestamp for local generated events.
			this.Timestamp = DateTime.Now.ToUniversalTime();
		}
	}
}
