using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.Windows;

namespace ItGoesChaChing.ViewModel
{
	public abstract class ViewModelBase : NotifyPropertyChangedBase, INotifyPropertyChanged
	{
		public string ApplicationTitle
		{
			get
			{
				Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
				AssemblyTitleAttribute attribute = (AssemblyTitleAttribute)assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false).FirstOrDefault();
				return attribute.Title;
			}
		}

	}
}
