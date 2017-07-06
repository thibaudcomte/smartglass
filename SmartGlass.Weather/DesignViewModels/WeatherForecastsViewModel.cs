using SmartGlass.Weather.Models;
using System;
using System.Collections.ObjectModel;

namespace SmartGlass.Weather.DesignViewModels
{
    internal class WeatherForecastsViewModel
    {
        public ObservableCollection<WeatherMetrics> WeatherForecasts { get; }

        public WeatherForecastsViewModel()
        {
            WeatherForecasts = new ObservableCollection<WeatherMetrics>();
            WeatherForecasts.Add(new WeatherMetrics
            {
                DateTime = DateTime.Now,
                Description = "description",
                MinTemperature = 11,
                MaxTemperature = 25,
                Icon = "01n"
            });
            WeatherForecasts.Add(new WeatherMetrics
            {
                DateTime = DateTime.Now.AddDays(1),
                Description = "description",
                MinTemperature = 14,
                MaxTemperature = 33,
                Icon = "02n"
            });
            WeatherForecasts.Add(new WeatherMetrics
            {
                DateTime = DateTime.Now.AddDays(2),
                Description = "description",
                MinTemperature = 12,
                MaxTemperature = 29,
                Icon = "09n"
            });
        }
    }
}
