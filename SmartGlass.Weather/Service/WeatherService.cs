using SmartGlass.Weather.Models;
using SmartGlass.Weather.Settings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace SmartGlass.Weather.Service
{
    internal class WeatherService : IWeatherService
    {
        public WeatherMetrics WeatherCurrent { get; private set; }

        private List<WeatherMetrics> _WeatherForecasts;
        public IEnumerable<WeatherMetrics> WeatherForecasts => _WeatherForecasts;

        private readonly IWeatherSettings _Settings;
        private readonly string _WeatherCurrentUri;
        private readonly string _WeatherForecastUri;

        public WeatherService(IWeatherSettings settings)
        {
            _Settings = settings;

            _WeatherCurrentUri = string.Format("http://api.openweathermap.org/data/2.5/weather?zip={0},{1}&units=metric&appId={2}&lang={3}",
                _Settings.ZipCode, _Settings.Country, _Settings.ApiKey, CultureInfo.CurrentCulture.TwoLetterISOLanguageName);

            _WeatherForecastUri = string.Format("http://api.openweathermap.org/data/2.5/forecast?zip={0},{1}&units=metric&appId={2}&lang={3}",
                _Settings.ZipCode, _Settings.Country, _Settings.ApiKey, CultureInfo.CurrentCulture.TwoLetterISOLanguageName);

            _WeatherForecasts = new List<WeatherMetrics>();
        }

        private async Task UpdateWeatherCurrentAsync()
        {
            using (var client = new HttpClient())
            {
                while (true)
                {
                    var response = await client.GetStringAsync(_WeatherCurrentUri);
                    var metrics = CurrentFromJson(response);
                    if (metrics != null)
                    {
                        WeatherCurrent = metrics;
                        break;
                    }

                    // use a 2 seconds delay so we don't exceed the openweather quota
                    await Task.Delay(2000);
                }
            }
        }

        private static WeatherMetrics CurrentFromJson(string response)
        {
            try
            {
                var json = JsonValue.Parse(response).GetObject();

                if (json.GetNamedNumber("cod") != 200)
                    return null;

                var metrics = new WeatherMetrics();

                metrics.DateTime = DateTimeOffset.FromUnixTimeSeconds((long)json.GetNamedNumber("dt"));
                metrics.Temperature = (int)json.GetNamedObject("main").GetNamedNumber("temp");
                var sys = json.GetNamedObject("sys");
                metrics.Sunrise = DateTimeOffset.FromUnixTimeSeconds((long)sys.GetNamedNumber("sunrise"));
                metrics.Sunset = DateTimeOffset.FromUnixTimeSeconds((long)sys.GetNamedNumber("sunset"));
                var weather = json.GetNamedArray("weather").GetObjectAt(0);
                metrics.Description = weather.GetNamedString("description");
                metrics.Icon = weather.GetNamedString("icon");

                return metrics;
            }
            catch
            {
                return null;
            }
        }

        private async Task UpdateWeatherForecastsAsync()
        {
            using (var client = new HttpClient())
            {
                IEnumerable<WeatherMetrics> fullDayForecasts = null;
                while (true)
                {
                    var response = await client.GetStringAsync(_WeatherForecastUri);
                    fullDayForecasts = ForecastsFromJson(response);
                    if (fullDayForecasts != null)
                        break;

                    // use a 2 seconds delay so we don't exceed the openweather quota
                    await Task.Delay(2000);
                }

                // remove all metrics and replace them with new and more accurate ones
                // however, if today's not included in the new data, keep it in the original data

                if (fullDayForecasts.First().DateTime.DayOfYear == DateTime.Today.DayOfYear)
                {
                    _WeatherForecasts.Clear();
                }
                else
                {
                    _WeatherForecasts.RemoveAll(m => m.DateTime.DayOfYear != DateTime.Today.DayOfYear);
                }

                // use latest forecasts
                _WeatherForecasts.AddRange(fullDayForecasts);
            }
        }

        private static IEnumerable<WeatherMetrics> ForecastsFromJson(string response)
        {
            try
            {
                var json = JsonValue.Parse(response).GetObject();

                if (json.GetNamedString("cod") != "200")
                    return null;

                var array = json.GetNamedArray("list");

                // position the data to the next day start time (hour 0)
                uint startIndex = 0;
                while (true)
                {
                    var dt = DateTimeOffset.FromUnixTimeSeconds((long)array.GetObjectAt(startIndex).GetNamedNumber("dt"));
                    if (dt.Hour == 0)
                        break;
                    startIndex++;
                }

                var forecasts = new List<WeatherMetrics>();
                WeatherMetrics metrics = null;

                // start at the very beginning of a day
                for (uint i = startIndex; i < json.GetNamedNumber("cnt"); i++)
                {
                    var item = array.GetObjectAt(i);

                    var dt = DateTimeOffset.FromUnixTimeSeconds((long)item.GetNamedNumber("dt"));

                    if ((dt.Hour == 0) && (metrics != null))
                    {
                        forecasts.Add(metrics);
                        metrics = null;
                    }

                    if (metrics == null)
                    {
                        metrics = new WeatherMetrics()
                        {
                            MinTemperature = int.MaxValue,
                            MaxTemperature = int.MinValue
                        };
                    }

                    var temperature = (int)item.GetNamedObject("main").GetNamedNumber("temp");
                    if (temperature < metrics.MinTemperature)
                        metrics.MinTemperature = temperature;
                    if (temperature > metrics.MaxTemperature)
                        metrics.MaxTemperature = temperature;

                    if (dt.Hour == 12)
                    {
                        metrics.DateTime = dt;
                        var weather = item.GetNamedArray("weather").GetObjectAt(0);
                        metrics.Description = weather.GetNamedString("description");
                        metrics.Icon = weather.GetNamedString("icon");
                    }
                }

                return forecasts;
            }
            catch
            {
                return null;
            }
        }

        public async Task UpdateAsync()
        {
            await UpdateWeatherCurrentAsync();
            await UpdateWeatherForecastsAsync();

            WeatherUpdated?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler WeatherUpdated;
    }
}
