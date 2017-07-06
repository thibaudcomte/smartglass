using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace SmartGlass.Weather.Converters
{
    internal class DateTimeToDayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var dt = (DateTimeOffset)value;
            return $"{dt.DayOfWeek.ToString().ToLower()} {dt.Day}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
