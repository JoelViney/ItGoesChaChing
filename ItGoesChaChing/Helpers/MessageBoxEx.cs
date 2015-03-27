using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ItGoesChaChing.ViewModel;
using ItGoesChaChing.View;

namespace ItGoesChaChing.Helpers
{
	/// <summary>
	/// Static access to the MessageBox so it behaves much like a standard messagebox.
	/// Implements a custom version of the standard System.Windows.MessageBox
	/// </summary>
	public class MessageBoxEx
	{
		public static MessageBoxResult Show(Window parentWindow, string messageBoxText)
		{
			
			return Show(parentWindow, messageBoxText, null, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
		}

		public static MessageBoxResult Show(Window parentWindow, string messageBoxText, string caption)
		{
			return Show(parentWindow, messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
		}

		public static MessageBoxResult Show(Window parentWindow, string messageBoxText, string caption, MessageBoxButton button)
		{
			return Show(parentWindow, messageBoxText, caption, button, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
		}

		public static MessageBoxResult Show(Window parentWindow, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
		{
			return Show(parentWindow, messageBoxText, caption, button, icon, MessageBoxResult.None, MessageBoxOptions.None);
		}

		public static MessageBoxResult Show(Window parentWindow, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
		{
			return Show(parentWindow, messageBoxText, caption, button, icon, defaultResult, MessageBoxOptions.None);
		}

		public static MessageBoxResult Show(Window parentWindow, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options)
		{
			MessageBoxViewModel viewModel = new MessageBoxViewModel(messageBoxText, caption, button, icon, defaultResult, options);
			MessageBoxView view = new MessageBoxView();

			view.DataContext = viewModel;
			view.Owner = parentWindow;
			view.ShowDialog();

			return viewModel.Result;
		}
	}
}
