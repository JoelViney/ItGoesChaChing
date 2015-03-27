using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ItGoesChaChing.Model.Factories
{
	/// <summary>Loads the stored items from the local settings file.</summary>
	public class SitesFactory : FactoryBase<Site>
	{
		#region Constructors...

		public SitesFactory() : base("Sites")
		{
		}

		#endregion

		protected override void SetLocalSetting(string value)
		{
			LocalSettings settings = new LocalSettings();
			settings.Sites = value;
			settings.Save();
		}

		protected override string GetLocalSetting()
		{
			LocalSettings settings = new LocalSettings();
			return settings.Sites;
		}

	}
}
