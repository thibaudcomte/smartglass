using SmartGlass.Core.Commanding;
using SmartGlass.Core.UI.Regions;
using SmartGlass.News.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Globalization;

namespace SmartGlass.News.Voice
{
    public class NewsVoiceCommandProcessor : IVoiceCommandProcessor
    {
        private IRegionManager _RegionManager;

        public NewsVoiceCommandProcessor(IRegionManager regionManager)
        {
            _RegionManager = regionManager;
        }

        public Uri GetVoiceCommandGrammarFileUri(Language language)
        {
            switch (language.LanguageTag)
            {
                case "en-CA": return new Uri("ms-appx:///SmartGlass.News/Voice/commands.en-CA.xml");
                case "fr-CA": return new Uri("ms-appx:///SmartGlass.News/Voice/commands.fr-CA.xml");
            }

            return null;
        }

        public async Task<bool> ProcessRecognizedVoiceCommandAsync(string command, IReadOnlyDictionary<string, string> tags)
        {
            if (!tags.ContainsKey("Module") || tags["Module"] != "news")
                return false;

            if (!tags.ContainsKey("Verb"))
                return false;

            if (tags["Verb"] == "show")
            {
                await _RegionManager.ActivateRegionViewAsync(ERegionLocation.Center, ViewNames.NewsViewName);
            }
            else if (tags["Verb"] == "hide")
            {
                await _RegionManager.DeactivateRegionAsync(ERegionLocation.Center);
            }

            return true;
        }
    }
}
