using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ItGoesChaChing.Ebay.ClientAlerts.Call;
using ItGoesChaChing.Ebay.ClientAlerts.Json;

namespace ItGoesChaChing.Ebay.Test
{
	[TestClass]
	public class LogoutTest
	{
		private ILogger Logger { get; set; }

		[TestInitialize]
		public void TestInitialise()
		{
			this.Logger = new ConsoleLogger();
		}
		
		// This is a cutdown version of the LoginTest as it simply has 2 less properties in the return result.
		// Because of this I am not sure this test is worth having, but it is here for completeness.
		[TestMethod]
		public void LogoutSuccess()
		{
			JsonServiceStub stub = new JsonServiceStub(@"{
					""Timestamp"":""2014-08-21T03:14:11.459Z"",
					""Ack"":""Success"",
					""Build"":""E847_CORE_APINOT_16496971_R1"",
					""CorrelationID"":""1234"",
					""Version"":""847"",
					}");

			LogoutCall call = new LogoutCall(this.Logger, stub);
			call.Execute();

			Assert.AreEqual(new DateTime(2014, 08, 21, 3, 14, 11, 459, DateTimeKind.Utc), call.ApiResponse.Timestamp);
			Assert.AreEqual(AckCodeType.Success, call.ApiResponse.Ack);
			Assert.IsNull(call.ApiResponse.Errors);
			Assert.AreEqual("E847_CORE_APINOT_16496971_R1", call.ApiResponse.Build);
			Assert.AreEqual("1234", call.ApiResponse.CorrelationID);
			Assert.AreEqual("847", call.ApiResponse.Version);
		}
	}
}
