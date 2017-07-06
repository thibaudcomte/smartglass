using SmartGlass.Video.Service;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SmartGlass.Video.Views
{
    public sealed partial class VideoView : UserControl
    {
        public VideoView(IVideoService service)
        {
            InitializeComponent();

            service.VideoPlaybackStateChanged += Service_VideoPlaybackStateChanged;
        }

        private async void Service_VideoPlaybackStateChanged(object sender, VideoPlaybackState state)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                VisualStateManager.GoToState(this, state == VideoPlaybackState.Playing ? "ShowVideo" : "PickChannel", true);
            });
        }
    }
}
