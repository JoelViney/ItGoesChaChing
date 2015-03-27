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

namespace ItGoesChaChing.ViewModel
{
	public class HistoryViewModel : ViewModelBase, INotifyPropertyChanged
	{
		private Engine Engine { get; set; }
		private Context Context { get; set; }
		public ObservableCollection<AlertBase> Alerts { get { return this.Context.Alerts; } }

		#region Constructors...

		public HistoryViewModel()
			: this(DependencyFactory.Resolve<Engine>()
			, DependencyFactory.Resolve<Context>())
		{

		}

		public HistoryViewModel(Engine engine, Context context) : base()
		{
			this.Engine = engine;
			this.Context = context;
		}

		#endregion

		#region Properties...

		public bool PlaySound
		{
			get { return false; }
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
