using ItGoesChaChing.Ebay.ClientAlerts.Call;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Ebay
{
	public class JsonServiceStub : IJsonWebRequest
	{
		private string _expectedOutput;

		public JsonServiceStub(string expectedOutput)
		{
			this._expectedOutput = expectedOutput;
		}

		public string Call(string url)
		{
			return this._expectedOutput;
		}

		public string EscapeParam(string str)
		{
			return str;
		}

	}
}
