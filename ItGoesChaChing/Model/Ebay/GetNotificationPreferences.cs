using eBay.Service.Core.Soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ItGoesChaChing.Model.Ebay
{
	public class GetNotificationPreferences : EbayCommandBase
	{
		public NotificationRoleCodeType NotificationRoleCode { get; set; }
		public List<NotificationPreference> NotificationPreferences { get; set; }

		#region Constructors...

		public GetNotificationPreferences(EbayContext context)
			: base(context)
		{
			this.NotificationRoleCode = NotificationRoleCodeType.Application;
		}

		#endregion

		protected override void ExecuteInternal()
		{
			eBay.Service.Core.Sdk.ApiContext apiContext = this.ApiContext;
			eBay.Service.Call.GetNotificationPreferencesCall apiCall = new eBay.Service.Call.GetNotificationPreferencesCall(apiContext);

			apiCall.GetNotificationPreferences(this.NotificationRoleCode);

			apiCall.PreferenceLevel = NotificationRoleCodeType.User;

			apiCall.Execute();

			if (apiCall.ApiResponse.ApplicationDeliveryPreferences != null)
			{
				Console.WriteLine(apiCall.ApplicationDeliveryPreferences.ApplicationEnable.ToString());
				Console.WriteLine(apiCall.ApplicationDeliveryPreferences.ApplicationURL);
			}

			this.NotificationPreferences = new List<NotificationPreference>();

			for (int i = 0; i <= 96; i++)
			{
				NotificationEventTypeCodeType code = (NotificationEventTypeCodeType)i;

				if (code == NotificationEventTypeCodeType.None 
					|| code == NotificationEventTypeCodeType.CustomCode)
					continue; 
				if (code == NotificationEventTypeCodeType.MyMessagesAlert || 
					code == NotificationEventTypeCodeType.MyMessagesAlertHeader)
					continue; // Depreciated
				
				// FeedbackForSeller == When we recieve feedback.
				// 
				NotificationPreference item = new NotificationPreference(code);
				this.NotificationPreferences.Add(item);
			}

			this.NotificationPreferences.Sort
				(
					delegate(NotificationPreference x1, NotificationPreference x2) 
					{ 
						return x1.EventType.ToString().CompareTo(x2.EventType.ToString()); 
					}
				);

			foreach (NotificationEnableType item in apiCall.ApiResponse.UserDeliveryPreferenceArray)
			{
				var preference = this.NotificationPreferences.Single(x => x.EventType == item.EventType);

				preference.Enabled = (item.EventEnable == EnableCodeType.Enable);
				preference.Dirty = false;
			}
		}
	}
}
