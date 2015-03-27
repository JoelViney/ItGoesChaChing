using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ItGoesChaChing
{
	public class RelayCommand : ICommand
	{
		private readonly Action<object> _execute;
		private readonly Predicate<object> _canExecute;

		#region Constructors...

		public RelayCommand(Action<object> execute) : this(execute, null)
		{

		}

		public RelayCommand(Action<object> execute, Predicate<object> canExecute)
		{
			if (execute == null) throw new ArgumentNullException("execute");

			this._execute = execute;
			this._canExecute = canExecute;
		}

		#endregion

		public void Execute(object parameter)
		{
			this._execute(parameter);
		}

		[DebuggerStepThrough]
		public bool CanExecute(object parameter)
		{
			return this._canExecute == null ? true : this._canExecute(parameter);
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
	}

}