using SmartGlass.Core.Commanding;
using SmartGlass.Core.UI.Regions;
using SmartGlass.Radio.Service;
using SmartGlass.Radio.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Globalization;

namespace SmartGlass.Radio.Voice
{
    public class RadioVoiceCommandProcessor : IVoiceCommandProcessor
    {
        private IRegionManager _RegionManager;
        private IRadioService _RadioService;

        public RadioVoiceCommandProcessor(IRegionManager regionManager, IRadioService radioService)
        {
            _RegionManager = regionManager;
            _RadioService = radioService;
        }

        public Uri GetVoiceCommandGrammarFileUri(Language language)
        {
            switch (language.LanguageTag)
            {
                case "en-CA": return new Uri("ms-appx:///SmartGlass.Radio/Voice/commands.en-CA.xml");
                case "fr-CA": return new Uri("ms-appx:///SmartGlass.Radio/Voice/commands.fr-CA.xml");
            }

            return null;
        }

        public async Task<bool> ProcessRecognizedVoiceCommandAsync(string command, IReadOnlyDictionary<string, string> tags)
        {
            if (!tags.ContainsKey("Module") || tags["Module"] != "radio")
                return false;

            if (!tags.ContainsKey("Action"))
                return false;

            switch (tags["Action"])
            {
                case "play":
                    {
                        int index = 0;
                        if (tags.ContainsKey("Index"))
                        {
                            if (int.TryParse(tags["Index"], out index))
                            {
                                index--;
                            }
                        }

                        if (index >= 0 && index < _RadioService.RadioChannels.Count)
                        {
                            _RadioService.SelectedRadioChannel = _RadioService.RadioChannels[index];
                        }

                        _RadioService.IsPlaying = true;
                        await _RegionManager.ActivateRegionViewAsync(ERegionLocation.Center, ViewNames.RadioViewName);
                    }
                    break;

                case "stop":
                    _RadioService.IsPlaying = false;
                    break;

                case "resume":
                    _RadioService.IsPlaying = true;
                    await _RegionManager.ActivateRegionViewAsync(ERegionLocation.Center, ViewNames.RadioViewName);
                    break;

                case "setVolume":
                    {
                        if (tags.ContainsKey("VolumeLevel"))
                        {
                            var level = tags["VolumeLevel"];

                            var volume = _RadioService.Volume;

                            if (level == "higher")
                            {
                                volume = Math.Min(1.0, volume + 0.1);
                            }
                            else if (level == "lower")
                            {
                                volume = Math.Max(0.1, volume - 0.1);
                            }
                            else
                            {
                                if (double.TryParse(level, out volume))
                                {
                                    volume /= 100;
                                }
                            }

                            _RadioService.Volume = volume;
                        }
                    }
                    break;
            }

            return true;
        }
    }
}
