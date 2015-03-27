using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model
{
	public static class Test
	{
		public static string StartJson = @"{
					""Timestamp"":""2014-08-21T04:05:18.408Z"",
					""Ack"":""Success"",
					""Build"":""E847_CORE_APINOT_16496971_R1"",
					""Version"":""847"",
					""ClientAlerts"":
					{
						""ClientAlertEvent"":[";

		public static string EndJson = @"]
					},
					""SessionData"":""AQAAAUf13OQAAA1TSUQ9NDA1MTI2M3xTVD0xfDF8TEFDVFY9MTQwODU5MzkxODM5M3xFSFdNPTE4MDUwNzc4NTMzfFRJRFg9MXxMSVVQPTE0MDg1OTMyNjE5MjJ8UExIUz1bXea4wEEeBR+lvVNEOtRmM9Gnk4Ei""
					}";

		public static string AskSellerQuestion2 = @"
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
						}";

		public static string AskSellerQuestion = @"
						{
							""EventType"":""AskSellerQuestion"",
							""AskSellerQuestion"":
							{
								""EventType"":""AskSellerQuestion"",
								""Timestamp"":""2014-10-21T23:30:46.572Z"",
								""ItemID"":""161394110562"",
								""Title"":""HURLEY Mens Rashie Wet Surf Swim Rash Vest Shirt Top (S M L) NEW"",
								""MessageID"":""926391702018"",
								""MessageType"":""AskSellerQuestion""
							}
						}";

		public static string FeedbackReceivedJson = @"
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
						}";

		public static string FixedPriceTransactionJson = @"
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
						}";
	}
}
