using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ItGoesChaChing.Ebay.ClientAlerts.Call;
using ItGoesChaChing.Ebay.ClientAlerts.Json;

namespace ItGoesChaChing.Ebay.Test
{
	[TestClass]
	public class GetUserAlertsTest
	{
		private ILogger Logger { get; set; }

		[TestInitialize]
		public void TestInitialise()
		{
			this.Logger = new ConsoleLogger();
		}

		[TestMethod]
		public void AskSellerQuestion()
		{
			JsonServiceStub stub = new JsonServiceStub(@"{
					""Timestamp"":""2014-08-21T04:05:18.408Z"",
					""Ack"":""Success"",
					""Build"":""E847_CORE_APINOT_16496971_R1"",
					""Version"":""847"",
					""ClientAlerts"":
					{
						""ClientAlertEvent"":[
						{
							""EventType"":""AskSellerQuestion"",
							""AskSellerQuestion"":
							{
								""EventType"":""AskSellerQuestion"",
								""Timestamp"":""2014-08-21T03:59:52.787Z"",
								""ItemID"":""121403478858"",
								""Title"":""HURLEY Mens ALL STATE Black T Shirt Top (M L XL XXL) NEW"",
								""MessageID"":""887697677018"",
								""MessageType"":""AskSellerQuestion""
							}
						}]
					},
					""SessionData"":""AQAAAUf13OQAAA1TSUQ9NDA1MTI2M3xTVD0xfDF8TEFDVFY9MTQwODU5MzkxODM5M3xFSFdNPTE4MDUwNzc4NTMzfFRJRFg9MXxMSVVQPTE0MDg1OTMyNjE5MjJ8UExIUz1bXea4wEEeBR+lvVNEOtRmM9Gnk4Ei""
					}");

			GetUserAlertsCall call = new GetUserAlertsCall(this.Logger, stub);
			call.Execute();

			Assert.IsNotNull(call.ApiResponse.ClientAlerts);
			Assert.IsNotNull(call.ApiResponse.ClientAlerts.ClientAlertEvent);
			Assert.AreEqual(ClientAlertsEventTypeCodeType.AskSellerQuestion, call.ApiResponse.ClientAlerts.ClientAlertEvent[0].EventType);
			Assert.AreEqual(1, call.ApiResponse.ClientAlerts.ClientAlertEvent.Length);
			Assert.IsInstanceOfType(call.ApiResponse.ClientAlerts.ClientAlertEvent[0], typeof(AskSellerQuestionEventType));

			AskSellerQuestionEventType eventType = (AskSellerQuestionEventType)call.ApiResponse.ClientAlerts.ClientAlertEvent[0];
			Assert.AreEqual(new DateTime(2014, 08, 21, 3, 59, 52, 787, DateTimeKind.Utc), eventType.Timestamp);
			Assert.AreEqual("121403478858", eventType.ItemID);
			Assert.AreEqual("HURLEY Mens ALL STATE Black T Shirt Top (M L XL XXL) NEW", eventType.Title);
			Assert.AreEqual("887697677018", eventType.MessageID);
			Assert.AreEqual(MessageTypeCodeType.AskSellerQuestion, eventType.MessageType);
		}

		[TestMethod]
		public void FeedbackLeft()
		{
			JsonServiceStub stub = new JsonServiceStub(@"{
					""Timestamp"":""2014-08-28T04:17:24.636Z"",
					""Ack"":""Success"",
					""Build"":""E847_CORE_APINOT_16496971_R1"",
					""Version"":""847"",
					""ClientAlerts"":
					{
						""ClientAlertEvent"":[
						{
							""EventType"":""FeedbackLeft"",
							""FeedbackLeft"":
							{
								""EventType"":""FeedbackLeft"",
								""Timestamp"":""2014-08-28T04:16:38.272Z"",
								""FeedbackDetail"":
								{
									""CommentingUser"":""beachrife"",
									""CommentText"":""★★★★★ Thank you for shopping at BeachRife. ★★★★★"",
									""CommentType"":""Positive"",
									""ItemID"":""171389168684"",
									""Role"":""Seller"",
									""ItemTitle"":""HURLEY Mens ICON Tee T Shirt Top (S M L XL XXL) NEW"",
									""ItemPrice"":{""Value"":26.95,""CurrencyID"":""AUD""},
									""FeedbackID"":""994813814014"",
									""TransactionID"":""1256593884007"",
									""FeedbackScore"":50674
								}
							}
						}]
					},
					""SessionData"":""AQAAAUgZ6WgAAA1TSUQ9NDc0MDQyNHxTVD0xfDF8TEFDVFY9MTQwOTE5OTQ0NDYzNnxFSFdNPTE4MjAwODI1MDAxfFRJRFg9MXxMSVVQPTE0MDkxOTkyNjQyODl8UExIUz1bXTYTsAoPHttjUHE457u2q+kO4JwK""
					}

");

			GetUserAlertsCall call = new GetUserAlertsCall(this.Logger, stub);
			call.Execute();

			Assert.IsNotNull(call.ApiResponse.ClientAlerts);
			Assert.IsNotNull(call.ApiResponse.ClientAlerts.ClientAlertEvent);
			Assert.AreEqual(ClientAlertsEventTypeCodeType.FeedbackLeft, call.ApiResponse.ClientAlerts.ClientAlertEvent[0].EventType);
			Assert.AreEqual(1, call.ApiResponse.ClientAlerts.ClientAlertEvent.Length);
			Assert.IsInstanceOfType(call.ApiResponse.ClientAlerts.ClientAlertEvent[0], typeof(FeedbackLeftEventType));

			FeedbackLeftEventType eventType = (FeedbackLeftEventType)call.ApiResponse.ClientAlerts.ClientAlertEvent[0];
			Assert.AreEqual(ClientAlertsEventTypeCodeType.FeedbackLeft, eventType.EventType);
			Assert.AreEqual(new DateTime(2014, 08, 28, 4, 16, 38, 272, DateTimeKind.Utc), eventType.Timestamp);

			Assert.IsNotNull(eventType.FeedbackDetail);
			FeedbackDetailType feedbackDetail = eventType.FeedbackDetail;
			Assert.AreEqual("beachrife", feedbackDetail.CommentingUser);
			Assert.AreEqual("★★★★★ Thank you for shopping at BeachRife. ★★★★★", feedbackDetail.CommentText);
			Assert.AreEqual(CommentTypeCodeType.Positive, feedbackDetail.CommentType);
			Assert.AreEqual("171389168684", feedbackDetail.ItemID);
			Assert.AreEqual(TradingRoleCodeType.Seller, feedbackDetail.Role);
			Assert.AreEqual("HURLEY Mens ICON Tee T Shirt Top (S M L XL XXL) NEW", feedbackDetail.ItemTitle);
			Assert.IsNotNull(feedbackDetail.ItemPrice);
			Assert.AreEqual(26.95, feedbackDetail.ItemPrice.Value);
			Assert.AreEqual("AUD", feedbackDetail.ItemPrice.CurrencyID);
			Assert.AreEqual("994813814014", feedbackDetail.FeedbackID);
			Assert.AreEqual("1256593884007", feedbackDetail.TransactionID);
			Assert.AreEqual(50674, feedbackDetail.FeedbackScore);
		}

		[TestMethod]
		public void FeedbackReceived()
		{
			JsonServiceStub stub = new JsonServiceStub(@"{
					""Timestamp"":""2014-08-21T04:05:18.408Z"",
					""Ack"":""Success"",
					""Build"":""E847_CORE_APINOT_16496971_R1"",
					""Version"":""847"",
					""ClientAlerts"":
					{
						""ClientAlertEvent"":[
						{
							""EventType"":""FeedbackReceived"",
							""FeedbackReceived"":
							{
								""EventType"":""FeedbackReceived"",
								""Timestamp"":""2014-08-21T04:01:23.579Z"",
								""FeedbackDetail"":
								{
									""CommentingUser"":""cooch07"",
									""CommentText"":""Thank You"",
									""CommentType"":""Positive"",
									""ItemID"":""121272962371"",
									""Role"":""Buyer"",
									""ItemTitle"":""VON ZIPPER Boys HOWLING T Shirt Top (10 12) NEW *Billabong Co."",
									""ItemPrice"":
									{
										""Value"":19.95,
										""CurrencyID"":""AUD""
									},
									""FeedbackID"":""929232133022"",
									""TransactionID"":""1336041473002"",
									""FeedbackScore"":50540
								}
							}
						}]
					},
					""SessionData"":""AQAAAUf13OQAAA1TSUQ9NDA1MTI2M3xTVD0xfDF8TEFDVFY9MTQwODU5MzkxODM5M3xFSFdNPTE4MDUwNzc4NTMzfFRJRFg9MXxMSVVQPTE0MDg1OTMyNjE5MjJ8UExIUz1bXea4wEEeBR+lvVNEOtRmM9Gnk4Ei""
					}");

			GetUserAlertsCall call = new GetUserAlertsCall(this.Logger, stub);
			call.Execute();

			Assert.IsNotNull(call.ApiResponse.ClientAlerts);
			Assert.IsNotNull(call.ApiResponse.ClientAlerts.ClientAlertEvent);
			Assert.AreEqual(ClientAlertsEventTypeCodeType.FeedbackReceived, call.ApiResponse.ClientAlerts.ClientAlertEvent[0].EventType);
			Assert.AreEqual(1, call.ApiResponse.ClientAlerts.ClientAlertEvent.Length);
			Assert.IsInstanceOfType(call.ApiResponse.ClientAlerts.ClientAlertEvent[0], typeof(FeedbackReceivedEventType));

			FeedbackReceivedEventType eventType = (FeedbackReceivedEventType)call.ApiResponse.ClientAlerts.ClientAlertEvent[0];
			Assert.AreEqual(ClientAlertsEventTypeCodeType.FeedbackReceived, eventType.EventType);
			Assert.AreEqual(new DateTime(2014, 08, 21, 4, 1, 23, 579, DateTimeKind.Utc), eventType.Timestamp);

			Assert.IsNotNull(eventType.FeedbackDetail);
			FeedbackDetailType feedbackDetail = eventType.FeedbackDetail;
			Assert.AreEqual("cooch07", feedbackDetail.CommentingUser);
			Assert.AreEqual("Thank You", feedbackDetail.CommentText);
			Assert.AreEqual(CommentTypeCodeType.Positive, feedbackDetail.CommentType);
			Assert.AreEqual("121272962371", feedbackDetail.ItemID);
			Assert.AreEqual(TradingRoleCodeType.Buyer, feedbackDetail.Role);
			Assert.AreEqual("VON ZIPPER Boys HOWLING T Shirt Top (10 12) NEW *Billabong Co.", feedbackDetail.ItemTitle);
			Assert.IsNotNull(feedbackDetail.ItemPrice);
			Assert.AreEqual(19.95, feedbackDetail.ItemPrice.Value);
			Assert.AreEqual("AUD", feedbackDetail.ItemPrice.CurrencyID);
			Assert.AreEqual("929232133022", feedbackDetail.FeedbackID);
			Assert.AreEqual("1336041473002", feedbackDetail.TransactionID);
			Assert.AreEqual(50540, feedbackDetail.FeedbackScore);
		}

		[TestMethod]
		public void FixedPriceTransaction()
		{
			JsonServiceStub stub = new JsonServiceStub(@"{
					""Timestamp"":""2014-08-28T00:57:16.237Z"",
					""Ack"":""Success"",
					""Build"":""E847_CORE_APINOT_16496971_R1"",
					""Version"":""847"",
					""ClientAlerts"":
					{
						""ClientAlertEvent"":[
						{
							""EventType"":""FixedPriceTransaction"",
							""FixedPriceTransaction"":
							{
								""EventType"":""FixedPriceTransaction"",
								""Timestamp"":""2014-08-28T00:56:44.298Z"",
								""ItemID"":""171285956209"",
								""BidCount"":0,
								""SellerUserID"":""beachrife"",
								""EndTime"":""2014-08-28T00:56:42.000Z"",
								""CurrentPrice"":{""Value"":39.95,""CurrencyID"":""AUD""},
								""Title"":""BILLABONG Ladies ROAMING Crew Fleece Jumper (8 10) NEW"",
								""GalleryURL"":""http://i.ebayimg.com/00/s/ODAwWDgwMA==/z/QJIAAOxy4eJTOj66/$_12.JPG?set_id=880000500F"",
								""Quantity"":6,
								""Transaction"":[
								{
									""AmountPaid"":{""Value"":39.95,""CurrencyID"":""AUD""},
									""QuantitySold"":1,
									""BuyerUserID"":""mahon_laure"",
									""TransactionID"":""1259131016007"",
									""CreatedDate"":""2014-08-28T00:56:42.000Z"",
									""OrderLineItemID"":""171285956209-1259131016007""
								}]
							}
						}]
					},
					""SessionData"":""AQAAAUgZ6WgAAA1TSUQ9NDcyNzk3N3xTVD0xfDF8TEFDVFY9MTQwOTE4NzQzNjIzN3xFSFdNPTE4MTk4MjUyNzE1fFRJRFg9MnxMSVVQPTE0MDkxODcwMTU4ODJ8UExIUz1bXbjHcSgRBfSOvMHkoy4pPXzm6iNv""
				}");

			GetUserAlertsCall call = new GetUserAlertsCall(this.Logger, stub);
			call.Execute();

			Assert.IsNotNull(call.ApiResponse.ClientAlerts);
			Assert.IsNotNull(call.ApiResponse.ClientAlerts.ClientAlertEvent);
			Assert.AreEqual(ClientAlertsEventTypeCodeType.FixedPriceTransaction, call.ApiResponse.ClientAlerts.ClientAlertEvent[0].EventType);
			Assert.AreEqual(1, call.ApiResponse.ClientAlerts.ClientAlertEvent.Length);
			Assert.IsInstanceOfType(call.ApiResponse.ClientAlerts.ClientAlertEvent[0], typeof(EndOfTransactionEventType));

			EndOfTransactionEventType eventType = (EndOfTransactionEventType)call.ApiResponse.ClientAlerts.ClientAlertEvent[0];
			Assert.AreEqual(new DateTime(2014, 08, 28, 0, 56, 44, 298, DateTimeKind.Utc), eventType.Timestamp);
			Assert.AreEqual("171285956209", eventType.ItemID);
			Assert.AreEqual("beachrife", eventType.SellerUserID);
			Assert.AreEqual(new DateTime(2014, 08, 28, 0, 56, 42, 000, DateTimeKind.Utc), eventType.EndTime);
			Assert.IsNotNull(eventType.CurrentPrice);
			Assert.AreEqual(39.95d, eventType.CurrentPrice.Value);
			Assert.AreEqual("AUD", eventType.CurrentPrice.CurrencyID);
			Assert.AreEqual("BILLABONG Ladies ROAMING Crew Fleece Jumper (8 10) NEW", eventType.Title);
			Assert.AreEqual("http://i.ebayimg.com/00/s/ODAwWDgwMA==/z/QJIAAOxy4eJTOj66/$_12.JPG?set_id=880000500F", eventType.GalleryURL);
			Assert.AreEqual(6, eventType.Quantity);
			Assert.IsNotNull(eventType.Transaction);

			ClientAlertsTransactionType transactionType = eventType.Transaction;
			Assert.IsNotNull(transactionType.AmountPaid);
			Assert.AreEqual(39.95d, transactionType.AmountPaid.Value);
			Assert.AreEqual("AUD", transactionType.AmountPaid.CurrencyID);
			Assert.AreEqual(1, transactionType.QuantitySold);
			Assert.AreEqual("mahon_laure", transactionType.BuyerUserID);
			Assert.AreEqual("1259131016007", transactionType.TransactionID);
			Assert.AreEqual(new DateTime(2014, 08, 28, 0, 56, 42, 000, DateTimeKind.Utc), transactionType.CreatedDate);
			Assert.AreEqual("171285956209-1259131016007", transactionType.OrderLineItemID);
		}

		[TestMethod]
		public void ItemListed()
		{
			JsonServiceStub stub = new JsonServiceStub(@"
					{
						""Timestamp"":""2014-09-02T00:04:20.985Z"",
						""Ack"":""Success"",
						""Build"":""E847_CORE_APINOT_16496971_R1"",
						""Version"":""847"",
						""ClientAlerts"":
						{
							""ClientAlertEvent"":[
							{
								""EventType"":""ItemListed"",
								""ItemListed"":
								{
									""EventType"":""ItemListed"",
									""Timestamp"":""2014-09-02T00:03:01.813Z"",
									""ItemID"":""121426279547"",
									""SellerUserID"":""beachrife"",
									""EndTime"":""2014-10-02T00:02:53.000Z"",
									""CurrentPrice"":
									{
										""Value"":24.95,
										""CurrencyID"":""AUD""
									},
									""Title"":""BILLABONG Ladies HUDSON T Shirt Top (10 12 14) NEW"",
									""GalleryURL"":""http://i.ebayimg.com/00/s/ODAwWDgwMA==/z/KHkAAOSw7NNUBQkf/$_12.JPG?set_id=880000500F"",
									""Quantity"":7
								}
							}]
						},
						""SessionData"":""AQAAAUgZ6WgAAA1TSUQ9NTIxODQwNnxTVD0xfDF8TEFDVFY9MTQwOTYxNjI2MDk2OXxFSFdNPTE4MzE0MzQ2MTU2fFRJRFg9MnxMSVVQPTE0MDk2MTU5MDAzODd8UExIUz1bXXaz/dbWMpQ74RRKsO+sB596mdVR""
					}");

			GetUserAlertsCall call = new GetUserAlertsCall(this.Logger, stub);
			call.Execute();

			Assert.IsNotNull(call.ApiResponse.ClientAlerts);
			Assert.IsNotNull(call.ApiResponse.ClientAlerts.ClientAlertEvent);
			Assert.AreEqual(1, call.ApiResponse.ClientAlerts.ClientAlertEvent.Length);
			Assert.IsInstanceOfType(call.ApiResponse.ClientAlerts.ClientAlertEvent[0], typeof(ItemListedEventType));

			ItemListedEventType eventType = (ItemListedEventType)call.ApiResponse.ClientAlerts.ClientAlertEvent[0];
			Assert.AreEqual(ClientAlertsEventTypeCodeType.ItemListed, eventType.EventType);
			Assert.AreEqual(new DateTime(2014, 09, 02, 00, 03, 01, 813, DateTimeKind.Utc), eventType.Timestamp);
			Assert.AreEqual("121426279547", eventType.ItemID);
			Assert.AreEqual("beachrife", eventType.SellerUserID);
			Assert.AreEqual(new DateTime(2014, 10, 02, 00, 02, 53, 000, DateTimeKind.Utc), eventType.EndTime);
			Assert.IsNotNull(eventType.CurrentPrice);
			Assert.AreEqual(24.95d, eventType.CurrentPrice.Value);
			Assert.AreEqual("AUD", eventType.CurrentPrice.CurrencyID);
			Assert.AreEqual("BILLABONG Ladies HUDSON T Shirt Top (10 12 14) NEW", eventType.Title);
			Assert.AreEqual("http://i.ebayimg.com/00/s/ODAwWDgwMA==/z/KHkAAOSw7NNUBQkf/$_12.JPG?set_id=880000500F", eventType.GalleryURL);
			Assert.AreEqual(7, eventType.Quantity);
		}
		
		[TestMethod]
		public void ItemMarkedShipped()
		{
			JsonServiceStub stub = new JsonServiceStub(@"
					{
						""Timestamp"":""2014-09-02T00:04:20.985Z"",
						""Ack"":""Success"",
						""Build"":""E847_CORE_APINOT_16496971_R1"",
						""Version"":""847"",
						""ClientAlerts"":
						{
							""ClientAlertEvent"":[
							{
								""EventType"":""ItemMarkedShipped"",
								""ItemMarkedShipped"":
								{
									""EventType"":""ItemMarkedShipped"",
									""Timestamp"":""2014-09-02T03:35:10.934Z"",
									""ItemID"":""121398075049"",
									""Title"":""LKI Loosekid Industries LEAKAGE Mens MX BMX T Shirt Tee Top (S M L XL XXL) NEW"",
									""SellerUserID"":""beachrife"",
									""TransactionID"":""1345176248002"",
									""Shipment"":{""ShippedTime"":""2014-09-02T03:03:21.747Z""
								}
							}
						}]
					},
					""SessionData"":""AQAAAUgZ6WgAAA1TSUQ9NTIyOTA3N3xTVD0xfDF8TEFDVFY9MTQwOTYyODkxMzkwMXxFSFdNPTE4MzE3MjQwNjMyfFRJRFg9MXxMSVVQPTE0MDk2MjYzOTI5MzF8UExIUz1bXeBjopJzzQIMWhASkSRLff8F670h""
				}");

			GetUserAlertsCall call = new GetUserAlertsCall(this.Logger, stub);
			call.Execute();

			Assert.IsNotNull(call.ApiResponse.ClientAlerts);
			Assert.IsNotNull(call.ApiResponse.ClientAlerts.ClientAlertEvent);
			Assert.AreEqual(1, call.ApiResponse.ClientAlerts.ClientAlertEvent.Length);
			Assert.IsInstanceOfType(call.ApiResponse.ClientAlerts.ClientAlertEvent[0], typeof(ItemMarkedShippedEventType));

			ItemMarkedShippedEventType eventType = (ItemMarkedShippedEventType)call.ApiResponse.ClientAlerts.ClientAlertEvent[0];
			Assert.AreEqual(ClientAlertsEventTypeCodeType.ItemMarkedShipped, eventType.EventType);
			Assert.AreEqual(new DateTime(2014, 09, 02, 03, 35, 10, 934, DateTimeKind.Utc), eventType.Timestamp);
			Assert.AreEqual("121398075049", eventType.ItemID);
			Assert.AreEqual("LKI Loosekid Industries LEAKAGE Mens MX BMX T Shirt Tee Top (S M L XL XXL) NEW", eventType.Title);
			Assert.AreEqual("beachrife", eventType.SellerUserID);
			Assert.AreEqual("1345176248002", eventType.TransactionID);
			Assert.IsNotNull(eventType.Shipment);
			Assert.AreEqual(new DateTime(2014, 09, 02, 03, 03, 21, 747, DateTimeKind.Utc), eventType.Shipment.ShippedTime);
		}

		[TestMethod]
		public void ItemMarkedShippedAsBuyer()
		{
			JsonServiceStub stub = new JsonServiceStub(@"{
					""Timestamp"":""2014-10-04T05:36:48.428Z"",
					""Ack"":""Success""
					,""Build"":""E847_CORE_APINOT_16496971_R1"",
					""Version"":""847"",
					""ClientAlerts"":
					{
						""ClientAlertEvent"":
						[{
							""EventType"":""ItemMarkedShipped"",
							""ItemMarkedShipped"":
							{
								""EventType"":""ItemMarkedShipped"",
								""Timestamp"":""2014-10-04T05:36:18.246Z"",
								""ItemID"":""161418904065"",
								""Title"":""Adventure Time Finn Beanie - Cosplay Hat Costume"",
								""SellerUserID"":""go_figure_website"",
								""TransactionID"":""1158300587006""
							}
						}
					]},
					""SessionData"":""AQAAAUjOJ/wAAA1TSUQ9ODM5MjM1NnxTVD0xfDF8TEFDVFY9MTQxMjQwMTAwODQyOHxFSFdNPTE5MDM2NTU2OTk0fFRJRFg9MnxMSVVQPTE0MTIzODkwMDUxODF8UExIUz1bXSj6LyuhsGJT6wQuElVBbUyBQWEb""
				}");

			GetUserAlertsCall call = new GetUserAlertsCall(this.Logger, stub);
			call.Execute();

			Assert.IsNotNull(call.ApiResponse.ClientAlerts);
			Assert.IsNotNull(call.ApiResponse.ClientAlerts.ClientAlertEvent);
			Assert.AreEqual(1, call.ApiResponse.ClientAlerts.ClientAlertEvent.Length);
			Assert.IsInstanceOfType(call.ApiResponse.ClientAlerts.ClientAlertEvent[0], typeof(ItemMarkedShippedEventType));

			ItemMarkedShippedEventType eventType = (ItemMarkedShippedEventType)call.ApiResponse.ClientAlerts.ClientAlertEvent[0];
			Assert.AreEqual(ClientAlertsEventTypeCodeType.ItemMarkedShipped, eventType.EventType);
			Assert.AreEqual(new DateTime(2014, 10, 04, 5, 36, 48, 246, DateTimeKind.Utc), eventType.Timestamp);
			Assert.AreEqual("161418904065", eventType.ItemID);
			Assert.AreEqual("Adventure Time Finn Beanie - Cosplay Hat Costume", eventType.Title);
			Assert.AreEqual("go_figure_website", eventType.SellerUserID);
			Assert.AreEqual("1158300587006", eventType.TransactionID);
		}

		[TestMethod]
		public void ItemMarkedPaid()
		{
			JsonServiceStub stub = new JsonServiceStub(@"{
					""Timestamp"":""2014-09-01T23:31:23.090Z"",
					""Ack"":""Success"",
					""Build"":""E847_CORE_APINOT_16496971_R1"",
					""Version"":""847"",
					""ClientAlerts"":
					{
						""ClientAlertEvent"":[
						{
							""EventType"":""ItemMarkedPaid"",
							""ItemMarkedPaid"":
							{
								""EventType"":""ItemMarkedPaid"",
								""Timestamp"":""2014-09-01T23:30:52.854Z"",
								""ItemID"":""161374550420"",
								""Title"":""HURLEY Ladies Girls COMPACT Backpack School Sports Gym Bag NEW"",
								""SellerUserID"":""beachrife"",
								""TransactionID"":""1142845248006""
							}
						}]
					},
					""SessionData"":""AQAAAUgZ6WgAAA1TSUQ9NTIxNTUwMnxTVD0xfDF8TEFDVFY9MTQwOTYxNDI4MzA3NXxFSFdNPTE4MzEzOTM0OTIxfFRJRFg9MnxMSVVQPTE0MDk2MTMwODI0NzR8UExIUz1bXdFgn6DfGQLgUSXikS9tSH4iMPtw""
				}");


			GetUserAlertsCall call = new GetUserAlertsCall(this.Logger, stub);
			call.Execute();

			Assert.IsNotNull(call.ApiResponse.ClientAlerts);
			Assert.IsNotNull(call.ApiResponse.ClientAlerts.ClientAlertEvent);
			Assert.AreEqual(1, call.ApiResponse.ClientAlerts.ClientAlertEvent.Length);
			Assert.IsInstanceOfType(call.ApiResponse.ClientAlerts.ClientAlertEvent[0], typeof(ItemMarkedPaidEventType));

			ItemMarkedPaidEventType eventType = (ItemMarkedPaidEventType)call.ApiResponse.ClientAlerts.ClientAlertEvent[0];
			Assert.AreEqual(ClientAlertsEventTypeCodeType.ItemMarkedPaid, eventType.EventType);
			Assert.AreEqual(new DateTime(2014, 09, 01, 23, 30, 52, 854, DateTimeKind.Utc), eventType.Timestamp);
			Assert.AreEqual("161374550420", eventType.ItemID);
			Assert.AreEqual("HURLEY Ladies Girls COMPACT Backpack School Sports Gym Bag NEW", eventType.Title);
			Assert.AreEqual("beachrife", eventType.SellerUserID);
			Assert.AreEqual("1142845248006", eventType.TransactionID);
		}

		[TestMethod]
		public void ItemSold()
		{
			JsonServiceStub stub = new JsonServiceStub(@"{
					""Timestamp"":""2014-08-28T00:57:16.237Z"",
					""Ack"":""Success"",
					""Build"":""E847_CORE_APINOT_16496971_R1"",
					""Version"":""847"",
					""ClientAlerts"":
					{
						""ClientAlertEvent"":[
						{
							""EventType"":""ItemSold"",
							""ItemSold"":
							{
								""EventType"":""ItemSold"",
								""Timestamp"":""2014-08-28T00:56:48.812Z"",
								""ItemID"":""171285956209"",
								""BidCount"":0,
								""SellerUserID"":""beachrife"",
								""EndTime"":""2014-08-28T00:56:42.000Z"",
								""CurrentPrice"":{""Value"":39.95,""CurrencyID"":""AUD""},
								""Title"":""BILLABONG Ladies ROAMING Crew Fleece Jumper (8 10) NEW"",
								""GalleryURL"":""http://i.ebayimg.com/00/s/ODAwWDgwMA==/z/QJIAAOxy4eJTOj66/$_12.JPG?set_id=880000500F"",
								""Quantity"":6
							}
						}]
					},
					""SessionData"":""AQAAAUgZ6WgAAA1TSUQ9NDcyNzk3N3xTVD0xfDF8TEFDVFY9MTQwOTE4NzQzNjIzN3xFSFdNPTE4MTk4MjUyNzE1fFRJRFg9MnxMSVVQPTE0MDkxODcwMTU4ODJ8UExIUz1bXbjHcSgRBfSOvMHkoy4pPXzm6iNv""
				}");

			GetUserAlertsCall call = new GetUserAlertsCall(this.Logger, stub);
			call.Execute();

			Assert.IsNotNull(call.ApiResponse.ClientAlerts);
			Assert.IsNotNull(call.ApiResponse.ClientAlerts.ClientAlertEvent);
			Assert.AreEqual(1, call.ApiResponse.ClientAlerts.ClientAlertEvent.Length);
			Assert.IsInstanceOfType(call.ApiResponse.ClientAlerts.ClientAlertEvent[0], typeof(ItemSoldEventType));

			ItemSoldEventType eventType = (ItemSoldEventType)call.ApiResponse.ClientAlerts.ClientAlertEvent[0];
			Assert.AreEqual(ClientAlertsEventTypeCodeType.ItemSold, eventType.EventType);
			Assert.AreEqual(new DateTime(2014, 08, 28, 0, 56, 48, 812, DateTimeKind.Utc), eventType.Timestamp);
			Assert.AreEqual("171285956209", eventType.ItemID);
			Assert.AreEqual(0, eventType.BidCount);
			Assert.AreEqual("beachrife", eventType.SellerUserID);
			Assert.AreEqual(new DateTime(2014, 08, 28, 0, 56, 42, 000, DateTimeKind.Utc), eventType.EndTime);

			Assert.IsNotNull(eventType.CurrentPrice);
			Assert.AreEqual(39.95d, eventType.CurrentPrice.Value);
			Assert.AreEqual("AUD", eventType.CurrentPrice.CurrencyID);
			Assert.AreEqual("BILLABONG Ladies ROAMING Crew Fleece Jumper (8 10) NEW", eventType.Title);
			Assert.AreEqual("http://i.ebayimg.com/00/s/ODAwWDgwMA==/z/QJIAAOxy4eJTOj66/$_12.JPG?set_id=880000500F", eventType.GalleryURL);
			Assert.AreEqual(6, eventType.Quantity);
		}

		[TestMethod]
		public void ItemUnsold()
		{
			JsonServiceStub stub = new JsonServiceStub(@"
					{
						""Timestamp"":""2014-09-02T04:27:38.765Z"",
						""Ack"":""Success"",
						""Build"":""E847_CORE_APINOT_16496971_R1"",
						""Version"":""847"",
						""ClientAlerts"":
						{
							""ClientAlertEvent"":[
							{
								""EventType"":""ItemUnsold"",
								""ItemUnsold"":
								{
									""EventType"":""ItemUnsold"",
									""Timestamp"":""2014-09-02T04:27:08.244Z"",
									""ItemID"":""161377125084"",
									""BidCount"":0,
									""SellerUserID"":""beachrife"",
									""EndTime"":""2014-09-02T04:26:56.000Z"",
									""CurrentPrice"":
									{
										""Value"":19.95,
										""CurrencyID"":""AUD""
									},
									""Title"":""BILLABONG Girls HAMPTON Long Sleeve Top T Shirt (8 10 12 14) NEW"",
									""GalleryURL"":""http://i.ebayimg.com/00/s/NTAwWDUwMA==/z/7UAAAOxyeR9TJjX-/$_1.JPG?set_id=880000500F"",
									""Quantity"":21
								}
							}]
						},
						""SessionData"":""AQAAAUgZ6WgAAA1TSUQ9NTIzNDMzMXxTVD0xfDF8TEFDVFY9MTQwOTYzMjA1ODczNHxFSFdNPTE4MzE3NzczMzQ2fFRJRFg9MXxMSVVQPTE0MDk2MzE3NTg0OTZ8UExIUz1bXc8s+T41PzBgQE3R8CU4P6roZcTI""
					}");

			GetUserAlertsCall call = new GetUserAlertsCall(this.Logger, stub);
			call.Execute();

			Assert.IsNotNull(call.ApiResponse.ClientAlerts);
			Assert.IsNotNull(call.ApiResponse.ClientAlerts.ClientAlertEvent);
			Assert.AreEqual(1, call.ApiResponse.ClientAlerts.ClientAlertEvent.Length);
			Assert.IsInstanceOfType(call.ApiResponse.ClientAlerts.ClientAlertEvent[0], typeof(ItemUnsoldEventType));

			ItemUnsoldEventType eventType = (ItemUnsoldEventType)call.ApiResponse.ClientAlerts.ClientAlertEvent[0];
			Assert.AreEqual(ClientAlertsEventTypeCodeType.ItemUnsold, eventType.EventType);
			Assert.AreEqual(new DateTime(2014, 09, 02, 03, 35, 10, 934, DateTimeKind.Utc), eventType.Timestamp);
			Assert.AreEqual("161377125084", eventType.ItemID);
			Assert.AreEqual(0, eventType.BidCount);
			Assert.AreEqual("beachrife", eventType.SellerUserID);
			Assert.AreEqual(new DateTime(2014, 09, 02, 04, 26, 56, 000, DateTimeKind.Utc), eventType.EndTime);
			Assert.IsNotNull(eventType.CurrentPrice);
			Assert.AreEqual(19.95, eventType.CurrentPrice.Value);
			Assert.AreEqual("AUD", eventType.CurrentPrice.CurrencyID);
			Assert.AreEqual("BILLABONG Girls HAMPTON Long Sleeve Top T Shirt (8 10 12 14) NEW", eventType.Title);
			Assert.AreEqual("http://i.ebayimg.com/00/s/NTAwWDUwMA==/z/7UAAAOxyeR9TJjX-/$_1.JPG?set_id=880000500F", eventType.GalleryURL);
			Assert.AreEqual(21, eventType.Quantity);
		}

		// The item was purchased.
		[TestMethod]
		public void ItemWon()
		{
			JsonServiceStub stub = new JsonServiceStub(@"{
				""Timestamp"":""2014-10-04T00:40:35.626Z"",
				""Ack"":""Success"",
				""Build"":""E847_CORE_APINOT_16496971_R1"",
				""Version"":""847"",
				""ClientAlerts"":
				{
					""ClientAlertEvent"":
					[{
						""EventType"":""ItemWon"",
						""ItemWon"":
						{
							""EventType"":""ItemWon"",
							""Timestamp"":""2014-10-04T00:39:41.689Z"",
							""ItemID"":""400768556570"",
							""BidCount"":0,
							""SellerUserID"":""gogo-life"",
							""EndTime"":""2014-10-05T06:13:53.000Z"",
							""CurrentPrice"":
							{
								""Value"":12.99,
								""CurrencyID"":""AUD""
							},
							""Title"":""Solid Color Mini Shoulder Backpack Casual Nylon School Travel Camping Unisex Bag"",
							""GalleryURL"":""http://idh.jebolist.com/CA02-06-0025/001.jpg"",
							""Quantity"":28
						}
					}
				]},
				""SessionData"":""AQAAAUjOJ/wAAA1TSUQ9ODM4Njc5NHxTVD0xfDF8TEFDVFY9MTQxMjM4MzIzNTYxMHxFSFdNPTE5MDMzNTA3NTYyfFRJRFg9MnxMSVVQPTE0MTIzODI5OTUwNzF8UExIUz1bXXNuS2leyISHNnZYRC1gMI0w0M1h""
			}");

			GetUserAlertsCall call = new GetUserAlertsCall(this.Logger, stub);
			call.Execute();

			Assert.IsNotNull(call.ApiResponse.ClientAlerts);
			Assert.IsNotNull(call.ApiResponse.ClientAlerts.ClientAlertEvent);
			Assert.AreEqual(1, call.ApiResponse.ClientAlerts.ClientAlertEvent.Length);
			Assert.IsInstanceOfType(call.ApiResponse.ClientAlerts.ClientAlertEvent[0], typeof(ItemWonEventType));

			ItemWonEventType eventType = (ItemWonEventType)call.ApiResponse.ClientAlerts.ClientAlertEvent[0];
			Assert.AreEqual(ClientAlertsEventTypeCodeType.ItemWon, eventType.EventType);
			Assert.AreEqual(new DateTime(2014, 10, 04, 0, 39, 41, 689, DateTimeKind.Utc), eventType.Timestamp);
			Assert.AreEqual("400768556570", eventType.ItemID);
			Assert.AreEqual(0, eventType.BidCount);
			Assert.AreEqual("gogo-life", eventType.SellerUserID);
			Assert.AreEqual(new DateTime(2014, 10, 05, 6, 13, 53, 000, DateTimeKind.Utc), eventType.EndTime);

			Assert.IsNotNull(eventType.CurrentPrice);
			Assert.AreEqual(12.99, eventType.CurrentPrice.Value);
			Assert.AreEqual("AUD", eventType.CurrentPrice.CurrencyID);
			Assert.AreEqual("Solid Color Mini Shoulder Backpack Casual Nylon School Travel Camping Unisex Bag", eventType.Title);
			Assert.AreEqual("http://idh.jebolist.com/CA02-06-0025/001.jpg", eventType.GalleryURL);
			Assert.AreEqual(28, eventType.Quantity);
		}
		
		[TestMethod]
		public void NoAlerts()
		{
			JsonServiceStub stub = new JsonServiceStub(@"{
					""Timestamp"":""2014-08-21T03:36:20.404Z"",
					""Ack"":""Success"",
					""Build"":""E847_CORE_APINOT_16496971_R1"",
					""Version"":""847"",
					""SessionData"":""AQAAAUf13OQAAA1TSUQ9NDA1MDIwOHxTVD0xfDF8TEFDVFY9MTQwODU5MjE4MDM4OXxFSFdNPTE4MDUwNTI3OTkzfFRJRFg9MXxMSVVQPTE0MDg1OTIxNjU3NzR8UExIUz1bXfhrAOPvmDyIcgP+WJ/mkAWHrEKz""
					}");

			GetUserAlertsCall call = new GetUserAlertsCall(this.Logger, stub);
			call.Execute();

			Assert.AreEqual(new DateTime(2014, 08, 21, 3, 36, 20, 404, DateTimeKind.Utc), call.ApiResponse.Timestamp);
			Assert.AreEqual(AckCodeType.Success, call.ApiResponse.Ack);
			Assert.IsNull(call.ApiResponse.Errors);
			Assert.AreEqual("E847_CORE_APINOT_16496971_R1", call.ApiResponse.Build);
			Assert.AreEqual("847", call.ApiResponse.Version);
			Assert.IsNull(call.ApiResponse.ClientAlerts);
			Assert.AreEqual("AQAAAUf13OQAAA1TSUQ9NDA1MDIwOHxTVD0xfDF8TEFDVFY9MTQwODU5MjE4MDM4OXxFSFdNPTE4MDUwNTI3OTkzfFRJRFg9MXxMSVVQPTE0MDg1OTIxNjU3NzR8UExIUz1bXfhrAOPvmDyIcgP+WJ/mkAWHrEKz", call.ApiResponse.SessionData);
		}

		[TestMethod]
		public void WatchItemEndingSoon()
		{

			JsonServiceStub stub = new JsonServiceStub(@"
				{
					""Timestamp"":""2014-10-05T04:28:29.653Z"",
					""Ack"":""Success"",
					""Build"":""E847_CORE_APINOT_16496971_R1"",
					""Version"":""847"",
					""ClientAlerts"":
					{
						""ClientAlertEvent"":
						[{
							""EventType"":""WatchedItemEndingSoon"",
							""WatchedItemEndingSoon"":
							{
								""EventType"":""WatchedItemEndingSoon"",
								""Timestamp"":""2014-10-05T04:27:52.640Z"",
								""ItemID"":""111454158574"",
								""BidCount"":0,
								""SellerUserID"":""dontaskannie"",
								""EndTime"":""2014-10-05T04:43:07.000Z"",
								""CurrentPrice"":
								{
									""Value"":14.99,
									""CurrencyID"":""AUD""
								},
								""ViewItemURL"":""http://cgi.ebay.com.au/ws/eBayISAPI.dll?ViewItem&item=111454158574&category=20349"",
								""GalleryURL"":""http://i.ebayimg.com/00/s/NTUwWDU1MA==/z/pbAAAOSw7NNUK5vw/$_1.JPG?set_id=880000500F"",
								""Title"":""Genuine RINGKE FUSION back Soft Bumper Hybrid Case Cover For iPhone 6 4.7\"" clear""
							}
						}]
					},
					""SessionData"":""AQAAAUjOJ/wAAA1TSUQ9ODM1MTIxM3xTVD0xfDF8TEFDVFY9MTQxMjQ4MzMwOTYzOHxFSFdNPTE5MDU2NzY5MzA2fFRJRFg9MXxMSVVQPTE0MTI0ODMyNDk0MTJ8UExIUz1bXTqEBKsC8T4wMCFn90idtEJxawQW""
				}}");

			GetUserAlertsCall call = new GetUserAlertsCall(this.Logger, stub);
			call.Execute();

			Assert.IsNotNull(call.ApiResponse.ClientAlerts);
			Assert.IsNotNull(call.ApiResponse.ClientAlerts.ClientAlertEvent);
			Assert.AreEqual(1, call.ApiResponse.ClientAlerts.ClientAlertEvent.Length);
			Assert.IsInstanceOfType(call.ApiResponse.ClientAlerts.ClientAlertEvent[0], typeof(WatchedItemEndingSoonEventType));

			WatchedItemEndingSoonEventType eventType = (WatchedItemEndingSoonEventType)call.ApiResponse.ClientAlerts.ClientAlertEvent[0];
			Assert.AreEqual(ClientAlertsEventTypeCodeType.WatchedItemEndingSoon, eventType.EventType);
			Assert.AreEqual(new DateTime(2014, 10, 05, 04, 27, 52, 640, DateTimeKind.Utc), eventType.Timestamp);
			Assert.AreEqual("111454158574", eventType.ItemID);
			Assert.AreEqual(0, eventType.BidCount);
			Assert.AreEqual("dontaskannie", eventType.SellerUserID);
			Assert.AreEqual(new DateTime(2014, 10, 05, 04, 43, 07, 000, DateTimeKind.Utc), eventType.EndTime);

			Assert.IsNotNull(eventType.CurrentPrice);
			Assert.AreEqual(14.99, eventType.CurrentPrice.Value);
			Assert.AreEqual("AUD", eventType.CurrentPrice.CurrencyID);
			Assert.AreEqual("http://cgi.ebay.com.au/ws/eBayISAPI.dll?ViewItem&item=111454158574&category=20349", eventType.ViewItemURL);
			Assert.AreEqual("http://i.ebayimg.com/00/s/NTUwWDU1MA==/z/pbAAAOSw7NNUK5vw/$_1.JPG?set_id=880000500F", eventType.GalleryURL);
			Assert.AreEqual("Genuine RINGKE FUSION back Soft Bumper Hybrid Case Cover For iPhone 6 4.7\" clear", eventType.Title);
		}
	}
}
