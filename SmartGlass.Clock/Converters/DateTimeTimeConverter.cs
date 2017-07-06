using System;
using Windows.UI.Xaml.Data;

namespace SmartGlass.Clock.Converters
{
    public class DateTimeTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((DateTime)value).ToString("T");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
