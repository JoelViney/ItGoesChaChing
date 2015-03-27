using eBay.Service.Call;
using eBay.Service.Core.Soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Ebay
{
	public class GetMemberMessages : EbayCommandBase
	{
		public string MessageId { get; set; }

		public MemberMessageExchangeType Message { get; set; }

		#region Constructors...

		public GetMemberMessages(EbayContext context)
			: base(context)
		{

		}

		#endregion

		protected override void ExecuteInternal()
		{
			this.Message = null;

			GetMemberMessagesCall apiCall = new GetMemberMessagesCall(this.ApiContext);

			apiCall.ApiRequest.MemberMessageID = this.MessageId;
			apiCall.ApiRequest.DetailLevel = new DetailLevelCodeTypeCollection();
			apiCall.ApiRequest.DetailLevel.Add(DetailLevelCodeType.ReturnHeaders);
			apiCall.ApiRequest.MailMessageType = MessageTypeCodeType.AskSellerQuestion;

			apiCall.Execute();


			if (apiCall.HasWarning)
			{
				string message = apiCall.ApiException.Message;

				// TODO;
			}
			if (apiCall.HasError)
			{
				string message = apiCall.ApiException.Message;
				// TODO:
			}

			if (apiCall.ApiResponse != null && apiCall.ApiResponse.MemberMessage != null && apiCall.ApiResponse.MemberMessage.Count > 0)
			{
				this.Message = apiCall.ApiResponse.MemberMessage[0];
			}
		}
	}
}
