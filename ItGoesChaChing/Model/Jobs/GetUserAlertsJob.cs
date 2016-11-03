using eBay.Service.Core.Soap;
using ItGoesChaChing.Ebay.ClientAlerts.Call;
using ItGoesChaChing.Ebay.ClientAlerts.Json;
using ItGoesChaChing.Model.Alerts;
using ItGoesChaChing.Model.Ebay;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Jobs
{
	public class GetUserAlertsJob
	{
		private ILogger Logger;
		private GetUserAlertsCall ApiCall { get; set; }

		#region Constructors...
		
		public GetUserAlertsJob()
		{
			this.Logger = LogManager.GetLogger(); 

			ItGoesChaChing.Ebay.ILogger clientAlertsLogger = this.Logger as ItGoesChaChing.Ebay.ILogger;
			this.ApiCall = new GetUserAlertsCall(clientAlertsLogger);
		}

		public GetUserAlertsJob(ILogger logger, GetUserAlertsCall apiCall)
		{
			this.Logger = logger;
			this.ApiCall = apiCall;
		}

		#endregion

		public List<AlertBase> GetUserAlerts(Account account)
		{
			List<AlertBase> alerts = new List<AlertBase>();

			this.ApiCall.ApiRequest.SessionID = account.SessionID;
			this.ApiCall.ApiRequest.SessionData = account.SessionData;
			this.ApiCall.ApiRequest.Version = EbayContext.ClientAlertsApiVersion;

			this.ApiCall.Execute();

			account.SessionData = this.ApiCall.ApiResponse.SessionData;

			if (this.ApiCall.ApiResponse.ClientAlerts == null)
				return alerts;

			foreach (ClientAlertEventType item in this.ApiCall.ApiResponse.ClientAlerts.ClientAlertEvent)
			{
				this.Logger.Log(LogLevel.Debug, "Alert type {0}", item.GetType());

				AlertBase alert = PopulateAlert(account, item);

				if (alert != null)
					alerts.Add(alert);
				else
					this.Logger.Log(LogLevel.Info, "Unknown EventType received: {0}.", item.EventType.ToString());
			}

			return alerts;
		}

		public AlertBase PopulateAlert(Account account, ClientAlertEventType item)
		{
			if (item is FeedbackLeftEventType)
				return null; // Feedback received alerts are kind of worthless

			if (item is ItemMarkedPaidEventType && ((ItemMarkedPaidEventType)item).SellerUserID == account.UserId)
				return null; // Not interested in alerts informing us we have marked the item as paid.

			// Feedback Received
			if (item is FeedbackReceivedEventType)
			{
				FeedbackReceivedEventType eventType = (FeedbackReceivedEventType)item;
				FeedbackReceivedAlert alert = new FeedbackReceivedAlert();

				alert.Account = account;
				alert.CommentingUser = new User(account.Site, eventType.FeedbackDetail.CommentingUser, eventType.FeedbackDetail.FeedbackScore);
				alert.Item = new Item(account.Site, eventType.FeedbackDetail.ItemID, eventType.FeedbackDetail.ItemTitle, eventType.FeedbackDetail.ItemPrice);
				alert.CommentText = eventType.FeedbackDetail.CommentText;
				alert.CommentType = eventType.FeedbackDetail.CommentType;

				return alert;
			} // AskSellerQuestion
			else if (item is AskSellerQuestionEventType)
			{
				AskSellerQuestionEventType eventType = (AskSellerQuestionEventType)item;
				MessageAlert alert = new MessageAlert();

				// Extract some more details from eBay
				{
					EbayContext context = new EbayContext(account.EbayToken);
					GetMemberMessages getMessagesCall = new GetMemberMessages(context);
					getMessagesCall.MessageId = eventType.MessageID;

					getMessagesCall.Execute();
					MemberMessageExchangeType messageType = getMessagesCall.Message;

					alert.Account = account;
					alert.Sender = new User(account.Site, messageType.Question.SenderID);
					alert.Subject = messageType.Question.Subject;
					alert.Body = messageType.Question.Body.Replace("&apos;", "'");
					alert.Item = new Item(account.Site, messageType.Item.ItemID, messageType.Item.Title);

					foreach (MessageMediaType mediaType in messageType.MessageMedia)
					{
						PictureDownloader downloader = new PictureDownloader();
						Bitmap bitmap = downloader.DownloadImage(mediaType.MediaURL);
						Media media = new Media(bitmap, mediaType.MediaURL);
						alert.MediaList.Add(media);
					}
				}

				return alert;
			}
			else if (item is EndOfTransactionEventType && item.EventType == ClientAlertsEventTypeCodeType.FixedPriceTransaction)
			{
				EndOfTransactionEventType eventType = (EndOfTransactionEventType)item;
				ItemSoldAlert alert = new ItemSoldAlert();

				alert.TransactionId = eventType.Transaction.TransactionID;
				alert.Account = account;
				alert.Item = new Item(account.Site, eventType.ItemID, eventType.Title, eventType.CurrentPrice);
				alert.Buyer = new User(account.Site, eventType.Transaction.BuyerUserID);
				alert.AmountPaid = eventType.Transaction.AmountPaid;
				alert.QuantitySold = eventType.Transaction.QuantitySold;

				if (!String.IsNullOrEmpty(eventType.GalleryURL))
				{
					PictureDownloader downloader = new PictureDownloader();
					Bitmap bitmap = downloader.DownloadImage(eventType.GalleryURL);
					Media media = new Media(bitmap, eventType.GalleryURL);
					alert.Item.Media = media;
				}

				return alert;
			}

			return null;
		}

	}
}
