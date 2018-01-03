using System;
using System.Globalization;
using Xamarin.Forms;

namespace TennisExplorer.CustomElements
{
	public class InverseBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var invertedValue = !((bool)value);
			return invertedValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}
