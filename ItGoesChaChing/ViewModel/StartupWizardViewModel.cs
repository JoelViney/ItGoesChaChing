using ItGoesChaChing.Model;
using ItGoesChaChing.Model.Ebay;
using ItGoesChaChing.Model.Factories;
using ItGoesChaChing.Model.Jobs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ItGoesChaChing.ViewModel
{
	public class StartupWizardViewModel : ViewModelBase, INotifyPropertyChanged
	{
		private ILogger Logger { get; set; }

		private LinkEbayAccountJob Job { get; set; }

		private int _wizardIndex;
		private bool _linkToEbayCommandEnabled;
		private string _errorMessage;
		

		#region Constructors...

		public StartupWizardViewModel(): 
			this(DependencyFactory.Resolve<ILogger>())
		{
		
		}

		public StartupWizardViewModel(ILogger logger)
		{
			this.Logger = logger;

			this._wizardIndex = 0;
			this._linkToEbayCommandEnabled = true;
			this._errorMessage = "An internal exception occurred.";

			this.Job = new LinkEbayAccountJob();
		}

		public void SetAddAccoutMode()
		{
			this.WizardIndex = 1;
		}

		#endregion

		#region Properties...

		
		public int WizardIndex
		{
			get { return this._wizardIndex; }
			set { this._wizardIndex = value; NotifyOfPropertyChange(() => WizardIndex); }
		}

		public bool LinkToEbayCommandEnabled
		{
			get { return this._linkToEbayCommandEnabled; }
			set { this._linkToEbayCommandEnabled = value; NotifyOfPropertyChange(() => LinkToEbayCommandEnabled); }
		}

		public string ErrorMessage
		{
			get { return this._errorMessage; }
			set { this._errorMessage = value; NotifyOfPropertyChange(() => ErrorMessage); }
		}
		
		#endregion

		#region ICommand Implementations...

		public ICommand LinkToEbayCommand
		{
			get { return new RelayCommand(param => this.LinkToEbay()); }
		}

		public ICommand ConfirmCommand
		{
			get { return new RelayCommand(param => this.Confirm()); }
		}
		
		public ICommand TryAgainCommand
		{
			get { return new RelayCommand(param => this.TryAgain()); }
		}

		public ICommand NextCommand
		{
			get { return new RelayCommand(param => this.Next()); }
		}
		public ICommand BackCommand
		{
			get { return new RelayCommand(param => this.Back()); }
		}

		#endregion

		public void LinkToEbay()
		{
			try
			{
				this.LinkToEbayCommandEnabled = false;
				this.Job.LinkToEbay();

				this.Next();
			}
			catch (Exception ex)
			{
				Logger.Log(LogLevel.Error, "Grant Access failed.", ex);
			}
			finally
			{
				this.LinkToEbayCommandEnabled = true; 
			}
		}
		public void Confirm()
		{
			try
			{
				this.Job.Confirm();

				this.Next();
			}
			catch (Exception ex)
			{
				Logger.Log(LogLevel.Error, "eBay verification failed.", ex);
				this.ErrorMessage = ex.Message;
				this.WizardIndex = 4;
			}
		}

		public void TryAgain()
		{
			// Load up the 2nd page
			this.WizardIndex = 1;
		}

		public void Back()
		{
			this.WizardIndex--;
		}

		public void Next()
		{
			this.WizardIndex++;
		}
	}
}
