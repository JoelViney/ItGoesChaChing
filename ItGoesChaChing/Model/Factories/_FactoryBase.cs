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
	public abstract class FactoryBase<T>
	{
		private string SettingName { get; set; }
		private ILogger Logger { get; set; }
		
		#region Constructors...

		public FactoryBase(string settingName) : this(settingName, DependencyFactory.Resolve<ILogger>())
		{

		}
		public FactoryBase(string settingName, ILogger logger)
		{
			this.SettingName = settingName;
			this.Logger = logger;
		}

		#endregion

		protected abstract void SetLocalSetting(string value);
		protected abstract string GetLocalSetting();

		internal LocalSettings LocalSettings
		{ 
			get 
			{
				LocalSettings settings = new LocalSettings();
				return settings;
			}
		}

		public virtual ObservableCollection<T> CreateNew()
		{
			return new ObservableCollection<T>();
		}

		public ObservableCollection<T> Load()
		{
			this.Logger.Log(LogLevel.Debug, "Loading {0}...", this.SettingName);
			
			string str = this.GetLocalSetting();

			if (String.IsNullOrEmpty(str))
				return this.CreateNew();

			List<T> list = Deserialize(str);

			ObservableCollection<T> result = new ObservableCollection<T>(list);

			// Clear the dirty flag.
			if (list.Count > 0 && list[0] is IDirty)
			{
				foreach (T item in list)
				{
					IDirty dirtyObject = (IDirty)item;
					dirtyObject.Dirty = false;
				}
			}

			this.Logger.Log(LogLevel.Info, "{0} {1} loaded", list.Count, this.SettingName);

			return result;
		}

		public void Save(ObservableCollection<T> list)
		{
			this.Logger.Log(LogLevel.Debug, "Saving {0}...", this.SettingName);
			
			List<T> temp = list.ToList();
			string str = Serialize(temp);

			this.SetLocalSetting(str);
			
			// Clear the dirty flag.
			if (list.Count > 0 && list[0] is IDirty)
			{
				foreach (T item in list)
				{
					IDirty dirtyObject = (IDirty)item;
					dirtyObject.Dirty = false;
				}
			}

			this.Logger.Log(LogLevel.Debug, "{0} {1} saved.", list.Count, this.SettingName);
		}

		private static string Serialize(List<T> list)
		{
			if (list == null || list.Count == 0)
				return null;

			XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
			StringBuilder stringBuilder = new StringBuilder();

			using (TextWriter writer = new StringWriter(stringBuilder))
			{
				serializer.Serialize(writer, list);
			}

			return stringBuilder.ToString();
		}

		private static List<T> Deserialize(string str)
		{
			if (String.IsNullOrEmpty(str))
				return new List<T>();

			XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
			List<T> result;

			using (TextReader reader = new StringReader(str))
			{
				result = (List<T>)serializer.Deserialize(reader);
			}

			return result;
		}
	}
}
