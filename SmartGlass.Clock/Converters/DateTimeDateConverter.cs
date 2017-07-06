using System;
using Windows.UI.Xaml.Data;

namespace SmartGlass.Clock.Converters
{
    public class DateTimeDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((DateTime)value).ToString("D");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
