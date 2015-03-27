using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model
{
	/// <summary>
	/// This is only used in XAML to display accounts while in the editor.
	/// </summary>
	public class AccountList : ObservableCollection<Account>
	{
		public AccountList() : base()
		{ 
			
		}

		public ObservableCollection<Account> Accounts
		{
			get { return this; }
			set 
			{
				this.Items.Clear();
				foreach(Account item in value)
				{
					this.Items.Add(item);
				};
			}
		}

	}
}
