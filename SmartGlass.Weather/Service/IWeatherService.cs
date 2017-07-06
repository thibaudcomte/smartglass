using SmartGlass.Weather.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGlass.Weather.Service
{
    internal interface IWeatherService
    {
        WeatherMetrics WeatherCurrent { get; }
        IEnumerable<WeatherMetrics> WeatherForecasts { get; }
        Task UpdateAsync();
        event EventHandler WeatherUpdated;
    }
}
