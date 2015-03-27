using ItGoesChaChing.Ebay.ClientAlerts.Json;
using ItGoesChaChing.Model.Alerts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model
{
	/// <summary>
	/// The context stores all of the data used by the Engine.
	/// </summary>
	public class Context : ModelBase, INotifyPropertyChanged
	{
		private ObservableCollection<Account> _accounts;
		private ObservableCollection<Site> _sites;
		private ObservableCollection<AlertPreference> _alertPreferences;
		private Schedule _schedule;

		private ObservableCollection<AlertBase> _alerts;
		private bool _debug;

		#region Constructors...

		public Context(ObservableCollection<Account> accounts
					 , ObservableCollection<Site> sites
					 , ObservableCollection<AlertPreference> alertPreferences
					 , Schedule schedule)
		{
			this._accounts = accounts;
			this._sites = sites;
			this._alertPreferences = alertPreferences;
			this._schedule = schedule;
			this._alerts = new ObservableCollection<AlertBase>();

			this._debug = (System.Diagnostics.Debugger.IsAttached);
		}
		
		#endregion

		#region Properties...

		public ObservableCollection<Account> Accounts
		{
			get { return this._accounts; }
			set { this._accounts = value; NotifyOfPropertyChange(() => Accounts); }
		}

		public ObservableCollection<Site> Sites
		{
			get { return this._sites; }
			set { this._sites = value; NotifyOfPropertyChange(() => Sites); }
		}

		public ObservableCollection<AlertPreference> AlertPreferences
		{
			get { return this._alertPreferences; }
			set { this._alertPreferences = value; NotifyOfPropertyChange(() => AlertPreferences); }
		}

		public Schedule Schedule
		{
			get { return this._schedule; }
			set { this._schedule = value; NotifyOfPropertyChange(() => Schedule); }
		}

		public ObservableCollection<AlertBase> Alerts
		{
			get { return this._alerts; }
			set { this._alerts = value; NotifyOfPropertyChange(() => Alerts); }
		}

		public bool Debug
		{
			get { return this._debug; }
			set { this._debug = value; NotifyOfPropertyChange(() => Debug); }
		}

		#endregion

	}
}
