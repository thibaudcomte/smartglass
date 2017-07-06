using Prism.Mvvm;
using SmartGlass.Weather.Models;
using SmartGlass.Weather.Service;
using System;
using System.Threading;

namespace SmartGlass.Weather.ViewModels
{
    internal class WeatherCurrentViewModel : BindableBase
    {
        private WeatherMetrics _WeatherMetrics;
        public WeatherMetrics WeatherMetrics
        {
            get { return _WeatherMetrics; }
            set { SetProperty(ref _WeatherMetrics, value); }
        }

        private readonly IWeatherService _Service;
        private readonly SynchronizationContext _SyncContext;

        public WeatherCurrentViewModel(IWeatherService service)
        {
            _Service = service;
            _SyncContext = SynchronizationContext.Current;
            _Service.WeatherUpdated += _Service_WeatherUpdated;
        }

        private void _Service_WeatherUpdated(object sender, EventArgs e)
        {
            _SyncContext.Post((o) =>
            {
                WeatherMetrics = _Service.WeatherCurrent;
            }, null);
        }
    }
}
