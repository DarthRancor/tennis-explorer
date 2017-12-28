using Xamarin.Forms;

namespace TennisExplorer.CustomElements
{
    public class ExtendedViewCell : ViewCell
    {
        public static readonly BindableProperty SelectedBackgroundColorProperty = BindableProperty.Create("SelectedBackgroundColor", typeof(Color), typeof(ExtendedViewCell), Color.Default);

        public Color SelectedBackgroundColor
        {
            get
            {
                var backgroundColor = (Color) GetValue(SelectedBackgroundColorProperty);
                if (backgroundColor == Color.Default)
                {
                    backgroundColor = Color.FromHex("#c8e6c9");
                }

                return backgroundColor;
            }
            set { SetValue(SelectedBackgroundColorProperty, value); }
        }
    }
}
