using System;
using Windows.UI.Xaml.Data;

namespace SmartGlass.Core.UI.Converters
{
    public class FlipBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
