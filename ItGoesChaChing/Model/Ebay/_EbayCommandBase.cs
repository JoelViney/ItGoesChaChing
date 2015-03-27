using eBay.Service.Core.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Ebay
{
	public abstract class EbayCommandBase
	{
		private EbayContext _context;

		#region Constructors...

		public EbayCommandBase(EbayContext context)
		{
			this._context = context;
		}

		#endregion

		#region Properties...

		protected ApiContext ApiContext
		{
			get { return this._context.ApiContext; }
		}

		protected EbayContext Context
		{
			get { return this._context; }
		}

		#endregion

		protected abstract void ExecuteInternal();

		public void Execute()
		{ 
			int retryAttempts = 3;
			int attempts = 0;
			while (attempts <= retryAttempts)
			{
				attempts++;
				try
				{
					ExecuteInternal();
					return;
				} 
				catch
				{	
					//  Remove the EX reference
					if (attempts == retryAttempts)
					{
						throw;
					}
				}
			}

			return;
		}
		

	}
}
