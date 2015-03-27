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
	/// <summary>
	/// Loads the stored items from the local settings file.
	/// This doesn't use the FactoryBase as we are only loading a single item</summary>
	public class ScheduleFactory
	{
		private ILogger Logger { get; set; }

		#region Constructors...

		public ScheduleFactory() : this(DependencyFactory.Resolve<ILogger>())
		{

		}
		public ScheduleFactory(ILogger logger)
		{
			this.Logger = logger;
		}

		#endregion

		public static Schedule CreateNew()
		{
			Schedule item = new Schedule();

			item.Week = new ScheduleWeek();

			item.Week.Days = new ObservableCollection<ScheduleDay>();
			for (int d = 0; d < 7; d++)
			{
				ScheduleDay day = new ScheduleDay() { Ddd = d };

				day.Hours = new ObservableCollection<ScheduleHour>();
				for (int h = 0; h < 24; h++)
				{
					ScheduleHour hour = new ScheduleHour() { Day = day, Hh = h };
					day.Hours.Add(hour);
				}

				item.Week.Days.Add(day);
			}

			return item;
		}
		
		public Schedule Load()
		{
			this.Logger.Log(LogLevel.Debug, "Loading the scheduler...");
			LocalSettings settings = new LocalSettings();

			string str = settings.Scheduler;
			Schedule item = null;
			if (String.IsNullOrEmpty(str))
				item = CreateNew();
			else
				item = Deserialize(str);

			// Re-setup the circular reference :P
			foreach (ScheduleDay day in item.Week.Days)
			{
				foreach (ScheduleHour hour in day.Hours)
				{
					hour.Day = day;
					hour.Dirty = false;
				}
			}

			this.Logger.Log(LogLevel.Info, "scheduler loaded");

			return item;
		}

		internal void Save(Schedule item)
		{
			this.Logger.Log(LogLevel.Debug, "Saving scheduler...");
			LocalSettings settings = new LocalSettings();

			string str = Serialize(item);
			settings.Scheduler = str;

			settings.Save();

			// Re-setup the circular reference :P
			foreach (ScheduleDay day in item.Week.Days)
			{
				foreach (ScheduleHour hour in day.Hours)
				{
					hour.Dirty = false;
				}
			}

			this.Logger.Log(LogLevel.Debug, "Scheduler saved.");
		}

		private static string Serialize(Schedule item)
		{
			if (item == null)
				return null;

			XmlSerializer serializer = new XmlSerializer(typeof(Schedule));
			StringBuilder stringBuilder = new StringBuilder();

			using (TextWriter writer = new StringWriter(stringBuilder))
			{
				serializer.Serialize(writer, item);
			}

			return stringBuilder.ToString();
		}

		private static Schedule Deserialize(string str)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Schedule));
			Schedule result;

			using (TextReader reader = new StringReader(str))
			{
				result = (Schedule)serializer.Deserialize(reader);
			}

			return result;
		}

	}
}
