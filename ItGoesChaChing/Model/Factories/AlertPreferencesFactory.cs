using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Factories
{
	public class AlertPreferencesFactory : FactoryBase<AlertPreference>
	{
		#region Constructors...

		public AlertPreferencesFactory() : base("AlertSettings")
		{

		}

		#endregion


		protected override void SetLocalSetting(string value)
		{
			LocalSettings settings = new LocalSettings();
			settings.AlertPreferences = value;
			settings.Save();
		}

		protected override string GetLocalSetting()
		{
			LocalSettings settings = new LocalSettings();
			return settings.AlertPreferences;
		}

		public override ObservableCollection<AlertPreference> CreateNew()
		{
			ObservableCollection<AlertPreference> list = new ObservableCollection<AlertPreference>();

			IEnumerable<AlertType> alertTypes = Enum.GetValues(typeof(AlertType)).Cast<AlertType>();

			foreach (AlertType alertType in alertTypes)
			{
				if (alertType == AlertType.NotSpecififed)
					continue;

				AlertPreference alertPreference = new AlertPreference(alertType, true, true);
				list.Add(alertPreference);
			}

			return list;
		}
	}
}
