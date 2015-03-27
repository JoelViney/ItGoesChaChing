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
	public class ExceptionAlert :  AlertBase, INotifyPropertyChanged
	{
		private Exception _ex;

		#region Constructors...

		public ExceptionAlert() : base()
		{
			// Nothing to do here.
		}

		public ExceptionAlert(Exception ex) : base()
		{
			this._ex = ex;
		}

		#endregion

		#region Properties...

		public string Message
		{
			get { return this._ex.Message; }
		}

		#endregion
	}
}
