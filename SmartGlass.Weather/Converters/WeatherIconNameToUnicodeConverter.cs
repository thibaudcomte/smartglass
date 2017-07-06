using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace SmartGlass.Weather.Converters
{
    internal class WeatherIconNameToUnicodeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var icon = value.ToString();
            switch (icon)
            {
                // clear sky
                case "01d":
                    return "\ue284";
                case "01n":
                    return "\ue28c";

                // few clouds 
                case "02d":
                    return "\ue286";
                case "02n":
                    return "\ue28d";

                // scattered clouds 
                case "03d":
                case "03n":
                    return "\ue287";

                // broken clouds 
                case "04d":
                case "04n":
                    return "\ue285";

                // shower rain 
                case "09d":
                case "09n":
                    return "\ue288";

                // rain 
                case "10d":
                case "10n":
                    return "\ue28a";

                // thunderstorm 
                case "11d":
                case "11n":
                    return "\ue289";

                // snow 
                case "13d":
                case "13n":
                    return "\ue28b";

                // mist 
                case "50d":
                case "50n":
                    return "\ue28d";
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
