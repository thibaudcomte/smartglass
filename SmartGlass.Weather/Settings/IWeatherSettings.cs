namespace SmartGlass.Weather.Settings
{
    internal interface IWeatherSettings
    {
        string ApiKey { get; }
        string ZipCode { get; }
        string Country { get; }
    }
}
