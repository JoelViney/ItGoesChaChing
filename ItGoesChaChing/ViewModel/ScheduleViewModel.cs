using ItGoesChaChing.Model;
using ItGoesChaChing.Model.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ItGoesChaChing.ViewModel
{
	public class ScheduleViewModel : ViewModelBase
	{
		public Schedule Scheduler { get; set; }

		#region Constructors...

		public ScheduleViewModel() : this(DependencyFactory.Resolve<Context>())
		{

		}

		public ScheduleViewModel(Context context)
		{
			if (context != null)
				this.Scheduler = context.Schedule;
			else
				this.Scheduler = ScheduleFactory.CreateNew(); // This is required when we are in design view.
		}

		#endregion

		// Track the mouse position so we can continue to make the same change while the mouse is down.
		private SchedulerSetting _settingOnMouseDown;

		public void TimeSegment_MouseDown(ScheduleHour hour, MouseButtonEventArgs e)
		{
			switch (hour.Setting)
			{
				case SchedulerSetting.Enabled: hour.Setting = SchedulerSetting.PopupOnly; break;
				case SchedulerSetting.PopupOnly: hour.Setting = SchedulerSetting.Disabled; break;
				case SchedulerSetting.Disabled: hour.Setting = SchedulerSetting.Enabled; break;
			}

			this._settingOnMouseDown = hour.Setting;
		}


		public void TimeSegment_MouseEnter(ScheduleHour hour, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				hour.Setting = this._settingOnMouseDown;
			}
		}

		public void Closing()
		{
			if (this.Scheduler.Week.Days.Any(d => d.Hours.Any(h => h.Dirty)))
			{ 
				ScheduleFactory factory = new ScheduleFactory();
				factory.Save(this.Scheduler);
			}
		}
	}
}
