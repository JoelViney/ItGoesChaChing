using System.Globalization;
using System.Runtime.Serialization;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	[DataContract]
	public class AmountType
	{
		[DataMember] public double Value { get; set; }

		[DataMember(Name = "CurrencyID")] 
		public string CurrencyID { get; set; }

		#region Constructors...

		public AmountType()
		{

		}

		public AmountType(double value, string currencyId)
		{
			this.Value = value;
			this.CurrencyID = currencyId;
		}

		#endregion
	}
}
