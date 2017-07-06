using SmartGlass.Weather.Models;
using System;

namespace SmartGlass.Weather.DesignViewModels
{
    internal class WeatherCurrentViewModel
    {
        public WeatherMetrics WeatherMetrics => new WeatherMetrics
        {
            Description = "description ...",
            DateTime = DateTime.Now,
            Temperature = 24,
            Icon = "01n"
        };
    }
}
