using SmartGlass.Core.Commanding;
using SmartGlass.Core.UI.Regions;
using SmartGlass.Video.Service;
using SmartGlass.Video.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Globalization;

namespace SmartGlass.Video.Voice
{
    public class VideoVoiceCommandProcessor : IVoiceCommandProcessor
    {
        private IRegionManager _RegionManager;
        private IVideoService _VideoService;

        public VideoVoiceCommandProcessor(IRegionManager regionManager, IVideoService videoService)
        {
            _RegionManager = regionManager;
            _VideoService = videoService;
        }

        public Uri GetVoiceCommandGrammarFileUri(Language language)
        {
            switch (language.LanguageTag)
            {
                case "en-CA": return new Uri("ms-appx:///SmartGlass.Video/Voice/commands.en-CA.xml");
                case "fr-CA": return new Uri("ms-appx:///SmartGlass.Video/Voice/commands.fr-CA.xml");
            }

            return null;
        }

        public async Task<bool> ProcessRecognizedVoiceCommandAsync(string command, IReadOnlyDictionary<string, string> tags)
        {
            if (!tags.ContainsKey("Module") || tags["Module"] != "video")
                return false;

            if (!tags.ContainsKey("Action"))
                return false;

            switch (tags["Action"])
            {
                case "list":
                    _VideoService.PlaybackState = VideoPlaybackState.Paused;
                    await _RegionManager.ActivateRegionViewAsync(ERegionLocation.Center, ViewNames.VideoViewName);
                    break;

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

                        if (index >= 0 && index < _VideoService.VideoChannels.Count)
                        {
                            _VideoService.SelectedVideoChannel = _VideoService.VideoChannels[index];
                        }

                        _VideoService.PlaybackState = VideoPlaybackState.Playing;
                        await _RegionManager.ActivateRegionViewAsync(ERegionLocation.Center, ViewNames.VideoViewName);
                    }
                    break;

                case "pause":
                    _VideoService.PlaybackState = VideoPlaybackState.Paused;
                    break;

                case "stop":
                    _VideoService.PlaybackState = VideoPlaybackState.Stopped;
                    break;

                case "resume":
                    _VideoService.PlaybackState = VideoPlaybackState.Playing;
                    await _RegionManager.ActivateRegionViewAsync(ERegionLocation.Center, ViewNames.VideoViewName);
                    break;

                case "setVolume":
                    {
                        if (tags.ContainsKey("VolumeLevel"))
                        {
                            var level = tags["VolumeLevel"];

                            var volume = _VideoService.Volume;

                            if (level == "higher")
                            {
                                volume = Math.Min(100, volume + 10);
                            }
                            else if (level == "lower")
                            {
                                volume = Math.Max(0, volume - 10);
                            }
                            else
                            {
                                int.TryParse(level, out volume);
                            }

                            _VideoService.Volume = volume;
                        }
                    }
                    break;

                case "muteUnmute":
                    {
                        var level = tags["VolumeLevel"];

                        if (level == "mute")
                        {
                            _VideoService.IsMuted = true;
                        }
                        else if (level == "unmute")
                        {
                            _VideoService.IsMuted = false;
                        }
                    }
                    break;

                default:
                    return false;
            }

            return true;
        }
    }
}
