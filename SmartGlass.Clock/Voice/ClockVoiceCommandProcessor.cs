using SmartGlass.Clock.Views;
using SmartGlass.Core.Commanding;
using SmartGlass.Core.UI.Regions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Globalization;

namespace SmartGlass.Clock.Voice
{
    public class ClockVoiceCommandProcessor : IVoiceCommandProcessor
    {
        private IRegionManager _RegionManager;

        public ClockVoiceCommandProcessor(IRegionManager regionManager)
        {
            _RegionManager = regionManager;
        }

        public Uri GetVoiceCommandGrammarFileUri(Language language)
        {
            switch (language.LanguageTag)
            {
                case "en-CA": return new Uri("ms-appx:///SmartGlass.Clock/Voice/commands.en-CA.xml");
                case "fr-CA": return new Uri("ms-appx:///SmartGlass.Clock/Voice/commands.fr-CA.xml");
            }

            return null;
        }

        public async Task<bool> ProcessRecognizedVoiceCommandAsync(string command, IReadOnlyDictionary<string, string> tags)
        {
            if (!tags.ContainsKey("Module") || tags["Module"] != "clock")
                return false;

            if (!tags.ContainsKey("Verb"))
                return false;

            if (tags["Verb"] == "show")
            {
                await _RegionManager.ActivateRegionViewAsync(ERegionLocation.Center, ViewNames.AuxiliaryClocksViewName);
            }
            else if (tags["Verb"] == "hide")
            {
                await _RegionManager.DeactivateRegionAsync(ERegionLocation.Center);
            }

            return true;
        }
    }
}
