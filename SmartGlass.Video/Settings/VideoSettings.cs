using SmartGlass.Video.Models;
using System.Collections.Generic;

namespace SmartGlass.Video.Settings
{
    public class VideoSettings : IVideoSettings
    {
        private readonly List<VideoChannel> _VideoChannels;
        public IEnumerable<VideoChannel> VideoChannels => _VideoChannels;

        public VideoSettings()
        {
            _VideoChannels = new List<VideoChannel>();

            _VideoChannels.Add(new VideoChannel("france 24 live", "Fwxuzl4ZrHo"));
            _VideoChannels.Add(new VideoChannel("euronews en direct", "q5H4bPSXthM"));
            _VideoChannels.Add(new VideoChannel("tropical reef livecam", "1tyxTfhlDmk"));
            _VideoChannels.Add(new VideoChannel("brooks fall livecam", "pHvmGucGm_E"));

        }
    }
}
