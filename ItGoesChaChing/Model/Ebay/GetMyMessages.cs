using eBay.Service.Call;
using eBay.Service.Core.Soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Ebay
{
	public class GetMyMessages : EbayCommandBase
	{
		public string MessageId { get; set; }

		public MyMessagesMessageType Message { get; set; }

		#region Constructors...

		public GetMyMessages(EbayContext context)
			: base(context)
		{

		}

		#endregion

		protected override void ExecuteInternal()
		{
			this.Message = null;

			GetMyMessagesCall apiCall = new GetMyMessagesCall(this.ApiContext);

			apiCall.ApiRequest.DetailLevel = new DetailLevelCodeTypeCollection();
			apiCall.ApiRequest.DetailLevel.Add(DetailLevelCodeType.ReturnMessages);
			apiCall.ApiRequest.ExternalMessageIDs = new StringCollection();
			apiCall.ApiRequest.ExternalMessageIDs.Add(this.MessageId);

			apiCall.Execute();


			if (apiCall.HasWarning)
			{
				string message = apiCall.ApiException.Message;
			}
			if (apiCall.HasError)
			{
				string message = apiCall.ApiException.Message;
			}

			if (apiCall.ApiResponse != null
					&& apiCall.ApiResponse.Messages != null
					&& apiCall.ApiResponse.Messages.Count > 0)
			{
				this.Message = apiCall.ApiResponse.Messages[0];
			}
		}

	}
}
