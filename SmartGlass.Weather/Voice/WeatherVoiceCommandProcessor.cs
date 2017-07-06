using SmartGlass.Core.Commanding;
using SmartGlass.Core.UI.Regions;
using SmartGlass.Weather.Service;
using SmartGlass.Weather.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Globalization;

namespace SmartGlass.Weather.Voice
{
    internal class WeatherVoiceCommandProcessor : IVoiceCommandProcessor
    {
        private IRegionManager _RegionManager;
        private IWeatherService _Service;

        internal WeatherVoiceCommandProcessor(IRegionManager regionManager, IWeatherService service)
        {
            _RegionManager = regionManager;
            _Service = service;
        }

        public Uri GetVoiceCommandGrammarFileUri(Language language)
        {
            switch (language.LanguageTag)
            {
                case "en-CA": return new Uri("ms-appx:///SmartGlass.Weather/Voice/commands.en-CA.xml");
                case "fr-CA": return new Uri("ms-appx:///SmartGlass.Weather/Voice/commands.fr-CA.xml");
            }

            return null;
        }

        public async Task<bool> ProcessRecognizedVoiceCommandAsync(string command, IReadOnlyDictionary<string, string> tags)
        {
            if (!tags.ContainsKey("Module") || tags["Module"] != "weather")
                return false;

            if (!tags.ContainsKey("Verb"))
                return false;

            if (tags["Verb"] == "show")
            {
                await _RegionManager.ActivateRegionViewAsync(ERegionLocation.Center, ViewNames.WeatherForecastsViewName);
            }
            else if(tags["Verb"] == "hide")
            {
                await _RegionManager.DeactivateRegionAsync(ERegionLocation.Center);
            }
            else if (tags["Verb"] == "update")
            {
                await _Service.UpdateAsync();
            }

            return true;
        }
    }
}
