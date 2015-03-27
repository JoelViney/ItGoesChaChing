using System;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Threading;
using Microsoft.Win32;

namespace ItGoesChaChing.Installer
{
	[Flags]
	public enum Keys
	{
		Shift = 65536,
		LButton = 1,
		Back = 8,
		Tab = Back | LButton,
		Space = 32,
		Down = Space | Back,
		MButton = 4,
		Clear = Back | MButton,
		Enter = Clear | Tab
	}

	public class ClickOnceHelper
	{
		public string PublisherName { get; private set; }
		public string ProductName { get; private set; }
		public string UninstallFile { get; private set; }
		private RegistryKey UninstallRegistryKey { get; set; }

		private const string UninstallString = "UninstallString";
		private const string DisplayNameKey = "DisplayName";
		private const string UninstallStringFile = "UninstallString.bat";
		private const string ApprefExtension = ".appref-ms";

		private static string Location
		{
			get { return Assembly.GetExecutingAssembly().Location; }
		}


		#region Constructors...

		public ClickOnceHelper(string publisherName, string productName)
		{
			this.PublisherName = publisherName;
			this.ProductName = productName;

			try
			{
				var publisherFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), PublisherName);
				if (!Directory.Exists(publisherFolder))
				{ 
					Directory.CreateDirectory(publisherFolder);
				}
				this.UninstallFile = Path.Combine(publisherFolder, UninstallStringFile);
				this.UninstallRegistryKey = GetUninstallRegistryKeyByProductName(ProductName);
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Failed to loadup the ClickOnceHelper.", ex);
			}
		}

		#endregion


		#region Update Registry

		public void UpdateUninstallParameters()
		{
			if (this.UninstallRegistryKey == null)
				return;

			UpdateUninstallString();
			UpdateDisplayIcon();
			SetNoModify();
			SetNoRepair();
			SetHelpLink();
			SetURLInfoAbout();
		}

		private void UpdateUninstallString()
		{
			var uninstallString = (string)UninstallRegistryKey.GetValue(UninstallString);
			if (!String.IsNullOrEmpty(UninstallFile) && uninstallString.StartsWith("rundll32.exe"))
				File.WriteAllText(UninstallFile, uninstallString);
			var str = String.Format("\"{0}\" uninstall", Path.Combine(Path.GetDirectoryName(Location), "uninstall.exe"));
			this.UninstallRegistryKey.SetValue(UninstallString, str);
		}

		private void UpdateDisplayIcon()
		{
			var str = String.Format("{0},0", Path.Combine(Path.GetDirectoryName(Location), "uninstall.exe"));
			this.UninstallRegistryKey.SetValue("DisplayIcon", str);
		}





		private RegistryKey GetUninstallRegistryKeyByProductName(string productName)
		{
			var subKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall");
			if (subKey == null)
				return null;
			foreach (var name in subKey.GetSubKeyNames())
			{
				var application = subKey.OpenSubKey(name, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.QueryValues | RegistryRights.ReadKey | RegistryRights.SetValue);
				if (application == null)
					continue;
				foreach (var appKey in application.GetValueNames().Where(appKey => appKey.Equals(DisplayNameKey)))
				{
					if (application.GetValue(appKey).Equals(productName))
						return application;
					break;
				}
			}
			return null;
		}



		private void SetNoModify()
		{
			this.UninstallRegistryKey.SetValue("NoModify", 1, RegistryValueKind.DWord);
		}

		private void SetNoRepair()
		{
			this.UninstallRegistryKey.SetValue("NoRepair", 1, RegistryValueKind.DWord);
		}

		private void SetHelpLink()
		{
			this.UninstallRegistryKey.SetValue("HelpLink", Globals.HelpLink, RegistryValueKind.String);
		}

		private void SetURLInfoAbout()
		{
			this.UninstallRegistryKey.SetValue("URLInfoAbout", Globals.AboutLink, RegistryValueKind.String);
		}

		#endregion

		#region Shortcut related

		private string GetShortcutPath()
		{
			var allProgramsPath = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
			var shortcutPath = Path.Combine(allProgramsPath, this.ProductName);
			return Path.Combine(shortcutPath, ProductName) + ApprefExtension;
		}

		private string GetStartupShortcutPath()
		{
			var startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
			return Path.Combine(startupPath, ProductName) + ApprefExtension;
		}

		public void AddShortcutToStartup()
		{
			if (!ApplicationDeployment.IsNetworkDeployed)
				return;

			var startupPath = GetStartupShortcutPath();

			if (File.Exists(startupPath))
				return;

			string shortcutPath = GetShortcutPath();
			try
			{
				File.Copy(shortcutPath, startupPath);
			}
			catch(Exception ex)
			{ 
				string str = String.Format("Failed to copy a file from '{0}' to '{1}'.", "" + shortcutPath, "" + startupPath);
				throw new ApplicationException(str, ex);
			}
		}

		private void RemoveShortcutFromStartup()
		{
			var startupPath = GetStartupShortcutPath();
			if (File.Exists(startupPath))
				File.Delete(startupPath);
		}

		#endregion


		#region uninstall

		public void Uninstall()
		{
			try
			{
				//kill process
				foreach (var process in Process.GetProcessesByName(ProductName))
				{
					process.Kill();
					break;
				}

				if (!File.Exists(UninstallFile))
					return;
				RemoveShortcutFromStartup();

				var uninstallString = File.ReadAllText(UninstallFile);
				var fileName = uninstallString.Substring(0, uninstallString.IndexOf(" "));
				var args = uninstallString.Substring(uninstallString.IndexOf(" ") + 1);

				var proc = new Process
				{
					StartInfo =
					{
						Arguments = args,
						FileName = fileName,
						UseShellExecute = false
					}
				};

				proc.Start();
				RespondToClickOnceRemovalDialog();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}

		[DllImport("user32.dll")]
		private static extern bool SetForegroundWindow(IntPtr hWnd);
		[DllImport("user32.dll", SetLastError = true)]
		static extern bool PostMessage(IntPtr hWnd, [MarshalAs(UnmanagedType.U4)] uint Msg, IntPtr wParam, IntPtr lParam);
		const int WM_KEYDOWN = 0x0100;
		const int WM_KEYUP = 0x0101;

		private void RespondToClickOnceRemovalDialog()
		{
			var myWindowHandle = IntPtr.Zero;
			for (var i = 0; i < 250 && myWindowHandle == IntPtr.Zero; i++)
			{
				Thread.Sleep(150);
				foreach (var proc in Process.GetProcessesByName("dfsvc"))
					if (!String.IsNullOrEmpty(proc.MainWindowTitle) && proc.MainWindowTitle.StartsWith(ProductName))
					{
						myWindowHandle = proc.MainWindowHandle;
						break;
					}
			}
			if (myWindowHandle == IntPtr.Zero)
				return;

			SetForegroundWindow(myWindowHandle);
			Thread.Sleep(100);
			const uint wparam = 0 << 29 | 0;

			PostMessage(myWindowHandle, WM_KEYDOWN, (IntPtr)(Keys.Shift | Keys.Tab), (IntPtr)wparam);
			//PostMessage(myWindowHandle, WM_KEYUP, (IntPtr)(Keys.Shift | Keys.Tab), (IntPtr)wparam);
			PostMessage(myWindowHandle, WM_KEYDOWN, (IntPtr)(Keys.Shift | Keys.Tab), (IntPtr)wparam);
			//PostMessage(myWindowHandle, WM_KEYUP, (IntPtr)(Keys.Shift | Keys.Tab), (IntPtr)wparam);

			PostMessage(myWindowHandle, WM_KEYDOWN, (IntPtr)Keys.Down, (IntPtr)wparam);
			//PostMessage(myWindowHandle, WM_KEYUP, (IntPtr)Keys.Down, (IntPtr)wparam);

			PostMessage(myWindowHandle, WM_KEYDOWN, (IntPtr)Keys.Tab, (IntPtr)wparam);
			//PostMessage(myWindowHandle, WM_KEYUP, (IntPtr)Keys.Tab, (IntPtr)wparam);

			PostMessage(myWindowHandle, WM_KEYDOWN, (IntPtr)Keys.Enter, (IntPtr)wparam);
			//PostMessage(myWindowHandle, WM_KEYUP, (IntPtr)Keys.Enter, (IntPtr)wparam);
		}
		#endregion
	}
}
