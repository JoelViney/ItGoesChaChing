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
using System.Windows.Shapes;

namespace ItGoesChaChing.View
{
	public partial class OptionsView : Window
	{
		public OptionsView()
		{
			InitializeComponent();
		}

		// Move the window around when the top bar is left clicked.
		private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
				this.DragMove();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			{
				ScheduleViewModel viewModel = this.ScheduleView.DataContext as ScheduleViewModel;

				if (viewModel != null)
					viewModel.Closing();
			}
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
