using SmartGlass.Video.Models;
using SmartGlass.Video.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartGlass.Video.Service
{
    public class VideoService : IVideoService
    {
        private readonly List<VideoChannel> _VideoChannels;
        public IReadOnlyList<VideoChannel> VideoChannels => _VideoChannels;

        private VideoChannel _SelectedVideoChannel;
        public VideoChannel SelectedVideoChannel
        {
            get { return _SelectedVideoChannel; }
            set
            {
                if (_SelectedVideoChannel != value)
                {
                    _SelectedVideoChannel = value;
                    SelectedVideoChannelChanged?.Invoke(this, value);
                }
            }
        }

        private VideoPlaybackState _State;
        public VideoPlaybackState PlaybackState
        {
            get { return _State; }
            set
            {
                if (_State != value)
                {
                    _State = value;
                    VideoPlaybackStateChanged?.Invoke(this, value);
                }
            }
        }

        private int _Volume;
        public int Volume
        {
            get { return _Volume; }
            set
            {
                if (_Volume != value)
                {
                    _Volume = value;
                    VideoPlaybackVolumeChanged?.Invoke(this, value);

                    if (value != 0)
                        IsMuted = false;
                }
            }
        }

        private bool _IsMuted;
        public bool IsMuted
        {
            get { return _IsMuted; }
            set
            {
                if (_IsMuted != value)
                {
                    _IsMuted = value;
                    VideoIsMutedVolumeChanged?.Invoke(this, value);
                }
            }
        }

        public event EventHandler<VideoChannel> SelectedVideoChannelChanged;
        public event EventHandler<VideoPlaybackState> VideoPlaybackStateChanged;
        public event EventHandler<int> VideoPlaybackVolumeChanged;
        public event EventHandler<bool> VideoIsMutedVolumeChanged;

        public VideoService(IVideoSettings settings)
        {
            _VideoChannels = new List<VideoChannel>();
            _VideoChannels.AddRange(settings.VideoChannels);

            SelectedVideoChannel = _VideoChannels.First();

            Volume = 50;
        }
    }
}
