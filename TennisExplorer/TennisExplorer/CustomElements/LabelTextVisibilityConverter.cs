using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TennisExplorer.CustomElements
{
    public class LabelTextVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isVisible = false;
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                isVisible = true;
            }

            return isVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
