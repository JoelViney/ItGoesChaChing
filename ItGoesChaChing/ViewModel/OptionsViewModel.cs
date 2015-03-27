using ItGoesChaChing.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ItGoesChaChing.ViewModel
{
	public class OptionsViewModel : ViewModelBase
	{
		public Context Context { get; set; }

		#region Constructors...
		
		public OptionsViewModel() : this(DependencyFactory.Resolve<Context>())
		{

		}

		public OptionsViewModel(Context context)
		{
			this.Context = context;
		}

		#endregion

		#region Properties

		public bool Debug
		{
			get { return this.Context.Debug; }
		}

		#endregion

		public ICommand CloseCommand
		{
			get { return new RelayCommand(param => this.Close()); }
		}

		public void Close()
		{ 
			// Shutdown everything and boom
		}
	}
}
