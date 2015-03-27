using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ItGoesChaChing.Ebay.ClientAlerts.Call;
using ItGoesChaChing.Ebay.ClientAlerts.Json;

namespace ItGoesChaChing.Ebay.Test
{
	[TestClass]
	public class UnitTest1
	{
		private ILogger Logger { get; set; }

		[TestInitialize]
		public void TestInitialise()
		{
			this.Logger = new ConsoleLogger();
		}

		[TestMethod]
		public void LoginSuccess()
		{
			JsonServiceStub stub = new JsonServiceStub(@"{
					""Timestamp"":""2014-08-21T03:14:11.459Z"",
					""Ack"":""Success"",
					""Build"":""E847_CORE_APINOT_16496971_R1"",
					""Version"":""847"",
					""SessionID"":""AQAAAUf13OQAAA0xfDQwMzc5MDV8MTEwODE1NDAwfDE1OTc5NnwxNDA4NjY2NDUxNDI4NNa5GgB0FOwVt9ya/u25En4gbTE="",
					""SessionData"":""AQAAAUf13OQAAA1TSUQ9NDAzNzkwNXxTVD0xfDF8TEFDVFY9MTQwODU4MDA1MTQxM3xFSFdNPTE4MDQ3OTYwMjkzfFRJRFg9MnxMSVVQPTE0MDg1ODAwNTE0MTN8UExIUz1bXZdcdmU9PhPhgr4v7o3B+UAmlI5S""
					}");

			LoginCall call = new LoginCall(this.Logger, stub);
			call.Execute();

			Assert.AreEqual(new DateTime(2014, 08, 21, 3, 14, 11, 459, DateTimeKind.Utc), call.ApiResponse.Timestamp);
			Assert.AreEqual(AckCodeType.Success, call.ApiResponse.Ack);
			Assert.IsNull(call.ApiResponse.Errors);
			Assert.AreEqual("E847_CORE_APINOT_16496971_R1", call.ApiResponse.Build);
			Assert.AreEqual("847", call.ApiResponse.Version);
			Assert.AreEqual("AQAAAUf13OQAAA0xfDQwMzc5MDV8MTEwODE1NDAwfDE1OTc5NnwxNDA4NjY2NDUxNDI4NNa5GgB0FOwVt9ya/u25En4gbTE=", call.ApiResponse.SessionID);
			Assert.AreEqual("AQAAAUf13OQAAA1TSUQ9NDAzNzkwNXxTVD0xfDF8TEFDVFY9MTQwODU4MDA1MTQxM3xFSFdNPTE4MDQ3OTYwMjkzfFRJRFg9MnxMSVVQPTE0MDg1ODAwNTE0MTN8UExIUz1bXZdcdmU9PhPhgr4v7o3B+UAmlI5S", call.ApiResponse.SessionData);
		}

		[TestMethod]
		public void LoginError()
		{
			JsonServiceStub stub = new JsonServiceStub(@"{
					""Timestamp"":""2014-08-21T03:17:33.871Z"",
					""Ack"":""Failure"",
					""Errors"":[
					{
						""ShortMessage"":""Invalid ClientAlerts Auth Token."",
						""LongMessage"":""The ClientAlerts Auth Token that you provided is not valid."",
						""ErrorCode"":""11.1"",
						""SeverityCode"":""Error"",
						""ErrorParameters"":[
						{
							""ParamID"":""0""
						}],
					""ErrorClassification"":""RequestError""
					}],
					""Build"":""E847_CORE_APINOT_16496971_R1"",
					""Version"":""847""
					}");

			LoginCall call = new LoginCall(this.Logger, stub);
			call.Execute();

			Assert.AreEqual(new DateTime(2014, 08, 21, 3, 17, 33, 871, DateTimeKind.Utc), call.ApiResponse.Timestamp);
			Assert.AreEqual(AckCodeType.Failure, call.ApiResponse.Ack);
			Assert.AreEqual(1, call.ApiResponse.Errors.Length);
			ErrorType errorType = call.ApiResponse.Errors[0];
			Assert.AreEqual("Invalid ClientAlerts Auth Token.", errorType.ShortMessage);
			Assert.AreEqual("The ClientAlerts Auth Token that you provided is not valid.", errorType.LongMessage);
			Assert.AreEqual("11.1", errorType.ErrorCode);
			Assert.AreEqual("Error", errorType.SeverityCode);
			Assert.AreEqual(1, errorType.ErrorParameters.Length);
			ErrorParameterType parameterType = errorType.ErrorParameters[0];
			Assert.AreEqual("0", parameterType.ParamID);
			Assert.AreEqual(ErrorClassificationCodeType.RequestError, errorType.ErrorClassification);
			Assert.AreEqual("E847_CORE_APINOT_16496971_R1", call.ApiResponse.Build);
			Assert.AreEqual("847", call.ApiResponse.Version);
		}
	}
}
