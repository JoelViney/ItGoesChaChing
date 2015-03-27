using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace ItGoesChaChing.Ebay.ClientAlerts.Json
{
	[DataContract]
	public class EndOfTransactionEventType : ListingEventTypesBase
	{
		/// <summary>Information about one transaction.</summary>
		[DataMember(Name = "Transaction")] 
		private ClientAlertsTransactionType[] TransactionArray { get; set; }

		/// <summary>Information about one transaction.</summary>
		public ClientAlertsTransactionType Transaction 
		{
			get 
			{
				if (this.TransactionArray == null || this.TransactionArray.Length == 0)
					return null;

				return this.TransactionArray[0];  
			}
			set
			{
				if (this.TransactionArray == null || this.TransactionArray.Length == 0)
					this.TransactionArray = new ClientAlertsTransactionType[1];

				this.TransactionArray[0] = value;
			}
		}
	}

	[DataContract]
	public class ClientAlertsTransactionType
	{
		/// <summary>The amount the buyer paid for the item(s) in the transaction.</summary>
		[DataMember] public AmountType AmountPaid { get; set; }

		/// <summary>eBay user ID of the buyer.</summary>
		[DataMember] public string BuyerUserID { get; set; }
		
		/// <summary>Indicates the date and time a transaction was created.</summary>
		[DataMember] public DateTime CreatedDate { get; set; }

		/// <summary>
		/// A unique identifier for every purchase from the seller. When a 
		/// buyer purchases multiple items from the same listing, each item 
		/// purchased will have an Order Line Item ID and all items in that 
		/// purchase will have the same Order ID. OrderLineItemID is based 
		/// upon the combination of the eBay Trading API's ItemID and 
		/// TransactionID fields. The number before the hyphen is the Item ID 
		/// and the number after the hyphen is the Transaction ID.
		/// </summary>
		[DataMember] public string OrderLineItemID { get; set; }

		/// <summary>Number of items purchased.</summary>
		[DataMember] public int QuantitySold { get; set; }

		/// <summary>Unique identifier for a single transaction.</summary>
		[DataMember] public string TransactionID { get; set; }
	}
}
