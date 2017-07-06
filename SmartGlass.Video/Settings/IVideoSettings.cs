using SmartGlass.Video.Models;
using System.Collections.Generic;

namespace SmartGlass.Video.Settings
{
    public interface IVideoSettings
    {
        IEnumerable<VideoChannel> VideoChannels { get; }
    }
}
