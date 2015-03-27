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
	/// <summary>
	/// Interaction logic for NewAccount.xaml
	/// </summary>
	public partial class StartupWizardView : Window
	{
		public StartupWizardView()
		{
			InitializeComponent();
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		public void SetAddAccoutMode()
		{
			StartupWizardViewModel model = this.DataContext as StartupWizardViewModel;

			if (model != null)
			{
				model.SetAddAccoutMode();
			}
		}
	}
}
