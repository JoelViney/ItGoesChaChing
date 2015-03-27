using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;


namespace ItGoesChaChing.Helpers
{
	public static class WindowHelper
	{
		/// <summary>
		/// Returns the first current application window of the specified type if it is loaded.
		/// </summary>
		public static T GetWindow<T>()
		{
			T result = default(T);

			foreach (var item in Application.Current.Windows)
			{
				if (item is T)
				{
					result = (T)item;
					break;
				}
			}

			return result;
		}


		#region HideClose

		private const int GWL_STYLE = -16;
		private const int WS_SYSMENU = 0x80000;
		[DllImport("user32.dll", SetLastError = true)]
		private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
		[DllImport("user32.dll", SetLastError = true)]
		private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		public static void HideClose(Window window)
		{
			var hwnd = new WindowInteropHelper(window).Handle;
			SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
		}

		#endregion

		#region HideIcon


		[DllImport("user32.dll")]
		static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int width, int height, uint flags);

		[DllImport("user32.dll")]
		static extern IntPtr SendMessage(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

		const int GWL_EXSTYLE = -20;
		const int WS_EX_DLGMODALFRAME = 0x0001;
		const int SWP_NOSIZE = 0x0001;
		const int SWP_NOMOVE = 0x0002;
		const int SWP_NOZORDER = 0x0004;
		const int SWP_FRAMECHANGED = 0x0020;
		const uint WM_SETICON = 0x0080;

		public static void RemoveIcon(Window window)
		{
			// Get this window's handle
			IntPtr hwnd = new WindowInteropHelper(window).Handle;

			// Change the extended window style to not show a window icon
			int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
			SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_DLGMODALFRAME);

			// Update the window's non-client area to reflect the changes
			SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, SWP_NOMOVE |
				  SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
		}

		#endregion
	}
}
