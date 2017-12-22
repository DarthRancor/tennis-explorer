using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TennisExplorer.CustomElements
{
    public class LabelTextHighlighterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var backColor = Color.Transparent;
            var match = value as Models.TennisMatch;
            if (match != null && match.IsFavorite)
            {
                backColor = Color.Yellow;
            }

            return backColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
