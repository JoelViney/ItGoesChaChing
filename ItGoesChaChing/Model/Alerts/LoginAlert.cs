using ItGoesChaChing.Ebay.ClientAlerts.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ItGoesChaChing.Helpers;

namespace ItGoesChaChing.Model.Alerts
{
	public class LoginAlert : AlertBase, INotifyPropertyChanged
	{
		private string _userId;
		private string _detail;
		private string _accountUrl;

		#region Constructors...

		public LoginAlert() : base()
		{
			// Nonthing to do here.
		}

		public LoginAlert(string userId, string detail, string accountUrl) : base()
		{
			this.UserId = userId;
			this.Detail = detail;
			this.AccountUrl = accountUrl;
		}

		#endregion

		public string AccountUrl
		{
			get { return this._accountUrl; }
			set { this._accountUrl = value; NotifyOfPropertyChange(() => AccountUrl); }
		}

		public string UserId
		{
			get { return this._userId; }
			set { this._userId = value; NotifyOfPropertyChange(() => UserId); }
		}

		public string Detail
		{
			get { return this._detail; }
			set { this._detail = value; NotifyOfPropertyChange(() => Detail); }
		}

	}
}
