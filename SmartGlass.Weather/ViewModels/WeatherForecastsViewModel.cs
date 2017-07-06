using Prism.Mvvm;
using SmartGlass.Weather.Models;
using SmartGlass.Weather.Service;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace SmartGlass.Weather.ViewModels
{
    internal class WeatherForecastsViewModel : BindableBase
    {
        public ObservableCollection<WeatherMetrics> WeatherForecasts { get; }

        private readonly IWeatherService _Service;
        private readonly SynchronizationContext _SyncContext;

        public WeatherForecastsViewModel(IWeatherService service)
        {
            _Service = service;
            _SyncContext = SynchronizationContext.Current;
            WeatherForecasts = new ObservableCollection<WeatherMetrics>();
            _Service.WeatherUpdated += _Service_WeatherUpdated;
        }

        private void _Service_WeatherUpdated(object sender, EventArgs e)
        {
            _SyncContext.Post((o) =>
            {
                WeatherForecasts.Clear();
                foreach (var forecast in _Service.WeatherForecasts)
                {
                    WeatherForecasts.Add(forecast);
                }
            }, null);
        }
    }
}
