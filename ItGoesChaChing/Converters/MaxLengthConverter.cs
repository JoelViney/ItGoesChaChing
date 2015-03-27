using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ItGoesChaChing.Converters
{

	/// <summary>
	/// Converts a string to a maximum length
	/// </summary>
	public class MaxLengthConverter : IValueConverter
	{
		public int MaxLength { get; set; }

		public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{

			try
			{
				string str = value as string;

				if (str == null)
					return null;

				if (str.Length <= this.MaxLength)
					return str;

				return str.Substring(0, this.MaxLength - 3) + "...";
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
