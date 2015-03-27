using ItGoesChaChing.Model;
using ItGoesChaChing.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ItGoesChaChing.View
{
	public partial class ScheduleView : UserControl
	{
		public ScheduleView()
		{
			InitializeComponent();
		}

		// Displays a fixed tool tip for the Scheduler.
		// Breaking MVVM but it done for simplicity.
		private void TimeSegment_MouseEnter(object sender, MouseEventArgs e)
		{
			Rectangle rect = sender as Rectangle;
			if (rect == null) return;
			ScheduleHour hour = rect.DataContext as ScheduleHour;
			if (hour == null) return;

			this.dayTimeHoverText.Text = String.Format("{0}, {1}", hour.Day.FullName, hour.FullName);

			ScheduleViewModel viewModel = this.DataContext as ScheduleViewModel;

			if (viewModel != null)
			{
				viewModel.TimeSegment_MouseEnter(hour, e);
			}
		}

		private void TimeSegment_MouseLeave(object sender, MouseEventArgs e)
		{
			this.dayTimeHoverText.Text = "";
		}

		private void TimeSegment_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Rectangle rect = sender as Rectangle;
			if (rect == null) return;
			ScheduleHour hour = rect.DataContext as ScheduleHour;
			if (hour == null) return;

			ScheduleViewModel viewModel = this.DataContext as ScheduleViewModel;

			if (viewModel != null)
			{
				viewModel.TimeSegment_MouseDown(hour, e);
			}
		}
	}
}
