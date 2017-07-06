using SmartGlass.Video.Models;
using SmartGlass.Video.Service;
using System.Collections.ObjectModel;
using System.Linq;

namespace SmartGlass.Video.DesignViewModels
{
    internal class VideoViewModel
    {
        public ObservableCollection<VideoChannel> VideoChannels { get; }
        public VideoChannel ActiveVideoChannel { get; }
        public VideoPlaybackState PlaybackState { get; }
        public int Volume { get; }
        public bool IsMuted { get; }

        public VideoViewModel()
        {
            VideoChannels = new ObservableCollection<VideoChannel>();
            VideoChannels.Add(new VideoChannel("name 1", ""));
            VideoChannels.Add(new VideoChannel("name 2", ""));
            VideoChannels.Add(new VideoChannel("name 3", ""));
            VideoChannels.Add(new VideoChannel("name 4", ""));

            ActiveVideoChannel = VideoChannels.First();

            PlaybackState = VideoPlaybackState.Playing;
            Volume = 10;
            IsMuted = false;
        }
    }
}
