using ItGoesChaChing.Ebay.ClientAlerts.Json;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ItGoesChaChing.Converters
{
	/// <summary>
	/// Convert an Amount type to the string representation of the currency.
	/// </summary>
	public class AmountTypeConverter : IValueConverter
	{
		public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				AmountType amountType = value as AmountType;

				if (amountType == null)
					return String.Format("{C:0}", value);
				
				string cultureString = "";
				string currencySymbol = "";
				switch (amountType.CurrencyID)
				{
					case "AUD": currencySymbol = "$"; cultureString = "en-AU"; break; // Australian Dollar
					case "CAD": currencySymbol = "$"; cultureString = "en-CA"; break; // Canadian Dollar.
					case "CHF": currencySymbol = "Fr"; cultureString = "fr-CH"; break; // Swiss Franc. 
					case "CNY": currencySymbol = "¥"; cultureString = "zh-CN"; break; // Chinese Yuan Renminbi.
					case "EUR": currencySymbol = "€"; cultureString = "en-GB"; break; // Euro. 
					case "GBP": currencySymbol = "£"; cultureString = "en-GB"; break; // Pound Sterling. 
					case "HKD": currencySymbol = "$"; cultureString = "zh-HK"; break; // Hong Kong Dollar.
					case "INR": currencySymbol = "₹"; cultureString = "gu-IN"; break; // Indian Rupee. 
					case "MYR": currencySymbol = "RM"; cultureString = "ms-MY"; break; // Malaysian Ringgit. 
					case "PHP": currencySymbol = "₱"; cultureString = "en-PH"; break; // Philippines Peso. 
					case "PLN": currencySymbol = "zł"; cultureString = "pl-PL"; break; // Poland, Zloty.
					case "SEK": currencySymbol = "kr"; cultureString = "sv-SE"; break; // Swedish Krona. 
					case "SGD": currencySymbol = "$"; cultureString = "zh-SG"; break; // Singapore Dollar. 
					case "USD": currencySymbol = "$"; cultureString = "en-US"; break; // US Dollar.
					default: return String.Format("{C:0}", value); 
				}

				CultureInfo cultureInfo = CultureInfo.GetCultureInfo(cultureString);
				NumberFormatInfo numberFormatInfo = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
				numberFormatInfo.CurrencySymbol = currencySymbol; // Replace with "$" or "£" or whatever you need

				// Output: "€ 12.30" if the CurrentCulture is "en-US", "12,30 €" if the CurrentCulture is "fr-FR".
				string formattedPrice = amountType.Value.ToString("C", numberFormatInfo);

				return formattedPrice;
			}
			catch (InvalidCastException)
			{
				return Binding.DoNothing;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
