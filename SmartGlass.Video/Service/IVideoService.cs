using SmartGlass.Video.Models;
using System;
using System.Collections.Generic;

namespace SmartGlass.Video.Service
{
    public enum VideoPlaybackState
    {
        Stopped,
        Playing,
        Paused
    }

    public interface IVideoService
    {
        IReadOnlyList<VideoChannel> VideoChannels { get; }

        VideoChannel SelectedVideoChannel { get; set; }
        event EventHandler<VideoChannel> SelectedVideoChannelChanged;

        VideoPlaybackState PlaybackState { get; set; }
        event EventHandler<VideoPlaybackState> VideoPlaybackStateChanged;

        bool IsMuted { get; set; }
        event EventHandler<bool> VideoIsMutedVolumeChanged;

        int Volume { get; set; }
        event EventHandler<int> VideoPlaybackVolumeChanged;
    }
}
