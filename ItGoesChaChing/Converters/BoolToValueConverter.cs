using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ItGoesChaChing.Converters
{
	public class BoolToBoolConverter : BoolToValueConverter<bool> { }
	public class BoolToVisibilityConverter : BoolToValueConverter<Visibility> { }
	public class BoolToStringConverter : BoolToValueConverter<String> { }
	// public class BoolToBrushConverter : BoolToValueConverter<Brush> { }
	// public class BoolToObjectConverter : BoolToValueConverter<Object> { }

	/// <summary>
	/// Converts a boolean value to something else that can be used in bindings
	/// such as a Brush value or a Visibility value.
	/// </summary>
	public class BoolToValueConverter<T> : IValueConverter
	{
		public T FalseValue { get; set; }
		public T TrueValue { get; set; }

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is bool)
			{
				return (bool)value ? TrueValue : FalseValue;
			}
			else if (value is bool?)
			{
				bool? nullable = (bool?)value;
				if (nullable.HasValue)
				{
					return nullable.Value ? TrueValue : FalseValue;
				}
				else
				{
					return FalseValue; // Default null to false.
				}
			}
			else
			{
				return FalseValue; // If its not a bool or a bool? just return false.
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
