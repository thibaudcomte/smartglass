using SmartGlass.Core.Commanding;
using SmartGlass.Core.UI.Regions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Globalization;

namespace SmartGlass.App.Voice
{
    public class MainPageVoiceCommandProcessor : IVoiceCommandProcessor
    {
        private IRegionManager _RegionManager;

        public MainPageVoiceCommandProcessor(IRegionManager regionManager)
        {
            _RegionManager = regionManager;
        }

        public Uri GetVoiceCommandGrammarFileUri(Language language)
        {
            switch (language.LanguageTag)
            {
                case "en-CA": return new Uri("ms-appx:///Voice/commands.en-CA.xml");
                case "fr-CA": return new Uri("ms-appx:///Voice/commands.fr-CA.xml");
            }

            return null;
        }

        public async Task<bool> ProcessRecognizedVoiceCommandAsync(string command, IReadOnlyDictionary<string, string> tags)
        {
            if (!tags.ContainsKey("Module") || tags["Module"] != "app")
                return false;

            if (!tags.ContainsKey("Action"))
                return false;

            if (tags["Action"] == "goHome")
            {
                await _RegionManager.DeactivateRegionAsync(ERegionLocation.Center);
            }
            else if (tags["Action"] == "hideDisplay")
            {
                _RegionManager.SetLayoutRootVisibility(false);
            }
            else if (tags["Action"] == "showDisplay")
            {
                _RegionManager.SetLayoutRootVisibility(true);
            }

            return true;
        }
    }
}
