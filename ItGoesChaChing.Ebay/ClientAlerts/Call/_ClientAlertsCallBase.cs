using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace ItGoesChaChing.Ebay.ClientAlerts.Call
{
	public abstract class ClientAlertsCallBase
	{
		protected readonly string UrlBase = @"http://clientalerts.ebay.com/ws/ecasvc/ClientAlerts";

		protected ILogger Logger { get; set; }
		protected IJsonWebRequest JsonWebService { get; private set; }

		#region Constructors...

		/// <summary>Default constructor.</summary>
		public ClientAlertsCallBase(ILogger logger)
		{
			this.Logger = logger;
			this.JsonWebService = new JsonWebRequest();
		}

		/// <summary>This constructor is used in unit tests.</summary>
		public ClientAlertsCallBase(ILogger logger, IJsonWebRequest jsonWebService)
		{
			this.Logger = logger;
			this.JsonWebService = jsonWebService;
		}

		#endregion

		protected string EscapeParam(string str)
		{
			return this.JsonWebService.EscapeParam(str);
		}
	}
}
