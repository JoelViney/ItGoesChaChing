using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ItGoesChaChing.Model
{
	public enum SchedulerSetting
	{
		Enabled,
		PopupOnly,
		Disabled,
	}

	public class ScheduleHour : ModelBase, IDirty
	{
		/// <summary>This is a circular reference to the parent Day of the Hour.</summary>
		[XmlIgnore]
		public ScheduleDay Day { get; set; }

		/// <summary>The hour represented in an integer from 0 to 23</summary>
		public int Hh { get; set; }

		private SchedulerSetting _setting;

		public bool Dirty { get; set; }


		#region Constructors...

		public ScheduleHour()
		{
			this.Setting = SchedulerSetting.Enabled;
			this.Hh = 0;
			this.Dirty = true;
		}

		#endregion

		#region Properties...

		/// <summary>The scheduler setting that applies for this Hour.</summary>
		public SchedulerSetting Setting
		{
			get { return this._setting; }
			set 
			{
				if (this._setting != value)
				{
					this._setting = value;
					NotifyOfPropertyChange(() => Setting);
					this.Dirty = true;
				}
			}
		}

		[XmlIgnore]
		public string FullName
		{
			get { return string.Format("{0}:00 - {0}:59", this.Hh); }
		}

		#endregion

	}

	public class ScheduleDay : ModelBase
	{
		public int Ddd { get; set; }

		public ObservableCollection<ScheduleHour> Hours { get; set; }

		public ScheduleDay()
		{
			this.Ddd = 0;
		}

		#region Properties...

		[XmlIgnore]
		public string Name
		{
			get
			{
				return DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames[this.Ddd];
			}
		}

		[XmlIgnore]
		public string FullName
		{
			get
			{
				return DateTimeFormatInfo.CurrentInfo.DayNames[this.Ddd];
			}
		}

		[XmlIgnore]
		public DayOfWeek DayOfWeek
		{
			get
			{
				return (DayOfWeek)this.Ddd;
			}
		}

		#endregion
	}

	public class ScheduleWeek : ModelBase
	{
		public ObservableCollection<ScheduleDay> Days { get; set; }

		public ScheduleWeek()
		{

		}
	}

	public class Schedule : ModelBase
	{
		private bool _enabled;

		public ScheduleWeek Week { get; set; }

		#region Constructors...

		public Schedule()
		{

		}

		#endregion

		#region Properties...

		/// <summary>The scheduler setting that applies for this Hour.</summary>
		public bool Enabled
		{
			get { return this._enabled; }
			set { this._enabled = value; NotifyOfPropertyChange(() => Enabled); }
		}

		#endregion

		public SchedulerSetting GetApplicableSetting(DateTime dateTime)
		{
			// Return the current scheduler's setting based on the schedule.
			ScheduleDay day = this.Week.Days.FirstOrDefault(d => d.DayOfWeek == dateTime.DayOfWeek);
			ScheduleHour hour = day.Hours.FirstOrDefault(h => h.Hh == dateTime.Hour);
			return hour.Setting;
		}
	}
}
