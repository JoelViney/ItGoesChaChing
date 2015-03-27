using ItGoesChaChing.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Reflection;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace ItGoesChaChing.ViewModel
{
	public class AboutViewModel : ViewModelBase
	{
		public ObservableCollection<Account> Accounts { get; set; }

		public string LogDirectory
		{
			get
			{
				string codeBase = Assembly.GetExecutingAssembly().CodeBase;
				UriBuilder uri = new UriBuilder(codeBase);
				string path = Uri.UnescapeDataString(uri.Path);
				string directory = Path.GetDirectoryName(path);
				string logDirectory = Path.Combine(directory, "logs\\");
				return logDirectory;
			}
		}

		public ICommand ViewLogFileClickedCommand
		{
			get
			{
				return new RelayCommand(
					param =>
					{
						Process.Start(this.LogDirectory);
					}
					);
			}
		}

		public string Version
		{
			get
			{
				/*
				We are using the clickonce version number, not the assembly version.
				Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
				System.Version version = assembly.GetName().Version;
				return String.Format("{0}.{1} (Build {2}.{3})", version.Major, version.Minor, version.Build, version.Revision);
				 * */

				XmlDocument xmlDoc = new XmlDocument();
				Assembly asmCurrent = System.Reflection.Assembly.GetExecutingAssembly();
				string executePath = new Uri(asmCurrent.GetName().CodeBase).LocalPath;

				xmlDoc.Load(executePath + ".manifest");
				string retval = string.Empty;
				if (xmlDoc.HasChildNodes)
				{
					retval = xmlDoc.ChildNodes[1].ChildNodes[0].Attributes.GetNamedItem("version").Value.ToString();
				}
				System.Version version = new System.Version(retval);
				return String.Format("{0}.{1} (Build {2}.{3})", version.Major, version.Minor, version.Build, version.Revision);
			}
		}
		public string Copyright
		{
			get
			{
				Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
				AssemblyCopyrightAttribute attribute = (AssemblyCopyrightAttribute)assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false).FirstOrDefault();
				return attribute.Copyright;
			}
		}
		public string Company
		{
			get
			{
				Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
				AssemblyCompanyAttribute attribute = (AssemblyCompanyAttribute)assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false).FirstOrDefault();
				return attribute.Company;
			}
		}
		
		public string Description 
		{ 
			get
			{
				Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
				AssemblyDescriptionAttribute attribute = (AssemblyDescriptionAttribute)assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false).FirstOrDefault();
				return attribute.Description;
			}
		}
	}
}
