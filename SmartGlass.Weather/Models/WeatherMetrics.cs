using System;

namespace SmartGlass.Weather.Models
{
    internal class WeatherMetrics
    {
        public string Description { get; set; }
        public int Temperature { get; set; }
        public int MinTemperature { get; set; }
        public int MaxTemperature { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public DateTimeOffset Sunrise { get; set; }
        public DateTimeOffset Sunset { get; set; }
        public string Icon { get; set; }
    }
}
