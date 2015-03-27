using ItGoesChaChing.Model;
using ItGoesChaChing.Model.Ebay;
using ItGoesChaChing.Model.Factories;
using ItGoesChaChing.Model.Jobs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ItGoesChaChing.ViewModel
{
	public class AlertPreferencesViewModel : ViewModelBase
	{
		public Context Context { get; set; }
		public ObservableCollection<AlertPreference> AlertPreferences { get; set; }

		#region Constructors...

		public AlertPreferencesViewModel()
			: this(DependencyFactory.Resolve<Context>())
		{

		}

		public AlertPreferencesViewModel(Context context)
		{
			this.Context = context;
			this.AlertPreferences = context.AlertPreferences;
		}

		#endregion

		public void Closing()
		{
			if (this.AlertPreferences.Any(p => p.Dirty))
			{
				AlertPreferencesFactory factory = new AlertPreferencesFactory();
				factory.Save(this.AlertPreferences);

				// TODO: Need to try catch this.
				UpdatePreferencesJob job = new UpdatePreferencesJob();
				job.Execute(this.Context.Accounts, this.Context.AlertPreferences);
			}
		}

	}
}
