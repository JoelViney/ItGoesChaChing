using ItGoesChaChing.Model;
using ItGoesChaChing.Model.Factories;
using ItGoesChaChing.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ItGoesChaChing.ViewModel
{
	public class AccountsViewModel : ViewModelBase
	{
		private Context Context { get; set; }
		public ObservableCollection<Account> Accounts { get { return this.Context.Accounts; } }

		#region Constructors...

		public AccountsViewModel() : this(DependencyFactory.Resolve<Context>())
		{

		}

		public AccountsViewModel(Context context)
		{
			this.Context = context;
		}

		#endregion

		public ICommand AddCommand
		{
			get
			{
				return new RelayCommand(
					param =>
					{
						StartupWizardView view = new StartupWizardView();
						view.SetAddAccoutMode();
						view.ShowDialog();
					}
					);
			}
		}
		
		public ICommand RemoveCommand
		{
			get
			{
				return new RelayCommand(
					param =>
					{
						// Remove the selected Account
						Account item = param as Account;
						if (item != null)
						{
							this.Accounts.Remove(item);

							AccountsFactory factory = new AccountsFactory();
							factory.Save(this.Accounts);
						}
					}
					);
			}
		}
	}
}
