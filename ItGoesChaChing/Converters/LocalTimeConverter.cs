using System;
using System.Globalization;
using System.Windows.Data;

namespace ItGoesChaChing.Converters
{
	/// <summary>
	/// Convert a datatime to a local datetime
	/// </summary>
	public class LocalTimeConverter : IValueConverter
	{
		public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{

			try
			{
				DateTime? date = value as DateTime?;
				if (date == null)
					return null;

				if (date.Value.Ticks == 0)
					return null;

				var datetime = (DateTime)System.Convert.ChangeType(value, typeof(DateTime));
				return datetime.ToLocalTime();
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
