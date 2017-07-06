using Prism.Mvvm;
using SmartGlass.Core.UI.Regions;
using SmartGlass.Video.Models;
using SmartGlass.Video.Service;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace SmartGlass.Video.ViewModels
{
    internal class VideoViewModel : BindableBase, IRegionActivationAware
    {
        public ObservableCollection<VideoChannel> VideoChannels { get; }

        private VideoChannel _ActiveVideoChannel;
        public VideoChannel ActiveVideoChannel
        {
            get { return _ActiveVideoChannel; }
            set { SetProperty(ref _ActiveVideoChannel, value); }
        }

        private VideoPlaybackState _PlaybackState;
        public VideoPlaybackState PlaybackState
        {
            get { return _PlaybackState; }
            set { SetProperty(ref _PlaybackState, value); }
        }

        private int _Volume;
        public int Volume
        {
            get { return _Volume; }
            set { SetProperty(ref _Volume, value); }
        }

        private bool _IsMuted;
        public bool IsMuted
        {
            get { return _IsMuted; }
            set { SetProperty(ref _IsMuted, value); }
        }

        private readonly IVideoService _Service;
        private readonly SynchronizationContext _SynchronizationContext;

        public VideoViewModel(IVideoService service)
        {
            _Service = service;
            _SynchronizationContext = SynchronizationContext.Current;

            VideoChannels = new ObservableCollection<VideoChannel>();
            foreach (var channel in service.VideoChannels)
            {
                VideoChannels.Add(channel);
            }

            ActiveVideoChannel = service.SelectedVideoChannel;
            Volume = service.Volume;
            PlaybackState = service.PlaybackState;
            IsMuted = service.IsMuted;

            service.SelectedVideoChannelChanged += SelectedVideoChannelChanged;
            service.VideoPlaybackStateChanged += VideoPlaybackStateChanged;
            service.VideoPlaybackVolumeChanged += VideoPlaybackVolumeChanged;
            service.VideoIsMutedVolumeChanged += VideoIsMutedVolumeChanged;
        }

        private void VideoIsMutedVolumeChanged(object sender, bool isMuted)
        {
            _SynchronizationContext.Post((o) => { IsMuted = isMuted; }, null);
        }

        private void VideoPlaybackStateChanged(object sender, VideoPlaybackState state)
        {
            _SynchronizationContext.Post((o) => { PlaybackState = state; }, null);
        }

        private void VideoPlaybackVolumeChanged(object sender, int volume)
        {
            _SynchronizationContext.Post((o) => { Volume = volume; }, null);
        }

        private void SelectedVideoChannelChanged(object sender, VideoChannel channel)
        {
            _SynchronizationContext.Post((o) => { ActiveVideoChannel = channel; }, null);
        }

        public Task OnBeforeActivatedAsync()
        {
            return Task.CompletedTask;
        }

        public Task OnAfterActivatedAsync()
        {
            return Task.CompletedTask;
        }

        public Task OnBeforeDeactivatedAsync()
        {
            _Service.PlaybackState = VideoPlaybackState.Stopped;
            return Task.CompletedTask;
        }

        public Task OnAfterDeactivatedAsync()
        {
            return Task.CompletedTask;
        }
    }
}
